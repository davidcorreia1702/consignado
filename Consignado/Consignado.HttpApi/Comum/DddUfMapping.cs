namespace Consignado.HttpApi.Comum
{
    public static class DddUfMapping
    {
        public static readonly Dictionary<string, string> DddToUf = new Dictionary<string, string>
            {
                { "11", "SP" },
                { "21", "RJ" },
                { "31", "MG" },
                { "61", "DF" },
                // Adicione todos os mapeamentos necessários
            };
    }
}
