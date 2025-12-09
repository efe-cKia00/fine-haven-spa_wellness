namespace CS212FinalProject.Models
{
    // Role enum - persisted as string in the database for readability
    public enum StatusType
    {
        Pending = 0,
        NoShow,
        CheckedIn,
        Completed
    }
}