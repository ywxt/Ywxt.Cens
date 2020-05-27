using System.Collections.Generic;
using Ywxt.Cens.Core.Exceptions;

namespace Ywxt.Cens.Core.Cpu
{
    public static class Instructions
    {
        private static IReadOnlyDictionary<byte, IInstruction> _instructions;


        static Instructions()
        {
            var insturctions = new Dictionary<byte, IInstruction>();
            _instructions = insturctions;
        }

        public static IInstruction Get(byte instruction)
        {
            if (_instructions.TryGetValue(instruction, out var ins))
            {
                return ins;
            }

            throw new UnknownInstructionException(instruction);
        }
    }
}