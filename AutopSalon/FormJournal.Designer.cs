namespace AutopSalon
{
    partial class FormJournal
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
            this.dataGridViewJournal = new System.Windows.Forms.DataGridView();
            this.buttonUpd = new System.Windows.Forms.Button();
            this.buttonDel = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboOperation = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonProvodki = new System.Windows.Forms.Button();
            this.buttonRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewJournal)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewJournal
            // 
            this.dataGridViewJournal.AllowUserToAddRows = false;
            this.dataGridViewJournal.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewJournal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewJournal.Location = new System.Drawing.Point(25, 41);
            this.dataGridViewJournal.Name = "dataGridViewJournal";
            this.dataGridViewJournal.ReadOnly = true;
            this.dataGridViewJournal.RowHeadersWidth = 51;
            this.dataGridViewJournal.RowTemplate.Height = 24;
            this.dataGridViewJournal.Size = new System.Drawing.Size(752, 470);
            this.dataGridViewJournal.TabIndex = 2;
            this.dataGridViewJournal.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewJournal_CellFormatting);
            // 
            // buttonUpd
            // 
            this.buttonUpd.Location = new System.Drawing.Point(807, 118);
            this.buttonUpd.Name = "buttonUpd";
            this.buttonUpd.Size = new System.Drawing.Size(172, 46);
            this.buttonUpd.TabIndex = 8;
            this.buttonUpd.Text = "Изменить";
            this.buttonUpd.UseVisualStyleBackColor = true;
            this.buttonUpd.Click += new System.EventHandler(this.buttonUpd_Click);
            // 
            // buttonDel
            // 
            this.buttonDel.Location = new System.Drawing.Point(807, 186);
            this.buttonDel.Name = "buttonDel";
            this.buttonDel.Size = new System.Drawing.Size(172, 44);
            this.buttonDel.TabIndex = 9;
            this.buttonDel.Text = "Удалить";
            this.buttonDel.UseVisualStyleBackColor = true;
            this.buttonDel.Click += new System.EventHandler(this.buttonDel_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripComboOperation,
            this.toolStripSeparator1,
            this.toolStripButtonAdd,
            this.toolStripSeparator2});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1018, 28);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 10;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(155, 20);
            this.toolStripLabel1.Text = "Выберите операцию";
            // 
            // toolStripComboOperation
            // 
            this.toolStripComboOperation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboOperation.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.toolStripComboOperation.Items.AddRange(new object[] {
            "Поступление серии авто",
            "Продажа авто"});
            this.toolStripComboOperation.Name = "toolStripComboOperation";
            this.toolStripComboOperation.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripComboOperation.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripComboOperation.Size = new System.Drawing.Size(215, 28);
            this.toolStripComboOperation.SelectedIndexChanged += new System.EventHandler(this.toolStripComboOperation_SelectedIndexChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
            // 
            // toolStripButtonAdd
            // 
            this.toolStripButtonAdd.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.toolStripButtonAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonAdd.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.toolStripButtonAdd.ImageTransparentColor = System.Drawing.Color.White;
            this.toolStripButtonAdd.Name = "toolStripButtonAdd";
            this.toolStripButtonAdd.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.toolStripButtonAdd.Size = new System.Drawing.Size(162, 24);
            this.toolStripButtonAdd.Text = "Добавить операцию";
            this.toolStripButtonAdd.Click += new System.EventHandler(this.toolStripButtonAdd_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 23);
            // 
            // buttonProvodki
            // 
            this.buttonProvodki.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonProvodki.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonProvodki.Location = new System.Drawing.Point(807, 41);
            this.buttonProvodki.Name = "buttonProvodki";
            this.buttonProvodki.Size = new System.Drawing.Size(172, 54);
            this.buttonProvodki.TabIndex = 11;
            this.buttonProvodki.Text = "Просмотреть проводки";
            this.buttonProvodki.UseVisualStyleBackColor = false;
            this.buttonProvodki.Click += new System.EventHandler(this.buttonProvodki_Click);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Location = new System.Drawing.Point(807, 248);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(172, 46);
            this.buttonRefresh.TabIndex = 12;
            this.buttonRefresh.Text = "Обновить";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // FormJournal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1018, 542);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.buttonProvodki);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.buttonDel);
            this.Controls.Add(this.buttonUpd);
            this.Controls.Add(this.dataGridViewJournal);
            this.Name = "FormJournal";
            this.Text = "Журнал Операций";
            this.Load += new System.EventHandler(this.FormJournal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewJournal)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridViewJournal;
        private System.Windows.Forms.Button buttonUpd;
        private System.Windows.Forms.Button buttonDel;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox toolStripComboOperation;
        private System.Windows.Forms.ToolStripButton toolStripButtonAdd;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Button buttonProvodki;
        private System.Windows.Forms.Button buttonRefresh;
    }
}