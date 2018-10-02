﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ncc.China.Services.Identity.Data
{
    [Table("login_user")]
    public class LoginUser
    {
        [Column("id", TypeName = "char(32)"), Required]
        public string Id { get; set; } = Guid.NewGuid().ToString("N");

        [Column("username", TypeName = "varchar(50)"), Required]
        public string Username { get; set; }

        [Column("email", TypeName = "varchar(100)"), Required]
        public string Email { get; set; }

        [Column("password", TypeName = "varchar(100)"), Required]
        public string Password { get; set; }

        [Column("salt", TypeName = "varchar(50)"), Required]
        public string Salt { get; set; }

        [Column("utc_created", TypeName = "datetime"), Required]
        public DateTime UtcCreated { get; set; } = DateTime.UtcNow;

        [Column("utc_updated", TypeName = "datetime "), Required]
        public DateTime UtcUpdated { get; set; }
    }
}
