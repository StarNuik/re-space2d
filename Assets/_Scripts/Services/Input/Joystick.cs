using SF = UnityEngine.SerializeField;
using UnityEngine;
using Zenject;
using PolygonArcana.Services;
using PolygonArcana.Essentials;
using PolygonArcana.Models;

namespace PolygonArcana
{
	public class Joystick : AMonoService<PlayerModel>
	{
		private void Update()
		{
			var movement = PollPad(
				KeyCode.D, KeyCode.A,
				KeyCode.W, KeyCode.S
			);
			var attack = PollPad(
				KeyCode.RightArrow, KeyCode.LeftArrow,
				KeyCode.UpArrow, KeyCode.DownArrow
			);

			model.Input.Set(
				new(movement, attack)
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

			if (Input.GetKeyDown(right)) result.x++;
			if (Input.GetKeyDown(left)) result.x--;
			if (Input.GetKeyDown(up)) result.y++;
			if (Input.GetKeyDown(down)) result.y--;

			return result;
		}
	}
}
