using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class BcsInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingType addrType, AddressingMode addrMode)> OpCodes { get; }
            = new Dictionary<byte, (AddressingType addrType, AddressingMode addrMode)>
            {
                {0xB0, (AddressingType.Address, AddressingMode.RelativeAddressingMode)}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort data)
        {
            var oldAddress = cpu.Registers.Pc;
            var jmpSuccess = cpu.Registers.P.HasFlag(PFlags.C);
            if (jmpSuccess)
            {
                cpu.Registers.Pc = data;
            }

            var cycle = 0;
            // 跳转成功
            if (jmpSuccess)
            {
                cycle += 1;
            }

            // 跨页访问
            if ((oldAddress & 0xFF00) != (cpu.Registers.Pc & 0xFF00))
            {
                cycle += 1;
            }

            return instruction switch
            {
                0xB0 => 2 + cycle,
                _ => 0
            };
        }
    }
}