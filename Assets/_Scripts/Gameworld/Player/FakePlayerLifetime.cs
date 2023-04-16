using SF = UnityEngine.SerializeField;
using UnityEngine;
using PolygonArcana._Test;
using PolygonArcana.Settings;
using PolygonArcana.Entities;
using NaughtyAttributes;

namespace PolygonArcana
{
	public class FakePlayerLifetime : MonoBehaviour
	{
		[SF] PlayerEntity target;
		[SF] Joystick joystick;
		[SF] PlayerSettings settings;

		[Button]
		private void Enable()
		{
			target.Initialize(joystick, settings);
			target.EnabledByPool = true;
		}
	}
}
