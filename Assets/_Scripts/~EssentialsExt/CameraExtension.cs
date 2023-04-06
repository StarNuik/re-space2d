using UnityEngine;
using UnityEngine.Assertions;
using SF = UnityEngine.SerializeField;

namespace PolygonArcana.Essentials
{
	public static class CameraExtension
	{
		//> sets the ortho box to bounds
		//> and positions the camera on the back edge plane
		public static void WithOrthoBounds(this Camera @this, Bounds bounds)
		{
			@this.orthographic = true;
			@this.nearClipPlane = 0.01f;
			@this.farClipPlane = bounds.size.z;

			var camTransform = @this.transform;
			camTransform.rotation = Quaternion.identity;
			camTransform.position = bounds.center - camTransform.forward * bounds.extents.z;
		
			@this.OrthoFitUnits(Mathf.Max(bounds.size.x, bounds.size.y));
			// @this.orthographicSize = bounds.extents.y;
		}

		public static void OrthoFitToRect(this Camera @this, Rect rect)
		{
			if (!@this.orthographic) return;

			var position = @this.transform.position;
			position -= position.TakeXY0();
			position += (Vector3)rect.center;
			@this.transform.position = position;

			var maxSide = Mathf.Max(rect.width, rect.height);
			@this.OrthoFitUnits(maxSide);
		}

		//> sets the size to fit x units with respect to aspect ratio
		public static void OrthoFitUnits(this Camera @this, float units)
		{
			if (!@this.orthographic) return;
			
			//> half size the units
			units *= 0.5f;

			@this.orthographicSize = (@this.aspect > 1f)
				? units
				: (units / @this.aspect);
		}

		public static Rect OrthoSizeToRect(this Camera @this, float margin = 0f)
		{
			Assert.IsTrue(@this.orthographic);

			var center = @this.transform.position.ToXY();
			var size = new Vector2(
				@this.orthographicSize * @this.aspect,
				@this.orthographicSize
			);

			var result = new Rect();
			result.size = size * 2f + Vector2.one * margin * 2f;
			result.center = center;
			//> .orthoSize is a HALF size
			return result;
		}
	}
}
