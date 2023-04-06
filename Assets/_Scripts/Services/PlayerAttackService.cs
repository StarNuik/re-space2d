using SF = UnityEngine.SerializeField;
using PolygonArcana.Essentials;
using PolygonArcana.Models;
using Zenject;
using PolygonArcana.Settings;
using UnityEngine;

namespace PolygonArcana.Services
{
	public class PlayerAttackService : AMonoService<PlayerModel, PlayerSettings>
	{
		//! ????????
		//! ????????
		//! ????????
		//! ????????
		[SF] Transform attackTransform;

		//! ????????
		[Inject] new Rigidbody2D rigidbody;

		[Inject] BulletsLifetimeService bulletsLifetime;

		private float lastAttack = float.NegativeInfinity;

		private Vector2Int input => model.Input.Value.Attack;
		private bool isAttacking => input.magnitude > 0f;
		private bool attackCooledDown => Time.time >= lastAttack + settings.AttackPeriod;

		private void FixedUpdate()
		{
			if (!isAttacking || !attackCooledDown) return;
			lastAttack = Time.time;

			attackTransform.LookInDirection2D((Vector2)input);

			foreach (var (localPosition, localDirection) in settings.TMP_AttackPattern.Points)
			{
				var position = attackTransform.TransformPoint(localPosition);
				var direction = attackTransform.TransformDirection(localDirection);

				bulletsLifetime.Take(position, direction, settings.BulletInfo);
			}
		}
	}
}
