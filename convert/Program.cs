using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace convert
{
    class Program
    {
        static void Main(string[] args)
        {
            string filetype;
            if (!Directory.Exists(@"./ansi/"))
            {
                Directory.CreateDirectory(@"./ansi/");
            }
            if (args.Length == 0)
            {
                filetype = @"./*.att";
                convert(filetype);

            }
            else {
                for (int i = 0; i < args.Length; i++)
                {
                    filetype = args[i];
                    string path = System.IO.Path.GetDirectoryName(args[i]);
                    if (path == "")
                    {
                        convert(filetype);
                    }
                    else {
                        if (!Directory.Exists(@"./ansi/" + path))
                        {
                            Directory.CreateDirectory(@"./ansi/" + path);
                        }
                        convert(filetype);
                        if (Directory.GetDirectories(@"./ansi/").Length > 0) 
                        {
                            foreach (string var in Directory.GetDirectories(@"./ansi/"))
                            {
                                Directory.Delete(var, true);
                            }
                        }
                        if (Directory.GetFiles(@"./ansi/").Length > 0)
                        {
                            foreach (string var in Directory.GetFiles(@"./ansi/"))
                            {
                                File.Delete(var);
                            }
                        }
                    }

                 }
            }
            Directory.Delete(@"./ansi/");
        }
        static void convert(string filestr)
        {
            var array = Directory.GetFiles(@"./", filestr);
            foreach (var f in array)
            {
                StreamReader sr = new StreamReader(f);
                StreamWriter sw = new StreamWriter(@"./ansi/" + f, false, Encoding.ASCII);
                sw.WriteLine(sr.ReadToEnd());
                sw.Close();
                sr.Close();
                File.Delete(@"./" + f);
                File.Copy(@"ansi/" + f, @"./" + f,true);
                File.Delete(@"./ansi/" + f);
            }
        }
    }
}
