using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutopSalon
{
    public partial class FormService : Form
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private static string sPath = "D:\\Users\\iliya\\Документы\\Политех\\3 курс\\1 семестр\\AutoSalonRight.db";
        private string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
        public FormService()
        {
            InitializeComponent();
        }
        private void FormService_Load(object sender, EventArgs e)
        {          
            String selectUnit = "Select ID, Name from Unit ";
            selectCombo(ConnectionString, selectUnit, comboBoxUnit, "Name", "ID");
            comboBoxUnit.SelectedIndex = -1;
            comboBoxColumn(ConnectionString, selectUnit, "ID", "Name", "UnitID", "UnitID");
            
            String selectCommand = "Select * from Service";
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
        //Выбор данных таблицы
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
            dataGridView1.Columns["ServiceName"].DisplayIndex = 1;
            dataGridView1.Columns["BuyCost"].DisplayIndex = 2;
            dataGridView1.Columns["SaleCost"].DisplayIndex = 3;
            dataGridView1.Columns["UnitID"].DisplayIndex = 4;
        }
        //Выбор данных comboboxа
        private void selectCombo(string ConnectionString, String selectCommand, ComboBox comboBoxUnit, string displayMember, string valueMember)
        {
            SQLiteConnection connect = new SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(selectCommand, connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            comboBoxUnit.DataSource = ds.Tables[0];
            comboBoxUnit.DisplayMember = displayMember;
            comboBoxUnit.ValueMember = valueMember;
            connect.Close();
        }
        //Валидация
        private void Validation()
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxBuyCost.Text))
            {
                MessageBox.Show("Заполните стоимость услуги", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(comboBoxUnit.Text))
            {
                MessageBox.Show("Выберите подразделение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Regex regex = new Regex(@"^\d{1,15}?(\,\d\d)?$");
            bool matches = regex.IsMatch(textBoxBuyCost.Text);
            if (!matches)
            {
                MessageBox.Show("Ошибка! Введа некорректная сумма. Проверьте,что десятичная часть указана через запятую,а не через точку");
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
            textBoxName.Text = "";
            textBoxBuyCost.Text = "";
            comboBoxUnit.SelectedIndex = -1;
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
        //Добавление услуги
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            //Валидация
            Validation();          
            String selectCommand = "select MAX(ID) from Service";
            object maxValue = selectValue(ConnectionString, selectCommand);
            if (Convert.ToString(maxValue) == "")
                maxValue = 0;
            //Добавление услуги в бд
            string valueUnit = comboBoxUnit.SelectedValue.ToString();
            string txtSQLQuery = "insert into Service (ID, ServiceName, BuyCost, SaleCost, UnitID ) values (" +
           (Convert.ToInt32(maxValue) + 1) + ", '" + textBoxName.Text + "', '" + Convert.ToDecimal(textBoxBuyCost.Text).ToString("0.##") + "','" + Convert.ToDecimal(textBoxSaleCost.Text).ToString("0.##") + "','" + Convert.ToInt32(valueUnit) + "')";
            ExecuteQuery(txtSQLQuery);
            //Обновление dataGridView
            selectCommand = "select * from Service";
            refreshForm(ConnectionString, selectCommand);          
        }             
        //Удаление услуги
        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int CurrentRow = dataGridView1.SelectedCells[0].RowIndex;
                string valueId = dataGridView1[0, CurrentRow].Value.ToString();
                //Удаление услуги из бд
                String selectCommand = "delete from Service where ID=" + valueId;
                changeValue(ConnectionString, selectCommand);
                //Обновление dataGridView
                selectCommand = "select * from Service";
                refreshForm(ConnectionString, selectCommand);
            }
        }  
        //Изменение услуги
        private void buttonUpd_Click(object sender, EventArgs e)
        {
            Validation();
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int CurrentRow = dataGridView1.SelectedCells[0].RowIndex;
                string valueId = dataGridView1[0, CurrentRow].Value.ToString();
                string changeName = textBoxName.Text;
                string changeCost = textBoxBuyCost.Text;
                string changeUnit = comboBoxUnit.SelectedValue.ToString();
                //Обновление услуги в бд
                String selectCommand = "update Service set ServiceName='" + changeName + "',Cost='" + Convert.ToDecimal(changeCost) + "',UnitID=" + Convert.ToInt32(changeUnit) + " where ID = " + valueId;
                changeValue(ConnectionString, selectCommand);
                //Обновление dataGridView
                selectCommand = "select * from Service";
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
                string cost = dataGridView1[2, CurrentRow].Value.ToString();
                textBoxBuyCost.Text = cost;
                string unit = dataGridView1[3, CurrentRow].Value.ToString();
                comboBoxUnit.SelectedValue = Convert.ToInt32(unit);
            }
        }
    }
}