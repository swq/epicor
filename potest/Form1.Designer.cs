namespace potest
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lb_log = new System.Windows.Forms.ListBox();
            this.btn_ponum = new System.Windows.Forms.Button();
            this.txt_ponum = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lb_log
            // 
            this.lb_log.FormattingEnabled = true;
            this.lb_log.ItemHeight = 12;
            this.lb_log.Location = new System.Drawing.Point(12, 50);
            this.lb_log.Name = "lb_log";
            this.lb_log.Size = new System.Drawing.Size(386, 424);
            this.lb_log.TabIndex = 0;
            // 
            // btn_ponum
            // 
            this.btn_ponum.Location = new System.Drawing.Point(150, 12);
            this.btn_ponum.Name = "btn_ponum";
            this.btn_ponum.Size = new System.Drawing.Size(97, 32);
            this.btn_ponum.TabIndex = 1;
            this.btn_ponum.Text = "button1";
            this.btn_ponum.UseVisualStyleBackColor = true;
            this.btn_ponum.Click += new System.EventHandler(this.btn_ponum_Click);
            // 
            // txt_ponum
            // 
            this.txt_ponum.Location = new System.Drawing.Point(12, 19);
            this.txt_ponum.Name = "txt_ponum";
            this.txt_ponum.Size = new System.Drawing.Size(130, 21);
            this.txt_ponum.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(266, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 31);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(365, 20);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(79, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(858, 484);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txt_ponum);
            this.Controls.Add(this.btn_ponum);
            this.Controls.Add(this.lb_log);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lb_log;
        private System.Windows.Forms.Button btn_ponum;
        private System.Windows.Forms.TextBox txt_ponum;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

