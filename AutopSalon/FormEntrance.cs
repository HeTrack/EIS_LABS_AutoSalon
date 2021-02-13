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
    public partial class FormEntrance : Form
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private static string sPath = Path.Combine(Application.StartupPath, "D:\\Users\\iliya\\Документы\\Политех\\3 курс\\1 семестр\\AutoSalonRight.db");
        public string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
        private string selectCommand;
        private string Date;
        private string CodeOperation;
        private string Supplier;
        private string Auto;
        private string Seria;
        private string Count;
        private string BuyCost;
        private string Summa;
        public FormEntrance()
        {
            InitializeComponent();
        }
        private void FormEntrance_Load(object sender, EventArgs e)
        {
            string selectSupply = "Select ID, SupplyName from Supplier";
            selectCombo(ConnectionString, selectSupply, comboBoxSupply, "SupplyName", "ID");
            comboBoxSupply.SelectedIndex = -1;
            comboBoxColumn(ConnectionString, selectSupply, "ID", "SupplyName", "SupplyID", "SupplyID");

            string selectSeria = "Select ID, SeriaName from Seria";
            selectCombo(ConnectionString, selectSeria, comboBoxSeria, "SeriaName", "ID");
            comboBoxSeria.SelectedIndex = -1;
            comboBoxColumn(ConnectionString, selectSeria, "ID", "SeriaName", "SeriaID", "SeriaID");

            string selectAuto = "Select ID, Model from Auto";
            comboBoxColumn(ConnectionString, selectAuto, "ID", "Model", "AutoID", "AutoID");

            selectCommand = "Select * from Entrance";
            selectTable(ConnectionString, selectCommand);
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
            dataGridViewEntrance.DataSource = ds;
            dataGridViewEntrance.DataMember = ds.Tables[0].ToString();
            connect.Close();
            dataGridViewEntrance.ColumnHeadersVisible = true;
            dataGridViewEntrance.Columns["ID"].DisplayIndex = 0;
            dataGridViewEntrance.Columns["ID"].HeaderText = "№";
            dataGridViewEntrance.Columns["Date"].DisplayIndex = 1;
            dataGridViewEntrance.Columns["Date"].HeaderText = "Дата";
            dataGridViewEntrance.Columns["CodeOperation"].DisplayIndex = 2;
            dataGridViewEntrance.Columns["CodeOperation"].HeaderText = "Код Операции";
            dataGridViewEntrance.Columns["SupplyID"].DisplayIndex = 3;
            dataGridViewEntrance.Columns["SupplyID"].HeaderText = "Поставщик";
            dataGridViewEntrance.Columns["AutoID"].DisplayIndex = 4;
            dataGridViewEntrance.Columns["AutoID"].HeaderText = "Модель Авто";
            dataGridViewEntrance.Columns["SeriaID"].DisplayIndex = 5;
            dataGridViewEntrance.Columns["SeriaID"].HeaderText = "Серия";
            dataGridViewEntrance.Columns["Count"].DisplayIndex = 6;
            dataGridViewEntrance.Columns["Count"].HeaderText = "Количество";
            dataGridViewEntrance.Columns["Summa"].DisplayIndex = 7;
            dataGridViewEntrance.Columns["Summa"].HeaderText = "Сумма";
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
            dataGridViewEntrance.Columns.Add(comboColumn);
        }
        //Выделить выбранную строку
        public void findStroka(string CodeOpetation)
        {
            for (int i = 0; i < dataGridViewEntrance.RowCount; i++)
            {
                dataGridViewEntrance.Rows[i].Selected = false;
                if (dataGridViewEntrance.Rows[i].Cells[2].Value != null)
                    if (dataGridViewEntrance.Rows[i].Cells[2].Value.ToString().Contains(CodeOpetation))
                    {
                        dataGridViewEntrance.Rows[i].Selected = true;
                        int CurrentRow = dataGridViewEntrance.SelectedCells[0].RowIndex;
                        Date = dataGridViewEntrance[1, CurrentRow].Value.ToString();
                        Supplier = dataGridViewEntrance[3, CurrentRow].Value.ToString();
                        Auto = dataGridViewEntrance[4, CurrentRow].Value.ToString();
                        Seria = dataGridViewEntrance[5, CurrentRow].Value.ToString();
                        Count = dataGridViewEntrance[6, CurrentRow].Value.ToString();
                        BuyCost = dataGridViewEntrance[7, CurrentRow].Value.ToString();
                        dateTimePicker.Text = Date;
                        comboBoxSupply.SelectedValue = Supplier;
                        comboBoxSeria.SelectedValue = Seria;
                        selectCommand = "select ID, Model from Auto where SeriaID = " + Convert.ToInt32(comboBoxSeria.SelectedValue);
                        selectCombo(ConnectionString, selectCommand, comboBoxAuto, "Model", "ID");
                        comboBoxAuto.SelectedValue = Auto;
                        textBoxCount.Text = Count;
                        textBoxBuyCost.Text = BuyCost;
                        break;
                    }
            }
        }
        private bool Validation()
        {
            if (string.IsNullOrEmpty(comboBoxSupply.Text))
            {
                MessageBox.Show("Выберите поставщика", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(comboBoxAuto.Text))
            {
                MessageBox.Show("Выберите авто", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(comboBoxSeria.Text))
            {
                MessageBox.Show("Выберите серию", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            Regex regex = new Regex(@"^\d{1,15}?(\,\d\d)?$");
            bool matches = regex.IsMatch(textBoxBuyCost.Text);
            if (!matches)
            {
                MessageBox.Show("Ошибка! Введена некорректная цена. Проверьте, что десятичная часть указана через запятую, а не через точку");
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
        //Обновление dataGridView
        public void refreshForm(string ConnectionString, String selectCommand)
        {
            selectTable(ConnectionString, selectCommand);
            dataGridViewEntrance.Update();
            dataGridViewEntrance.Refresh();
            dateTimePicker.Text = "";
            comboBoxAuto.SelectedIndex = -1;
            comboBoxSeria.SelectedIndex = -1;
            comboBoxSupply.SelectedIndex = -1;
            textBoxCount.Text = "";
            textBoxBuyCost.Text = "";
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

        //Добавление операции поступления
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            //валидация полей
            if (Validation())
            {
                //Max ID
                selectCommand = "select MAX(ID) from Entrance";
                object maxValue = selectValue(ConnectionString, selectCommand);
                if (Convert.ToString(maxValue) == "")
                    maxValue = 0;
                Date = dateTimePicker.Value.ToString("yyyy-MM-dd");
                string nulik = "0000000";
                CodeOperation = "ПСТ" + nulik.Substring(0, 7 - (Convert.ToInt32(maxValue) + 1).ToString().Length) + (Convert.ToInt32(maxValue) + 1).ToString();
                Supplier = comboBoxSupply.SelectedValue.ToString();
                Auto = comboBoxAuto.SelectedValue.ToString();
                Seria = comboBoxSeria.SelectedValue.ToString();
                Count = textBoxCount.Text;
                BuyCost = textBoxBuyCost.Text;
                Summa = (Convert.ToDecimal(BuyCost) * Convert.ToDecimal(Count)).ToString("0.##");
                //Добавление операцию поступление
                string txtSQLQuery = "insert into Entrance (ID, Date, CodeOperation, SupplyID, AutoID, SeriaID, Count, Summa) values (" +
               (Convert.ToInt32(maxValue) + 1) + ", '" + Date + "', '" + CodeOperation + "', " + Convert.ToInt32(Supplier) + ", " +
               Convert.ToInt32(Auto) + ", " + Convert.ToInt32(Seria) + ", " + Convert.ToInt32(Count) + ", '" + Summa + "')";
                ExecuteQuery(txtSQLQuery);

                /*Добавление в Журнал Операций
                            Max ID             */
                selectCommand = "select MAX(ID) from JournalOper";
                maxValue = selectValue(ConnectionString, selectCommand);
                if (Convert.ToString(maxValue) == "")
                    maxValue = 0;
                //Добавление в журнал операций
                txtSQLQuery = "insert into JournalOper (ID, Date, CodeOperation, OperationName, Summa) values (" +
               (Convert.ToInt32(maxValue) + 1) + ", '" + Date + "', '" + CodeOperation + "', '" + "Поступление серии авто" + "', '" + Summa + "')";
                ExecuteQuery(txtSQLQuery);

                //Добавление в журнал проводок           
                AddProvodki(CodeOperation, comboBoxAuto.Text, comboBoxSeria.Text, comboBoxSupply.Text, Count, Summa);

                //Добавление закупочной и розничной цены в справочник Авто
                selectCommand = "update Auto set BuyCost='" + BuyCost + "' where ID='" + Auto + "'";
                changeValue(ConnectionString, selectCommand);
                selectCommand = "select Procent from Constant where Mark='" + comboBoxAuto.Text + "'";
                object procent = selectValue(ConnectionString, selectCommand);
                selectCommand = "update Auto set SaleCost='" + (Convert.ToDecimal(BuyCost) * (1 + Convert.ToDecimal(procent) / 100)).ToString("0.##") + "' where ID='" + Auto + "'";
                changeValue(ConnectionString, selectCommand);

                //Обновление dataGridView
                selectCommand = "Select * from Entrance";
                refreshForm(ConnectionString, selectCommand);
            }
        }
        //Удаление операции поступления
        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridViewEntrance.SelectedRows.Count > 0)
            {
                int CurrentRow = dataGridViewEntrance.SelectedCells[0].RowIndex;
                string valueId = dataGridViewEntrance[0, CurrentRow].Value.ToString();
                Date = dataGridViewEntrance[1, CurrentRow].Value.ToString();
                CodeOperation = dataGridViewEntrance[2, CurrentRow].Value.ToString();
                //Удаление поступления из бд
                selectCommand = "delete from Entrance where ID=" + valueId;
                changeValue(ConnectionString, selectCommand);

                //Удаление из Журнала
                selectCommand = "delete from JournalOper where CodeOperation='" + CodeOperation + "'";
                changeValue(ConnectionString, selectCommand);

                //Удаление проводок
                selectCommand = "delete from Provodki where OperationID='" + CodeOperation + "'";
                changeValue(ConnectionString, selectCommand);

                //Обновление dataGridView
                selectCommand = "select * from Entrance";
                refreshForm(ConnectionString, selectCommand);
            }
        }
        //Редактирование операции поступления
        public void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridViewEntrance.SelectedRows.Count > 0)
            {
                int CurrentRow = dataGridViewEntrance.SelectedCells[0].RowIndex;
                string valueId = dataGridViewEntrance[0, CurrentRow].Value.ToString();
                Date = dateTimePicker.Text;
                CodeOperation = dataGridViewEntrance[1, CurrentRow].Value.ToString();
                Supplier = comboBoxSupply.SelectedValue.ToString();
                Auto = comboBoxAuto.SelectedValue.ToString();
                Seria = comboBoxSeria.SelectedValue.ToString();
                Count = textBoxCount.Text;
                BuyCost = textBoxBuyCost.Text;
                Summa = (Convert.ToDecimal(BuyCost) * Convert.ToDecimal(Count)).ToString("0.##");
                //Валидация
                if (Validation())
                {
                    //Редактировать операцию
                    selectCommand = "update Entrance set Date='" + Date + "', SupplyID=" + Convert.ToInt32(Supplier) + ", AutoID=" +
                    Convert.ToInt32(Auto) + ", SeriaID=" + Convert.ToInt32(Seria) + ", Count=" +
                    Convert.ToInt32(Count) + ", Summa='" + Summa + "' where ID = " + valueId;
                    changeValue(ConnectionString, selectCommand);

                    //Обновление журнала операций
                    selectCommand = "update JournalOper set Data='" + Date + "', Summa='" + Summa + "' where CodeOperation='" + CodeOperation + "'";
                    changeValue(ConnectionString, selectCommand);

                    //Обновление журнала-проводок
                    //Субконто_Авто
                    selectCommand = "update Provodki set Date='" + Date + "', SubkontoDebet1='" + comboBoxAuto.Text + "', SubkontoDebet2='" +
                    comboBoxSeria.Text + "', SubkontoCredit1='" + comboBoxSupply.Text + "', Count=" +
                    Convert.ToInt32(Count) + ", Summa='" + Summa + "' where OperationID='" + CodeOperation + "'";
                    changeValue(ConnectionString, selectCommand);

                    //Обновить закупочную и розничную цену
                    selectCommand = "update Auto set BuyCost='" + BuyCost + "' where ID='" + Auto + "'";
                    changeValue(ConnectionString, selectCommand);
                    selectCommand = "select Procent from Constant where Mark='" + comboBoxAuto.Text + "'";
                    object procent = selectValue(ConnectionString, selectCommand);
                    selectCommand = "update Auto set SaleCost='" + (Convert.ToDecimal(BuyCost) * (1 + Convert.ToDecimal(procent) / 100)).ToString("0.##") + "' where ID='" + Auto + "'";
                    changeValue(ConnectionString, selectCommand);

                    //обновление dataGridView
                    selectCommand = "select * from Entrance";
                    refreshForm(ConnectionString, selectCommand);
                }
            }
        }
        private void dataGridViewEntrance_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //выбрана строка CurrentRow  ID, Date, CodeOperation, Count, Summa, SupplyID, SeriaID
            int CurrentRow = dataGridViewEntrance.SelectedCells[0].RowIndex;
            Date = dataGridViewEntrance[1, CurrentRow].Value.ToString();
            Supplier = dataGridViewEntrance[3, CurrentRow].Value.ToString();
            Auto = dataGridViewEntrance[4, CurrentRow].Value.ToString();
            Seria = dataGridViewEntrance[5, CurrentRow].Value.ToString();
            Count = dataGridViewEntrance[6, CurrentRow].Value.ToString();
            Summa = dataGridViewEntrance[7, CurrentRow].Value.ToString();
            BuyCost = (Convert.ToDecimal(Summa) / Convert.ToDecimal(Count)).ToString();
            dateTimePicker.Text = Date;
            comboBoxSupply.SelectedValue = Convert.ToInt32(Supplier);
            comboBoxSeria.SelectedValue = Convert.ToInt32(Seria);
            selectCommand = "select ID, Model from Auto where SeriaID = " + Convert.ToInt32(comboBoxSeria.SelectedValue);
            selectCombo(ConnectionString, selectCommand, comboBoxAuto, "Model", "ID");
            comboBoxAuto.SelectedValue = Convert.ToInt32(Auto);
            textBoxCount.Text = Count;
            textBoxBuyCost.Text = BuyCost;
        }
        private void AddProvodki(string CodeOperation, string subkontoDebet1, string subkontoDebet2, string subkontoCredit, string Count, string Summa)
        {
            /*Добавление в Журнал Проводок
                        Max ID             */
            selectCommand = "select MAX(ID) from Provodki";
            object maxValue = selectValue(ConnectionString, selectCommand);
            if (Convert.ToString(maxValue) == "")
                maxValue = 0;
            //дебет
            string debet = "41";
            //кредит
            string credit = "60";
            string OperationName = "Поступление серии авто";
            //Добавление в журнал проводок
            string txtSQLQuery = "insert into Provodki (ID, Date, OperationID, OperationName, Debet, SubkontoDebet1, SubkontoDebet2, Credit, SubkontoCredit1, Count, Summa) values (" +
         (Convert.ToInt32(maxValue) + 1) + ", '" + dateTimePicker.Value.ToString("yyyy-MM-dd") + "', '" + CodeOperation + "', '" + OperationName + "', " + debet + ", '" +
         subkontoDebet1 + "', '" + subkontoDebet2 + "', " + credit + ", '" + subkontoCredit + "', " + Count + ", '" + Summa + "')";
            ExecuteQuery(txtSQLQuery);
        }
        private void textBoxCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 44)
            {
                e.Handled = true;
            }
        }
        private void textBoxBuyCost_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 44)
            {
                e.Handled = true;
            }
        }
        private void comboBoxSeria_SelectionChangeCommitted(object sender, EventArgs e)
        {
            selectCommand = "select ID, Model from Auto where SeriaID = " + comboBoxSeria.SelectedValue;
            selectCombo(ConnectionString, selectCommand, comboBoxAuto, "Model", "ID");
            comboBoxAuto.SelectedIndex = -1;
        }
        private void dataGridViewEntrance_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewEntrance.Columns[e.ColumnIndex].Name == "Date")
            {
                DateFormat(e);
            }
            if (dataGridViewEntrance.Columns[e.ColumnIndex].Name == "Summa")
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
