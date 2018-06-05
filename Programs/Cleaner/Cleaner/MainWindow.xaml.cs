using System;
using System.Collections.Generic;
using System.Linq;
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
using System.IO;
using System.Security.Principal;

namespace Cleaner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TempFiles_Click(object sender, RoutedEventArgs e)
        {
            //Grab username for the Path
            string user = Environment.UserName;
            //Set the path of the Temp files
            string path = "C:\\Users\\" + user + "\\AppData\\Local\\Temp";
            //Grab all files in Directory and insert into array
            string[] files = Directory.GetFiles(path);
            //Grab all folders in Directory and insert into array
            string[] directorys = Directory.GetDirectories(path);
            //Grab the directoryInfo and then assign the infomation to an array
            DirectoryInfo di = new DirectoryInfo(path);
            FileInfo[] fileArr = di.GetFiles();
            //Declares the variables for the size of the files and the size of files deleted
            int possibleDeleted = 0;
            int amountDeleted = 0;
            //Declares the size of a MB for later use
            decimal MBSize = 1024;
            //Used to do math to figure out the final sizes
            decimal possibleDeletedMB = 0;
            decimal amountDeletedMB = 0;
            decimal amountDeletedFinal = 0;
            decimal possibleDeletedFinal = 0;
            //foreach file do function
            foreach (FileInfo f in fileArr)
            {
                //Grab the file's .Length (file size)
                string filesize = String.Format("{0}", f.Length);
                //Add each file size to the final amount that can be deleted
                possibleDeleted = possibleDeleted + Int32.Parse(filesize);
            }
            //Checks that the files size are not greater then 1024 (1 MB)
            if (amountDeleted >= MBSize)
            {
                //If true then Divide the possibleDeleted by 1024
                possibleDeletedMB = possibleDeleted / MBSize;
                //Round the value returned to 2 decimal places
                possibleDeletedFinal = Math.Round(amountDeletedMB, 2);
                //Print the total possible to delete
                lstBox.Items.Add("cleaner has cleaned " + possibleDeletedFinal + " MB");
            }
            else
            {
                //else just print the amount in KB
                lstBox.Items.Add("Possible Amount To Delete: " + possibleDeleted + " KB");
            }
            //foreach file do function
            foreach (string file in files)
            {
                //Try and delete the File
                try
                {
                    File.Delete(file);
                }
                //If failed catch and print "Failed to delete" and the file path
                catch
                {
                    lstBox.Items.Add("Failed to delete '" + file + "' due to file being open");
                }
            }
            //For each file do function
            foreach (string directory in directorys)
            {
                //Try to delete all folders and files inside
                try
                {
                    Directory.Delete(directory, true);
                }
                //If failed catch and print "Failed to delete" and the folder path
                catch
                {
                    lstBox.Items.Add("Failed to delete '" + directory + "' due to folder/files being open");
                }
            }
            //Assign all the directory files after deletion to an array
            FileInfo[] fileArrAfter = di.GetFiles();
            //foreach file do function
            foreach (FileInfo f in fileArrAfter)
            {
                //Grab the file's .Length (file size)
                string fileSizeAfter = String.Format("{0}", f.Length);
                //Take the total files sizes and take away from the possible amount to get amount deleted
                amountDeleted = possibleDeleted - Int32.Parse(fileSizeAfter);

            }
            //Checks that the files size for amount deleted are not greater then 1024 (1 MB)
            if (amountDeleted >= MBSize)
            {
                //If true then Divide the amountDeleted by 1024
                amountDeletedMB = amountDeleted / MBSize;
                //Round the value returned to 2 decimal places
                amountDeletedFinal = Math.Round(amountDeletedMB, 2);
#                //Print the total deleted in MB
                lstBox.Items.Add("cleaner has cleaned " + amountDeletedFinal + " MB");
            }
            else
            {
                //else print the total deleted in KB
                lstBox.Items.Add("cleaner has cleaned " + amountDeleted + " KB");
            }

        }

    }
}
