using Newtonsoft.Json;
using System.Text;

namespace Workouts.BusinessRuleLogic
{
    //TODO : Check And Fix
    public class BusinessRuleObjects
    {
        #region Responses
        public class BusinessRuleResponse
        {
            public BusinessRuleResponse()
            {

            }
            public BusinessRuleResponse(string errorMessage)
            {
                AddError(errorMessage);
            }

            public bool IsValid { get; private set; } = true;
            public string ErrorMessage { get; set; }

            public void AddError(string errorMessage)
            {
                ErrorMessage = errorMessage;
                IsValid = false;
            }
        }
        public class ChainOfBusinessRuleResponse
        {
            public ChainOfBusinessRuleResponse()
            {
                BusinessRuleResponses = new List<BusinessRuleResponse>();
            }
            public List<BusinessRuleResponse> BusinessRuleResponses { get; set; }
            public bool IsValid => !BusinessRuleResponses.Any(r => r.IsValid == false);
            public override string ToString() => JsonConvert.SerializeObject(this);

            public string ToMessage()
            {
                StringBuilder messageBuilder = new StringBuilder();

                var errors = BusinessRuleResponses.Where(b => b.IsValid == false);
                foreach (BusinessRuleResponse ruleResponse in errors)
                {
                    messageBuilder.AppendLine(ruleResponse.ErrorMessage);
                    messageBuilder.AppendLine();
                }

                return messageBuilder.ToString();
            }
        }

        #endregion

        public class BusinessRuleManager
        {
            public static ChainOfBusinessRuleResponse Control(bool stopOnFirstError, params Func<BusinessRuleResponse>[] rules)
            {
                ChainOfBusinessRuleResponse chainOfBusinessRuleResponse = new ChainOfBusinessRuleResponse();
                foreach (var rule in rules)
                {
                    BusinessRuleResponse businessRuleResponse = rule.Invoke();

                    chainOfBusinessRuleResponse.BusinessRuleResponses.Add(businessRuleResponse);
                    if (stopOnFirstError && businessRuleResponse.IsValid == false)
                        break;
                }

                return chainOfBusinessRuleResponse;
            }
        }
    }
}
