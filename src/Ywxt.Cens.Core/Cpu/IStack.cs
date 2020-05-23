namespace Ywxt.Cens.Core.Cpu
{
    public interface IStack
    {
        void PushByte(byte data);

        void PushWord(ushort data);

        byte PopByte();

        ushort PopWord();
    }
}