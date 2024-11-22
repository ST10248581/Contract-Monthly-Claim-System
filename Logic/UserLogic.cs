
using CMCS.Models;
using CMCS.Repository;
using Microsoft.AspNetCore.Identity;

namespace CMCS.Logic
{
    public class UserLogic
    {

        public void CreateUser(CreateUserRequest request)
        {
            using (var dm = new DataModel())
            {
                int userRoleId = 0;

                if (request.UserRole.Equals("Lecturer"))
                {
                    userRoleId = 1;
                }

                if (request.UserRole.Equals("Programme Coordinator"))
                {
                    userRoleId = 2;
                }

                if (request.UserRole.Equals("Academic Manager"))
                {
                    userRoleId = 3;
                }


                if (request.UserRole.Equals("HR"))
                {
                    userRoleId = 4;
                }

                var authLogic = new AuthLogic();
                var passwordHash = authLogic.HashPassword(request.Password);

                var existingEmail = dm.SystemUsers.FirstOrDefault(u => u.Email.ToLower() == request.Email.ToLower());
                if (existingEmail != null) throw new Exception($"User with the email '{request.Email}' already exists please use a unique email.");

                dm.SystemUsers.Add(new SystemUser()
                {
                   PasswordHash = passwordHash,
                   FirstName = request.FirstName,
                   LastName = request.LastName,
                   Email = request.Email.ToLower(),
                   UserRole = userRoleId
                   
                });

                dm.SaveChanges();
            }
            
        }
    }
}
