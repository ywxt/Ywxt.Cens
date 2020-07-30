using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Xunit;
using Xunit.Abstractions;
using Ywxt.Cens.Core.Cpu;
using Ywxt.Cens.Core.Rom;

namespace Ywxt.Cens.Core.Test
{
    public class CpuTest
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly Cartridge _cartridge;
        private readonly IEnumerator<Match> _textLog;

        private bool _isEnd;

        public CpuTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            var file = File.ReadAllBytes("./nestest.nes");
            var log = File.ReadAllText("./nestext.txt");

            var sram = new byte[0x2000];
            var ines = new Ines(file);
            _cartridge = new Cartridge(ines, sram);
            _textLog = ((IEnumerable<Match>) Regex.Matches(log,
                    @"^(?<ADDR>[A-Z0-9]{4})  ([A-Z0-9]{2} )+\s+A:(?<A>[A-Z0-9]{2}) X:(?<X>[A-Z0-9]{2}) Y:(?<Y>[A-Z0-9]{2}) P:(?<P>[A-Z0-9]{2}) SP:(?<SP>[A-Z0-9]{2})  CYC:(?<CYC>\d+)",
                    RegexOptions.Multiline))
                .GetEnumerator();
        }

        [Fact]
        public void CpuInstructionTest()
        {
            var cpu = new Cpu.Cpu(_cartridge);
            cpu.Reset();
            cpu.Registers.Pc = 0xC000;
            cpu.IsDevelopment = true;
            cpu.StepBeforeEvent += (registers, stack, cycles) =>
            {
                _testOutputHelper.WriteLine("{0:X2}  A:{1:X2} X:{2:X2} Y:{3:X2} P:{4:X2} SP:{5:X2} CYC:{6}",
                    (int) registers.Pc, (int) registers.A, (int) registers.X, (int) registers.Y, (int) registers.P,
                    (int) registers.Sp, cycles);
                Assert.True(Check(cpu.Registers, cycles), "寄存器状态校验失败");
            };
            while (!_isEnd)
            {
                cpu.Clock();
            }
        }

        private bool Check(Registers registers, int cycle)
        {
            // 测试结束
            if (!_textLog.MoveNext())
            {
                _isEnd = true;
                return true;
            }

            var match = _textLog.Current;
            if (match == null) return true;
            _testOutputHelper.WriteLine(
                $"{match.Groups["ADDR"].Value}  A:{match.Groups["A"].Value} X:{match.Groups["X"].Value} Y:{match.Groups["Y"].Value} P:{match.Groups["P"].Value} SP:{match.Groups["SP"].Value} CYC:{match.Groups["CYC"].Value}");
            return
                match.Groups["ADDR"].Value == ((int) registers.Pc).ToString("X4") &&
                match.Groups["A"].Value == ((int) registers.A).ToString("X2") &&
                match.Groups["P"].Value == ((int) registers.P).ToString("X2") &&
                match.Groups["X"].Value == ((int) registers.X).ToString("X2") &&
                match.Groups["Y"].Value == ((int) registers.Y).ToString("X2") &&
                match.Groups["SP"].Value == ((int) registers.Sp).ToString("X2") &&
                match.Groups["CYC"].Value == cycle.ToString();
        }
    }
}