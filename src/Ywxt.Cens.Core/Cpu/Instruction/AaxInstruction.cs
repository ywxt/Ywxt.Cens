using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class AaxInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0x87, (AddressingMode.ZeroPageAddressingMode, InstructionType.Common, 3)},
                {0x97, (AddressingMode.ZeroPageYAddressingMode, InstructionType.Common, 4)},
                {0x83, (AddressingMode.IndirectXAddressingMode, InstructionType.Common, 6)},
                {0x8F, (AddressingMode.AbsoluteAddressingMode, InstructionType.Common, 4)}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            var result = (byte) (cpu.Registers.A & cpu.Registers.X);
            cpu.Bus.WriteByte(address, result);
            return 0;
        }
    }
}