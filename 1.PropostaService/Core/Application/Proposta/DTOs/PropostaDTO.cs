using Domain.Enums;
using Domain.Ports;
using Entities = Domain.Entities;
using ClienteProposta = Domain.Entities.Cliente;
using Application.Cliente.DTO;

namespace Application.Proposta.DTO
{
    public class PropostaDTO
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string?NomeCliente { get; set; }
        public decimal ValorProposta { get; set; }
        public DateTime DataCriacao { get; set; }
        public decimal ValorSegurado { get; set; }
        public decimal ValorPremio { get; set; }
        public int? Status { get; set; }
        //private Status? Status { get; set; }

        public static Entities.Proposta MapToEntity(PropostaDTO dto)
        {
            return new Entities.Proposta
            {
                Id = dto.Id,
                ClienteId = dto.ClienteId,
                NomeCliente = dto.NomeCliente,
                ValorProposta = dto.ValorProposta,
                DataCriacao = dto.DataCriacao,
                ValorSegurado = dto.ValorSegurado,
                ValorPremio = dto.ValorPremio,
                Status = dto.Status
            };
        }


        public static PropostaDTO MapFromEntity(Entities.Proposta proposta)
        {
            return new PropostaDTO
            {
                Id = proposta.Id,
                ClienteId = proposta.ClienteId,
                NomeCliente = proposta.NomeCliente,
                ValorProposta = proposta.ValorProposta,
                DataCriacao = proposta.DataCriacao,
                ValorSegurado = proposta.ValorSegurado,
                ValorPremio = proposta.ValorPremio,
                Status = proposta.Status
            };
        }
    }
}
