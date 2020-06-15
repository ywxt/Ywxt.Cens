using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class RolInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0x2A, AddressingMode.AccumulatorAddressingMode},
                {0x26, AddressingMode.ZeroPageAddressingMode},
                {0x36, AddressingMode.ZeroPageXAddressingMode},
                {0x2E, AddressingMode.AbsoluteAddressingMode},
                {0x3E, AddressingMode.AbsoluteXAddressingMode}
            };

        public AddressingType AddressingType { get; } = AddressingType.Data;

        public int Invoke(ICpu cpu, byte instruction, ushort address, byte data, bool pageCrossed)
        {
            byte old = data;
            byte @new = 0;
            switch (instruction)
            {
                case 0x2A:
                    cpu.Registers.A = (byte) ((data << 1) | (byte) (cpu.Registers.P & PFlags.C));
                    @new = cpu.Registers.A;
                    break;
                case 0x26:
                case 0x36:
                case 0x2E:
                case 0x3E:
                    cpu.Bus.WriteByte(address, (byte) ((data << 1) | (byte) (cpu.Registers.P & PFlags.C)));
                    @new = (byte) ((data << 1) | (byte) (cpu.Registers.P & PFlags.C));
                    break;
            }


            cpu.Registers.SetC(old >> 7 == 1);
            cpu.Registers.SetZAndN(@new);

            return instruction switch
            {
                0x2A => 2,
                0x26 => 5,
                0x36 => 6,
                0x2E => 6,
                0x3E => 7,
                _ => 0
            };
        }
    }
}