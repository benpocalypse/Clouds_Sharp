using System;
using System.Runtime.InteropServices;
using SDL2;
using static SDL2.SDL;

namespace CloudsSharp
{
	public class MainClass
	{
        private static GameManager gManager;

        public static void Main(string[] args)
		{
            gManager = new GameManager();

            Console.ReadLine();

			SDL_Quit();
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
