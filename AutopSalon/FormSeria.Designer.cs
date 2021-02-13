namespace AutopSalon
{
    partial class FormSeria
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
            this.dataGridViewSeria = new System.Windows.Forms.DataGridView();
            this.Серия = new System.Windows.Forms.Label();
            this.textBoxSeria = new System.Windows.Forms.TextBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonUpd = new System.Windows.Forms.Button();
            this.buttonDel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSeria)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewSeria
            // 
            this.dataGridViewSeria.AllowUserToAddRows = false;
            this.dataGridViewSeria.AllowUserToDeleteRows = false;
            this.dataGridViewSeria.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSeria.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewSeria.Name = "dataGridViewSeria";
            this.dataGridViewSeria.ReadOnly = true;
            this.dataGridViewSeria.RowHeadersWidth = 51;
            this.dataGridViewSeria.RowTemplate.Height = 24;
            this.dataGridViewSeria.Size = new System.Drawing.Size(395, 300);
            this.dataGridViewSeria.TabIndex = 0;
            this.dataGridViewSeria.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewSeria_CellMouseClick);
            // 
            // Серия
            // 
            this.Серия.AutoSize = true;
            this.Серия.Location = new System.Drawing.Point(441, 43);
            this.Серия.Name = "Серия";
            this.Серия.Size = new System.Drawing.Size(49, 17);
            this.Серия.TabIndex = 1;
            this.Серия.Text = "Серия";
            // 
            // textBoxSeria
            // 
            this.textBoxSeria.Location = new System.Drawing.Point(554, 38);
            this.textBoxSeria.Name = "textBoxSeria";
            this.textBoxSeria.Size = new System.Drawing.Size(174, 22);
            this.textBoxSeria.TabIndex = 2;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(554, 107);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(178, 36);
            this.buttonAdd.TabIndex = 3;
            this.buttonAdd.Text = "Добавить";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonUpd
            // 
            this.buttonUpd.Location = new System.Drawing.Point(554, 160);
            this.buttonUpd.Name = "buttonUpd";
            this.buttonUpd.Size = new System.Drawing.Size(178, 36);
            this.buttonUpd.TabIndex = 4;
            this.buttonUpd.Text = "Изменить";
            this.buttonUpd.UseVisualStyleBackColor = true;
            this.buttonUpd.Click += new System.EventHandler(this.buttonUpd_Click);
            // 
            // buttonDel
            // 
            this.buttonDel.Location = new System.Drawing.Point(554, 214);
            this.buttonDel.Name = "buttonDel";
            this.buttonDel.Size = new System.Drawing.Size(178, 36);
            this.buttonDel.TabIndex = 5;
            this.buttonDel.Text = "Удалить";
            this.buttonDel.UseVisualStyleBackColor = true;
            this.buttonDel.Click += new System.EventHandler(this.buttonDel_Click);
            // 
            // FormSeria
            // 
            this.ClientSize = new System.Drawing.Size(768, 329);
            this.Controls.Add(this.buttonDel);
            this.Controls.Add(this.buttonUpd);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.textBoxSeria);
            this.Controls.Add(this.Серия);
            this.Controls.Add(this.dataGridViewSeria);
            this.Name = "FormSeria";
            this.Load += new System.EventHandler(this.FormSeria_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSeria)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridViewSeria;
        private System.Windows.Forms.Label Серия;
        private System.Windows.Forms.TextBox textBoxSeria;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonUpd;
        private System.Windows.Forms.Button buttonDel;
    }
}