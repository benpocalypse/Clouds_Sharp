using System;
using System.Runtime.InteropServices;
using SDL2;

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

		SDL2.SDL.SDL_Surface sScreen;
		SDL2.SDL.SDL_Surface sBuffer;

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

			//SDL.SDL_Surface SurfaceStruct = SurfaceIntPtr.Struct<SDL.SDL_Surface>(sdlTexture);

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

			SDL.SDL_FreeSurface(tempSurface);

			SDL.SDL_Surface sur;
			sur = (SDL.SDL_Surface)Marshal.PtrToStructure(
							textSurface,
							typeof(SDL_Surface)
						);
		}

	}
}
