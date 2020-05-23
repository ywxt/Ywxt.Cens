using System;

namespace Ywxt.Cens.Core.Cpu
{
    [Flags]
    public enum PFlags : byte
    {
        /// <summary>
        /// 进位标志
        /// </summary>
        C = 1,

        /// <summary>
        /// 零标志，计算结果为0
        /// </summary>
        Z = 1 << 1,

        /// <summary>
        /// 中断使能，屏蔽IRQ中断
        /// </summary>
        I = 1 << 2,

        /// <summary>
        /// 十进制标志，未使用
        /// </summary>
        D = 1 << 3,

        B = 1 << 4,

        /// <summary>
        /// 未使用，为1
        /// </summary>
        U = 1 << 5,

        /// <summary>
        /// 溢出标志
        /// </summary>
        V = 1 << 6,

        /// <summary>
        /// 负标志
        /// </summary>
        N = 1 << 7,
    }
}