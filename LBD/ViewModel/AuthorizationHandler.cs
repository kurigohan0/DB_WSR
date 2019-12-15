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
                                return Position.EmployeePosition.Common;
                            else
                            {
                                if (staff.Position.Replace(" ", "") == "Менеджер")
                                {
                                    CurrentUserID = staff.Personnel_Id;
                                    UserName = staff.First_Name + " " + staff.Last_Name;
                                    Login = login;
                                    
                                    return Position.EmployeePosition.Manager;
                                }
                                else
                                {
                                    foreach (var user in rs.Clients)
                                    {
                                        if (user.Login == login)
                                            return Position.EmployeePosition.Client;

                                    }
                                }
                            }                           
                        }
                    }
                }
            }
            return Position.EmployeePosition.NotFound;
        }
    }
}
