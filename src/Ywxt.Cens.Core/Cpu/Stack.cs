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
            _bus.WriteWord((ushort) (CpuBus.AddressStackStart + _registers.Sp), data);
            _registers.Sp -= 2;
        }

        public byte PopByte()
        {
            var data = _bus.ReadByte((ushort) (CpuBus.AddressStackStart + _registers.Sp));
            _registers.Sp++;
            return data;
        }

        public ushort PopWord()
        {
            var data = _bus.ReadWord((ushort) (CpuBus.AddressStackStart + _registers.Sp));
            _registers.Sp += 2;
            return data;
        }
    }
}