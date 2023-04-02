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

		private static Lazy<Material> spriteMaterial = new(
			() => new(
				Resources.Load<Material>(materialPath)
			)
		);

		//> draws an XY facing quad
		public static void DrawSprite(
			this PreviewRenderUtility @this,
			Sprite sprite,
			Vector2 position,
			float rotation
		)
		{
			Assert.IsNotNull(sprite);

			var material = spriteMaterial.Value;
			material.mainTexture = sprite.texture;
			
			var modelMatrix = Matrix4x4.TRS(
				(Vector3)position,
				Quaternion.Euler(0f, 0f, rotation),
				Vector3.one * 0.5f
			);
			
			Graphics.DrawMesh(
				quadMesh.Value,
				modelMatrix,
				material,
				0,
				@this.camera,
				0,
				null,
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
