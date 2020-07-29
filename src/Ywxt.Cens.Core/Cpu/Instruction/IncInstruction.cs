using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class IncInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0xE6, AddressingMode.ZeroPageAddressingMode},
                {0xF6, AddressingMode.ZeroPageXAddressingMode},
                {0xEE, AddressingMode.AbsoluteAddressingMode},
                {0xFE, AddressingMode.AbsoluteXAddressingMode}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address, bool pageCrossed)
        {
            var data = cpu.Bus.ReadByte(address);
            data++;
            cpu.Bus.WriteByte(address, data);
            cpu.Registers.SetZAndN(data);
            return instruction switch
            {
                0xE6 => 5,
                0xF6 => 6,
                0xEE => 6,
                0xFE => 7,
                _ => 0
            };
        }
    }
}