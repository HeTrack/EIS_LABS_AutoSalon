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
    public partial class FormApplication : Form
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private static string sPath = Path.Combine(Application.StartupPath, "D:\\Users\\iliya\\Документы\\Политех\\3 курс\\1 семестр\\AutoSalonRight.db");
        public string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
        private string ApplicationID;
        private string selectCommand;
        private string Date;
        private string Contragent;
        private string Auto;
        private string Seria;
        private string CountAuto;
        private string Service;
        private string CountService;
        public FormApplication()
        {
            InitializeComponent();
        }
        private void FormApplication_Load(object sender, EventArgs e)
        {
            ApplicationID = "";

            string selectContragent = "Select ID, FIO from Contragent";
            selectCombo(ConnectionString, selectContragent, comboBoxContragent, "FIO", "ID");
            comboBoxColumn(ConnectionString, selectContragent, "ID", "FIO", "ContragentID", "ContragentID");
            comboBoxContragent.SelectedIndex = -1;
           
            string selectSeria = "Select ID, SeriaName from Seria";
            selectCombo(ConnectionString, selectSeria, comboBoxSeria, "SeriaName", "ID");
            comboBoxColumn(ConnectionString, selectSeria, "ID", "SeriaName", "SeriaID", "SeriaID");
            comboBoxSeria.SelectedIndex = -1;

            string selectService = "Select ID, ServiceName from Service";
            selectCombo(ConnectionString, selectService, comboBoxService, "ServiceName", "ID");
            comboBoxColumn(ConnectionString, selectService, "ID", "ServiceName", "ServiceID", "ServiceID");
            comboBoxService.SelectedIndex = -1;

            string selectAuto = "Select ID, Model from Auto";
            comboBoxColumn(ConnectionString, selectAuto, "ID", "Model", "AutoID", "AutoID");

            string selectCommand = "Select * from Application";
            selectTable(ConnectionString, selectCommand);
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
            dataGridViewApp.Columns.Add(comboColumn);
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
        public void selectTable(string ConnectionString, String selectCommand)
        {
            SQLiteConnection connect = new SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(selectCommand, connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridViewApp.DataSource = ds;
            dataGridViewApp.DataMember = ds.Tables[0].ToString();
            connect.Close();
            dataGridViewApp.ColumnHeadersVisible = true;
            dataGridViewApp.Columns["ID"].DisplayIndex = 0;
            dataGridViewApp.Columns["ID"].HeaderText = "№";
            dataGridViewApp.Columns["Date"].DisplayIndex = 1;
            dataGridViewApp.Columns["Date"].HeaderText = "Дата";
            dataGridViewApp.Columns["Number"].DisplayIndex = 2;
            dataGridViewApp.Columns["Number"].HeaderText = "Номер Заявки";
            dataGridViewApp.Columns["ContragentID"].DisplayIndex = 3;
            dataGridViewApp.Columns["ContragentID"].HeaderText = "Контрагент";
            dataGridViewApp.Columns["AutoID"].DisplayIndex = 4;
            dataGridViewApp.Columns["AutoID"].HeaderText = "Автомобиль";
            dataGridViewApp.Columns["SeriaID"].DisplayIndex = 5;
            dataGridViewApp.Columns["SeriaID"].HeaderText = "Серия";
            dataGridViewApp.Columns["CountAuto"].DisplayIndex = 6;
            dataGridViewApp.Columns["CountAuto"].HeaderText = "Количество Авто";
            dataGridViewApp.Columns["ServiceID"].DisplayIndex = 7;
            dataGridViewApp.Columns["ServiceID"].HeaderText = "Дополнительные услуги";
            dataGridViewApp.Columns["CountService"].DisplayIndex = 8;
            dataGridViewApp.Columns["CountService"].HeaderText = "Количество услуг";
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
        public void refreshForm(string ConnectionString, String selectCommand)
        {
            selectTable(ConnectionString, selectCommand);
            dataGridViewApp.Update();
            dataGridViewApp.Refresh();        
            textBoxCountAuto.Text = "";
            textBoxCountService.Text = "";
            comboBoxAuto.SelectedIndex = -1;
            comboBoxSeria.SelectedIndex = -1;
            comboBoxContragent.SelectedIndex = -1;
            comboBoxService.SelectedIndex = -1;
        }
        public object selectValue(string ConnectionString, String selectCommand)
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
        public void changeValue(string ConnectionString, String selectCommand)
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
        private bool Validation()
        {        
            if (string.IsNullOrEmpty(comboBoxContragent.Text))
            {
                MessageBox.Show("Выберите контрагента", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(comboBoxSeria.Text))
            {
                MessageBox.Show("Выберите серию авто", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(comboBoxAuto.Text))
            {
                MessageBox.Show("Выберите модель авто", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }            
            if (string.IsNullOrEmpty(textBoxCountAuto.Text))
            {
                MessageBox.Show("Укажите количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!string.IsNullOrEmpty(comboBoxService.Text) && string.IsNullOrEmpty(textBoxCountService.Text))
            {              
                 MessageBox.Show("Укажите количество услуг", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 return false;             
            }
            if (string.IsNullOrEmpty(comboBoxService.Text) && !string.IsNullOrEmpty(textBoxCountService.Text))
            {
                MessageBox.Show("Выберите дополнительную услугу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
 
            return true;
        }
        private void dataGridViewApp_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridViewApp.SelectedRows.Count > 0)
            {
                int CurrentRow = dataGridViewApp.SelectedCells[0].RowIndex;
                Date = dataGridViewApp[1, CurrentRow].Value.ToString();
                Contragent = dataGridViewApp[3, CurrentRow].Value.ToString();
                Seria = dataGridViewApp[4, CurrentRow].Value.ToString();
                Auto = dataGridViewApp[5, CurrentRow].Value.ToString();
                CountAuto = dataGridViewApp[6, CurrentRow].Value.ToString();
                Service = dataGridViewApp[7, CurrentRow].Value.ToString();
                CountService = dataGridViewApp[8, CurrentRow].Value.ToString();

                dateTimePicker.Text = Date;
                comboBoxContragent.SelectedValue = Contragent;
                comboBoxSeria.SelectedValue = Seria;
                selectCommand = "select ID, Model from Auto where SeriaID = " + Convert.ToInt32(comboBoxSeria.SelectedValue);
                selectCombo(ConnectionString, selectCommand, comboBoxAuto, "Model", "ID");
                comboBoxAuto.SelectedValue = Auto;
                textBoxCountAuto.Text = CountAuto;
                if (Service != "")
                {
                    comboBoxService.SelectedValue = Service;
                    textBoxCountService.Text = CountService;
                }
            }
        }
        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            //Валидация
            if (Validation())
            {
                //Max ID
                selectCommand = "select MAX(ID) from Application";
                object maxValue = selectValue(ConnectionString, selectCommand);
                if (Convert.ToString(maxValue) == "")
                    maxValue = 0;
                Date = dateTimePicker.Value.ToString("yyyy-MM-dd");
                string nulik = "0000000";
                string numberApp = "ЗВ" + nulik.Substring(0, 7 - (Convert.ToInt32(maxValue) + 1).ToString().Length) + (Convert.ToInt32(maxValue) + 1).ToString();
                Contragent = comboBoxContragent.SelectedValue.ToString();
                Auto = comboBoxAuto.SelectedValue.ToString();
                Seria = comboBoxSeria.SelectedValue.ToString();
                CountAuto = textBoxCountAuto.Text;
                //Добавление в бд заявки
                string txtSQLQuery = "insert into Application (ID, Date, Number, ContragentID, AutoID, SeriaID, CountAuto) values (" +
                (Convert.ToInt32(maxValue) + 1) + ", '" + Date + "', '" + numberApp + "', '" + Contragent + "', " + Auto + ", " +
                Seria + ", " + CountAuto + " )";
                ExecuteQuery(txtSQLQuery);
                if (comboBoxService.SelectedIndex != -1)
                {
                    Service = comboBoxService.SelectedValue.ToString();
                    CountService = textBoxCountService.Text;
                    selectCommand = "update Application set ServiceID='" + Service + "', CountService='" + CountService + "' where ID = " + (Convert.ToInt32(maxValue) + 1);
                    changeValue(ConnectionString, selectCommand);
                }      
                
                //Обновление dataGridView
                selectCommand = "select * from Application";
                refreshForm(ConnectionString, selectCommand);
            }
        }
        private void toolStripButtonUpd_Click(object sender, EventArgs e)
        {
            //Валидация
            if (Validation())
            {
                if (dataGridViewApp.SelectedRows.Count > 0)
                {
                    int CurrentRow = dataGridViewApp.SelectedCells[0].RowIndex;
                    string valueId = dataGridViewApp[0, CurrentRow].Value.ToString();
                    Date = dateTimePicker.Value.ToString("yyyy-MM-dd");
                    Contragent = comboBoxContragent.SelectedValue.ToString();
                    Auto = comboBoxAuto.SelectedValue.ToString();
                    Seria = comboBoxSeria.SelectedValue.ToString();
                    CountAuto = textBoxCountAuto.Text;
                    Service = "";
                    CountService = "";
                    if (comboBoxService.SelectedIndex != -1)
                    {
                        Service = comboBoxService.SelectedValue.ToString();
                        CountService = textBoxCountService.Text;
                    }
                    //Обновление данных в бд
                    selectCommand = "update Application set Date='" + Date + "', ContragentID='" + Contragent + "', AutoID=" + Auto + ", SeriaID=" +
                    Seria + ", CountAuto=" + CountAuto + ", ServiceID='" + Service + "', CountService='" + CountService + "' where ID = " + valueId;
                    changeValue(ConnectionString, selectCommand);
                    //Обновление dataGridView
                    selectCommand = "select * from Application";
                    refreshForm(ConnectionString, selectCommand);
                }
            }
        }
        //Удаление заявки
        private void toolStripButtonDel_Click(object sender, EventArgs e)
        {
            if (dataGridViewApp.SelectedRows.Count > 0)
            {
                int CurrentRow = dataGridViewApp.SelectedCells[0].RowIndex;
                string valueId = dataGridViewApp[0, CurrentRow].Value.ToString();
                //Удаление из бд
                selectCommand = "delete from Application where ID=" + valueId;
                changeValue(ConnectionString, selectCommand);
                //Обновление dataGridView
                selectCommand = "select * from Application";
                refreshForm(ConnectionString, selectCommand);
            }
        }
        private void ButtonVibor_Click(object sender, EventArgs e)
        {
            if (dataGridViewApp.SelectedRows.Count > 0)
            {
                int CurrentRow = dataGridViewApp.SelectedCells[0].RowIndex;
                DialogResult result = MessageBox.Show("Вы уверены", "Выбор заявки", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    ApplicationID = dataGridViewApp[0, CurrentRow].Value.ToString();         
                    Close();                 
                }
            }
            else
            {             
                MessageBox.Show("Выберите строку", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }  
        public string getID()
        {
            return ApplicationID;
        }
        private void comboBoxSeria_SelectionChangeCommitted(object sender, EventArgs e)
        {
            selectCommand = "select ID, Model from Auto where SeriaID = " + Convert.ToInt32(comboBoxSeria.SelectedValue);
            selectCombo(ConnectionString, selectCommand, comboBoxAuto, "Model", "ID");
            comboBoxAuto.SelectedIndex = -1;
        }
        private void dataGridViewApp_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewApp.Columns[e.ColumnIndex].Name == "Date")
            {
                DateFormat(e);
            }           
        }
        private static void DateFormat(DataGridViewCellFormattingEventArgs formatting)
        {
            if (formatting.Value != null)
            {
                try
                {
                    DateTime theDate = DateTime.Parse(formatting.Value.ToString());
                    String dateString = theDate.ToString("D");
                    formatting.Value = dateString;
                    formatting.FormattingApplied = true;
                }
                catch (FormatException)
                {
                    // Set to false in case there are other handlers interested trying to
                    // format this DataGridViewCellFormattingEventArgs instance.
                    formatting.FormattingApplied = false;
                }
            }
        }
    }
}