using BlogMVC.Data;
using BlogMVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogMVC.App_Start
{
    public class RoleConfig
    {
       public static void RegisterRoles()
        {
            BlogContext db = new BlogContext();
            RoleStore<Role> roleStore = new RoleStore<Role>(db);
            RoleManager<Role> roleManager = new RoleManager<Role>(roleStore);

            if (!roleManager.RoleExists("Administrator"))
            {
                Role role = new Role("Administrator", "Administrators can add, edit and delete data.");
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Editor"))
            {
                Role role = new Role("Editor", "Editors can only add or edit data.");
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Subscriber"))
            {
                Role role = new Role("Subscriber", "Subscriber can comment");
                roleManager.Create(role);
            }
        }
    }
}