using System;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;

namespace MyRepo
{
    public class Email
    {
        private string SMTP; 
        public Email(string from)
        {
            SMTP = from;
        }
        public void SendEmail(string from, List<string> to, string sub, string body)
        {
            String source = from; // Replace with the sender address.

            // The destination for this email, in this case, the same as the sender.
            List<string> destination = to;

            // The subject line for the email.
            String subject = sub;


            // The email body for recipients with non-HTML email clients.
            //String bodyText = "Amazon SES Test\r\n"
            //                + "This email was sent through Amazon SES "
            //                + "using the AWS SDK for .NET.";
            String bodyText = body; 

            using (var client = new AmazonSimpleEmailServiceClient())
            {
                var sendRequest = new SendEmailRequest
                {
                    Source = source,
                    Destination = new Destination
                    {
                        ToAddresses = destination 
                    },
                    Message = new Message
                    {
                        Subject = new Content(subject),
                        Body = new Body
                        {
                            Text = new Content
                            {
                                Charset = "UTF-8",
                                Data = bodyText
                            }
                        }
                    }
                };
                try
                {
                    Console.WriteLine("Sending email using Amazon SES...");
                    var response = client.SendEmailAsync(sendRequest);
                    Console.WriteLine("The email was sent successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("The email was not sent.");
                    Console.WriteLine("Error message: " + ex.Message);
                }
            }
        }
    }
}
