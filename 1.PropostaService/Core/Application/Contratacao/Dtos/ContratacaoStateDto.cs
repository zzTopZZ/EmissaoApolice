namespace Application.Contratacao.Dtos
{
    public enum Status
    {
        Sucesso = 0,
        Failded = 1,
        Error = 2,
        Undefined = 3,
    }

    public class ContratacaoStateDto
    {
        public Status Status { get; set; }
        public string ContratacaoId { get; set; }
        public DateTime DataCriacao { get; set; }
        public string  Message { get; set; }

    }
}
