using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class RtsInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0x60, (AddressingMode.ImplicitAddressingMode, InstructionType.Common, 6)}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address1)
        {
            var address = cpu.Stack.PopWord();
            cpu.Registers.Pc = (ushort) (address + 1);

            return 0;
        }
    }
}