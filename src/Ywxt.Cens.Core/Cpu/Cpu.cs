using System;
using Ywxt.Cens.Core.Rom;

namespace Ywxt.Cens.Core.Cpu
{
    public class Cpu : ICpu
    {
        private const ushort NmiVector = 0xFFFA;
        private const ushort ResetVector = 0xFFFC;
        private const ushort IrqOrBrkVector = 0xFFFE;

        private int _deferCycles = 0;

        public Registers Registers { get; } = new Registers();

        public IBus Bus { get; }

        public Cpu(Cartridge cartridge)
        {
            Bus = new CpuBus(cartridge);
        }

        public void Clock()
        {
            if (_deferCycles == 0)
            {
                Step();
                return;
            }

            _deferCycles--;
        }

        public void Reset()
        {
            Registers.A = 0;
            Registers.X = 0;
            Registers.Y = 0;
            Registers.P = 0;
            Registers.Sp = 0xFF;
            Registers.Pc = Bus.ReadWord(ResetVector);
            _deferCycles = 8;
        }

        public void Nmi()
        {
            throw new System.NotImplementedException();
        }

        public void Irq()
        {
            if (Registers.P.HasFlag(PFlags.I))
            {
                return;
            }
        }

        public void PushByte(byte data)
        {
            Registers.Sp--;
            Bus.WriteByte((ushort) (CpuBus.AddressStackStart + Registers.Sp), data);
        }

        public void PushWord(ushort data)
        {
            Registers.Sp -= 2;
            Bus.WriteWord((ushort) (CpuBus.AddressStackStart + Registers.Sp), data);
        }

        public byte PopByte()
        {
            var data = Bus.ReadByte((ushort) (CpuBus.AddressStackStart + Registers.Sp));
            Registers.Sp++;
            return data;
        }

        public ushort PopWord()
        {
            var data = Bus.ReadWord((ushort) (CpuBus.AddressStackStart + Registers.Sp));
            Registers.Sp += 2;
            return data;
        }


        private void Step()
        {
        }
    }
}