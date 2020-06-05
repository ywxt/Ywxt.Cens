using System;
using Ywxt.Cens.Core.Exceptions;

namespace Ywxt.Cens.Core.Rom
{
    public sealed class MapperFactory
    {
        private MapperFactory()
        {
        }

        public static readonly MapperFactory Factory = new MapperFactory();

        public IMapper GetMapper(byte number, Ines ines, Memory<byte> sram)
        {
            return number switch
            {
                0 => GetMapper0(ines, sram),
                _ => throw new MapperException("未知的Mapper号", number),
            };
        }

        private IMapper GetMapper0(Ines ines, Memory<byte> sram)
        {
            return new Mapper0(ines.Prg, sram, ines.Chr);
        }
    }
}