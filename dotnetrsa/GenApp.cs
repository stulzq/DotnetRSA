using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using McMaster.Extensions.CommandLineUtils;
using XC.RSAUtil;

namespace dotnetrsa
{
    [Command(Name = "dotnetrsa gen", Description = "Generate xml, pkcs1, pkcs8 keys.")]
    [HelpOption("-h|--help")]
    public class GenApp
    {
        [Option("-f|--format", Description = "Required.Gen keys's format.The value must be xml, pkcs1 ,pkcs8.")]
        [Required]
        [AllowedValues("xml", "pkcs1", "pkcs8", IgnoreCase = false)]
        public string Format { get; }

        [Option("-s|--size <int>", Description = "Key Size.Default 2048.")]
        public int KeySize { get; } = 2048;

        [Option("--pem", Description = "Pem Format. true of false.Default false.")]
        [AllowedValues("true", "false", IgnoreCase = true)]
        public string PemFormat { get; } = "false";

        [Option("-o|--output <path>", Description =
            "File output path.If you do not specify it will be output in the current directory.")]
        public string Output { get; } = Environment.CurrentDirectory;

        private int OnExecute(CommandLineApplication app)
        {
            if (app.Options.Count == 1 && app.Options[0].ShortName == "h")
            {
                app.ShowHelp();
            }

            try
            {
                bool pemFormat = bool.Parse(PemFormat);
                string publicKey = string.Empty;
                string privateKey = string.Empty;
                List<string> keyList;

                switch (Format)
                {
                    case "xml":
                        keyList = RsaKeyGenerator.XmlKey(KeySize);
                        privateKey = keyList[0];
                        publicKey = keyList[1];
                        break;
                    case "pkcs1":
                        keyList = RsaKeyGenerator.Pkcs1Key(KeySize, pemFormat);
                        privateKey = keyList[0];
                        publicKey = keyList[1];
                        break;
                    case "pkcs8":
                        keyList = RsaKeyGenerator.Pkcs8Key(KeySize, pemFormat);
                        privateKey = keyList[0];
                        publicKey = keyList[1];
                        break;
                }

                if (!Directory.Exists(Output))
                {
                    Directory.CreateDirectory(Output);
                }

                File.WriteAllText(Path.Combine(Output, "public.key"), publicKey);
                File.WriteAllText(Path.Combine(Output, "private.key"), privateKey);

                app.Out.WriteLine($"Process success.File saved in {Output}.");

            }
            catch (Exception e)
            {
                app.Out.WriteLine($"Process error.Detail message:{e.Message}");
                return 1;
            }

            return 0;
        }
    }
}