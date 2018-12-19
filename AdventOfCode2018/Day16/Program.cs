using SharedLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Day16
{
    public enum Operation
    {
        Add,
        Multiply,
        And,
        Or
    }

    public enum EqualityType
    {
        GreaterThan,
        Equal
    }

    public enum Comparison
    {
        ImmediateRegister,
        RegisterImmediate,
        RegisterRegister
    }

    public enum OperationCode
    {
        Addr,
        Addi,
        Mulr,
        Muli,
        Banr,
        Bani,
        Borr,
        Bori,
        Setr,
        Seti,
        Gtir,
        Gtri,
        Gtrr,
        Eqir,
        Eqri,
        Eqrr
    }

    class Program
    {
        static void Main(string[] args)
        {
            var codesWithAtLeast3 = 0;
            var lines = FileHelper.GetLines("Day16").ToList();

            var before = new[] { 3, 2, 1, 1 };
            var instruction = new Instruction { OpCode = 9, A = 2, B = 1, C = 2 };
            var after = new[] { 3, 2, 2, 1 };

            var operationsDictionary = new Dictionary<int, HashSet<OperationCode>>();
            var numberOfOperations = Enum.GetValues(typeof(OperationCode)).Length;
            var enumValues = Enum.GetValues(typeof(OperationCode)).Cast<OperationCode>().ToHashSet();
            for (int i = 0; i < numberOfOperations; i++)
            {
                operationsDictionary.Add(i, new HashSet<OperationCode>());
            }

            for (int i = 0; i < lines.Count(); i++)
            {
                if (lines[i] == "End") break;
                switch (i % 4)
                {
                    case 0:
                        var split = lines[i].Split(',');
                        before = new[]
                        {
                            int.Parse(split[0][split[0].Length - 1].ToString()), int.Parse(split[1]), int.Parse(split[2]), int.Parse(split[3][1].ToString())
                        };
                        break;
                    case 1:
                        var spaceSplit = lines[i].Split(' ');
                        instruction = new Instruction { OpCode = int.Parse(spaceSplit[0]), A = int.Parse(spaceSplit[1]), B = int.Parse(spaceSplit[2]), C = int.Parse(spaceSplit[3]) };
                        break;
                    case 2:
                        var split2 = lines[i].Split(',');
                        after = new[]
                        {
                            int.Parse(split2[0][split2[0].Length - 1].ToString()), int.Parse(split2[1]), int.Parse(split2[2]), int.Parse(split2[3][1].ToString())
                        };
                        break;
                    case 3:
                        var result = TestInstructions(before, instruction, after);
                        if (result.Count >= 3)
                        {
                            codesWithAtLeast3++;
                        }
                        if (operationsDictionary[instruction.OpCode].Count != 1)
                        {
                            var hashSet = operationsDictionary[instruction.OpCode];
                            if (hashSet.Count == 0)
                            {
                                hashSet = result;
                            }
                            else
                            {
                                hashSet.IntersectWith(result);
                            }

                            operationsDictionary[instruction.OpCode] = hashSet;
                        }
                        break;
                }
            }

            while (operationsDictionary.Values.Any(h => h.Count != 1))
            {
                foreach (var operationsDictionaryKey in operationsDictionary.Keys)
                {
                    var hashSet = operationsDictionary[operationsDictionaryKey];
                    if (hashSet.Count == 1)
                    {
                        foreach (var operationsDictionaryValue in operationsDictionary.Values)
                        {
                            if (operationsDictionaryValue.Count != 1 && operationsDictionaryValue.Contains(hashSet.First()))
                            {
                                operationsDictionaryValue.Remove(hashSet.First());
                            }
                        }
                    }
                }
            }

            lines = FileHelper.GetLines("Day16", "PuzzleInputPart2.txt").ToList();
            var register = new[] { 0, 0, 0, 0 };
            foreach (var line in lines)
            {
                var spaceSplit = line.Split(' ');
                instruction = new Instruction { OpCode = int.Parse(spaceSplit[0]), A = int.Parse(spaceSplit[1]), B = int.Parse(spaceSplit[2]), C = int.Parse(spaceSplit[3]) };

                var operation = operationsDictionary[instruction.OpCode].First();
                switch (operation)
                {
                    case OperationCode.Addr:
                        TestOperationRegister(register, instruction, register, Operation.Add, out register);
                        break;
                    case OperationCode.Addi:
                        TestOperationImmediate(register, instruction, register, Operation.Add, out register);
                        break;
                    case OperationCode.Mulr:
                        TestOperationRegister(register, instruction, register, Operation.Multiply, out register);
                        break;
                    case OperationCode.Muli:
                        TestOperationImmediate(register, instruction, register, Operation.Multiply, out register);
                        break;
                    case OperationCode.Banr:
                        TestOperationRegister(register, instruction, register, Operation.And, out register);
                        break;
                    case OperationCode.Bani:
                        TestOperationImmediate(register, instruction, register, Operation.And, out register);
                        break;
                    case OperationCode.Borr:
                        TestOperationRegister(register, instruction, register, Operation.Or, out register);
                        break;
                    case OperationCode.Bori:
                        TestOperationImmediate(register, instruction, register, Operation.Or, out register);
                        break;
                    case OperationCode.Setr:
                        TestAssignment(register, instruction, register, false, out register);
                        break;
                    case OperationCode.Seti:
                        TestAssignment(register, instruction, register, true, out register);
                        break;
                    case OperationCode.Gtir:
                        TestEquality(register, instruction, register, EqualityType.GreaterThan, Comparison.ImmediateRegister, out register);
                        break;
                    case OperationCode.Gtri:
                        TestEquality(register, instruction, register, EqualityType.GreaterThan, Comparison.RegisterImmediate, out register);
                        break;
                    case OperationCode.Gtrr:
                        TestEquality(register, instruction, register, EqualityType.GreaterThan, Comparison.RegisterRegister, out register);
                        break;
                    case OperationCode.Eqir:
                        TestEquality(register, instruction, register, EqualityType.Equal, Comparison.ImmediateRegister, out register);
                        break;
                    case OperationCode.Eqri:
                        TestEquality(register, instruction, register, EqualityType.Equal, Comparison.RegisterImmediate, out register);
                        break;
                    case OperationCode.Eqrr:
                        TestEquality(register, instruction, register, EqualityType.Equal, Comparison.RegisterRegister, out register);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            Console.WriteLine(register[0]);
            Console.ReadKey();
        }



        public static HashSet<OperationCode> TestInstructions(int[] before, Instruction instruction, int[] after)
        {
            var results = new HashSet<OperationCode>();
            var copy = new int[4];

            if (TestOperationRegister(before, instruction, after, Operation.Add, out copy))
                results.Add(OperationCode.Addr);
            if (TestOperationRegister(before, instruction, after, Operation.Multiply, out copy))
                results.Add(OperationCode.Mulr);
            if (TestOperationRegister(before, instruction, after, Operation.And, out copy))
                results.Add(OperationCode.Banr);
            if (TestOperationRegister(before, instruction, after, Operation.Or, out copy))
                results.Add(OperationCode.Borr);
            if (TestOperationImmediate(before, instruction, after, Operation.Add, out copy))
                results.Add(OperationCode.Addi);
            if (TestOperationImmediate(before, instruction, after, Operation.Multiply, out copy))
                results.Add(OperationCode.Muli);
            if (TestOperationImmediate(before, instruction, after, Operation.And, out copy))
                results.Add(OperationCode.Bani);
            if (TestOperationImmediate(before, instruction, after, Operation.Or, out copy))
                results.Add(OperationCode.Bori);
            if (TestAssignment(before, instruction, after, true, out copy))
                results.Add(OperationCode.Seti);
            if (TestAssignment(before, instruction, after, false, out copy))
                results.Add(OperationCode.Setr);
            if (TestEquality(before, instruction, after, EqualityType.Equal, Comparison.ImmediateRegister, out copy))
                results.Add(OperationCode.Eqir);
            if (TestEquality(before, instruction, after, EqualityType.Equal, Comparison.RegisterImmediate, out copy))
                results.Add(OperationCode.Eqri);
            if (TestEquality(before, instruction, after, EqualityType.Equal, Comparison.RegisterRegister, out copy))
                results.Add(OperationCode.Eqrr);
            if (TestEquality(before, instruction, after, EqualityType.GreaterThan, Comparison.ImmediateRegister, out copy))
                results.Add(OperationCode.Gtir);
            if (TestEquality(before, instruction, after, EqualityType.GreaterThan, Comparison.RegisterImmediate, out copy))
                results.Add(OperationCode.Gtri);
            if (TestEquality(before, instruction, after, EqualityType.GreaterThan, Comparison.RegisterRegister, out copy))
                results.Add(OperationCode.Gtrr);

            return results;
        }

        public static bool TestOperationRegister(int[] before, Instruction instruction, int[] after, Operation operation, out int[] copy)
        {
            copy = (int[])before.Clone();
            try
            {
                copy = (int[])before.Clone();
                switch (operation)
                {
                    case Operation.Add:
                        copy[instruction.C] = copy[instruction.A] + copy[instruction.B];
                        break;
                    case Operation.Multiply:
                        copy[instruction.C] = copy[instruction.A] * copy[instruction.B];
                        break;
                    case Operation.And:
                        copy[instruction.C] = copy[instruction.A] & copy[instruction.B];
                        break;
                    case Operation.Or:
                        copy[instruction.C] = copy[instruction.A] | copy[instruction.B];
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(operation), operation, null);
                }

                return copy.SequenceEqual(after);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool TestOperationImmediate(int[] before, Instruction instruction, int[] after, Operation operation, out int[] copy)
        {
            copy = (int[])before.Clone();
            try
            {
                switch (operation)
                {
                    case Operation.Add:
                        copy[instruction.C] = copy[instruction.A] + instruction.B;
                        break;
                    case Operation.Multiply:
                        copy[instruction.C] = copy[instruction.A] * instruction.B;
                        break;
                    case Operation.And:
                        copy[instruction.C] = copy[instruction.A] & instruction.B;
                        break;
                    case Operation.Or:
                        copy[instruction.C] = copy[instruction.A] | instruction.B;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(operation), operation, null);
                }
                return copy.SequenceEqual(after);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool TestAssignment(int[] before, Instruction instruction, int[] after, bool immediate, out int[] copy)
        {
            copy = (int[])before.Clone();
            try
            {
                if (immediate) copy[instruction.C] = copy[instruction.A];
                else copy[instruction.C] = instruction.A;

                return copy.SequenceEqual(after);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool TestEquality(int[] before, Instruction instruction, int[] after, EqualityType equalityType, Comparison comparison, out int[] copy)
        {
            copy = (int[])before.Clone();
            try
            {

                switch (equalityType)
                {
                    case EqualityType.GreaterThan:
                        switch (comparison)
                        {
                            case Comparison.ImmediateRegister:
                                copy[instruction.C] = instruction.A > copy[instruction.B] ? 1 : 0;
                                break;
                            case Comparison.RegisterImmediate:
                                copy[instruction.C] = copy[instruction.A] > instruction.B ? 1 : 0;
                                break;
                            case Comparison.RegisterRegister:
                                copy[instruction.C] = copy[instruction.A] > copy[instruction.B] ? 1 : 0;
                                break;
                            default:
                                throw new ArgumentOutOfRangeException(nameof(comparison), comparison, null);
                        }
                        break;
                    case EqualityType.Equal:
                        switch (comparison)
                        {
                            case Comparison.ImmediateRegister:
                                copy[instruction.C] = instruction.A == copy[instruction.B] ? 1 : 0;
                                break;
                            case Comparison.RegisterImmediate:
                                copy[instruction.C] = copy[instruction.A] == instruction.B ? 1 : 0;
                                break;
                            case Comparison.RegisterRegister:
                                copy[instruction.C] = copy[instruction.A] == copy[instruction.B] ? 1 : 0;
                                break;
                            default:
                                throw new ArgumentOutOfRangeException(nameof(comparison), comparison, null);
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(equalityType), equalityType, null);
                }

                return copy.SequenceEqual(after);
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }

    public class Instruction
    {
        public int OpCode { get; set; }
        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }
    }
}
