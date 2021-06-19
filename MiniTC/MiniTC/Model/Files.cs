using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MiniTC.Model
{
    class Files
    {
        public string[] Get_Drives()
        {
            string[] drives = Directory.GetLogicalDrives();
            return drives;
        }
        public string[] Get_Paths(string path, bool d)
        {
            List<string> list = new List<string>();
            if (d == false)
                list.Add("...");
            string[] directories = Directory.GetDirectories(path);
            string[] files = Directory.GetFiles(path);
            foreach (string dir in directories)
            {
                int len = dir.Length - path.Length;
                string name  = dir.Substring(dir.Length - len);
                name = "<D>" + name;
                list.Add(name);
            }

            foreach (string file in files) 
            {
                int len = file.Length - path.Length;
                string name = file.Substring(file.Length - len);
                list.Add(Path.GetFileName(name));
            }

            string[] paths = list.ToArray();
            return paths;
        }

        public void CopyFile(string file, string directory)
        {
            File.Copy(file, directory, true);
        }
    }
}
