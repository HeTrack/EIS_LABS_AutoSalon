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
    public partial class FormProvodki : Form
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private static string sPath = Path.Combine(Application.StartupPath, "D:\\Users\\iliya\\Документы\\Политех\\3 курс\\1 семестр\\AutoSalonRight.db");
        private string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
        public string selectCommand;
        public FormProvodki()
        {
            InitializeComponent();
        }
        //Выбор данных для таблицы 
        private void FormProvodki_Load(object sender, EventArgs e)
        {
            selectTable(ConnectionString, selectCommand);
        }
        private void selectTable(string ConnectionString, String selectCommand)
        {                  
            SQLiteConnection connect = new SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(selectCommand, connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridViewProvodki.DataSource = ds;
            dataGridViewProvodki.DataMember = ds.Tables[0].ToString();
            connect.Close();
            dataGridViewProvodki.ColumnHeadersVisible = true;
            dataGridViewProvodki.Columns["ID"].DisplayIndex = 0;
            dataGridViewProvodki.Columns["ID"].HeaderText = "№";
            dataGridViewProvodki.Columns["Date"].DisplayIndex = 1;
            dataGridViewProvodki.Columns["Date"].HeaderText = "Дата";
            dataGridViewProvodki.Columns["OperationID"].DisplayIndex = 2;
            dataGridViewProvodki.Columns["OperationID"].HeaderText = "Код Операции";
            dataGridViewProvodki.Columns["OperationName"].DisplayIndex = 3;
            dataGridViewProvodki.Columns["OperationName"].HeaderText = "Название Операции";
            dataGridViewProvodki.Columns["Debet"].DisplayIndex = 4;
            dataGridViewProvodki.Columns["Debet"].HeaderText = "Счёт Дебет";
            dataGridViewProvodki.Columns["SubkontoDebet1"].DisplayIndex = 5;
            dataGridViewProvodki.Columns["SubkontoDebet1"].HeaderText = "Субконто Дебет 1";
            dataGridViewProvodki.Columns["SubkontoDebet2"].DisplayIndex = 6;
            dataGridViewProvodki.Columns["SubkontoDebet2"].HeaderText = "Субконто Дебет 2";
            dataGridViewProvodki.Columns["Credit"].DisplayIndex = 7;
            dataGridViewProvodki.Columns["Credit"].HeaderText = "Счёт кредит";
            dataGridViewProvodki.Columns["SubkontoCredit1"].DisplayIndex = 8;
            dataGridViewProvodki.Columns["SubkontoCredit1"].HeaderText = "Субконто Кредит 1";
            dataGridViewProvodki.Columns["SubkontoCredit2"].DisplayIndex = 9;
            dataGridViewProvodki.Columns["SubkontoCredit2"].HeaderText = "Субконто Кредит 2";
            dataGridViewProvodki.Columns["Count"].DisplayIndex = 10;
            dataGridViewProvodki.Columns["Count"].HeaderText = "Количество";
            dataGridViewProvodki.Columns["Summa"].DisplayIndex = 11;
            dataGridViewProvodki.Columns["Summa"].HeaderText = "Сумма";
        }
        //обновление dataGridView
        private void refreshForm(string ConnectionString, String selectCommand)
        {
            selectTable(ConnectionString, selectCommand);
            dataGridViewProvodki.Update();
            dataGridViewProvodki.Refresh();          
        }
        private void buttonFind_Click(object sender, EventArgs e)
        {
            string DateFrom = dateTimePickerFrom.Value.ToString("yyyy-MM-dd");
            string DateTo = dateTimePickerTo.Value.ToString("yyyy-MM-dd");
            selectCommand = "Select * from Provodki WHERE date(Date) >= date('" + DateFrom + "') AND date(Date) <= date('" + DateTo + "')";
            refreshForm(ConnectionString, selectCommand);
        }

        private void dataGridViewProvodki_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewProvodki.Columns[e.ColumnIndex].Name == "Date")
            {
                DateFormat(e);
            }
            if (dataGridViewProvodki.Columns[e.ColumnIndex].Name == "Summa")
            {
                SumFormat(e);
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
    }
}
