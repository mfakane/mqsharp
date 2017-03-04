using System.Collections;
using System.Collections.Generic;

namespace Metasequoia
{
	public class SceneGlobalLightCollection : IReadOnlyList<GlobalLight>
	{
		public Scene Scene { get; }
		public GlobalLight this[int index] =>
			index < Count ? new GlobalLight(Scene, index) : null;

		public int Count
		{
			get
			{
				var rt = new int[1];

				NativeMethods.MQScene_IntValue(Scene.Handle, MQScene.GetMultilightNumber, rt);

				return rt[0];
			}
		}

		public SceneGlobalLightCollection(Scene scene) =>
			Scene = scene;

		public GlobalLight Add()
		{
			var rt = new int[1];

			NativeMethods.MQScene_IntValue(Scene.Handle, MQScene.AddMultilight, rt);

			return rt[0] == -1 ? null : new GlobalLight(Scene, rt[0]);
		}

		public bool Remove(GlobalLight light) =>
			RemoveAt(light.Index);

		public bool RemoveAt(int index)
		{
			var rt = new int[] { index, 0 };

			NativeMethods.MQScene_IntValue(Scene.Handle, MQScene.DeleteMultilight, rt);

			return rt[0] != 0;
		}

		public IEnumerator<GlobalLight> GetEnumerator()
		{
			for (var i = 0; i < Count; i++)
				yield return this[i];
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}
