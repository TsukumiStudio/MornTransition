using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace MornLib
{
	internal sealed class MornTransitionFade : MornTransitionBase
	{
		[SerializeField] private Image _image;
		[SerializeField] private SpriteRenderer _renderer;
		[SerializeField] private float _fillDuration;
		[SerializeField] private MornEaseType _fillEase;
		[SerializeField] private float _clearDuration;
		[SerializeField] private MornEaseType _clearEase;

		public override async UniTask FillAsync(CancellationToken ct = default)
		{
			var elapsed = 0f;
			while (elapsed < _fillDuration)
			{
				elapsed += Time.deltaTime;
				var easedT = Mathf.Clamp01(elapsed / _fillDuration).Ease(_fillEase);
				SetAlpha(easedT);
				await UniTask.Yield(PlayerLoopTiming.Update, ct);
			}

			SetAlpha(1f);
		}

		public override async UniTask ClearAsync(CancellationToken ct = default)
		{
			var elapsed = 0f;
			while (elapsed < _clearDuration)
			{
				elapsed += Time.deltaTime;
				var easedT = 1f - Mathf.Clamp01(elapsed / _clearDuration).Ease(_clearEase);
				SetAlpha(easedT);
				await UniTask.Yield(PlayerLoopTiming.Update, ct);
			}

			SetAlpha(0f);
		}

		public override void FillImmediate()
		{
			SetAlpha(1f);
		}

		public override void ClearImmediate()
		{
			SetAlpha(0f);
		}

		private void SetAlpha(float alpha)
		{
			if (_image != null)
			{
				var color = _image.color;
				color.a = alpha;
				_image.color = color;
			}

			if (_renderer != null)
			{
				var color = _renderer.color;
				color.a = alpha;
				_renderer.color = color;
			}
		}
	}
}