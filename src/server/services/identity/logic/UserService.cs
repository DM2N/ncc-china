﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Ncc.China.Services.Identity.Loigc
{
    using Http.Message;
    using Data;
    using Dto;
    using Common;

    public class UserService : IDisposable
    {
        private IdentityDbContext _context;

        public UserService(IdentityDbContext context)
        {
            _context = context;
        }

        public IList<LoginUser> GetUsers()
        {
            using (_context)
            {
                var users = _context.LoginUsers.ToArray();
                return users;
            }
        }

        public object GetUser(string id)
        {
            using (_context)
            {
                var user = _context.LoginUsers
                    .FirstOrDefault(u => u.Id.Equals(id));
                if(user == null)
                {
                    return new FailedResponseMessage("未找到该用户");
                }
                return new SucceededResponseMessage(new {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    UtcCreated = user.UtcCreated,
                    UtcUpdated = user.UtcCreated
                });
            }
        }

        public object Login(LoginDto dto)
        {
            using (_context)
            {
                var user = _context.LoginUsers
                    .FirstOrDefault(u => u.Username.Equals(dto.Login) ||
                        u.Email.Equals(dto.Login));
                if (user == null)
                {
                    return new FailedResponseMessage("登录名错误");
                }
                var encode = CryptoUtil.Encrypt(dto.Password, user.Salt);
                if (!encode.Equals(user.Password))
                {
                    return new FailedResponseMessage("密码错误");
                }
                return new SucceededResponseMessage(user.Id);
            }
        }

        public object Register(RegisterDto dto)
        {
            using (_context)
            {
                var hasUser = _context.LoginUsers
                    .FirstOrDefault(u => u.Username.Equals(dto.Username));
                if (hasUser != null)
                {
                    return new FailedResponseMessage("该用户名已注册");
                }

                hasUser = _context.LoginUsers
                    .FirstOrDefault(u => u.Email.Equals(dto.Email));
                if (hasUser != null)
                {
                    return new FailedResponseMessage("该邮箱已注册");
                }
                var salt = CryptoUtil.CreateSalt(10);
                var encode = CryptoUtil.Encrypt(dto.Password, salt);
                var user = new LoginUser
                {
                    // Id = Guid.NewGuid().ToString().Replace("-", ""),
                    Username = dto.Username,
                    Email = dto.Email,
                    Password = encode,
                    Salt = salt,
                    UtcCreated = DateTime.UtcNow,
                    UtcUpdated = DateTime.UtcNow,
                };
                _context.LoginUsers.Add(user);
                if (_context.SaveChanges() <= 0)
                {
                    return new FailedResponseMessage("数据库操作异常");
                }
                return new SucceededResponseMessage(user.Id);
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
