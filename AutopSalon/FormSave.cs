using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutopSalon
{
    public partial class FormSave : Form
    {
        public string format { get; set; }
        public bool ischeck { set; get; }
        public string Email { get; set; }
        public FormSave()
        {
            InitializeComponent();
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            buttonOK.DialogResult = DialogResult.OK;
            ischeck = false;
            format = comboBox1.SelectedItem.ToString();
            if (checkBoxSend.Checked)
            {
                ischeck = true;
                Email = textBoxEmail.Text;
                if (!string.IsNullOrEmpty(Email))
                {
                    if (Regex.IsMatch(Email, @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[- !#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9az][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$"))
                    {
                        MessageBox.Show("Неверный формат для электронной почты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        buttonOK.DialogResult = DialogResult.Cancel;
                    }
                }
            }
        }
        private void checkBoxSend_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSend.Checked)
            {
                textBoxEmail.ReadOnly = false;
            }
        }
    }
}