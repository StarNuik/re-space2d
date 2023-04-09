using System.Collections.Generic;
using PolygonArcana.Entities;
using PolygonArcana.Essentials;
using PolygonArcana.Models;
using PolygonArcana.Services;
using UnityEngine;
using Zenject;
using SF = UnityEngine.SerializeField;

namespace PolygonArcana.Factories
{
	public abstract class AEntityFactory<TResult>
		where TResult : MonoBehaviour, IRareTickable
	{
		[Inject] PrefabFactory factory;
		[Inject] RareTickService rareTick;

		protected abstract IChange<List<TResult>> trackerList { get; }
		private List<TResult> entities => trackerList;

		public abstract TResult Create();
		public abstract void Destroy(TResult instance);

		protected TResult ACreate(TResult prefab)
		{
			var instance = factory.Create(prefab);

			rareTick.AddTarget(instance);
			entities.Add(instance);
			trackerList.InvokeChanged();

			return instance;
		}

		protected void ADestroy(TResult instance)
		{
			rareTick.RemoveTarget(instance);
			entities.Remove(instance);
			trackerList.InvokeChanged();

			//> god, I h8 the MB public api's open-ness
			Object.Destroy(instance.gameObject);
		}
	}
}
