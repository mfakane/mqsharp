using System;
using System.Collections.Generic;

namespace Metasequoia
{
	public sealed class DrawingText : IDisposable
    {
		public IntPtr Handle { get; }
		public Document Document { get; set; }
		public bool IsInstantDrawingText { get; private set; }

		DrawingText(Document doc, IntPtr ptr)
		{
			Document = doc;
			Handle = ptr;
		}

		public static DrawingText CreateDrawingText(Document doc, string text, Point position, Color color, DrawingTextParameters param, bool isInstant = true)
		{
			using (var textPtr = new UnicodeString(text))
			using (var positionPtr = new PinnedStructure<Point>(position))
			using (var colorPtr = new PinnedStructure<Color>(color))
			using (var fontScalePtr = new PinnedStructure<float>(param.FontScale ?? 1))
			using (var horizontalAlignmentPtr = new PinnedStructure<int>((int)param.HorizontalAlignment))
			using (var verticalAlignmentPtr = new PinnedStructure<int>((int)param.VerticalAlignment))
			using (var isInstantPtr = new PinnedStructure<bool>(isInstant))
			{
				var dict = new Dictionary<string, IntPtr>
				{
					["document"] = doc.Handle,
					["text"] = textPtr,
					["color"] = colorPtr,
					["instant"] = isInstantPtr,
					["result"] = IntPtr.Zero,
				};

				if (param.FontScale.HasValue) dict["font_scale"] = fontScalePtr;
				if (horizontalAlignmentPtr.Value != 0) dict["horz_align"] = horizontalAlignmentPtr;
				if (verticalAlignmentPtr.Value != 0) dict["vert_align"] = verticalAlignmentPtr;

				using (var args = new NamedPtrDictionary(dict))
				{
					Metaseq.SendMessage(MQMessage.NewDrawText, args);

					return new DrawingText(doc, args["result"])
					{
						IsInstantDrawingText = isInstant,
					};
				}
			}
		}

		public void Dispose()
		{
			if (!IsInstantDrawingText)
				using (var args = new NamedPtrDictionary(new Dictionary<string, IntPtr>
				{
					["document"] = Document.Handle,
					["object"] = Handle,
				}))
					Metaseq.SendMessage(MQMessage.DeleteDrawObject, args);
		}
	}
}
