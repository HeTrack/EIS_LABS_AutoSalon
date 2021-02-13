using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Application = System.Windows.Forms.Application;
using DataTable = System.Data.DataTable;

namespace AutopSalon
{
    public partial class ReportRequests : Form
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private static string sPath = Path.Combine(Application.StartupPath, "D:\\Users\\iliya\\Документы\\Политех\\3 курс\\1 семестр\\AutoSalonRight.db");
        private string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
        public string selectCommand;
        public ReportRequests()
        {
            InitializeComponent();
        }
        public void Clear(DataGridView dataGridView)
        {
            while (dataGridView.Rows.Count > 0)
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                    dataGridView.Rows.Remove(dataGridView.Rows[i]);
        }
        private void ReportRequests_Load(object sender, EventArgs e)
        {
            selectTable();
            dataGridViewReport.ColumnHeadersVisible = true;
            dataGridViewReport.Columns["Date"].Visible = false;
            dataGridViewReport.Columns["SubkontoDebet1"].HeaderText = "Заявка";
            dataGridViewReport.Columns["Count"].HeaderText = "Количество";
            dataGridViewReport.Columns["BuyCost"].HeaderText = "Себестоимость";
            dataGridViewReport.Columns["SaleCost"].HeaderText = "Сумма продажи";
        }

        // Источник данных для заполнения таблицы
        private void selectTable()
        {
            selectCommand = "Select * from (Select p.Date, p.SubkontoDebet1, p.SubkontoCredit1 AS 'Авто|Услуга', p.Count, a.BuyCost, a.SaleCost, p.Count*(a.SaleCost - a.BuyCost) as 'Прибыль' " +
                "from Provodki p JOIN Auto a ON p.SubkontoCredit1 = a.Model AND p.SubkontoCredit2 = (select SeriaName from Seria where a.SeriaID = ID) AND date(p.Date) >= date('" + dateTimePickerFrom.Value.ToString("yyyy-MM-dd") + "') " +
                "AND date(p.Date) <= date('" + dateTimePickerTo.Value.ToString("yyyy-MM-dd") + "')" +
                "UNION " +
                "Select p.Date, p.SubkontoDebet1, serv.ServiceName AS 'Авто|Услуга', p.Count, serv.BuyCost, serv.SaleCost, p.Count*(serv.SaleCost - serv.BuyCost) as 'Прибыль'" +
                "from Provodki p JOIN Application app ON p.SubkontoDebet1 = app.Number AND p.Credit != 41 AND date(p.Date) >= date('" + dateTimePickerFrom.Value.ToString("yyyy-MM-dd") + "') " +
                "AND date(p.Date) <= date('" + dateTimePickerTo.Value.ToString("yyyy-MM-dd") + "') JOIN Service serv ON app.ServiceID = serv.ID) ORDER BY Date";
            SQLiteConnection connect = new SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(selectCommand, connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridViewReport.DataSource = ds;
            dataGridViewReport.DataMember = ds.Tables[0].ToString();
            connect.Close();
            selectCommand = "Select SUM(Count*(SaleCost - BuyCost)) from (Select p.Date, p.SubkontoDebet1, p.SubkontoCredit1, p.Count, a.BuyCost, a.SaleCost, p.Count*(a.SaleCost - a.BuyCost) " +
               "from Provodki p JOIN Auto a ON p.SubkontoCredit1 = a.Model AND p.SubkontoCredit2 = (select SeriaName from Seria where a.SeriaID = ID) AND date(p.Date) >= date('" + dateTimePickerFrom.Value.ToString("yyyy-MM-dd") + "') " +
               "AND date(p.Date) <= date('" + dateTimePickerTo.Value.ToString("yyyy-MM-dd") + "')" +
               "UNION " +
               "Select p.Date, p.SubkontoDebet1, serv.ServiceName, p.Count, serv.BuyCost, serv.SaleCost, p.Count*(serv.SaleCost - serv.BuyCost) " +
               "from Provodki p JOIN Application app ON p.SubkontoDebet1 = app.Number AND p.Credit != 41 AND date(p.Date) >= date('" + dateTimePickerFrom.Value.ToString("yyyy-MM-dd") + "') " +
               "AND date(p.Date) <= date('" + dateTimePickerTo.Value.ToString("yyyy-MM-dd") + "') JOIN Service serv ON app.ServiceID = serv.ID)";
            object itog = selectValue(ConnectionString, selectCommand);
            if(Convert.ToString(itog) != "")
            textBoxItog.Text = Convert.ToDecimal(itog).ToString("C2");
        }

        private void dataGridViewReport_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewReport.Columns[e.ColumnIndex].Name == "BuyCost" ||
                dataGridViewReport.Columns[e.ColumnIndex].Name == "SaleCost" ||
                dataGridViewReport.Columns[e.ColumnIndex].Name == "Прибыль")
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
                    String sumString = Convert.ToDecimal(formatting.Value).ToString("C2");
                    formatting.Value = sumString;
                    formatting.FormattingApplied = true;
                }
                catch (FormatException)
                {              
                    formatting.FormattingApplied = false;
                }
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
        private void buttonCreate_Click(object sender, EventArgs e)
        {
            Clear(dataGridViewReport);

            if (dateTimePickerFrom.Value.Date > dateTimePickerTo.Value.Date)
            {
                MessageBox.Show("Выберите верные даты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }         
            selectTable();
            if (dataGridViewReport.Rows.Count == 0)
            {
                MessageBox.Show("На данные даты нет выполненных заявок", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            FormSave save = new FormSave();
            if (save.ShowDialog() == DialogResult.OK)
            {
                string ReportFile = "";
                if (save.format == "doc")
                {
                    SaveFileDialog sfd = new SaveFileDialog
                    {
                        Filter = "doc|*.doc"
                    };
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            saveDoc(sfd.FileName);
                            ReportFile = sfd.FileName;
                            MessageBox.Show("Файл успешно сохранён", "Выполнено", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else if (save.format == "pdf")
                {
                    SaveFileDialog sfd = new SaveFileDialog
                    {
                        Filter = "pdf|*.pdf"
                    };
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            savePDF(sfd.FileName);
                            ReportFile = sfd.FileName;
                            MessageBox.Show("Файл успешно сохранён", "Выполнено", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                }
                else if (save.format == "xls")
                {
                    SaveFileDialog sfd = new SaveFileDialog
                    {
                        Filter = "xls|*.xls|xlsx|*.xlsx"
                    };
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            saveXls(sfd.FileName);
                            ReportFile = sfd.FileName;
                            MessageBox.Show("Файл успешно сохранён", "Выполнено", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        }
                    }
                }
                else if (save.format == "Архив")
                {
                    try
                    {
                        var fbd = new FolderBrowserDialog();
                        if (fbd.ShowDialog() == DialogResult.OK)
                        {
                            CreateArchive(fbd.SelectedPath);
                            ReportFile = $"{fbd.SelectedPath}.zip"; ;
                            MessageBox.Show("Архив успешно сохранён", "Выполнено", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                if (save.ischeck)
                {
                    SendEmailForClients(save.Email, "Отчет", "Список выполненных заявок за период с " + dateTimePickerFrom.Text + " по " + dateTimePickerTo.Text, ReportFile);
                }
            }
        }
        public void savePDF(string FileName)
        {
            string FONT_LOCATION = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.TTF");
            BaseFont baseFont = BaseFont.CreateFont(FONT_LOCATION, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font fontParagraph = new iTextSharp.text.Font(baseFont, 17, iTextSharp.text.Font.NORMAL);
            string title = "Список выполненных заявок за период с " + dateTimePickerFrom.Text + " по " + dateTimePickerTo.Text;
            var phraseTitle = new Phrase(title, new iTextSharp.text.Font(baseFont, 18, iTextSharp.text.Font.BOLD));
            iTextSharp.text.Paragraph paragraph = new
            iTextSharp.text.Paragraph(phraseTitle)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 12
            };
            PdfPTable table = new PdfPTable(dataGridViewReport.Columns.Count - 1);
            for (int i = 0; i < dataGridViewReport.Columns.Count - 1; i++)
            {
                table.AddCell(new Phrase(dataGridViewReport.Columns[i + 1].HeaderCell.Value.ToString(), fontParagraph));

            }
            for (int i = 0; i < dataGridViewReport.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridViewReport.Columns.Count - 1; j++)
                {
                    table.AddCell(new Phrase(dataGridViewReport.Rows[i].Cells[j + 1].FormattedValue.ToString(), fontParagraph));
                }
            }
            PdfPTable table2 = new PdfPTable(dataGridViewReport.Columns.Count - 1);
            List<string> words = new List<string>();
            string[] sum = { "", "", "", "", "ИТОГО : " };
            words.AddRange(sum);
            words.Add(textBoxItog.Text);
            for (int j = 0; j < words.Count; j++)
            {
                table2.AddCell(new Phrase(words[j], fontParagraph));
            }
            using (FileStream stream = new FileStream(FileName, FileMode.Create))
            {
                iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(PageSize.A2, 10f, 10f, 10f, 0f);
                PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                pdfDoc.Add(paragraph);
                pdfDoc.Add(table);
                pdfDoc.Add(table2);
                pdfDoc.Close();
                stream.Close();
            }
        }
        public void saveDoc(string FileName)
        {
            var winword = new Microsoft.Office.Interop.Word.Application();
            try
            {
                object missing = System.Reflection.Missing.Value;
                //создаем документ
                Microsoft.Office.Interop.Word.Document document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);
                document.Sections.PageSetup.LeftMargin = 45;
                //получаем ссылку на параграф
                var paragraph = document.Paragraphs.Add(missing);
                var range = paragraph.Range;
                string title = "Список выполненных заявок за период с " + dateTimePickerFrom.Text + " по " + dateTimePickerTo.Text;
                //задаем текст
                range.Text = title;
                //задаем настройки шрифта
                var font = range.Font;
                font.Size = 14;
                font.Name = "Times New Roman";
                font.Bold = 1;
                //задаем настройки абзаца
                var paragraphFormat = range.ParagraphFormat;
                paragraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                paragraphFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;
                paragraphFormat.SpaceAfter = 10;
                paragraphFormat.SpaceBefore = 0;
                //добавляем абзац в документ
                range.InsertParagraphAfter();
                //создаем таблицу
                var paragraphTable = document.Paragraphs.Add(Type.Missing);
                var rangeTable = paragraphTable.Range;
                var table = document.Tables.Add(rangeTable, dataGridViewReport.Rows.Count + 2, 6, ref missing, ref missing);
                font = table.Range.Font;
                font.Size = 14;
                font.Name = "Times New Roman";
                var paragraphTableFormat = table.Range.ParagraphFormat;
                paragraphTableFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;
                paragraphTableFormat.SpaceAfter = 0;
                paragraphTableFormat.SpaceBefore = 0;

                for (int j = 0; j < dataGridViewReport.Columns.Count - 1; ++j)
                {
                    table.Cell(1, 1 + j).Range.Text = dataGridViewReport.Columns[j + 1].HeaderCell.Value.ToString();
                }

                for (int i = 0; i < dataGridViewReport.Rows.Count; ++i)
                {
                    for (int j = 0; j < dataGridViewReport.Columns.Count - 1; ++j)
                    {
                        table.Cell(2 + i, 1 + j).Range.Text = dataGridViewReport.Rows[i].Cells[j + 1].FormattedValue.ToString();
                    }
                }
                List<string> words = new List<string>();
                string[] sum = { "", "", "", "", "Итого:" };
                words.AddRange(sum);
                words.Add(textBoxItog.Text);

                for (int j = 0; j < words.Count; j++)
                {
                    table.Cell(dataGridViewReport.Rows.Count + 2, 1 + j).Range.Text = words[j];
                }
                table.Cell(dataGridViewReport.Rows.Count + 2, 5).Range.Bold = 1;
                //задаем границы таблицы
                table.Borders.InsideLineStyle = WdLineStyle.wdLineStyleInset;
                table.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;
                //сохраняем
                object fileFormat = WdSaveFormat.wdFormatXMLDocument;
                document.SaveAs(FileName, ref fileFormat, ref missing,
                ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing,
                ref missing);
                document.Close(ref missing, ref missing, ref missing);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                winword.Quit();
            }

        }
        public void saveXls(string FileName)
        {
            var excel = new Microsoft.Office.Interop.Excel.Application();
            try
            {
                if (File.Exists(FileName))
                {
                    excel.Workbooks.Open(FileName, Type.Missing, Type.Missing,
                   Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                   Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                   Type.Missing,
                    Type.Missing);
                }
                else
                {
                    excel.SheetsInNewWorkbook = 1;
                    excel.Workbooks.Add(Type.Missing);
                    excel.Workbooks[1].SaveAs(FileName, XlFileFormat.xlExcel8,
                    Type.Missing,
                     Type.Missing, false, false, XlSaveAsAccessMode.xlNoChange,
                    Type.Missing,
                     Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                }
                Sheets excelsheets = excel.Workbooks[1].Worksheets;

                var excelworksheet = (Worksheet)excelsheets.get_Item(1);
                excelworksheet.Cells.Clear();
                Microsoft.Office.Interop.Excel.Range excelcells = excelworksheet.get_Range("A1", "F1");
                excelcells.Merge(Type.Missing);
                excelcells.Font.Bold = true;
                string title = "Список выполненных заявок за период с " + dateTimePickerFrom.Text + " по " + dateTimePickerTo.Text;
                excelcells.Value2 = title;
                excelcells.RowHeight = 40;
                excelcells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                excelcells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                excelcells.Font.Name = "Times New Roman";
                excelcells.Font.Size = 14;

                for (int j = 0; j < dataGridViewReport.Columns.Count-1; j++)
                {
                    excelcells = excelworksheet.get_Range("A3", "A3");
                    excelcells = excelcells.get_Offset(0, j);
                    excelcells.ColumnWidth = 15;
                    excelcells.Value2 = dataGridViewReport.Columns[j+1].HeaderCell.Value.ToString();
                    excelcells.Font.Bold = true;
                }

                for (int i = 0; i < dataGridViewReport.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridViewReport.Columns.Count-1; j++)
                    {
                        excelcells = excelworksheet.get_Range("A4", "A4");
                        excelcells = excelcells.get_Offset(i, j);
                        excelcells.ColumnWidth = 20;
                        excelcells.Value2 = dataGridViewReport.Rows[i].Cells[j+1].Value.ToString();
                    }
                }
                List<string> words = new List<string>();
                string[] sum = { "", "", "", "", "Итого:" };
                words.AddRange(sum);
                words.Add(textBoxItog.Text);
                for (int j = 0; j < words.Count; j++)
                {
                    excelcells = excelworksheet.get_Range("A3", "A3");
                    excelcells = excelcells.get_Offset(dataGridViewReport.Rows.Count + 1, j);
                    excelcells.ColumnWidth = 25;
                    excelcells.Value2 = words[j].ToString();
                    excelcells.Font.Bold = true;
                }
                excel.Workbooks[1].Save();
                excel.Workbooks[1].Close();
            }
            catch (Exception)
            {
            }
            finally
            {
                excel.Quit();
            }
        }
        public void CreateArchive(string folderName)
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(folderName);
                if (dirInfo.Exists)
                {
                    foreach (FileInfo file in dirInfo.GetFiles())
                    {
                        file.Delete();
                    }
                }
                string fileName = $"{folderName}.zip";
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                savePDF(folderName + "\\ОтчётPdf.pdf");
                saveDoc(folderName + "\\ОтчётDoc.doc");
                saveXls(folderName + "\\ОтчётXls.xls");
                ZipFile.CreateFromDirectory(folderName, fileName);
                dirInfo.Delete(true);
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void SendEmailForClients(string mailAddress, string subject, string text, string attachmentPath)
        {
            System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage();
            SmtpClient smtpClient = null;
            try
            {
                m.From = new MailAddress(ConfigurationManager.AppSettings["MailLogin"]);
                m.To.Add(new MailAddress(mailAddress));
                m.Subject = subject;
                m.Body = text;
                m.SubjectEncoding = System.Text.Encoding.UTF8;
                m.BodyEncoding = System.Text.Encoding.UTF8;
                m.Attachments.Add(new Attachment(attachmentPath));
                smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.EnableSsl = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Credentials = new NetworkCredential(
                    ConfigurationManager.AppSettings["MailLogin"],
                    ConfigurationManager.AppSettings["MailPassword"]
                    );
                smtpClient.Send(m);
            }
            catch (Exception ex)
            {
                 throw ex;
            }
            finally
            {
                m = null;
                smtpClient = null;
            }
        }
    }
}
