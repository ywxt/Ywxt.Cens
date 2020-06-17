using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class StxInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0x86, AddressingMode.ZeroPageAddressingMode},
                {0x8E, AddressingMode.AbsoluteAddressingMode},
                {0x96, AddressingMode.ZeroPageYAddressingMode}
            };

        public AddressingType AddressingType { get; } = AddressingType.Address;

        public int Invoke(ICpu cpu, byte instruction, ushort address, byte data, bool pageCrossed)
        {
            cpu.Bus.WriteByte(address, cpu.Registers.X);
            return instruction switch
            {
                0x86 => 3,
                0x96 => 4,
                0x8E => 4,
                _ => 0
            };
        }
    }
}