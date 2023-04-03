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

		public void InitializeNew(
			Vector2 position,
			Vector2 direction,
			Bullet.ISetupInfo setupInfo
		)
		{
			var bullet = New();
			bullet.Initialize(position, direction, setupInfo);
		}

		public Bullet New()
		{
			var instance = NewInstance();

			{
				Assert.IsNotNull(instance);
				Assert.IsTrue(OwnedByThis(instance));
				Assert.IsTrue(!trackedBullets.Contains(instance));
			}

			trackedBullets.Add(instance);
			model.Bullets.InvokeChanged();

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
			model.Bullets.InvokeChanged();
		}

		//> switch to pools if performance drops
		private Bullet NewInstance()
		{
			var view = factory.Create(settings.BulletPrefab);
			return new Bullet(view);
		}

		//> switch to pools if performance drops
		private void DestroyInstance(Bullet bullet)
		{
			//! ?????????
			// Object.Destroy(bullet.View.Rigidbody.gameObject);
		}

		private bool OwnedByThis(Bullet instance)
		{
			return true;
		}
	}
}
