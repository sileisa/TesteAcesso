namespace TesteFiltrosDeAcao.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ACESSOes",
                c => new
                    {
                        LoginId = c.Int(nullable: false, identity: true),
                        EMAIL = c.String(),
                        SENHA = c.String(),
                        PERFIL = c.String(),
                        NOME = c.String(),
                        SOBRENOME = c.String(),
                    })
                .PrimaryKey(t => t.LoginId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ACESSOes");
        }
    }
}
