namespace Chapter4_Objective1
{
    using System;
    using System.IO;
    using System.Security.AccessControl;

    public class Program
    {
        public static void Main(string[] args)
        {

        }

        private static void DeleteFileIfExists()
        {
            var path = @"D:\Temp\text.txt";

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            var fileInfo = new FileInfo(path);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }
        }

        private static void ListFilesInDirectory()
        {
            foreach (string file in Directory.GetFiles(@"C:\Windows"))
            {
                Console.WriteLine(file);
            }

            var dirInfo = new DirectoryInfo(@"C:\Windows");
            foreach (FileInfo fileInfo in dirInfo.GetFiles())
            {
                Console.WriteLine(fileInfo.FullName);
            }
        }

        private static void ListDirectories(DirectoryInfo root, string searchPattern, int maxLevel, int currentLevel)
        {
            if (currentLevel >= maxLevel)
            {
                return;
            }

            var indent = new string('-', currentLevel);

            try
            {
                var subDirectories = root.GetDirectories(searchPattern);
                foreach (var subdir in subDirectories)
                {
                    Console.WriteLine($"{indent}{subdir.Name}");
                    ListDirectories(subdir, searchPattern, maxLevel, currentLevel + 1);
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine($"{indent}Can't access: {root.Name}");
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine($"{indent}Can't find: {root.Name}");
            }
        }

        private static void SetAccessControl()
        {
            var dirInfo = new DirectoryInfo(@"C:\Users\Admin\Desktop\TestDir");
            dirInfo.Create();
            var dirSecurity = dirInfo.GetAccessControl();
            dirSecurity.AddAccessRule(
                new FileSystemAccessRule(
                    "everyone",
                    FileSystemRights.ReadAndExecute,
                    AccessControlType.Allow));
            dirInfo.SetAccessControl(dirSecurity);
        }

        private static void ListDriveInfo()
        {
            var drivesInfo = DriveInfo.GetDrives();

            foreach (var driveInfo in drivesInfo)
            {
                Console.WriteLine($"Drive: {driveInfo.Name}");
                Console.WriteLine($"\tFile type: {driveInfo.DriveType}");

                if (driveInfo.IsReady)
                {
                    Console.WriteLine($"\tVolume label: {driveInfo.VolumeLabel}");
                    Console.WriteLine($"\tFile system: {driveInfo.DriveFormat}");
                    Console.WriteLine($"\tAvailable space for current user: {driveInfo.AvailableFreeSpace,15}");
                    Console.WriteLine($"\tTotal available space:            {driveInfo.TotalFreeSpace,15}");
                    Console.WriteLine($"\tTotal size of drive:              {driveInfo.TotalSize,15}");
                }
            }
        }
    }
}
