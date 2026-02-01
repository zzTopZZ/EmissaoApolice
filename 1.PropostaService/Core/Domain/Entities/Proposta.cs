using Domain.Enums;
using Domain.Exceptons;
using Domain.Ports;
using Action = Domain.Enums.Action;

namespace Domain.Entities
{
    public class Proposta
    {
        public Proposta() { this.Status = Status.Criada; }

        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string NomeCliente { get; set; }
        public decimal ValorProposta { get; set; }
        public DateTime DataCriacao { get; set; }
        public decimal ValorSegurado { get; set; }
        public decimal ValorPremio { get; set; }
        private Status Status { get; set; }
        public Status SituacaoStatus { get { return this.Status; } }

        public void ChangeState(Action action)
        {
            this.Status = (this.Status, action) switch
            {
                (Status.Criada, Action.preenchida) => Status.Criada,
                (Status.Criada, Action.finalizada) => Status.Aprovada,
                (Status.Criada, Action.cancelada)  => Status.Rejeitada,
                _ => this.Status
            };
        }

        private void ValidateState()
        {
            //if (DocumentId == null || 
            //    string.IsNullOrEmpty(DocumentId.IdNumber) ||
            //    DocumentId.IdNumber.Length <= 3 ||
            //    DocumentId.DocumentType == 0)
            //{
            //    throw new InvalidPersonDocumentIdException();
            //}


            if (string.IsNullOrEmpty(NomeCliente) ||
                (ValorProposta == 0) ||
                (ValorSegurado == 0) ||
                (ValorPremio   == 0))
            {
                throw new MissingRequiredInformation();
            }
        }

        public async Task Save(IPropostaRepository propostaRepository)
        {
            this.ValidateState();
            if (this.Id == 0)
            {
                this.Id = await propostaRepository.Create(this);
            }
            else
            {
                //this.Id = await clienteRepository.Update(this);
            }
        }
    }
}
