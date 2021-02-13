using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutopSalon
{
    public partial class FormSeria : Form
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private static string sPath = Path.Combine(Application.StartupPath, "D:\\Users\\iliya\\Документы\\Политех\\3 курс\\1 семестр\\AutoSalonRight.db");
        private string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
        private string Seria;
        private string selectCommand;
        public FormSeria()
        {
            InitializeComponent();
        }
        private void FormSeria_Load(object sender, EventArgs e)
        {
            selectCommand = "Select * from Seria";
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
            dataGridViewSeria.DataSource = ds;
            dataGridViewSeria.DataMember = ds.Tables[0].ToString();
            connect.Close();
            dataGridViewSeria.Columns["ID"].HeaderText = "№";
            dataGridViewSeria.Columns["SeriaName"].HeaderText = "Серия";
        }
        //Валидация
        private bool Validation()
        {
            if (string.IsNullOrEmpty(textBoxSeria.Text))
            {
                MessageBox.Show("Заполните Марку", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            dataGridViewSeria.Update();
            dataGridViewSeria.Refresh();
            textBoxSeria.Text = "";
        }
        private object selectValue(string ConnectionString, String selectCommand)
        {
            SQLiteConnection connect = new SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteCommand command = new SQLiteCommand(selectCommand,
            connect);
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
        //Добавление Серии
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            //Валидация
            if (Validation())
            {
                //MAX ID
                String selectCommand = "select MAX(ID) from Seria";
                object maxValue = selectValue(ConnectionString, selectCommand);
                if (Convert.ToString(maxValue) == "")
                    maxValue = 0;
                //Добавление в бд
                string txtSQLQuery = "insert into Seria (ID, SeriaName) values (" + (Convert.ToInt32(maxValue) + 1) + ", '" + textBoxSeria.Text + "')";
                ExecuteQuery(txtSQLQuery);
                //обновление dataGridView
                selectCommand = "select * from Seria";
                refreshForm(ConnectionString, selectCommand);
            }
        }
        //Удаление Серии
        private void buttonDel_Click(object sender, EventArgs e)
        {
            {
                //выбрана строка CurrentRow
                int CurrentRow = dataGridViewSeria.SelectedCells[0].RowIndex;
                //получить значение ID выбранной строки
                string valueId = dataGridViewSeria[0, CurrentRow].Value.ToString();
                //Удаление из бд
                selectCommand = "delete from Seria where ID=" + valueId;
                changeValue(ConnectionString, selectCommand);
                //обновление dataGridView
                selectCommand = "select * from Seria";
                refreshForm(ConnectionString, selectCommand);
            }
        }
        //Редактирование Серии
        private void buttonUpd_Click(object sender, EventArgs e)
        {
            //выбрана строка CurrentRow
            int CurrentRow = dataGridViewSeria.SelectedCells[0].RowIndex;
            string valueId = dataGridViewSeria[0, CurrentRow].Value.ToString();
            Seria = textBoxSeria.Text;
            //Валидация
           // if (Validation())
            //{
                //Обновление данных в бд
                selectCommand = "update Seria set SeriaName='" + Seria + "' where ID = " + valueId;
                changeValue(ConnectionString, selectCommand);
                //обновление dataGridView
                selectCommand = "select * from Seria";
                refreshForm(ConnectionString, selectCommand);
            //}
        }
        //Выбор строки dataGridView
        private void dataGridViewSeria_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //выбрана строка CurrentRow
            int CurrentRow = dataGridViewSeria.SelectedCells[0].RowIndex;
            Seria = dataGridViewSeria[1, CurrentRow].Value.ToString();
            textBoxSeria.Text = Seria;
        }
    }
}
