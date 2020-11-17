using System;
using System.Collections.Generic;

namespace Snapshot
{
    public interface IView
    {
        event Action Started;
        void OnTaskFinished(List<ProcessInfo> processes);
    }
}