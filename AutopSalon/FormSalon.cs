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
    public partial class FormSalon : Form
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        public FormSalon()
        {
            InitializeComponent();
        }
        private void планСчетовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chartOfAccounts chartOfAccounts = new chartOfAccounts();
            chartOfAccounts.ShowDialog();
        }
        private void контрагентыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormContragent formContragent = new FormContragent();
            formContragent.ShowDialog();
        }
        private void автомобильToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAuto formAuto = new FormAuto();
            formAuto.ShowDialog();
        }

        private void подразделенияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUnit unit = new FormUnit();
            unit.ShowDialog();
        }
        private void допУслугиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormService service = new FormService();
            service.ShowDialog();
        }
        private void серииАвтоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSeria seria = new FormSeria();
            seria.ShowDialog();
        }
        private void поставщикиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSupplier supplier = new FormSupplier();
            supplier.ShowDialog();
        }
        private void журналОперацийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormJournal journal = new FormJournal();
            journal.ShowDialog();
        }
        private void заявкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormApplication app = new FormApplication();
            app.ShowDialog();
        }

        private void журналПроводокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormProvodki provodki = new FormProvodki();
            provodki.selectCommand = "Select * from Provodki";
            provodki.ShowDialog();
        }

        private void константыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormConstant cnst = new FormConstant();
            cnst.ShowDialog();

        }

        private void оборотносальдоваяВедомостьДвиженияАвтомобилейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoRequest autoRequest = new AutoRequest();
            autoRequest.ShowDialog();
        }

        private void отчётВыполненныхЗаявокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportRequests reportRequests = new ReportRequests();
            reportRequests.ShowDialog();
        }
    }
}
