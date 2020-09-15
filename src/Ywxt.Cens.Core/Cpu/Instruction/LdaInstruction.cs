using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class LdaInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0xA9, (AddressingMode.ImmediateAddressingMode, InstructionType.Common, 2)},
                {0xA5, (AddressingMode.ZeroPageAddressingMode, InstructionType.Common, 3)},
                {0xB5, (AddressingMode.ZeroPageXAddressingMode, InstructionType.Common, 4)},
                {0xAD, (AddressingMode.AbsoluteAddressingMode, InstructionType.Common, 4)},
                {0xBD, (AddressingMode.AbsoluteXAddressingMode, InstructionType.CrossingPage, 4)},
                {0xB9, (AddressingMode.AbsoluteYAddressingMode, InstructionType.CrossingPage, 4)},
                {0xA1, (AddressingMode.IndirectXAddressingMode, InstructionType.Common, 6)},
                {0xB1, (AddressingMode.IndirectYAddressingMode, InstructionType.CrossingPage, 5)}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            var data = this.ReadData(address, cpu, instruction);
            cpu.CpuRegisters.A = data;
            cpu.CpuRegisters.SetZAndNFlags(cpu.CpuRegisters.A);

            return 0;
        }
    }
}