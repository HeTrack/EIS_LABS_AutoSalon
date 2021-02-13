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
    public partial class FormSale : Form
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private static string sPath = Path.Combine(Application.StartupPath, "D:\\Users\\iliya\\Документы\\Политех\\3 курс\\1 семестр\\AutoSalonRight.db");
        public string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
        private string selectCommand;
        private string CodeOperation;
        private string Date;
        private string Worker;
        private string RequestID;
        private string AutoSum;
        private string ServiceSum;
        private string Summa;
        public FormSale()
        {
            InitializeComponent();
        }
        private void FormSale_Load(object sender, EventArgs e)
        {
            String selectRequest = "Select ID, Number from Application";
            selectCombo(ConnectionString, selectRequest, comboBoxRequest, "Number", "ID");
            comboBoxColumn(ConnectionString, selectRequest, "ID", "Number", "RequestID", "RequestID");
            comboBoxRequest.SelectedIndex = -1;
            selectCommand = "Select ID, CodeOperation, Date, Worker, RequestID, (ServiceSum + AutoSum) AS Summa FROM Sale";
            selectTable(ConnectionString, selectCommand);
        }
        //Выбор данных для таблицы
        public void selectTable(string ConnectionString, String selectCommand)
        {
            SQLiteConnection connect = new SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(selectCommand, connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridViewSale.DataSource = ds;
            dataGridViewSale.DataMember = ds.Tables[0].ToString();
            connect.Close();
            dataGridViewSale.ColumnHeadersVisible = true;
            dataGridViewSale.Columns["ID"].DisplayIndex = 0;
            dataGridViewSale.Columns["ID"].HeaderText = "№";
            dataGridViewSale.Columns["CodeOperation"].DisplayIndex = 1;
            dataGridViewSale.Columns["CodeOperation"].HeaderText = "Код Операции";
            dataGridViewSale.Columns["Date"].DisplayIndex = 2;
            dataGridViewSale.Columns["Date"].HeaderText = "Дата";
            dataGridViewSale.Columns["Worker"].DisplayIndex = 3;
            dataGridViewSale.Columns["Worker"].HeaderText = "Сотрудник";
            dataGridViewSale.Columns["RequestID"].DisplayIndex = 4;
            dataGridViewSale.Columns["RequestID"].HeaderText = "№ Заявки";
            dataGridViewSale.Columns["Summa"].DisplayIndex = 5;
            dataGridViewSale.Columns["Summa"].HeaderText = "Сумма";
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
        //Отображение вместо ID
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
            dataGridViewSale.Columns.Add(comboColumn);
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
        public void refreshForm()
        {
            selectCommand = "Select ID, CodeOperation, Date, Worker, RequestID, (ServiceSum + AutoSum) AS Summa FROM Sale";
            selectTable(ConnectionString, selectCommand);
            dataGridViewSale.Update();
            dataGridViewSale.Refresh();
            dateTimePicker.Text = "";
            textBoxWorker.Text = "";
            comboBoxRequest.Text = "";
            textBoxSum.Text = "";
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
            SQLiteCommand cmd = new SQLiteCommand();
            trans = connect.BeginTransaction();
            cmd.Connection = connect;
            cmd.CommandText = selectCommand;
            cmd.ExecuteNonQuery();
            trans.Commit();
            connect.Close();
        }
        //валидация полей
        private bool Validation()
        {
            if (string.IsNullOrEmpty(dateTimePicker.Text))
            {
                MessageBox.Show("Выберите дату", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(textBoxWorker.Text))
            {
                MessageBox.Show("Заполните ФИО сотрудника", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(comboBoxRequest.Text))
            {
                MessageBox.Show("Выберите заявку", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(textBoxSum.Text))
            {
                MessageBox.Show("Измените заявку или выберите другую");
                 return false;
            }
            return true;
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            //Валидация
            if (Validation())
            {
                //Max ID
                selectCommand = "select MAX(ID) from Sale";
                object maxValue = selectValue(ConnectionString, selectCommand);
                if (Convert.ToString(maxValue) == "")
                    maxValue = 0;
                //CodeOperation
                string nulik = "0000000";
                CodeOperation = "ПРЖ" + nulik.Substring(0, 7 - Convert.ToInt32(maxValue).ToString().Length) + (Convert.ToInt32(maxValue) + 1).ToString();
                Date = dateTimePicker.Value.ToString("yyyy-MM-dd");
                Worker = textBoxWorker.Text;
                RequestID = comboBoxRequest.SelectedValue.ToString();
                Summa = textBoxSum.Text;
                //Журнал продаж
                string txtSQLQuery = "insert into Sale (ID, CodeOperation, Date, Worker, RequestID, ServiceSum, AutoSum) values (" +
               (Convert.ToInt32(maxValue) + 1) + ", '" + CodeOperation + "', '" + Date + "', '" + Worker + "', " + RequestID + ", '" +
               Convert.ToDecimal(ServiceSum).ToString("0.##") + "', '" + Convert.ToDecimal(AutoSum).ToString("0.##") + "')";
                ExecuteQuery(txtSQLQuery);

                //Добавление в Журнал Операций
                selectCommand = "select MAX(ID) from JournalOper";
                maxValue = selectValue(ConnectionString, selectCommand);
                if (Convert.ToString(maxValue) == "")
                    maxValue = 0;
                txtSQLQuery = "insert into JournalOper (ID, Date, CodeOperation, OperationName, Summa) values (" +
              (Convert.ToInt32(maxValue) + 1) + ", '" + Date + "','" + CodeOperation + "',  '" + "Продажа авто" + "',  '" +
              Convert.ToDecimal(Summa).ToString("0.##") + "')";
                ExecuteQuery(txtSQLQuery);

                //Добавление в Журнал проводок
                AddSaleProvodki();
                AddKassa(comboBoxRequest.Text, Summa);
                
                //Обновить dataGridView            
                refreshForm();
            }
        }
        private void заякиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormApplication app = new FormApplication();
            app.ShowDialog();
        }
        //Выбор заявки
        private void buttonPick_Click(object sender, EventArgs e)
        {
            FormApplication app = new FormApplication();
            app.toolStrip1.Visible = false;
            app.dateTimePicker.Enabled = false;
            app.comboBoxSeria.Enabled = false;
            app.comboBoxAuto.Enabled = false;
            app.comboBoxContragent.Enabled = false;
            app.textBoxCountAuto.ReadOnly = true;
            app.ShowDialog();
            RequestID = app.getID();
            if (RequestID != "")
            {            
                //Остаток
                selectCommand = "select AutoID from Application where ID='" + RequestID + "'";
                string AutoID = selectValue(ConnectionString, selectCommand).ToString();
                selectCommand = "select SUM(Count) from Entrance where date(Date) <= date('" + dateTimePicker.Value.ToString("yyyy-MM-dd") + "') AND AutoID ='" + AutoID + "'";
                string AutoEntrance = selectValue(ConnectionString, selectCommand).ToString();
                if (AutoEntrance == "")
                {
                    AutoEntrance = "0";
                }
                selectCommand = "select SUM(a.CountAuto) from Sale s JOIN Application a ON s.RequestID = a.ID where date(s.Date) <= date('" + dateTimePicker.Value.ToString("yyyy-MM-dd") + "') AND AutoID ='" + AutoID + "'";
                string AutoSale = selectValue(ConnectionString, selectCommand).ToString();
                if (AutoSale == "")
                {
                    AutoSale = "0";
                }
                int Ostatok = Convert.ToInt32(AutoEntrance) - Convert.ToInt32(AutoSale);
                selectCommand = "select CountAuto from Application where ID='" + RequestID + "'";
                string CountAuto = selectValue(ConnectionString, selectCommand).ToString();
                selectCommand = "select SaleCost from Service where ID = (select ServiceID from Application where ID = '" + RequestID + "')";
                if (Ostatok < Convert.ToInt32(CountAuto))
                {
                    MessageBox.Show("Остаток данной серии автомобилей составляет " + Ostatok + ". Продажа невозможна", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    comboBoxRequest.SelectedValue = RequestID;
                    selectCommand = "select SaleCost from Auto where ID = (select AutoID from Application where ID = '" + RequestID + "')";
                    AutoSum = selectValue(ConnectionString, selectCommand).ToString();
                    selectCommand = "select SaleCost from Service where ID = (select ServiceID from Application where ID = '" + RequestID + "')";
                    ServiceSum = selectValue(ConnectionString, selectCommand).ToString();
                    string CountService;
                    if (ServiceSum == "")
                    {
                        ServiceSum = "0";
                        CountService = "0";
                    }
                    else
                    {
                        selectCommand = "select CountService from Application where ID = '" + RequestID + "'";
                        CountService = selectValue(ConnectionString, selectCommand).ToString();
                    }
                    AutoSum = (Convert.ToDecimal(AutoSum) * Convert.ToDecimal(CountAuto)).ToString();
                    ServiceSum = (Convert.ToDecimal(ServiceSum) * Convert.ToDecimal(CountService)).ToString();
                    textBoxSum.Text = (Convert.ToDecimal(AutoSum) + Convert.ToDecimal(ServiceSum)).ToString();
                }
            }
        }
        private void dataGridViewSale_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //выбрана строка CurrentRow
            if (dataGridViewSale.SelectedRows.Count > 0)
            {
                int CurrentRow = dataGridViewSale.SelectedCells[0].RowIndex;
                CodeOperation = dataGridViewSale[1, CurrentRow].Value.ToString();
                Date = dataGridViewSale[2, CurrentRow].Value.ToString();
                Worker = dataGridViewSale[3, CurrentRow].Value.ToString();
                RequestID = dataGridViewSale[4, CurrentRow].Value.ToString();
                Summa = dataGridViewSale[5, CurrentRow].Value.ToString();
                dateTimePicker.Text = Date;
                textBoxWorker.Text = Worker;
                comboBoxRequest.SelectedValue = RequestID;
                textBoxSum.Text = Summa;
            }
        }
        private void buttonUpd_Click(object sender, EventArgs e)
        {
            int CurrentRow = dataGridViewSale.SelectedCells[0].RowIndex;
            string valueId = dataGridViewSale[0, CurrentRow].Value.ToString();
            Date = dateTimePicker.Value.ToString("yyyy-MM-dd");
            Worker = textBoxWorker.Text;
            RequestID = comboBoxRequest.SelectedValue.ToString();
            Summa = textBoxSum.Text;
            //Валидация
            if (Validation())
            {
                //Журнал продаж
                selectCommand = "update Sale set Date='" + Date + "', Worker='" + Worker + "', RequestID='" + RequestID + "', Summa='" +
                     Convert.ToDecimal(Summa).ToString("0.##") + "' where ID = " + valueId;
                changeValue(ConnectionString, selectCommand);

                //Журнал Операций
                selectCommand = "update JournalOper set Date='" + Date + "', Summa='" + Convert.ToDecimal(Summa).ToString("0.##");
                changeValue(ConnectionString, selectCommand);

                //Журнал проводок
               //1. Удаление текущих проводок
                selectCommand = "delete from Provodki where OperationID='" + CodeOperation + "'";
                changeValue(ConnectionString, selectCommand);
                //2. Добавление в Журнал проводок изм. проводок
                AddSaleProvodki();
                AddKassa(comboBoxRequest.Text, Summa);
                refreshForm();
            }
        }
        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridViewSale.SelectedRows.Count > 0)
            {
                int CurrentRow = dataGridViewSale.SelectedCells[0].RowIndex;
                string valueId = dataGridViewSale[0, CurrentRow].Value.ToString();
                CodeOperation = dataGridViewSale[2, CurrentRow].Value.ToString();
                //Удаление поступления из бд
                selectCommand = "delete from Sale where Id=" + valueId;
                changeValue(ConnectionString, selectCommand);
                //Обновление dataGridView
                refreshForm();
                //Удаление из Журнала
                selectCommand = "delete from JournalOper where CodeOperation='" + CodeOperation + "'";
                changeValue(ConnectionString, selectCommand);
                //Удаление из Журнала Проволок
                selectCommand = "delete from Provodki where OperationID='" + CodeOperation + "'";
                changeValue(ConnectionString, selectCommand);
            }
        }
        //Добавление проводок
        private void AddSaleProvodki()
        {
            //1) Авто Дт 90 - Кт 41 Сумма проводки = Кол-во * закуп. цена
            selectCommand = "select BuyCost from Auto where ID = (select AutoID from Application where ID='" + RequestID + "')";
            string BuyCost = selectValue(ConnectionString, selectCommand).ToString();
            selectCommand = "select Model from Auto where ID = (select AutoID from Application where ID='" + RequestID + "')";
            string Auto = selectValue(ConnectionString, selectCommand).ToString();
            selectCommand = "select SeriaName from Seria where ID = (select SeriaID from Application where ID='" + RequestID + "')";
            string Seria = selectValue(ConnectionString, selectCommand).ToString();
            selectCommand = "select CountAuto from Application where ID='" + RequestID + "'";
            string CountAuto = selectValue(ConnectionString, selectCommand).ToString();
            string AutoBuySum = (Convert.ToDecimal(BuyCost) * Convert.ToDecimal(CountAuto)).ToString("0.##");
            Provodka(CodeOperation, "90", comboBoxRequest.Text , "", "41", Auto, Seria, CountAuto, AutoBuySum);
            //2)Доп. услуги
            selectCommand = "select ServiceID from Application where ID='" + RequestID + "'";
            object Service = selectValue(ConnectionString, selectCommand);
            if (Convert.ToString(Service) != "")
            {
                //Подразделение
                selectCommand = "select UnitID from Service where ID = (select ServiceID from Application where ID = '" + RequestID + "')";
                string UnitID = selectValue(ConnectionString, selectCommand).ToString();
                selectCommand = "select AccountNumber from ChartOfAccounts where ID = (select ChartID from Unit where ID = '" + UnitID + "')";
                string Credit = selectValue(ConnectionString, selectCommand).ToString();
                selectCommand = "select Name from Unit where ID= '" + UnitID + "'";
                string subkontoCredit1 = selectValue(ConnectionString, selectCommand).ToString();
                selectCommand = "select CountService from Application where ID = '" + RequestID + "'";
                string CountService = selectValue(ConnectionString, selectCommand).ToString();
                selectCommand = "select BuyCost from Service where ID = (select ServiceID from Application where ID = '" + RequestID + "')";
                BuyCost = selectValue(ConnectionString, selectCommand).ToString();
                string ServiceBuySum = (Convert.ToDecimal(BuyCost) * Convert.ToDecimal(CountService)).ToString("0.##");
                Provodka(CodeOperation, "90", comboBoxRequest.Text, "", Credit, subkontoCredit1, "", CountService, ServiceBuySum);
            }
        }
        //Формирование проводки
        private void Provodka(string CodeOperation, string Debet, string subkontoDebet1,
            string subkontoDebet2, string Credit, string subkontoCredit1, string subkontoCredit2, string Count, string Summa)
        {
            //Max ID
            selectCommand = "select MAX(ID) from Provodki";
            object maxValue = selectValue(ConnectionString, selectCommand);
            if (Convert.ToString(maxValue) == "")
                maxValue = 0;
            //Добавление в журнал проводок
            string OperationName = "Продажа авто";
            string txtSQLQuery = "insert into Provodki (ID, Date, OperationID, OperationName, Debet, SubkontoDebet1, SubkontoDebet2, Credit, SubkontoCredit1, SubkontoCredit2, Count, Summa) values (" +
         (Convert.ToInt32(maxValue) + 1) + ", '" + Date + "', '" + CodeOperation + "', '" + OperationName + "', '" + Debet + "', '" +
         subkontoDebet1 + "', '" + subkontoDebet2 + "', '" + Credit + "', '" + subkontoCredit1 + "', '" + subkontoCredit2 + "', '" + Count + "', '" + Summa + "')";
            ExecuteQuery(txtSQLQuery);
        }
        private void AddKassa(string subkontoCredit1, string Summa)
        {
           //Max ID
            selectCommand = "select MAX(ID) from Provodki";
            object maxValue = selectValue(ConnectionString, selectCommand);
            if (Convert.ToString(maxValue) == "")
                maxValue = 0;
            string OperationName = "Оплата в кассу";
            string Debet = "50";
            string Credit = "90";
            string txtSQLQuery = "insert into Provodki (ID, Date, OperationID, OperationName, Debet, Credit, SubkontoCredit1, Summa) values (" +
         (Convert.ToInt32(maxValue) + 1) + ", '" + Date + "', '" + CodeOperation + "', '" + 
         OperationName + "', '" + Debet + "', '" + Credit + "', '" + subkontoCredit1 + "', '" + Summa + "')";
            ExecuteQuery(txtSQLQuery);
        }
        private void comboBoxRequest_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        //Выделить выбранную строку
        public void findStroka(string CodeOpetation)
        {
            for (int i = 0; i < dataGridViewSale.RowCount; i++)
            {
                dataGridViewSale.Rows[i].Selected = false;
                if (dataGridViewSale.Rows[i].Cells[2].Value != null)
                    if (dataGridViewSale.Rows[i].Cells[2].Value.ToString().Contains(CodeOpetation))
                    {
                        dataGridViewSale.Rows[i].Selected = true;
                        int CurrentRow = dataGridViewSale.SelectedCells[0].RowIndex;
                        string Date = dataGridViewSale[2, CurrentRow].Value.ToString();
                        string Worker = dataGridViewSale[3, CurrentRow].Value.ToString();
                        string RequestId = dataGridViewSale[4, CurrentRow].Value.ToString();
                        string Summa = dataGridViewSale[5, CurrentRow].Value.ToString();
                        dateTimePicker.Text = Date;
                        textBoxWorker.Text = Worker;                    
                        comboBoxRequest.SelectedValue = RequestId;
                        textBoxSum.Text = Summa;
                        break;
                    }
            }
        }
        private void dataGridViewSale_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewSale.Columns[e.ColumnIndex].Name == "Date")
            {
                DateFormat(e);
            }
            if (dataGridViewSale.Columns[e.ColumnIndex].Name == "Summa")
            {
                SumFormat(e);
            }
        }
        private static void SumFormat(DataGridViewCellFormattingEventArgs formatting)
        {
            if (formatting.Value != null)
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
