# DotnetRSA | [中文](README_zh-cn.md)

[![NuGet][main-nuget-badge]][main-nuget]

[main-nuget]: https://www.nuget.org/packages/dotnetrsa/
[main-nuget-badge]: https://img.shields.io/nuget/v/dotnetrsa.svg?style=flat-square&amp;amp;label=nuget

DotnetRSA is a .NET Core Global Tool.Dotnet RSA Tool can help you generate xml pkcs1, pkcs8 three kinds of format keys, and supports three types of mutual conversion.

>More https://github.com/natemcmaster/dotnet-tools

## Install

Install dotnetrsa as a .NET Core Global tool using the following command:

```
dotnet tool install -g dotnetrsa
```

You have it now available on your command line: 

```
dotnetrsa --help
```

*Note: to use CLI tool command you must have .NET Core 2.1 or higher.* 

## Usage

```
Usage: dotnetrsa [options] [command]

Options:
  -?|-h|--help  Show help information

Commands:
  convert       Convert you keys to xml pkcs1, pkcs8 format.
  gen           Generate xml, pkcs1, pkcs8 keys.

Run 'dotnetrsa [command] --help' for more information about a command.
```
### • `convert` command

This command can convert you keys to xml pkcs1, pkcs8 format.such as xml->pkcs1, xml->pkcs8.

````
Convert you keys to xml pkcs1, pkcs8 format.

Usage: dotnetrsa convert [arguments] [options]

Arguments:
  KeyFilePath         Required.Secret key file path.

Options:
  -h|--help           Show help information
  -f|--from <format>  Required.Source format.The value must be xml, pkcs1,pkcs8.
  -t|--to <format>    Required.Target format.The value must be xml, pkcs1,pkcs8.
  -k                  Required.Key type.The value must be pri, pub.'pub' represents the public key.
  -o|--output <path>  File output path.If you do not specify it will be output in the current directory.
````

### • `gen` command

This command can generate xml, pkcs1, pkcs8 keys.

````
Generate xml, pkcs1, pkcs8 keys.

Usage: dotnetrsa gen [options]

Options:
  -h|--help           Show help information
  -f|--format         Required.Gen keys's format.The value must be xml, pkcs1 ,pkcs8.
  -s|--size <int>     Key Size.
  --pem               Pem Format. true of false.
  -o|--output <path>  File output path.If you do not specify it will be output in the current directory.
````

The DotnetRSA build and conversion key uses the open source project RSAUtil: https://github.com/stulzq/RSAUtil
