using apophis.Lexer;
using Autofac;

namespace ArithmeticParser.Test
{
    public class ParserModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Parser>().AsSelf().SingleInstance();
            builder.RegisterType<Tokenizer>().AsSelf().InstancePerDependency();
            builder.RegisterType<LexerReader>().As<ILexerReader>().InstancePerDependency();
        }
    }
}
