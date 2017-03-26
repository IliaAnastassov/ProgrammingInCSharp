namespace Chapter3_Objective1.DataAccess
{
    using System.Collections.Generic;
    using Models;

    public interface IRepository<T> where T : IEntity, new()
    {
        T GetEntity(int id);

        IEnumerable<T> GetAllEntities();

        void AddEntity(T entity);
    }
}
