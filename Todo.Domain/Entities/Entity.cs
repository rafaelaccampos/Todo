namespace Todo.Domain.Entities
{
    /* Annotations
        IEquatable<Entity> => permite que execute comparações com um ou mais objetos do mesmo tipo
    */
    public abstract class Entity : IEquatable<Entity>
    {
        /*bom pq tem dominio, ruim pq é mais lento do que um int */
        public Guid Id { get; private set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public bool Equals(Entity? other)
        {
            return Id == other?.Id;
        }
    }
}
