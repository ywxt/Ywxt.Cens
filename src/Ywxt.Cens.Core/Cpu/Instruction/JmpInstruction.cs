using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class JmpInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0x4C, (AddressingMode.AbsoluteAddressingMode, InstructionType.Common, 3)},
                {0x6C, (AddressingMode.IndirectAddressingMode, InstructionType.Common, 5)},
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            cpu.CpuRegisters.Pc = address;
            return 0;
        }
    }
}