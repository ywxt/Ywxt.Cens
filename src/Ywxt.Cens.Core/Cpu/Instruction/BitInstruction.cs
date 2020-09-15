using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class BitInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0x24, (AddressingMode.ZeroPageAddressingMode, InstructionType.Common, 3)},
                {0x2C, (AddressingMode.AbsoluteAddressingMode, InstructionType.Common, 4)}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            var data = this.ReadData(address, cpu, instruction);
            var value = data;
            cpu.CpuRegisters.SetNFlag(data);
            cpu.CpuRegisters.SetVFlag((value & 0b01000000) >> 6 == 1);

            value &= cpu.CpuRegisters.A;
            cpu.CpuRegisters.SetZFlag(value);

            return 0;
        }
    }
}