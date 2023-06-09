using SF = UnityEngine.SerializeField;
using PolygonArcana.Essentials;
using PolygonArcana.Models;
using PolygonArcana.Services;
using PolygonArcana.Factories;
using UnityEngine;

namespace PolygonArcana._Test
{
	public class DevInstaller : AMonoInstaller
	{
		public override void InstallBindings()
		{
			//> factories
			SingleNew<PrefabFactory>();
			SingleNew<ClassFactory>();
			SingleNew<BulletFactory>();
			SingleNew<EnemyFactory>();

			//> models
			SingleNew<MainModel>();
			SingleNew<PlayerModel>();

			//> class services
			SingleNew<GamestateService>();
			SingleNew<BulletsLifetimeService>();
			SingleNew<EnemiesLifetimeService>();

			//> mono services
			SingleHierarchy<ScreenBoundsService>();
			
			//> 
			SingleHierarchy<Camera>();
		}
	}
}