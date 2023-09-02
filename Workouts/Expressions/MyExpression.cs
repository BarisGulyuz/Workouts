using System.Linq.Expressions;
using System.Reflection;

namespace Workouts.Expressions
{
    //not completed - on-progress
    public class MyExpression<T>
    {
        public Expression<Func<T, bool>> GetExpression(List<ExpressionModel> expressionModels, bool useAnd)
        {
            if (expressionModels == null || expressionModels.Count == 0)
            {
                throw new ArgumentException("ExpressionModels CanNot Be Empty Or Null");
            }

            var parameter = Expression.Parameter(typeof(T), "x");
            Expression expression = null;

            foreach (var condition in expressionModels)
            {
                Expression currentExpression = CreateExpression(parameter, condition);

                if (expression == null)
                {
                    expression = currentExpression;
                }
                else
                {
                    expression = useAnd ? Expression.AndAlso(expression, currentExpression)
                                        : Expression.OrElse(expression, currentExpression);
                }
            }

            return Expression.Lambda<Func<T, bool>>(expression, parameter);
        }
        private static Expression CreateExpression(ParameterExpression parameter, ExpressionModel condition)
        {
            var member = Expression.PropertyOrField(parameter, condition.ColumnName);
            var constant = Expression.Constant(condition.Value);
            Expression currentExpression = null;

            switch (condition.OperatorEnum)
            {
                case OperatorEnum.Equal:
                    currentExpression = Expression.Equal(member, constant);
                    break;
                case OperatorEnum.NotEqual:
                    currentExpression = Expression.NotEqual(member, constant);
                    break;
                case OperatorEnum.LessThan:
                    currentExpression = Expression.LessThan(member, constant);
                    break;
                case OperatorEnum.LessThanOrEqual:
                    currentExpression = Expression.LessThanOrEqual(member, constant);
                    break;
                case OperatorEnum.GreaterThan:
                    currentExpression = Expression.GreaterThan(member, constant);
                    break;
                case OperatorEnum.GreaterThanOrEqual:
                    currentExpression = Expression.GreaterThanOrEqual(member, constant);
                    break;
                case OperatorEnum.Contains:
                    MethodInfo containsMethod = typeof(string).GetMethods().First(m => m.Name == "Contains" && m.GetParameters().Length == 1);
                    currentExpression = Expression.Call(member, containsMethod, constant);
                    break;
            }

            return currentExpression;
        }
    }
}
