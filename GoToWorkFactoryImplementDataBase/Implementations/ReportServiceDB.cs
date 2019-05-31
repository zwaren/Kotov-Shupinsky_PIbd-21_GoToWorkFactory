using GoToWorkFactoryServiceDAL.BindingModels;
using GoToWorkFactoryServiceDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using GoToWorkFactoryModel;
using System.Data.Entity.SqlServer;
using Microsoft.Office.Interop.Word;
using System.Net.Mail;
using System.Configuration;
using System.Net;
using GoToWorkFactoryServiceDAL.ViewModels;

namespace GoToWorkFactoryImplementDataBase.Implementations
{
    public class ReportServiceDB : IReportService
    {
        private FactoryDbContext context;

        public ReportServiceDB(FactoryDbContext context)
        {
            this.context = context;
        }

        public void createMaterialRequest(ReportBindingModel model)
        {
            if (File.Exists(model.FileName))
            {
                File.Delete(model.FileName);
            }
            var winword = new Microsoft.Office.Interop.Word.Application();
            try
            {
                object missing = System.Reflection.Missing.Value;


                //создаем документ
                Microsoft.Office.Interop.Word.Document document =
                winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);


                //получаем ссылку на параграф
                var paragraph = document.Paragraphs.Add(missing);
                var range = paragraph.Range;
                //задаем текст
                range.Text = "Заявка на материалы";
                //задаем настройки шрифта
                var font = range.Font;
                font.Size = 16;
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


                var materials = new Dictionary<Material, int>();
                foreach (var o in context.Orders.Where(o => o.Status == OrderStatus.Принят))
                    foreach (var p in context.OrderProducts.Where(op => op.OrderId == o.Id).Select(op => context.Products.FirstOrDefault(p => p.Id == op.ProductId)))
                        foreach (var pm in context.ProductMaterials.Where(pm => pm.ProductId == p.Id))
                        {
                            var m = context.Materials.FirstOrDefault(x => x.Id == pm.MaterialId);
                            if (!materials.ContainsKey(m))
                                materials.Add(m, 0);
                            materials[m] += pm.Count;
                        }
                foreach (var m in materials.Keys.ToArray())
                    materials[m] = materials[m] - context.Materials.First(rec => rec.Id == m.Id).Count;


                //создаем таблицу
                var paragraphTable = document.Paragraphs.Add(Type.Missing);
                var rangeTable = paragraphTable.Range;
                var table = document.Tables.Add(rangeTable, materials.Count, 2, ref missing, ref missing);
                font = table.Range.Font;
                font.Size = 14;
                font.Name = "Times New Roman";
                var paragraphTableFormat = table.Range.ParagraphFormat;
                paragraphTableFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;
                paragraphTableFormat.SpaceAfter = 0;
                paragraphTableFormat.SpaceBefore = 0;
                var orderedMaterials = materials.ToList().OrderBy(kv => kv.Key.Name);
                for (int i = 0; i < materials.Count; ++i)
                {
                    table.Cell(i + 1, 1).Range.Text = orderedMaterials.ElementAt(i).Key.Name;
                    table.Cell(i + 1, 2).Range.Text = orderedMaterials.ElementAt(i).Value.ToString();
                }
                //задаем границы таблицы
                table.Borders.InsideLineStyle = WdLineStyle.wdLineStyleInset;
                table.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;


                paragraph = document.Paragraphs.Add(missing);
                range = paragraph.Range;
                range.Text = "Дата: " + DateTime.Now.ToLongDateString();
                font = range.Font;
                font.Size = 12;
                font.Name = "Times New Roman";
                paragraphFormat = range.ParagraphFormat;
                paragraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                paragraphFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;
                paragraphFormat.SpaceAfter = 10;
                paragraphFormat.SpaceBefore = 10;
                range.InsertParagraphAfter();


                //сохраняем
                object fileFormat = WdSaveFormat.wdFormatXMLDocument;
                document.SaveAs(model.FileName, ref fileFormat, ref missing,
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

            SendEmail(model.Email, "Заявка на материалы", "", model.FileName);
        }

        public void getAdminOrderList(ReportBindingModel model)
        {
            if (!File.Exists("TIMCYR.TTF"))
            {
                File.WriteAllBytes("TIMCYR.TTF", Properties.Resources.TIMCYR);
            }
            //открываем файл для работы
            FileStream fs = new FileStream(model.FileName, FileMode.OpenOrCreate, FileAccess.Write);
            //создаем документ, задаем границы, связываем документ и поток
            iTextSharp.text.Document doc = new iTextSharp.text.Document();
            doc.SetMargins(0.5f, 0.5f, 0.5f, 0.5f);
            PdfWriter writer = PdfWriter.GetInstance(doc, fs);
            doc.Open();
            BaseFont baseFont = BaseFont.CreateFont("TIMCYR.TTF", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            

            //вставляем заголовок
            var phraseTitle = new Phrase("Заказы клиентов",
            new iTextSharp.text.Font(baseFont, 16, iTextSharp.text.Font.BOLD));
            iTextSharp.text.Paragraph paragraph = new iTextSharp.text.Paragraph(phraseTitle)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 12
            };
            doc.Add(paragraph);
            var phrasePeriod = new Phrase("c " + model.DateFrom.Value.ToShortDateString() +
            " по " + model.DateTo.Value.ToShortDateString(),
            new iTextSharp.text.Font(baseFont, 14, iTextSharp.text.Font.BOLD));
            paragraph = new iTextSharp.text.Paragraph(phrasePeriod)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 12
            };
            doc.Add(paragraph);


            //вставляем таблицу, задаем количество столбцов, и ширину колонок
            PdfPTable table = new PdfPTable(6)
            {
                TotalWidth = 800F
            };
            table.SetTotalWidth(new float[] { 160, 140, 160, 100, 100, 140 });


            //вставляем шапку
            PdfPCell cell = new PdfPCell();
            var fontForCellBold = new iTextSharp.text.Font(baseFont, 10, iTextSharp.text.Font.BOLD);
            table.AddCell(new PdfPCell(new Phrase("ФИО клиента", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            table.AddCell(new PdfPCell(new Phrase("Дата создания", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            table.AddCell(new PdfPCell(new Phrase("Изделие", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            table.AddCell(new PdfPCell(new Phrase("Количество", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            table.AddCell(new PdfPCell(new Phrase("Сумма", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            table.AddCell(new PdfPCell(new Phrase("Статус", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });


            //заполняем таблицу
            var list = context.Orders.Where(o => o.Status == OrderStatus.Принят).Select(o => new OrderViewModel
            {
                Id = o.Id,
                ClientId = o.ClientId,
                Sum = o.Sum,
                Reserved = o.Reserved,
                Status = o.Status.ToString(),
                DateCreate = SqlFunctions.DateName("dd", o.DateCreate) + " " + 
                             SqlFunctions.DateName("mm", o.DateCreate) + " " +
                             SqlFunctions.DateName("yyyy", o.DateCreate)
            }).ToList();
            iTextSharp.text.Font fontForCells = new iTextSharp.text.Font(baseFont, 10);
            foreach (var order in list)
            {
                foreach (var op in context.OrderProducts.Where(op => op.OrderId == order.Id))
                {
                    cell = new PdfPCell(new Phrase(context.Clients.First(c => c.Id == order.ClientId).Name, fontForCells));
                    table.AddCell(cell);
                    cell = new PdfPCell(new Phrase(order.DateCreate, fontForCells));
                    table.AddCell(cell);
                
                    cell = new PdfPCell(new Phrase(context.Products.First(p => p.Id == op.ProductId).Name, fontForCells));
                    table.AddCell(cell);
                    cell = new PdfPCell(new Phrase(op.Count.ToString(), fontForCells));
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    table.AddCell(cell);
                
                    cell = new PdfPCell(new Phrase((context.Products.First(p => p.Id == op.ProductId).Price * op.Count).ToString(), fontForCells));
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    table.AddCell(cell);
                    cell = new PdfPCell(new Phrase(order.Status, fontForCells));
                    table.AddCell(cell);
                }
            }


            //вставляем итого
            cell = new PdfPCell(new Phrase("Итого:", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_RIGHT,
                Colspan = 4,
                Border = 0
            };
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(list.Sum(rec => rec.Sum).ToString(), fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_RIGHT,
                Border = 0
            };
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase("", fontForCellBold))
            {
                Border = 0
            };
            table.AddCell(cell);


            //вставляем таблицу
            doc.Add(table);
            doc.Close();

            SendEmail(model.Email, "Оповещение по заказам", "", model.FileName);
        }

        public void getClentOrderList(ReportBindingModel model, int clientId)
        {
            if (!File.Exists("TIMCYR.TTF"))
            {
                File.WriteAllBytes("TIMCYR.TTF", Properties.Resources.TIMCYR);
            }
            //открываем файл для работы
            FileStream fs = new FileStream(model.FileName, FileMode.OpenOrCreate, FileAccess.Write);
            //создаем документ, задаем границы, связываем документ и поток
            iTextSharp.text.Document doc = new iTextSharp.text.Document();
            doc.SetMargins(0.5f, 0.5f, 0.5f, 0.5f);
            PdfWriter writer = PdfWriter.GetInstance(doc, fs);
            doc.Open();
            BaseFont baseFont = BaseFont.CreateFont("TIMCYR.TTF", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);


            //вставляем заголовок
            var phraseTitle = new Phrase("Заказы клиента",
            new iTextSharp.text.Font(baseFont, 16, iTextSharp.text.Font.BOLD));
            iTextSharp.text.Paragraph paragraph = new iTextSharp.text.Paragraph(phraseTitle)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 12
            };
            doc.Add(paragraph);
            var phrasePeriod = new Phrase("c " + model.DateFrom.Value.ToShortDateString() +
            " по " + model.DateTo.Value.ToShortDateString(),
            new iTextSharp.text.Font(baseFont, 14, iTextSharp.text.Font.BOLD));
            paragraph = new iTextSharp.text.Paragraph(phrasePeriod)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 12
            };
            doc.Add(paragraph);


            //вставляем таблицу, задаем количество столбцов, и ширину колонок
            PdfPTable table = new PdfPTable(6)
            {
                TotalWidth = 800F
            };
            table.SetTotalWidth(new float[] { 160, 140, 160, 100, 100, 140 });


            //вставляем шапку
            PdfPCell cell = new PdfPCell();
            var fontForCellBold = new iTextSharp.text.Font(baseFont, 10, iTextSharp.text.Font.BOLD);
            table.AddCell(new PdfPCell(new Phrase("ФИО клиента", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            table.AddCell(new PdfPCell(new Phrase("Дата создания", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            table.AddCell(new PdfPCell(new Phrase("Изделие", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            table.AddCell(new PdfPCell(new Phrase("Количество", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            table.AddCell(new PdfPCell(new Phrase("Сумма", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            table.AddCell(new PdfPCell(new Phrase("Статус", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });


            //заполняем таблицу
            var list = context.Orders.Where(o => o.ClientId == clientId).Select(o => new OrderViewModel
            {
                Id = o.Id,
                ClientId = o.ClientId,
                Sum = o.Sum,
                Reserved = o.Reserved,
                Status = o.Status.ToString(),
                DateCreate = SqlFunctions.DateName("dd", o.DateCreate) + " " +
                             SqlFunctions.DateName("mm", o.DateCreate) + " " +
                             SqlFunctions.DateName("yyyy", o.DateCreate)
            }).ToList();
            iTextSharp.text.Font fontForCells = new iTextSharp.text.Font(baseFont, 10);
            foreach (var order in list)
            {
                foreach (var op in context.OrderProducts.Where(op => op.OrderId == order.Id))
                {
                    cell = new PdfPCell(new Phrase(context.Clients.First(c => c.Id == order.ClientId).Name, fontForCells));
                    table.AddCell(cell);
                    cell = new PdfPCell(new Phrase(order.DateCreate, fontForCells));
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(context.Products.First(p => p.Id == op.ProductId).Name, fontForCells));
                    table.AddCell(cell);
                    cell = new PdfPCell(new Phrase(op.Count.ToString(), fontForCells));
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase((context.Products.First(p => p.Id == op.ProductId).Price * op.Count).ToString(), fontForCells));
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    table.AddCell(cell);
                    cell = new PdfPCell(new Phrase(order.Status, fontForCells));
                    table.AddCell(cell);
                }
            }


            //вставляем итого
            cell = new PdfPCell(new Phrase("Итого:", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_RIGHT,
                Colspan = 4,
                Border = 0
            };
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(list.Sum(rec => rec.Sum).ToString(), fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_RIGHT,
                Border = 0
            };
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase("", fontForCellBold))
            {
                Border = 0
            };
            table.AddCell(cell);


            //вставляем таблицу
            doc.Add(table);
            doc.Close();

            SendEmail(model.Email, "Оповещение по заказам", "", model.FileName);
        }

        private void SendEmail(string mailAddress, string subject, string text, string attachmentPath)
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
