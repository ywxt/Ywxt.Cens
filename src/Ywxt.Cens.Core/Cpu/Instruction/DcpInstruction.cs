using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class DcpInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0xC7, (AddressingMode.ZeroPageAddressingMode, InstructionType.Common, 5)},
                {0xD7, (AddressingMode.ZeroPageXAddressingMode, InstructionType.Common, 6)},
                {0xCF, (AddressingMode.AbsoluteAddressingMode, InstructionType.Common, 6)},
                {0xDF, (AddressingMode.AbsoluteXAddressingMode, InstructionType.Common, 7)},
                {0xDB, (AddressingMode.AbsoluteYAddressingMode, InstructionType.Common, 7)},
                {0xC3, (AddressingMode.IndirectXAddressingMode, InstructionType.Common, 8)},
                {0xD3, (AddressingMode.IndirectYAddressingMode, InstructionType.Common, 8)}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            var data = this.ReadData(address, cpu, instruction);
            var tmp = unchecked((byte) (data - 1));
            cpu.Bus.WriteByte(address, tmp);
            var result = unchecked((ushort) (cpu.CpuRegisters.A - tmp));
            cpu.CpuRegisters.SetCFlag(result < 0x0100);

            cpu.CpuRegisters.SetZAndNFlags(unchecked((byte) result));
            return 0;
        }
    }
}