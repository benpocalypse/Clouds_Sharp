using System;
namespace CloudsSharp
{
	public class Font
	{
		// SDL_Surface * font;

		private SDL2.SDL.SDL_Surface fFont;

		public Font()
		{
			//font = IMG_Load("data/fonts/realfont.png");
			//SDL_SetColorKey(font, SDL_SRCCOLORKEY, SDL_MapRGB(font->format, 255, 0, 255));
		}

		public void Print(SDL2.SDL.SDL_Surface surf, string sText, int x, int y)
		{
			/*
			SDL_Rect sourcetemp;
			SDL_Rect desttemp;

			desttemp.y = y;
			desttemp.x = x;
			sourcetemp.w = 8;
			sourcetemp.h = 8;
			for (int i = 0; i < length; i++)
			{
				sourcetemp.x = (16 * (text[i] % 16)) / 2;
				sourcetemp.y = (16 * ((int)floor(text[i] / 16))) / 2;
				SDL_BlitSurface(font, &sourcetemp, surf, &desttemp);
				desttemp.x += 8;
			}
			*/
		}
	}
}
