namespace Chapter3_Objective1.Validation
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Models;

    public static class EntityValidator<T> where T : IEntity
    {
        public static IEnumerable<ValidationResult> Validate(T entity)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(entity);
            Validator.TryValidateObject(entity, context, results);

            return results;
        }
    }
}
