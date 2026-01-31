using Domain.Exceptons;
using Domain.Ports;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public  class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Sobrenome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        //public PersonId DocumentId { get; set; }

        private void ValidateState()
        {
            //if (DocumentId == null || 
            //    string.IsNullOrEmpty(DocumentId.IdNumber) ||
            //    DocumentId.IdNumber.Length <= 3 ||
            //    DocumentId.DocumentType == 0)
            //{
            //    throw new InvalidPersonDocumentIdException();
            //}


            if (string.IsNullOrEmpty(Nome) ||
                string.IsNullOrEmpty(Sobrenome) ||
                string.IsNullOrEmpty(Email))
            {
                throw new MissingRequiredInformation();
            }
        }

        public async Task Save(IClienteRepository clienteRepository)
        {
            this.ValidateState();
            if (this.Id == 0)
            {
                this.Id = await clienteRepository.Create(this);
            }
            else
            {
                //this.Id = await clienteRepository.Update(this);
            }
        }
    }
}
