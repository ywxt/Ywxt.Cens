namespace Ywxt.Cens.Core.Utils
{
    public static class BranchInstructionUtil
    {
        public static int GetClockCycleIncrement(bool success, ushort oldAddress, ushort newAddress)
        {
            var cycle = 0;
            // 跳转成功
            if (success)
            {
                cycle += 1;
            }

            // 跨页访问
            if ((oldAddress & 0xFF00) != (newAddress & 0xFF00))
            {
                cycle += 1;
            }

            return cycle;
        }
    }
}