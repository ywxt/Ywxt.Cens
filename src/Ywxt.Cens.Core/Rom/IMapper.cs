namespace Ywxt.Cens.Core.Rom
{
    public interface IMapper
    {
        byte Read(ushort address);
        void Write(ushort address, byte data);
    }
}