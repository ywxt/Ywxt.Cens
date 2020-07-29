using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    /// <summary>
    /// Shift Left then 'Or' - ASL + ORA
    /// <br/>
    /// 助记符号: A |= (M &lt;&lt;= 1)
    /// </summary>
    public sealed class SloInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0x07, AddressingMode.ZeroPageAddressingMode},
                {0x17, AddressingMode.ZeroPageXAddressingMode},
                {0x0F, AddressingMode.AbsoluteAddressingMode},
                {0x1F, AddressingMode.AbsoluteXAddressingMode},
                {0x1B, AddressingMode.AbsoluteYAddressingMode},
                {0x03, AddressingMode.IndirectXAddressingMode},
                {0x13, AddressingMode.IndirectYAddressingMode}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address, bool pageCrossed)
        {
            var data = cpu.Bus.ReadByte(address);
            cpu.Registers.SetC(data >> 7 == 1);
            data <<= 1;
            cpu.Bus.WriteByte(address, data);
            cpu.Registers.A |= data;
            cpu.Registers.SetZAndN(cpu.Registers.A);
            return instruction switch
            {
                0x07 => 5,
                0x17 => 6,
                0x0F => 6,
                0x1F => 7,
                0x1B => 7,
                0x03 => 8,
                0x13 => 8,
                _ => 0
            };
        }
    }
}