using SF = UnityEngine.SerializeField;
using PolygonArcana.Essentials;
using PolygonArcana.Models;
using PolygonArcana.Settings;
using UnityEngine;
using Zenject;

namespace PolygonArcana.Views
{
	public class PlayerMovementView : AView<PlayerModel, PlayerSettings>
	{
		[Inject] new Rigidbody2D rigidbody;

		private InputData input => model.Input;

		private void FixedUpdate()
		{
			var direction = (Vector2)input.Movement;
			var delta = direction.normalized * settings.TMP_MoveSpeed;

			rigidbody.MovePosition(rigidbody.position + delta * Time.deltaTime);
		
			//> not sure if this is the best place, but oh well
			model.Location.Set(rigidbody.transform.ToLocation2D());
		}
	}
}
