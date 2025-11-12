using FluentMigrator;

namespace OnlineShop.Migrations.Migrations
{
    [Migration(21, "Convert Product timestamp columns to timestamptz")]
    public class ConvertProductTimestampsToTimestamptz : Migration
    {
        public override void Up()
        {
            Alter.Column("CreatedAt").OnTable("Products").AsCustom("timestamptz").NotNullable();

            Alter.Column("UpdatedAt").OnTable("Products").AsCustom("timestamptz").Nullable();

            Alter.Column("DeletedAt").OnTable("Products").AsCustom("timestamptz").Nullable();
        }

        public override void Down()
        { 
            Alter.Column("CreatedAt").OnTable("Products").AsCustom("timestamp").NotNullable();
            Alter.Column("UpdatedAt").OnTable("Products").AsCustom("timestamp").Nullable();
            Alter.Column("DeletedAt").OnTable("Products").AsCustom("timestamp").Nullable();
        }
    }
}