using System.IO;
using System;


namespace DirectorySize
{
    public class FileSystem
    {
        public readonly string Name;
        public readonly string Path;
        public readonly long Count;
        public readonly long Size;
        public readonly bool IsDir;

        public FileSystem(string path, string search = "*.*")
        {
            this.Path = path;
            if (Directory.Exists(path))
            {
                this.Name = new DirectoryInfo(path).Name;
                this.IsDir = true;
                var calculation = CalculateSize(new DirectoryInfo(path), search);
                this.Size = calculation[0];
                this.Count = calculation[1];
            }

            else if (File.Exists(path))
            {
                this.Name = new FileInfo(path).Name;
                this.Size = new FileInfo(path).Length;
                this.Count = 1;
                this.IsDir = false;
            }

            else
            {
                this.Name = "";
                this.Size = 0;
                this.IsDir = false;
            }
        }

        public static long[] CalculateSize(DirectoryInfo dir, string search = "*.*")
        {
            long[] result = new long[2];
            long size = 0;
            long count = 0;
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
                    size += file.Length;
                    count++;
                }

                subDirs = dir.GetDirectories(search, SearchOption.TopDirectoryOnly);
                foreach (var directory in subDirs)
                {
                    size += CalculateSize(directory, search)[0];
                    count += CalculateSize(directory, search)[1];
                }
            }
            result[0] = size;
            result[1] = count;
            return result;
        }
    }
}
