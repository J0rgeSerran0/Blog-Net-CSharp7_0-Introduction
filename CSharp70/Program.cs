using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CSharp70
{

    public class Program
    {
        private static Random random;

        public static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("C# 7.0 - Specification");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("https://github.com/J0rgeSerran0");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("------------------------------");
            Console.WriteLine("Generalized async return types");
            Console.WriteLine("------------------------------");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"The sum of the values is {GetValues().Result}");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------");
            Console.WriteLine("Discards");
            Console.WriteLine("--------");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Discards();

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("----------------------------");
            Console.WriteLine("Tuples (Tuples Enhancements)");
            Console.WriteLine("----------------------------");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Tuples();
            TuplesEnhancements();

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------------");
            Console.WriteLine("Deconstruction");
            Console.WriteLine("--------------");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Deconstruction();

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("-------------");
            Console.WriteLine("Out Variables");
            Console.WriteLine("-------------");
            Console.ForegroundColor = ConsoleColor.Cyan;
            OutStandard();
            OutVariables();

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("-------------------------");
            Console.WriteLine("Expression Bodied Members");
            Console.WriteLine("-------------------------");
            Console.ForegroundColor = ConsoleColor.Cyan;
            ExpressionBodiedMembers();

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("-----------------");
            Console.WriteLine("Throw Expressions");
            Console.WriteLine("-----------------");
            Console.ForegroundColor = ConsoleColor.Cyan;
            try
            {
                ThrowExpressions(null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------------------------");
            Console.WriteLine("Ref returns and Ref locals");
            Console.WriteLine("--------------------------");
            Console.ForegroundColor = ConsoleColor.Cyan;
            RefReturnsAndLocals();

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("----------------");
            Console.WriteLine("Pattern Matching");
            Console.WriteLine("----------------");
            Console.ForegroundColor = ConsoleColor.Cyan;
            PatternMatching();

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("----------------");
            Console.WriteLine("Digit Separators");
            Console.WriteLine("----------------");
            Console.ForegroundColor = ConsoleColor.Cyan;
            DigitSeparators();

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("---------------");
            Console.WriteLine("Binary Literals");
            Console.WriteLine("---------------");
            Console.ForegroundColor = ConsoleColor.Cyan;
            BinaryLiterals();

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("---------------");
            Console.WriteLine("Local Functions");
            Console.WriteLine("---------------");
            Console.ForegroundColor = ConsoleColor.Cyan;
            LocalFunctions();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("Press any key to close");
            Console.ReadLine();
        }

        #region "Generalized async return types"

        private static async ValueTask<int> GetValues()
        {
            Console.WriteLine("Getting values");

            var value1 = await GetRandomValue();
            var value2 = await GetRandomValue();

            Console.WriteLine($"\tValue 1 => {value1}");
            Console.WriteLine($"\tValue 2 => {value2}");

            return value1 + value2;
        }

        private static async ValueTask<int> GetRandomValue()
        {
            random = random == null ? new Random() : random;

            await Task.Delay(100);

            var value = random.Next(1, 101);

            return value;
        }

        #endregion

        #region "Discards"

        private static void Discards()
        {
            // Traditional use (without Discards)
            bool parsedValue;
            if (bool.TryParse("TRUE", out parsedValue))
            {
                Console.WriteLine($"{parsedValue} without Discards");
            }

            // Using Discards
            if (bool.TryParse("TRUE", out bool _))
            {
                Console.WriteLine("True using Discards");
            }
        }

        #endregion

        #region "Tuples (Tuples Enhancements)"

        private static List<object> GetCollection()
        {
            var collection = new List<object>();

            collection.Add(new { Id = 1, Name = "John", Age = 32 });
            collection.Add(new { Id = 2, Name = "Mary", Age = 29 });
            collection.Add(new { Id = 3, Name = "Robert", Age = 24 });
            collection.Add(new { Id = 4, Name = "Loorena", Age = 34 });

            return collection;
        }

        private static void Tuples()
        {
            random = random == null ? new Random() : random;

            var id = random.Next(1, 5);

            var element = GetTuplePersonBy(id);

            Console.WriteLine("Tuples => standard (reference type)");
            Console.WriteLine($"\tid: {id}");
            Console.WriteLine($"\tName: {element.Item1} - Age: {element.Item2}");
        }

        private static Tuple<string, int> GetTuplePersonBy(int id)
        {
            var element = (GetCollection()).Cast<dynamic>().Where(p => p.Id == id).SingleOrDefault();

            return new Tuple<string, int>(element.Name, element.Age);
        }

        private static void TuplesEnhancements()
        {
            random = random == null ? new Random() : random;

            var id = random.Next(1, 5);

            var element = GetTuplesEnhancementsPersonBy(id);

            Console.WriteLine("Tuples => Enhancements (value type)");
            Console.WriteLine($"\tid: {id}");
            Console.WriteLine($"\tName: {element.Item1} - Age: {element.Item2}");
        }

        private static (string, int) GetTuplesEnhancementsPersonBy(int id)
        {
            var element = (GetCollection()).Cast<dynamic>().Where(p => p.Id == id).SingleOrDefault();

            return (element.Name, element.Age);
        }

        #endregion

        #region "Deconstruction"

        private static void Deconstruction()
        {
            var(one, two, three) = GetData();

            Console.WriteLine($"{three} :: {one} is {two} years old");
        }

        private static (string, int, DateTime) GetData()
        {
            var name = "Charles";
            var age = 30;
            var date = DateTime.UtcNow;

            return (name, age, date);
        }

        #endregion

        #region "Out variables"

        private static void OutStandard()
        {
            int value1 = 1;
            int value2 = 2;

            int result;

            GetGreaterOrEqual(value1, value2, out result);

            Console.WriteLine("C# 6.0");
            Console.WriteLine($"\tThe greater or equal value between {value1} and {value2} is {result}");
        }

        private static void OutVariables()
        {
            int value1 = 1;
            int value2 = 2;

            GetGreaterOrEqual(value1, value2, out int result);

            Console.WriteLine("C# 7.0");
            Console.WriteLine($"\tThe greater or equal value between {value1} and {value2} is {result}");
        }

        private static void GetGreaterOrEqual(int value1, int value2, out int greaterValue)
        {
            if (value1 >= value2)
            {
                greaterValue = value1;
            }
            else
            {
                greaterValue = value2;
            }
        }

        #endregion

        #region "Expression Bodied Members"

        //// Method => C# 5.0
        //private static bool IsEqual(int value1, int value2)
        //{
        //    return value1 == value2;
        //}

        //// Method => C# 6.0
        //private static bool IsEqual(int value1, int value2) => value1 == value2;


        //// Property => C# 5.0
        //public static DateTime GetDateTime
        //{
        //    get
        //    {
        //        return DateTime.UtcNow;
        //    }
        //}

        //// Property => C# 6.0
        //public static DateTime GetDateTime => DateTime.UtcNow;


        // Class => C# 7.0
        private class Foo
        {
            public Foo() => Console.WriteLine($"Class => Constructor of {nameof(Foo)}");
            ~Foo() => Console.WriteLine($"Class => Destructor of {nameof(Foo)}");

        }

        // Property Accessors => C# 7.0
        private static int _value;
        public static int Value
        {
            get => _value;
            set => _value = value;
            }

        private static void ExpressionBodiedMembers()
        {
            var foo = new Foo();
            foo = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();

            Value = 7;
            Console.WriteLine($"Property Accessors => {Value}");
        }

        #endregion

        #region "Throw Expressions"

        private static void ThrowExpressions(string name)
        {
            var firstName = name ?? throw new ArgumentNullException();

            Console.WriteLine(firstName);
        }

        #endregion

        #region "Ref returns and Ref locals"

        private class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }

        private static void RefReturnsAndLocals()
        {
            var people = new Person[] { new Person() { Name = "Charles", Age = 19 }, new Person() { Name = "Mary", Age = 24 } };

            try
            {
                ref Person person = ref GetPersonBy("Mary", people);
                Console.WriteLine($"{person.Name} is {person.Age} years old");

                ref Person personThrowException = ref GetPersonBy("Aaa", people);
                Console.WriteLine($"{personThrowException.Name} is {personThrowException.Age} years old");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static ref Person GetPersonBy(string name, Person[] people)
        {
            if (people.Where(a => a.Name == name).SingleOrDefault() == null)
            {
                throw new IndexOutOfRangeException($"{nameof(name)} {name} not found");
            }

            var position = people.Select((Value, Index) => new { Value, Index }).Single(p => p.Value.Name == name);

            return ref people[position.Index];
        }

        #endregion

        #region "Pattern Matching"

        private class Cat { public string Name { get; set; } }
        private class Dog { public string Name { get; set; } }
        private class Horse { public string Name { get; set; } }

        private static void PatternMatching()
        {
            Console.WriteLine("is-expressions");
            ConvertToUpperCase("\tpattern matching");

            Console.WriteLine("switch-statements");
            Console.WriteLine("\t" + WhoIam(new Dog { Name = "Pupi" }));
            try
            {
                Console.WriteLine("throwing an exception");
                Console.WriteLine("\t" + WhoIam(new Tuple<string>("foo")));
            }
            catch (Exception ex)
            {
                Console.WriteLine("\t" + ex.Message);
            }
        }

        private static void ConvertToUpperCase(object o)
        {
            // Constant pattern (null)
            if (o is null) return;
            // Type pattern (int i)
            if (!(o is string s)) return;

            Console.WriteLine(s.ToUpper());
        }

        private static string WhoIam(object o)
        {
            switch (o)
            {
                case Cat s:
                    return $"{o.GetType().Name} is {s.Name}";
                case Dog s:
                    return $"{o.GetType().Name} is {s.Name}";
                case Horse s:
                    return $"{o.GetType().Name} is {s.Name}";
                default:
                    throw new ArgumentException(message: "the object is not a recognized expected object", paramName: nameof(o));
            }
        }

        #endregion

        #region "Digit Separators"

        private static void DigitSeparators()
        {
            var number = 123_456;

            Console.WriteLine("123_456 => " + number);
            Console.WriteLine("123_456 + 6543_21 => " + (number + 6543_21));
        }

        #endregion

        #region "Binary Literals"

        private static void BinaryLiterals()
        {
            byte aa = 0b10101010;
            byte dd = 0b11011101;
            byte ff = 0b11111111;

            Console.WriteLine($"{aa:X}{dd:X}{ff:X}");
        }

        #endregion

        #region "Local Functions"

        private static void LocalFunctions()
        {
            GetURLFriendly("Esto es una pasada. La versión 2.0, es muy interesante");
        }

        private static void GetURLFriendly(string title)
        {
            Console.WriteLine(title);
            Console.WriteLine(ConvertToUrlFriendly(title));

            string ConvertToUrlFriendly(string text)
            {
                var slugCharacter = "-";

                var normalizedString = text.Normalize(NormalizationForm.FormD);
                var stringBuilder = new StringBuilder();

                foreach (var character in normalizedString)
                {
                    var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(character);
                    if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                    {
                        stringBuilder.Append(character);
                    }
                }

                var result = stringBuilder.ToString().Normalize(NormalizationForm.FormC);

                var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(result);

                return Regex.Replace(Regex.Replace(Encoding.ASCII.GetString(bytes), @"\s{2,}|[^\w]", " ", RegexOptions.ECMAScript).Trim(), @"\s+", slugCharacter.ToString()).ToLowerInvariant();
            }
        }

        #endregion

    }

}