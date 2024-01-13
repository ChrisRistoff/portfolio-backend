using System.Text;
using FluentMigrator;

namespace portfolio.Migrations;

[Migration(1)]
public class PersonalInfo : Migration
{
    public override void Up()
    {
        StringBuilder sql = new StringBuilder();

        sql.Append("CREATE TABLE personal_info (");
        sql.Append("id INT NOT NULL,");
        sql.Append("name VARCHAR(50) NOT NULL,");
        sql.Append("email VARCHAR(50) NOT NULL,");
        sql.Append("bio TEXT NOT NULL,");
        sql.Append("github VARCHAR(50) NOT NULL,");
        sql.Append("linkedin VARCHAR(50),");
        sql.Append("image TEXT NOT NULL");
        sql.Append(")");

        Execute.Sql(sql.ToString());
    }

    public override void Down()
    {
        Execute.Sql("DROP TABLE IF EXISTS personal_info");
    }
}
