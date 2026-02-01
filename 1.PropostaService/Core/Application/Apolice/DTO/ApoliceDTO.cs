using Entities = Domain.Entities;

namespace Application.Apolice.DTO
{
    public class ApoliceDTO
    {
        public int Id { get; set; }
        public int PropostaId { get; set; }
        public DateTime DataContratacao { get; set; }
        public decimal ValorSegurado { get; set; }
        public decimal ValorPremio { get; set; }

        public static Entities.Apolice MapToEntity(ApoliceDTO dto)
        {
            return new Entities.Apolice
            {
                Id = dto.Id,
                PropostaId = dto.PropostaId,
                DataContratacao = dto.DataContratacao,
                ValorSegurado = dto.ValorSegurado,
                ValorPremio = dto.ValorPremio,
            };
        }

        public static ApoliceDTO MapFromEntity(Entities.Apolice apolice)
        {
            return new ApoliceDTO
            {
                Id = apolice.Id,
                PropostaId = apolice.PropostaId,
                DataContratacao = apolice.DataContratacao,
                ValorSegurado = apolice.ValorSegurado,
                ValorPremio = apolice.ValorPremio,
            };
        }
    }
}
