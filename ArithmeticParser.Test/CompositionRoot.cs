using Autofac;

namespace ArithmeticParser.Test
{
    internal class CompositionRoot
    {
        public IContainer Build()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new ParserModule());

            return builder.Build();
        }
    }
}