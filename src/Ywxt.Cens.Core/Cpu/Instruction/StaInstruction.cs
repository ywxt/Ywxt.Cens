using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class StaInstruction:IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingType addrType, AddressingMode addrMode)> OpCodes { get; }
        = new Dictionary<byte, (AddressingType addrType, AddressingMode addrMode)>
        {
            {0x85,(AddressingType.Address,AddressingMode.ZeroPageAddressingMode)}
        };
        public int Invoke(ICpu cpu, byte instruction, ushort data)
        {
            cpu.Bus.WriteByte(data, cpu.Registers.A);
            return instruction switch
            {
                0x85 => 3,
                _ => 0
            };
        }
    }
}