using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
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
    public partial class AutoRequest : Form
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private static string sPath = Path.Combine(Application.StartupPath, "D:\\Users\\iliya\\Документы\\Политех\\3 курс\\1 семестр\\AutoSalonRight.db");
        private string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
        public string selectCommand;
        public AutoRequest()
        {
            InitializeComponent();
        }
        public void Clear(DataGridView dataGridView)
        {
            while (dataGridView.Rows.Count > 0)
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                    dataGridView.Rows.Remove(dataGridView.Rows[i]);
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
            dataGridViewAutoReport.Columns.Add(comboColumn);
        }
        private void AutoRequest_Load(object sender, EventArgs e)
        {
            selectCommand = "Select ID, SeriaName from Seria";
            comboBoxColumn(ConnectionString, selectCommand, "ID", "SeriaName", "Серия автомобиля", "Серия автомобиля");
            selectTable();
            dataGridViewHeader.Columns[0].Width = 80;
            dataGridViewHeader.Columns[1].Width = 80;
            dataGridViewHeader.Columns[2].Width = 80;
            dataGridViewHeader.Columns[3].Width = 260;
            dataGridViewAutoReport.Columns["Код автомобиля"].Width = 80;
            dataGridViewAutoReport.Columns["Модель автомобиля"].Width = 80;
            dataGridViewAutoReport.Columns["Серия автомобиля"].Width = 80;
            dataGridViewAutoReport.Columns["Серия автомобиля"].DisplayIndex = 2;
            dataGridViewAutoReport.Columns["Начальный остаток"].Width = 80;
            dataGridViewAutoReport.Columns["Приход"].Width = 50;
            dataGridViewAutoReport.Columns["Расход"].Width = 50;
            dataGridViewAutoReport.Columns["Конечный остаток"].Width = 80;
            textBoxStartOst.Width = dataGridViewAutoReport.Columns["Начальный остаток"].Width;
            textBoxPrihod.Width = dataGridViewAutoReport.Columns["Приход"].Width;
            textBoxRashod.Width = dataGridViewAutoReport.Columns["Расход"].Width;
            textBoxEndOst.Width = dataGridViewAutoReport.Columns["Конечный остаток"].Width;
        }

        // Источник данных для заполнения таблицы
        private void selectTable()
        {
            selectCommand =
                "Select a.ID as 'Код автомобиля', a.Model as 'Модель автомобиля', a.SeriaID as 'Серия автомобиля', " +
            "ROUND(SUM(CASE WHEN date(p.Date) < date('" + dateTimePickerFrom.Value.ToString("yyyy-MM-dd") + "') AND p.OperationName = 'Поступление серии авто' THEN p.Count ELSE 0 END),2) as 'Начальный остаток'," +
            "ROUND(SUM(CASE WHEN date(p.Date) >= date('" + dateTimePickerFrom.Value.ToString("yyyy-MM-dd") + "')  AND date(p.Date) <= date('" + dateTimePickerTo.Value.ToString("yyyy-MM-dd") + "')" +
            "AND p.OperationName = 'Поступление серии авто' THEN p.Count ELSE 0 END),2) as 'Приход'," +
            "ROUND(SUM(CASE WHEN date(p.Date) >= date('" + dateTimePickerFrom.Value.ToString("yyyy-MM-dd") + "') AND date(p.Date) <= date('" + dateTimePickerTo.Value.ToString("yyyy-MM-dd") + "')" +
            " AND p.OperationName = 'Продажа авто' THEN p.Count ELSE 0 END),2) as 'Расход'," +
            "ROUND(SUM(CASE WHEN date(p.Date) < date('" + dateTimePickerFrom.Value.ToString("yyyy-MM-dd") + "') AND p.OperationName = 'Поступление серии авто' THEN p.Count ELSE 0 END),2) + " +
            "ROUND(SUM(CASE WHEN date(p.Date) >= date('" + dateTimePickerFrom.Value.ToString("yyyy-MM-dd") + "')  AND date(p.Date) <= date('" + dateTimePickerTo.Value.ToString("yyyy-MM-dd") + "')" +
            "AND p.OperationName = 'Поступление серии авто' THEN p.Count ELSE 0 END),2) - " +
            "ROUND(SUM(CASE WHEN date(p.Date) >= date('" + dateTimePickerFrom.Value.ToString("yyyy-MM-dd") + "') AND date(p.Date) <= date('" + dateTimePickerTo.Value.ToString("yyyy-MM-dd") + "')" +
            " AND p.OperationName = 'Продажа авто' THEN p.Count ELSE 0 END),2) as 'Конечный остаток'" +
            " from Auto a LEFT JOIN Provodki p ON (p.SubkontoDebet1 = a.Model AND a.SeriaID = " +
                 "(select ID from Seria where SeriaName = p.SubkontoDebet2)) OR (p.SubkontoCredit1 = a.Model AND a.SeriaID = (select ID from Seria where SeriaName = p.SubkontoCredit2)) GROUP BY a.ID";
            SQLiteConnection connect = new SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(selectCommand, connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridViewAutoReport.DataSource = ds;
            dataGridViewAutoReport.DataMember = ds.Tables[0].ToString();
            connect.Close();
            textBoxStartOst.Text = dataGridViewAutoReport.Rows.Cast<DataGridViewRow>().Sum(rec => Convert.ToInt32(rec.Cells["Начальный остаток"].Value)).ToString();
            textBoxPrihod.Text = dataGridViewAutoReport.Rows.Cast<DataGridViewRow>().Sum(rec => Convert.ToInt32(rec.Cells["Приход"].Value)).ToString();
            textBoxRashod.Text = dataGridViewAutoReport.Rows.Cast<DataGridViewRow>().Sum(rec => Convert.ToInt32(rec.Cells["Расход"].Value)).ToString();
            textBoxEndOst.Text = dataGridViewAutoReport.Rows.Cast<DataGridViewRow>().Sum(rec => Convert.ToInt32(rec.Cells["Конечный остаток"].Value)).ToString();
        }

        private void buttonForm_Click(object sender, EventArgs e)
        {
            Clear(dataGridViewAutoReport);
            if (dateTimePickerFrom.Value.Date > dateTimePickerTo.Value.Date)
            {
                MessageBox.Show("Выберите верные даты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            selectTable();
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
                            ReportFile = $"{fbd.SelectedPath}.zip";
                            MessageBox.Show("Архив успешно сохранён", "Выполнено", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
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
            PdfPTable table = new PdfPTable(7);

            table.AddCell(new Phrase(dataGridViewAutoReport.Columns[0].HeaderCell.Value.ToString(), fontParagraph));
            table.AddCell(new Phrase(dataGridViewAutoReport.Columns[1].HeaderCell.Value.ToString(), fontParagraph));
            table.AddCell(new Phrase(dataGridViewAutoReport.Columns[2].HeaderCell.Value.ToString(), fontParagraph));
            PdfPTable rows1 = new PdfPTable(4);//Создание таблицы с количеством колонок
            PdfPCell header1 = new PdfPCell(new Phrase(dataGridViewHeader.Columns[3].HeaderCell.Value.ToString(), fontParagraph));//создание ячейки, которая будет в дальнейшем "объеденена"
            header1.Colspan = 4;//количество. сколько ячеек нужно будет объединить
            rows1.AddCell(header1);//создание ячеек
            rows1.AddCell(new Phrase(dataGridViewAutoReport.Columns[3].HeaderCell.Value.ToString(), fontParagraph));
            rows1.AddCell(new Phrase(dataGridViewAutoReport.Columns[4].HeaderCell.Value.ToString(), fontParagraph));
            rows1.AddCell(new Phrase(dataGridViewAutoReport.Columns[5].HeaderCell.Value.ToString(), fontParagraph));
            rows1.AddCell(new Phrase(dataGridViewAutoReport.Columns[6].HeaderCell.Value.ToString(), fontParagraph));
            PdfPCell nesthousing1 = new PdfPCell(rows1);//создание ячейки с встроенной таблицей
            nesthousing1.Colspan = 4;//чтобы количество ячеек соответствовало количеству столбцов, занимаемому в общей таблице
            table.AddCell(nesthousing1);//добавление в общую таблицу ячейки со встроенной таблицей

            for (int i = 0; i < dataGridViewAutoReport.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridViewAutoReport.Columns.Count; j++)
                {
                    table.AddCell(new Phrase(dataGridViewAutoReport.Rows[i].Cells[j].FormattedValue.ToString(), fontParagraph));
                }
            }
            PdfPTable table2 = new PdfPTable(7);
            List<string> words = new List<string>();
            string[] sum = { "", "","ИТОГО : " };
            words.AddRange(sum);
            words.Add(textBoxStartOst.Text);
            words.Add(textBoxPrihod.Text);
            words.Add(textBoxRashod.Text);
            words.Add(textBoxEndOst.Text);
            fontParagraph = new iTextSharp.text.Font(baseFont, 17, iTextSharp.text.Font.BOLD);
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
                var table = document.Tables.Add(rangeTable, dataGridViewAutoReport.Rows.Count + 3, 7, ref missing, ref missing);
                font = table.Range.Font;
                font.Size = 14;
                font.Name = "Times New Roman";
                var paragraphTableFormat = table.Range.ParagraphFormat;
                paragraphTableFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;
                paragraphTableFormat.SpaceAfter = 0;
                paragraphTableFormat.SpaceBefore = 0;

                for (int j = 1; j <= 3; ++j)
                {
                    table.Cell(1, j).Range.Text = dataGridViewAutoReport.Columns[j - 1].HeaderCell.Value.ToString();
                    table.Cell(1, j).Merge(table.Cell(2, j));
                }
                table.Cell(1, 4).Range.Text = dataGridViewHeader.Columns[3].HeaderCell.Value.ToString();
                table.Cell(1, 4).Merge(table.Cell(1, 7));
               
                for (int j = 4; j <= dataGridViewAutoReport.Columns.Count; ++j)
                {
                    table.Cell(2, j).Range.Text = dataGridViewAutoReport.Columns[j-1].HeaderCell.Value.ToString();
                }

                for (int i = 1; i <= dataGridViewAutoReport.Rows.Count; ++i)
                {
                    for (int j = 1; j <= dataGridViewAutoReport.Columns.Count; ++j)
                    {
                        table.Cell(2 + i, j).Range.Text = dataGridViewAutoReport.Rows[i - 1].Cells[j - 1].FormattedValue.ToString();
                    }
                }
               
                List<string> words = new List<string>();
                string[] sum = { "", "", "Итого:" };
                words.AddRange(sum);
                words.Add(textBoxStartOst.Text);
                words.Add(textBoxPrihod.Text);
                words.Add(textBoxRashod.Text);
                words.Add(textBoxEndOst.Text);

                for (int j = 0; j < words.Count; j++)
                {
                    table.Cell(dataGridViewAutoReport.Rows.Count + 3, 1 + j).Range.Text = words[j];
                    table.Cell(dataGridViewAutoReport.Rows.Count + 3, 1 + j).Range.Bold = 1;
                }           
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
                Microsoft.Office.Interop.Excel.Range excelcells = excelworksheet.get_Range("A1", "G1");
                excelcells.Merge(Type.Missing);
                excelcells.Font.Bold = true;
                string title = "Список выполненных заявок за период с " + dateTimePickerFrom.Text + " по " + dateTimePickerTo.Text;
                excelcells.Value2 = title;
                excelcells.RowHeight = 40;
                excelcells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                excelcells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                excelcells.Font.Name = "Times New Roman";
                excelcells.Font.Size = 14;
                
                excelcells = excelworksheet.get_Range("A3","A4"); 
                excelcells.Merge(Type.Missing);
                excelcells = excelworksheet.get_Range("B3", "B4");
                excelcells.Merge(Type.Missing);
                excelcells = excelworksheet.get_Range("C3", "C4");
                excelcells.Merge(Type.Missing);
                for(int j = 0; j < 3; ++j)
                {
                    excelcells = excelworksheet.get_Range("A3", "A3");
                    excelcells = excelcells.get_Offset(0, j);
                    excelcells.Value2 = dataGridViewAutoReport.Columns[j].HeaderCell.Value.ToString();
                    excelcells.ColumnWidth = 15;
                    excelcells.Font.Bold = true;
                }

                    excelcells = excelworksheet.get_Range("D3", "G3");
                    excelcells.Merge(Type.Missing);
                    excelcells.Value2 = dataGridViewHeader.Columns[3].HeaderCell.Value.ToString();
                    excelcells.ColumnWidth = 15;
                    excelcells.Font.Bold = true;

                for (int j = 0; j < 4; j++)
                {
                    excelcells = excelworksheet.get_Range("D4", "D4");
                    excelcells = excelcells.get_Offset(0, j);
                    excelcells.Value2 = dataGridViewAutoReport.Columns[j + 3].HeaderCell.Value.ToString();
                    excelcells.ColumnWidth = 15;
                    excelcells.Font.Bold = true;
                }
                
                for (int i = 0; i < dataGridViewAutoReport.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridViewAutoReport.Columns.Count; j++)
                    {
                        excelcells = excelworksheet.get_Range("A5", "A5");
                        excelcells = excelcells.get_Offset(i, j);
                        excelcells.ColumnWidth = 20;
                        excelcells.Value2 = dataGridViewAutoReport.Rows[i].Cells[j].Value.ToString();
                    }
                }
                List<string> words = new List<string>();
                string[] sum = { "", "", "Итого:" };
                words.AddRange(sum);
                words.Add(textBoxStartOst.Text);
                words.Add(textBoxPrihod.Text);
                words.Add(textBoxRashod.Text);
                words.Add(textBoxEndOst.Text);
                for (int j = 0; j < words.Count; j++)
                {
                    excelcells = excelworksheet.get_Range("A5", "A5");
                    excelcells = excelcells.get_Offset(dataGridViewAutoReport.Rows.Count + 1, j);
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
