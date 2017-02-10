using System;
using System.Runtime.InteropServices;
using SDL2;
using System.Diagnostics;
using static SDL2.SDL;
using System.Collections.Generic;

namespace CloudsSharp
{
	public class RenderManager
	{
		private SDL_Surface sScreen;
		private SDL_Surface sBuffer = new SDL_Surface();
        private IntPtr sdlRenderer = new IntPtr();
        private IntPtr sdlWindow = new IntPtr();
        private Font fFont;

		public RenderManager()
		{
            SDL.SDL_Init(SDL.SDL_INIT_VIDEO);// | SDL_INIT_TIMER | SDL_INIT_AUDIO);//.SDL_INIT_EVERYTHING);
            /*
            IntPtr sdlWindow = SDL.SDL_CreateWindow("Test SDL",
                                              100,
                                              100,
                                              800,
                                              600,
                                              SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE |
                                              SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);
            */

            SDL_CreateWindowAndRenderer(640, 480, SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE | SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN, out sdlWindow, out sdlRenderer);

            //IntPtr sdlTexture = SDL.SDL_CreateTexture(sdlRenderer, SDL.SDL_PIXELFORMAT_ARGB8888, Convert.ToInt16(SDL.SDL_TextureAccess.SDL_TEXTUREACCESS_TARGET), 320, 240);

            SDL.SDL_SetHint(SDL.SDL_HINT_RENDER_SCALE_QUALITY, "linear");  // make the scaled rendering look smoother.
            SDL.SDL_RenderSetLogicalSize(sdlRenderer, 320, 240);

            SDL.SDL_SetRenderDrawColor(sdlRenderer, 0, 0, 0, 255);
            SDL.SDL_RenderClear(sdlRenderer);
            SDL.SDL_RenderPresent(sdlRenderer);

            //SDL_SetWindowIcon(sdlWindow, SDL_image.IMG_Load("data/misc/icon.png"));

            /*
			if (SDL_Init(SDL_INIT_VIDEO | SDL_INIT_TIMER | SDL_INIT_AUDIO) < 0)
			{
				printf("Unable to initialize SDL: %s\n", SDL_GetError());
				return 0;
			}

			atexit(SDL_Quit);

			SDL_WM_SetCaption("Clouds", "Clouds");
			SDL_WM_SetIcon(IMG_Load("data/misc/icon.png"), NULL);

			buffer = SDL_CreateRGBSurface(SDL_HWSURFACE, 320, 240, 32,
										   0, 0, 0, 0);

			screen = SDL_SetVideoMode(640, 480, 32,
						SDL_HWSURFACE | SDL_DOUBLEBUF);// | SDL_FULLSCREEN);

			if ((screen == NULL) || (buffer == NULL))
			{
				printf("Unable to set 640x480x32 video: %s\n", SDL_GetError());
				return 0;
			}
			else
				return 1;
			*/
        }

		public void DrawScreen(string sName)
		{
            SDL.SDL_Rect tempRect = new SDL.SDL_Rect();
			SDL.SDL_Surface tempSurface;

			tempSurface = SurfaceIntPtr.Struct<SDL.SDL_Surface>(SDL_image.IMG_Load(sName));
			tempRect.x = 0;
			tempRect.y = 0;

			IntPtr ptrSurface = new IntPtr();
			IntPtr ptrBuffer = new IntPtr();
			IntPtr ptrRect = new IntPtr();

			Marshal.StructureToPtr(tempSurface, ptrSurface, false);
			Marshal.StructureToPtr(sBuffer, ptrBuffer, false);
			Marshal.StructureToPtr(tempRect, ptrRect, false);

			SDL.SDL_BlitSurface(ptrSurface, IntPtr.Zero, ptrBuffer, ref tempRect);

            SDL.SDL_FreeSurface(ptrSurface);
		}


        public void DrawMap(GameMap gamemap, TextureManager gametexman, int xoffset, int xoffset_small)
        {
            SDL.SDL_Rect temp;
            int guess;
            int guess_small;

            //HACK!!!
            //clear the buffer to black
            //SDL_FillRect(buffer, NULL, SDL_MapRGB(buffer->format,0,0,0));
            //HACK!!

            for (int i = 1; i < gamemap.NumLayers; i++)
            {
                guess_small = ((16 * xoffset + xoffset_small) / (gamemap.NumLayers - i)) % 16;
                guess = (xoffset) / (gamemap.NumLayers - i);

                for (int k = 0; k < gamemap.Height; k++)
                {
                    for (int j = guess; j < guess + 21; j++)
                    {
                        temp.x = 16 * (j - guess) - guess_small;
                        temp.y = 16 * k;
                        if (j < gamemap.Width)
                            if (gamemap.Data[i][j][k] != 0)
                                Debug.WriteLine("blarg!");
                                //SDL_BlitSurface(gametexman->tex_list[((gamemap->data[i][j][k]) - 1)], NULL, buffer, &temp);
                    }
                }
            }
        }

		public void Print(string sText, int x, int y)
		{
			fFont.Print(sBuffer, sText, x, y);
		}

		public void DrawPlayer(GObject theplayer, TextureManager gametexman,
								int camera_pos, int camera_pos_small)
		{
			SDL_Rect temp = new SDL_Rect();

			temp.x = (16 * (theplayer.x - camera_pos)) + (theplayer.small_x - camera_pos_small);
			temp.y = (16 * theplayer.y) + theplayer.small_y;

			if (theplayer.active)
			{
                IntPtr ptrTexture = new IntPtr();
                IntPtr ptrBuffer = new IntPtr();
                SDL_Rect tmpRect = new SDL_Rect();

                Marshal.StructureToPtr(gametexman.lsTextureList[theplayer.texture_id], ptrTexture, false);
                Marshal.StructureToPtr(sBuffer, ptrBuffer, false);

                SDL.SDL_BlitSurface(ptrTexture, ref tmpRect, ptrBuffer, ref temp);

				#if (GAMEDEBUG)
					circleColor(buffer, temp.x + 8, temp.y + 8, 8, 250);
				#endif
			}
		}


        public void DrawEnemies(List<GObject> theenemies, TextureManager gametexman, int numenemies,
                                 int camera_pos, int camera_pos_small)
        {
            SDL_Rect temp = new SDL_Rect();

            for (int i = 0; i < numenemies; i++)
            {
                if (theenemies[i].active)
                {
                    IntPtr ptrTexture = new IntPtr();
                    IntPtr ptrBuffer = new IntPtr();
                    SDL_Rect tmpRect = new SDL_Rect();

                    Marshal.StructureToPtr(gametexman.lsTextureList[theenemies[i].texture_id], ptrTexture, false);
                    Marshal.StructureToPtr(sBuffer, ptrBuffer, false);

                    temp.x = (16 * (theenemies[i].x - camera_pos)) + (theenemies[i].small_x - camera_pos_small);
                    temp.y = (16 * (theenemies[i].y)) + theenemies[i].small_y;

                    SDL_BlitSurface(ptrTexture, ref tmpRect, ptrBuffer, ref temp);

                    #if GAMEDEBUG
                        circleColor(buffer, temp.x + 8, temp.y + 8, 8, 250);
                    #endif
                }
            }
        }

        
        public void DrawBullets(List<GObject> thebullets, TextureManager gametexman,
                                int camera_pos, int camera_pos_small)
        {
            SDL_Rect temp = new SDL_Rect();
            for (int i = 0; i < thebullets.Count; i++)
            {
                if (thebullets[i].active)
                {
                    IntPtr ptrTexture = new IntPtr();
                    IntPtr ptrBuffer = new IntPtr();
                    SDL_Rect tmpRect = new SDL_Rect();

                    Marshal.StructureToPtr(gametexman.lsTextureList[thebullets[i].texture_id], ptrTexture, false);
                    Marshal.StructureToPtr(sBuffer, ptrBuffer, false);

                    temp.x = (16 * (thebullets[i].x - camera_pos) + thebullets[i].small_x - camera_pos_small);
                    temp.y = (16 * (thebullets[i].y)) + thebullets[i].small_y;

                    SDL_BlitSurface(ptrTexture, ref tmpRect, ptrBuffer, ref temp);

                    #if GAMEDEBUG
                        circleColor(buffer, temp.x + 8, temp.y + 8, 5, 250);
                    #endif
                }
            }
        }

        public void DrawExplosion(List<Explosion> explosions)
        {
            SDL_Rect temp = new SDL_Rect();
            SDL_Surface surf;

            for (int i = 0; i < explosions.Count; i++)
            {
                for (int j = 0; j < explosions[i].bits.Count; j++)
                {
                    if (explosions[i].active)
                    {
                        if (explosions[i].bits[j].active)
                        {
                            temp.x = explosions[i].bits[j].x;
                            temp.y = explosions[i].bits[j].y;

                            /*
                            surf = SDL_CreateRGBSurface(SDL_HWSURFACE, explosions[i].bits[j].health,
                                                        explosions[i].bits[j].health, 32, 0, 0, 0, 0);
                            
                            circleRGBA(buffer, temp.x, temp.y, explosions[i].bits[j].health, 255, 90, 0, 125);

                            SDL_FillRect(surf, NULL, SDL_MapRGB(surf->format, (explosions[i].bits[j].health + 5) * 25, (explosions[i].bits[j].health + 5) * 9, 0));
                            SDL_BlitSurface(surf, NULL, buffer, &temp);
                            SDL_FreeSurface(surf);
                            */
                        }
                    }
                }
            }
        }

        public void DrawPowerup(PowerupManager gpower, TextureManager gametexman)
        {
            SDL_Rect temp = new SDL_Rect();

            for (int i = 0; i < Globals.MAXPOWERUPS; i++)
            {
                if (gpower.powerups[i].active)
                {
                    IntPtr ptrTexture = new IntPtr();
                    IntPtr ptrBuffer = new IntPtr();
                    SDL_Rect tmpRect = new SDL_Rect();

                    Marshal.StructureToPtr(gametexman.lsTextureList[gpower.powerups[i].texture_id], ptrTexture, false);
                    Marshal.StructureToPtr(sBuffer, ptrBuffer, false);

                    temp.x = gpower.powerups[i].x;
                    temp.y = gpower.powerups[i].y;
                    SDL_SetSurfaceAlphaMod(ptrTexture, (byte)(gpower.powerups[i].health + 105));

                    SDL_BlitSurface(ptrTexture, ref tmpRect, ptrBuffer, ref temp);
                }
            }
        }

        public void DrawHud(GObject obj, Player player)
        {//^ //_
            char []buffer = new char[5];

            //draw Health meter
            for (int i = 0; i < 10; i++)
            {
                if (((10 * obj.health) / Globals.PLAYERBASEHEALTH) > ((10 * i) / Globals.PLAYERBASEHEALTH))
                    Print("_", 8 + (2 * i), 2);
                else
                    Print("`", 8 + (2 * i), 2);
            }

            //draw power meter
            for (int i = 0; i < 10; i++)
            {
                if (((10 * player.Power) / Globals.MAXPOWER) > ((i)))
                    Print("^", 8 + (2 * i), 8);
                else
                    Print("`", 8 + (2 * i), 8);
            }

            //draw spare lives
            for (int i = 0; i < player.Lives; i++)
            {
                Print(";", 38 + (8 * i), 5);
            }

            //show the score
            Print("Score:", 230, 2);
            Print(player.Score.ToString(), 278, 2);
        }

        public void Flip()
        {
            IntPtr ptrScreen = new IntPtr();
            IntPtr ptrBuffer = new IntPtr();
            SDL_Rect tmpSrcRect = new SDL_Rect();
            SDL_Rect tmpDstRect = new SDL_Rect();

            Marshal.StructureToPtr(sScreen, ptrScreen, false);
            Marshal.StructureToPtr(sBuffer, ptrBuffer, false);

            //temp = zoomSurface(buffer, 2, 2, 0);
            SDL_BlitSurface(ptrBuffer, ref tmpSrcRect, sdlRenderer, ref tmpDstRect);

            SDL.SDL_RenderClear(sdlRenderer);
            SDL.SDL_RenderPresent(sdlRenderer);
            //SDL_FreeSurface(temp);
            //SDL_Flip(screen);
        }
    }
}
