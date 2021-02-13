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
    public partial class FormJournal : Form
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private static string sPath = Path.Combine(Application.StartupPath, "D:\\Users\\iliya\\Документы\\Политех\\3 курс\\1 семестр\\AutoSalonRight.db");
        private string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
        private string selectCommand;
        public FormJournal()
        {
            InitializeComponent();
        }
        private void FormJournal_Load(object sender, EventArgs e)
        {            
            selectCommand = "Select * from JournalOper";
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
            dataGridViewJournal.DataSource = ds;
            dataGridViewJournal.DataMember = ds.Tables[0].ToString();
            connect.Close();
            dataGridViewJournal.Columns["ID"].HeaderText = "№";
            dataGridViewJournal.Columns["Date"].HeaderText = "Дата";
            dataGridViewJournal.Columns["CodeOperation"].HeaderText = "Код Операции";
            dataGridViewJournal.Columns["OperationName"].HeaderText = "Название Операции";
            dataGridViewJournal.Columns["Summa"].HeaderText = "Сумма";
        }
        //Обновление dataGridView
        public void refreshForm(string ConnectionString, String selectCommand)
        {
            selectTable(ConnectionString, selectCommand);
            dataGridViewJournal.Update();
            dataGridViewJournal.Refresh();         
        }
        private void toolStripComboOperation_SelectedIndexChanged(object sender, EventArgs e)
        {
            toolStrip1.Focus();
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
        //Добавление записи в жур. операций
        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            if (toolStripComboOperation.Text == "")
            {
                MessageBox.Show("Выберите операцию", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if(toolStripComboOperation.Text =="Поступление серии авто")
            {
                FormEntrance entr = new FormEntrance();
                entr.ShowDialog();         
            }
            else if(toolStripComboOperation.Text == "Продажа авто")
            {
                FormSale sale = new FormSale();
                sale.ShowDialog();
            }
            selectCommand = "Select * from JournalOper";
            refreshForm(ConnectionString, selectCommand);
        }  
        //Удаление из журнала операций
        private void buttonDel_Click(object sender, EventArgs e)
        {
            int CurrentRow = dataGridViewJournal.SelectedCells[0].RowIndex;
            string valueId = dataGridViewJournal[0, CurrentRow].Value.ToString();
            string Date = dataGridViewJournal[1, CurrentRow].Value.ToString();
            string CodeOperation = dataGridViewJournal[2, CurrentRow].Value.ToString();
            string OperationName = dataGridViewJournal[3, CurrentRow].Value.ToString();
            if(OperationName == "Поступление серии авто")
            {              
                //Удаление операции поступлени из жур. операций
                selectCommand = "delete from JournalOper where ID=" + valueId;              
                changeValue(ConnectionString, selectCommand);
                //обновление dataGridViewJournal
                selectCommand = "select * from JournalOper";
                refreshForm(ConnectionString, selectCommand);
                //Удаление операции из бд поступления
                selectCommand = "delete from Entrance where CodeOperation='" + CodeOperation + "'";
                changeValue(ConnectionString, selectCommand);
                //Удаление проводок по операции
                selectCommand = "delete from Provodki where OperationID='" + CodeOperation + "'";
                changeValue(ConnectionString, selectCommand);
            }  
            else if(OperationName == "Продажа авто")
            {
                //Удаление операции продажи из жур. операций
                selectCommand = "delete from JournalOper where ID=" + valueId;
                changeValue(ConnectionString, selectCommand);
                //обновление dataGridViewJournal
                selectCommand = "select * from JournalOper";
                refreshForm(ConnectionString, selectCommand);
                //Удаление операции из бд поступления
                selectCommand = "delete from Sale where CodeOperation='" + CodeOperation + "'";
                changeValue(ConnectionString, selectCommand);
                //Удаление проводок по операции
                selectCommand = "delete from Provodki where OperationID='" + CodeOperation + "'";
                changeValue(ConnectionString, selectCommand);
            }
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            int CurrentRow = dataGridViewJournal.SelectedCells[0].RowIndex;
            string CodeOperation = dataGridViewJournal[2, CurrentRow].Value.ToString();
            string OperationName = dataGridViewJournal[3, CurrentRow].Value.ToString();
            if (OperationName == "Поступление серии авто")
            {
                FormEntrance entrance = new FormEntrance();
                entrance.Show();
                entrance.findStroka(CodeOperation);              
            }
            else if (OperationName == "Продажа авто")
            {
                FormSale sale = new FormSale();
                sale.Show();
                sale.findStroka(CodeOperation);
            }
        }

        private void buttonProvodki_Click(object sender, EventArgs e)
        {
            int CurrentRow = dataGridViewJournal.SelectedCells[0].RowIndex;
            string CodeOperation = dataGridViewJournal[2, CurrentRow].Value.ToString();
            FormProvodki provodki = new FormProvodki();        
            provodki.selectCommand = "Select * from Provodki where OperationID='" + CodeOperation + "'";          
            provodki.ShowDialog();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            String selectCommand = "Select * from JournalOper";
            refreshForm(ConnectionString, selectCommand);
        }

        private void dataGridViewJournal_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewJournal.Columns[e.ColumnIndex].Name == "Date")
            {
                DateFormat(e);
            }
            if (dataGridViewJournal.Columns[e.ColumnIndex].Name == "Summa")
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
