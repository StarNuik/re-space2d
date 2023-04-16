using System.Collections.Generic;
using PolygonArcana.Entities;
using PolygonArcana.Models;
using PolygonArcana.Services;
using PolygonArcana.Settings;
using UnityEngine;
using Zenject;
using SF = UnityEngine.SerializeField;

namespace PolygonArcana.Factories
{
	public class BulletFactory
	{
		[Inject] DiContainer container;
		[Inject] PrefabFactory factory;
		[Inject] GameSettings settings;
		
		[Inject] RareTickService rareTick;
		[Inject] MainModel mainModel;

		private List<BulletEntity> mainBullets => mainModel.Bullets;

		public BulletEntity Create()
		{
			var prefab = settings.BulletPrefab;
			var instance = container.InstantiatePrefabForComponent<BulletEntity>(prefab);
			
			rareTick.AddTarget(instance);
			mainBullets.Add(instance);
			mainModel.Bullets.InvokeChanged();
			
			return instance;
		}

		public void Destroy(BulletEntity instance)
		{
			rareTick.RemoveTarget(instance);
			mainBullets.Remove(instance);
			mainModel.Bullets.InvokeChanged();
			
			Object.Destroy(instance.gameObject);
		}
	}
}
