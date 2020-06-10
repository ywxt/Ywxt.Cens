namespace Ywxt.Cens.Core.Cpu
{
    public sealed class Stack : IStack
    {
        private readonly IBus _bus;
        private readonly Registers _registers;

        public Stack(IBus bus, Registers registers)
        {
            _bus = bus;
            _registers = registers;
        }


        public void PushByte(byte data)
        {
            _bus.WriteByte((ushort) (CpuBus.AddressStackStart + _registers.Sp), data);
            _registers.Sp--;
        }

        public void PushWord(ushort data)
        {
            var low = (byte) (data & 0x00FF);
            var high = (byte) ((data & 0xFF00) >> 8);
            PushByte(high);
            PushByte(low);
            
        }

        public byte PopByte()
        {
            _registers.Sp++;
            var data = _bus.ReadByte((ushort) (CpuBus.AddressStackStart + _registers.Sp));
            return data;
        }

        public ushort PopWord()
        {
            var low = PopByte();
            var high = PopByte();
            return (ushort) ((high << 8) + low);
        }
    }
}