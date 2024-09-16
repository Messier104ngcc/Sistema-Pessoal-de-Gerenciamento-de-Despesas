using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema_Pessoal_de_Gerenciamento_de_Despesas.Models;

namespace Sistema_Pessoal_de_Gerenciamento_de_Despesas.Date
{
    public class Usuarios : IEntityTypeConfiguration<Models.Usuarios>
    {
        public void Configure(EntityTypeBuilder<Models.Usuarios> builder) 
        {
            builder.ToTable("Usuarios");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Nome).HasColumnType("varchar(20)");
            builder.Property(t => t.UserName).HasColumnType("varchar(50)");
            builder.Property(t => t.Senha).HasColumnType("varchar(14)");
        }

    }
}
