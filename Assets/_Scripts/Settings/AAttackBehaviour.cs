using UnityEngine;
using PolygonArcana.Essentials;

namespace PolygonArcana.Settings
{
	public abstract class AAttackBehaviour : ASettings
	{
		public abstract bool TryAttack(float time, object state, out AttackPattern pattern);
	}

	public abstract class AAttackBehaviour<TState> : AAttackBehaviour
		where TState : class
	{
		//> cringe on purpose
		//> attack behaviour is concerned with all attacking
		//> but as it is an SO, it has to keep state somewhere else
		public override bool TryAttack(float time, object state, out AttackPattern pattern)
			=> TryAttack(time, (TState)state, out pattern);

		public abstract bool TryAttack(float time, TState state, out AttackPattern pattern);
	}
}
