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
    public partial class FormContragent : Form
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private static string sPath = Path.Combine(Application.StartupPath, "D:\\Users\\iliya\\Документы\\Политех\\3 курс\\1 семестр\\AutoSalonRight.db");
        private string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";

        public FormContragent()
        {
            InitializeComponent();
        }
        private void FormContragent_Load(object sender, EventArgs e)
        {           
            String selectCommand = "Select * from Contragent";
            selectTable(ConnectionString, selectCommand);
        }
        //Выбор данных таблицы
        private void selectTable(string ConnectionString, String selectCommand)
        {
            SQLiteConnection connect = new SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteDataAdapter dataAdapter = new
            SQLiteDataAdapter(selectCommand, connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = ds.Tables[0].ToString();
            connect.Close();
        }
        //Валидация
        private void Validation()
        {
            if (string.IsNullOrEmpty(textBoxFIO.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPassport.Text))
            {
                MessageBox.Show("Заполните Паспорт", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Регулрка на пасспорт
            Regex regex = new Regex(@"\d{4}\s\d{6}$");
            bool matches = regex.IsMatch(textBoxPassport.Text);
            if (!matches)
            {
                MessageBox.Show("Ошибка! Введ неккоректный паспорт. Введите в формате серия_пробел_номер");
                return;
            }
            if (string.IsNullOrEmpty(textBoxPhone.Text))
            {
                MessageBox.Show("Заполните номер телефона", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Регулрка на телефон
            Regex regex1 = new Regex(@"^(8|\+7)\d{10}$");
            bool phones = regex1.IsMatch(textBoxPhone.Text);
            if (!phones)
            {
                MessageBox.Show("Ошибка! Введ неккоректный телефон");
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
        //Обновление dataGridView
        private void refreshForm(string ConnectionString, String selectCommand)
        {
            selectTable(ConnectionString, selectCommand);
            dataGridView1.Update();
            dataGridView1.Refresh();
            textBoxFIO.Text = "";
            textBoxPassport.Text = "";
            textBoxPhone.Text = "";
        }
        private object selectValue(string ConnectionString, String selectCommand)
        {
            SQLiteConnection connect = new
            SQLiteConnection(ConnectionString);
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
            SQLiteConnection connect = new
            SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteTransaction trans;
            SQLiteCommand cmd = new SQLiteCommand();
            trans = connect.BeginTransaction();
            cmd.Connection = connect;
            cmd.CommandText = selectCommand;
            cmd.ExecuteNonQuery();
            trans.Commit();
            connect.Close();
        }
        //Добавление контрагента
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            //Валидация
            Validation();
            //Max ID
            String selectCommand = "select MAX(ID) from Contragent";
            object maxValue = selectValue(ConnectionString, selectCommand);
            if (Convert.ToString(maxValue) == "")
                maxValue = 0;
            //Добавление контрагента в бд
            string txtSQLQuery = "insert into Contragent (ID, FIO, Passport, Phone) values (" +
           (Convert.ToInt32(maxValue) + 1) + ", '" + textBoxFIO.Text + "','" + 
           textBoxPassport.Text + "', '" + textBoxPhone.Text + "')";
            ExecuteQuery(txtSQLQuery);
            //обновление dataGridView
            selectCommand = "select * from Contragent";
            refreshForm(ConnectionString, selectCommand);        
        }
        //Удаление контрагента
        private void buttonDel_Click(object sender, EventArgs e)
        {
            {
                //выбрана строка CurrentRow
                int CurrentRow = dataGridView1.SelectedCells[0].RowIndex;
                string valueId = dataGridView1[0, CurrentRow].Value.ToString();
                //Удаление контрагента из бд
                String selectCommand = "delete from Contragent where ID=" + valueId;              
                changeValue(ConnectionString, selectCommand);
                //обновление dataGridView
                selectCommand = "select * from Contragent";
                refreshForm(ConnectionString, selectCommand);
            }
        }
        //Редактирование контрагента
        private void buttonUpd_Click(object sender, EventArgs e)
        {
            Validation();
            //выбрана строка CurrentRow
            int CurrentRow = dataGridView1.SelectedCells[0].RowIndex;
            string valueId = dataGridView1[0, CurrentRow].Value.ToString();
            string FIO = textBoxFIO.Text;
            string Passport = textBoxPassport.Text;
            string Phone = textBoxPhone.Text;
            //Редактирование контрагента в бд
            String selectCommand = "update Contragent set FIO='" + FIO + "', Passport='" + Passport + "', Phone='" + Phone +"' where id = " + valueId;
            changeValue(ConnectionString, selectCommand);
            //обновление dataGridView
            selectCommand = "select * from Contragent";
            refreshForm(ConnectionString, selectCommand);     
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //выбрана строка CurrentRow
            int CurrentRow = dataGridView1.SelectedCells[0].RowIndex;
            string FIOId = dataGridView1[1, CurrentRow].Value.ToString();
            string PassportId = dataGridView1[2, CurrentRow].Value.ToString();
            string PhoneId = dataGridView1[3, CurrentRow].Value.ToString();
            textBoxFIO.Text = FIOId;
            textBoxPassport.Text = PassportId;
            textBoxPhone.Text = PhoneId;
        }

        private void textBoxFIO_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsLetter(ch) && ch != 8 && ch != 32)
            {
                e.Handled = true;
            }
        }

        private void textBoxPassport_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 32)
            {
                e.Handled = true;
            }
        }

        private void textBoxPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 43)
            {
                e.Handled = true;
            }
        }
    }
}
