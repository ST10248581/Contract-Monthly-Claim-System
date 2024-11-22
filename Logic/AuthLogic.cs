using System.Security.Cryptography;
using System.Text;
using CMCS.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CMCS.Logic
{
    public class AuthLogic
    {
        public string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        public int AuthenticateUser(string userIdString)
        {
            if (string.IsNullOrEmpty(userIdString)) return 0;
            return int.Parse(userIdString);
        }

        public bool authorizeSubmitClaim(int userRoleId)
        {
            using (var dm = new DataModel())
            {
                var userRole = dm.SystemUserRoles.FirstOrDefault(R => R.SystemUserRoleId ==  userRoleId);
               if (userRole == null) throw new Exception("User Role Not Found.");

                return userRole.CanSubmitClaim;
            }
        }

        public bool authorizeProcessClaim(int userRoleId)
        {
            using (var dm = new DataModel())
            {
                var userRole = dm.SystemUserRoles.FirstOrDefault(R => R.SystemUserRoleId == userRoleId);
                if (userRole == null) throw new Exception("User Role Not Found.");

                return userRole.CanProcessClaim;
            }
        }

        public bool authorizeVeiwClaim(int userRoleId)
        {
            using (var dm = new DataModel())
            {
                var userRole = dm.SystemUserRoles.FirstOrDefault(R => R.SystemUserRoleId == userRoleId);
                if (userRole == null) throw new Exception("User Role Not Found.");

                return userRole.CanVeiwClaim;
            }
        }

        public bool authorizeUpdateUser(int userRoleId)
        {
            using (var dm = new DataModel())
            {
                var userRole = dm.SystemUserRoles.FirstOrDefault(R => R.SystemUserRoleId == userRoleId);
                if (userRole == null) throw new Exception("User Role Not Found.");

                return userRole.CanUpdateUserDetails;
            }
        }

    }
}
