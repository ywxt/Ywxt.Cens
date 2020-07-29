namespace Ywxt.Cens.Core.Utils
{
    public static class InstructionUtil
    {
        public static int GetClockCyclesByCrossingPage(bool pageCrossed)
        {
            return pageCrossed ? 1 : 0;
        }

        public static bool IsPageCrossed(ushort old, ushort @new)
        {
            return (old & 0xFF00) != (@new & 0xFF00);
        }

        public static int GetJmpClockCycleIncrement(bool success, bool pageCrossed)
        {
            var cycle = 0;
            // 跳转成功
            if (success)
            {
                cycle = 1 + GetClockCyclesByCrossingPage(pageCrossed);
            }

            return cycle;
        }
    }
}