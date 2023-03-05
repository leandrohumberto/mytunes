namespace MyTunes.Core.Entities
{
    public abstract class BaseEntity
    {
        public BaseEntity() { } // Criado para o EFCore

        public int Id { get; protected set; }
    }
}
