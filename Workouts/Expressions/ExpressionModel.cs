using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workouts.Expressions
{
    public class ExpressionModel
    {
        public string ColumnName { get; set; }
        public OperatorEnum OperatorEnum { get; set; }
        public object Value { get; set; }
    }
}
