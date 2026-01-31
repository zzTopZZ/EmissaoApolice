using Domain.Enums;
using Action = Domain.Enums.Action;

namespace Domain.Entities
{
    public class Proposta
    {

        public Proposta() { this.Status = Status.Criada; }

        public int Id { get; set; }

        public Cliente Cliente { get; set; }
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
    }
}
