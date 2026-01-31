using Domain.Enums;
using System.Runtime.Intrinsics.X86;
using Action = Domain.Enums.Action;
using Propostas = Domain.Entities.Proposta;

namespace DomainTests.Proposta
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DeveSempreComecarComStatusCriado()
        {
            var proposta = new Propostas();
            Assert.AreEqual(proposta.SituacaoStatus, Status.Criada);

        }

        [Test]
        public void DeveDefinirStatusComoFinalizadaStatusAprovada()
        {
            var proposta = new Propostas();
            proposta.ChangeState(Action.finalizada);
            Assert.AreEqual(proposta.SituacaoStatus, Status.Aprovada);

        }

        [Test]
        public void DeveDefinirStatusComoFinalizadaStatusCancelada()
        {
            var proposta = new Propostas();
            proposta.ChangeState(Action.cancelada);
            Assert.AreEqual(proposta.SituacaoStatus, Status.Rejeitada);

        }
    }
}