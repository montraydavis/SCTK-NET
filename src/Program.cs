using SCTK.Tokenizers;

namespace SCTK
{

    public class Program
    {

        public static void Main(string[] args)
        {

            TypeScriptTokenizer tsTokenizer = new TypeScriptTokenizer("data/universal/ts_tokens.out");
            tsTokenizer.LoadCorpus("data/typescript/simple_class.ts");

            var tokenized = tsTokenizer.Tokens.Select(x => (x.Name, x.EntityName));

            var tokenized_text = tokenized.Select(x => $"{x.Name}/{x.EntityName}").Aggregate((curr, next) => curr + "\n" + next);

            System.IO.File.WriteAllText("tkn.out", tokenized_text);
        }
    }
}



