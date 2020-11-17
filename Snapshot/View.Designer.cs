namespace Snapshot
{
    partial class View
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.treeViewProcesses = new System.Windows.Forms.TreeView();
            this.buttonTask = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // treeViewProcesses
            // 
            this.treeViewProcesses.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.900001F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.treeViewProcesses.Location = new System.Drawing.Point(26, 119);
            this.treeViewProcesses.Name = "treeViewProcesses";
            this.treeViewProcesses.Size = new System.Drawing.Size(729, 1022);
            this.treeViewProcesses.TabIndex = 0;
            // 
            // buttonTask
            // 
            this.buttonTask.Location = new System.Drawing.Point(256, 28);
            this.buttonTask.Name = "buttonTask";
            this.buttonTask.Size = new System.Drawing.Size(282, 64);
            this.buttonTask.TabIndex = 1;
            this.buttonTask.Text = "Сделать снимок";
            this.buttonTask.UseVisualStyleBackColor = true;
            this.buttonTask.Click += new System.EventHandler(this.buttonTask_Click);
            // 
            // View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 1168);
            this.Controls.Add(this.buttonTask);
            this.Controls.Add(this.treeViewProcesses);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "View";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "View";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewProcesses;
        private System.Windows.Forms.Button buttonTask;
    }
}

