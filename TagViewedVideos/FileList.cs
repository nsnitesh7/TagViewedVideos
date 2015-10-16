using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TagViewedVideos
{
    public class FileList
    {
        private string historyFilePath;
        public FileList()
        {
            Files = new ObservableCollection<FileInfo>();
            NotViewedFiles = new ObservableCollection<FileInfo>();
        }

        public ObservableCollection<FileInfo> Files { get; private set; }

        public ObservableCollection<FileInfo> NotViewedFiles { get; private set; }

        public string Path { get; set; }


        private HashSet<string> ReadHistoryFile()
        {

            var list = new HashSet<string>();

            using (StreamReader sr = new StreamReader(historyFilePath))
            {
                while (sr.Peek() >= 0)
                {
                    list.Add(sr.ReadLine());
                }
            }
            return list;
        }

        public void RefreshFiles()
        {
            try
            {
                Files.Clear();
                NotViewedFiles.Clear();

                historyFilePath = Path + @"\ViewedHistory.txt";

                HashSet<string> viewedHistoryFilesList = new HashSet<string>();

                if (File.Exists(historyFilePath))
                {
                    viewedHistoryFilesList = ReadHistoryFile();
                }

                foreach (FileInfo file in new DirectoryInfo(Path).GetFiles())
                {
                    if (string.Equals(file.Name, "ViewedHistory.txt")) { continue; }

                    if (viewedHistoryFilesList.Contains(file.Name))
                    {
                        Files.Add(file);
                    }
                    else
                    {
                        NotViewedFiles.Add(file);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Unable to read folder", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        internal void UpdateHistoryFile()
        {
            if (!File.Exists(historyFilePath))
            {
                File.CreateText(historyFilePath).Close();
                //File.SetAttributes(historyFilePath, FileAttributes.Normal | FileAttributes.Hidden);
            }

            using (StreamWriter sw = new StreamWriter(historyFilePath))
            {
                foreach (FileInfo f in Files)
                {
                    sw.WriteLine(f.Name);
                }
            }
        }
    }
}
