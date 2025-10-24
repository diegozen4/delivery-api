
namespace Infrastructure.Persistence;

public static class MigrationConstants
{
    public static class Schemas
    {
        public const string Auth = "auth";
        public const string Commerce = "commerce";
        public const string Delivery = "delivery";
        public const string Order = "order";
        public const string Payments = "payments";
        public const string Shared = "shared";
    }

    public static class Columns
    {
        public const string CreatedAt = "CreatedAt";
        public const string CreatedBy = "CreatedBy";
        public const string UpdatedAt = "UpdatedAt";
        public const string UpdatedBy = "UpdatedBy";
        public const string Status = "Status";
    }
    
    public static class DataTypes
    {
        public const string TimestampTz = "timestamp with time zone";
        public const string Text = "text";
        public const string Uuid = "uuid";
        public const string Integer = "integer";
        public const string Numeric = "numeric";
        public const string Boolean = "boolean";
        public const string Varchar256 = "character varying(256)";
    }
}
