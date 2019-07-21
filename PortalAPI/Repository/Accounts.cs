using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using PortalAPI.Models;

namespace PortalAPI.Repository
{
    public class Accounts
    {
        private readonly Portal_DBEntities ctx;

        public Accounts()
        {
            ctx = new Portal_DBEntities();
        }

        public bool Login(string userName, string password)
        {
            try
            {
                #region  Password Hashing 
                string hashPassword = Crypto.Hash(password);
                #endregion

                var userInfo = ctx.CIFOnlineUsers.Where(x => x.Username == userName && x.Password == hashPassword).FirstOrDefault();
                if (userInfo != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}