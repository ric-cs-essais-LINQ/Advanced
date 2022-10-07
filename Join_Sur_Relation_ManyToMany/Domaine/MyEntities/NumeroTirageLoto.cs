namespace Domaine.MyEntities
{
    public class NumeroTirageLoto 
    {
        public int Id { get; set; }

        public int NumeroId { get; set; }
        //public Numero Numero { get; set; } //Non, pose problème de réf. circulaire lorsque je fais un Include() dessus, ET que je veux faire afficher le résultat.

        public int TirageLotoId { get; set; }
        //public TirageLoto TirageLoto { get; set; } //Non, pose problème de réf. circulaire lorsque je fais un Include() dessus, ET que je veux faire afficher le résultat.
    }
}
