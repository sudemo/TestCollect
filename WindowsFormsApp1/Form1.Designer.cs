
namespace SixAsixesAnalyse
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
            this.RunMtth = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // RunMtth
            // 
            this.RunMtth.Location = new System.Drawing.Point(570, 136);
            this.RunMtth.Name = "RunMtth";
            this.RunMtth.Size = new System.Drawing.Size(149, 131);
            this.RunMtth.TabIndex = 0;
            this.RunMtth.Text = "run";
            this.RunMtth.UseVisualStyleBackColor = true;
            this.RunMtth.Click += new System.EventHandler(this.RunMtth_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.RunMtth);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button RunMtth;
    }
}

