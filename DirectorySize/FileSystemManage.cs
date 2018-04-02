using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace DirectorySize
{

    public static class FileSystemManage
    {
        public static List<FileSystem> ListSubDirectories(string path, string search = "*.*")
        {
            List<FileSystem> result = new List<FileSystem>();
            var list = Directory.GetDirectories(path);
            foreach (var directory in list)
            {
                result.Add(new FileSystem(directory, search));
            }

            return result;
        }

        public static List<FileSystem> ListCurrentFiles(string path, string search = "*.*")
        {
            List<FileSystem> result = new List<FileSystem>();
            var list = Directory.GetFiles(path, search, SearchOption.TopDirectoryOnly);
            foreach (var file in list)
            {
                result.Add(new FileSystem(file, search));
            }
            return result;
        }

        public static List<FileSystem> SortBySize(List<FileSystem> list, bool desc = true)
        {
            List<FileSystem> result = new List<FileSystem>();
            if (desc)
            {
                result = list.OrderByDescending(p => p.Size).ToList();
            }
            else
            {
                result = list.OrderBy(p => p.Size).ToList();
            }
            return result;
        }

        public static List<FileSystem> SortByCount(List<FileSystem> list, bool desc = true)
        {
            List<FileSystem> result = new List<FileSystem>();
            if (desc)
            {
                result = list.OrderByDescending(p => p.Count).ToList();
            }
            else
            {
                result = list.OrderBy(p => p.Count).ToList();
            }
            return result;
        }

        public static List<FileSystem> ListAllItems(string path, bool size = true, bool desc = true, bool unsort = false)
        {
            List<FileSystem> result = new List<FileSystem>();
            if (size)
            {
                if (unsort)
                {
                    List<FileSystem> buffer = new List<FileSystem>();
                    buffer.AddRange(ListSubDirectories(path));
                    buffer.AddRange(ListCurrentFiles(path));
                    result = SortBySize(buffer, desc);
                }
                else
                {
                    result = SortBySize(ListSubDirectories(path), desc);
                    result.AddRange(SortBySize(ListCurrentFiles(path), desc));
                }
            }
            else
            {
                if (unsort)
                {
                    List<FileSystem> buffer = new List<FileSystem>();
                    buffer.AddRange(ListSubDirectories(path));
                    buffer.AddRange(ListCurrentFiles(path));
                    result = SortByCount(buffer, desc);
                }
                else
                {
                    result = SortByCount(ListSubDirectories(path), desc);
                    result.AddRange(ListCurrentFiles(path));
                }
            }
            
            return result;
        }
    }
}