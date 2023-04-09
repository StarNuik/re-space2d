using SF = UnityEngine.SerializeField;
using PolygonArcana.Essentials;
using UnityEngine;
using System.Collections.Generic;
using System;

//> mad mans ramblings, these are
//> The road to hell is paved with good intentions
namespace PolygonArcana._
{
	public interface ICollidesWith<TThis, TCol>
		where TCol : ICanCollide<TThis>
	{
		void OnCollision(TCol with);
	}

	public interface ICanCollide<T>
	{}

	public class CollisionsService : AService
	{
		private Dictionary<Rigidbody2D, Dictionary<Type, object>> listeners;
		private Dictionary<Rigidbody2D, Dictionary<Type, object>> targets;

		//> register
		public void RegisterListener<TThis, TCol>(ICollidesWith<TThis, TCol> listener, Rigidbody2D rigidbody)
			where TCol : ICanCollide<TThis>
		{
			var key = rigidbody;
			if (!listeners.ContainsKey(key))
				listeners.Add(key, new());
			
			var types = listeners[key];
			if (!types.ContainsKey(typeof(TCol)))
				types.Add(typeof(TCol), listener);
		}

		public void RegisterTarget<TCol>(ICanCollide<TCol> target, Rigidbody2D rigidbody)
		{
			var key = rigidbody;
			if (!targets.ContainsKey(key))
				targets.Add(key, new());
			
			var types = targets[rigidbody];
			if (!types.ContainsKey(typeof(TCol)))
				types.Add(typeof(TCol), target);
		}
	}
}
