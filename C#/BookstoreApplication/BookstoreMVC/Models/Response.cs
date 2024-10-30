namespace BookstoreMVC.Models
{
    public class Response
    {
        public Response() { }
        public string message { get; set; }
        public List<Book> data { get; set; }
        public int statusCode { get; set; }

        public Response(string message, int statusCode, List<Book> data =default)
        {
            this.message = message;
            this.data = data;
            this.statusCode = statusCode;
        }
    }
}
