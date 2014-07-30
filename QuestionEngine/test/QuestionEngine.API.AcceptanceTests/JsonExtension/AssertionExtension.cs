using System.Collections.Generic;
using FluentAssertions.Execution;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace QuestionEngine.API.AcceptanceTests.JsonExtension
{
    public static class AssertionExtensions
    {

        public static void BeValidJson(this FluentAssertions.Primitives.StringAssertions assertions, JsonSchema schema, string reason = "", params object[] reasonArgs)
        {
            IList<string> errors;
            Execute.Verification
               .ForCondition(JObject.Parse(assertions.Subject).IsValid(schema, out errors))
               .BecauseOf(reason, reasonArgs)
               .FailWith("Expected object to be valid json {0}{reason}", errors);
        }
    }
}
