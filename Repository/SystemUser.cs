using System;
using System.Collections.Generic;

namespace CMCS.Repository;

public partial class SystemUser
{
    public int UserId { get; set; }

    public string PasswordHash { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int UserRole { get; set; }

    public virtual ICollection<Claim> ClaimApprovedByProgrammeManagers { get; set; } = new List<Claim>();

    public virtual ICollection<Claim> ClaimLecturers { get; set; } = new List<Claim>();
}
