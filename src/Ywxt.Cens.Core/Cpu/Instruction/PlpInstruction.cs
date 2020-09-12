using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class PlpInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0x28, (AddressingMode.ImplicitAddressingMode, InstructionType.Common, )}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            var flags = cpu.Stack.PopByte();
            cpu.Registers.P = (PFlags) flags;
            cpu.Registers.P &= ~PFlags.B;
            cpu.Registers.P |= PFlags.U;
            return instruction switch
            {
                0x28 => 4,
                _ => 0
            };
        }
    }
}