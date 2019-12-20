using System;

namespace Domain
{
    public class Note : Entity
    {
        public bool Completed { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
