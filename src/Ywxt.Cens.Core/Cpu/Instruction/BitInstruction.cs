using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class BitInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0x24, AddressingMode.ZeroPageAddressingMode},
                {0x2C, AddressingMode.AbsoluteAddressingMode}
            };

        public AddressingType AddressingType { get; } = AddressingType.Data;

        public int Invoke(ICpu cpu, byte instruction, ushort address, byte data, bool pageCrossed)
        {
            var value = data;
            cpu.Registers.SetN(data);
            cpu.Registers.SetV((value & 0b01000000) >> 6 == 1);

            value &= cpu.Registers.A;
            cpu.Registers.SetZ(value);

            return instruction switch
            {
                0x24 => 3,
                0x2C => 4,
                _ => 0
            };
        }
    }
}