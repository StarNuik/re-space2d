using SF = UnityEngine.SerializeField;
using PolygonArcana.Essentials;
using Zenject;
using PolygonArcana.Settings;
using UnityEngine;

namespace PolygonArcana.Installers
{
	[CreateAssetMenu(
		menuName = ("Settings/Installers/" + nameof(SettingsInstaller)),
		fileName = nameof(SettingsInstaller)
	)]
	public class SettingsInstaller : ScriptableObjectInstaller
	{
		[SF] GuiSettings guiSettings;
		[SF] GameSettings gameSettings;
		[SF] PlayerSettings playerSettings;

		public override void InstallBindings()
		{
			Container.BindInstances(
				guiSettings,
				gameSettings,
				playerSettings
			);
		}
	}
}