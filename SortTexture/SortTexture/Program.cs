using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SortTexture
{
    public class BrowserDialog
    {
        public void OpenFolderBrowserDialog()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择图片所在文件夹";
            dialog.ShowNewFolderButton = false;
            dialog.SelectedPath = Environment.CurrentDirectory;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show("文件夹路径不能为空", "提示");
                    return;
                }
                DirectoryInfo root = new DirectoryInfo(dialog.SelectedPath);
                FileInfo[] files = root.GetFiles("*png");
                string strLog = "";
                List<string> listDir = new List<string>();
                for (int i = 0; i < files.Length; i++)
                {
                    string ImgExtention = Path.GetExtension(files[i].Name);
                    if (ImgExtention == ".png" || ImgExtention == ".PNG")
                    {
                        if (files[i].Name.StartsWith("common"))
                        {
                            string selectedPath = dialog.SelectedPath + "/Common";
                            CopyFile(selectedPath, files[i]);
                            if (!listDir.Contains("Common"))
                            {
                                listDir.Add("Common");
                            }
                        }
                        else if (files[i].Name.StartsWith("head"))
                        {
                            string selectedPath = dialog.SelectedPath + "/Head";
                            CopyFile(selectedPath, files[i]);
                            if (!listDir.Contains("Head"))
                            {
                                listDir.Add("Head");
                            }
                        }
                        else if (files[i].Name.StartsWith("icon"))
                        {
                            string selectedPath = dialog.SelectedPath + "/Icon";
                            CopyFile(selectedPath, files[i]);
                            if (!listDir.Contains("Icon"))
                            {
                                listDir.Add("Icon");
                            }
                        }
                        else 
                        {
                            Size size = ImageHeader.GetDimensions(files[i].FullName);
                            if (size.Width > 500f && size.Height > 500f)
                            {
                                string selectedPath = dialog.SelectedPath + "/Texture";
                                CopyFile(selectedPath, files[i]);
                            }
                            else 
                            {
                                string selectedPath = dialog.SelectedPath + "/Sprite";
                                CopyFile(selectedPath, files[i]);
                            }
                        }
                    }
                }
                for (int i = 0; i < listDir.Count; i++)
                {
                    string name = listDir[i];
                    string dirPath = dialog.SelectedPath + "/" + name;
                    DirectoryInfo subRoot = new DirectoryInfo(dirPath);
                    FileInfo[] subFiles = subRoot.GetFiles("*png");
                    for (int j = 0; j < subFiles.Length; j++)
                    {
                        Size size = ImageHeader.GetDimensions(subFiles[j].FullName);
                        if (size.Width > 512f && size.Height > 512f)
                        {
                            string selectedPath = dirPath + "/Texture";
                            MoveFile(selectedPath, subFiles[j]);
                        }
                        else
                        {
                            string selectedPath = dirPath + "/Sprite";
                            MoveFile(selectedPath, subFiles[j]);
                        }
                    }
                    //strLog = subFiles.Length.ToString();
                    //WriteLog(dirPath, name, strLog);
                }
            }
        }

        public static void CopyFile(string selectedPath,FileInfo file) 
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

        public static void WriteLog(string sFilePath, string sFileName, string strLog)
        {
            sFileName = sFileName + ".log";
            sFileName = sFilePath + "\\" + sFileName; //文件的绝对路径
            if (!Directory.Exists(sFilePath))//验证路径是否存在
            {
                Directory.CreateDirectory(sFilePath);
                //不存在则创建
            }
            FileStream fs;
            StreamWriter sw;
            if (File.Exists(sFileName))
            //验证文件是否存在，有则追加，无则创建
            {
                fs = new FileStream(sFileName, FileMode.Append, FileAccess.Write);
            }
            else
            {
                fs = new FileStream(sFileName, FileMode.Create, FileAccess.Write);
            }
            sw = new StreamWriter(fs);
            sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + "   ---   \n" + strLog);
            sw.Close();
            fs.Close();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("By ");

            BrowserDialog browserDialog = new BrowserDialog();
            Thread t = new Thread(new ThreadStart(browserDialog.OpenFolderBrowserDialog));
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
        }
    }
}
