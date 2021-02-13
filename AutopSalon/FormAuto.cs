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
    public partial class FormAuto : Form
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
        private string selectCommand;
        private string Mark;
        private string Model;
        private string Seria;
        public FormAuto()
        {
            InitializeComponent();
        }
        private void FormAuto_Load(object sender, EventArgs e)
        {
            selectCommand = "Select ID, SeriaName from Seria";
            selectCombo(ConnectionString, selectCommand, comboBoxSeria, "SeriaName", "ID");
            comboBoxSeria.SelectedIndex = -1;
            comboBoxColumn(ConnectionString, selectCommand, "ID", "SeriaName", "SeriaID", "SeriaID");
            
            selectCommand = "Select * from Auto";
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
            dataGridViewAuto.DataSource = ds;
            dataGridViewAuto.DataMember = ds.Tables[0].ToString();
            connect.Close();
            dataGridViewAuto.Columns["ID"].DisplayIndex = 0;
            dataGridViewAuto.Columns["ID"].HeaderText = "№";
            dataGridViewAuto.Columns["Mark"].DisplayIndex = 1;
            dataGridViewAuto.Columns["Mark"].HeaderText = "Марка";
            dataGridViewAuto.Columns["Model"].DisplayIndex = 2;
            dataGridViewAuto.Columns["Model"].HeaderText = "Модель";
            dataGridViewAuto.Columns["SeriaID"].DisplayIndex = 3;
            dataGridViewAuto.Columns["SeriaID"].HeaderText = "Серия";
            dataGridViewAuto.Columns["BuyCost"].DisplayIndex = 4;
            dataGridViewAuto.Columns["BuyCost"].HeaderText = "Закупочная цена";
            dataGridViewAuto.Columns["SaleCost"].DisplayIndex = 5;
            dataGridViewAuto.Columns["SaleCost"].HeaderText = "Розничная цена";
        }
        //Выбор значений в combobox
        public void selectCombo(string ConnectionString, String selectCommand, ComboBox comboBox, string displayMember, string valueMember)
        {
            SQLiteConnection connect = new SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(selectCommand, connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            comboBox.DataSource = ds.Tables[0];
            comboBox.DisplayMember = displayMember;
            comboBox.ValueMember = valueMember;
            connect.Close();
        }
        //Отображение вместо Id
        private void comboBoxColumn(string ConnectionString, String selectCommand, string valueMember, string displayMember, string headerText, string dataPropertyName)
        {
            SQLiteConnection connect = new SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(selectCommand, connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            BindingSource licorgSource = new BindingSource
            {
                DataSource = ds,
                DataMember = ds.Tables[0].ToString()
            };
            connect.Close();
            DataGridViewComboBoxColumn comboColumn = new DataGridViewComboBoxColumn
            {
                DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing,
                FlatStyle = FlatStyle.Popup,
                HeaderText = headerText,
                DataSource = licorgSource,
                DataPropertyName = dataPropertyName,
                Name = dataPropertyName,
                DisplayMember = displayMember,
                ValueMember = valueMember
            };
            dataGridViewAuto.Columns.Add(comboColumn);
        }
        //Валидация
        private bool Validation()
        {
            if (string.IsNullOrEmpty(textBoxMark.Text))
            {
                MessageBox.Show("Заполните Марку", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(textBoxModel.Text))
            {
                MessageBox.Show("Заполните Модель", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(comboBoxSeria.Text))
            {
                MessageBox.Show("Заполните Серию", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!marks.Contains(textBoxMark.Text))
            {
                MessageBox.Show("Ошибка! Введа неверная марка авто.");
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
            dataGridViewAuto.Update();
            dataGridViewAuto.Refresh();
            textBoxMark.Text = "";
            textBoxModel.Text = "";
            comboBoxSeria.SelectedIndex = -1;
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
        //Добавление Автомобиля
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            //Валидация
            if (Validation())
            {
                //MAX ID
                selectCommand = "select MAX(ID) from Auto";
                object maxValue = selectValue(ConnectionString, selectCommand);
                if (Convert.ToString(maxValue) == "")
                    maxValue = 0;
                Mark = textBoxMark.Text;
                Model = textBoxModel.Text;
                Seria = comboBoxSeria.SelectedValue.ToString();
                //Добавление в бд
                string txtSQLQuery = "insert into Auto (ID, Mark, Model, SeriaID) values (" +
                (Convert.ToInt32(maxValue) + 1) + ", '" + textBoxMark.Text + "','" + textBoxModel.Text + "', " + Convert.ToInt32(Seria) + ")";
                ExecuteQuery(txtSQLQuery);
                //обновление dataGridView
                selectCommand = "select * from Auto";
                refreshForm(ConnectionString, selectCommand);
            }
        }
        //Удаление автомобиля
        private void buttonDel_Click(object sender, EventArgs e)
        {
            {
                //выбрана строка CurrentRow
                int CurrentRow = dataGridViewAuto.SelectedCells[0].RowIndex;
                //получить значение ID выбранной строки
                string valueId = dataGridViewAuto[0, CurrentRow].Value.ToString();
                //Удаление из бд
                selectCommand = "delete from Auto where ID=" + valueId;
                changeValue(ConnectionString, selectCommand);
                //обновление dataGridView
                selectCommand = "select * from Auto";
                refreshForm(ConnectionString, selectCommand);
            }
        }
        //Выбор строки dataGridView
        private void dataGridViewAuto_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //выбрана строка CurrentRow
            int CurrentRow = dataGridViewAuto.SelectedCells[0].RowIndex;
            Mark = dataGridViewAuto[1, CurrentRow].Value.ToString();
            Model = dataGridViewAuto[2, CurrentRow].Value.ToString();
            Seria = dataGridViewAuto[3, CurrentRow].Value.ToString();
            textBoxMark.Text = Mark;
            textBoxModel.Text = Model;
            comboBoxSeria.SelectedValue = Convert.ToInt32(Seria);
        }
        //Редактирование
        private void buttonUpd_Click(object sender, EventArgs e)
        {
            //выбрана строка CurrentRow
            int CurrentRow = dataGridViewAuto.SelectedCells[0].RowIndex;
            string valueId = dataGridViewAuto[0, CurrentRow].Value.ToString();
            Mark = textBoxMark.Text;
            Model = textBoxModel.Text;
            Seria = comboBoxSeria.SelectedValue.ToString();
            //Валидация
            if (Validation())
            {
                //Обновление данных в бд
                selectCommand = "update Auto set Mark='" + Mark + "', Model='" + Model + "', SeriaID=" + Seria + " where ID = " + valueId;
                changeValue(ConnectionString, selectCommand);
                //обновление dataGridView
                selectCommand = "select * from Auto";
                refreshForm(ConnectionString, selectCommand);
            }
        }
        private void dataGridViewAuto_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewAuto.Columns[e.ColumnIndex].Name == "BuyCost" ||
                dataGridViewAuto.Columns[e.ColumnIndex].Name == "SaleCost")
            {
                SumFormat(e);
            }
        }
        private static void SumFormat(DataGridViewCellFormattingEventArgs formatting)
        {
            if ((formatting.Value) != DBNull.Value)
            {
                try
                {                 
                    String sumFormat = Convert.ToDecimal(formatting.Value).ToString("C2");
                    formatting.Value = sumFormat;
                    formatting.FormattingApplied = true;
                }
                catch (FormatException)
                {                  
                    formatting.FormattingApplied = false;
                }
            }
        }
    }
}
