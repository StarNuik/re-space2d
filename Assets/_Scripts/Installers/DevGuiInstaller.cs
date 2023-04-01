using SF = UnityEngine.SerializeField;
using PolygonArcana.Essentials;
using PolygonArcana.Services;
using PolygonArcana.Models;

namespace PolygonArcana.Installers
{
	public class DevGuiInstaller : AMonoInstaller
	{
		public override void InstallBindings()
		{
			SingleNew<MainModel>();
			SingleNew<GamestateService>();
		}
	}
}