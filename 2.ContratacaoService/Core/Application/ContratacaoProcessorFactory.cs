using Application.Booking.Dtos;
using Application.Contratacao.Ports;
using Contratacao.Application.Manual;
using Payments.Application;

namespace Application.Payment
{
    public class ContratacaoProcessorFactory : IContratacaoProcessorFactory
    {
        public IContratacaoProcessor GetContratacaoProcessor(SupportedContratacaoProviders selectedContratacaoProvider)
        {
            switch (selectedContratacaoProvider)
            {
                case SupportedContratacaoProviders.Manual:
                    return new ContratacaoManualAdapter();

                default: return new NotImplementedContratacaotProvider();
            }
        }


    }
}
