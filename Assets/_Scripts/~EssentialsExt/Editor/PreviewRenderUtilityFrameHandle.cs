using System;
using UnityEngine.Assertions;
using UnityEditor;
using UnityEngine;
using SF = UnityEngine.SerializeField;

namespace PolygonArcana.Essentials
{
	public partial class PreviewRenderUtilityExtension
	{
		public class FrameHandle : IDisposable
		{
			private bool isDisposed;
			private PreviewRenderUtility utility;
			private Rect rect;

			// // > i dont like the bool but making a second class
			// // > for the BeginStaticPreview is too much of a hassle
			public FrameHandle(PreviewRenderUtility utility, Rect rect, GUIStyle background/* , bool isStatic = false */)
			{
				Assert.IsNotNull(utility);
				Assert.IsNotNull(background);

				this.utility = utility;
				this.rect = rect;

				isDisposed = false;

				// if (isStatic)
				// 	utility.BeginStaticPreview(rect);
				// else
					utility.BeginPreview(rect, background);
			}

			~FrameHandle() => Dispose();

			public void Dispose()
			{
				if (isDisposed) return;

				utility.EndAndDrawPreview(rect);

				isDisposed = true;
			}
		}
	}
}
