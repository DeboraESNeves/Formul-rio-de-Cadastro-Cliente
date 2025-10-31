namespace Formulario_Cadastro_Cliente.Models
{
    public class PageResult<T>
    {
        public PageResult(List<T> items, int pageNumber, int pageSize, int totalRecords)
        {
            Items = items;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalRecords = totalRecords;
        }

        public List<T> Items { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }


        public int TotalPages =>(int) Math.Ceiling((double)TotalRecords / PageSize);
        public bool HasPreviousPage => PageNumber > 0;
        public bool HasNextpage => PageNumber < TotalPages;

    }
}
