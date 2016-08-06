using System;
using DependencyInjection.Extensions;

namespace DependencyInjection
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var cat = new Cat();
            cat.Register<IFoo, Foo>();
            cat.Register<IBar, Bar>();
            cat.Register<IBaz, Baz>();
            cat.Register<IQux, Qux>();

            var service = cat.GetService<IFoo>();
            var foo = (Foo) service;
            var baz = (Baz) foo.Baz;

            Console.WriteLine("cat.GetService<IFoo>(): {0}", service);
            Console.WriteLine("cat.GetService<IFoo>().Bar: {0}", foo.Bar);
            Console.WriteLine("cat.GetService<IFoo>().Baz: {0}", foo.Baz);
            Console.WriteLine("cat.GetService<IFoo>().Baz.Qux: {0}", baz.Qux);

            Console.ReadKey();
        }
    }

    public interface IFoo
    {
    }

    public interface IBar
    {
    }

    public interface IBaz
    {
    }

    public interface IQux
    {
    }

    public class Foo : IFoo
    {
        public Foo()
        {
        }

        [Injection]
        public Foo(IBar bar)
        {
            Bar = bar;
        }

        public IBar Bar { get; }

        [Injection]
        public IBaz Baz { get; set; }
    }

    public class Bar : IBar
    {
    }

    public class Baz : IBaz
    {
        public IQux Qux { get; private set; }

        [Injection]
        public void Initialize(IQux qux)
        {
            Qux = qux;
        }
    }

    public class Qux : IQux
    {
    }
}