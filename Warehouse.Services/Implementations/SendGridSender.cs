using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Warehouse.Services.Implementations
{
    public class SendGridSender
    {
        public static string SenderEmail { get; set; }
        public static string SenderName { get; set; }

        public static string RecipientEmail { get; set; }
        public static string RecipientName { get; set; }

        public static string Subject { get; set; }

        public static string PlainTextContent { get; set; }
        public static string HtmlContent { get; set; }

        public static string ApiKey { private get; set; }

        public static async void  Send()
        {
            var client = new SendGridClient(ApiKey);

            var msg = MailHelper.CreateSingleEmail(
                new EmailAddress(SenderEmail, SenderName),
                new EmailAddress(RecipientEmail, RecipientName),
                Subject,
                PlainTextContent,
                HtmlContent
            );
            
            var response = await client.SendEmailAsync(msg);
        }
    }
}
