using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using McMaster.Extensions.CommandLineUtils;
using XC.Framework.Security.RSAUtil;

namespace dotnetrsa
{
    [Command(Name = "dotnetrsa convert", Description = "Convert you keys to xml pkcs1, pkcs8 format.")]
    [HelpOption("-h|--help")]
    public class ConvertApp
    {
        [Option("-f|--from <format>",Description = "Required.Source format.The value must be xml, pkcs1,pkcs8.")]
        [Required]
        [AllowedValues("xml", "pkcs1", "pkcs8", IgnoreCase = false)]
        public string SourceFormat { get; }

        [Option("-t|--to <format>", Description = "Required.Target format.The value must be xml, pkcs1,pkcs8.")]
        [Required]
        [AllowedValues("xml", "pkcs1", "pkcs8", IgnoreCase = false)]
        public string TargetFormat { get; }

        [Option("-k", Description = "Required.Key type.The value must be pri, pub.'pub' represents the public key.")]
        [Required]
        [AllowedValues("pub", "pri", IgnoreCase = false)]
        public string KeyType { get; }

        [Argument(0,"KeyFilePath", "Required.Secret key file path.")]
        [Required]
        [FileExists]
        public string KeyFilePath { get; }

        [Option("-o|--output <path>", Description =
            "File output path.If you do not specify it will be output in the current directory.")]
        public string Output { get; } = Environment.CurrentDirectory;
        private int OnExecute(CommandLineApplication app)
        {
            if (app.Options.Count == 1 && app.Options[0].ShortName=="h")
            {
                app.ShowHelp();
                return 0;
            }

            if (KeyType == "pub" && ((SourceFormat == "pkcs1" && TargetFormat == "pkcs8") ||
                                     (SourceFormat == "pkcs8" && TargetFormat == "pkcs1")))
            {
                app.Out.WriteLine("This public key does not need to be converted.");
                return 1;
            }

            if (SourceFormat == TargetFormat)
            {
                app.Out.WriteLine("Target format can not equal Source format.");
                return 1;
            }

            try
            {
                string result = string.Empty;
                string keyContent = File.ReadAllText(KeyFilePath);

                if (KeyType == "pri")
                {
                    switch ($"{SourceFormat}->{TargetFormat}")
                    {
                        case "xml->pkcs1":
                            result = RsaKeyConvert.PrivateKeyXmlToPkcs1(keyContent);
                            break;
                        case "xml->pkcs8":
                            result = RsaKeyConvert.PrivateKeyXmlToPkcs8(keyContent);
                            break;
                        case "pkcs1->xml":
                            result = RsaKeyConvert.PrivateKeyPkcs1ToXml(keyContent);
                            break;
                        case "pkcs1->pkcs8":
                            result = RsaKeyConvert.PrivateKeyPkcs1ToPkcs8(keyContent);
                            break;
                        case "pkcs8->xml":
                            result = RsaKeyConvert.PrivateKeyPkcs8ToXml(keyContent);
                            break;
                        case "pkcs8->pkcs1":
                            result = RsaKeyConvert.PrivateKeyPkcs8ToPkcs1(keyContent);
                            break;
                    }
                }
                else
                {
                    result = SourceFormat == "xml" ? RsaKeyConvert.PublicKeyXmlToPem(keyContent) : RsaKeyConvert.PublicKeyPemToXml(keyContent);
                }

                if (!Directory.Exists(Output))
                {
                    Directory.CreateDirectory(Output);
                }
                string fileName = $"{new FileInfo(KeyFilePath).Name}.new.key";
                string savePath = Path.Combine(Output, fileName);
                File.WriteAllText(savePath, result);
                app.Out.WriteLine($"Process success.File saved in {savePath}.");
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