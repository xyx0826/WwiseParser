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
                Output("WwiseParser - Wwise BNK inspector for Wwise 2016.1");
                Output("Usage: WwiseParser.exe {path-to-bnk-file} [params]");
                Output("Parameters:");
                Output("\t--help (-h) (-?):\tprint this help message");
                Output("\t--inspector (-i):\tdump HIRC objects, including unparsed blobs, separately for inspection");
                //Output("\t--diff (-d):\tprint a message if output files have changed, and back up the old version");
                Output("\t--no-stmg (-ns):\tskip STMG section");
                Output("\t--no-hirc (-nh):\tskip HIRC section");
                Output("build 20200419. partial EventAction support");
                return;
            }

            // Set switches and go
            var parser = new SoundBankParser(args[0],
                args.HasSwitch("no-stmg", "ns"),
                args.HasSwitch("no-hirc", "nh"),
                args.HasSwitch("inspector", "i"));
            parser.Parse();
        }

        static void Output(string value)
        {
            //if (Debugger.IsAttached)
            //{
            //    Debug.WriteLine(value);
            //}
            //else
            //{
                Console.WriteLine(value);
            //}
        }
    }
}
