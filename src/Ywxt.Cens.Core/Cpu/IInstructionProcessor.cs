namespace Ywxt.Cens.Core.Cpu
{
    public interface IInstructionProcessor
    {
        int Process(ICpu cpu, byte instruction) ;
    }
}