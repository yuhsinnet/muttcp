namespace OneMore
{

    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.TGIP_box = new System.Windows.Forms.TextBox();
            this.Bind_box = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TGPORT_box = new System.Windows.Forms.TextBox();
            this.DLBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // TGIP_box
            // 
            this.TGIP_box.Location = new System.Drawing.Point(12, 12);
            this.TGIP_box.Name = "TGIP_box";
            this.TGIP_box.Size = new System.Drawing.Size(122, 22);
            this.TGIP_box.TabIndex = 0;
            this.TGIP_box.Text = "192.168.0.79";
            // 
            // Bind_box
            // 
            this.Bind_box.Location = new System.Drawing.Point(230, 12);
            this.Bind_box.Name = "Bind_box";
            this.Bind_box.Size = new System.Drawing.Size(100, 22);
            this.Bind_box.TabIndex = 1;
            this.Bind_box.Text = "8888";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(199, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "<-->";
            // 
            // TGPORT_box
            // 
            this.TGPORT_box.Location = new System.Drawing.Point(140, 12);
            this.TGPORT_box.Name = "TGPORT_box";
            this.TGPORT_box.Size = new System.Drawing.Size(44, 22);
            this.TGPORT_box.TabIndex = 3;
            this.TGPORT_box.Text = "1589";
            // 
            // DLBox
            // 
            this.DLBox.Location = new System.Drawing.Point(12, 40);
            this.DLBox.Multiline = true;
            this.DLBox.Name = "DLBox";
            this.DLBox.Size = new System.Drawing.Size(318, 398);
            this.DLBox.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.DLBox);
            this.Controls.Add(this.TGPORT_box);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Bind_box);
            this.Controls.Add(this.TGIP_box);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TGIP_box;
        private System.Windows.Forms.TextBox Bind_box;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TGPORT_box;
        private System.Windows.Forms.TextBox DLBox;
    }
}

