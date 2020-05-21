using System;

namespace Ywxt.Cens.Core.Exceptions
{
    public class MapperException:Exception
    {
        public MapperException(string message, byte mapperNumber) : base($"{message}, Mapper号: {mapperNumber}")
        {
            MapperNumber = mapperNumber;
        }

        public byte MapperNumber { get; }
        
    }
}