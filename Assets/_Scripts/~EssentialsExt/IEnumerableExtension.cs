using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;

namespace PolygonArcana.Essentials
{
	public static class IEnumerableExtension
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Random<T>(this IEnumerable<T> collection)
		{
			Assert.IsNotNull(collection);

			var count = collection.Count();
			Assert.IsTrue(count >= 1);

			var randomIndex = UnityEngine.Random.Range(0, count);

			return collection.ElementAt(randomIndex);
		}

		public static (T, T) PairAroundF<T>(this IEnumerable<T> collection, float f)
		{
			Assert.IsNotNull(collection);

			var count = collection.Count();
			Assert.IsTrue(count >= 2);

			f = Mathf.Clamp01(f) * (count - 1);
			var iLower = Mathf.FloorToInt(f);
			var iHigher = Mathf.CeilToInt(f);

			//> ugly
			if (iLower == iHigher)
			{
				iHigher++;
				if (iHigher == count)
				{
					iLower--;
					iHigher--;
				}
			}

			var atLower = collection.Skip(iLower);
			
			return (atLower.First(), atLower.Skip(1).First());
		}
	}
}
