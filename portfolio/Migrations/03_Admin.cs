using System.Text;
using FluentMigrator;

namespace portfolio.Migrations;

[Migration(3)]
public class Admin: Migration
{
    public override void Up()
    {
        StringBuilder sql = new StringBuilder();

        sql.Append("CREATE TABLE admin (");
        sql.Append("username VARCHAR(100) NOT NULL,");
        sql.Append("password VARCHAR(100) NOT NULL,");
        sql.Append("role VARCHAR(100) NOT NULL");
        sql.Append(");");

        Execute.Sql(sql.ToString());
    }

    public override void Down()
    {
        Execute.Sql("DROP TABLE IF EXISTS admin");
    }
}
