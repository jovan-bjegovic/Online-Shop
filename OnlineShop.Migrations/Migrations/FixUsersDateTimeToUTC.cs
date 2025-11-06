
using FluentMigrator;

namespace OnlineShop.Migrations.Migrations;

[Migration(14, "Fix Users DateTime to UTC")]
public class FixUsersDateTimeUtc : Migration
{
    public override void Up()
    {
        Execute.Sql(@"
                UPDATE ""Users""
                SET ""CreatedAt"" = COALESCE(""CreatedAt"" AT TIME ZONE 'UTC', now() AT TIME ZONE 'UTC'),
                    ""RefreshTokenExpiry"" = COALESCE(""RefreshTokenExpiry"" AT TIME ZONE 'UTC', NULL)
                WHERE TRUE;
            ");
    }

    public override void Down()
    {
    }
}