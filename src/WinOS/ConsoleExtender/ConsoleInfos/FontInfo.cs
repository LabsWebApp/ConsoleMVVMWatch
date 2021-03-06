using System.Runtime.InteropServices;

namespace ConsoleExtender.ConsoleInfos
{
    /// <summary>
    /// Структура связанная со структурой из "kernel32.dll" - FontInfo
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct FontInfo
    {
        internal int cbSize;
        internal int FontIndex;
        internal short FontWidth;
        /// <summary>
        /// Размер шрифта
        /// </summary>
        public short FontSize;
        internal int FontFamily;
        /// <summary>
        /// Толщина шрифта 
        /// </summary>
        public int FontWeight;
        /// <summary>
        /// Имя шрифта
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        //[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.wc, SizeConst = 32)]
        public string FontName;
    }
}