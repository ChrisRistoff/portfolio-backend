using System.Text;
using FluentMigrator;

namespace portfolio.Migrations;

[Migration(2)]
public class Projects : Migration
{
    public override void Up()
    {
        StringBuilder sql = new StringBuilder();

        sql.Append("CREATE TABLE projects (");
        sql.Append("id INT NOT NULL,");
        sql.Append("name VARCHAR(100) NOT NULL,");
        sql.Append("tagline VARCHAR(100) NOT NULL,");
        sql.Append("description TEXT NOT NULL,");
        sql.Append("image TEXT NOT NULL,");
        sql.Append("repo VARCHAR(100) NOT NULL,");
        sql.Append("link VARCHAR(100) NOT NULL,");
        sql.Append("tech_stack TEXT[] NOT NULL,");
        sql.Append("project_type TEXT NOT NULL");
        sql.Append(");");

        Execute.Sql(sql.ToString());
    }

    public override void Down()
    {
        Execute.Sql("DROP TABLE IF EXISTS projects");
    }
}
