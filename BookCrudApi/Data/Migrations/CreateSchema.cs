using FluentMigrator;

namespace BookCrudApi.Data.Migrations
{
    [Migration(20032024)]
    public class CreateSchema : Migration
    {
        public override void Down()
        {
            
        }

        public override void Up()
        {
            Create.Table("book")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("title").AsString().NotNullable()
                .WithColumn("author").AsString().NotNullable()
                .WithColumn("genre").AsString().NotNullable();
        }
    }
}
