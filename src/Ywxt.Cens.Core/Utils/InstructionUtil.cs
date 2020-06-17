namespace Ywxt.Cens.Core.Utils
{
    public static class InstructionUtil
    {
        public static int GetClockCycleByCrossingPage(bool pageCrossed)
        {
            return pageCrossed ? 1 : 0;
        }

        public static bool IsPageCrossed(ushort old, ushort @new)
        {
            return (old & 0xFF00) != (@new & 0xFF00);
        }
    }
}