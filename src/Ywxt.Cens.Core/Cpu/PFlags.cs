using System;

namespace Ywxt.Cens.Core.Cpu
{
    [Flags]
    public enum PFlags : byte
    {
        /// <summary>
        /// 进位标志
        /// </summary>
        C = 0b0000_00000,

        /// <summary>
        /// 零标志，计算结果为0
        /// </summary>
        Z = 0b0000_0010,

        /// <summary>
        /// 中断使能，屏蔽IRQ中断
        /// </summary>
        I = 0b0000_0100,

        /// <summary>
        /// 十进制标志，未使用
        /// </summary>
        D = 0b0000_1000,

        B = 0b0001_0000,
        /// <summary>
        /// 未使用，为1
        /// </summary>
        U = 0b0010_0000,
        /// <summary>
        /// 溢出标志
        /// </summary>
        V = 0b0100_0000,
        /// <summary>
        /// 负标志
        /// </summary>
        N = 0b1000_0000,
    }
}