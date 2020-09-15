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
                {0xE7, (AddressingMode.ZeroPageAddressingMode, InstructionType.Common, 5)},
                {0xF7, (AddressingMode.ZeroPageXAddressingMode, InstructionType.Common, 6)},
                {0xEF, (AddressingMode.AbsoluteAddressingMode, InstructionType.Common, 6)},
                {0xFF, (AddressingMode.AbsoluteXAddressingMode, InstructionType.Common, 7)},
                {0xFB, (AddressingMode.AbsoluteYAddressingMode, InstructionType.Common, 7)},
                {0xE3, (AddressingMode.IndirectXAddressingMode, InstructionType.Common, 8)},
                {0xF3, (AddressingMode.IndirectYAddressingMode, InstructionType.Common, 8)}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            var data = cpu.Bus.ReadByte(address);
            data++;
            cpu.Bus.WriteByte(address, data);
            var result = unchecked(cpu.CpuRegisters.A - data - 1 + (byte) (cpu.CpuRegisters.P & PFlags.C));

            var af = cpu.CpuRegisters.A >> 7;
            var bf = data >> 7;
            var cf = (result >> 7) & 1;
            //判断溢出
            cpu.CpuRegisters.SetVFlag((af == 1 && cf == 0) | (af == 0 && bf == 1 && cf == 1));

            cpu.CpuRegisters.SetCFlag(((result >> 8) & 1) != 1);

            cpu.CpuRegisters.A = unchecked((byte) result);
            cpu.CpuRegisters.SetZAndNFlags(cpu.CpuRegisters.A);
            return 0;
        }
    }
}