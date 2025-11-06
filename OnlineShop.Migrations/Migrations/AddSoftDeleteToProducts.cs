using FluentMigrator;

namespace OnlineShop.Migrations.Migrations
{
    [Migration(17, "Add IsDeleted and DeletedAt columns to Products")]
    public class AddSoftDeleteToProducts : Migration
    {
        public override void Up()
        {
            Alter.Table("Products")
                .AddColumn("IsDeleted")
                .AsBoolean()
                .NotNullable()
                .WithDefaultValue(false);

            Alter.Table("Products")
                .AddColumn("DeletedAt")
                .AsDateTime()
                .Nullable();
        }

        public override void Down()
        {
            Delete.Column("DeletedAt").FromTable("Products");
            Delete.Column("IsDeleted").FromTable("Products");
        }
    }
}