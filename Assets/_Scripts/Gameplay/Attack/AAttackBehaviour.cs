using UnityEngine;
using PolygonArcana.Essentials;

namespace PolygonArcana.Settings
{
	public abstract class AAttackBehaviour : ASettings
	{
		public abstract object NewState();
		public abstract bool TryAttack(float time, object state, out AttackPattern pattern);
	}

	//> cringe on purpose
	//> attack behaviour is concerned with all attacking
	//> but as it is an SO, it has to keep the state somewhere else
	public abstract class AAttackBehaviour<TState> : AAttackBehaviour
		where TState : class, new()
	{
		public override object NewState()
			=> State();
		
		public override bool TryAttack(float time, object state, out AttackPattern pattern)
			=> TryAttack(time, (TState)state, out pattern);


		public virtual TState State()
			=> new TState();

		public abstract bool TryAttack(float time, TState state, out AttackPattern pattern);
	}
}
