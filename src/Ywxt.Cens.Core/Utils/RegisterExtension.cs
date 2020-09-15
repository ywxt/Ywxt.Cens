using Ywxt.Cens.Core.Cpu;

namespace Ywxt.Cens.Core.Utils
{
    public static class RegisterExtension
    {
        public static void SetZAndNFlags(this CpuRegisters registers, byte flag)
        {
            registers.SetZFlag(flag);
            registers.SetNFlag(flag);
        }

        public static void SetZFlag(this CpuRegisters registers, byte flag)
        {
            if (flag == 0)
            {
                registers.P |= PFlags.Z;
            }
            else
            {
                registers.P &= ~PFlags.Z;
            }
        }

        public static void SetNFlag(this CpuRegisters registers, byte flag)
        {
            if (flag >> 7 == 1)
            {
                registers.P |= PFlags.N;
            }
            else
            {
                registers.P &= ~PFlags.N;
            }
        }


        public static void SetCFlag(this CpuRegisters registers, bool value)
        {
            if (value)
            {
                registers.P |= PFlags.C;
            }
            else
            {
                registers.P &= ~PFlags.C;
            }
        }

        public static void SetVFlag(this CpuRegisters registers, bool value)
        {
            if (value)
            {
                registers.P |= PFlags.V;
            }
            else
            {
                registers.P &= ~PFlags.V;
            }
        }
    }
}