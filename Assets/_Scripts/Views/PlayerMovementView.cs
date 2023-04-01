using SF = UnityEngine.SerializeField;
using PolygonArcana.Essentials;
using PolygonArcana.Models;
using PolygonArcana.Settings;
using UnityEngine;

namespace PolygonArcana.Views
{
	public class PlayerMovementView : AView<PlayerModel, PlayerSettings>
	{
		[SF] new Rigidbody2D rigidbody;

		private InputData input => model.Input;

		private void FixedUpdate()
		{
			var direction = (Vector2)input.Movement;
			var delta = direction.normalized * settings.TMP_MoveSpeed;

			rigidbody.MovePosition(rigidbody.position + delta * Time.deltaTime);
		}
	}
}
