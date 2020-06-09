using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class BmiInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingType addrType, AddressingMode addrMode)> OpCodes { get; }
            = new Dictionary<byte, (AddressingType addrType, AddressingMode addrMode)>
            {
                {0x30, (AddressingType.Address, AddressingMode.RelativeAddressingMode)}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort data)
        {
            var oldAddress = cpu.Registers.Pc;
            var success = cpu.Registers.P.HasFlag(PFlags.N);
            if (success)
            {
                cpu.Registers.Pc = data;
            }

            return instruction switch
            {
                0x30 => 2 + BranchInstructionUtil.GetClockCycleIncrement(success, oldAddress, cpu.Registers.Pc),
                _ => 0
            };

        }
    }
}