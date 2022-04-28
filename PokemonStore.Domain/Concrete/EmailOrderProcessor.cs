using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using PokemonStore.Domain.Abstract;
using PokemonStore.Domain.Entities;
using System.Net.Mail;

namespace PokemonStore.Domain.Concrete
{
   public class EmailOrderProcessor:IOrderProcessor
    {

       public void ProcessOrder(OrderContainer container)
       {
           string MailToAddress = container.User_for_Order.Email;
           string MailFromAddress = "Schwaini89@gmail.com";
           bool UseSsl = false;
           string Username = "Schwaini89@gmail.co";
           string Password = "Turk1sh202!";
           string ServerName = "gmauil.com";
           int ServerPort = 587;
           using (var smtpClient = new SmtpClient())
           {
               smtpClient.EnableSsl = UseSsl;
               smtpClient.Host = ServerName;
               smtpClient.Port = ServerPort;
               smtpClient.UseDefaultCredentials = false;
               smtpClient.Credentials = new NetworkCredential(Username, Password);
               StringBuilder body = new StringBuilder()
               .AppendLine("A new order has been submitted")
               .AppendLine(container.order.Date.ToString())
                .AppendLine(container.Pokemon_for_Order.Name)
                 .AppendLine(container.order.Count.ToString());
               MailMessage mailMessage = new MailMessage(
                   MailFromAddress,
                   MailToAddress,
                   "New order submitted!",
                   body.ToString());

               try
               {
                   smtpClient.Send(mailMessage);
               }
               catch
               {

               }
           }
       }

    }
}
