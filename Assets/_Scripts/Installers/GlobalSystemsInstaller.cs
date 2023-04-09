using SF = UnityEngine.SerializeField;
using PolygonArcana.Essentials;
using PolygonArcana.Services;
using Zenject;
using System;

namespace PolygonArcana.Installers
{
	public class GlobalSystemsInstaller : AMonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.BindInterfacesAndSelfTo<RareTickService>()
				.FromNew()
				.AsSingle();			
		}
	}
}