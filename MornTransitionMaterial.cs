using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace MornLib
{
    internal sealed class MornTransitionMaterial : MornTransitionBase
    {
        [SerializeField] private Image _image;
        [SerializeField] private string _fillRatePropertyName;
        [SerializeField] private string _isClearPropertyName;
        [SerializeField] private float _fillDuration;
        [SerializeField] private float _clearDuration;
        private Material _material;
        private int _rateId;
        private int _isClearId;
        private CancellationTokenSource _cts;

        private void Awake()
        {
            // 操作用にマテリアルを複製してImageに入れる
            _material = new Material(_image.materialForRendering);
            _image.material = _material;
            _rateId = Shader.PropertyToID(_fillRatePropertyName);
            _isClearId = Shader.PropertyToID(_isClearPropertyName);
        }

        public override async UniTask FillAsync(CancellationToken ct = default)
        {
            _cts?.Cancel();
            _cts = CancellationTokenSource.CreateLinkedTokenSource(ct);
            var start = _material.GetFloat(_rateId);
            _material.SetFloat(_isClearId, 0);
            await ColorTweenTask(start, 1, _fillDuration * (1 - start), _cts.Token);
        }

        public override async UniTask ClearAsync(CancellationToken ct = default)
        {
            _cts?.Cancel();
            _cts = CancellationTokenSource.CreateLinkedTokenSource(ct);
            var start = _material.GetFloat(_rateId);
            _material.SetFloat(_isClearId, 1);
            await ColorTweenTask(start, 0, _clearDuration * start, _cts.Token);
        }

        public override void FillImmediate()
        {
            _cts?.Cancel();
            _cts = null;
            _material.SetFloat(_rateId, 1);
            _material.SetFloat(_isClearId, 0);
        }

        public override void ClearImmediate()
        {
            _cts?.Cancel();
            _cts = null;
            _material.SetFloat(_rateId, 0);
            _material.SetFloat(_isClearId, 1);
        }

        private async UniTask ColorTweenTask(float start, float end, float duration, CancellationToken ct = default)
        {
            if (duration > 0)
            {
                var elapsedTime = 0f;
                while (elapsedTime < duration)
                {
                    elapsedTime += Time.unscaledDeltaTime;
                    _material.SetFloat(_rateId, Mathf.Lerp(start, end, elapsedTime / duration));
                    await UniTask.Yield(ct);
                }
            }

            _material.SetFloat(_rateId, end);
        }
    }
}