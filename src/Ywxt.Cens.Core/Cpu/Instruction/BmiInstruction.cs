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

        public int Invoke(ICpu cpu, byte instruction, ushort address, bool pageCrossed)
        {
            var success = cpu.Registers.P.HasFlag(PFlags.N);
            if (success)
            {
                cpu.Registers.Pc = address;
            }

            return instruction switch
            {
                0x30 => 2 + InstructionUtil.GetJmpClockCycleIncrement(success, pageCrossed),
                _ => 0
            };
        }
    }
}