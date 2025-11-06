using FluentMigrator;

namespace OnlineShop.Migrations.Migrations
{
    [Migration(16, "Add Featured column to Products")]
    public class AddFeaturedToProducts : Migration
    {
        public override void Up()
        {
            Alter.Table("Products")
                .AddColumn("Featured")
                .AsBoolean()
                .NotNullable()
                .WithDefaultValue(false);
        }

        public override void Down()
        {
            Delete.Column("Featured").FromTable("Products");
        }
    }
}