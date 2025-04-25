namespace BankEase.Services.Interfaces
{
    public interface IXmlService<T> where T : class
    {
        public string FilePath { get; set; }
        T Add(T item);
        List<T> GetAll();
        T GetElement(string propertyName, string value);
        T Update(T item, string propertyName, string value);
    }
}
