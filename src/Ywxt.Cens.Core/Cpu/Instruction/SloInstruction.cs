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
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0x07, (AddressingMode.ZeroPageAddressingMode, InstructionType.Common, 5)},
                {0x17, (AddressingMode.ZeroPageXAddressingMode, InstructionType.Common, 6)},
                {0x0F, (AddressingMode.AbsoluteAddressingMode, InstructionType.Common, 6)},
                {0x1F, (AddressingMode.AbsoluteXAddressingMode, InstructionType.Common, 7)},
                {0x1B, (AddressingMode.AbsoluteYAddressingMode, InstructionType.Common, 7)},
                {0x03, (AddressingMode.IndirectXAddressingMode, InstructionType.Common, 8)},
                {0x13, (AddressingMode.IndirectYAddressingMode, InstructionType.Common, 8)}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            var data = cpu.Bus.ReadByte(address);
            cpu.CpuRegisters.SetCFlag(data >> 7 == 1);
            data <<= 1;
            cpu.Bus.WriteByte(address, data);
            cpu.CpuRegisters.A |= data;
            cpu.CpuRegisters.SetZAndNFlags(cpu.CpuRegisters.A);
            return 0;
        }
    }
}