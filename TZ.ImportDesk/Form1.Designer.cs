﻿namespace TZ.ImportDesk
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.opDiaglog = new System.Windows.Forms.OpenFileDialog();
            this.lblTime = new System.Windows.Forms.Label();
            this.lbFile = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // opDiaglog
            // 
            this.opDiaglog.FileName = "opDiaglog";
            this.opDiaglog.FileOk += new System.ComponentModel.CancelEventHandler(this.opDiaglog_FileOk);
            // 
            // lblTime
            // 
            this.lblTime.Location = new System.Drawing.Point(34, 32);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(437, 27);
            this.lblTime.TabIndex = 2;
            // 
            // lbFile
            // 
            this.lbFile.Location = new System.Drawing.Point(34, 191);
            this.lbFile.Name = "lbFile";
            this.lbFile.Size = new System.Drawing.Size(437, 44);
            this.lbFile.TabIndex = 4;
            this.lbFile.Click += new System.EventHandler(this.lbFile_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(34, 625);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(725, 44);
            this.lblStatus.TabIndex = 5;
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(188, 287);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(428, 61);
            this.button4.TabIndex = 8;
            this.button4.Text = "Setup Import Schema";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(65, 120);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(665, 22);
            this.textBox1.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 696);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lbFile);
            this.Controls.Add(this.lblTime);
            this.Name = "Form1";
            this.Text = "Import Data";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog opDiaglog;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lbFile;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox textBox1;
    }
}

