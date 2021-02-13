namespace AutopSalon
{
    partial class FormConstant
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
            this.dataGridViewCnst = new System.Windows.Forms.DataGridView();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonUpd = new System.Windows.Forms.Button();
            this.buttonDel = new System.Windows.Forms.Button();
            this.textBoxMark = new System.Windows.Forms.TextBox();
            this.textBoxMarkUp = new System.Windows.Forms.TextBox();
            this.Марка = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCnst)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewCnst
            // 
            this.dataGridViewCnst.AllowUserToAddRows = false;
            this.dataGridViewCnst.AllowUserToDeleteRows = false;
            this.dataGridViewCnst.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewCnst.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCnst.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewCnst.Name = "dataGridViewCnst";
            this.dataGridViewCnst.ReadOnly = true;
            this.dataGridViewCnst.RowHeadersWidth = 51;
            this.dataGridViewCnst.RowTemplate.Height = 24;
            this.dataGridViewCnst.Size = new System.Drawing.Size(544, 426);
            this.dataGridViewCnst.TabIndex = 0;
            this.dataGridViewCnst.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewCnst_CellMouseClick);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(725, 120);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(151, 39);
            this.buttonAdd.TabIndex = 1;
            this.buttonAdd.Text = "Добавить";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonUpd
            // 
            this.buttonUpd.Location = new System.Drawing.Point(725, 180);
            this.buttonUpd.Name = "buttonUpd";
            this.buttonUpd.Size = new System.Drawing.Size(151, 39);
            this.buttonUpd.TabIndex = 2;
            this.buttonUpd.Text = "Изменить";
            this.buttonUpd.UseVisualStyleBackColor = true;
            this.buttonUpd.Click += new System.EventHandler(this.buttonUpd_Click);
            // 
            // buttonDel
            // 
            this.buttonDel.Location = new System.Drawing.Point(725, 241);
            this.buttonDel.Name = "buttonDel";
            this.buttonDel.Size = new System.Drawing.Size(151, 38);
            this.buttonDel.TabIndex = 3;
            this.buttonDel.Text = "Удалить";
            this.buttonDel.UseVisualStyleBackColor = true;
            this.buttonDel.Click += new System.EventHandler(this.buttonDel_Click);
            // 
            // textBoxMark
            // 
            this.textBoxMark.Location = new System.Drawing.Point(725, 31);
            this.textBoxMark.Name = "textBoxMark";
            this.textBoxMark.Size = new System.Drawing.Size(145, 22);
            this.textBoxMark.TabIndex = 4;
            // 
            // textBoxMarkUp
            // 
            this.textBoxMarkUp.Location = new System.Drawing.Point(725, 67);
            this.textBoxMarkUp.Name = "textBoxMarkUp";
            this.textBoxMarkUp.Size = new System.Drawing.Size(145, 22);
            this.textBoxMarkUp.TabIndex = 5;
            // 
            // Марка
            // 
            this.Марка.AutoSize = true;
            this.Марка.Location = new System.Drawing.Point(594, 34);
            this.Марка.Name = "Марка";
            this.Марка.Size = new System.Drawing.Size(50, 17);
            this.Марка.TabIndex = 6;
            this.Марка.Text = "Марка";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(594, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Наценка, %";
            // 
            // FormConstant
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Марка);
            this.Controls.Add(this.textBoxMarkUp);
            this.Controls.Add(this.textBoxMark);
            this.Controls.Add(this.buttonDel);
            this.Controls.Add(this.buttonUpd);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.dataGridViewCnst);
            this.Name = "FormConstant";
            this.Text = "FormConstant";
            this.Load += new System.EventHandler(this.FormConstant_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCnst)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewCnst;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonUpd;
        private System.Windows.Forms.Button buttonDel;
        private System.Windows.Forms.TextBox textBoxMark;
        private System.Windows.Forms.TextBox textBoxMarkUp;
        private System.Windows.Forms.Label Марка;
        private System.Windows.Forms.Label label2;
    }
}