using System;

namespace Ywxt.Cens.Core.Rom
{
    [Flags]
    public enum CtrlFlags : byte
    {
        /// <summary>
        /// 添加256到X滚动位置
        /// </summary>
        XScroll = 1,

        /// <summary>
        /// 添加256到Y滚动位置
        /// </summary>
        YScroll = 1 << 1,

        /// <summary>
        /// VRAM address increment per CPU read/write of PPUDATA
        /// </summary>
        VramAddressIncrement = 1 << 2,

        /// <summary>
        /// Sprite pattern table address for 8x8 sprites
        /// </summary>
        SpritePatternTableAddress = 1 << 3,

        /// <summary>
        /// Background pattern table address (0: $0000; 1: $1000)
        /// </summary>
        BackgroundPatternTableAddress = 1 << 4,

        /// <summary>
        /// Sprite size (0: 8x8 pixels; 1: 8x16 pixels)
        /// </summary>
        SpriteSize = 1 << 5,

        /// <summary>
        /// PPU master/slave select
        /// (0: read backdrop from EXT pins; 1: output color on EXT pins)
        /// </summary>
        PpuExtMode = 1 << 6,

        /// <summary>
        /// Generate an NMI at the start of the
        /// vertical blanking interval (0: off; 1: on)
        /// </summary>
        NmiAtStart = 1 << 7,
    }
}