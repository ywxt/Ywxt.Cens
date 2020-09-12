using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class StxInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0x86, (AddressingMode.ZeroPageAddressingMode, InstructionType.Common, 3)},
                {0x8E, (AddressingMode.AbsoluteAddressingMode, InstructionType.Common, 4)},
                {0x96, (AddressingMode.ZeroPageYAddressingMode, InstructionType.Common, 4)}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            cpu.Bus.WriteByte(address, cpu.Registers.X);
            return 0;
        }
    }
}