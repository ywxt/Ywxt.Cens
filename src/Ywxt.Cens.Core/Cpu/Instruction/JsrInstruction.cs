using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class JsrInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0x20,  AddressingMode.AbsoluteAddressingMode}
            };

        public AddressingType AddressingType { get; }= AddressingType.Address;

        public int Invoke(ICpu cpu, byte instruction, ushort data, bool pageCrossed)
        {
            cpu.Stack.PushWord(cpu.Registers.Pc);
            cpu.Registers.Pc = data;
            return instruction switch
            {
                0x20 => 6,
                _ => 0,
            };
        }
    }
}