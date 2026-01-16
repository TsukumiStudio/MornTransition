using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace MornLib
{
    public sealed class MornTransitionCore : MornGlobalMonoBase<MornTransitionCore>
    {
        protected override string ModuleName => "MornTransitionCore";
        private List<MornTransitionBase> _allTransitions;
        private List<MornTransitionBase> _activeTransitions;

        protected override void OnInitialized()
        {
            _allTransitions = new List<MornTransitionBase>(GetComponentsInChildren<MornTransitionBase>());
            _activeTransitions = new List<MornTransitionBase>();
        }

        private static bool TryGetTransition(MornTransitionType transitionType, out MornTransitionBase transition)
        {
            transition = I._allTransitions.Find(t => t.Type.Key == transitionType.Key);
            return transition != null;
        }

        public static bool IsFilling()
        {
            return I._activeTransitions.Count > 0;
        }

        public async static UniTask FillAsync(MornTransitionType transitionType, CancellationToken ct = default)
        {
            if (TryGetTransition(transitionType, out var transition))
            {
                I._activeTransitions.Add(transition);
                await transition.FillAsync(ct);
            }
        }

        public async static UniTask ClearAsync(CancellationToken ct = default)
        {
            var taskList = new List<UniTask>();
            foreach (var transition in I._activeTransitions)
            {
                taskList.Add(transition.ClearAsync(ct));
            }

            I._activeTransitions.Clear();
            await UniTask.WhenAll(taskList);
        }

        public static void FillImmediate(MornTransitionType transitionType)
        {
            if (TryGetTransition(transitionType, out var transition))
            {
                I._activeTransitions.Add(transition);
                transition.FillImmediate();
            }
        }

        public static void ClearImmediate()
        {
            foreach (var transition in I._activeTransitions)
            {
                transition.ClearImmediate();
            }

            I._activeTransitions.Clear();
        }
    }
}