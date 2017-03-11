namespace Chapter2_Objective4
{
    using System.Collections.Generic;
    using System.Linq;

    public class Repository<T> where T : IEntity
    {
        protected IEnumerable<T> elements;

        public Repository(IEnumerable<T> entities)
        {
            this.elements = entities;
        }

        public T GetById(int id)
        {
            return this.elements.SingleOrDefault(e => e.Id == id);
        }
    }
}
