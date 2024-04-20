using FluentMigrator;

namespace BookCrudApi.Data.Migrations
{
    [Migration(20032024654)]
    public class TestMigrate : Migration
    {
        public override void Down()
        {
           
        }

        public override void Up()
        {
            Execute.Script(@"./Data/Scripts/data.sql");
        }
    }
}
