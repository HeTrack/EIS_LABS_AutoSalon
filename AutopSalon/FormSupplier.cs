using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutopSalon
{
    public partial class FormSupplier : Form
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private static string sPath = Path.Combine(Application.StartupPath, "D:\\Users\\iliya\\Документы\\Политех\\3 курс\\1 семестр\\AutoSalonRight.db");
        private string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
        public FormSupplier()
        {
            InitializeComponent();
        }
        private void FormSupplier_Load(object sender, EventArgs e)
        {           
            String selectCommand = "Select * from Supplier";
            selectTable(ConnectionString, selectCommand);
        }
        private void selectTable(string ConnectionString, String selectCommand)
        {
            SQLiteConnection connect = new SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(selectCommand, connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = ds.Tables[0].ToString();
            connect.Close();
        }
        //Валидация
        private void Validation()
        {
            if (string.IsNullOrEmpty(textBoxSupplierFIO.Text))
            {
                MessageBox.Show("Заполните ФИО поставщика", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxAdress.Text))
            {
                MessageBox.Show("Заполните Юр.Адрес поставщика", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPayAccount.Text))
            {
                MessageBox.Show("Заполните расчётный счёт поставщика", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (textBoxPayAccount.Text.Length != 20)
            {
                MessageBox.Show("Введён неправильный номер расчетного счета", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private void ExecuteQuery(string txtQuery)
        {
            sql_con = new SQLiteConnection("Data Source=" + sPath + ";Version=3;New=False;Compress=True;");
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = txtQuery;
            sql_cmd.ExecuteNonQuery();
            sql_con.Close();
        }
        private void refreshForm(string ConnectionString, String selectCommand)
        {
            selectTable(ConnectionString, selectCommand);
            dataGridView1.Update();
            dataGridView1.Refresh();
            textBoxSupplierFIO.Text = "";
            textBoxAdress.Text = "";
            textBoxPayAccount.Text = "";
        }
        private object selectValue(string ConnectionString, String selectCommand)
        {
            SQLiteConnection connect = new SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteCommand command = new SQLiteCommand(selectCommand, connect);
            SQLiteDataReader reader = command.ExecuteReader();
            object value = "";
            while (reader.Read())
            {
                value = reader[0];
            }
            connect.Close();
            return value;
        }
        private void changeValue(string ConnectionString, String selectCommand)
        {
            SQLiteConnection connect = new SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteTransaction trans;
            //pragma
            var pragma = new SQLiteCommand("PRAGMA foreign_keys = true;", connect);
            pragma.ExecuteNonQuery();
            SQLiteCommand cmd = new SQLiteCommand();
            trans = connect.BeginTransaction();
            cmd.Connection = connect;
            cmd.CommandText = selectCommand;
            cmd.ExecuteNonQuery();
            trans.Commit();
            connect.Close();
        }
        //Добавление поставшика
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Validation();
            //MAX ID
            String selectCommand = "select MAX(ID) from Supplier";
            object maxValue = selectValue(ConnectionString, selectCommand);
            if (Convert.ToString(maxValue) == "")
                maxValue = 0;
             //Добавление поставщика в бд
            string txtSQLQuery = "insert into Supplier (ID, SupplyName, Adress, AccountPay) values (" +
           (Convert.ToInt32(maxValue) + 1) + ", '" + textBoxSupplierFIO.Text + "','" +
           textBoxAdress.Text + "',  '" + textBoxPayAccount.Text + "')";
            ExecuteQuery(txtSQLQuery);
            //обновление dataGridView
            selectCommand = "select * from Supplier";
            refreshForm(ConnectionString, selectCommand);          
        } 
        //Удаление поставщика
        private void buttonDel_Click(object sender, EventArgs e)
        {
            {
                //выбрана строка CurrentRow
                int CurrentRow = dataGridView1.SelectedCells[0].RowIndex;             
                string valueId = dataGridView1[0, CurrentRow].Value.ToString();
                //Удаление поставщика из бд
                String selectCommand = "delete from Supplier where ID=" + valueId;
                changeValue(ConnectionString, selectCommand);
                //обновление dataGridView
                selectCommand = "select * from Supplier";
                refreshForm(ConnectionString, selectCommand);              
            }
        } 
        //Изменение поставщика
        private void buttonUpd_Click(object sender, EventArgs e)
        {
            Validation();
            //выбрана строка CurrentRow
            int CurrentRow = dataGridView1.SelectedCells[0].RowIndex;           
            string valueId = dataGridView1[0, CurrentRow].Value.ToString();
            string SupplierName = textBoxSupplierFIO.Text;
            string Adress = textBoxAdress.Text;
            string PayAccount = textBoxPayAccount.Text;
            //Обновление поставщика в бд
            String selectCommand = "update Supplier set SupplyName='" + SupplierName + "', Adress='" + Adress + "', AccountPay='" + PayAccount + "' where ID = " + valueId;
            changeValue(ConnectionString, selectCommand);
            //обновление dataGridView
            selectCommand = "select * from Supplier";
            refreshForm(ConnectionString, selectCommand);          
        }
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //выбрана строка CurrentRow
            int CurrentRow = dataGridView1.SelectedCells[0].RowIndex;
            string SupplierName = dataGridView1[1, CurrentRow].Value.ToString();
            string Adress = dataGridView1[2, CurrentRow].Value.ToString();
            string PayAccount = dataGridView1[3, CurrentRow].Value.ToString();
            textBoxSupplierFIO.Text = SupplierName;
            textBoxAdress.Text = Adress;
            textBoxPayAccount.Text = PayAccount;
        }
        private void textBoxSupplierFIO_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsLetter(ch) && ch != 8 && ch!=32)
            {
                e.Handled = true;
            }
        }
        private void textBoxPayAccount_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }
    }
}
