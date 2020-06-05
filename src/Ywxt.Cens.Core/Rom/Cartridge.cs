using System;

namespace Ywxt.Cens.Core.Rom
{
    /// <summary>
    /// 卡带
    /// </summary>
    public sealed class Cartridge
    {
        public IMapper Mapper { get;}

        public Ines Ines { get;}

        public Memory<byte> Sram { get;}

        public Cartridge(Ines ines, Memory<byte> sram)
        {
            var mapper = MapperFactory.Factory.GetMapper(ines.Header.MapperNumber(), ines, sram);
            Mapper = mapper;
            Ines = ines;
            Sram = sram;
        }
        
    }
}