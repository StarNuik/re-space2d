using UnityEngine;
using Zenject;

namespace PolygonArcana.Essentials
{
	//< is this an overcomplication or not
	public abstract class AMonoService : MonoBehaviour
	{
		//
	}

	public abstract class AMonoService<TModel> : AMonoService
		where TModel : AModel
	{
		[Inject]
		protected TModel model { get; private set; }
	}

	public abstract class AMonoService<TModel, TSettings> : AMonoService<TModel>
		where TModel : AModel
		where TSettings : ASettings
	{
		[Inject]
		protected TSettings settings { get; private set; }
	}
}