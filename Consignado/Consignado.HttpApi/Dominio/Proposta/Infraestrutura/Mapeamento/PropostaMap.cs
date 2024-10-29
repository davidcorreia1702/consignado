using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Consignado.HttpApi.Dominio.Inscricao.Infraestrutura.Mapeamento
{
    public class PropostaMap : IEntityTypeConfiguration<Proposta>
    {
        public void Configure(EntityTypeBuilder<Proposta> builder)
        {
            builder.ToTable("Proposta");

            builder.HasKey(p => p.NumeroProposta);

            // Propriedades obrigatórias
            builder.Property(p => p.CpfAgente)
                .IsRequired()
                .HasColumnType("varchar(11)");

            builder.Property(p => p.Cpf)
                .IsRequired()
                .HasColumnType("varchar(20)");

            builder.Property(p => p.DataNascimento)
                .IsRequired();

            builder.Property(p => p.DDD)
                .IsRequired()
                .HasColumnType("varchar(3)");

            builder.Property(p => p.Telefone)
                  .IsRequired()
                  .HasColumnType("varchar(15)");

            builder.Property(p => p.Email)
                  .IsRequired()
                  .HasColumnType("varchar(100)");

            builder.Property(p => p.Cep)
                  .IsRequired()
                  .HasColumnType("varchar(8)");

            builder.Property(p => p.Endereco)
                  .IsRequired()
                  .HasColumnType("varchar(200)");

            builder.Property(p => p.Numero)
                  .IsRequired()
                  .HasColumnType("varchar(4)");

            builder.Property(p => p.Cidade)
                  .IsRequired()
                  .HasColumnType("varchar(100)");

            builder.Property(p => p.Uf)
                  .IsRequired()
                  .HasColumnType("varchar(2)");

            builder.Property(p => p.ConveniadaId)
                  .IsRequired();

            builder.Property(p => p.Matricula)
                  .IsRequired()
                  .HasColumnType("varchar(20)");

            builder.Property(p => p.ValorRendimento)
                  .IsRequired()
                  .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Prazo)
                  .IsRequired()
                  .HasColumnType("varchar(3)");

            builder.Property(p => p.ValorOperacao)
                  .IsRequired()
                  .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Prestacao)
                  .IsRequired()
                  .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Banco)
                  .IsRequired()
                  .HasColumnType("varchar(50)");

            builder.Property(p => p.Agencia)
                  .IsRequired()
                  .HasColumnType("varchar(10)");

            builder.Property(p => p.Conta)
                  .IsRequired()
                  .HasColumnType("varchar(20)");

            builder.Property(p => p.TipoConta)
                  .IsRequired()
                  .HasConversion(new EnumToStringConverter<Tipoconta>())
                  .HasColumnType("varchar(20)");

            builder.Property(p => p.TipoOperacao)
                  .IsRequired()
                  .HasConversion(new EnumToStringConverter<TipoOperacao>())
                  .HasColumnType("varchar(20)");

            builder.Property(p => p.TipoAssinatura)
                  .IsRequired()
                  .HasConversion(new EnumToStringConverter<TipoAssinatura>())
                  .HasColumnType("varchar(20)");

            builder.Property(p => p.Situacao)
                  .IsRequired()
                  .HasConversion(new EnumToStringConverter<SituacaoProposta>())
                  .HasColumnType("varchar(20)");

            builder.HasOne<Conveniada>()
                  .WithMany()
                  .HasForeignKey(p => p.ConveniadaId)
                  .IsRequired();
        }
    }
}
