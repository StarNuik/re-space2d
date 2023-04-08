using SF = UnityEngine.SerializeField;
using UnityEngine;
using PolygonArcana.Entities;
using Zenject;
using PolygonArcana.Factories;
using PolygonArcana.Essentials;

namespace PolygonArcana
{
	public class DevEnemySpawner : MonoBehaviour
	{
		[Inject] PrefabFactory factory;

		[SF] Enemy prefab;
		[SF] Settings.Enemy setup;

		public void Spawn()
		{
			const float spawnRadius = 10f;

			var location = new Location2D(
				Random.insideUnitCircle * 10f,
				Random.insideUnitCircle.normalized
			);

			var instance = factory.Create(prefab);
			instance.Initialize(location, setup);
			instance.EnabledByPool = true;
		}
	}
}
