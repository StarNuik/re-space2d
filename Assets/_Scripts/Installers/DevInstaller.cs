using SF = UnityEngine.SerializeField;
using PolygonArcana.Essentials;
using PolygonArcana.Models;
using PolygonArcana.Services;
using PolygonArcana.Factories;
using UnityEngine;

namespace PolygonArcana.Installers
{
	public class DevInstaller : AMonoInstaller
	{
		public override void InstallBindings()
		{
			SingleNew<PrefabFactory>();

			SingleNew<MainModel>();
			SingleNew<PlayerModel>();

			SingleNew<GamestateService>();
			SingleNew<BulletsLifetimeService>();

			SingleHierarchy<BulletsTickService>();
			SingleHierarchy<Camera>();
		}
	}
}