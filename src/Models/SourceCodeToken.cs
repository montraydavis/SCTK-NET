using System;

namespace SCTK.Models
{
    public class SourceCodeToken
    {
        public string Name { get; set; }
        public string EntityName { get; set; }
        public SourceCodeTokenType TokenType { get; set; }

        public SourceCodeToken(string name, string entityName, SourceCodeTokenType tokenType = SourceCodeTokenType.NAN)
        {
            this.Name = name;
            this.EntityName = entityName;
            this.TokenType = SourceCodeTokenType.NAN;
        }
    }
}