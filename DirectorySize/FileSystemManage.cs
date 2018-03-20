using System.IO;
using System.Collections.Generic;

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
            foreach (var directory in list)
            {
                result.Add(new FileSystem(directory, search));
            }
            return result;
        }

        public static List<FileSystem> SortBySize(List<FileSystem> list, bool desc = true)
        {
            var buffer = new List<FileSystem>(list);
            var result = new List<FileSystem>();
            if (desc)
            {
                while (result.Count < list.Count)
                {
                    double max = -1;
                    for (var i = 0; i < buffer.Count; i++)
                    {
                        if (buffer[i].Size > max)
                        {
                            max = buffer[i].Size;
                        }
                    }
                    var index = 0;
                    for (var i = 0; i < buffer.Count; i++)
                    {
                        if (buffer[i].Size == max)
                        {
                            index = i;
                            break;
                        }
                    }
                    result.Add(buffer[index]);
                    buffer.RemoveAt(index);
                }
            }

            else
            {
                while (result.Count < list.Count)
                {
                    double min = buffer[0].Size;
                    for (var i = 0; i < buffer.Count; i++)
                    {
                        if (buffer[i].Size < min)
                        {
                            min = buffer[i].Size;
                        }
                    }
                    var index = 0;
                    for (var i = 0; i < buffer.Count; i++)
                    {
                        if (buffer[i].Size == min)
                        {
                            index = i;
                            break;
                        }
                    }
                    result.Add(buffer[index]);
                    buffer.RemoveAt(index);
                }
            }
            return result;
        }

        public static List<FileSystem> ListAllItems(string path, bool desc = true, bool unsort = false)
        {
            List<FileSystem> result = new List<FileSystem>();
            if (unsort)
            {
                List<FileSystem> buffer = new List<FileSystem>();
                buffer.AddRange(ListSubDirectories(path));
                buffer.AddRange(ListCurrentFiles(path));
                result = SortBySize(buffer, desc);
            }
            else
            {
                result = SortBySize(ListSubDirectories(path));
                result.AddRange(SortBySize(ListCurrentFiles(path)));
            }
            return result;
        }
    }
}