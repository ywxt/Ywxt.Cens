using Ywxt.Cens.Core.Cpu;

namespace Ywxt.Cens.Core.Utils
{
    public static class RegisterExtension
    {
        public static void SetZAndN(this Registers registers, byte flag)
        {
            registers.SetZ(flag);
            registers.SetN(flag);
        }

        public static void SetZ(this Registers registers, byte flag)
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

        public static void SetN(this Registers registers, byte flag)
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

        public static void SetC(this Registers registers, bool value)
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
    }
}