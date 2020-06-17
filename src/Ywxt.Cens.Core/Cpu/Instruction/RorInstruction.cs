using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class RorInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0x6A, AddressingMode.AccumulatorAddressingMode},
                {0x66, AddressingMode.ZeroPageAddressingMode},
                {0x76, AddressingMode.ZeroPageXAddressingMode},
                {0x6E, AddressingMode.AbsoluteAddressingMode},
                {0x7E, AddressingMode.AbsoluteXAddressingMode}
            };

        public AddressingType AddressingType { get; } = AddressingType.Data;

        public int Invoke(ICpu cpu, byte instruction, ushort address, byte data, bool pageCrossed)
        {
            byte old = data;
            byte @new = 0;
            switch (instruction)
            {
                case 0x6A:
                    cpu.Registers.A = (byte) ((data >> 1) | ((byte) (cpu.Registers.P & PFlags.C) << 7));
                    @new = cpu.Registers.A;
                    break;
                case 0x66:
                case 0x76:
                case 0x6E:
                case 0x7E:
                    cpu.Bus.WriteByte(address, (byte) ((data >> 1) | ((byte) (cpu.Registers.P & PFlags.C) << 7)));
                    @new = (byte) ((data >> 1) | ((byte) (cpu.Registers.P & PFlags.C) << 7));
                    break;
            }


            cpu.Registers.SetC((old & 1) == 1);
            cpu.Registers.SetZAndN(@new);

            return instruction switch
            {
                0x6A => 2,
                0x66 => 5,
                0x76 => 6,
                0x6E => 6,
                0x7E => 7,
                _ => 0
            };
        }
    }
}