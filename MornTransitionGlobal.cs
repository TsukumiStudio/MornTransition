using UnityEngine;

namespace MornLib
{
    [CreateAssetMenu(menuName = "Morn/" + nameof(MornTransitionGlobal), fileName = nameof(MornTransitionGlobal))]
    internal sealed class MornTransitionGlobal : MornGlobalBase<MornTransitionGlobal>
    {
        public override string ModuleName => nameof(MornTransitionAnimation);
        [SerializeField] private string[] _transitionNames;
        public string[] TransitionNames => _transitionNames;
    }
}