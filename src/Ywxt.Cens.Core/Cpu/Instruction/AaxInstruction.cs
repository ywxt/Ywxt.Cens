using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class AaxInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0x87, AddressingMode.ZeroPageAddressingMode},
                {0x97, AddressingMode.ZeroPageYAddressingMode},
                {0x83, AddressingMode.IndirectXAddressingMode},
                {0x8F, AddressingMode.AbsoluteAddressingMode}
            };

        public AddressingType AddressingType { get; } = AddressingType.Address;

        public int Invoke(ICpu cpu, byte instruction, ushort address, byte data, bool pageCrossed)
        {
            var result = (byte) (cpu.Registers.A & cpu.Registers.X);
            cpu.Bus.WriteByte(address, result);
            return instruction switch
            {
                0x87 => 3,
                0x97 => 4,
                0x83 => 6,
                0x8F => 4,
                _ => 0
            };
        }
    }
}