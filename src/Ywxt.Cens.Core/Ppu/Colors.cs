using System;

namespace Ywxt.Cens.Core.Ppu
{
    public static class Colors
    {
        /// <summary>
        /// 调色板，64种颜色，RGBA
        /// </summary>
        public static readonly ReadOnlyMemory<uint> PaletteData =
            new uint[]
            {
                0x7F7F7FFF, 0x2000B0FF, 0x2800B8FF, 0x6010A0FF,
                0x982078FF, 0xB01030FF, 0xA03000FF, 0x784000FF,
                0x485800FF, 0x386800FF, 0x386C00FF, 0x306040FF,
                0x305080FF, 0x000000FF, 0x000000FF, 0x000000FF,

                0xBCBCBCFF, 0x4060F8FF, 0x4040FFFF, 0x9040F0FF,
                0xD840C0FF, 0xD84060FF, 0xE05000FF, 0xC07000FF,
                0x888800FF, 0x50A000FF, 0x48A810FF, 0x48A068FF,
                0x4090C0FF, 0x000000FF, 0x000000FF, 0x000000FF,

                0xFFFFFFFF, 0x60A0FFFF, 0x5080FFFF, 0xA070FFFF,
                0xF060FFFF, 0xFF60B0FF, 0xFF7830FF, 0xFFA000FF,
                0xE8D020FF, 0x98E800FF, 0x70F040FF, 0x70E090FF,
                0x60D0E0FF, 0x606060FF, 0x000000FF, 0x000000FF,

                0xFFFFFFFF, 0x90D0FFFF, 0xA0B8FFFF, 0xC0B0FFFF,
                0xE0B0FFFF, 0xFFB8E8FF, 0xFFC8B8FF, 0xFFD8A0FF,
                0xFFF090FF, 0xC8F080FF, 0xA0F0A0FF, 0xA0FFC8FF,
                0xA0FFF0FF, 0xA0A0A0FF, 0x000000FF, 0x000000FF
            };
    }
}