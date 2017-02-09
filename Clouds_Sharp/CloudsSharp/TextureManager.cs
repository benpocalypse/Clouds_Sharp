using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SDL2.SDL;

namespace CloudsSharp
{
    public class TextureManager
    {
        public List<SDL2.SDL.SDL_Surface> lsTextureList;

        public TextureManager()
        {
            lsTextureList = new List<SDL2.SDL.SDL_Surface>();
        }

        public void AddTexture(string sTextureFile)
        {
            SDL_Surface tmpSurface = SurfaceIntPtr.Struct<SDL_Surface>(SDL_image.IMG_Load(sTextureFile));

            lsTextureList.Add(tmpSurface);
        }

        public void RemoveAll()
        {
            lsTextureList.Clear();
        }
    }
}
