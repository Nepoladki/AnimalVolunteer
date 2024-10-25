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

        public static class Pets
        {
            public const string Create = "volunteers.pets.create";
            public const string Read = "volunteers.pets.read";
            public const string Update = "volunteers.pets.update";
            public const string HardDelete = "volunteers.pets.hard.delete";
            public const string SoftDelete = "volunteers.pets.soft.delete";
        }
    }
    public static class Species
    {
        public const string Create = "species.create";
        public const string Read = "species.read";
        public const string Update = "species.update";
        public const string HardDelete = "species.hard.delete";
    }
    public static class Accounts
    {
        
    }
}
