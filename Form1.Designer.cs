namespace Dempster_Shafer
{
    partial class Form1
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
            this.EvidList = new System.Windows.Forms.CheckedListBox();
            this.mainButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // EvidList
            // 
            this.EvidList.FormattingEnabled = true;
            this.EvidList.Location = new System.Drawing.Point(12, 39);
            this.EvidList.Name = "EvidList";
            this.EvidList.Size = new System.Drawing.Size(373, 514);
            this.EvidList.TabIndex = 0;
            // 
            // mainButton
            // 
            this.mainButton.Location = new System.Drawing.Point(391, 39);
            this.mainButton.Name = "mainButton";
            this.mainButton.Size = new System.Drawing.Size(145, 156);
            this.mainButton.TabIndex = 1;
            this.mainButton.Text = "Вывести";
            this.mainButton.UseVisualStyleBackColor = true;
            this.mainButton.Click += new System.EventHandler(this.mainButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(544, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(831, 611);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.mainButton);
            this.Controls.Add(this.EvidList);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox EvidList;
        private System.Windows.Forms.Button mainButton;
        private System.Windows.Forms.Label label2;
    }
}

