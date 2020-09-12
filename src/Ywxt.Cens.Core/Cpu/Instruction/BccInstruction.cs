using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class BccInstruction : IInstruction
    {
        IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> IInstruction.OpCodes
        {
            get;
        }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0xF0, (AddressingMode.RelativeAddressingMode, InstructionType.CrossingPage, 2)}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            var jmpSuccess = cpu.Registers.P.HasFlag(PFlags.Z);
            if (jmpSuccess)
            {
                cpu.Registers.Pc = address;
            }


            return InstructionUtil.GetBranchClockCycle(jmpSuccess);
        }
    }
}