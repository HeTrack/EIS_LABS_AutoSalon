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
    public partial class FormConstant : Form
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private static string sPath = Path.Combine(Application.StartupPath, "D:\\Users\\iliya\\Документы\\Политех\\3 курс\\1 семестр\\AutoSalonRight.db");
        private string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
        private List<string> marks = new List<string>() {
            "Audi","BMW","Chery",
            "Chevrolet","Datsun","Ford",
            "Honda","Hyundai","Kia",
            "Lada","Lexus","Mazda",
            "Mercedes-Benz","Nissan","Opel",
            "Peugeot","Porshe","Renault",
            "Skoda","Suzuki","Toyota",
            "Volkswagen","Volvo"
        };
        private string Mark;
        private string Procent;
        public FormConstant()
        {
            InitializeComponent();
        }
        private void FormConstant_Load(object sender, EventArgs e)
        {
            String selectCommand = "Select * from Constant";
            selectTable(ConnectionString, selectCommand);
        }
        //Выбор данных для таблицы
        private void selectTable(string ConnectionString, String selectCommand)
        {
            SQLiteConnection connect = new SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteDataAdapter dataAdapter = new
            SQLiteDataAdapter(selectCommand, connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridViewCnst.DataSource = ds;
            dataGridViewCnst.DataMember = ds.Tables[0].ToString();
            connect.Close();
            dataGridViewCnst.Columns["ID"].HeaderText = "№";
            dataGridViewCnst.Columns["Mark"].HeaderText = "Марка";
            dataGridViewCnst.Columns["Procent"].HeaderText = "Наценка, %";
        }
        //Валидация
        private bool Validation()
        {
            if (string.IsNullOrEmpty(textBoxMark.Text))
                {
                    MessageBox.Show("Заполните Марку", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (string.IsNullOrEmpty(textBoxMarkUp.Text))
                {
                    MessageBox.Show("Заполните Модель", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (!marks.Contains(textBoxMark.Text))
                {
                    MessageBox.Show("Ошибка! Введа неверная марка авто.");
                    return false;
                }
                Regex regex = new Regex(@"^\d{1,15}?(\,\d\d)?$");
                bool matches = regex.IsMatch(textBoxMarkUp.Text);
                if (!matches)
                {
                    MessageBox.Show("Ошибка! Введёно некорректное число. Проверьте, что десятичная часть указана через запятую, а не через точку");
                    return false;
                }
            return true;
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
        //обновление dataGridView
        private void refreshForm(string ConnectionString, String selectCommand)
        {
            selectTable(ConnectionString, selectCommand);
            dataGridViewCnst.Update();
            dataGridViewCnst.Refresh();
            textBoxMark.Text = "";
            textBoxMarkUp.Text = "";
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
            SQLiteCommand cmd = new SQLiteCommand();
            trans = connect.BeginTransaction();
            cmd.Connection = connect;
            cmd.CommandText = selectCommand;
            cmd.ExecuteNonQuery();
            trans.Commit();
            connect.Close();
        }
        //Добавление Наценки
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            //Валидация          
            if (Validation())
            {
                //MAX ID
                String selectCommand = "select MAX(ID) from Constant";
                object maxValue = selectValue(ConnectionString, selectCommand);
                if (Convert.ToString(maxValue) == "")
                    maxValue = 0;
                //Добавление в бд
                string txtSQLQuery = "insert into Constant (ID, Mark, Procent) values (" +
               (Convert.ToInt32(maxValue) + 1) + ", '" + textBoxMark.Text + "','" + textBoxMarkUp.Text + "')";
                ExecuteQuery(txtSQLQuery);
                //обновление dataGridView
                selectCommand = "select * from Constant";
                refreshForm(ConnectionString, selectCommand);
            }
        }
        //Удаление Наценки
        private void buttonDel_Click(object sender, EventArgs e)
        {
            {
                //выбрана строка CurrentRow
                int CurrentRow = dataGridViewCnst.SelectedCells[0].RowIndex;
                //получить значение ID выбранной строки
                string valueId = dataGridViewCnst[0, CurrentRow].Value.ToString();
                //Удаление из бд
                String selectCommand = "delete from Constant where ID=" + valueId;
                changeValue(ConnectionString, selectCommand);
                //обновление dataGridView
                selectCommand = "select * from Constant";
                refreshForm(ConnectionString, selectCommand);
            }
        }
        //Редактирование Наценки
        private void buttonUpd_Click(object sender, EventArgs e)
        {
            //выбрана строка CurrentRow
            int CurrentRow = dataGridViewCnst.SelectedCells[0].RowIndex;
            string valueId = dataGridViewCnst[0, CurrentRow].Value.ToString();
            Mark = textBoxMark.Text;
            Procent = textBoxMarkUp.Text;
            //Валидация
            if (Validation())
            {
                //Обновление данных в бд
                String selectCommand = "update Constant set Mark='" + Mark + "', Procent='" + Procent + "' where ID = " + valueId;
                changeValue(ConnectionString, selectCommand);
                //обновление dataGridView
                selectCommand = "select * from Constant";
                refreshForm(ConnectionString, selectCommand);
            }
        }
        //Выбор строки dataGridView
        private void dataGridViewCnst_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //выбрана строка CurrentRow
            int CurrentRow = dataGridViewCnst.SelectedCells[0].RowIndex;
            Mark = dataGridViewCnst[1, CurrentRow].Value.ToString();
            Procent = dataGridViewCnst[2, CurrentRow].Value.ToString();
            textBoxMark.Text = Mark;
            textBoxMarkUp.Text = Procent;
        }
        //Валидация ввода процента
        private void textBoxRetail_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 44)
            {
                e.Handled = true;
            }
        }
    }
}
