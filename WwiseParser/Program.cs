using System;
using System.Linq;

namespace WwiseParser
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0 || args.Contains("--help") || args.Contains("-h") || args.Contains("-?"))
            {
                Output("WwiseParser - Wwise BNK inspector for Wwise 2013 and 2016.1");
                Output("Usage: WwiseParser.exe {path-to-bnk-file} [params]");
                Output("Parameters:");
                Output("\t--help (-h) (-?):\tprint this help message");
                Output("\t--inspector (-i):\tdump HIRC objects, including unparsed blobs, separately for inspection");
                Output("\t--inspector-blobs (-ib):\tdump HIRC objects separately only in blob form");
                //Output("\t--actor (-amh): dump the actor-mixer hierarchy");
                //Output("\t--master (-mmh): dump the master-mixer hierarchy");
                //Output("\t--interactive (-imh): dump the interactive music hierarchy");
                Output("\t--no-stmg (-ns):\tskip STMG (State Manager) chunk, found in Init.bnk");
                Output("\t--no-hirc (-nh):\tskip HIRC (Hierarchy) chunk, found in typical SoundBanks");
                Output("build 20201102. Refactored library, adding Wwise 2013 support");
                return;
            }

            // Set switches and go
            var parser = new SoundBankParser(args[0],
                args.HasSwitch("no-stmg", "ns"),
                args.HasSwitch("no-hirc", "nh"),
                args.HasSwitch("inspector", "i"),
                args.HasSwitch("inspector-blobs", "ib"));
            parser.Parse();
        }

        static void Output(string value)
        {
            Console.WriteLine(value);
        }
    }
}
