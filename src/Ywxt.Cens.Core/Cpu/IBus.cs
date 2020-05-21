namespace Ywxt.Cens.Core.Cpu
{
    /// <summary>
    /// 总线
    /// </summary>
    public interface IBus
    {
        byte ReadByte(ushort address);

        ushort ReadWord(ushort address)
        {
            var low = ReadByte(address);
            var high = ReadByte((ushort) (address + 1));
            return (ushort) ((high << 8) | low);
        }

        void WriteByte(ushort address, byte data);

        void WriteWord(ushort address, ushort data)
        {
            WriteByte(address, (byte) (data & 0x00FF));
            WriteByte((ushort) (address + 1), (byte) (data >> 8));
        }
    }
}