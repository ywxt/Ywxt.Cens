using System.Collections.Generic;
using Ywxt.Cens.Core.Cpu;
using Ywxt.Cens.Core.Cpu.Instruction;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class IscInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0xE7, (AddressingMode.ZeroPageAddressingMode, InstructionType.Common, )},
                {0xF7, (AddressingMode.ZeroPageXAddressingMode, InstructionType.Common, )},
                {0xEF, (AddressingMode.AbsoluteAddressingMode, InstructionType.Common, )},
                {0xFF, (AddressingMode.AbsoluteXAddressingMode, InstructionType.Common, )},
                {0xFB, (AddressingMode.AbsoluteYAddressingMode, InstructionType.Common, )},
                {0xE3, (AddressingMode.IndirectXAddressingMode, InstructionType.Common, )},
                {0xF3, (AddressingMode.IndirectYAddressingMode, InstructionType.Common, )}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            var data = cpu.Bus.ReadByte(address);
            data++;
            cpu.Bus.WriteByte(address, data);
            var result = unchecked(cpu.Registers.A - data - 1 + (byte) (cpu.Registers.P & PFlags.C));

            var af = cpu.Registers.A >> 7;
            var bf = data >> 7;
            var cf = (result >> 7) & 1;
            //判断溢出
            cpu.Registers.SetVFlag((af == 1 && cf == 0) | (af == 0 && bf == 1 && cf == 1));

            cpu.Registers.SetCFlag(((result >> 8) & 1) != 1);

            cpu.Registers.A = unchecked((byte) result);
            cpu.Registers.SetZAndNFlags(cpu.Registers.A);
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