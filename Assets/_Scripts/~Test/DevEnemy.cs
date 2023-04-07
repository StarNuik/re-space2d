using SF = UnityEngine.SerializeField;
using UnityEngine;
using PolygonArcana.Settings;
using PolygonArcana.Essentials;

namespace PolygonArcana._Test
{
	public class DevEnemy : MonoBehaviour
	{
		[SF] MovementBehaviour movementBehaviour;

		private Location2D startLoc;

		public float TimeOffset { get; set; }

		private void Awake()
		{
			startLoc = transform.ToLocation2D();
		}

		private void FixedUpdate()
		{
			var time = Time.time - TimeOffset;
			var location = movementBehaviour.Evaluate(time);
			
			transform.SetLocation2D(location.AddOffsetOf(startLoc));
		}
	}
}
