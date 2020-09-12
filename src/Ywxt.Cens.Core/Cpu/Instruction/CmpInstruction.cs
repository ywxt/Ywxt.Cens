using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class CmpInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0xC9, (AddressingMode.ImmediateAddressingMode, InstructionType.Common, 2)},
                {0xC5, (AddressingMode.ZeroPageAddressingMode, InstructionType.Common, 3)},
                {0xD5, (AddressingMode.ZeroPageXAddressingMode, InstructionType.Common, 4)},
                {0xCD, (AddressingMode.AbsoluteAddressingMode, InstructionType.Common, 4)},
                {0xDD, (AddressingMode.AbsoluteXAddressingMode, InstructionType.CrossingPage, 4)},
                {0xD9, (AddressingMode.AbsoluteYAddressingMode, InstructionType.CrossingPage, 4)},
                {0xC1, (AddressingMode.IndirectXAddressingMode, InstructionType.Common, 6)},
                {0xD1, (AddressingMode.IndirectYAddressingMode, InstructionType.CrossingPage, 5)}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            var data = this.ReadData(address, cpu, instruction);
            var result = cpu.Registers.A - data;
            cpu.Registers.SetZAndNFlags(unchecked((byte) result));
            cpu.Registers.SetCFlag(result >= 0);

            return 0;
        }
    }
}