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

        public int Invoke(ICpu cpu, byte instruction, ushort address, bool pageCrossed)
        {
            var data = this.ReadData(address, cpu, instruction);
            var value = data;
            cpu.Registers.SetNFlag(data);
            cpu.Registers.SetVFlag((value & 0b01000000) >> 6 == 1);

            value &= cpu.Registers.A;
            cpu.Registers.SetZFlag(value);

            return instruction switch
            {
                0x24 => 3,
                0x2C => 4,
                _ => 0
            };
        }
    }
}