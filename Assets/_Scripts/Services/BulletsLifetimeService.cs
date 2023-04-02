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

		public Bullet New()
		{
			var instance = NewInstance();

			{
				Assert.IsNotNull(instance);
				Assert.IsTrue(OwnedByThis(instance));
				Assert.IsTrue(!trackedBullets.Contains(instance));
			}

			trackedBullets.Add(instance);

			return instance;
		}

		public void Destroy(Bullet instance)
		{
			{
				Assert.IsTrue(OwnedByThis(instance));
				Assert.IsTrue(trackedBullets.Contains(instance));
			}

			trackedBullets.Remove(instance);
			DestroyInstance(instance);
		}

		//> switch to pools if performance drops
		private Bullet NewInstance()
		{
			var instance = factory.Create(settings.BulletPrefab);
			return instance;
		}

		//> switch to pools if performance drops
		private void DestroyInstance(Bullet instance)
		{
			Object.Destroy(instance.gameObject);
		}

		private bool OwnedByThis(Bullet instance)
		{
			return true;
		}
	}
}
