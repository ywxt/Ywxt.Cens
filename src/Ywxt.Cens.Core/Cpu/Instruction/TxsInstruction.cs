using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class TxsInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0x9A, (AddressingMode.ImplicitAddressingMode, InstructionType.Common, 2)}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            cpu.Registers.Sp = cpu.Registers.X;
            return 0;
        }
    }
}