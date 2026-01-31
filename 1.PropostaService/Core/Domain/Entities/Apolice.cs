namespace Domain.Entities
{
    public class Apolice
    {
        public int Id { get; set; }
        public Proposta Proposta { get; set; }
        public DateTime DataContratacao { get; set; }
        public decimal ValorSegurado { get; set; }
        public decimal ValorPremio { get; set; }

    }
}
