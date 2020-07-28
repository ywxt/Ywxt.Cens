using System;
using System.Collections.Generic;
using System.Linq;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public static class Instructions
    {
        private static readonly IReadOnlyDictionary<byte, IInstruction> InnerInstructions;


        static Instructions()
        {
            var instructions = new Dictionary<byte, IInstruction>();
            LoadInstructions(instructions);
            InnerInstructions = instructions;
        }

        public static IInstruction? Get(byte instruction)
        {
            if (InnerInstructions.TryGetValue(instruction, out var ins))
            {
                return ins;
            }
            return null;
        }

        private static void LoadInstructions(IDictionary<byte, IInstruction> instructions)
        {
            var types = typeof(Instructions).Assembly.ExportedTypes.Where(type =>
                    type.Namespace == "Ywxt.Cens.Core.Cpu.Instruction"
                    && type.GetInterfaces().Contains(typeof(IInstruction))
                )
                .Select(Activator.CreateInstance);
            foreach (var instruction in types)
            {
                if (!(instruction is IInstruction ins)) continue;
                foreach (var opCodesKey in ins.OpCodes.Keys)
                {
                    instructions.Add(opCodesKey, ins);
                }
            }
        }
    }
}