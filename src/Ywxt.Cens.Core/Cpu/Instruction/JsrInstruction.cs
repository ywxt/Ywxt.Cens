using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class JsrInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0x20, (AddressingMode.AbsoluteAddressingMode, InstructionType.Common, )}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            cpu.Stack.PushWord((ushort) (cpu.Registers.Pc - 1));
            cpu.Registers.Pc = address;
            return instruction switch
            {
                0x20 => 6,
                _ => 0,
            };
        }
    }
}