namespace ObjectResponse_POC.Model
{
    public class ObjectResponse<T>
    {
        public bool Success { get; set; } = true;
        public T? Data { get; set; }
        public string? Message { get; set; }
    }
}
