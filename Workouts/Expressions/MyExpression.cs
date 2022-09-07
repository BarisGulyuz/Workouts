using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Workouts.Expressions
{
    //not completed - on-progress
    public class MyExpression<T>
    {
        public bool IsAndConjunction { get; set; } //make enum
        public MyExpression()
        {
            IsAndConjunction = false;
        }
        public MyExpression(bool isAndConjunction)
        {
            this.IsAndConjunction = isAndConjunction;
        }
        public Expression<Func<T, bool>> GetExpression(List<ExpressionModel> expressionModels)
        {
            ParameterExpression entityParameter = Expression.Parameter(typeof(T), "x");

            List<Expression> expressions = CreateExpression(expressionModels, entityParameter);

            if (expressions.Count == 0)
            {
                throw new Exception($"0 Expression Created. Your List Can Be Empty");
            }

            Expression finalExpression = null;
            if (expressions.Count > 0)
            {
                if (expressions.Count == 1)
                {
                    finalExpression = expressions.FirstOrDefault();
                }
                else
                {
                    if (IsAndConjunction)
                    {
                        for (int i = 0; i < expressions.Count; i++)
                        {
                            if (i == 0)
                            {
                                finalExpression = Expression.AndAlso(expressions[i], expressions[i + 1]);
                            }
                            else if (i < expressions.Count - 1)
                            {
                                finalExpression = Expression.AndAlso(finalExpression, expressions[i + 1]);
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < expressions.Count; i++)
                        {
                            if (i == 0)
                            {
                                finalExpression = Expression.OrElse(expressions[i], expressions[i + 1]);
                            }
                            else if (i < expressions.Count - 1)
                            {
                                finalExpression = Expression.OrElse(finalExpression, expressions[i + 1]);
                            }
                        }
                    }

                }
            }
            return Expression.Lambda<Func<T, bool>>(finalExpression, entityParameter);

        }

        private List<Expression> CreateExpression(List<ExpressionModel> expressionModels, ParameterExpression entityParameter)
        {
            List<Expression> expressions = new List<Expression>();
            foreach (ExpressionModel expressionModel in expressionModels)
            {
                MemberExpression property = Expression.Property(entityParameter, expressionModel.ColumnName);

                Expression expression = null;
                switch (expressionModel.OperatorEnum)
                {
                    case OperatorEnum.Equal:
                        expression = Expression.Equal(property, Expression.Constant(expressionModel.Value));
                        break;
                    case OperatorEnum.GreaterThanOrEqual:
                        expression = Expression.GreaterThanOrEqual(property, Expression.Constant(expressionModel.Value));
                        break;
                    case OperatorEnum.LessThanOrEqual:
                        expression = Expression.LessThanOrEqual(property, Expression.Constant(expressionModel.Value));
                        break;
                    case OperatorEnum.Contains:
                        MethodInfo containsMethod = typeof(string).GetMethods().First(m => m.Name == "Contains" && m.GetParameters().Length == 1);
                        expression = Expression.Call(property, containsMethod, Expression.Constant(expressionModel.Value));
                        break;
                }

                expressions.Add(expression);
            }

            return expressions;
        }


    }
}
