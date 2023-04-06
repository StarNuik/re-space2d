using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using SF = UnityEngine.SerializeField;

namespace PolygonArcana.Essentials
{
	public static class RectExtension
	{
		private static Rect NullRect => new(Vector2.zero, Vector2.zero);

		public static Rect Encapsulate(this Rect @this, Rect other)
		{
			var result = new Rect();

			result.xMin = Mathf.Min(@this.xMin, other.xMin);
			result.yMin = Mathf.Min(@this.yMin, other.yMin);
			result.xMax = Mathf.Max(@this.xMax, other.xMax);
			result.yMax = Mathf.Max(@this.yMax, other.yMax);

			return result;
		}

		public static Rect Encapsulate(this Rect @this, Vector2 point)
		{
			return @this.Encapsulate(
				new Rect(point, Vector2.zero)
			);
		}

		public static Rect Encapsulate(this Rect @this, IEnumerable<Vector2> points)
		{
			if (points == null) return @this;
			if (points.Count() == 0) return @this;

			//> are we the baddies?
			points = points
				.Append(@this.min)
				.Append(@this.max);
			
			return Encapsulate(points);
		}

		public static Rect Encapsulate(IEnumerable<Vector2> points)
		{
			if (points == null) return NullRect;
			if (points.Count() == 0) return NullRect;

			var first = points.First();
			var result = new Rect(first, Vector2.zero);

			foreach (var point in points)
			{
				result = result.Encapsulate(point);
			}

			return result;
		}

		public static Rect WithMargin(this Rect @this, float margin)
		{
			var result = @this;
			
			result.width += margin * 2f;
			result.height += margin * 2f;
			result.x -= margin;
			result.y -= margin;
			
			return result;
		}

		public static bool OverlapsCircleDumb(this Rect @this, Vector2 circlePosition, float circleRadius)
		{
			var rectPoints = new Vector2[]
			{
				@this.center,
				new (@this.xMin, @this.yMin),
				new (@this.xMin, @this.yMax),
				new (@this.xMax, @this.yMin),
				new (@this.xMax, @this.yMax),
			};

			bool isCircleInside = false;
			for (int i = 0; i < 5; i++)
			{
				var fromTo = (rectPoints[i] - circlePosition).normalized;
				var circlePoint = circlePosition + fromTo * circleRadius;

				isCircleInside |= @this.Contains(circlePoint);
			}

			return isCircleInside;
		}
	}
}
