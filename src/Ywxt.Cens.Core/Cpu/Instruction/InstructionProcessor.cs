using System;
using Ywxt.Cens.Core.Exceptions;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class InstructionProcessor : IInstructionProcessor
    {
        public int Process(ICpu cpu, byte instruction)
        {
            var ins = Instructions.Get(instruction);
            if (ins is null) throw new UnknownInstructionException(instruction);
            var addrMode = AddressingModes.Get(ins.OpCodes[instruction]);
            var (address, pageCrossed) = addrMode.Addressing(cpu.Registers, cpu.Bus);
            return ins.Invoke(cpu, instruction, address, pageCrossed);
        }
    }
}