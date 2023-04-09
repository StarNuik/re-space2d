using SF = UnityEngine.SerializeField;
using PolygonArcana.Essentials;
using PolygonArcana.Models;
using PolygonArcana.Settings;
using Zenject;
using PolygonArcana.Factories;
using System.Collections.Generic;
using PolygonArcana.Entities;

namespace PolygonArcana.Services
{
	using Enemy = Entities.Enemy;

	public class EnemiesLifetimeService : AService<MainModel>
	{
		[Inject] EnemyFactory factory;

		private List<Enemy> enemies => model.Enemies;

		public Enemy Take(
			Location2D location,
			Settings.Enemy settings
		)
		{
			var instance = factory.Create();
			instance.Initialize(location, settings);
			instance.EnabledByPool = true;
			return instance;
		}

		public void Return(Enemy instance)
		{
			instance.EnabledByPool = false;
			factory.Destroy(instance);
		}
	}
}
