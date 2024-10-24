namespace AnimalVolunteer.Framework.Authorization;

public static class Permissions
{
    public static class Volunteers
    {
        public const string Create = "volunteers.create";
        public const string Read = "volunteers.read";
        public const string Update = "volunteers.update";
        public const string HardDelete = "volunteers.hard.delete";
        public const string SoftDelete = "volunteers.soft.delete";
    }
    public static class Species
    {
        public const string Create = "species.create";
        public const string Read = "species.read";
        public const string Update = "species.update";
        public const string SoftDelete = "species.soft.delete";
        public const string HardDelete = "species.hard.delete";
    }
    public static class Accounts
    {
        
    }
}
