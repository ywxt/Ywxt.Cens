namespace Ywxt.Cens.Core.Cpu
{
    public interface ICpu
    {
        CpuRegisters CpuRegisters { get; }

        IStack Stack { get; }

        IBus Bus { get; }

        void Clock();

        void Reset();

        void Nmi();

        void Irq();
    }
}