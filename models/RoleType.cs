namespace CS212FinalProject.Models
{
    // Role enum - persisted as string in the database for readability
    public enum RoleType
    {
        None = 0,
        Customer,
        Receptionist,
        ServiceProvider,
        Manager
    }
}