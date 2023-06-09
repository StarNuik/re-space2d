using UnityEngine;
using PolygonArcana.Essentials;
using SF = UnityEngine.SerializeField;

namespace PolygonArcana.Settings
{
	[CreateAssetMenu(
		menuName = ("Settings/Attack Behaviours/" + nameof(RepeatPattern)),
		fileName = nameof(RepeatPattern)
	)]
	public class RepeatPattern : AAttackBehaviour<RepeatPattern.StateObject>
	{
		public class StateObject
		{
			public float LastAttackTime { get; set; } = -1f;
		}

		[SF] AttackPattern pattern;
		[SF] float period;

		public override bool TryAttack(float time, StateObject state, out AttackPattern pattern)
		{
			pattern = null;

			if (state.LastAttackTime == -1f)
			{
				state.LastAttackTime = period;
			}

			var isPewFrame = time > state.LastAttackTime;
			if (isPewFrame)
			{
				pattern = this.pattern;
				state.LastAttackTime = time + period;
			}

			return isPewFrame;
		}
	}
}
