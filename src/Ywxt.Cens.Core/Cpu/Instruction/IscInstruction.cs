using System.Collections.Generic;
using Ywxt.Cens.Core.Cpu;
using Ywxt.Cens.Core.Cpu.Instruction;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class IscInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0xE7, AddressingMode.ZeroPageAddressingMode},
                {0xF7, AddressingMode.ZeroPageXAddressingMode},
                {0xEF, AddressingMode.AbsoluteAddressingMode},
                {0xFF, AddressingMode.AbsoluteXAddressingMode},
                {0xFB, AddressingMode.AbsoluteYAddressingMode},
                {0xE3, AddressingMode.IndirectXAddressingMode},
                {0xF3, AddressingMode.IndirectYAddressingMode}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address, bool pageCrossed)
        {
            var data = cpu.Bus.ReadByte(address);
            data++;
            cpu.Bus.WriteByte(address, data);
            var result = unchecked(cpu.Registers.A - data - 1 + (byte) (cpu.Registers.P & PFlags.C));

            var af = cpu.Registers.A >> 7;
            var bf = data >> 7;
            var cf = (result >> 7) & 1;
            //判断溢出
            cpu.Registers.SetV((af == 1 && cf == 0) | (af == 0 && bf == 1 && cf == 1));

            cpu.Registers.SetC(((result >> 8) & 1) != 1);

            cpu.Registers.A = unchecked((byte) result);
            cpu.Registers.SetZAndN(cpu.Registers.A);
            return instruction switch
            {
                0xE7 => 5,
                0xF7 => 6,
                0xEF => 6,
                0xFF => 7,
                0xFB => 7,
                0xE3 => 8,
                0xF3 => 8,
                _ => 0
            };
        }
    }
}