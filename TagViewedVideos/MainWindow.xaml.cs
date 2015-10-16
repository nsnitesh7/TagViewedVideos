using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using WinForms = System.Windows.Forms;
using System.IO;
using System.Collections;

namespace TagViewedVideos
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new FileList();
        }

        private void pickFolderButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new WinForms.FolderBrowserDialog();
            WinForms.DialogResult result = dialog.ShowDialog();
            folderLocationTextBox.Text = dialog.SelectedPath;

            var dc = DataContext as FileList;
            dc.Path = folderLocationTextBox.Text;
            dc.RefreshFiles();
        }

        private void viewedClickHandler(object sender, RoutedEventArgs e)
        {
            var dc = DataContext as FileList;
            var selectedFiles = (notViewedListView.SelectedItems).Cast<FileInfo>().ToList();
            foreach (FileInfo f in selectedFiles)
            {
                dc.NotViewedFiles.Remove(f);
                dc.Files.Add(f);
            }
            dc.UpdateHistoryFile();
        }

        private void notViewedClickHandler(object sender, RoutedEventArgs e)
        {
            var dc = DataContext as FileList;
            var selectedFiles = (viewedListView.SelectedItems).Cast<FileInfo>().ToList();
            foreach (FileInfo f in selectedFiles)
            {
                dc.Files.Remove(f);
                dc.NotViewedFiles.Add(f);
            }
            dc.UpdateHistoryFile();
        }
    }
}
