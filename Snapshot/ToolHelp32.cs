using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming

namespace Snapshot
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct ProcessEntry32
    {
        public uint dwSize; // Размер записи
        public uint cntUsage; // Счетчик ссылок 
        public uint th32ProcessID; // Идентификационный номер 
        public IntPtr th32DefaultHeapID; // Идентификационный номер основной кучи 
        public uint th32ModuleID; // Идентифицирует модуль связанный с процессом 
        public uint cntThreads; // Кол-во потоков в данном процессе
        public uint th32ParentProcessID; // Идентификатор родительского процесса
        public int pcPriClassBase; // Базовый приоритет 
        public uint dwFlags; // Зарезервировано

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string szExeFile; // Путь к exe файлу или драйверу, связанному с этим процессом
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct ModuleEntry32
    {
        public uint dwSize; // размер записи
        public uint th32ModuleID; // идентификатор модуля
        public uint th32ProcessID; // идентификатор процесса
        public uint GlblcntUsage; // глобальный счетчик ссылок
        public uint ProccntUsage; // счетчик ссылок в контексте процессора
        public IntPtr modBaseAddr; // базовый адресс модюля в памяти
        public uint modBaseSize; // размер модуля памяти
        public IntPtr hModule; // дескриптор модуля

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string szModule; // название модуля

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string szExePath; // полный путь модуля
    }

    public class ToolHelp32
    {
        [Flags]
        public enum SnapshotFlags : uint
        {
            HeapList = 0x00000001,
            Process = 0x00000002,
            Thread = 0x00000004,
            Module = 0x00000008,
            Module32 = 0x00000010,
            Inherit = 0x80000000,
            All = 0x0000001F,
            NoHeaps = 0x40000000
        }

        #region Import from kernel32

        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Auto)]
        static extern IntPtr CreateToolhelp32Snapshot([In] uint dwFlags, [In] uint th32ProcessID);

        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool Process32First([In] IntPtr hSnapshot, ref ProcessEntry32 lppe);

        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool Process32Next([In] IntPtr hSnapshot, ref ProcessEntry32 lppe);

        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool Module32First([In] IntPtr hSnapshot, ref ModuleEntry32 lpme);

        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool Module32Next([In] IntPtr hSnapshot, ref ModuleEntry32 lpme);

        [DllImport("kernel32", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle([In] IntPtr hObject);

        #endregion

        public IEnumerable<ProcessInfo> GetProcesses()
        {
            var snapshot = IntPtr.Zero;
            try
            {
                snapshot = CreateToolhelp32Snapshot((uint)SnapshotFlags.All, 0);
                var process = new ProcessEntry32 {dwSize = (uint) Marshal.SizeOf(typeof(ProcessEntry32))};

                if (Process32First(snapshot, ref process))
                {
                    do
                    {
                        var processInfo = new ProcessInfo { Name = process.szExeFile };
                        foreach (var module in GetModules(snapshot, process.th32ProcessID))
                        {
                            var moduleInfo = new ModuleInfo { Size = module.modBaseSize, Name = module.szModule };
                            processInfo.Modules.Add(moduleInfo);
                            processInfo.TotalSize += moduleInfo.Size;
                        }
                        yield return processInfo;
                    } while (Process32Next(snapshot, ref process));
                }
                else
                {
                    throw new ApplicationException($"System error: {Marshal.GetLastWin32Error()}");
                }
            }
            finally
            {
                CloseHandle(snapshot);
            }
        }

        public IEnumerable<ModuleEntry32> GetModules(IntPtr snapshot, uint th32ProcessID = 0)
        {
//            var snapshot = IntPtr.Zero;
//            try
//            {
                var moduleData = new ModuleEntry32 {dwSize = (uint) Marshal.SizeOf(typeof(ModuleEntry32))};
//                snapshot = CreateToolhelp32Snapshot((uint) SnapshotFlags.All, th32ProcessID);

                if (!Module32First(snapshot, ref moduleData)) yield break;
                do
                {
                    if (moduleData.th32ProcessID == th32ProcessID)
                        yield return moduleData;
                } while (Module32Next(snapshot, ref moduleData));
//            }
//            finally
//            {
//                CloseHandle(snapshot);
//            }
        }
    }
}