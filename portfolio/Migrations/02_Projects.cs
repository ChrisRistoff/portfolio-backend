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
        sql.Append("id SERIAL PRIMARY KEY,");
        sql.Append("name VARCHAR(100) NOT NULL,");
        sql.Append("tagline VARCHAR(100) NOT NULL,");
        sql.Append("description TEXT NOT NULL,");
        sql.Append("image TEXT NOT NULL,");
        sql.Append("repo VARCHAR(50) NOT NULL,");
        sql.Append("link VARCHAR(100) NOT NULL");
        sql.Append("tech_stack TEXT[] NOT NULL,");
        sql.Append("project_type ENUM('BACK-END', 'FRONT-END') NOT NULL");
        sql.Append(")");
    }

    public override void Down()
    {
        Delete.Table("projects");
    }
}
