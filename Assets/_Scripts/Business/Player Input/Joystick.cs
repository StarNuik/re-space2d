using SF = UnityEngine.SerializeField;
using UnityEngine;
using Zenject;
using PolygonArcana.Services;
using PolygonArcana.Essentials;
using PolygonArcana.Models;

namespace PolygonArcana
{
	public class Joystick : MonoBehaviour, IJoystick
	{
		public Vector2Int Movement { get; private set; }
		public Vector2Int Attack { get; private set; }
		public BoolTrigger SubmitTrigger { get; } = new();
		public BoolTrigger CancelTrigger { get; } = new();

		private void Update()
		{
			Movement = PollPad(
				KeyCode.D, KeyCode.A,
				KeyCode.W, KeyCode.S
			);
			Attack = PollPad(
				KeyCode.RightArrow, KeyCode.LeftArrow,
				KeyCode.UpArrow, KeyCode.DownArrow
			);
			SubmitTrigger.Set(
				Input.GetKeyDown(KeyCode.Comma)
			);
			CancelTrigger.Set(
				Input.GetKeyDown(KeyCode.Period)
			);
		}

		private Vector2Int PollPad(
			KeyCode right,
			KeyCode left,
			KeyCode up,
			KeyCode down
		)
		{
			var result = Vector2Int.zero;

			if (Input.GetKey(right)) result.x++;
			if (Input.GetKey(left)) result.x--;
			if (Input.GetKey(up)) result.y++;
			if (Input.GetKey(down)) result.y--;

			return result;
		}
	}
}
