using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boilerplate.Infrastructure.Configuration;

public class PostulantConfiguration : IEntityTypeConfiguration<Postulant>
{
    public void Configure(EntityTypeBuilder<Postulant> entity)
    {
        entity.Property(e => e.Id).HasConversion<PostulantId.EfCoreValueConverter>();
        entity.HasKey(e => e.Id).HasName("Postulants_Id_pkey");

        entity.ToTable("Postulants", "web", tb => tb.HasComment("POSTULANTES AQUI SE GUARDAN LAS PERSONAS QUE SE REGISTRAN EN EL SISTEMA"));

        entity.HasIndex(e => e.Email, "Postulants_Email_key").IsUnique();

        entity.HasIndex(e => e.Ndocument, "Postulants_Ndocument_key").IsUnique();

        entity.HasIndex(e => e.UserName, "Postulants_UserName_key").IsUnique();

        entity.Property(e => e.Address).HasMaxLength(200);
        entity.Property(e => e.CreatedAt).HasColumnType("timestamp without time zone");
        entity.Property(e => e.CurriculumUrl).HasMaxLength(200);
        entity.Property(e => e.DeletedAt).HasColumnType("timestamp without time zone");
        entity.Property(e => e.Email).HasMaxLength(60);
        entity.Property(e => e.ImgUrl).HasMaxLength(200);
        entity.Property(e => e.Mobile).HasMaxLength(15);
        entity.Property(e => e.Name).HasMaxLength(50);
        entity.Property(e => e.Ndocument).HasMaxLength(15);
        entity.Property(e => e.Phone).HasMaxLength(12);
        entity.Property(e => e.State).HasDefaultValueSql("118");
        entity.Property(e => e.SurName).HasMaxLength(50);
        entity.Property(e => e.UpdatedAt).HasColumnType("timestamp without time zone");
        entity.Property(e => e.UserName).HasMaxLength(50);
    }
}