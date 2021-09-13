using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortTextureForms
{
    class FileTool
    {
        public static void CopyFile(string selectedPath, FileInfo file)
        {
            if (!Directory.Exists(selectedPath))
            {
                Directory.CreateDirectory(selectedPath);
            }
            string path = Path.Combine(selectedPath, file.Name);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            file.CopyTo(path);
        }

        public static void MoveFile(string selectedPath, FileInfo file)
        {
            if (!Directory.Exists(selectedPath))
            {
                Directory.CreateDirectory(selectedPath);
            }
            string path = Path.Combine(selectedPath, file.Name);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            file.MoveTo(path);
        }
    }
}
