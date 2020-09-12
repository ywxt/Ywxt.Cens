using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class NopInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0x1A, (AddressingMode.ImplicitAddressingMode, InstructionType.Common, )},
                {0x3A, (AddressingMode.ImplicitAddressingMode, InstructionType.Common, )},
                {0x5A, (AddressingMode.ImplicitAddressingMode, InstructionType.Common, )},
                {0x7A, (AddressingMode.ImplicitAddressingMode, InstructionType.Common, )},
                {0xDA, (AddressingMode.ImplicitAddressingMode, InstructionType.Common, )},
                {0xEA, (AddressingMode.ImplicitAddressingMode, InstructionType.Common, )},
                {0xFA, (AddressingMode.ImplicitAddressingMode, InstructionType.Common, )}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            return instruction switch
            {
                0x1A => 2,
                0x3A => 2,
                0x5A => 2,
                0x7A => 2,
                0xDA => 2,
                0xEA => 2,
                0xFA => 2,
                _ => 0,
            };
        }
    }
}