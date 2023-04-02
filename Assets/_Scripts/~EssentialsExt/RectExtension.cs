using UnityEngine;
using SF = UnityEngine.SerializeField;

namespace PolygonArcana.Essentials
{
	public static class RectExtension
	{
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

		public static Rect WithMargin(this Rect @this, float margin)
		{
			var result = @this;
			
			result.width += margin * 2f;
			result.height += margin * 2f;
			result.x -= margin;
			result.y -= margin;
			
			return result;
		}
	}
}
