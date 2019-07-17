using System;
using apophis.Lexer;
using apophis.Lexer.Tokens;
using ArithmeticParser.Lexing;
using ArithmeticParser.Parsing;
using ArithmeticParser.Tokens;
using Autofac;

namespace ArithmeticParser
{
    public class ParserModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Parser>().AsSelf().SingleInstance();
            builder.RegisterType<ExpressionParser>().AsSelf().SingleInstance().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            builder.RegisterType<TermParser>().AsSelf().SingleInstance();
            builder.RegisterType<PowerTermParser>().AsSelf().SingleInstance();
            builder.RegisterType<FactorParser>().AsSelf().SingleInstance();
            builder.RegisterType<FunctionParser>().AsSelf().SingleInstance();
            builder.RegisterType<VariableParser>().AsSelf().SingleInstance();
            builder.RegisterType<FactorParser>().AsSelf().SingleInstance();
            builder.RegisterType<FactorParser>().AsSelf().SingleInstance();
            builder.Register(context => new TokenWalker(context.Resolve<Tokenizer>(), () => new EpsilonToken(), t => t.GetType() != typeof(WhiteSpaceToken)));
            builder.RegisterType<LexerRules>().As<ILexerRules>().InstancePerDependency();
            builder.RegisterType<Tokenizer>().AsSelf().InstancePerDependency();
            builder.RegisterType<LexerReader>().As<ILexerReader>().InstancePerDependency();
        }
    }
}
