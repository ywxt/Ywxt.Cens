namespace Ywxt.Cens.Core.Cpu
{
    public interface ICpu
    {
        public Registers Registers { get; }

        public IBus Bus { get; }

        void Clock();

        void Reset();

        void Nmi();

        void Irq();

        void PushByte(byte data);

        void PushWord(ushort data);

        byte PopByte();

        ushort PopWord();
    }
}