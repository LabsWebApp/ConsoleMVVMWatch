using System;
using System.Runtime.InteropServices;
using ConsoleExtender.ConsoleInfos;

namespace ConsoleExtender
{
    // (класс взят и доработан с сайта:
    // https://coderoad.ru/6554536/%D0%BC%D0%BE%D0%B6%D0%BD%D0%BE-%D0%BB%D0%B8-%D0%BF%D0%BE%D0%BB%D1%83%D1%87%D0%B8%D1%82%D1%8C-%D1%83%D1%81%D1%82%D0%B0%D0%BD%D0%BE%D0%B2%D0%B8%D1%82%D1%8C-%D0%BA%D0%BE%D0%BD%D1%81%D0%BE%D0%BB%D1%8C%D0%BD%D1%8B%D0%B9-%D1%80%D0%B0%D0%B7%D0%BC%D0%B5%D1%80-%D1%88%D1%80%D0%B8%D1%84%D1%82%D0%B0-%D0%B2-c-net)
    /// <summary>
    /// Подключение к Windows библиотеки "kernel32.dll".
    /// Позволяет у текущей консоли
    ///     - установить доступные шрифты,
    ///     - поменять размер шрифта.
    /// </summary>
    public static class ConsoleHelper
    {
        private const int FixedWidthTrueType = 54;
        private const int StandardOutputHandle = -11;

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int nStdHandle);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool SetCurrentConsoleFontEx(IntPtr hConsoleOutput, bool MaximumWindow, ref FontInfo ConsoleCurrentFontEx);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool GetCurrentConsoleFontEx(IntPtr hConsoleOutput, bool MaximumWindow, ref FontInfo ConsoleCurrentFontEx);


        private static readonly IntPtr ConsoleOutputHandle = GetStdHandle(StandardOutputHandle);

        /// <summary>
        /// Пытается изменить размер, свойство "FontWeight", тип шрифта
        /// </summary>
        /// <param name="font">имя типа шрифта</param>
        /// <param name="fontSize">размер шрифта</param>
        /// <param name="fontWight">толщина шрифта</param>
        /// <returns>три состояния консоли в виде массива (изначальное, желаемое, фактически изменённое)</returns>
        public static FontInfo[] SetCurrentFont(string font, short fontSize = 0, FontWeight fontWight = FontWeight.Normal)
        {
            //Console.WriteLine("Set Current Font: " + font);
            FontInfo before = new FontInfo
            {
                cbSize = Marshal.SizeOf<FontInfo>()
            };

            if (GetCurrentConsoleFontEx(ConsoleOutputHandle, false, ref before))
            {

                FontInfo set = new FontInfo
                {
                    cbSize = Marshal.SizeOf<FontInfo>(),
                    FontIndex = 0,
                    FontFamily = FixedWidthTrueType,
                    FontName = font,
                    FontWeight = (int)fontWight,
                    FontSize = fontSize > 0 ? fontSize : before.FontSize
                };

                // Get some settings from current font.
                if (!SetCurrentConsoleFontEx(ConsoleOutputHandle, false, ref set))
                {
                    var ex = Marshal.GetLastWin32Error();
                    Console.WriteLine("Set error " + ex);
                    throw new System.ComponentModel.Win32Exception(ex);
                }

                FontInfo after = new FontInfo
                {
                    cbSize = Marshal.SizeOf<FontInfo>()
                };
                GetCurrentConsoleFontEx(ConsoleOutputHandle, false, ref after);

                return new[] { before, set, after };
            }

            var er = Marshal.GetLastWin32Error();
            Console.WriteLine("Get error " + er);
            throw new System.ComponentModel.Win32Exception(er);
        }
    }
}