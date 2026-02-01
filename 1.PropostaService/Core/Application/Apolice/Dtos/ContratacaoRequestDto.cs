namespace Application.Booking.Dtos
{
    public enum SupportedContratacaoProviders
    { 
        Manual = 1,
        Automatico = 2
    }

    public enum SupportedContratacaoMethods
    { 
        Telefone = 1,
        Mesa = 2,
        Email = 3,
    }

    public class ContratacaoRequestDto
    {
        public int PropostaId { get; set; }
        public string ContratacaoIntention { get; set; }
        public SupportedContratacaoProviders SelectedContratacaoProvider { get; set; }
        public SupportedContratacaoMethods SelectedContratacaoMethod { get; set; }
    }
}
