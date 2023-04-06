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
		[Inject] PrefabFactory factory;

		private List<Bullet> trackedBullets => model.Bullets;

		public Bullet Take(
			Vector2 position,
			Vector2 direction,
			IBulletSettup setup
		)
		{
			var instance = New();
			instance.Initialize(position, direction, setup);
			instance.EnabledByPool = true;
			return instance;
		}

		public void Return(Bullet instance)
		{
			{
				Assert.IsTrue(trackedBullets.Contains(instance));
			}

			instance.EnabledByPool = false;
			trackedBullets.Remove(instance);
			DestroyInstance(instance);
			model.Bullets.InvokeChanged();
		}

		private Bullet New()
		{
			var instance = NewInstance();

			{
				Assert.IsNotNull(instance);
				Assert.IsTrue(!trackedBullets.Contains(instance));
			}

			trackedBullets.Add(instance);
			model.Bullets.InvokeChanged();

			return instance;
		}

		//> switch to pools if performance drops
		private Bullet NewInstance()
		{
			var instance = factory.Create(settings.BulletPrefab);
			return instance;
		}

		//> switch to pools if performance drops
		private void DestroyInstance(Bullet bullet)
		{
			//! ?????????
			Object.Destroy(bullet.gameObject);
		}
	}
}
