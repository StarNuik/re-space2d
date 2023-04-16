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
	using EnemyEntity = Entities.EnemyEntity;

	public class EnemyFactory : AEntityFactory<EnemyEntity>
	{
		[Inject] GameSettings settings;
		[Inject] MainModel model;

		protected override IChange<List<EnemyEntity>> trackerList => model.Enemies;

		public override EnemyEntity Create()
			=> ACreate(settings.EnemyPrefab);

		public override void Destroy(EnemyEntity instance)
			=> ADestroy(instance);
	}
}
