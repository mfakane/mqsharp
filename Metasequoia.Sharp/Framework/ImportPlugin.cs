using System;
using System.Collections.Generic;

namespace Metasequoia
{
	public abstract class ImportPlugin : Plugin, IImportPlugin
	{
		bool IImportPlugin.IsBackground { get; set; }
		IntPtr IImportPlugin.ImportOptions { get; set; }
		public bool IsBackground => ((IImportPlugin)this).IsBackground;

		public bool IsCanceled
		{
			get
			{
				using (var result = new PinnedStructure<bool>())
				using (var option = new NamedPtrDictionary(new Dictionary<string, IntPtr>
				{
					["options"] = ((IImportPlugin)this).ImportOptions,
					["result"] = result,
				}))
				{
					Metaseq.SendMessage(MQMessage.ImportQueryCancel, option);

					return result.Value;
				}
			}
		}

		public abstract bool SupportsBackgroundLoading { get; }
		public abstract FileFilter[] AvailableFileTypes { get; }

		public abstract bool Import(FileFilter type, string path, Document doc);

		protected void SetProgress(float value)
		{
			using (var progressPtr = new PinnedObject(value))
			using (var option = new NamedPtrDictionary(new Dictionary<string, IntPtr>
			{
				["options"] = ((IImportPlugin)this).ImportOptions,
				["progress"] = progressPtr,
			}))
			{
				Metaseq.SendMessage(MQMessage.ImportProgress, option);
			}
		}
	}
}
