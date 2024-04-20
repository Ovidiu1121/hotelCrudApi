using FluentMigrator;

namespace HotelCrudApi.Data.Migrations
{
    [Migration(20032024)]
    public class CreateSchema : Migration
    {
        public override void Down()
        {

        }

        public override void Up()
        {
            Create.Table("hotel")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("name").AsString().NotNullable()
                .WithColumn("location").AsString().NotNullable()
                .WithColumn("stars").AsInt32().NotNullable();
        }
    }
}
