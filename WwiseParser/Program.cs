using System;
using System.IO;
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
                Output("\t--actor-mixer-hierarchy (-amh): dump the actor-mixer hierarchy");
                Output("\t--master-mixer-hierarchy (-mmh): dump the master-mixer hierarchy");
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

            // crappy code
            var fileName = Path.GetFileNameWithoutExtension(args[0]);
            var hier = parser.SoundBank.CreateActorMixerHierarchy();
            File.WriteAllText("amh_" + fileName + ".txt", hier.Serialize());
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
