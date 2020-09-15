using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class JsrInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0x20, (AddressingMode.AbsoluteAddressingMode, InstructionType.Common, 6)}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            cpu.Stack.PushWord((ushort) (cpu.CpuRegisters.Pc - 1));
            cpu.CpuRegisters.Pc = address;
            return 0;
        }
    }
}