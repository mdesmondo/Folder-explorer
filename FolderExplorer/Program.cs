using System;
using System.IO;

namespace PolytechPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите название директории:");
            string searchDirectory = Console.ReadLine();
            if (Directory.Exists(searchDirectory))
            {
                getDir(searchDirectory, 1);
            }
            else
            {
                try
                {
                    string[] drives = Directory.GetLogicalDrives();
                    bool found = false;

                    foreach (string drive in drives)
                    {
                        string foundPath = FindDirectory(drive, searchDirectory);
                        if (!string.IsNullOrEmpty(foundPath))
                        {
                            found = true;
                            Console.WriteLine($"\nНайдена директория: {foundPath}");
                            getDir(foundPath, 1);
                            
                        }
                    }

                    if (!found)
                    {
                        Console.WriteLine("Директория не найдена на компьютере");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Произошла ошибка: {ex.Message}");
                }

                Console.WriteLine("\nНажмите Enter для завершения...");
                Console.ReadKey();
            }
        }
        static string FindDirectory(string startPath, string searchPattern)
        {
            try
            {
                if (Path.GetFileName(startPath).Equals(searchPattern))
                {
                    return startPath;
                }

                foreach (string dir in Directory.GetDirectories(startPath))
                {
                    try
                    {
                        if (Path.GetFileName(dir).Equals(searchPattern))
                        {
                            return dir;
                        }

                        string found = FindDirectory(dir, searchPattern);
                        if (!string.IsNullOrEmpty(found))
                        {
                            return found;
                        }
                    }
                    catch (UnauthorizedAccessException)
                    {
                        continue;
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                return null;
            }
            catch (PathTooLongException)
            {
                return null;
            }

            return null;
        }

        static void getDir(string foldername, int n)
        {
            int nn = 2 * n;
            Console.CursorLeft = nn;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("*");
            Console.ForegroundColor = ConsoleColor.White;
            Console.CursorLeft = nn + 2;
            Console.WriteLine(Path.GetFileName(foldername));

            foreach (string file in Directory.GetFiles(foldername))
            {
                Console.CursorLeft = nn + 1;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("L");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.CursorLeft = nn + 3;
                Console.WriteLine(Path.GetFileName(file));
                Console.CursorLeft = nn - 1;
                Console.ForegroundColor = ConsoleColor.White;
            }

            string[] directories = Directory.GetDirectories(foldername);
            foreach (string directory in directories)
            {
                getDir(directory, n + 1);
            }
        }
    }
}