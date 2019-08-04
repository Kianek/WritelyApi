using System;

namespace WritelyApi.Data
{
    public class Entity
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset LastModified { get; set; }

        public Entity()
        {
            CreatedAt = Update();
        }

        public DateTimeOffset Update() => LastModified = DateTimeOffset.UtcNow;
    }
}