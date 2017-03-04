namespace Metasequoia
{
	public class NewDocumentParameters
    {
		public XmlElement Element { get; }

		public NewDocumentParameters(XmlElement element) =>
			Element = element;
    }
}
