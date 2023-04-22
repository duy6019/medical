using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Bravure.Entities;
using Bravure.Entities.Enums;
using Microsoft.AspNetCore.Identity;

namespace Bravure.Models.User
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string DisplayId { get; set; }
        public string FullName { get; set; }
        public string PositionTitle { get; set; }
        public string Position { get; set; }
        public string IdentityNumber { get; set; }
        public Guid DepartmentId { get; set; }
        public Gender Gender { get; set; }
        public string PasswordHash { get; set; }
        public string Password { get; set; }

        public Department Department { get; set; }
    }

    public class UpdateUserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string DisplayId { get; set; }
        public string FullName { get; set; }
        public string PositionTitle { get; set; }
        public string Position { get; set; }
        public string IdentityNumber { get; set; }
        public Guid DepartmentId { get; set; }
        public Gender Gender { get; set; }
        public string PasswordHash { get; set; }
        public string Password { get; set; }
    }
}
