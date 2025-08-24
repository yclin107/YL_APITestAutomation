namespace API.TestBase.Models.PPSProformaModels.Entities.Proformas;
public enum ProformaStatus
{
    None = 0,
    NeedsReview = 10,
    InReview = 20,
    Urgent = 30,
    ApprovalPending = 40,
    Completed = 50,
    Submitted = 60,
    Approved = 70,
    Rejected = 80,
    Deferred = 90,
    Undeferred = 95,
    Billed = 100,
    Archived = 110,
    Closed = 120
}