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
    }
}
