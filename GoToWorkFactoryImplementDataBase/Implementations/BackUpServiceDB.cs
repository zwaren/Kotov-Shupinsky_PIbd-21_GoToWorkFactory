using GoToWorkFactoryModel;
using GoToWorkFactoryServiceDAL.BindingModels;
using GoToWorkFactoryServiceDAL.Interfaces;
using GoToWorkFactoryServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace GoToWorkFactoryImplementDataBase.Implementations
{
    public class BackUpServiceDB : IBackUpService
    {
        private FactoryDbContext context;

        public BackUpServiceDB(FactoryDbContext context)
        {
            this.context = context;
        }

        public void BackUpAdmin()
        {
            var ms = context.Materials.ToList();
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Material>));
            using (FileStream fs = new FileStream("materials.json", FileMode.OpenOrCreate))
            {
                jsonFormatter.WriteObject(fs, ms);
            }

            var ps = context.Products.ToList();
            jsonFormatter = new DataContractJsonSerializer(typeof(List<Product>));

            using (FileStream fs = new FileStream("products.json", FileMode.OpenOrCreate))
            {
                jsonFormatter.WriteObject(fs, ps);
            }

            SendEmail(@"deviler.san@yandex.ru", "Бекап бд для админа", "", new string[] { "materials.json", "products.json" });
        }

        public void BackUpClent()
        {
            var os = context.Orders.ToList();
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Order>));
            using (FileStream fs = new FileStream("orders.json", FileMode.OpenOrCreate))
            {
                jsonFormatter.WriteObject(fs, os);
            }

            var ops = context.OrderProducts.ToList();
            jsonFormatter = new DataContractJsonSerializer(typeof(List<OrderProduct>));

            using (FileStream fs = new FileStream("orderproducts.json", FileMode.OpenOrCreate))
            {
                jsonFormatter.WriteObject(fs, ops);
            }

            var ps = context.Products.ToList();
            jsonFormatter = new DataContractJsonSerializer(typeof(List<Product>));

            using (FileStream fs = new FileStream("products.json", FileMode.OpenOrCreate))
            {
                jsonFormatter.WriteObject(fs, ps);
            }

            SendEmail(@"deviler.san@yandex.ru", "Бекап бд для клиента", "", new string[] { "orders.json", "orderproducts.json", "products.json" });
        }

        private void SendEmail(string mailAddress, string subject, string text, string[] attachmentPath)
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
                foreach (var f in attachmentPath)
                    m.Attachments.Add(new Attachment(f));
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
