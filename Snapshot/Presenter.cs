using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Snapshot
{
    public class Presenter
    {
        private readonly IView _view;
        public event Action<List<ProcessInfo>> TaskFinished;

        public Presenter(IView view)
        {
            _view = view;
            _view.Started += Run;
            TaskFinished += _view.OnTaskFinished;
            ContextProvider.GetInstance().Context = SynchronizationContext.Current;
        }

        private void Run()
        {
            var thread = new Thread(Task);
            thread.Start();
        }

        private void Task()
        {
            var tool = new ToolHelp32();
            var processes = tool.GetProcesses().ToList();
            ContextProvider.Send(obj => TaskFinished?.Invoke(processes), null);
        }

//        public void Test()
//        {
//            var processes = new List<ProcessInfo>();
//            foreach (var process in Process.GetProcesses())
//            {
//                try
//                {
//                    var processInfo = new ProcessInfo { Name = process.ProcessName };
//                    foreach (var module in process.Modules)
//                    {
//                        var mod = (ProcessModule)module;
//                        var moduleInfo = new ModuleInfo { Size = (uint)mod.ModuleMemorySize, Name = mod.ModuleName };
//                        processInfo.Modules.Add(moduleInfo);
//                        processInfo.TotalSize += moduleInfo.Size;
//                    }
//
//
//                    processes.Add(processInfo);
//                }
//                catch (Win32Exception)
//                {
//                }
//            }
//            ContextProvider.Send(obj => TaskFinished?.Invoke(processes), null);
//        }

//        private void Task()
//        {
//            var tool = new ToolHelp32();
//            var processes = new List<ProcessInfo>();
//            foreach (var process in tool.GetProcesses())
//            {
//                var processInfo = new ProcessInfo { Name = process.szExeFile };
//                foreach (var module in tool.GetModules(process.th32ProcessID))
//                {
//                    var moduleInfo = new ModuleInfo { Size = module.modBaseSize, Name = module.szModule };
//                    processInfo.Modules.Add(moduleInfo);
//                    processInfo.TotalSize += moduleInfo.Size;
//                }
//
//                processes.Add(processInfo);
//            }
//            ContextProvider.Send(obj => TaskFinished?.Invoke(processes), null);
//        }
    }
}