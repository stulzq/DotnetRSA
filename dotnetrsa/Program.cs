

using System;
using McMaster.Extensions.CommandLineUtils;

namespace dotnetrsa
{
    [Command(Name = "dotnetrsa", Description = "DotnetRSA is a .NET Core Global Tool.Dotnet RSA Tool can help you generate xml pkcs1, pkcs8 three kinds of format keys, and supports three types of mutual conversion.Github: https://github.com/stulzq/dotnetrsa")]
    [Subcommand("gen",typeof(GenApp))]
    [Subcommand("convert", typeof(ConvertApp))]
    class Program
    {
        public static int Main(string[] args)
        {
            return CommandLineApplication.Execute<Program>(args);
        }

        private int OnExecute(CommandLineApplication app)
        {
            app.ShowHelp();
            return 0;
        }
    }
}
