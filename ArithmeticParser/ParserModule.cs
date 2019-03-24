using ArithmeticParser.Parsing;
using Autofac;

namespace ArithmeticParser
{
    public class ParserModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Parser>().AsSelf();
            builder.RegisterType<ExpressionParser>().AsSelf().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            builder.RegisterType<TermParser>().AsSelf();
            builder.RegisterType<FactorParser>().AsSelf();
            builder.RegisterType<FunctionParser>().AsSelf();
            builder.RegisterType<VariableParser>().AsSelf();
            builder.RegisterType<FactorParser>().AsSelf();
        }
    }
}
