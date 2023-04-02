using SF = UnityEngine.SerializeField;
using PolygonArcana.Essentials;
using PolygonArcana.Models;
using UnityEngine;
using Zenject;

namespace PolygonArcana.Views
{
	public class PlayerRotationView : AView<PlayerModel>
	{
		[Inject] new Rigidbody2D rigidbody;

		private InputData input => model.Input;

		private void Awake()
		{
			model.Input.OnChanged += OnInputChanged;
		}

		private void OnInputChanged()
		{
			var hasAttack = input.Attack != Vector2Int.zero;
			var targetStick = hasAttack ? input.Attack : input.Movement;

			if (targetStick == Vector2Int.zero) return;

			var direction = (Vector2)targetStick;
			var angle = Vector2.SignedAngle(Vector2.right, direction);

			rigidbody.MoveRotation(angle);
		}
	}
}
