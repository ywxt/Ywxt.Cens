using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class PhaInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0x48, (AddressingMode.ImplicitAddressingMode, InstructionType.Common, 3)}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            cpu.Stack.PushByte(cpu.CpuRegisters.A);
            return 0;
        }
    }
}