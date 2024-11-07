using Expense_Tracker_API.Entity;
using Expense_Tracker_API.Entity.DBContext;
using Expense_Tracker_API.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace Expense_Tracker_API.Repository.Implement
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext context;
        public UserRepository(UserContext context)
        {
            this.context = context; 
        }

        public async Task<IEnumerable<Users>> Get()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<Users> Get(int id)
        {
            return await context.Users.FirstOrDefaultAsync(e => e.UserID == id);
        }

        public async Task<bool> Add(Users entity)
        {

            Users User = await context.Users.FirstOrDefaultAsync(e => e.Email == entity.Email || e.MobileNumber == entity.MobileNumber);

            //Users User = await context.Users.FirstOrDefaultAsync(e => (e.UserName == user.UserName && e.Password == user.Password) );

            if (User == null)
            {
                //entity.Password = RandomString(8, false);

                await context.Users.AddAsync(entity);
                await context.SaveChangesAsync();
                SendEmail(entity);
                return true;
            }
            return false;
        }


        public void SendEmail(Users user, string subject="Expense Tracker Account Created")
        {
            // Set up SMTP client
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("giritharan.d@mitrahsoft.com", "jgvb oulh lyra tsoe");

           

            // Create email message
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("giritharan.d@mitrahsoft.com");
            mailMessage.To.Add(user.Email);
            mailMessage.CC.Add("giritharan01062002@gmail.com");
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;

            StringBuilder mailBody = new StringBuilder();
           
            string body = $"<h3>Welcome to Expense Tracker</h3>" +
               $"Hi! <b>{user.UserName}</b>,you have successfully created account with us.<br><br> Temporary Password: <b>{user.Password}</b>" +
               $"<br>Use this link to login : https://localhost:7269/ <br><br>'Save money save happiness...'." +
               $"<br><p>Thank you For Registering account</p>";


            mailBody.AppendFormat(body);
            mailMessage.Body = mailBody.ToString();

            // Send email
            client.Send(mailMessage);
        }


        public async Task<bool> Edit(Users entity)
        {
            Users UserDB = await context.Users.FirstOrDefaultAsync(e => (e.Email == entity.Email || e.MobileNumber == entity.MobileNumber) && e.UserID != entity.UserID);

            if (UserDB != null)
                return false;
            
                Users user = await Get(entity.UserID);

                user.UserName = entity.UserName;
                user.Password = entity.Password;
                user.Gender = entity.Gender;
                user.Email = entity.Email;
                user.MobileNumber = entity.MobileNumber;

                context.Users.Update(user);
                await context.SaveChangesAsync();
            return true;
        }
    }
}
