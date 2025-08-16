using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary
{
    public class CoreConfig
    {
        public string Title { get; set; } = "Game Title";
        public int Width { get; set; } = 1920;
        public int Height { get; set; } = 1080;
        public bool FullScreen { get; set; } = false;
    }

}
