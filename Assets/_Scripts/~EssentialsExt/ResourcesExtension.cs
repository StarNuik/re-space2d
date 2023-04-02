using System;
using UnityEngine;
using SF = UnityEngine.SerializeField;

namespace PolygonArcana.Essentials
{
	using Object = UnityEngine.Object;

	public static class ResourcesExtension
	{
		public static Lazy<TAsset> LoadLazy<TAsset>(string path)
			where TAsset : Object
			=> new(
				() => Resources.Load<TAsset>(path)
			);
	}
}
