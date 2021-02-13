using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutopSalon
{
    public partial class FormUnit : Form
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private static string sPath = "D:\\Users\\iliya\\Документы\\Политех\\3 курс\\1 семестр\\AutoSalonRight.db";
        private string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
        public FormUnit()
        {
            InitializeComponent();
        }
        private void FormUnit_Load(object sender, EventArgs e)
        {               
            String selectAccount = "Select ID, AccountNumber from ChartOfAccounts Where AccountNumber <30";
            selectCombo(ConnectionString, selectAccount, comboBoxAccount, "AccountNumber", "ID");
            comboBoxAccount.SelectedIndex = -1;
            comboBoxColumn(ConnectionString, selectAccount, "ID", "AccountNumber", "ChartID", "ChartID");
            
            String selectCommand = "Select * from Unit";
            selectTable(ConnectionString, selectCommand);
        }
        //Замена ID на название
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
            dataGridView1.Columns.Add(comboColumn);
        }
        //Выбор данных для таблицы
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
            dataGridView1.ColumnHeadersVisible = true;
            dataGridView1.Columns["ID"].DisplayIndex = 0;
            dataGridView1.Columns["Name"].DisplayIndex = 1;          ;
            dataGridView1.Columns["ChartID"].DisplayIndex = 2;
        }
        //Выбор дання для comboboxa
        private void selectCombo(string ConnectionString, String selectCommand, ComboBox comboBoxAccount, string displayMember, string valueMember)
        {
            SQLiteConnection connect = new SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(selectCommand, connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            comboBoxAccount.DataSource = ds.Tables[0];
            comboBoxAccount.DisplayMember = displayMember;
            comboBoxAccount.ValueMember = valueMember;
            connect.Close();
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
            textBoxName.Text = "";
            comboBoxAccount.SelectedIndex = -1;
        }
        //Валидация
        private void Validation()
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(comboBoxAccount.Text))
            {
                MessageBox.Show("Выберите счет", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
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
        //Добавление подразделения
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Validation();
            //MAX ID
            String selectCommand = "select MAX(ID) from Unit";
            object maxValue = selectValue(ConnectionString, selectCommand);
            if (Convert.ToString(maxValue) == "")
                maxValue = 0;
            //Добавление поставщика в бд
            string valueAccount = comboBoxAccount.SelectedValue.ToString();
            string txtSQLQuery = "insert into Unit (ID,Name, ChartID ) values (" +
           (Convert.ToInt32(maxValue) + 1) + ", '" + textBoxName.Text + "'," + Convert.ToInt32(valueAccount) + ")";
            ExecuteQuery(txtSQLQuery);
            //Обновление dataGridView
            selectCommand = "select * from Unit";
            refreshForm(ConnectionString, selectCommand);
        }
        //Удаление подразделения
        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int CurrentRow = dataGridView1.SelectedCells[0].RowIndex;
                string valueId = dataGridView1[0, CurrentRow].Value.ToString();
                //Удаление подразделения из бд
                String selectCommand = "delete from Unit where ID=" + valueId;
                changeValue(ConnectionString, selectCommand);
                //Обновление dataGridView
                selectCommand = "select * from Unit";
                refreshForm(ConnectionString, selectCommand);     
            }
        }
        //Редактирование подразделения
        private void buttonUpd_Click(object sender, EventArgs e)
        {
            Validation();
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int CurrentRow = dataGridView1.SelectedCells[0].RowIndex;
                string valueId = dataGridView1[0, CurrentRow].Value.ToString();
                string changeType = textBoxName.Text;
                //Обновление подразделения в бд
                string changeAccount = comboBoxAccount.SelectedValue.ToString();
                String selectCommand = "update Unit set Name='" + changeType + "',ChartID=" + Convert.ToInt32(changeAccount) + " where ID = " + valueId;
                changeValue(ConnectionString, selectCommand);
                //Обновление dataGridView
                selectCommand = "select * from Unit";
                refreshForm(ConnectionString, selectCommand);
            }
        }
        private void dataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int CurrentRow = dataGridView1.SelectedCells[0].RowIndex;
                string nameId = dataGridView1[1, CurrentRow].Value.ToString();
                textBoxName.Text = nameId;
                string codid = dataGridView1[2, CurrentRow].Value.ToString();
                comboBoxAccount.SelectedValue = Convert.ToInt32(codid);
            }
        }
    }
}