using BookstoreApplication.Context;
using BookstoreApplication.Models;
using BookstoreApplication.Repository.Interface;
using Dapper;
using mailslurp.Model;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace BookstoreApplication.Repository.Implementation
{
    public class UserRepository:IUserRepository
    {
        public readonly BookDBContext db;
        private readonly DapperContext _context;

        public UserRepository(BookDBContext context, DapperContext _context)
        {
            db = context;
            this._context = _context;
        }


        public string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890"; 
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        public async Task SendEmail(string toEmail, string password)
        {
            // Set up SMTP client
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); 
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("sriharinimk@mitrahsoft.com", "bvgnsmjtsmdertun");//bvgn smjt smde rtun

            // Create email message
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("sriharinimk@mitrahsoft.com");
            mailMessage.To.Add(toEmail);
            mailMessage.Subject = "Password generated successfully";
            mailMessage.IsBodyHtml = true;
            StringBuilder mailBody = new StringBuilder();
            mailBody.AppendFormat($"<h4>Your Password:{password}</h4>");
            //mailBody.AppendFormat("Login here=> https://localhost:7076/User/Login");
            mailBody.AppendFormat(" <a href=https://localhost:7076/User/Login >Login</a>"+" here");
            mailMessage.Body = mailBody.ToString();

            // Send email
            client.Send(mailMessage);
        }

        public async Task<int> Register(User User)
        {
            var password = CreatePassword(6);
            var query = $"if exists(select 1 from USERS where Username =trim( '{User.Username}') or Email= trim('{User.Email}') or ContactNo={User.ContactNo})select 'success' else select 'failure' as res";
            using (var connection = _context.CreateConnection())
            {
                var result = connection.QueryFirstOrDefault<string>(query);
                if (result == "failure")
                {
                    var newUser = new User()
                    {
                        FirstName = User.FirstName,
                        LastName = User.LastName,
                        Username = User.Username,
                        Email = User.Email,
                        Password = password,
                        Address = User.Address,
                        ContactNo = User.ContactNo,
                        RoleId = User.RoleId
                    };
                    await db.Users.AddAsync(newUser);
                    await db.SaveChangesAsync();
                    await SendEmail(User.Email, password);
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        //public async Task<int> Register(User User)
        //{
        //    var query = $"if exists(select 1 from USERS where Username =trim( '{User.Username}') or Email= trim('{User.Email}') or ContactNo={User.ContactNo})select 'success' else select 'failure' as res";
        //    using (var connection = _context.CreateConnection())
        //    {
        //        var result = connection.QueryFirstOrDefault<string>(query);
        //        if (result == "failure")
        //        {
        //            var newUser = new User()
        //            {
        //                FirstName = User.FirstName,
        //                LastName = User.LastName,
        //                Username = User.Username,
        //                Email = User.Email,
        //                Password = User.Password,
        //                Address = User.Address,
        //                ContactNo = User.ContactNo,
        //                RoleId = User.RoleId
        //            };
        //            await db.Users.AddAsync(newUser);
        //            await db.SaveChangesAsync();
        //            return 1;
        //        }
        //        else
        //        {
        //            return 0;
        //        }
        //    }
        //}
        public async Task<User?> GetUserById(int Id)
        {
            var user = await db.Users.FindAsync(Id);
            return user;
        }

        public async Task<int> UpdateUser(int UserId, User user)
        {
            var query = $"if exists(select 1 from USERS where Username = '{user.Username}' and UserId != {UserId})select 'success' else select 'failure' as res";
            var existing = await db.Users.FindAsync(UserId);
            using (var connection = _context.CreateConnection())
            {
                var result = connection.QueryFirstOrDefault<string>(query);
                
                if (result == "failure")
                {
                    if (existing != null)
                    {
                        existing.FirstName = user.FirstName;
                        existing.LastName = user.LastName;
                        existing.Email = user.Email;
                        existing.Password = user.Password;
                        existing.Address = user.Address;
                        existing.ContactNo = user.ContactNo;
                        await db.SaveChangesAsync();
                        
                    }
                    return 1;
                }
                else
                {
                    return 0;
                }


            }
            
            
        }

    }
}

