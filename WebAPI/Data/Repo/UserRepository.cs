using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Interfaces;
using WebAPI.Models;

namespace WebAPI.Data.Repo
{
    public class UserRepository : IUserRepository
    {
        private readonly Dbclass db ;
        public UserRepository(Dbclass db)
        {
            this.db = db;
            
        }
        public async Task<User> Authenticate(string Username, string PasswordText)
        {
           var user=await db.Users.FirstOrDefaultAsync(x => x.Username == Username);

           if(user == null || user.PasswordKey==null)
            return null;
           
           if(!MatchPasswordHash(PasswordText,user.Password,user.PasswordKey))
            return null;
           
           return user;
        }


        private bool MatchPasswordHash(string passwordText, byte[] Password, byte[] passwordKey)
        {
             using(var hmac=new HMACSHA512(passwordKey)){
               
              var  PasswordHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passwordText));
              for(int i=0;i<PasswordHash.Length;i++){
                if(PasswordHash[i] !=Password[i])
                return false;
              }
              return true;

            }
        }

        public void Register(string Username, string Password)
        {
            byte[] PasswordHash,PasswordKey;
            using(var hmac=new HMACSHA512()){
                PasswordKey=hmac.Key;
                PasswordHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));

            }
            User user=new User();
            user.Username=Username;
            user.Password=PasswordHash;
            user.PasswordKey=PasswordKey;
            db.Users.Add(user);

            
        }

        public async Task<bool> UserAlreadyExists(string Username)
        {
            return await db.Users.AnyAsync(x=>x.Username==Username);
        }
    }
}