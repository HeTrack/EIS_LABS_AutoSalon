namespace AutopSalon
{
    partial class AutoRequest
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
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.dataGridViewHeader = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Количество = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewAutoReport = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePickerFrom = new System.Windows.Forms.DateTimePicker();
            this.по = new System.Windows.Forms.Label();
            this.dateTimePickerTo = new System.Windows.Forms.DateTimePicker();
            this.buttonForm = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxStartOst = new System.Windows.Forms.TextBox();
            this.textBoxPrihod = new System.Windows.Forms.TextBox();
            this.textBoxRashod = new System.Windows.Forms.TextBox();
            this.textBoxEndOst = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAutoReport)).BeginInit();
            this.SuspendLayout();
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Location = new System.Drawing.Point(518, 139);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(8, 8);
            this.propertyGrid1.TabIndex = 0;
            // 
            // dataGridViewHeader
            // 
            this.dataGridViewHeader.AllowUserToAddRows = false;
            this.dataGridViewHeader.AllowUserToDeleteRows = false;
            this.dataGridViewHeader.AllowUserToResizeColumns = false;
            this.dataGridViewHeader.AllowUserToResizeRows = false;
            this.dataGridViewHeader.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewHeader.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridViewHeader.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridViewHeader.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewHeader.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Количество});
            this.dataGridViewHeader.Location = new System.Drawing.Point(26, 95);
            this.dataGridViewHeader.Name = "dataGridViewHeader";
            this.dataGridViewHeader.ReadOnly = true;
            this.dataGridViewHeader.RowHeadersWidth = 51;
            this.dataGridViewHeader.RowTemplate.Height = 24;
            this.dataGridViewHeader.Size = new System.Drawing.Size(735, 22);
            this.dataGridViewHeader.TabIndex = 1;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 125;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 125;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 125;
            // 
            // Количество
            // 
            this.Количество.HeaderText = "Количество";
            this.Количество.MinimumWidth = 6;
            this.Количество.Name = "Количество";
            this.Количество.ReadOnly = true;
            this.Количество.Width = 125;
            // 
            // dataGridViewAutoReport
            // 
            this.dataGridViewAutoReport.AllowUserToAddRows = false;
            this.dataGridViewAutoReport.AllowUserToDeleteRows = false;
            this.dataGridViewAutoReport.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewAutoReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAutoReport.Location = new System.Drawing.Point(26, 116);
            this.dataGridViewAutoReport.Name = "dataGridViewAutoReport";
            this.dataGridViewAutoReport.ReadOnly = true;
            this.dataGridViewAutoReport.RowHeadersWidth = 51;
            this.dataGridViewAutoReport.RowTemplate.Height = 24;
            this.dataGridViewAutoReport.Size = new System.Drawing.Size(735, 384);
            this.dataGridViewAutoReport.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "период с";
            // 
            // dateTimePickerFrom
            // 
            this.dateTimePickerFrom.Location = new System.Drawing.Point(110, 12);
            this.dateTimePickerFrom.Name = "dateTimePickerFrom";
            this.dateTimePickerFrom.Size = new System.Drawing.Size(200, 22);
            this.dateTimePickerFrom.TabIndex = 4;
            // 
            // по
            // 
            this.по.AutoSize = true;
            this.по.Location = new System.Drawing.Point(338, 17);
            this.по.Name = "по";
            this.по.Size = new System.Drawing.Size(24, 17);
            this.по.TabIndex = 5;
            this.по.Text = "по";
            // 
            // dateTimePickerTo
            // 
            this.dateTimePickerTo.Location = new System.Drawing.Point(384, 12);
            this.dateTimePickerTo.Name = "dateTimePickerTo";
            this.dateTimePickerTo.Size = new System.Drawing.Size(200, 22);
            this.dateTimePickerTo.TabIndex = 6;
            // 
            // buttonForm
            // 
            this.buttonForm.Location = new System.Drawing.Point(622, 52);
            this.buttonForm.Name = "buttonForm";
            this.buttonForm.Size = new System.Drawing.Size(139, 28);
            this.buttonForm.TabIndex = 7;
            this.buttonForm.Text = "Сформировать";
            this.buttonForm.UseVisualStyleBackColor = true;
            this.buttonForm.Click += new System.EventHandler(this.buttonForm_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(622, 12);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(139, 28);
            this.buttonSave.TabIndex = 8;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(338, 530);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 17);
            this.label2.TabIndex = 10;
            this.label2.Text = "ИТОГО:";
            // 
            // textBoxStartOst
            // 
            this.textBoxStartOst.Location = new System.Drawing.Point(410, 525);
            this.textBoxStartOst.Name = "textBoxStartOst";
            this.textBoxStartOst.Size = new System.Drawing.Size(80, 22);
            this.textBoxStartOst.TabIndex = 11;
            // 
            // textBoxPrihod
            // 
            this.textBoxPrihod.Location = new System.Drawing.Point(519, 525);
            this.textBoxPrihod.Name = "textBoxPrihod";
            this.textBoxPrihod.Size = new System.Drawing.Size(50, 22);
            this.textBoxPrihod.TabIndex = 12;
            // 
            // textBoxRashod
            // 
            this.textBoxRashod.Location = new System.Drawing.Point(587, 525);
            this.textBoxRashod.Name = "textBoxRashod";
            this.textBoxRashod.Size = new System.Drawing.Size(50, 22);
            this.textBoxRashod.TabIndex = 13;
            // 
            // textBoxEndOst
            // 
            this.textBoxEndOst.Location = new System.Drawing.Point(655, 525);
            this.textBoxEndOst.Name = "textBoxEndOst";
            this.textBoxEndOst.Size = new System.Drawing.Size(80, 22);
            this.textBoxEndOst.TabIndex = 14;
            // 
            // AutoRequest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 583);
            this.Controls.Add(this.textBoxEndOst);
            this.Controls.Add(this.textBoxRashod);
            this.Controls.Add(this.textBoxPrihod);
            this.Controls.Add(this.textBoxStartOst);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonForm);
            this.Controls.Add(this.dateTimePickerTo);
            this.Controls.Add(this.по);
            this.Controls.Add(this.dateTimePickerFrom);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridViewAutoReport);
            this.Controls.Add(this.dataGridViewHeader);
            this.Controls.Add(this.propertyGrid1);
            this.Name = "AutoRequest";
            this.Text = "Ведомость: Движение авто за период";
            this.Load += new System.EventHandler(this.AutoRequest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAutoReport)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.DataGridView dataGridViewHeader;
        private System.Windows.Forms.DataGridView dataGridViewAutoReport;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Количество;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePickerFrom;
        private System.Windows.Forms.Label по;
        private System.Windows.Forms.DateTimePicker dateTimePickerTo;
        private System.Windows.Forms.Button buttonForm;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxStartOst;
        private System.Windows.Forms.TextBox textBoxPrihod;
        private System.Windows.Forms.TextBox textBoxRashod;
        private System.Windows.Forms.TextBox textBoxEndOst;
    }
}