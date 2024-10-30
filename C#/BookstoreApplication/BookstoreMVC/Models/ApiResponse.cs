namespace BookstoreMVC.Models
{
    public class ApiResponse<T>
    {
        public ApiResponse() { }
        public string message { get; set; }
        public T? data { get; set; }
        public int statusCode { get; set; }

        public ApiResponse(string message, int statusCode, T? data = default)
        {
            this.message = message;
            this.data = data;
            this.statusCode = statusCode;
        }
    }
}
