using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class JsrInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0x20, AddressingMode.AbsoluteAddressingMode}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address, bool pageCrossed)
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