using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class DcpInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0xC7, AddressingMode.ZeroPageAddressingMode},
                {0xD7, AddressingMode.ZeroPageXAddressingMode},
                {0xCF, AddressingMode.AbsoluteAddressingMode},
                {0xDF, AddressingMode.AbsoluteXAddressingMode},
                {0xDB, AddressingMode.AbsoluteYAddressingMode},
                {0xC3, AddressingMode.IndirectXAddressingMode},
                {0xD3, AddressingMode.IndirectYAddressingMode}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address, bool pageCrossed)
        {
            var data = this.GetData(address, cpu, instruction);
            var tmp = unchecked((byte) (data - 1));
            cpu.Bus.WriteByte(address, tmp);
            var result = unchecked((ushort) (cpu.Registers.A - tmp));
            cpu.Registers.SetC(result < 0x0100);

            cpu.Registers.SetZAndN(unchecked((byte) result));
            return instruction switch
            {
                0xC7 => 5,
                0xD7 => 6,
                0xCF => 6,
                0xDF => 7,
                0xDB => 7,
                0xC3 => 8,
                0xD3 => 8,
                _ => 0
            };
        }
    }
}