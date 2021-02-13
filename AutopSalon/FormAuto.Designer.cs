namespace AutopSalon
{
    partial class FormAuto
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
            this.dataGridViewAuto = new System.Windows.Forms.DataGridView();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonUpd = new System.Windows.Forms.Button();
            this.buttonDel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxMark = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxModel = new System.Windows.Forms.TextBox();
            this.Серия = new System.Windows.Forms.Label();
            this.comboBoxSeria = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAuto)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewAuto
            // 
            this.dataGridViewAuto.AllowUserToAddRows = false;
            this.dataGridViewAuto.AllowUserToDeleteRows = false;
            this.dataGridViewAuto.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewAuto.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAuto.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewAuto.Name = "dataGridViewAuto";
            this.dataGridViewAuto.ReadOnly = true;
            this.dataGridViewAuto.RowHeadersWidth = 51;
            this.dataGridViewAuto.RowTemplate.Height = 24;
            this.dataGridViewAuto.Size = new System.Drawing.Size(655, 527);
            this.dataGridViewAuto.TabIndex = 0;
            this.dataGridViewAuto.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewAuto_CellFormatting);
            this.dataGridViewAuto.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewAuto_CellMouseClick);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(819, 174);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(193, 34);
            this.buttonAdd.TabIndex = 1;
            this.buttonAdd.Text = "Добавить";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonUpd
            // 
            this.buttonUpd.Location = new System.Drawing.Point(819, 226);
            this.buttonUpd.Name = "buttonUpd";
            this.buttonUpd.Size = new System.Drawing.Size(193, 37);
            this.buttonUpd.TabIndex = 2;
            this.buttonUpd.Text = "Редактировать";
            this.buttonUpd.UseVisualStyleBackColor = true;
            this.buttonUpd.Click += new System.EventHandler(this.buttonUpd_Click);
            // 
            // buttonDel
            // 
            this.buttonDel.Location = new System.Drawing.Point(819, 280);
            this.buttonDel.Name = "buttonDel";
            this.buttonDel.Size = new System.Drawing.Size(193, 36);
            this.buttonDel.TabIndex = 3;
            this.buttonDel.Text = "Удалить";
            this.buttonDel.UseVisualStyleBackColor = true;
            this.buttonDel.Click += new System.EventHandler(this.buttonDel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(691, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Марка";
            // 
            // textBoxMark
            // 
            this.textBoxMark.Location = new System.Drawing.Point(796, 36);
            this.textBoxMark.Name = "textBoxMark";
            this.textBoxMark.Size = new System.Drawing.Size(234, 22);
            this.textBoxMark.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(691, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Модель";
            // 
            // textBoxModel
            // 
            this.textBoxModel.Location = new System.Drawing.Point(796, 79);
            this.textBoxModel.Name = "textBoxModel";
            this.textBoxModel.Size = new System.Drawing.Size(234, 22);
            this.textBoxModel.TabIndex = 8;
            // 
            // Серия
            // 
            this.Серия.AutoSize = true;
            this.Серия.Location = new System.Drawing.Point(691, 131);
            this.Серия.Name = "Серия";
            this.Серия.Size = new System.Drawing.Size(49, 17);
            this.Серия.TabIndex = 10;
            this.Серия.Text = "Серия";
            // 
            // comboBoxSeria
            // 
            this.comboBoxSeria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSeria.FormattingEnabled = true;
            this.comboBoxSeria.Location = new System.Drawing.Point(796, 124);
            this.comboBoxSeria.Name = "comboBoxSeria";
            this.comboBoxSeria.Size = new System.Drawing.Size(234, 24);
            this.comboBoxSeria.TabIndex = 11;
            // 
            // FormAuto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1118, 551);
            this.Controls.Add(this.comboBoxSeria);
            this.Controls.Add(this.Серия);
            this.Controls.Add(this.textBoxModel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxMark);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonDel);
            this.Controls.Add(this.buttonUpd);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.dataGridViewAuto);
            this.Name = "FormAuto";
            this.Text = "Авто";
            this.Load += new System.EventHandler(this.FormAuto_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAuto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewAuto;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonUpd;
        private System.Windows.Forms.Button buttonDel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxMark;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxModel;
        private System.Windows.Forms.Label Серия;
        private System.Windows.Forms.ComboBox comboBoxSeria;
    }
}