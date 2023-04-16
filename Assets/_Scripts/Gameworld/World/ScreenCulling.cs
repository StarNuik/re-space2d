using SF = UnityEngine.SerializeField;
using UnityEngine;
using Zenject;
using PolygonArcana.Settings;
using PolygonArcana.Essentials;

namespace PolygonArcana.Entities
{
	public class ScreenCulling : MonoBehaviour
	{
		[Inject] GameSettings settings;
		
		[SF] new Camera camera;
		[SF] new BoxCollider2D collider;
		[SF] Rigidbody2DEvents events;
		
		private void Awake()
		{
			var rect = camera
				.OrthoSizeToRect()
				.WithMargin(settings.ScreenBorderMargin);
			collider.offset = rect.center;
			collider.size = rect.size;

			events.TriggerExit2D += OnTrigger;
		}

		private void OnTrigger(Collider2D collider)
		{
			if (!collider.attachedRigidbody.TryGetComponent<ICulled>(out var culled)) return;

			culled.OnOutOfScreen();
		}
	}
}
