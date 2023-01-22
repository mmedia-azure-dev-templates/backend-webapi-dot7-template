using Boilerplate.Application.Common;
using Boilerplate.Domain.Entities;
using Boilerplate.Infrastructure.Configuration;
using EntityFramework.Exceptions.SqlServer;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Boilerplate.Infrastructure.Context;

public class ApplicationDbContext : DbContext, IContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Hero> Heroes { get; set; } = null!;

    public DbSet<User> Users { get; set; } = null!;

    /*public virtual DbSet<Articulo> Articulos { get; set; }

    public virtual DbSet<Catalogo> Catalogos { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Contadore> Contadores { get; set; }

    public virtual DbSet<Documento> Documentos { get; set; }

    public virtual DbSet<FailedJob> FailedJobs { get; set; }

    public virtual DbSet<Identificacione> Identificaciones { get; set; }

    public virtual DbSet<Inscription> Inscriptions { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Migration> Migrations { get; set; }

    public virtual DbSet<ModelHasPermission> ModelHasPermissions { get; set; }

    public virtual DbSet<ModelHasRole> ModelHasRoles { get; set; }

    public virtual DbSet<OauthAccessToken> OauthAccessTokens { get; set; }

    public virtual DbSet<OauthAuthCode> OauthAuthCodes { get; set; }

    public virtual DbSet<OauthClient> OauthClients { get; set; }

    public virtual DbSet<OauthPersonalAccessClient> OauthPersonalAccessClients { get; set; }

    public virtual DbSet<OauthRefreshToken> OauthRefreshTokens { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Orderproduct> Orderproducts { get; set; }

    public virtual DbSet<PasswordReset> PasswordResets { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<PermissionsCompany> PermissionsCompanies { get; set; }

    public virtual DbSet<PersonalAccessToken> PersonalAccessTokens { get; set; }

    public virtual DbSet<Postulant> Postulants { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<Ubicaciongeografica> Ubicaciongeograficas { get; set; }

    public virtual DbSet<User> Users { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Articulo>(entity =>
        {
            entity.HasKey(e => e.Artid).HasName("articles_pkey");

            entity.ToTable("articulos");

            entity.Property(e => e.Artid).HasColumnName("ARTID");
            entity.Property(e => e.Artabrevia)
                .HasMaxLength(150)
                .HasColumnName("ARTABREVIA");
            entity.Property(e => e.Artcosto)
                .HasPrecision(14, 2)
                .HasColumnName("ARTCOSTO");
            entity.Property(e => e.Artdiscontinued).HasColumnName("ARTDISCONTINUED");
            entity.Property(e => e.Artmarca).HasColumnName("ARTMARCA");
            entity.Property(e => e.Artmeta)
                .HasColumnType("json")
                .HasColumnName("ARTMETA");
            entity.Property(e => e.Artnombre)
                .HasMaxLength(150)
                .HasColumnName("ARTNOMBRE");
            entity.Property(e => e.Artnotas)
                .HasMaxLength(150)
                .HasColumnName("ARTNOTAS");
            entity.Property(e => e.Artproveedor).HasColumnName("ARTPROVEEDOR");
            entity.Property(e => e.Artsku)
                .HasMaxLength(10)
                .HasColumnName("ARTSKU");
        });

        modelBuilder.Entity<Catalogo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("catalogos_pkey");

            entity.ToTable("catalogos", tb => tb.HasComment("TABLA MAESTRA CATALOGO DEL SISTEMA CONTIENE DE TODO CONFIGURACIONES"));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Idpadre).HasColumnName("idpadre");
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnName("nombre");
            entity.Property(e => e.Valor)
                .HasColumnType("jsonb")
                .HasColumnName("valor");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Ctoid).HasName("contacts_pkey");

            entity.ToTable("contacts", tb => tb.HasComment("TABLA DONDE SE ALMACENAN LOS CLIENTES"));

            entity.HasIndex(e => e.Ctodocumento, "CTODOCUMENTOUNIQUE").IsUnique();

            entity.HasIndex(e => e.Ctoapellidos, "IDX_CTOAPELLIDOS").HasOperators(new[] { "varchar_pattern_ops" });

            entity.HasIndex(e => e.Ctonombres, "IDX_CTONOMBRES").HasOperators(new[] { "varchar_pattern_ops" });

            entity.Property(e => e.Ctoid).HasColumnName("CTOID");
            entity.Property(e => e.Ctoapellidos)
                .HasMaxLength(50)
                .HasColumnName("CTOAPELLIDOS");
            entity.Property(e => e.Ctocanton).HasColumnName("CTOCANTON");
            entity.Property(e => e.Ctocelular)
                .HasMaxLength(20)
                .HasColumnName("CTOCELULAR");
            entity.Property(e => e.Ctodireccion)
                .HasMaxLength(400)
                .HasColumnName("CTODIRECCION");
            entity.Property(e => e.Ctodocumento)
                .HasMaxLength(15)
                .HasColumnName("CTODOCUMENTO");
            entity.Property(e => e.Ctoemail)
                .HasMaxLength(50)
                .HasColumnName("CTOEMAIL");
            entity.Property(e => e.Ctoestadocivil)
                .HasMaxLength(25)
                .HasColumnName("CTOESTADOCIVIL");
            entity.Property(e => e.Ctofechanacimiento)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("CTOFECHANACIMIENTO");
            entity.Property(e => e.Ctonombres)
                .HasMaxLength(50)
                .HasColumnName("CTONOMBRES");
            entity.Property(e => e.Ctonotas)
                .HasMaxLength(50)
                .HasColumnName("CTONOTAS");
            entity.Property(e => e.Ctoparroquia).HasColumnName("CTOPARROQUIA");
            entity.Property(e => e.Ctoprovincia).HasColumnName("CTOPROVINCIA");
            entity.Property(e => e.Ctosupervisor).HasColumnName("CTOSUPERVISOR");
            entity.Property(e => e.Ctotelefono)
                .HasMaxLength(20)
                .HasColumnName("CTOTELEFONO");
            entity.Property(e => e.Ctotipodocumento).HasColumnName("CTOTIPODOCUMENTO");
            entity.Property(e => e.Ctotiponacionalidad).HasColumnName("CTOTIPONACIONALIDAD");
        });

        modelBuilder.Entity<Contadore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("contadores_pkey");

            entity.ToTable("contadores", tb => tb.HasComment("TABLA DE CONTADORES DE ORDENES DEL SISTEMA"));

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Customcounter).HasColumnName("CUSTOMCOUNTER");
            entity.Property(e => e.Slug)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("SLUG");
        });

        modelBuilder.Entity<Documento>(entity =>
        {
            entity.HasKey(e => e.Docuid).HasName("documentos_pkey");

            entity.ToTable("documentos", tb => tb.HasComment("TABLA DONDE SE ALMACENA EL INVENTARIO DE LOS DOCUMENTOS REQUERIDOS EN LAS ORDENES"));

            entity.HasIndex(e => e.Docucodigo, "CODIGO_UNICO").IsUnique();

            entity.HasIndex(e => e.Docudescripcion, "DESCRIPCION_UNICO").IsUnique();

            entity.Property(e => e.Docuid).HasColumnName("DOCUID");
            entity.Property(e => e.Docucodigo)
                .IsRequired()
                .HasColumnName("DOCUCODIGO");
            entity.Property(e => e.Docudescripcion)
                .IsRequired()
                .HasColumnName("DOCUDESCRIPCION");
        });

        modelBuilder.Entity<FailedJob>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("failed_jobs_pkey");

            entity.ToTable("failed_jobs");

            entity.HasIndex(e => e.Uuid, "failed_jobs_uuid_unique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Connection)
                .IsRequired()
                .HasColumnName("connection");
            entity.Property(e => e.Exception)
                .IsRequired()
                .HasColumnName("exception");
            entity.Property(e => e.FailedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("failed_at");
            entity.Property(e => e.Payload)
                .IsRequired()
                .HasColumnName("payload");
            entity.Property(e => e.Queue)
                .IsRequired()
                .HasColumnName("queue");
            entity.Property(e => e.Uuid)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("uuid");
        });

        modelBuilder.Entity<Identificacione>(entity =>
        {
            entity.HasKey(e => e.Idtid).HasName("identificaciones_pkey");

            entity.ToTable("identificaciones", tb => tb.HasComment("TABLA HACE JOIN CON TABLA USERS AQUI SE ALMACENA LOS DATOS INFORMATIVOS DEL USUARIO"));

            entity.HasIndex(e => e.Idtndocumento, "identificaciones_n_documento_key").IsUnique();

            entity.HasIndex(e => e.Usuid, "identificaciones_usuario_id_key").IsUnique();

            entity.Property(e => e.Idtid).HasColumnName("IDTID");
            entity.Property(e => e.Catestadocivil).HasColumnName("CATESTADOCIVIL");
            entity.Property(e => e.Catgenero).HasColumnName("CATGENERO");
            entity.Property(e => e.Catnacionalidad).HasColumnName("CATNACIONALIDAD");
            entity.Property(e => e.Cattipodocumento).HasColumnName("CATTIPODOCUMENTO");
            entity.Property(e => e.Idtcelular)
                .HasMaxLength(50)
                .HasColumnName("IDTCELULAR");
            entity.Property(e => e.Idtcontratado)
                .HasDefaultValueSql("'0'::smallint")
                .HasColumnName("IDTCONTRATADO");
            entity.Property(e => e.Idtdireccion)
                .HasMaxLength(200)
                .HasColumnName("IDTDIRECCION");
            entity.Property(e => e.Idtfecingreso).HasColumnName("IDTFECINGRESO");
            entity.Property(e => e.Idtfecnacimiento).HasColumnName("IDTFECNACIMIENTO");
            entity.Property(e => e.Idtfecsalida).HasColumnName("IDTFECSALIDA");
            entity.Property(e => e.Idthojavidaurl)
                .HasMaxLength(200)
                .HasColumnName("IDTHOJAVIDAURL");
            entity.Property(e => e.Idtimgurl)
                .HasMaxLength(200)
                .HasColumnName("IDTIMGURL");
            entity.Property(e => e.Idtndocumento)
                .IsRequired()
                .HasMaxLength(15)
                .HasDefaultValueSql("'0'::bpchar")
                .HasColumnName("IDTNDOCUMENTO");
            entity.Property(e => e.Idtnotas)
                .HasMaxLength(50)
                .HasColumnName("IDTNOTAS");
            entity.Property(e => e.Idttelefono)
                .HasMaxLength(50)
                .HasColumnName("IDTTELEFONO");
            entity.Property(e => e.Ubccanton).HasColumnName("UBCCANTON");
            entity.Property(e => e.Ubcparroquia).HasColumnName("UBCPARROQUIA");
            entity.Property(e => e.Ubcprovincia).HasColumnName("UBCPROVINCIA");
            entity.Property(e => e.Usuid).HasColumnName("USUID");
        });

        modelBuilder.Entity<Inscription>(entity =>
        {
            entity.HasKey(e => e.Insid).HasName("inscriptions_pkey");

            entity.ToTable("inscriptions");

            entity.HasIndex(e => e.Inssolicitante, "inssolicitante").IsUnique();

            entity.Property(e => e.Insid).HasColumnName("insid");
            entity.Property(e => e.Insconvenio)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("insconvenio");
            entity.Property(e => e.Insdatos)
                .HasColumnType("jsonb")
                .HasColumnName("insdatos");
            entity.Property(e => e.Insfecha).HasColumnName("insfecha");
            entity.Property(e => e.Inssolicitante).HasColumnName("inssolicitante");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("jobs_pkey");

            entity.ToTable("jobs");

            entity.HasIndex(e => e.Queue, "jobs_queue_index");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Attempts).HasColumnName("attempts");
            entity.Property(e => e.AvailableAt).HasColumnName("available_at");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.Payload)
                .IsRequired()
                .HasColumnName("payload");
            entity.Property(e => e.Queue)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("queue");
            entity.Property(e => e.ReservedAt).HasColumnName("reserved_at");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("log_tthh_pkey");

            entity.ToTable("log", tb => tb.HasComment("SE ENCUENTRA INACTIVA 18-02-2022"));

            entity.HasIndex(e => new { e.Id, e.Usuario, e.Operacion, e.Usuarioafectado }, "log_tthh_1_idx");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('log_tthh_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Operacion)
                .IsRequired()
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("operacion");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("'1970-01-01 00:00:00'::timestamp without time zone")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.Usuario)
                .IsRequired()
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("usuario");
            entity.Property(e => e.Usuarioafectado)
                .IsRequired()
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("usuarioafectado");
            entity.Property(e => e.Valoractual).HasColumnName("valoractual");
            entity.Property(e => e.Valoranterior).HasColumnName("valoranterior");
        });

        modelBuilder.Entity<Migration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("migrations_pkey");

            entity.ToTable("migrations", tb => tb.HasComment("TABLA DEFAULT DEL FRAMEWORK LARAVEL"));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Batch).HasColumnName("batch");
            entity.Property(e => e.Migration1)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("migration");
        });

        modelBuilder.Entity<ModelHasPermission>(entity =>
        {
            entity.HasKey(e => new { e.PermissionId, e.ModelId, e.ModelType }).HasName("model_has_permissions_pkey");

            entity.ToTable("model_has_permissions", tb => tb.HasComment("TABLA DE PERMISOS DE LOS USUARIOS SPATIE "));

            entity.HasIndex(e => new { e.ModelId, e.ModelType }, "model_has_permissions_model_id_model_type_index");

            entity.Property(e => e.PermissionId).HasColumnName("permission_id");
            entity.Property(e => e.ModelId).HasColumnName("model_id");
            entity.Property(e => e.ModelType)
                .HasMaxLength(255)
                .HasColumnName("model_type");

            entity.HasOne(d => d.Permission).WithMany(p => p.ModelHasPermissions)
                .HasForeignKey(d => d.PermissionId)
                .HasConstraintName("model_has_permissions_permission_id_foreign");
        });

        modelBuilder.Entity<ModelHasRole>(entity =>
        {
            entity.HasKey(e => new { e.RoleId, e.ModelId, e.ModelType }).HasName("model_has_roles_pkey");

            entity.ToTable("model_has_roles", tb => tb.HasComment("TABLA DE PERMISOS DE LOS USUARIOS SPATIE "));

            entity.HasIndex(e => new { e.ModelId, e.ModelType }, "model_has_roles_model_id_model_type_index");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.ModelId).HasColumnName("model_id");
            entity.Property(e => e.ModelType)
                .HasMaxLength(255)
                .HasColumnName("model_type");

            entity.HasOne(d => d.Role).WithMany(p => p.ModelHasRoles)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("model_has_roles_role_id_foreign");
        });

        modelBuilder.Entity<OauthAccessToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("oauth_access_tokens_pkey");

            entity.ToTable("oauth_access_tokens", tb => tb.HasComment("TABLA DEL FRAMEWORK LARAVEL OAUTH2"));

            entity.HasIndex(e => e.UserId, "oauth_access_tokens_user_id_index");

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .HasColumnName("id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.ExpiresAt)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("expires_at");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Revoked).HasColumnName("revoked");
            entity.Property(e => e.Scopes).HasColumnName("scopes");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<OauthAuthCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("oauth_auth_codes_pkey");

            entity.ToTable("oauth_auth_codes", tb => tb.HasComment("TABLA DEL FRAMEWORK LARAVEL OAUTH2"));

            entity.HasIndex(e => e.UserId, "oauth_auth_codes_user_id_index");

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .HasColumnName("id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.ExpiresAt)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("expires_at");
            entity.Property(e => e.Revoked).HasColumnName("revoked");
            entity.Property(e => e.Scopes).HasColumnName("scopes");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<OauthClient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("oauth_clients_pkey");

            entity.ToTable("oauth_clients", tb => tb.HasComment("TABLA DEL FRAMEWORK LARAVEL OAUTH2"));

            entity.HasIndex(e => e.UserId, "oauth_clients_user_id_index");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.PasswordClient).HasColumnName("password_client");
            entity.Property(e => e.PersonalAccessClient).HasColumnName("personal_access_client");
            entity.Property(e => e.Provider)
                .HasMaxLength(255)
                .HasColumnName("provider");
            entity.Property(e => e.Redirect)
                .IsRequired()
                .HasColumnName("redirect");
            entity.Property(e => e.Revoked).HasColumnName("revoked");
            entity.Property(e => e.Secret)
                .HasMaxLength(100)
                .HasColumnName("secret");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<OauthPersonalAccessClient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("oauth_personal_access_clients_pkey");

            entity.ToTable("oauth_personal_access_clients", tb => tb.HasComment("TABLA DEL FRAMEWORK LARAVEL OAUTH2"));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<OauthRefreshToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("oauth_refresh_tokens_pkey");

            entity.ToTable("oauth_refresh_tokens", tb => tb.HasComment("TABLA DEL FRAMEWORK LARAVEL OAUTH2"));

            entity.HasIndex(e => e.AccessTokenId, "oauth_refresh_tokens_access_token_id_index");

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .HasColumnName("id");
            entity.Property(e => e.AccessTokenId)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("access_token_id");
            entity.Property(e => e.ExpiresAt)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("expires_at");
            entity.Property(e => e.Revoked).HasColumnName("revoked");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Ordid).HasName("orders_pkey");

            entity.ToTable("orders");

            entity.HasIndex(e => e.Ordnumero, "ORDNUMERO_UNIQUE").IsUnique();

            entity.Property(e => e.Ordid).HasColumnName("ORDID");
            entity.Property(e => e.Ctoid).HasColumnName("CTOID");
            entity.Property(e => e.Ordabono)
                .HasPrecision(14, 2)
                .HasDefaultValueSql("0.00")
                .HasColumnName("ORDABONO");
            entity.Property(e => e.Ordassigned).HasColumnName("ORDASSIGNED");
            entity.Property(e => e.Ordconvenio).HasColumnName("ORDCONVENIO");
            entity.Property(e => e.Ordcredito)
                .HasPrecision(14, 2)
                .HasColumnName("ORDCREDITO");
            entity.Property(e => e.Orddatesalepayment).HasColumnName("ORDDATESALEPAYMENT");
            entity.Property(e => e.Orddespacho)
                .HasMaxLength(25)
                .HasColumnName("ORDDESPACHO");
            entity.Property(e => e.Orddocumentacion)
                .HasColumnType("jsonb")
                .HasColumnName("ORDDOCUMENTACION");
            entity.Property(e => e.Ordempresa)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("ORDEMPRESA");
            entity.Property(e => e.Ordestado).HasColumnName("ORDESTADO");
            entity.Property(e => e.Ordextras).HasColumnName("ORDEXTRAS");
            entity.Property(e => e.Ordimgurl)
                .HasMaxLength(100)
                .HasColumnName("ORDIMGURL");
            entity.Property(e => e.Ordiva)
                .HasPrecision(14, 2)
                .HasDefaultValueSql("0.00")
                .HasColumnName("ORDIVA");
            entity.Property(e => e.Ordnotas)
                .HasMaxLength(150)
                .HasColumnName("ORDNOTAS");
            entity.Property(e => e.Ordnotasobs)
                .HasMaxLength(150)
                .HasColumnName("ORDNOTASOBS");
            entity.Property(e => e.Ordnumero).HasColumnName("ORDNUMERO");
            entity.Property(e => e.Ordplazo).HasColumnName("ORDPLAZO");
            entity.Property(e => e.Ordregistrofecha1).HasColumnName("ORDREGISTROFECHA1");
            entity.Property(e => e.Ordregistrofecha2).HasColumnName("ORDREGISTROFECHA2");
            entity.Property(e => e.Ordregistrohora1).HasColumnName("ORDREGISTROHORA1");
            entity.Property(e => e.Ordsaldo)
                .HasPrecision(14, 2)
                .HasDefaultValueSql("0.00")
                .HasColumnName("ORDSALDO");
            entity.Property(e => e.Ordsalestatepayment)
                .HasDefaultValueSql("false")
                .HasColumnName("ORDSALESTATEPAYMENT");
            entity.Property(e => e.Ordsaleuserpayment).HasColumnName("ORDSALEUSERPAYMENT");
            entity.Property(e => e.Ordsaleusertypepayment)
                .HasMaxLength(1)
                .HasColumnName("ORDSALEUSERTYPEPAYMENT");
            entity.Property(e => e.Ordsubtotal)
                .HasPrecision(14, 2)
                .HasDefaultValueSql("0.00")
                .HasColumnName("ORDSUBTOTAL");
            entity.Property(e => e.Ordtotal)
                .HasPrecision(14, 2)
                .HasDefaultValueSql("0.00")
                .HasColumnName("ORDTOTAL");
            entity.Property(e => e.Persontype)
                .HasMaxLength(1)
                .HasColumnName("PERSONTYPE");
            entity.Property(e => e.Usuid).HasColumnName("USUID");
        });

        modelBuilder.Entity<Orderproduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("orderitems_pkey");

            entity.ToTable("orderproducts", tb => tb.HasComment("TABLA DE LOS PRODUCTOS ASOCIADO A UNA ORDEN"));

            entity.HasIndex(e => e.Ordernumero, "idx_orderproducts_ordnumero");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Brand).HasColumnName("BRAND");
            entity.Property(e => e.Description).HasColumnName("DESCRIPTION");
            entity.Property(e => e.Name).HasColumnName("NAME");
            entity.Property(e => e.Ordernumero).HasColumnName("ORDERNUMERO");
            entity.Property(e => e.Price)
                .HasPrecision(14, 2)
                .HasColumnName("PRICE");
            entity.Property(e => e.Quantity).HasColumnName("QUANTITY");
            entity.Property(e => e.Sku).HasColumnName("SKU");
            entity.Property(e => e.Total)
                .HasPrecision(14, 2)
                .HasColumnName("TOTAL");
            entity.Property(e => e.Weight)
                .HasPrecision(14, 1)
                .HasColumnName("WEIGHT");
        });

        modelBuilder.Entity<PasswordReset>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("password_resets", tb => tb.HasComment("TABLA DEL FRAMEWORK SE GUARDAN LOS RESETS PARA RECUPERAR PASSWORD"));

            entity.HasIndex(e => e.Email, "password_resets_email_idx");

            entity.HasIndex(e => e.Token, "password_resets_token_idx");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Token)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("token");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Payid).HasName("payments_pkey");

            entity.ToTable("payments", tb => tb.HasComment("TABLA DONDE SE GUARDAN LOS DEPOSITOS"));

            entity.HasIndex(e => e.Payorden, "idx_payments_payorden");

            entity.Property(e => e.Payid).HasColumnName("PAYID");
            entity.Property(e => e.Payammount)
                .HasPrecision(14, 2)
                .HasColumnName("PAYAMMOUNT");
            entity.Property(e => e.Paydate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("PAYDATE");
            entity.Property(e => e.Payentidadfinanciera).HasColumnName("PAYENTIDADFINANCIERA");
            entity.Property(e => e.Payobservacion)
                .HasMaxLength(200)
                .HasColumnName("PAYOBSERVACION");
            entity.Property(e => e.Payorden).HasColumnName("PAYORDEN");
            entity.Property(e => e.Paytransaction)
                .HasMaxLength(50)
                .HasColumnName("PAYTRANSACTION");
            entity.Property(e => e.Payusugenera).HasColumnName("PAYUSUGENERA");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("permissions_pkey");

            entity.ToTable("permissions", tb => tb.HasComment("TABLA DE PERMISOS DE LOS USUARIOS SPATIE "));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.GuardName)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("guard_name");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("updated_at");

            entity.HasMany(d => d.Roles).WithMany(p => p.Permissions)
                .UsingEntity<Dictionary<string, object>>(
                    "RoleHasPermission",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("role_has_permissions_role_id_foreign"),
                    l => l.HasOne<Permission>().WithMany()
                        .HasForeignKey("PermissionId")
                        .HasConstraintName("role_has_permissions_permission_id_foreign"),
                    j =>
                    {
                        j.HasKey("PermissionId", "RoleId").HasName("role_has_permissions_pkey");
                        j.ToTable("role_has_permissions", tb => tb.HasComment("RELACION DE ROLES A PERMISOS SPATIE"));
                    });
        });

        modelBuilder.Entity<PermissionsCompany>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("permissions_company_pkey");

            entity.ToTable("permissions_company");

            entity.HasIndex(e => e.Permission, "unique_permissionid").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Company)
                .IsRequired()
                .HasColumnName("company");
            entity.Property(e => e.Permission).HasColumnName("permission");

            entity.HasOne(d => d.PermissionNavigation).WithOne(p => p.PermissionsCompany)
                .HasForeignKey<PermissionsCompany>(d => d.Permission)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_permission");
        });

        modelBuilder.Entity<PersonalAccessToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("personal_access_tokens_pkey");

            entity.ToTable("personal_access_tokens");

            entity.HasIndex(e => e.Token, "personal_access_tokens_token_unique").IsUnique();

            entity.HasIndex(e => new { e.TokenableType, e.TokenableId }, "personal_access_tokens_tokenable_type_tokenable_id_index");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Abilities).HasColumnName("abilities");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.LastUsedAt)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("last_used_at");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Token)
                .IsRequired()
                .HasMaxLength(64)
                .HasColumnName("token");
            entity.Property(e => e.TokenableId).HasColumnName("tokenable_id");
            entity.Property(e => e.TokenableType)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("tokenable_type");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Postulant>(entity =>
        {
            entity.HasKey(e => e.Ptlid).HasName("postulants_pkey");

            entity.ToTable("postulants", tb => tb.HasComment("POSTULANTES AQUI SE GUARDAN LAS PERSONAS QUE SE REGISTRAN EN EL SISTEMA"));

            entity.HasIndex(e => e.Ptlemail, "postulants_email_key").IsUnique();

            entity.HasIndex(e => e.Ptlndocumento, "postulants_ndocumento_key").IsUnique();

            entity.HasIndex(e => e.Ptlusername, "postulants_username_key").IsUnique();

            entity.Property(e => e.Ptlid).HasColumnName("PTLID");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Ptlapellidos)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("PTLAPELLIDOS");
            entity.Property(e => e.Ptlcanton).HasColumnName("PTLCANTON");
            entity.Property(e => e.Ptlcelular)
                .IsRequired()
                .HasMaxLength(15)
                .HasColumnName("PTLCELULAR");
            entity.Property(e => e.Ptlcontacted).HasColumnName("PTLCONTACTED");
            entity.Property(e => e.Ptldireccion)
                .HasMaxLength(200)
                .HasColumnName("PTLDIRECCION");
            entity.Property(e => e.Ptlemail)
                .IsRequired()
                .HasMaxLength(60)
                .HasColumnName("PTLEMAIL");
            entity.Property(e => e.Ptlestado)
                .HasDefaultValueSql("118")
                .HasColumnName("PTLESTADO");
            entity.Property(e => e.Ptlestadocivil).HasColumnName("PTLESTADOCIVIL");
            entity.Property(e => e.Ptlfecnacimiento).HasColumnName("PTLFECNACIMIENTO");
            entity.Property(e => e.Ptlgenero).HasColumnName("PTLGENERO");
            entity.Property(e => e.Ptlhojavidaurl)
                .HasMaxLength(200)
                .HasColumnName("PTLHOJAVIDAURL");
            entity.Property(e => e.Ptlimgurl)
                .HasMaxLength(200)
                .HasColumnName("PTLIMGURL");
            entity.Property(e => e.Ptlnacionalidad).HasColumnName("PTLNACIONALIDAD");
            entity.Property(e => e.Ptlndocumento)
                .IsRequired()
                .HasMaxLength(15)
                .HasColumnName("PTLNDOCUMENTO");
            entity.Property(e => e.Ptlnombres)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("PTLNOMBRES");
            entity.Property(e => e.Ptlparroquia).HasColumnName("PTLPARROQUIA");
            entity.Property(e => e.Ptlprovincia).HasColumnName("PTLPROVINCIA");
            entity.Property(e => e.Ptltelefono)
                .IsRequired()
                .HasMaxLength(12)
                .HasColumnName("PTLTELEFONO");
            entity.Property(e => e.Ptltipodocumento).HasColumnName("PTLTIPODOCUMENTO");
            entity.Property(e => e.Ptlusername)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("PTLUSERNAME");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("roles_pkey");

            entity.ToTable("roles", tb => tb.HasComment("TABLA DE ROLES DE LOS USUARIOS SPATIE"));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.GuardName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("guard_name");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Routes)
                .HasColumnType("jsonb")
                .HasColumnName("routes");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("sessions", tb => tb.HasComment("SE ALMACENA LAS SESIONES DEL USUARIOS ESTA TABLA ES DEL FRAMEWORK"));

            entity.HasIndex(e => e.Id, "sessions_id_key").IsUnique();

            entity.Property(e => e.Id)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("id");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(45)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("ip_address");
            entity.Property(e => e.LastActivity).HasColumnName("last_activity");
            entity.Property(e => e.Payload)
                .IsRequired()
                .HasColumnName("payload");
            entity.Property(e => e.UserAgent).HasColumnName("user_agent");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("teams_pkey");

            entity.ToTable("teams", tb => tb.HasComment("TABLA EQUIPOS\nColumna name contiene el id del usuario\nColumna parent_id contiene el id del padre"));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Lft).HasColumnName("_lft");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");
            entity.Property(e => e.Rgt).HasColumnName("_rgt");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.NameNavigation).WithMany(p => p.Teams)
                .HasForeignKey(d => d.Name)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NAME_USERS");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK_PARENT_ID_ID");
        });

        modelBuilder.Entity<Ubicaciongeografica>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ubicaciongeografica_pkey");

            entity.ToTable("ubicaciongeografica", tb => tb.HasComment("TABLA CON LA DISTRIBUCION GEOGRÁFICA DEL ECUADOR PROVINCIAS, CANTONES, PARROQUIAS"));

            entity.HasIndex(e => e.Idpadre, "ubicaciongeografica_idpadre_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Codigo)
                .IsRequired()
                .HasMaxLength(50)
                .HasDefaultValueSql("'0'::character varying")
                .HasColumnName("codigo");
            entity.Property(e => e.Idpadre)
                .HasDefaultValueSql("0")
                .HasColumnName("idpadre");
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(150)
                .HasDefaultValueSql("'0'::character varying")
                .HasColumnName("nombre");
            entity.Property(e => e.Parroquia)
                .HasDefaultValueSql("'0'::smallint")
                .HasColumnName("parroquia");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users", tb => tb.HasComment("EN ESTA TABLA SE GUARDAN LOS USUARIOS DEL SISTEMA"));

            entity.HasIndex(e => e.Apellidos, "idx_users_apellidos");

            entity.HasIndex(e => e.Nombres, "idx_users_nombres");

            entity.HasIndex(e => e.Email, "users_email").IsUnique();

            entity.HasIndex(e => e.Username, "users_username").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apellidos)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("apellidos");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(60)
                .HasColumnName("email");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'0'::smallint")
                .HasColumnName("is_active");
            entity.Property(e => e.LastLogin)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("last_login");
            entity.Property(e => e.LastLoginIp)
                .HasMaxLength(50)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("last_login_ip");
            entity.Property(e => e.Nombres)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("nombres");
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.RememberToken)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("remember_token");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }*/

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseExceptionProcessor();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
        modelBuilder.HasDefaultSchema("web");
    }
}