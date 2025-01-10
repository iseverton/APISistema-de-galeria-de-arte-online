namespace SistemaDeGaleriaDeArteAPI.ViewModels
{
    public class ResultViewModels<T>
    {
        public T? Data { get; private set; }
        public List<string> Erros { get; private set; } = new();


        // dados e erros
        public ResultViewModels(T? data, List<string> erros)
        {
            Data = data;
            Erros = erros;
        }

        // apenas com dados (sucesso)
        public ResultViewModels(T? data)
        {
            Data = data;
        }

        // apenas com uma lista de erros
        public ResultViewModels(List<string> erros)
        {
            Erros = erros;
        }

        // apenas com um unico erro
        public ResultViewModels(string erro)
        {
            Erros.Add(erro);
        }
    }
}
