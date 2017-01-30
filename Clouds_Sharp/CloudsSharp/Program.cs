using System;
using SDL2;

namespace CloudsSharp
{
	class MainClass
	{
		private GameManager gManager = new GameManager();

		public static void Main(string[] args)
		{
			SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING);
			IntPtr sdlWindow = SDL.SDL_CreateWindow("Test SDL",
											  100,
											  100,
											  800,
											  600,
											  SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE |
											  SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

			IntPtr sdlRenderer = SDL.SDL_CreateRenderer(sdlWindow, -1, 0);

			IntPtr sdlTexture = SDL.SDL_CreateTexture(sdlRenderer, SDL.SDL_PIXELFORMAT_ARGB8888, Convert.ToInt16(SDL.SDL_TextureAccess.SDL_TEXTUREACCESS_TARGET), 320, 240);

			SDL.SDL_SetHint(SDL.SDL_HINT_RENDER_SCALE_QUALITY, "linear");  // make the scaled rendering look smoother.
			SDL.SDL_RenderSetLogicalSize(sdlRenderer, 320, 240);

			SDL.SDL_SetRenderDrawColor(sdlRenderer, 0, 0, 0, 255);
			SDL.SDL_RenderClear(sdlRenderer);
			SDL.SDL_RenderPresent(sdlRenderer);

			//SDL.SDL_Surface SurfaceStruct = SurfaceIntPtr.Struct<SDL.SDL_Surface>(sdlTexture);

			Console.ReadLine();

			SDL.SDL_Quit();
		}


	}

	public static class SurfaceIntPtr
	{
		public static T Struct<T>(this IntPtr pointer)
		{
			return (T)System.Runtime.InteropServices.Marshal.PtrToStructure(pointer, typeof(T));
		}
	}
}
