using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Workouts.Expressions
{
    public class MyExpression<T>
    {

        //not completed - on-progress
        public Expression<Func<T, bool>> GetExpression(List<ExpressionModel> expressionModels)
        {
            ParameterExpression entityParameter = Expression.Parameter(typeof(T), "x");

            List<BinaryExpression> binaryExpressions = new List<BinaryExpression>();
            List<MethodCallExpression> methodCallExpressions = new List<MethodCallExpression>();

            foreach (ExpressionModel expressionModel in expressionModels)
            {
                MemberExpression property = Expression.Property(entityParameter, expressionModel.ColumnName);

                BinaryExpression expression = null;
                switch (expressionModel.OperatorEnum)
                {
                    case OperatorEnum.Equal:
                        expression = Expression.Equal(property, Expression.Constant(expressionModel.Value));
                        binaryExpressions.Add(expression);
                        break;
                    case OperatorEnum.GreaterThanOrEqual:
                        expression = Expression.GreaterThanOrEqual(property, Expression.Constant(expressionModel.Value));
                        binaryExpressions.Add(expression);
                        break;
                    case OperatorEnum.LessThanOrEqual:
                        expression = Expression.LessThanOrEqual(property, Expression.Constant(expressionModel.Value));
                        binaryExpressions.Add(expression);
                        break;
                    case OperatorEnum.Contains:
                        MethodInfo containsMethod = typeof(string).GetMethods().First(m => m.Name == "Contains" && m.GetParameters().Length == 1);
                        MethodCallExpression methodCallExp = Expression.Call(property, containsMethod, Expression.Constant(expressionModel.Value));
                        methodCallExpressions.Add(methodCallExp);
                        break;
                }
            }

            Expression finalExpression = null;
            if (binaryExpressions.Count > 0)
            {
                for (int i = 0; i < binaryExpressions.Count; i++)
                {
                    if (i == 0)
                    {
                        finalExpression = Expression.OrElse(binaryExpressions[i], binaryExpressions[i + 1]);
                    }
                    else if (i < binaryExpressions.Count - 1)
                    {
                        finalExpression = Expression.OrElse(finalExpression, binaryExpressions[i + 1]);
                    }
                }

            }

            if (finalExpression != null && methodCallExpressions.Count > 0)
            {
                finalExpression = Expression.OrElse(finalExpression, methodCallExpressions.FirstOrDefault()); //for now
            }
            else if (finalExpression == null)
            {
                finalExpression = methodCallExpressions.FirstOrDefault(); //for now
            }

            return Expression.Lambda<Func<T, bool>>(finalExpression, entityParameter);

        }


    }
}
