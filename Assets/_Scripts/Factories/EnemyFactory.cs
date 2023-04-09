using System.Collections.Generic;
using PolygonArcana.Entities;
using PolygonArcana.Essentials;
using PolygonArcana.Models;
using PolygonArcana.Services;
using PolygonArcana.Settings;
using UnityEngine;
using Zenject;
using SF = UnityEngine.SerializeField;

namespace PolygonArcana.Factories
{
	using Enemy = Entities.Enemy;

	public class EnemyFactory : AEntityFactory<Enemy>
	{
		[Inject] GameSettings settings;
		[Inject] MainModel model;

		protected override IChange<List<Enemy>> trackerList => model.Enemies;

		public override Enemy Create()
			=> ACreate(settings.EnemyPrefab);

		public override void Destroy(Enemy instance)
			=> ADestroy(instance);
	}
}
