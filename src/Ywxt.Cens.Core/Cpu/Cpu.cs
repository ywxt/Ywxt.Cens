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
        internal event Action<CpuRegisters, IStack, int>? StepBeforeEvent;
#endif

        public CpuRegisters CpuRegisters { get; } = new CpuRegisters();

        public IStack Stack { get; }

        public IBus Bus { get; }

        public Cpu(Cartridge cartridge)
        {
            Bus = new CpuBus(cartridge);
            Stack = new Stack(Bus, CpuRegisters);
        }

        public void Clock()
        {
            if (_deferCycles == 0)
            {
#if DEBUG

                OnStepBeforeEvent(CpuRegisters, Stack, _cycles);
#endif
                Step();
            }

            _cycles++;
            _deferCycles--;
        }

        public void Reset()
        {
            CpuRegisters.A = 0;
            CpuRegisters.X = 0;
            CpuRegisters.Y = 0;
            CpuRegisters.P = PFlags.U | PFlags.I;
            CpuRegisters.Sp = 0xFD;
            CpuRegisters.Pc = Bus.ReadWord(ResetVector);
            _deferCycles = 7;
        }

        public void Nmi()
        {
            Stack.PushWord(CpuRegisters.Pc);
            Stack.PushByte((byte) ((CpuRegisters.P | PFlags.U) & ~PFlags.B));
            CpuRegisters.P |= PFlags.I;
            CpuRegisters.Pc = Bus.ReadWord(NmiVector);
            _deferCycles += 7;
        }

        public void Irq()
        {
            if (CpuRegisters.P.HasFlag(PFlags.I))
            {
                return;
            }

            Stack.PushWord(CpuRegisters.Pc);
            Stack.PushByte((byte) ((CpuRegisters.P | PFlags.U) & ~PFlags.B));
            CpuRegisters.P |= PFlags.I;
            CpuRegisters.Pc = Bus.ReadWord(IrqOrBrkVector);
            _deferCycles += 7;
        }


        private void Step()
        {
            var op = Bus.ReadByte(CpuRegisters.Pc++);
            var cycle = _processor.Process(this, op);
            _deferCycles += cycle;
        }

        private void OnStepBeforeEvent(CpuRegisters registers, IStack stack, int cycles)
        {
            StepBeforeEvent?.Invoke(registers, stack, cycles);
        }
    }
}