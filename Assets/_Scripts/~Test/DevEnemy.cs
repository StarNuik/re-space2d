using SF = UnityEngine.SerializeField;
using UnityEngine;
using PolygonArcana.Settings;
using PolygonArcana.Essentials;

namespace PolygonArcana
{
	public class DevEnemy : MonoBehaviour
	{
		// [SF] new Rigidbody2D rigidbody;
		[SF] MovementBehaviour movementBehaviour;
		
		public float TimeOffset { get; set; }

		private void FixedUpdate()
		{
			var time = Time.time - TimeOffset;
			var location = movementBehaviour.Evaluate(time);
			
			transform.position = location.origin;
			transform.SetLookDirection2D(location.direction);
		}
	}
}
