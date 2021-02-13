namespace AutopSalon
{
    partial class FormEntrance
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
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonUpd = new System.Windows.Forms.Button();
            this.buttonDel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Количество = new System.Windows.Forms.Label();
            this.textBoxBuyCost = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxCount = new System.Windows.Forms.TextBox();
            this.sqLiteCommand1 = new System.Data.SQLite.SQLiteCommand();
            this.comboBoxSupply = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxSeria = new System.Windows.Forms.ComboBox();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.dataGridViewEntrance = new System.Windows.Forms.DataGridView();
            this.comboBoxAuto = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEntrance)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(609, 52);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(193, 34);
            this.buttonAdd.TabIndex = 1;
            this.buttonAdd.Text = "Добавить";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonUpd
            // 
            this.buttonUpd.Location = new System.Drawing.Point(609, 114);
            this.buttonUpd.Name = "buttonUpd";
            this.buttonUpd.Size = new System.Drawing.Size(193, 37);
            this.buttonUpd.TabIndex = 2;
            this.buttonUpd.Text = "Редактировать";
            this.buttonUpd.UseVisualStyleBackColor = true;
            this.buttonUpd.Click += new System.EventHandler(this.buttonUpd_Click);
            // 
            // buttonDel
            // 
            this.buttonDel.Location = new System.Drawing.Point(609, 178);
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
            this.label1.Location = new System.Drawing.Point(39, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Дата поступления";
            // 
            // Количество
            // 
            this.Количество.AutoSize = true;
            this.Количество.Location = new System.Drawing.Point(40, 202);
            this.Количество.Name = "Количество";
            this.Количество.Size = new System.Drawing.Size(86, 17);
            this.Количество.TabIndex = 7;
            this.Количество.Text = "Количество";
            // 
            // textBoxBuyCost
            // 
            this.textBoxBuyCost.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxBuyCost.Location = new System.Drawing.Point(201, 237);
            this.textBoxBuyCost.Name = "textBoxBuyCost";
            this.textBoxBuyCost.Size = new System.Drawing.Size(234, 22);
            this.textBoxBuyCost.TabIndex = 8;
            this.textBoxBuyCost.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxBuyCost_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 242);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Закупочная цена";
            // 
            // textBoxCount
            // 
            this.textBoxCount.Location = new System.Drawing.Point(201, 197);
            this.textBoxCount.MaxLength = 18;
            this.textBoxCount.Name = "textBoxCount";
            this.textBoxCount.Size = new System.Drawing.Size(234, 22);
            this.textBoxCount.TabIndex = 10;
            this.textBoxCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxCount_KeyPress);
            // 
            // sqLiteCommand1
            // 
            this.sqLiteCommand1.CommandText = null;
            // 
            // comboBoxSupply
            // 
            this.comboBoxSupply.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSupply.FormattingEnabled = true;
            this.comboBoxSupply.Location = new System.Drawing.Point(201, 157);
            this.comboBoxSupply.Name = "comboBoxSupply";
            this.comboBoxSupply.Size = new System.Drawing.Size(234, 24);
            this.comboBoxSupply.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 17);
            this.label2.TabIndex = 12;
            this.label2.Text = "Серия";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(39, 164);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 17);
            this.label4.TabIndex = 13;
            this.label4.Text = "Поставщик";
            // 
            // comboBoxSeria
            // 
            this.comboBoxSeria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSeria.FormattingEnabled = true;
            this.comboBoxSeria.Location = new System.Drawing.Point(201, 74);
            this.comboBoxSeria.Name = "comboBoxSeria";
            this.comboBoxSeria.Size = new System.Drawing.Size(234, 24);
            this.comboBoxSeria.TabIndex = 14;
            this.comboBoxSeria.SelectionChangeCommitted += new System.EventHandler(this.comboBoxSeria_SelectionChangeCommitted);
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Location = new System.Drawing.Point(201, 31);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(234, 22);
            this.dateTimePicker.TabIndex = 15;
            // 
            // dataGridViewEntrance
            // 
            this.dataGridViewEntrance.AllowUserToAddRows = false;
            this.dataGridViewEntrance.AllowUserToDeleteRows = false;
            this.dataGridViewEntrance.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewEntrance.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEntrance.Location = new System.Drawing.Point(12, 293);
            this.dataGridViewEntrance.Name = "dataGridViewEntrance";
            this.dataGridViewEntrance.RowHeadersWidth = 51;
            this.dataGridViewEntrance.RowTemplate.Height = 24;
            this.dataGridViewEntrance.Size = new System.Drawing.Size(887, 391);
            this.dataGridViewEntrance.TabIndex = 16;
            this.dataGridViewEntrance.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewEntrance_CellFormatting);
            this.dataGridViewEntrance.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewEntrance_CellMouseClick);
            // 
            // comboBoxAuto
            // 
            this.comboBoxAuto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAuto.FormattingEnabled = true;
            this.comboBoxAuto.Location = new System.Drawing.Point(201, 114);
            this.comboBoxAuto.Name = "comboBoxAuto";
            this.comboBoxAuto.Size = new System.Drawing.Size(234, 24);
            this.comboBoxAuto.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(40, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 17);
            this.label5.TabIndex = 18;
            this.label5.Text = "Автомобиль";
            // 
            // FormEntrance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(911, 696);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBoxAuto);
            this.Controls.Add(this.dataGridViewEntrance);
            this.Controls.Add(this.dateTimePicker);
            this.Controls.Add(this.comboBoxSeria);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxSupply);
            this.Controls.Add(this.textBoxCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxBuyCost);
            this.Controls.Add(this.Количество);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonDel);
            this.Controls.Add(this.buttonUpd);
            this.Controls.Add(this.buttonAdd);
            this.Name = "FormEntrance";
            this.Text = "Поступление";
            this.Load += new System.EventHandler(this.FormEntrance_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEntrance)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonUpd;
        private System.Windows.Forms.Button buttonDel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Количество;
        private System.Windows.Forms.TextBox textBoxBuyCost;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxCount;
        private System.Data.SQLite.SQLiteCommand sqLiteCommand1;
        private System.Windows.Forms.ComboBox comboBoxSupply;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxSeria;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.DataGridView dataGridViewEntrance;
        private System.Windows.Forms.ComboBox comboBoxAuto;
        private System.Windows.Forms.Label label5;
    }
}