using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTC.Model
{
    class ManageDirectories
    {
        List<string> directories = new List<string>();

        public void Clear_Directories()
        {
            directories.Clear();
        }
        public void Add_Directory(string p)
        {
            directories.Add(p);
        }
        public void Delete_Last_Directory()
        {
            if (directories.Any()) //prevent IndexOutOfRangeException for empty list
            {
                directories.RemoveAt(directories.Count - 1);
            }
        }

        public string Get_Last_Directory()
        {
            string[] paths = directories.ToArray();
            string last = "";
            foreach (string dir in paths)
                last = dir;
            
            return last;
        }
        public bool Go_back(string p)
        {
            if (p == "...")
                return true;
            return false;
        }
        public bool Only_Drive()
        {
            int n = directories.Count();
            if (n == 1)
                return true;
            return false;
        }
        public string Full_Name(string name)
        {
            string newname = Get_Last_Directory();
            newname = newname + name.Substring(name.Length - (name.Length - 3));
            return newname;
        }

    }
}
