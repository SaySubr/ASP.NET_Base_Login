using HomeWork.Context;
using HomeWork.Controller;
using HomeWork.User;
using Microsoft.EntityFrameworkCore;
using System;

namespace ASP_Authorization
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            Sample_0(ref app);

            app.Run();
        }
        
        private static void Sample_0(ref WebApplication app)
        {
            DBAppContext dbContext = new();
            DbUserController Controls = new(dbContext);
            app.Run
            (
                async context =>
                {
                    var responce = context.Response;
                    var request = context.Request;

                    responce.ContentType = "text/html; chaset=utf-8";


                    if (request.Path == "/" || request.Path == "/login")//переход на страницу авторизации
                    {
                        await responce.SendFileAsync("Pages/Login.html");
                    }
                    else if (request.Path == "/post_user_login")//переход на главную страницу
                    {
                        var form = request.Form;
                        string login = form["login"];
                        string password = form["password"];
                        DBUser user = new DBUser();

                        user.Password = password;
                        user.Login = login;

                        if (Controls.IsAuthorized(user))//переход на страницу информации о себе
                        {

                            await responce.SendFileAsync("Pages/Index.html");
                        }
                        else
                        {
                            await responce.WriteAsync($"<div><h3>{login} - {password}: Not Found!</h3></div>");
                        }
                    }
                }
            );
        }
    }
}
