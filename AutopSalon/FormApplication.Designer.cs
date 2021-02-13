namespace AutopSalon
{
    partial class FormApplication
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormApplication));
            this.dataGridViewApp = new System.Windows.Forms.DataGridView();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxAuto = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxContragent = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonUpd = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonDel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonVibor = new System.Windows.Forms.Button();
            this.textBoxCountAuto = new System.Windows.Forms.TextBox();
            this.Количество = new System.Windows.Forms.Label();
            this.comboBoxSeria = new System.Windows.Forms.ComboBox();
            this.Серия = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxCountService = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxService = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewApp)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewApp
            // 
            this.dataGridViewApp.AllowUserToAddRows = false;
            this.dataGridViewApp.AllowUserToDeleteRows = false;
            this.dataGridViewApp.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewApp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewApp.Location = new System.Drawing.Point(13, 42);
            this.dataGridViewApp.Name = "dataGridViewApp";
            this.dataGridViewApp.ReadOnly = true;
            this.dataGridViewApp.RowHeadersWidth = 51;
            this.dataGridViewApp.RowTemplate.Height = 24;
            this.dataGridViewApp.Size = new System.Drawing.Size(1008, 586);
            this.dataGridViewApp.TabIndex = 0;
            this.dataGridViewApp.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewApp_CellFormatting);
            this.dataGridViewApp.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewApp_CellMouseClick);
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Location = new System.Drawing.Point(1247, 63);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(200, 22);
            this.dateTimePicker.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1112, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Дата заявки";
            // 
            // comboBoxAuto
            // 
            this.comboBoxAuto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAuto.FormattingEnabled = true;
            this.comboBoxAuto.Location = new System.Drawing.Point(1247, 213);
            this.comboBoxAuto.Name = "comboBoxAuto";
            this.comboBoxAuto.Size = new System.Drawing.Size(200, 24);
            this.comboBoxAuto.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1114, 220);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Автомобиль";
            // 
            // comboBoxContragent
            // 
            this.comboBoxContragent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxContragent.FormattingEnabled = true;
            this.comboBoxContragent.Location = new System.Drawing.Point(1247, 110);
            this.comboBoxContragent.Name = "comboBoxContragent";
            this.comboBoxContragent.Size = new System.Drawing.Size(200, 24);
            this.comboBoxContragent.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1114, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Контрагент";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator4,
            this.toolStripButtonAdd,
            this.toolStripSeparator1,
            this.toolStripButtonUpd,
            this.toolStripSeparator2,
            this.toolStripButtonDel,
            this.toolStripSeparator3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1482, 31);
            this.toolStrip1.TabIndex = 12;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 29);
            // 
            // toolStripButtonAdd
            // 
            this.toolStripButtonAdd.BackColor = System.Drawing.SystemColors.Desktop;
            this.toolStripButtonAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonAdd.Font = new System.Drawing.Font("MV Boli", 10.2F, System.Drawing.FontStyle.Italic);
            this.toolStripButtonAdd.ForeColor = System.Drawing.SystemColors.Control;
            this.toolStripButtonAdd.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAdd.Image")));
            this.toolStripButtonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAdd.Name = "toolStripButtonAdd";
            this.toolStripButtonAdd.Size = new System.Drawing.Size(95, 28);
            this.toolStripButtonAdd.Text = "Добавить";
            this.toolStripButtonAdd.Click += new System.EventHandler(this.toolStripButtonAdd_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 29);
            // 
            // toolStripButtonUpd
            // 
            this.toolStripButtonUpd.BackColor = System.Drawing.SystemColors.Desktop;
            this.toolStripButtonUpd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonUpd.Font = new System.Drawing.Font("MV Boli", 10.2F, System.Drawing.FontStyle.Italic);
            this.toolStripButtonUpd.ForeColor = System.Drawing.SystemColors.Control;
            this.toolStripButtonUpd.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonUpd.Image")));
            this.toolStripButtonUpd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonUpd.Name = "toolStripButtonUpd";
            this.toolStripButtonUpd.Size = new System.Drawing.Size(141, 26);
            this.toolStripButtonUpd.Text = "Редактировать";
            this.toolStripButtonUpd.Click += new System.EventHandler(this.toolStripButtonUpd_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 29);
            // 
            // toolStripButtonDel
            // 
            this.toolStripButtonDel.BackColor = System.Drawing.SystemColors.Desktop;
            this.toolStripButtonDel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonDel.Font = new System.Drawing.Font("MV Boli", 10.2F, System.Drawing.FontStyle.Italic);
            this.toolStripButtonDel.ForeColor = System.Drawing.SystemColors.Control;
            this.toolStripButtonDel.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDel.Image")));
            this.toolStripButtonDel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDel.Name = "toolStripButtonDel";
            this.toolStripButtonDel.Size = new System.Drawing.Size(84, 26);
            this.toolStripButtonDel.Text = "Удалить";
            this.toolStripButtonDel.Click += new System.EventHandler(this.toolStripButtonDel_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 29);
            // 
            // buttonVibor
            // 
            this.buttonVibor.Location = new System.Drawing.Point(1181, 462);
            this.buttonVibor.Name = "buttonVibor";
            this.buttonVibor.Size = new System.Drawing.Size(280, 49);
            this.buttonVibor.TabIndex = 13;
            this.buttonVibor.Text = "Выбрать";
            this.buttonVibor.UseVisualStyleBackColor = true;
            this.buttonVibor.Click += new System.EventHandler(this.ButtonVibor_Click);
            // 
            // textBoxCountAuto
            // 
            this.textBoxCountAuto.Location = new System.Drawing.Point(1247, 258);
            this.textBoxCountAuto.Name = "textBoxCountAuto";
            this.textBoxCountAuto.Size = new System.Drawing.Size(200, 22);
            this.textBoxCountAuto.TabIndex = 14;
            // 
            // Количество
            // 
            this.Количество.AutoSize = true;
            this.Количество.Location = new System.Drawing.Point(1112, 263);
            this.Количество.Name = "Количество";
            this.Количество.Size = new System.Drawing.Size(86, 17);
            this.Количество.TabIndex = 15;
            this.Количество.Text = "Количество";
            // 
            // comboBoxSeria
            // 
            this.comboBoxSeria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSeria.FormattingEnabled = true;
            this.comboBoxSeria.Location = new System.Drawing.Point(1247, 163);
            this.comboBoxSeria.Name = "comboBoxSeria";
            this.comboBoxSeria.Size = new System.Drawing.Size(200, 24);
            this.comboBoxSeria.TabIndex = 16;
            this.comboBoxSeria.SelectionChangeCommitted += new System.EventHandler(this.comboBoxSeria_SelectionChangeCommitted);
            // 
            // Серия
            // 
            this.Серия.AutoSize = true;
            this.Серия.Location = new System.Drawing.Point(1114, 170);
            this.Серия.Name = "Серия";
            this.Серия.Size = new System.Drawing.Size(49, 17);
            this.Серия.TabIndex = 17;
            this.Серия.Text = "Серия";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1114, 304);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 17);
            this.label2.TabIndex = 19;
            this.label2.Text = "Доп. услуги";
            // 
            // textBoxCountService
            // 
            this.textBoxCountService.Location = new System.Drawing.Point(1247, 338);
            this.textBoxCountService.Name = "textBoxCountService";
            this.textBoxCountService.Size = new System.Drawing.Size(200, 22);
            this.textBoxCountService.TabIndex = 20;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1114, 343);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 17);
            this.label5.TabIndex = 21;
            this.label5.Text = "Кол-во услуг";
            // 
            // comboBoxService
            // 
            this.comboBoxService.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxService.FormattingEnabled = true;
            this.comboBoxService.Location = new System.Drawing.Point(1247, 301);
            this.comboBoxService.Name = "comboBoxService";
            this.comboBoxService.Size = new System.Drawing.Size(200, 24);
            this.comboBoxService.TabIndex = 22;
            // 
            // FormApplication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1482, 663);
            this.Controls.Add(this.comboBoxService);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxCountService);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Серия);
            this.Controls.Add(this.comboBoxSeria);
            this.Controls.Add(this.Количество);
            this.Controls.Add(this.textBoxCountAuto);
            this.Controls.Add(this.buttonVibor);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBoxContragent);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxAuto);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePicker);
            this.Controls.Add(this.dataGridViewApp);
            this.Name = "FormApplication";
            this.Text = "Заявка";
            this.Load += new System.EventHandler(this.FormApplication_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewApp)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewApp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        public System.Windows.Forms.ToolStripButton toolStripButtonAdd;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonUpd;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButtonDel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        public System.Windows.Forms.ToolStrip toolStrip1;
        public System.Windows.Forms.DateTimePicker dateTimePicker;
        public System.Windows.Forms.ComboBox comboBoxAuto;
        public System.Windows.Forms.ComboBox comboBoxContragent;
        private System.Windows.Forms.Button buttonVibor;
        public System.Windows.Forms.TextBox textBoxCountAuto;
        private System.Windows.Forms.Label Количество;
        private System.Windows.Forms.Label Серия;
        public System.Windows.Forms.ComboBox comboBoxSeria;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox textBoxCountService;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.ComboBox comboBoxService;
    }
}