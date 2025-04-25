using BankEase.Services.Interfaces;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace BankEase.Services
{
    public class XmlService<T> : IXmlService<T> where T : class
    {
        public XmlService(string path)
        {
            if (!Directory.Exists(path)) 
            {
                Directory.CreateDirectory(path);
            }
            var fileName = typeof(T).Name + ".xml";   
            string filePath = Path.Combine($"{path}", fileName);
            if (!File.Exists(filePath)) 
            {
                var document = new XDocument(new XElement($"{typeof(T).Name}"));
                document.Save(filePath);
            }
            FilePath = filePath;
        }
        public string FilePath { get; set; }
        public XDocument GetDocument() => XDocument.Load(FilePath);
        public T Add(T item)
        {
            var doc = GetDocument();
            var root = doc.Root;

            var serializer = new XmlSerializer(typeof(T));
            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, item);
                var element = XElement.Parse(writer.ToString());
                root.Add(element);
            }
            doc.Save(FilePath);

            return item;
        }
        public List<T> GetAll()
        {
            var doc = GetDocument();
            var serializer = new XmlSerializer(typeof(T));
            var result = new List<T>();

            foreach (var element in doc.Root.Elements()) 
            {
                using (var reader = element.CreateReader())
                {
                    if (serializer.Deserialize(reader) is T item) 
                    {
                        result.Add(item);
                    }
                }
            }
            return result;
        }

        public T GetElement(string propertyName, string value) 
        {
            var element = GetDocument().Descendants();

            var res = element
            .FirstOrDefault(e => (e.Attribute(propertyName)?.Value == value?.ToString() ||
                                  e.Element(propertyName)?.Value == value?.ToString()));
            if (res == null) 
            {
                return null;
            }

            var serializer = new XmlSerializer(typeof(T));
            using (var reader = res.CreateReader()) 
            {
                if (serializer.Deserialize(reader) is T item)
                {
                    return item;
                }
            }
            return null;
        }

        public T Update(T item, string propertyName, string value) 
        {
            var doc = GetDocument();
            var element = doc.Descendants()
                .FirstOrDefault(e => (e.Attribute(propertyName)?.Value == value?.ToString() ||
                                      e.Element(propertyName)?.Value == value?.ToString()));

            if (element == null)
            {
                return null;
            }

            var serializer = new XmlSerializer(typeof(T));
            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, item);
                var updatedElement = XElement.Parse(writer.ToString());
                element.ReplaceWith(updatedElement);
            }

            doc.Save(FilePath);
            return item;
        }

    }
}
