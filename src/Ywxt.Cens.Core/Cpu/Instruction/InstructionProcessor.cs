namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class InstructionProcessor : IInstructionProcessor
    {
        public int Process(ICpu cpu, byte instruction)
        {
            var ins = Instructions.Get(instruction);
            var addrMode = AddressingModes.Get(ins.OpCodes[instruction]);
            var data = addrMode.Addressing(cpu.Registers, cpu.Bus);
            var pageCrossed = false;
            if (ins.AddressingType == AddressingType.Data &&
                addrMode.AddressingType == AddressingType.Address)
            {
                pageCrossed = (cpu.Registers.Pc & 0xFF00) != (data & 0xFF00);
                data = cpu.Bus.ReadByte(data);
            }
            
            return ins.Invoke(cpu, instruction, data, pageCrossed);
        }
    }
}