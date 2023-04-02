using Zenject;
using UnityEngine;

namespace PolygonArcana.Factories
{
	public class PrefabFactory
	{
		private DiContainer container;

		public PrefabFactory(DiContainer container)
		{
			this.container = container;
		}

		public GameObject Create(GameObject prefab)
		{
			var instance = container.InstantiatePrefab(prefab);
			return instance;
		}

		public T Create<T>(T prefab)
			where T : Component
		{
			var instance = container.InstantiatePrefabForComponent<T>(prefab);
			return instance;
		}
	}
}
