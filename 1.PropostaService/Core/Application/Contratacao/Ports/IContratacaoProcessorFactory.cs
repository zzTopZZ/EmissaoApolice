using Application.Booking.Dtos;
using Application.Payment;

namespace Application.Contratacao.Ports
{
    public interface IContratacaoProcessorFactory
    {
        IContratacaoProcessor GetContratacaoProcessor(SupportedContratacaoProviders selectedPaymentProvider);
    }
}
