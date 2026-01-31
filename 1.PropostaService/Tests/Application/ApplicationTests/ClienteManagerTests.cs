using Application;
using Application.Cliente.DTO;
using Application.Cliente.Ports;
using Application.Cliente.Request;
using Domain.Entities;
using Domain.Ports;
using Moq;

namespace ApplicationTests
{

    public class Tests
    {
        ClienteManager clienteManager;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task HappyPath()
        {
            var clienteDTO = new ClienteDTO
            {
                Nome = "João",
                Sobrenome = "da Silva",
                IdNumber = "12345678900",
                Email = "testehappy@gmail.com",
                IdTypeCode = 1
            };

            int expectedId = 111;

            var request = new CreateClienteRequest()
            {
                Data = clienteDTO,
            };

            var fakeRepo = new Mock<IClienteRepository>();

            fakeRepo.Setup(x => x.Create(It.IsAny<Cliente>())).ReturnsAsync(expectedId);

            clienteManager = new ClienteManager(fakeRepo.Object);

            var response = await clienteManager.CreateCliente(request);
            Assert.IsNotNull(response);
            Assert.True(response.Success);
            Assert.AreEqual(response.Data.Id, expectedId);
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("a")]
        [TestCase("abc")]
        public async Task InvalidPersonDocumentId(string? docNumber)
        {
            var clienteDTO = new ClienteDTO
            {
                Nome = "João",
                Sobrenome = "da Silva",
                IdNumber = docNumber,
                Email = "testehappy@gmail.com",
                IdTypeCode = 1
            };

            var request = new CreateClienteRequest()
            {
                Data = clienteDTO,
            };

            var fakeRepo = new Mock<IClienteRepository>();

            fakeRepo.Setup(x => x.Create(It.IsAny<Cliente>())).ReturnsAsync(555);

            clienteManager = new ClienteManager(fakeRepo.Object);

            var response = await clienteManager.CreateCliente(request);
            Assert.IsNotNull(response);
            Assert.False(response.Success);
            Assert.AreEqual(response.ErrorCode, ErrorCode.INVALID_PERSON_ID);
            Assert.AreEqual(response.Message, "O ID passado esta invalido");
        }


        [TestCase("", "", "")]
        [TestCase(null, null, null)]
        [TestCase(null, "de Souza", "teste23@gmail.com")]
        [TestCase("Fernando", "", "teste23@gmail.com")]
        public async Task MissingRequiredInformation(string? name, string? sobreNome, string? email)
        {
            var clienteDTO = new ClienteDTO
            {
                Nome = name,
                Sobrenome = sobreNome,
                IdNumber = "12345678",
                Email = email,
                IdTypeCode = 1
            };

            var request = new CreateClienteRequest()
            {
                Data = clienteDTO,
            };

            var fakeRepo = new Mock<IClienteRepository>();

            fakeRepo.Setup(x => x.Create(It.IsAny<Cliente>())).ReturnsAsync(555);

            clienteManager = new ClienteManager(fakeRepo.Object);

            var response = await clienteManager.CreateCliente(request);
            Assert.IsNotNull(response);
            Assert.False(response.Success);
            Assert.AreEqual(response.ErrorCode, ErrorCode.MISSION_REQUIRED_INFORMATION);
            Assert.AreEqual(response.Message, "Informações requeridas.");
        }
    }
}