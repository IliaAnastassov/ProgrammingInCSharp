namespace Chapter2_Objective5
{
    using System;
    using System.CodeDom;
    using System.CodeDom.Compiler;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Microsoft.CSharp;

    public class Program
    {
        public static void Main(string[] args)
        {

        }

        private static void CreateHelloWorldWithExpressionTree()
        {
            var blockExpression = Expression.Block(
                Expression.Call(
                    null,
                    typeof(Console).GetMethod("Write", new Type[] { typeof(string) }),
                    Expression.Constant("Hello ")),
                Expression.Call(
                    null,
                    typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }),
                    Expression.Constant("World")));

            Expression.Lambda<Action>(blockExpression).Compile();
        }

        private static void GenerateSourceFileFromCodeCompileUnit()
        {
            var compileUnit = new CodeCompileUnit();
            var myNamespace = new CodeNamespace("MyNamespace");
            myNamespace.Imports.Add(new CodeNamespaceImport("System"));
            var myclass = new CodeTypeDeclaration("MyClass");
            var start = new CodeEntryPointMethod();
            var consoleWriteline = new CodeMethodInvokeExpression(
                                                                  new CodeTypeReferenceExpression("Console"),
                                                                  "WriteLine",
                                                                  new CodePrimitiveExpression("Hello, World!"));

            compileUnit.Namespaces.Add(myNamespace);
            myNamespace.Types.Add(myclass);
            myclass.Members.Add(start);
            start.Statements.Add(consoleWriteline);

            var provider = new CSharpCodeProvider();

            using (var streamWriter = new StreamWriter("HelloWorld.cs", false))
            {
                var textWriter = new IndentedTextWriter(streamWriter, "    ");
                provider.GenerateCodeFromCompileUnit(compileUnit, textWriter, new CodeGeneratorOptions());
                textWriter.Close();
            }
        }

        private static void ExecuteMethodWithReflection()
        {
            var number = 66;
            var compareToMethod = number.GetType().GetMethod("CompareTo", new Type[] { typeof(int) });
            var result = (int)compareToMethod.Invoke(number, new object[] { 65 });
            Console.WriteLine(result);
        }

        private static void DumpObject(object obj)
        {
            var fields = obj.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var field in fields)
            {
                Console.WriteLine($"{field.Name}: {field.GetValue(obj)}");
            }
        }

        private static void InspectAssembly()
        {
            var mscorlib = Assembly.Load("mscorlib");

            var abstractTypes = mscorlib.GetTypes()
                                         .Where(t => t.IsAbstract)
                                         .ToList();

            foreach (var abstractType in abstractTypes)
            {
                Console.WriteLine(abstractType.Name);
            }
        }

        private static void UseBasicReflection()
        {
            var number = 66;
            var type = number.GetType();
            Console.WriteLine(type.Assembly);
        }

        private static void FindCustomAttribute()
        {
            var conditionalAttribute = (ConditionalAttribute)Attribute.GetCustomAttribute(
                                                                                          typeof(ConditionalClass).GetMethod("ConditionalMethod"),
                                                                                          typeof(ConditionalAttribute));
            var condition = conditionalAttribute.ConditionString;
            Console.WriteLine(condition);
        }

        private static void VerifyDefinedAttributes()
        {
            if (Attribute.IsDefined(typeof(Person), typeof(SerializableAttribute)))
            {
                Console.WriteLine("The Person class is serializable");
            }
            else
            {
                Console.WriteLine("The Person class is not serializable");
            }
        }
    }
}
