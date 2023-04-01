using SF = UnityEngine.SerializeField;
using PolygonArcana.Essentials;
using PolygonArcana.Models;
using PolygonArcana.Services;

namespace PolygonArcana.Installers
{
	public class DevInstaller : AMonoInstaller
	{
		public override void InstallBindings()
		{
			SingleNew<MainModel>();

			SingleNew<GamestateService>();
		}
	}
}