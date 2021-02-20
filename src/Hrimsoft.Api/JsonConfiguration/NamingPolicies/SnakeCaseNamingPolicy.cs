using System.Text.Json;
using Hrimsoft.StringCases;

namespace Hrimsoft.Api
{
    /// <summary>
    /// Converts property names to the snack_case
    /// </summary>
    public class SnakeCaseNamingPolicy: JsonNamingPolicy
    {
        public override string ConvertName(string name) => name.ToSnakeCase();
    }
}