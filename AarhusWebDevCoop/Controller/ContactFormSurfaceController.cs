using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Umbraco.Core.Models;
using AarhusWebDevCoop.ViewModels;
using System.Net.Mail;

namespace AarhusWebDevCoop.Controller
{
    public class ContactFormSurfaceController : SurfaceController
    {
        // GET: ContactFormSurface
        public ActionResult Index() {
            return PartialView("ContactForm", new ContactForm());
        }

        [HttpPost]
        public ActionResult HandleSubmitForm(ContactForm form) {
            
            if (!ModelState.IsValid) { return CurrentUmbracoPage(); }
            IContent comment = Services.ContentService.CreateContent(form.Subject, CurrentPage.Id, "Message");

            MailMessage message = new MailMessage();
            message.To.Add("uldum42@gmail.com");
            message.Subject = form.Subject;
            message.From = new MailAddress(form.Email, form.Name);
            message.Body = form.Message;

            //Create the message to that is to be send to server.
            comment.SetValue("MessageName", form.Name);
            comment.SetValue("Email", form.Email);
            comment.SetValue("Subject", form.Subject);
            comment.SetValue("MessageContent", form.Message);

            //Saves it to the backoffice.
            Services.ContentService.Save(comment);

            using (SmtpClient smtp = new SmtpClient()){
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential("uldum42@gmail.com", "nqaxlyajpwrmstny");

                smtp.Send(message);
                TempData["success"] = true; 
            }
            return RedirectToCurrentUmbracoPage();
        }
    }
}