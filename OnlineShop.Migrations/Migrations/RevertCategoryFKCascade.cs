using FluentMigrator;
using System.Data;

namespace OnlineShop.Migrations.Migrations
{
    [Migration(5, "Revert Categories FK to restrict delete (no cascade)")]
    public class RevertCategoryFkCascade : Migration
    {
        public override void Up()
        {
            if (Schema.Table("Categories").Constraint("FK_Categories_Parent").Exists())
            {
                Delete.ForeignKey("FK_Categories_Parent").OnTable("Categories");
            }

            Create.ForeignKey("FK_Categories_Parent")
                .FromTable("Categories").ForeignColumn("ParentCategoryId")
                .ToTable("Categories").PrimaryColumn("Id")
                .OnDeleteOrUpdate(Rule.None);
        }

        public override void Down()
        {
            if (Schema.Table("Categories").Constraint("FK_Categories_Parent").Exists())
            {
                Delete.ForeignKey("FK_Categories_Parent").OnTable("Categories");
            }

            Create.ForeignKey("FK_Categories_Parent")
                .FromTable("Categories").ForeignColumn("ParentCategoryId")
                .ToTable("Categories").PrimaryColumn("Id")
                .OnDeleteOrUpdate(Rule.Cascade);
        }
    }
}