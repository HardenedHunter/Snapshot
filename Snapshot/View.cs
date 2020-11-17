using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

// ReSharper disable LocalizableElement

namespace Snapshot
{
    public partial class View : Form, IView
    {
        public event Action Started;

        public View()
        {
            InitializeComponent();
        }

        private void buttonTask_Click(object sender, EventArgs e)
        {
            buttonTask.Enabled = false;
            Started?.Invoke();
        }

        public void OnTaskFinished(List<ProcessInfo> processes)
        {
            treeViewProcesses.Nodes.Clear();
            processes = processes.OrderByDescending(processInfo => processInfo.TotalSize).ToList();
            foreach (var processInfo in processes)
            {
                if (processInfo.TotalSize > 0)
                {
                    var moduleNodes = new TreeNode[processInfo.Modules.Count];
                    for (int i = 0; i < processInfo.Modules.Count; i++)
                    {
                        moduleNodes[i] = new TreeNode($"{processInfo.Modules[i].Name} — {processInfo.Modules[i].Size} байт");
                    }

                    treeViewProcesses.Nodes.Add(new TreeNode($"{processInfo.Name} — {processInfo.TotalSize} байт",
                        moduleNodes));
                }
            }

            treeViewProcesses.CollapseAll();
            buttonTask.Enabled = true;
        }

    }
}