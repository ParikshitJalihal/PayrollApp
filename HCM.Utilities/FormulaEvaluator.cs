using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Utilities
{
    public class FormulaEvaluator
    {
        private readonly Dictionary<string, decimal> _componentValues;

        public FormulaEvaluator(Dictionary<string, decimal> componentValues)
        {
            _componentValues = componentValues;
        }

        public decimal Evaluate(string formula)
        {
            if (string.IsNullOrWhiteSpace(formula)) return 0;

            // Replace placeholders with actual values
            foreach (var kvp in _componentValues)
            {
                formula = formula.Replace($"#{kvp.Key}#", kvp.Value.ToString());
            }

            // Handle percentages (e.g., *50%#)
            formula = formula.Replace("%", "/100");

            // Evaluate expression safely
            var result = new DataTable().Compute(formula, "");
            return Convert.ToDecimal(result);
        }
    }

}
