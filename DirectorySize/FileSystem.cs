using System.IO;
using System;

namespace DirectorySize
{
    public class FileSystem
    {
        public readonly string Name;
        public readonly string Path;
        public readonly long Size;
        public readonly bool IsDir;

        public FileSystem(string path, string search = "*.*")
        {            
            this.Path = path;
            if (Directory.Exists(path))
            {
                this.Name = new DirectoryInfo(path).Name;
                this.IsDir = true;                
                this.Size = CalculateSize(new DirectoryInfo(path), search);
            }

            else if (File.Exists(path))
            {
                this.Name = new FileInfo(path).Name;
                this.Size = new FileInfo(path).Length;
                this.IsDir = false;
            }

            else
            {
                this.Name = "";
                this.Size = 0;
                this.IsDir = false;
            }
        }

        public static long CalculateSize(DirectoryInfo dir, string search = "*.*")
        {
            long result = 0;
            FileInfo[] files = null;
            DirectoryInfo[] subDirs = null;
            try
            {
                files = dir.GetFiles(search, SearchOption.TopDirectoryOnly);
            }

            catch (UnauthorizedAccessException)
            {
                //catch unauthorized exception
            }

            catch (PathTooLongException)
            {
                //catch pathtoolong exception
            }

            if (files != null)
            {
                foreach (var file in files)
                {
                    result += file.Length;
                }

                subDirs = dir.GetDirectories(search, SearchOption.TopDirectoryOnly);
                foreach (var directory in subDirs)
                {
                    result += CalculateSize(directory, search);
                }
            }
            return result;
        }
    }
}
