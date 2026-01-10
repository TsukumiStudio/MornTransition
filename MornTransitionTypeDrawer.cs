#if UNITY_EDITOR
using MornEnum;
using UnityEditor;
using UnityEngine;

namespace MornLib
{
    [CustomPropertyDrawer(typeof(MornTransitionType))]
    internal class MornTransitionTypeDrawer : MornEnumDrawerBase
    {
        protected override string[] Values => MornTransitionGlobal.I.TransitionNames;
        protected override Object PingTarget => MornTransitionGlobal.I;
    }
}
#endif