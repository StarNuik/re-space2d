using SF = UnityEngine.SerializeField;
using UnityEngine;
using PolygonArcana.Entities;
using Zenject;
using PolygonArcana.Factories;
using PolygonArcana.Essentials;
using PolygonArcana.Services;

namespace PolygonArcana
{
	public class DevEnemySpawner : MonoBehaviour
	{
		[Inject] EnemiesLifetimeService enemiesLifetime;

		[SF] Enemy prefab;
		[SF] Settings.Enemy setup;

		public void Spawn()
		{
			const float spawnRadius = 10f;

			var location = new Location2D(
				Random.insideUnitCircle * spawnRadius,
				Random.insideUnitCircle.normalized
			);

			enemiesLifetime.Take(location, setup);
		}
	}
}
