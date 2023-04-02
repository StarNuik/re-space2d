using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace PolygonArcana.Essentials
{
	public static partial class PreviewRenderUtilityExtension
	{
		const string quadPath = "Quad.fbx";
		const string materialPath = "Editor/UnlitSprite";

		private static Lazy<Mesh> quadMesh = new(
			() => Resources.GetBuiltinResource<Mesh>(quadPath)
		);

		private static Lazy<Material> spriteMaterial
			= ResourcesExtension.LoadLazy<Material>("Editor/UnlitSprite");

		//> draws an XY facing quad
		public static void DrawSprite(
			this PreviewRenderUtility @this,
			Sprite sprite,
			Vector2 position,
			float rotation = 0f,
			float size = 1f
		)
		{
			Assert.IsNotNull(sprite);

			var modelMatrix = Matrix4x4.TRS(
				(Vector3)position,
				Quaternion.Euler(0f, 0f, rotation),
				//> mult by 0.5f here because
				//> Unity's quad mesh is 2 x 2 units
				Vector3.one * 0.5f * size
			);

			var properties = new MaterialPropertyBlock();
			properties.SetTexture("_MainTex", sprite.texture);
			
			Graphics.DrawMesh(
				quadMesh.Value,
				modelMatrix,
				spriteMaterial.Value,
				0,
				@this.camera,
				0,
				properties,
				false,
				false,
				false
			);
		}
	
		public static FrameHandle WithFrame(this PreviewRenderUtility utility, Rect rect, GUIStyle background)
			=> new FrameHandle(utility, rect, background);

		// public static Meow WithStaticFrame(this PreviewRenderUtility utility, Rect rect, GUIStyle background)
		// 	=> new Meow(utility, rect, background, true);
	}
}
