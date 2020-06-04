using System;
using System.IO;
using Xunit;
using Xunit.Abstractions;
using Ywxt.Cens.Core.Rom;

namespace Ywxt.Cens.Core.Test
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public UnitTest1(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Test1()
        {
            var file = File.ReadAllBytes("./nestest.nes");
            var sram = new byte[0x2000];
            var ines = new Ines(file);
            var cat = new Cartridge(ines, sram);
            var cpu = new Cpu.Cpu(cat);
            cpu.Reset();
            cpu.Registers.Pc = 0xC000;
            var address = cpu.Registers.Pc;
            var cycle = 0;
            do
            {
                
            } while (true);
        }
    }
}