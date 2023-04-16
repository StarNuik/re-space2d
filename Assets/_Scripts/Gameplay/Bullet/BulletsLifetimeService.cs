using SF = UnityEngine.SerializeField;
using PolygonArcana.Essentials;
using PolygonArcana.Factories;
using Zenject;
using PolygonArcana.Models;
using PolygonArcana.Entities;
using PolygonArcana.Settings;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace PolygonArcana.Services
{
	public class BulletsLifetimeService : AService<MainModel, GameSettings>
	{
		[Inject] BulletFactory factory;

		private List<BulletEntity> trackedBullets => model.Bullets;

		public BulletEntity Take(
			Vector2 position,
			Vector2 direction,
			IBulletSetup setup
		)
		{
			var instance = NewInstance();
			instance.Initialize(position, direction, setup);
			instance.EnabledByPool = true;
			return instance;
		}

		public void Return(BulletEntity instance)
		{
			instance.EnabledByPool = false;
			DestroyInstance(instance);
		}

		//> switch to pools if performance drops
		private BulletEntity NewInstance()
		{
			return factory.Create();
		}

		private void DestroyInstance(BulletEntity bullet)
		{
			factory.Destroy(bullet);
		}
		//> switch to pools if performance drops
	}
}
