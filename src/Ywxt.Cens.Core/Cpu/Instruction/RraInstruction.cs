using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    /// <summary>
    /// Rotate Right then Add with Carry - ROR + ADC
    /// </summary>
    public sealed class RraInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                [0x67] = AddressingMode.ZeroPageAddressingMode,
                [0x77] = AddressingMode.ZeroPageXAddressingMode,
                [0x6F] = AddressingMode.AbsoluteAddressingMode,
                [0x7F] = AddressingMode.AbsoluteXAddressingMode,
                [0x7B] = AddressingMode.AbsoluteYAddressingMode,
                [0x63] = AddressingMode.IndirectXAddressingMode,
                [0x73] = AddressingMode.IndirectYAddressingMode
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address, bool pageCrossed)
        {
            // ROR
            ushort data = cpu.Bus.ReadByte(address);
            var @new = (byte) ((data >> 1) | ((byte) (cpu.Registers.P & PFlags.C) << 7));
            cpu.Bus.WriteByte(address, @new);
            cpu.Registers.SetCFlag((data & 1) == 1);
            // ADC
            var result = cpu.Registers.A + @new + (byte) (cpu.Registers.P & PFlags.C);
            var af = cpu.Registers.A >> 7;
            var bf = @new >> 7;
            var cf = (result >> 7) & 1;
            cpu.Registers.SetVFlag(af == bf && af != cf);
            cpu.Registers.SetCFlag(((result >> 8) & 1) == 1);
            cpu.Registers.A = unchecked((byte) result);
            cpu.Registers.SetZAndNFlags(cpu.Registers.A);
            return instruction switch
            {
                0x67 => 5,
                0x77 => 6,
                0x6F => 6,
                0x7F => 7,
                0x7B => 7,
                0x63 => 8,
                0x73 => 8,
                _ => 0
            };
        }
    }
}