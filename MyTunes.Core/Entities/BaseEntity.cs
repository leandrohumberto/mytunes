namespace MyTunes.Core.Entities
{
    public abstract class BaseEntity
    {
        protected BaseEntity() { } // Criado para o EFCore

        public int Id { get; private set; }
    }
}
