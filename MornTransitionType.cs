using System;
using MornEnum;

namespace MornLib
{
    [Serializable]
    public sealed class MornTransitionType : MornEnumBase
    {
        protected override string[] Values => MornTransitionGlobal.I.TransitionNames;
    }
}