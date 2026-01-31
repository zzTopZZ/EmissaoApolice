using Domain.Enums;
using Entities = Domain.Entities;

namespace Application.Cliente.DTO
{
    public class ClienteDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Sobrenome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        //public PersonId DocumentId { get; set; }
        public string IdNumber { get; set; }
        public int IdTypeCode { get; set; }

        public static Entities.Cliente MapToEntity(ClienteDTO dto)
        {
            return new Entities.Cliente
            {
                Id = dto.Id,
                Nome = dto.Nome,
                Sobrenome = dto.Sobrenome,
                Email = dto.Email,
                //DocumentId = new Domain.ValueObjects.PersonId
                //{
                //    IdNumber = dto.IdNumber,
                //    DocumentType = (DocumentType)dto.IdTypeCode
                //}
            };
        }

        public static object MapToEntity(Entities.Cliente cliente)
        {
            throw new NotImplementedException();
        }
    }
}
