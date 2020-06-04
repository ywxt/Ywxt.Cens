using System;
using System.Collections.Generic;
using System.Linq;
using Ywxt.Cens.Core.Exceptions;

namespace Ywxt.Cens.Core.Cpu
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

        public static IInstruction Get(byte instruction)
        {
            if (InnerInstructions.TryGetValue(instruction, out var ins))
            {
                return ins;
            }

            throw new UnknownInstructionException(instruction);
        }

        private static void LoadInstructions(IDictionary<byte, IInstruction> instructions)
        {
            var types = typeof(Instructions).Assembly.ExportedTypes.Where(type =>
                    type.Namespace == "Ywxt.Cens.Core.Cpu"
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

        public class JmpInstruction : IInstruction
        {
            public IReadOnlyDictionary<byte, (AddressingType addrType, AddressingMode addrMode)> OpCodes { get; }
                = new Dictionary<byte, (AddressingType addrType, AddressingMode addrMode)>
                {
                    {0x4C, (AddressingType.Address, AddressingMode.AbsoluteAddressingMode)},
                    {0x6C, (AddressingType.Address, AddressingMode.IndirectAddressingMode)},
                };

            public int Invoke(ICpu cpu, byte instruction, ushort data)
            {
                cpu.Registers.Pc = data;
                return instruction switch
                {
                    0x4C => 3,
                    0x6C => 5,
                    _ => 0,
                };
            }
        }

        public class LdxInstruction : IInstruction
        {
            public IReadOnlyDictionary<byte, (AddressingType addrType, AddressingMode addrMode)> OpCodes { get; }
                = new Dictionary<byte, (AddressingType addrType, AddressingMode addrMode)>
                {
                    {0xA2, (AddressingType.Data, AddressingMode.ImmediateAddressingMode)}
                };

            public int Invoke(ICpu cpu, byte instruction, ushort data)
            {
                cpu.Registers.X = (byte) data;
                if (cpu.Registers.X == 0)
                {
                    cpu.Registers.P |= PFlags.Z;
                }

                if (cpu.Registers.X >> 7 == 1)
                {
                    cpu.Registers.P |= PFlags.N;
                }

                return instruction switch
                {
                    0xA2 => 2,
                    _ => 0
                };
            }
        }

        public class StxInstruction : IInstruction
        {
            public IReadOnlyDictionary<byte, (AddressingType addrType, AddressingMode addrMode)> OpCodes { get; }
                = new Dictionary<byte, (AddressingType addrType, AddressingMode addrMode)>
                {
                    {0x86, (AddressingType.Address, AddressingMode.ZeroPageAddressingMode)}
                };

            public int Invoke(ICpu cpu, byte instruction, ushort data)
            {
                cpu.Bus.WriteByte(data, cpu.Registers.X);
                return instruction switch
                {
                    0x86 => 3,
                    _ => 0
                };
            }
        }

        public class JsrInstruction : IInstruction
        {
            public IReadOnlyDictionary<byte, (AddressingType addrType, AddressingMode addrMode)> OpCodes { get; }
                = new Dictionary<byte, (AddressingType addrType, AddressingMode addrMode)>
                {
                    {0x20, (AddressingType.Address, AddressingMode.AbsoluteAddressingMode)}
                };

            public int Invoke(ICpu cpu, byte instruction, ushort data)
            {
                cpu.Stack.PushWord((ushort) (cpu.Registers.Pc - 1));
                cpu.Registers.Pc = data;
                return instruction switch
                {
                    0x20 => 6,
                    _ => 0,
                };
            }
        }

        public class NopInstruction : IInstruction
        {
            public IReadOnlyDictionary<byte, (AddressingType addrType, AddressingMode addrMode)> OpCodes { get; }
                = new Dictionary<byte, (AddressingType addrType, AddressingMode addrMode)>
                {
                    {0xEA, (AddressingType.Address, AddressingMode.ImplicitAddressingMode)}
                };

            public int Invoke(ICpu cpu, byte instruction, ushort data)
            {
                return instruction switch
                {
                    0xEA => 2,
                    _ => 0,
                };
            }
        }

        public class SecInstruction : IInstruction
        {
            public IReadOnlyDictionary<byte, (AddressingType addrType, AddressingMode addrMode)> OpCodes { get; }
                = new Dictionary<byte, (AddressingType addrType, AddressingMode addrMode)>
                {
                    {0x38, (AddressingType.Address, AddressingMode.ImplicitAddressingMode)}
                };

            public int Invoke(ICpu cpu, byte instruction, ushort data)
            {
                cpu.Registers.P |= PFlags.C;
                return instruction switch
                {
                    0x38 => 2,
                    _ => 0,
                };
            }
        }

        public class BcsInstruction : IInstruction
        {
            public IReadOnlyDictionary<byte, (AddressingType addrType, AddressingMode addrMode)> OpCodes { get; }
                = new Dictionary<byte, (AddressingType addrType, AddressingMode addrMode)>
                {
                    {0xB0, (AddressingType.Address, AddressingMode.RelativeAddressingMode)}
                };

            public int Invoke(ICpu cpu, byte instruction, ushort data)
            {
                var oldAddress = cpu.Registers.Pc;
                var jmpSuccess = cpu.Registers.P.HasFlag(PFlags.C);
                if (jmpSuccess)
                {
                    cpu.Registers.Pc = data;
                }

                var cycle = 0;
                // 跳转成功
                if (jmpSuccess)
                {
                    cycle += 1;
                }

                // 跨页访问
                if ((oldAddress & 0xFF00) != (cpu.Registers.Pc & 0xFF00))
                {
                    cycle += 1;
                }

                return instruction switch
                {
                    0xB0 => 2 + cycle,
                    _ => 0
                };
            }
        }
        public class ClcInstruction : IInstruction
        {
            public IReadOnlyDictionary<byte, (AddressingType addrType, AddressingMode addrMode)> OpCodes { get; }
                = new Dictionary<byte, (AddressingType addrType, AddressingMode addrMode)>
                {
                    {0x18, (AddressingType.Address, AddressingMode.ImplicitAddressingMode)}
                };

            public int Invoke(ICpu cpu, byte instruction, ushort data)
            {
                cpu.Registers.P &= ~PFlags.C;
                return instruction switch
                {
                    0x18 => 2,
                    _ => 0,
                };
            }
        }
    }
}