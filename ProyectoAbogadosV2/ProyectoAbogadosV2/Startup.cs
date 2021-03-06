﻿using Microsoft.Owin;
using ProyectoAbogadosV2.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProyectoAbogadosV2.Startup))]
namespace ProyectoAbogadosV2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            crearRolesyUsuariosPredeterminados();

            //Código Introducido para SignalR

            app.MapSignalR();

        }

        // En este método se crearán los Roles predeterminados y el usuario
        // que actuará como Administrador predeterminado
        private void crearRolesyUsuariosPredeterminados()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager =
            new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager =
            new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            // Se crea el rol de Administrador y el usuario Administrador predeterminado
            if (!roleManager.RoleExists("Administrador"))
            {
                // Si no existe, se crea el rol de Administrador
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();

                role.Name = "Administrador";
                roleManager.Create(role);
                // Se crea el Administrador predeterminado que mantendrá el sitio web.
                // Se recomienda cambiar la contraseña al entrar por primera vez
                // en la aplicación Web
                var user = new Models.ApplicationUser();
                user.UserName = "admin@empresa.com";
                user.Email = "admin@empresa.com";
                string userPWD = "admin123";
                var chkUser = userManager.Create(user, userPWD);
                // Agregar el administrador predeterminado al rol de Administrador
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "Administrador");
                }
            }
            // Si no exsiste, se crea el rol Usuario
            if (!roleManager.RoleExists("Usuario"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Usuario";
                roleManager.Create(role);
            }
        }

    }
}