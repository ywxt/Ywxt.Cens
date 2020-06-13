using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class BmiInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0x30, AddressingMode.RelativeAddressingMode}
            };

        public AddressingType AddressingType { get; } = AddressingType.Address;

        public int Invoke(ICpu cpu, byte instruction, ushort address, byte data, bool pageCrossed)
        {
            var oldAddress = cpu.Registers.Pc;
            var success = cpu.Registers.P.HasFlag(PFlags.N);
            if (success)
            {
                cpu.Registers.Pc = address;
            }

            return instruction switch
            {
                0x30 => 2 + BranchInstructionUtil.GetClockCycleIncrement(success, oldAddress, cpu.Registers.Pc),
                _ => 0
            };
        }
    }
}