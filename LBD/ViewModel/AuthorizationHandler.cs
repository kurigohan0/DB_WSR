using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LBD.ViewModel
{
    class AuthorizationHandler
    {
        public static int CurrentUserID { get; set; }
        public static string UserName { get; set; }
        public static string Login { get; set; }
        public static Position.EmployeePosition CurrentPosition;

        public Position.EmployeePosition Auth(string login, string pass)
        {
            Model.RentalShopEntities rs = new Model.RentalShopEntities();
            foreach (var item in rs.Users)
            {
                if(item.Login == login && item.Password == pass)
                {
                    foreach (var staff in rs.Staff)
                    {
                        if(login == staff.Login)
                        {
                            if (staff.Position.Replace(" ", "") == "Работник")
                            {
                                CurrentPosition = Position.EmployeePosition.Common;
                                return Position.EmployeePosition.Common;
                            }
                            else
                            {
                                if (staff.Position.Replace(" ", "") == "Менеджер")
                                {
                                    CurrentUserID = staff.Personnel_Id;
                                    CurrentPosition = Position.EmployeePosition.Manager;
                                    UserName = staff.First_Name + " " + staff.Last_Name;
                                    Login = login;

                                    return Position.EmployeePosition.Manager;
                                }

                            }                           
                        }
                    }
                    foreach (var user in rs.Clients)
                    {
                        if (user.Login == login)
                        {
                            UserName = user.First_Name + " " + user.Second_Name;
                            Login = user.Login;
                            CurrentPosition = Position.EmployeePosition.Client;
                            return Position.EmployeePosition.Client;
                        }

                    }
                }
            }
            return Position.EmployeePosition.NotFound;
        }
    }
}
