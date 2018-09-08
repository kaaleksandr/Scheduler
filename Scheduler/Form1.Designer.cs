namespace Scheduler
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
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.yearList = new System.Windows.Forms.ListBox();
            this.monthGroup = new System.Windows.Forms.GroupBox();
            this.monthList = new System.Windows.Forms.CheckedListBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.logLabel = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.outDir = new System.Windows.Forms.TextBox();
            this.createButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.monthGroup.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(4, 1, 0, 0);
            this.label1.Size = new System.Drawing.Size(605, 92);
            this.label1.TabIndex = 0;
            this.label1.Text = "Для создания файлов расписаний необходимо:\r\n1) выбрать год;\r\n2) отметить нужные м" +
    "есяцы;\r\n3) указать директорию для файлов расписаний;\r\n4) нажать кнопку \"Создать\"" +
    ".";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.yearList);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(296, 276);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "1) Выберите год:";
            // 
            // yearList
            // 
            this.yearList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.yearList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.yearList.FormattingEnabled = true;
            this.yearList.ItemHeight = 17;
            this.yearList.Location = new System.Drawing.Point(3, 21);
            this.yearList.Name = "yearList";
            this.yearList.Size = new System.Drawing.Size(290, 252);
            this.yearList.TabIndex = 0;
            // 
            // monthGroup
            // 
            this.monthGroup.Controls.Add(this.monthList);
            this.monthGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.monthGroup.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.monthGroup.Location = new System.Drawing.Point(0, 0);
            this.monthGroup.Name = "monthGroup";
            this.monthGroup.Size = new System.Drawing.Size(299, 276);
            this.monthGroup.TabIndex = 3;
            this.monthGroup.TabStop = false;
            this.monthGroup.Text = "2) Выберите месяцы:";
            // 
            // monthList
            // 
            this.monthList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.monthList.CheckOnClick = true;
            this.monthList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.monthList.FormattingEnabled = true;
            this.monthList.Location = new System.Drawing.Point(3, 21);
            this.monthList.Name = "monthList";
            this.monthList.Size = new System.Drawing.Size(293, 252);
            this.monthList.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(611, 553);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.logLabel);
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Controls.Add(this.progressBar);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.createButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 95);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(605, 455);
            this.panel1.TabIndex = 1;
            // 
            // logLabel
            // 
            this.logLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.logLabel.AutoSize = true;
            this.logLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.logLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.logLabel.Location = new System.Drawing.Point(10, 377);
            this.logLabel.Name = "logLabel";
            this.logLabel.Size = new System.Drawing.Size(0, 17);
            this.logLabel.TabIndex = 9;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.monthGroup);
            this.splitContainer1.Size = new System.Drawing.Size(599, 276);
            this.splitContainer1.SplitterDistance = 296;
            this.splitContainer1.TabIndex = 8;
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(9, 349);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(587, 23);
            this.progressBar.Step = 1;
            this.progressBar.TabIndex = 7;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.browseButton);
            this.groupBox3.Controls.Add(this.outDir);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox3.Location = new System.Drawing.Point(9, 285);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(587, 58);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "3) Укажите директорию для хранения файлов расписания:";
            // 
            // browseButton
            // 
            this.browseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.browseButton.Location = new System.Drawing.Point(506, 24);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(75, 23);
            this.browseButton.TabIndex = 6;
            this.browseButton.Text = "Обзор...";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // outDir
            // 
            this.outDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outDir.BackColor = System.Drawing.Color.Gainsboro;
            this.outDir.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.outDir.Location = new System.Drawing.Point(6, 24);
            this.outDir.Name = "outDir";
            this.outDir.ReadOnly = true;
            this.outDir.Size = new System.Drawing.Size(494, 25);
            this.outDir.TabIndex = 5;
            // 
            // createButton
            // 
            this.createButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.createButton.BackColor = System.Drawing.SystemColors.Highlight;
            this.createButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.createButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.createButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.createButton.Location = new System.Drawing.Point(497, 415);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(99, 29);
            this.createButton.TabIndex = 4;
            this.createButton.Text = "Создать";
            this.createButton.UseVisualStyleBackColor = false;
            this.createButton.Click += new System.EventHandler(this.createButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 553);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Программа для создания расписания с сайта  http://days.pravoslavie.ru";
            this.groupBox1.ResumeLayout(false);
            this.monthGroup.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox yearList;
        private System.Windows.Forms.GroupBox monthGroup;
        private System.Windows.Forms.CheckedListBox monthList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.TextBox outDir;
        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label logLabel;
    }
}

