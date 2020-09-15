using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class PlpInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0x28, (AddressingMode.ImplicitAddressingMode, InstructionType.Common, 4)}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            var flags = cpu.Stack.PopByte();
            cpu.CpuRegisters.P = (PFlags) flags;
            cpu.CpuRegisters.P &= ~PFlags.B;
            cpu.CpuRegisters.P |= PFlags.U;
            return 0;
        }
    }
}