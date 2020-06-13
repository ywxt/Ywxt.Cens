using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class StaInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0x85, AddressingMode.ZeroPageAddressingMode}
            };

        public AddressingType AddressingType { get; } = AddressingType.Address;

        public int Invoke(ICpu cpu, byte instruction, ushort address, byte data, bool pageCrossed)
        {
            cpu.Bus.WriteByte(address, cpu.Registers.A);
            return instruction switch
            {
                0x85 => 3,
                _ => 0
            };
        }
    }
}