using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class BplInstruction: IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingType addrType, AddressingMode addrMode)> OpCodes { get; }
            = new Dictionary<byte, (AddressingType addrType, AddressingMode addrMode)>
            {
                {0x10, (AddressingType.Address, AddressingMode.RelativeAddressingMode)}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort data)
        {
            var oldAddress = cpu.Registers.Pc;
            var jmpSuccess = !cpu.Registers.P.HasFlag(PFlags.N);
            if (jmpSuccess)
            {
                cpu.Registers.Pc = data;
            }

            return instruction switch
            {
                0x10 => 2 + BranchInstructionUtil.GetClockCycleIncrement(jmpSuccess, oldAddress, cpu.Registers.Pc),
                _ => 0
            };
        }
    }
}