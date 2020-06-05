namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public interface IInstructionProcessor
    {
        int Process(ICpu cpu, byte instruction) ;
    }
}