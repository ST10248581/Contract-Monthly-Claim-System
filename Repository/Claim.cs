using System;
using System.Collections.Generic;

namespace CMCS.Repository;

public partial class Claim
{
    public int ClaimId { get; set; }

    public int LecturerId { get; set; }

    public DateOnly ClaimDate { get; set; }

    public decimal HourlyRate { get; set; }

    public int HoursWorked { get; set; }

    public string Status { get; set; } = null!;

    public int? ApprovedByProgrammeManagerId { get; set; }

    public DateOnly? ReviewedDate { get; set; }

    public virtual SystemUser? ApprovedByProgrammeManager { get; set; }

    public virtual SystemUser Lecturer { get; set; } = null!;
}
