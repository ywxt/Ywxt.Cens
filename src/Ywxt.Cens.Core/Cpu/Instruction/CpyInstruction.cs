using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class CpyInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0xC0, (AddressingMode.ImmediateAddressingMode, InstructionType.Common, 2)},
                {0xC4, (AddressingMode.ZeroPageAddressingMode, InstructionType.Common, 3)},
                {0xCC, (AddressingMode.AbsoluteAddressingMode, InstructionType.Common, 4)}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            var data = this.ReadData(address, cpu, instruction);
            var result = cpu.Registers.Y - data;
            cpu.Registers.SetZAndNFlags(unchecked((byte) result));
            cpu.Registers.SetCFlag(result >= 0);

            return 0;
        }
    }
}