using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domaine.MyEntities;


namespace DataAccess.ModelBuilderConfigs
{
    public class NumeroTirageLotoConfig : IEntityTypeConfiguration<NumeroTirageLoto>
    {
        public void Configure(EntityTypeBuilder<NumeroTirageLoto> entityModelBuilder)
        {
            //On ne veut pas de doublon sur cette clef composée :
            entityModelBuilder.HasIndex(numeroTirageLoto => new { numeroTirageLoto.NumeroId, numeroTirageLoto.TirageLotoId }).IsUnique();
        }
    }
}
