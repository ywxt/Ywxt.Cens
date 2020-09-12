using System;
using Ywxt.Cens.Core.Exceptions;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class InstructionProcessor : IInstructionProcessor
    {
        public int Process(ICpu cpu, byte instruction)
        {
            var ins = Instructions.Get(instruction);
            if (ins is null) throw new UnknownInstructionException(instruction);
            var (mode, insType, cycles) = ins.OpCodes[instruction];
            var addrMode = AddressingModes.Get(mode);
            var (address, pageCrossed) = addrMode.Addressing(cpu.Registers, cpu.Bus);
            var extraCycles = ins.Invoke(cpu, instruction, address);
            var cyclesIncrement = insType switch
            {
                InstructionType.Common => 0,
                InstructionType.CrossingPage => InstructionUtil.GetCrossingPageClockCycles(pageCrossed),
                InstructionType.Branch => InstructionUtil.GetBranchClockCycle(Convert.ToBoolean(extraCycles),
                    pageCrossed),
                _ => 0
            };
            return cycles + cyclesIncrement;
        }
    }
}