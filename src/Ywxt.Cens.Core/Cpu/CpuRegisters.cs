namespace Ywxt.Cens.Core.Cpu
{
    public class CpuRegisters
    {
        /// <summary>
        /// 累加器
        /// </summary>
        public byte A { get; set; }

        /// <summary>
        /// 循环计数器
        /// </summary>
        public byte X { get; set; }

        /// <summary>
        /// 循环计数器
        /// </summary>
        public byte Y { get; set; }

        /// <summary>
        /// 程序计数器
        /// </summary>
        public ushort Pc { get; set; }

        /// <summary>
        /// 堆栈寄存器，其值为 0x00 ~ 0xFF，对应着 CPU 总线上的 0x100 ~ 0x1FF
        /// </summary>
        public byte Sp { get; set; }

        /// <summary>
        /// 标志寄存器
        /// </summary>
        public PFlags P { get; set; }
    }
}