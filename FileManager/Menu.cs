using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace FileManager
{
    internal class Menu
    {
        public int SelectedIndex;
        private string Label = "NAME                                                  DATE                                                                ";

        private string CurrentPath;

        private string[] Options;
        private string[] PrevOptions;

        private string[] Paths;
        

        public void MainMenu()
        {
            Console.Title = "Console File Manager";
            CurrentPath = contol.DriveMenu();
            Console.CursorVisible = false;
            Paths = contol.GetPaths(CurrentPath);
            Options = contol.GetDirectoryInfo(CurrentPath);
            PrevOptions = Options;
            Run();
        }

        private void Display_Options()
        {
            Console.CursorVisible = false;
            Console.ResetColor();
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(Label);
            Console.ResetColor();

            Console.SetCursorPosition(0, 1);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("CURRENT DIRECTORY: " + CurrentPath);

            Console.ResetColor();

            
            for (int i = 0; i < Options.Length; i++)
            {
                string SelectedOption = Options[i];

                if (i == SelectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else if (i >= Directory.GetDirectories(CurrentPath).Length)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.SetCursorPosition(0, 2 + i);
                Console.WriteLine(SelectedOption);
                Instructons();
            }
            Console.ResetColor();
        }
        public void Run()
        {
            ConsoleKey Key_Pressed;
            do
            {
                new Thread(Display_Options).Start();
                Thread.Sleep(50);
                ConsoleKeyInfo Key_Inf = Console.ReadKey(true);
                Key_Pressed = Key_Inf.Key;

                if (Key_Pressed == ConsoleKey.UpArrow)
                {
                    
                    if (SelectedIndex > 0)
                    {
                        SelectedIndex--;
                    };
                }
                else if (Key_Pressed == ConsoleKey.DownArrow)
                {
                    
                    if (SelectedIndex < Options.Length - 1)
                    {
                        SelectedIndex++;
                    };
                }
                Console.ForegroundColor = ConsoleColor.Black;

                switch (Key_Pressed)
                {
                    case ConsoleKey.Escape:
                        try
                        {
                            PrevOptions = Options;
                            CurrentPath = Path.GetDirectoryName(CurrentPath);
                            Options = contol.GetDirectoryInfo(CurrentPath);
                            MenuClear();
                        }
                        catch (System.ArgumentNullException)
                        {
                            PrevOptions = Options;
                            CurrentPath = contol.DriveMenu();
                            Options = contol.GetDirectoryInfo(CurrentPath);
                            MenuClear();
                        }
                        break;

                    case ConsoleKey.Enter:
                        PrevOptions = Options;
                        try
                        {
                            PrevOptions = Options;
                            Paths = contol.GetPaths(CurrentPath);
                            Options = contol.GetDirectoryInfo(Paths[SelectedIndex]);
                            CurrentPath = Paths[SelectedIndex];
                            SelectedIndex = 0;
                            MenuClear();
                        }
                        catch (System.IO.IOException)
                        {
                            Process.Start(new ProcessStartInfo { FileName = Paths[SelectedIndex], UseShellExecute = true });
                        }
                        catch (System.UnauthorizedAccessException)
                        {
                            contol.AccessException();
                        }
                        break;

                    case ConsoleKey.Delete:
                        contol.DeleteFile(Paths[SelectedIndex]);
                        MenuClear();
                        Options = contol.GetDirectoryInfo(CurrentPath);
                        break;
                    case ConsoleKey.F1:
                        contol.CreateDirectory(CurrentPath);
                        MenuClear();
                        Options = contol.GetDirectoryInfo(CurrentPath);
                        break;
                    case ConsoleKey.F2:
                        SelectedIndex = 0;
                        contol.CreateFile(CurrentPath);
                        MenuClear();
                        Options = contol.GetDirectoryInfo(CurrentPath);
                        break;
                }
            } while (true);
        }
        private void MenuClear()
        {
            for (int i = 0; i < PrevOptions.Length + 10; i++)
            {
                Console.SetCursorPosition(0, 1);
                Console.ForegroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(0, i + 1);
                Console.WriteLine(Label);
                Console.WriteLine(Label);
            }
            Console.ResetColor();
        }

        private void Instructons()
        {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(85, 2);
            Console.WriteLine("│Используйте стрелки для навигации.│");
            Console.SetCursorPosition(85, 3);
            Console.WriteLine("│Используйте стрелки для навигации.│");
            Console.SetCursorPosition(85, 4);
            Console.WriteLine("│Используйте Enter, чтобы открыть папку.│");
            Console.SetCursorPosition(85, 5);
            Console.WriteLine("│Используйте ESC, чтобы открыть папку privios.│");
            Console.SetCursorPosition(85, 6);
            Console.WriteLine("│Используйте DELETE для удаления папки / файла.│");
            Console.SetCursorPosition(85, 7);
            Console.WriteLine("│Используйте F1 для создания папки.  │");
            Console.SetCursorPosition(85, 8);
            Console.WriteLine("│Используйте F2 для создания файлов.│");
        }
    }
}
