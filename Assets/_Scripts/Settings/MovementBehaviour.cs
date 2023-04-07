using UnityEngine;
using SF = UnityEngine.SerializeField;
using PolygonArcana.Essentials;

namespace PolygonArcana.Settings
{
	[CreateAssetMenu(
		menuName = ("Settings/" + nameof(MovementBehaviour)),
		fileName = nameof(MovementBehaviour)
	)]
	public class MovementBehaviour : ASettings
	{
		[SF] MovementPattern pattern;
		[SF] MovementEvalFunc evalFunction;
		[SF] float duration;

		public Location2D Evaluate(float localTime)
		{
			var f = evalFunction.GetF(localTime, duration);
			var location = pattern.Evaluate(f);
			return location;
		}
	}
}
