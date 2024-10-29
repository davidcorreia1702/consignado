using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using static Dapper.SqlMapper;

namespace Consignado.HttpApi.Dominio.Inscricao.Infraestrutura.Mapeamento
{
    public class ConveniadaUfRestricaoMap : IEntityTypeConfiguration<ConveniadaUfRestricao>
    {
        public void Configure(EntityTypeBuilder<ConveniadaUfRestricao> builder)
        {
            builder.ToTable("ConveniadaUFRestricao");

            builder.HasKey(cufr => new { cufr.UF, cufr.ConveiadaID }); // Chave composta

            builder.HasOne(cufr => cufr.UnidadeFederativa)
                  .WithMany()
                  .HasForeignKey("UnidadeFederativaId")
                  .IsRequired();

            builder.Property(cufr => cufr.ValorLimite)
                  .IsRequired(false);
        }
    }
}
