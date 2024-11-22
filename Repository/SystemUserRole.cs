using System;
using System.Collections.Generic;

namespace CMCS.Repository;

public partial class SystemUserRole
{
    public int SystemUserRoleId { get; set; }

    public string RoleTitle { get; set; } = null!;

    public bool CanSubmitClaim { get; set; }

    public bool CanProcessClaim { get; set; }

    public bool CanVeiwClaim { get; set; }

    public bool CanUpdateUserDetails { get; set; }
}
