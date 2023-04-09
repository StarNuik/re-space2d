using PolygonArcana.Entities;
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
		[Inject] RareTickService rareTick;
		[Inject] PrefabFactory factory;
		[Inject] GameSettings settings;

		public Bullet Create()
		{
			var prefab = settings.BulletPrefab;
			var instance = container.InstantiatePrefabForComponent<Bullet>(prefab);
			rareTick.AddTarget(instance);
			return instance;
		}

		public void Destroy(Bullet instance)
		{
			rareTick.RemoveTarget(instance);
			Object.Destroy(instance.gameObject);
		}
	}
}
