using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Bravure.Entities.Seed
{
    public static class SeedData
    {
        private const string _defaultPasswordd = "!Q@W3e4r";
        public static void EnsureSeedData(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<BravureDbContext>();

                context.Database.Migrate();

                var env = serviceProvider.GetService<IWebHostEnvironment>();
                var configuration = serviceProvider.GetService<IConfiguration>();
                var contentPath = env.ContentRootPath;

                var services = scope.ServiceProvider;
                var config = serviceProvider.GetService<IConfiguration>();

                var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
                if (!context.Roles.Any())
                {
                    SeedRoles(roleMgr);
                }

                if (!context.Users.Any())
                {
                    var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                    SeedUsers(context, contentPath, userMgr);
                }
            }
        }

        private static void SeedUsers(BravureDbContext context, string contentPath, UserManager<ApplicationUser> userMgr)
        {
            // try
            // {
            //     var csvPath = Path.Combine(contentPath, "data\\seeddata", @"user.csv");
            //     var users = GetData(new string[] { "firstname", "lastname", "email", "userid" }, csvPath, (string[] x) =>
            //     {
            //         var firstname = x[0].Replace("\'", "").Trim('"').Trim();
            //         var lastname = x[1].Replace("\'", "").Trim('"').Trim();
            //         return new
            //         {
            //             UserName = $"{firstname.Split(' ').Last()}.{lastname.Split(' ').Last()}".ToLower(),
            //             FirstName = x[0],
            //             LastName = x[1],
            //             FullName = $"{x[0]} {x[1]}",
            //             Email = $"{firstname.Split(' ').Last()}.{lastname.Split(' ').Last()}@gmail.com".ToLower(),
            //             UserId = x.Length > 3 ? GetGuid(x[3]) : new Guid(),
            //             DateOfBirth = DateTime.Now
            //         };
            //     }, () => null);

            //     foreach (var s in users)
            //     {
            //         var user = userMgr.FindByNameAsync(s.UserName).Result;
            //         if (user == null)
            //         {
            //             user = new ApplicationUser()
            //             {
            //                 Id = s.UserId,
            //                 UserName = s.UserName,
            //                 FullName = s.FullName,
            //                 Email = s.Email
            //             };
            //             var result = userMgr.CreateAsync(user, _defaultPasswordd).Result;
            //             userMgr.AddToRoleAsync(user, "staff").Wait();
            //             if (!result.Succeeded)
            //             {
            //                 throw new Exception(result.Errors.First().Description);
            //             }
            //             context.SaveChanges();
            //         }
            //     }
            //     context.SaveChanges();
            // }
            // catch (Exception ex)
            // {
            //     throw ex;
            // }
        }

        private static void SeedRoles(RoleManager<ApplicationRole> roleMgr)
        {
            string[] roles = { "inmate", "staff", "external", "host" };

            foreach (var role in roles)
            {
                var exists = roleMgr.RoleExistsAsync(role).Result;

                if (!exists)
                {
                    roleMgr.CreateAsync(new ApplicationRole
                    {
                        Name = role,
                        NormalizedName = role,
                    }).Wait();
                }
            }
        }

        private static List<T> GetData<T>(string[] headers, string path, Func<string[], T> mapping, Func<List<T>> fallback)
        {
            if (!File.Exists(path))
            {
                return fallback();
            }

            string[] csvheaders;
            try
            {
                csvheaders = GetHeaders(headers, path);
            }
            catch (Exception ex)
            {
                //log Exception ex
                return fallback();
            }

            return File.ReadAllLines(path)
                        .Skip(1)
                        .Select(x => mapping(x.Split(',')))
                        .ToList();
        }

        private static string[] GetHeaders(string[] requiredHeaders, string csvfile)
        {
            var csvheaders = File.ReadLines(csvfile).First().ToLowerInvariant().Split(',');

            if (csvheaders.Count() != requiredHeaders.Count())
            {
                throw new Exception($"requiredHeader count '{requiredHeaders.Count()}' is different then read header '{csvheaders.Count()}'");
            }

            foreach (var requiredHeader in requiredHeaders)
            {
                if (!csvheaders.Contains(requiredHeader.ToLowerInvariant()))
                {
                    throw new Exception($"does not contain required header '{requiredHeader}'");
                }
            }

            return csvheaders;
        }

        private static Guid GetGuid(string guid)
        {
            if (!Guid.TryParse(guid, out Guid result))
                result = new Guid();
            return result;
        }

        private static DisplayAttribute GetDisplayAttribute(object value)
        {
            Type type = value.GetType();

            // Get the enum field.
            var field = type.GetField(value.ToString());
            return field == null ? null : field.GetCustomAttribute<DisplayAttribute>();
        }

        private static void SeedDataFromJson<T>(BravureDbContext context, string filePath) where T : class
        {
            if (!context.Set<T>().Any())
            {
                if (File.Exists(filePath))
                {
                    var content = File.ReadAllText(filePath);
                    var entities = JsonConvert.DeserializeObject<List<T>>(content);

                    context.Set<T>().AddRange(entities);
                    context.SaveChanges();
                }
            }
        }
    }
}
