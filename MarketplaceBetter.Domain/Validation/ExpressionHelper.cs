using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Validation
{
    public static class ExpressionHelper
    {
        public static string GetPropertyNameFrom<T>(Expression<Func<T, object>> expression)
        {
            MemberExpression memberExpression = expression.Body as MemberExpression;
            UnaryExpression unaryExpression = expression.Body as UnaryExpression;

            memberExpression = memberExpression ?? (unaryExpression != null ? unaryExpression.Operand as MemberExpression : null);

            return memberExpression.Member.Name;
        }

        public static string GetPropertyKeyForModelError<T>(Expression<Func<T, object>> expression)
        {
            Stack<string> nameParts = new Stack<string>();
            Expression part = expression.Body;

            while (part != null)
            {
                MemberExpression memberExpressionPart = part as MemberExpression;
                UnaryExpression unaryExpressionPart = part as UnaryExpression;

                memberExpressionPart = memberExpressionPart ?? (unaryExpressionPart != null ? unaryExpressionPart.Operand as MemberExpression : null);

                if (memberExpressionPart != null)
                {
                    nameParts.Push("." + memberExpressionPart.Member.Name);

                    part = memberExpressionPart.Expression;
                }
                else
                {
                    part = null;
                }
            }

            // If it starts with "model", then strip that away
            if (nameParts.Count > 0 && string.Equals(nameParts.Peek(), ".model", StringComparison.OrdinalIgnoreCase))
            {
                nameParts.Pop();
            }

            if (nameParts.Count > 0)
            {
                return nameParts.Aggregate((left, right) => left + right).TrimStart('.');
            }

            return string.Empty;
        }
    }
}
