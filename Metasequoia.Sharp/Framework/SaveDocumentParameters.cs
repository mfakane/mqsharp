namespace Metasequoia
{
	public class SaveDocumentParameters
    {
		public XmlElement Element { get; }
		public bool SaveUniqueId { get; set; }

		public SaveDocumentParameters(XmlElement element) =>
			Element = element;
	}
}
