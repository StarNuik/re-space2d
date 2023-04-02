using SF = UnityEngine.SerializeField;
using PolygonArcana.Essentials;
using UnityEngine;

namespace PolygonArcana.Installers
{
	public class PlayerInstaller : AMonoInstaller
	{
		[SF] new Rigidbody2D rigidbody;

		public override void InstallBindings()
		{
			Container.BindInstances(
				rigidbody
			);	
		}
	}
}