using System;

namespace Metasequoia
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class PluginAttribute : Attribute
    {
		public string DisplayName { get; }
		public uint AuthorId { get; }
		public uint PluginId { get; }

		public PluginAttribute(string displayName, uint authorId, uint pluginId)
		{
			DisplayName = displayName;
			AuthorId = authorId;
			PluginId = pluginId;
		}
    }
}
