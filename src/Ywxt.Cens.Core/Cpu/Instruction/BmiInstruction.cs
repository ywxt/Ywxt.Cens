using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class BmiInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0x30, (AddressingMode.RelativeAddressingMode, InstructionType.CrossingPage, 2)}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            var success = cpu.Registers.P.HasFlag(PFlags.N);
            if (success)
            {
                cpu.Registers.Pc = address;
            }

            return InstructionUtil.GetBranchClockCycle(success);
        }
    }
}