using SF = UnityEngine.SerializeField;
using PolygonArcana.Essentials;
using Zenject;
using System.Collections.Generic;
using PolygonArcana.Entities;
using System.Linq;

namespace PolygonArcana.Services
{
	public class RareTickService : AService, IFixedTickable
	{
		private List<IRareTickable> targets = new();

		public void AddTarget(IRareTickable target)
		{
			targets.Add(target);
		}

		public void RemoveTarget(IRareTickable target)
		{
			targets.Remove(target);
		}

		public void FixedTick()
		{
			var nullCount = 0;

			var count = targets.Count - 1;
			for (int i = count; i >= 0; i--)
			{
				var t = targets[i];

				t?.RareTick();

				nullCount += t == null ? 1 : 0;
			}

			//> cull null's
			if (nullCount > 0)
			{
				targets = targets
					.Where(t => t != null)
					.ToList();
			}
		}
	}
}
