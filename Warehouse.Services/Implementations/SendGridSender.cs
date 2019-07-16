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

        public static async void  Send()
        {
            //TODO Stoyan Lupov 20 June, 2019 Extract to user secret
            var apiKey = "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";

            var client = new SendGridClient(apiKey);

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
