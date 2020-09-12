using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class SreInstruction : IInstruction
    {
        /// <summary>
        /// SRE - Shift Right then "Exclusive-Or" - LSR + EOR
        /// </summary>
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                [0x47] = AddressingMode.ZeroPageAddressingMode,
                [0x57] = AddressingMode.ZeroPageXAddressingMode,
                [0x4F] = AddressingMode.AbsoluteAddressingMode,
                [0x5F] = AddressingMode.AbsoluteXAddressingMode,
                [0x5B] = AddressingMode.AbsoluteYAddressingMode,
                [0x43] = AddressingMode.IndirectXAddressingMode,
                [0x53] = AddressingMode.IndirectYAddressingMode
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            var data = cpu.Bus.ReadByte(address);
            cpu.Registers.SetCFlag((data & 1) == 1);
            data >>= 1;
            cpu.Bus.WriteByte(address, data);

            cpu.Registers.A ^= data;
            cpu.Registers.SetZAndNFlags(cpu.Registers.A);

            return instruction switch
            {
                0x47 => 5,
                0x57 => 6,
                0x4F => 6,
                0x5F => 7,
                0x5B => 7,
                0x43 => 8,
                0x53 => 8,
                _ => 0
            };
        }
    }
}