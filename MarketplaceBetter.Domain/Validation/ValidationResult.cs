using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Validation
{
    public class ValidationResult
    {
        public ValidationResult()
        {
            Errors = new List<ValidationError>();

            IsValid = true;
        }

        public bool IsValid { get; private set; }

        public IList<ValidationError> Errors { get; private set; }

        public void AddError(string message, params object[] parameters)
        {
            AddError(string.Empty, message, parameters);
        }

        public void AddError(string key, string message, params object[] parameters)
        {
            key = key == null ? string.Empty : key;

            string formattedMessage = string.Format(message, parameters);

            Errors.Add(new ValidationError(key, formattedMessage));

            IsValid = false;
        }

        public void AddErrorFor<TModel>(Expression<Func<TModel, object>> property, string message, params object[] parameters)
        {
            string propertyName = ExpressionHelper.GetPropertyKeyForModelError(property);

            AddError(propertyName, message, parameters);
        }
    }
}
