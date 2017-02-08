using System;
using System.Runtime.InteropServices;
using SDL2;
using System.Diagnostics;

namespace CloudsSharp
{
	public class RenderManager
	{
		/*
		//variable(s)
		SDL_Surface* buffer;
		SDL_Surface* screen;
		gfont myfont;
		*/

		private SDL2.SDL.SDL_Surface sScreen;
		private SDL2.SDL.SDL_Surface sBuffer;
		private Font fFont;

		public RenderManager()
		{
			SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING);

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
			SDL2.SDL.SDL_Rect temp = new SDL.SDL_Rect();

			temp.x = (16 * (theplayer.x - camera_pos)) + (theplayer.small_x - camera_pos_small);
			temp.y = (16 * theplayer.y) + theplayer.small_y;

			if (theplayer.active)
			{
				SDL2.SDL.SDL_BlitSurface(gametexman.tex_list[theplayer.texture_id], null, sBuffer, temp);

				#if (GAMEDEBUG)
					circleColor(buffer, temp.x + 8, temp.y + 8, 8, 250);
				#endif
			}
		}
    }
}
