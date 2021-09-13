using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SortTextureForms
{
    class FolderBrowser
    {
        public static void OpenFolderBrowserDialog()
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
                        bool isInclude = false;
                        foreach (string pattern in Form1.patterns)
                        {
                            string lower = files[i].Name.ToLower();
                            if (lower.StartsWith(pattern.ToLower()))
                            {
                                isInclude = true;
                                string selectedPath = dialog.SelectedPath + "/" + pattern;
                                FileTool.CopyFile(selectedPath, files[i]);
                                if (!listDir.Contains(pattern))
                                {
                                    listDir.Add(pattern);
                                }
                            }
                        }
                        if (!isInclude)
                        {
                            Size size = ImageHeader.GetDimensions(files[i].FullName);
                            if (size.Width >= Form1.imgWidth && size.Height >= Form1.imgHeight)
                            {
                                string selectedPath = dialog.SelectedPath + "/Texture";
                                FileTool.CopyFile(selectedPath, files[i]);
                            }
                            else
                            {
                                string selectedPath = dialog.SelectedPath + "/Sprite";
                                FileTool.CopyFile(selectedPath, files[i]);
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
                        if (size.Width >= Form1.imgWidth && size.Height >= Form1.imgHeight)
                        {
                            string selectedPath = dirPath + "/Texture";
                            FileTool.MoveFile(selectedPath, subFiles[j]);
                        }
                        else
                        {
                            string selectedPath = dirPath + "/Sprite";
                            FileTool.MoveFile(selectedPath, subFiles[j]);
                        }
                    }
                    //strLog = subFiles.Length.ToString();
                    //WriteLog(dirPath, name, strLog);
                }
            }
        }
    }
}
