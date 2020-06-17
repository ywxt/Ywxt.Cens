using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class DecInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0xC6, AddressingMode.ZeroPageAddressingMode},
                {0xD6, AddressingMode.ZeroPageXAddressingMode},
                {0xCE, AddressingMode.AbsoluteAddressingMode},
                {0xDE, AddressingMode.AbsoluteXAddressingMode}
            };

        public AddressingType AddressingType { get; } = AddressingType.Data;

        public int Invoke(ICpu cpu, byte instruction, ushort address, byte data, bool pageCrossed)
        {
            data--;
            cpu.Bus.WriteByte(address, data);
            cpu.Registers.SetZAndN(data);
            return instruction switch
            {
                0xC6 => 5,
                0xD6 => 6,
                0xCE => 6,
                0xDE => 7,
                _ => 0
            };
        }
    }
}