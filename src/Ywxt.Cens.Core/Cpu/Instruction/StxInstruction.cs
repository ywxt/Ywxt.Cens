using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class StxInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0x86, (AddressingMode.ZeroPageAddressingMode, InstructionType.Common, )},
                {0x8E, (AddressingMode.AbsoluteAddressingMode, InstructionType.Common, )},
                {0x96, (AddressingMode.ZeroPageYAddressingMode, InstructionType.Common, )}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            cpu.Bus.WriteByte(address, cpu.Registers.X);
            return instruction switch
            {
                0x86 => 3,
                0x96 => 4,
                0x8E => 4,
                _ => 0
            };
        }
    }
}