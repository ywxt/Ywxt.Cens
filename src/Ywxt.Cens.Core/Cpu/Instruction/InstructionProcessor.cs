namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class InstructionProcessor : IInstructionProcessor
    {
        public int Process(ICpu cpu, byte instruction)
        {
            var ins = Instructions.Get(instruction);
            var addrMode = AddressingModes.Get(ins.OpCodes[instruction].addrMode);
            var data = addrMode.Addressing(cpu.Registers, cpu.Bus);
            if (ins.OpCodes[instruction].addrType == AddressingType.Data &&
                addrMode.AddressingType == AddressingType.Address)
            {
                data = cpu.Bus.ReadByte(data);
            }
            return ins.Invoke(cpu, instruction, data);
        }
    }
}