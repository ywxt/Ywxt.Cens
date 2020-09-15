using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class PhpInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0x08, (AddressingMode.ImplicitAddressingMode, InstructionType.Common, 3)}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            cpu.Stack.PushByte((byte) (cpu.CpuRegisters.P | PFlags.U | PFlags.B));
            return 0;
        }
    }
}