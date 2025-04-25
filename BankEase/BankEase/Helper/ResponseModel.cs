namespace BankEase.Helper
{
    public record ResponseModel<T>
    {
        public T Data { get; init; }
        public bool Success { get; init; }
        public string? Message { get; set; }
    }
}
