using System;

namespace PZ1
{
    class Program
    {
        delegate T CovariantDelegate<out T>();
        delegate void ContravariantDelegate<in T>(T value);

        static void Main(string[] args)
        {
            var del1 = new CovariantDelegate<MyParentType>(MyStaticMethodWithReturnParentType);
            var del2 = new CovariantDelegate<MyParentType>(MyStaticMethodWithReturnChildType);

            // Error
            //var del3 = new CovariantDelegate<MyChildType>(MyStaticMethodWithReturnParentType);
            var del4 = new CovariantDelegate<MyChildType>(MyStaticMethodWithReturnChildType);

            var del5 = new ContravariantDelegate<MyParentType>(MyStaticMethodWithInParentType);
            // Error
            //var del6 = new ContravariantDelegate<MyParentType>(MyStaticMethodWithInChildType);

            var del7 = new ContravariantDelegate<MyChildType>(MyStaticMethodWithInParentType);
            var del8 = new ContravariantDelegate<MyChildType>(MyStaticMethodWithInChildType);

            del1();
            del2.Invoke();
            del4();
            del5(new MyParentType());
            del7(new MyChildType());
            del8(new MyChildType());

            var myType = new MyType();

            var del9= new CovariantDelegate<MyParentType>(myType.MyMethodWithReturnParentType);
            var del10 = new CovariantDelegate<MyParentType>(myType.MyMethodWithReturnChildType);

            del9();
            del10();

            Console.ReadLine();
        }

        static MyParentType MyStaticMethodWithReturnParentType()
        {
            Console.WriteLine("MyStaticMethodWithReturnParentType called.");
            return new MyParentType();
        }

        static MyChildType MyStaticMethodWithReturnChildType()
        {
            Console.WriteLine("MyStaticMethodWithReturnChildType called.");
            return new MyChildType();
        }

        static void MyStaticMethodWithInParentType(MyParentType parentType)
        {
            Console.WriteLine("MyStaticMethodWithInParentType called.");
        }

        static void MyStaticMethodWithInChildType(MyChildType childType)
        {
            Console.WriteLine("MyStaticMethodWithInChildType called.");
        }

        static MyChildType MyStaticMethodWithChildType(MyChildType childType)
        {
            Console.WriteLine($"MyStaticMethodWithChildType called with {childType.GetType().Name} argument type.");
            return childType;
        }
    }

    public class MyParentType { }
    public class MyChildType : MyParentType { }
    public class MyType
    {
        public MyParentType MyMethodWithReturnParentType()
        {
            Console.WriteLine("MyMethodWithReturnParentType called.");
            return new MyParentType();
        }

        public MyChildType MyMethodWithReturnChildType()
        {
            Console.WriteLine("MyMethodWithReturnChildType called.");
            return new MyChildType();
        }
    }
}
