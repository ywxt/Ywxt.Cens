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

        public IStack Stack { get; }

        public IBus Bus { get; }

        public Cpu(Cartridge cartridge)
        {
            Bus = new CpuBus(cartridge);
            Stack = new Stack(Bus, Registers);
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
            Stack.PushWord(Registers.Pc);
            Stack.PushByte((byte) ((Registers.P | PFlags.U) & ~PFlags.B));
            Registers.P |= PFlags.I;
            Registers.Pc = Bus.ReadWord(NmiVector);
            _deferCycles += 7;
        }

        public void Irq()
        {
            if (Registers.P.HasFlag(PFlags.I))
            {
                return;
            }

            Stack.PushWord(Registers.Pc);
            Stack.PushByte((byte) ((Registers.P | PFlags.U) & ~PFlags.B));
            Registers.P |= PFlags.I;
            Registers.Pc = Bus.ReadWord(IrqOrBrkVector);
            _deferCycles += 7;
        }


        private void Step()
        {
        }
    }
}