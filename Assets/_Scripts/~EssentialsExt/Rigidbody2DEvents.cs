using SF = UnityEngine.SerializeField;
using UnityEngine;
using System;

namespace PolygonArcana.Essentials
{
	public sealed class Rigidbody2DEvents : MonoBehaviour
	{
		public event Action<Collision2D> CollisionEnter2D;
		public event Action<Collision2D> CollisionExit2D;
		public event Action<Collision2D> CollisionStay2D;
		public event Action<Collider2D> TriggerEnter2D;
		public event Action<Collider2D> TriggerExit2D;
		public event Action<Collider2D> TriggerStay2D;

		private void OnCollisionEnter2D(Collision2D c)
			=> Invoke(CollisionEnter2D, c);

		private void OnCollisionExit2D(Collision2D c)
			=> Invoke(CollisionExit2D, c);

		private void OnCollisionStay2D(Collision2D c)
			=> Invoke(CollisionStay2D, c);
		
		private void OnTriggerEnter2D(Collider2D c)
			=> Invoke(TriggerEnter2D, c);
		
		private void OnTriggerExit2D(Collider2D c)
			=> Invoke(TriggerExit2D, c);
		
		private void OnTriggerStay2D(Collider2D c)
			=> Invoke(TriggerStay2D, c);

		private void Invoke<TArg>(Action<TArg> ev, TArg arg)
		{
			ev?.Invoke(arg);
		}
	}
}
