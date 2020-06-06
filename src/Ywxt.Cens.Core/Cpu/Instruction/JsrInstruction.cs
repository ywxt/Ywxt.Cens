using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class JsrInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingType addrType, AddressingMode addrMode)> OpCodes { get; }
            = new Dictionary<byte, (AddressingType addrType, AddressingMode addrMode)>
            {
                {0x20, (AddressingType.Address, AddressingMode.AbsoluteAddressingMode)}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort data)
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