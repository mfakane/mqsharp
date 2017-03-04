using System.Linq;
using Metasequoia;

namespace Explosion
{
	class ExplosionCommand : IPluginCommand
	{
		public string Caption { get; } = "エクスプロージョン";

		public bool Execute(Document doc)
		{
			foreach (var obj in doc.Objects)
				foreach (var face in obj.Faces.Where(i => i.IsSelected).ToArray())
				{
					var normal = face.Normal;
					var pow = 20;
					var newVertices = face.Vertices.Select(v => obj.Vertices.Add(v.Position + normal * pow, v));
					var newFace = obj.Faces.Add(newVertices, face);

					newFace.IsSelected = true;
					face.Remove();
				}

			return true;
		}
	}
}
