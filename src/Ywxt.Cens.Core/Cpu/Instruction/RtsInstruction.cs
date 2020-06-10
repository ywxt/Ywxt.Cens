using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class RtsInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0x60,  AddressingMode.ImplicitAddressingMode}
            };

        public AddressingType AddressingType { get; }= AddressingType.Data;

        public int Invoke(ICpu cpu, byte instruction, ushort data, bool pageCrossed)
        {
            var address = cpu.Stack.PopWord();
            cpu.Registers.Pc = (ushort)(address + 1);

            return instruction switch
            {
                0x60 => 6,
                _ => 0
            };
        }
    }
}