namespace Formulario_Cadastro_Cliente.Models
{
    public class PageResult
    {
        public int TotalItens { get; set; }
        public int PaginaAtual { get; set; }
        public int TamanhoPagina { get; set; }
        public int TotalPaginas { get; set; }
        public int PaginaInicial { get; set; }
        public int PaginaFinal { get; set; }



        public PageResult()
        {

        }

        public PageResult(int totalItens, int pagina, int tamanhoPagina = 10)
        {
            int totalPaginas = (int)Math.Ceiling((decimal)totalItens / (decimal)tamanhoPagina);
            int paginaAtual = pagina;

            int paginaInicial = paginaAtual - 5;
            int paginaFinal = paginaAtual + 4;

            if (paginaInicial <= 0)
            {
                paginaFinal = paginaFinal - (paginaInicial - 1);
                paginaInicial = 1;
            }

            if (paginaFinal > totalPaginas)
            {
                paginaFinal = totalPaginas;
                if (paginaFinal > 10)
                {
                    paginaInicial = paginaFinal - 9;
                }
            }

            TotalItens = totalItens;
            PaginaAtual = paginaAtual;
            TamanhoPagina = tamanhoPagina;
            TotalPaginas = totalPaginas;
            PaginaInicial = paginaInicial;
            PaginaFinal = paginaFinal;
        }

    }

}
