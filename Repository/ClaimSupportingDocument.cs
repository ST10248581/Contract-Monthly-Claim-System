using System;
using System.Collections.Generic;

namespace CMCS.Repository;

public partial class ClaimSupportingDocument
{
    public int ClaimSupportingDocumentId { get; set; }

    public int ClaimId { get; set; }

    public byte[] SupportingDocument { get; set; } = null!;
}
