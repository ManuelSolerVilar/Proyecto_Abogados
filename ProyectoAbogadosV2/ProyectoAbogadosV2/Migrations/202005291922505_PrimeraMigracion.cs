namespace ProyectoAbogadosV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrimeraMigracion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actuacion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExpedienteId = c.Int(nullable: false),
                        FechaInicio = c.DateTime(nullable: false),
                        FechaCierre = c.DateTime(),
                        Descripcion = c.String(),
                        NotificacionCliente = c.String(),
                        NotificacionJuzgado = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Expediente", t => t.ExpedienteId)
                .Index(t => t.ExpedienteId);
            
            CreateTable(
                "dbo.Documento",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExpedienteId = c.Int(nullable: false),
                        Descripcion = c.String(),
                        Documentacion = c.String(),
                        Actuacion_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Actuacion", t => t.Actuacion_Id)
                .Index(t => t.Actuacion_Id);
            
            CreateTable(
                "dbo.Expediente",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TituloExpediente = c.String(nullable: false),
                        FechaInicio = c.DateTime(nullable: false),
                        FechaCierre = c.DateTime(),
                        Descripcion = c.String(),
                        AbogadoId = c.Int(nullable: false),
                        ClienteId = c.Int(nullable: false),
                        JurisdiccionId = c.Int(nullable: false),
                        ProvisionFondos = c.Int(nullable: false),
                        TotalMinuta = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Abogado", t => t.AbogadoId)
                .ForeignKey("dbo.Cliente", t => t.ClienteId)
                .ForeignKey("dbo.Jurisdiccion", t => t.JurisdiccionId)
                .Index(t => t.AbogadoId)
                .Index(t => t.ClienteId)
                .Index(t => t.JurisdiccionId);
            
            CreateTable(
                "dbo.Abogado",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NombreAbogado = c.String(nullable: false),
                        ApellidosAbogado = c.String(nullable: false),
                        NifAbogado = c.String(nullable: false),
                        MovilAbogado = c.String(nullable: false),
                        FijoAbogado = c.String(),
                        Email = c.String(nullable: false),
                        Direccion_Cliente = c.String(),
                        Poblacion_Cliente = c.String(),
                        Cp_Cliente = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cliente",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Apellidos = c.String(nullable: false),
                        NifCliente = c.String(nullable: false),
                        MovilCliente = c.String(nullable: false),
                        FijoCliente = c.String(),
                        FechaIngreso = c.DateTime(),
                        FechaBaja = c.DateTime(),
                        Email = c.String(nullable: false),
                        Descripcion_Cliente = c.String(),
                        Direccion_Cliente = c.String(),
                        Poblacion_Cliente = c.String(),
                        Cp_Cliente = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Jurisdiccion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Expediente", "JurisdiccionId", "dbo.Jurisdiccion");
            DropForeignKey("dbo.Expediente", "ClienteId", "dbo.Cliente");
            DropForeignKey("dbo.Actuacion", "ExpedienteId", "dbo.Expediente");
            DropForeignKey("dbo.Expediente", "AbogadoId", "dbo.Abogado");
            DropForeignKey("dbo.Documento", "Actuacion_Id", "dbo.Actuacion");
            DropIndex("dbo.Expediente", new[] { "JurisdiccionId" });
            DropIndex("dbo.Expediente", new[] { "ClienteId" });
            DropIndex("dbo.Expediente", new[] { "AbogadoId" });
            DropIndex("dbo.Documento", new[] { "Actuacion_Id" });
            DropIndex("dbo.Actuacion", new[] { "ExpedienteId" });
            DropTable("dbo.Jurisdiccion");
            DropTable("dbo.Cliente");
            DropTable("dbo.Abogado");
            DropTable("dbo.Expediente");
            DropTable("dbo.Documento");
            DropTable("dbo.Actuacion");
        }
    }
}
