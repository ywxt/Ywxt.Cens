using System;
using Ywxt.Cens.Core.Cpu.Instruction;
using Ywxt.Cens.Core.Rom;

namespace Ywxt.Cens.Core.Cpu
{
    public sealed class Cpu : ICpu
    {
        private const ushort NmiVector = 0xFFFA;
        private const ushort ResetVector = 0xFFFC;
        private const ushort IrqOrBrkVector = 0xFFFE;

        private readonly IInstructionProcessor _processor = new InstructionProcessor();

        private int _deferCycles = 0;
        private int _cycles = 0;

#if DEBUG
        internal event Action<Registers, IStack, int>? StepBeforeEvent;
#endif

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
#if DEBUG

                OnStepBeforeEvent(Registers, Stack, _cycles);
#endif
                Step();
            }

            _cycles++;
            _deferCycles--;
        }

        public void Reset()
        {
            Registers.A = 0;
            Registers.X = 0;
            Registers.Y = 0;
            Registers.P = PFlags.U | PFlags.I;
            Registers.Sp = 0xFD;
            Registers.Pc = Bus.ReadWord(ResetVector);
            _deferCycles = 7;
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
            var op = Bus.ReadByte(Registers.Pc++);
            var cycle = _processor.Process(this, op);
            _deferCycles += cycle;
        }

        private void OnStepBeforeEvent(Registers registers, IStack stack, int cycles)
        {
            StepBeforeEvent?.Invoke(registers, stack, cycles);
        }
    }
}