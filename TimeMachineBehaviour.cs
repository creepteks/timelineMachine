using System;
using UnityEngine;
using UnityEngine.Playables;

namespace neo.timelineExtensions
{
    [Serializable]
    public class TimeMachineBehaviour : PlayableBehaviour
    {
        public TimeMachineAction action;
        public TimeMachineCondition condition;
        public string markerToJumpTo, markerLabel;
        public float timeToJumpTo;
        public TimelineConditional conditionalBehaviour;

        [HideInInspector]
        public bool clipExecuted = false; //the user shouldn't author this, the Mixer does

        public bool ConditionMet()
        {
            return conditionalBehaviour.IsConditionMet(condition);
        }

        public enum TimeMachineAction
        {
            Marker,
            JumpToTime,
            JumpToMarker,
            Pause,
        }
    }

}