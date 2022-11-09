using Microsoft.AspNetCore.Mvc;
using PersonalSite2.Models;
using System.Diagnostics;
using MimeKit;
using MailKit.Net.Smtp;


namespace PersonalSite2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Contact()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Contact(ContactViewModel cvm)
        {
            if (!ModelState.IsValid)
            {
                return View(cvm);
            }


            string message = $"You have received a new email from your site's contact form!<br />" +
                $"Sender: {cvm.Name}<br/>Email: {cvm.Email}<br/>Subject: {cvm.Subject}<br/>Message: {cvm.Message}";

            //Create a MimeMessage object to assist with storing/transporting the email information
            //from the contact form.
            var mm = new MimeMessage();

            //Even though the user is the one attempting to send a message to us, the ACTUAL sender
            //of the email is the email user we set up with our hosting provider.

            //We can access the credentials for this email user from our appsettings.json file as shown below:
            mm.From.Add(new MailboxAddress("Sender", _config.GetValue<string>("Credentials:Email:User")));

            //The recipient of this email, will be our personal email address, which is also stored in the appsettings.json
            mm.To.Add(new MailboxAddress("Recipient", _config.GetValue<string>("Credentials:Email:Recipient")));

            //The subject will be the one provided by the user, stored in our cvm object
            mm.Subject = cvm.Subject;

            //The body of the message will be formatted with the string we created above
            mm.Body = new TextPart("HTML") { Text = message };

            //We can set the priority of the message as "urgent" so it will be flagged in our email client
            mm.Priority = MessagePriority.Urgent;

            //We can also add the user's provided email address to the list of reply to addresses.
            //Allows us to reply directly to the person who sent the message instead of our email user.
            mm.ReplyTo.Add(new MailboxAddress("User", cvm.Email));

            //The using directive will create an SmtpClient object used to send the email.
            //Once all of the code inside the using directives scope has been executed,
            //it will close any open connections and dispose of the object automatically.
            using (var client = new SmtpClient())
            {
                //Connect to the mail server using the credentials in our appsettins.json
                client.Connect(_config.GetValue<string>("Credentials:Email:Client"));

                //Login to the mail server using the credentials for our email user
                client.Authenticate(
                //Username
                    _config.GetValue<string>("Credentials:Email:User"),
                //Password
                    _config.GetValue<string>("Credentials:Email:Password")
                    );

                //It's possible the mail server may be down when the user attempts to contact us,
                //so we can encapsulate our code to send the message in a try/catch

                try
                {
                    //Try to send the email
                    client.Send(mm);
                }
                catch (Exception ex)
                {
                    //If there is an issue, we can store an error message in a ViewBag variable
                    //to be displayed in the View
                    ViewBag.ErrorMessage = $"There was an error processing your request. Please" +
                        $"try again later.<br/>Error Message: {ex.StackTrace}";

                    //Return the user to the View with their form info intact.
                    return View(cvm);
                }
            }

            //If all goes well, return a View that displays a confirmation to the user that
            //their email was sent.
            return View("EmailConfirmation", cvm);
        }

        public IActionResult Services()
        {
            return View();
        }
        public IActionResult Classmates()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }

    }

}