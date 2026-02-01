using Domain.Exceptons;
using Domain.Ports;

namespace Domain.Entities
{
    public class Apolice
    {
        public int Id { get; set; }
        public int PropostaId { get; set; }
        public DateTime DataContratacao { get; set; }
        public decimal ValorSegurado { get; set; }
        public decimal ValorPremio { get; set; }

        private void ValidateState()
        {

            if (PropostaId == 0 ||
                ValorSegurado == 0 ||
                ValorPremio == 0)
            {
                throw new MissingRequiredInformation();
            }
        }

        public async Task Save(IApoliceRepository apoliceRepository)
        {
            this.ValidateState();
            if (this.Id == 0)
            {
                this.Id = await apoliceRepository.Create(this);
            }
            else
            {
                //this.Id = await apoliceRepository.Update(this);
            }
        }
    }
}
