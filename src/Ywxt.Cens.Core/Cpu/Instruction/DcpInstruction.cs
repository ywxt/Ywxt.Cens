using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class DcpInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0xC7, (AddressingMode.ZeroPageAddressingMode, InstructionType.Common, )},
                {0xD7, (AddressingMode.ZeroPageXAddressingMode, InstructionType.Common, )},
                {0xCF, (AddressingMode.AbsoluteAddressingMode, InstructionType.Common, )},
                {0xDF, (AddressingMode.AbsoluteXAddressingMode, InstructionType.Common, )},
                {0xDB, (AddressingMode.AbsoluteYAddressingMode, InstructionType.Common, )},
                {0xC3, (AddressingMode.IndirectXAddressingMode, InstructionType.Common, )},
                {0xD3, (AddressingMode.IndirectYAddressingMode, InstructionType.Common, )}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            var data = this.ReadData(address, cpu, instruction);
            var tmp = unchecked((byte) (data - 1));
            cpu.Bus.WriteByte(address, tmp);
            var result = unchecked((ushort) (cpu.Registers.A - tmp));
            cpu.Registers.SetCFlag(result < 0x0100);

            cpu.Registers.SetZAndNFlags(unchecked((byte) result));
            return instruction switch
            {
                0xC7 => 5,
                0xD7 => 6,
                0xCF => 6,
                0xDF => 7,
                0xDB => 7,
                0xC3 => 8,
                0xD3 => 8,
                _ => 0
            };
        }
    }
}