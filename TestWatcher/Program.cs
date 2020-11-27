
namespace TestWatcher
{


    class Program
    {


        private static void OnChange(object sender, System.IO.FileSystemEventArgs e)
        {
            System.Console.WriteLine(e.FullPath.ToString() + " is changed!");
        }


        private static void OnRenamed(object sender, System.IO.RenamedEventArgs e)
        {
            System.Console.WriteLine(e.FullPath.ToString() + " was renamed!");
            // System.IO.FileSystemEventArgs changeArguments = new System.IO.FileSystemEventArgs(System.IO.WatcherChangeTypes.Changed, e.FullPath, e.Name);
            // OnChange(sender, changeArguments);
        }


        public static System.IO.FileSystemWatcher RunWatcher(string filePath)
        {
            System.IO.FileSystemWatcher watcher = new System.IO.FileSystemWatcher();
            watcher.Path = System.IO.Path.GetDirectoryName(filePath);
            watcher.Filter = System.IO.Path.GetFileName(filePath);
            // watcher.Filter = "*.txt";
            // watcher.NotifyFilter = System.IO.NotifyFilters.LastAccess | System.IO.NotifyFilters.LastWrite | System.IO.NotifyFilters.FileName;
            watcher.NotifyFilter = System.IO.NotifyFilters.Size
                | System.IO.NotifyFilters.LastWrite
                | System.IO.NotifyFilters.CreationTime
                | System.IO.NotifyFilters.FileName // Needed if changed with Visual Studio ...
                
                // | System.IO.NotifyFilters.LastAccess
                // | System.IO.NotifyFilters.Attributes
                // | System.IO.NotifyFilters.DirectoryName
                // | System.IO.NotifyFilters.Security
                ;

            watcher.Changed += new System.IO.FileSystemEventHandler(OnChange);
            watcher.Created += new System.IO.FileSystemEventHandler(OnChange);
            watcher.Deleted += new System.IO.FileSystemEventHandler(OnChange);
            // watcher.Renamed += new System.IO.RenamedEventHandler(OnRenamed);

            watcher.EnableRaisingEvents = true;

            return watcher;
        } // End Function RunWatcher 


        static void Main(string[] args)
        {
            string filePath = System.IO.Path.GetDirectoryName(typeof(Program).Assembly.Location);
            filePath = System.IO.Path.Combine(filePath, "..","..","..", "myfile.txt");
            filePath = System.IO.Path.GetFullPath(filePath);
            System.Console.WriteLine(filePath);

            using (System.IO.FileSystemWatcher watcher = RunWatcher(filePath))
            {
                System.Console.WriteLine(" --- Press any key to continue --- ");
                System.Console.ReadKey();
            }
            
        }


    }


}
