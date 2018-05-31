

using System;
using McMaster.Extensions.CommandLineUtils;

namespace dotnetrsa
{
    class Program
    {
        public static int Main(string[] args)
        {
            Span<string> spanArgs = args;

            if (spanArgs.Length == 0)
            {
                return ShowHelp();
            }

            var subArgs = spanArgs.Slice(1).ToArray();
            var action = spanArgs[0];

            switch (action)
            {
                case "convert":
                    return CommandLineApplication.Execute<ConvertApp>(subArgs);
                case "gen":
                    return CommandLineApplication.Execute<GenApp>(subArgs);
                default:
                    return ShowHelp();
            }
        }

        public static int ShowHelp()
        {
            Console.WriteLine("Dotnet RSA Tool can help you generate xml pkcs1, pkcs8 three kinds of format keys, and supports three types of mutual conversion.");
            Console.WriteLine();
            Console.WriteLine("Command:");
            Console.WriteLine("\tdotnetrsa convert        Convert you keys to xml pkcs1, pkcs8 format.");
            Console.WriteLine("\tdotnetrsa gen            Generate xml, pkcs1, pkcs8 keys.");
            return 0;
        }
    }
}
