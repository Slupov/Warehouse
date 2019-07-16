using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Warehouse.Services.Implementations
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            SendGridSender.SenderEmail = "warehouse.stores222@gmail.com";
            SendGridSender.SenderName  = "Warehouses";

            //TODO Stoyan Lupov 16 July, 2019 Change email to passed-in parameter email
            SendGridSender.RecipientEmail = "stoyan.lupov@gmail.com";
            SendGridSender.RecipientName  = "Stoyan Lupov";

            SendGridSender.Subject = "Testing WAREHOUSE Send grid emails";

            SendGridSender.PlainTextContent = "Testing plain text";
            SendGridSender.HtmlContent = "<strong>Testing HTML content of email</strong>" + message;

            SendGridSender.Send();

            return Task.CompletedTask;
        }
    }
}
