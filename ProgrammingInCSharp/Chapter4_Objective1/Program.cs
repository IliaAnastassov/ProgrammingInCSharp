namespace Chapter4_Objective1
{
    using System;
    using System.IO;
    using System.Security.AccessControl;
    using System.Text;

    public class Program
    {
        public static void Main(string[] args)
        {

        }

        private static void ReadFromFileUsingStreamReader()
        {
            var path = @"D:\Temp\file.dat";

            using (var reader = File.OpenText(path))
            {
                Console.WriteLine(reader.ReadLine());
            }
        }

        private static void ReadFromFileUsingFileStream()
        {
            var path = @"D:\Temp\file.dat";

            using (var stream = File.OpenRead(path))
            {
                var data = new byte[stream.Length];

                for (int i = 0; i < stream.Length; i++)
                {
                    data[i] = (byte)stream.ReadByte();
                }

                Console.WriteLine(Encoding.UTF8.GetString(data));
            }
        }

        private static void WriteTextToFileUsingStreamWriter()
        {
            var path = @"D:\Temp\file.dat";

            using (var stream = File.CreateText(path))
            {
                var value = "Sample text";
                stream.Write(value);
            }
        }

        private static void WriteDataUsingFileStream()
        {
            var path = @"D:\Temp\file.dat";

            using (var stream = File.Create(path))
            {
                var value = "Sample data";
                var data = Encoding.UTF8.GetBytes(value);
                stream.Write(data, 0, data.Length);
            }
        }

        private static void UseCoolPathMethods()
        {
            var file = Path.GetRandomFileName();
            var path = Path.GetTempPath();
            var fileName = Path.GetTempFileName();

            Console.WriteLine(file);
            Console.WriteLine(path);
            Console.WriteLine(fileName);
        }

        private static void UsePathMethods()
        {
            var path = @"D:\Temp\subdir\file.txt";

            Console.WriteLine(Path.GetDirectoryName(path));
            Console.WriteLine(Path.GetExtension(path));
            Console.WriteLine(Path.GetFileName(path));
            Console.WriteLine(Path.GetPathRoot(path));
        }

        private static void UsePathCombine()
        {
            var folder = @"D:\Temp";
            var file = "text.txt";

            var fullPath = Path.Combine(folder, file);
            Console.WriteLine(fullPath);
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
