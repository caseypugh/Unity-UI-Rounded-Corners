using UnityEngine;
using UnityEngine.UI;

namespace Nobi.UiRoundedCorners {
	[RequireComponent(typeof(RectTransform))]
	public class ImageWithRoundedCorners : MonoBehaviour {
		private static readonly int Props = Shader.PropertyToID("_WidthHeightRadius");

		public float radius;
		private Material material;

		[HideInInspector, SerializeField] private MaskableGraphic image;

		private void OnValidate() {
			if (Application.isPlaying) {
				Validate();
				Refresh();
			}
		}

		private void OnDestroy() {
			DestroyHelper.Destroy(material);
			image = null;
			material = null;
		}

		private void OnEnable() {
			if (Application.isPlaying) {
				Validate();
				Refresh();
			}
		}

		private void OnRectTransformDimensionsChange() {
			if (enabled && material != null && Application.isPlaying) {
				Refresh();
			}
		}

		public void Validate() {
			if (material == null) {
				material = new Material(Shader.Find("UI/RoundedCorners/RoundedCorners"));
			}

			if (image == null) {
				TryGetComponent(out image);
			}

			if (image != null) {
				image.material = material;
			}
		}

		public void Refresh() {
			var rect = ((RectTransform)transform).rect;
			material.SetVector(Props, new Vector4(rect.width, rect.height, radius, 0));
		}
	}
}
