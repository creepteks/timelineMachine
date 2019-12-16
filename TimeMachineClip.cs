using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace neo.timelineExtensions
{
    [Serializable]
    public class TimeMachineClip : PlayableAsset, ITimelineClipAsset
    {
        [HideInInspector]
        public TimeMachineBehaviour template = new TimeMachineBehaviour();

        public TimeMachineBehaviour.TimeMachineAction action;
        public TimeMachineCondition condition;
        public string markerToJumpTo = "", markerLabel = "";
        public float timeToJumpTo = 0f;
        public ExposedReference<TimelineConditional> interactionDetector;


        public ClipCaps clipCaps => ClipCaps.None;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<TimeMachineBehaviour>.Create(graph, template);
            TimeMachineBehaviour clone = playable.GetBehaviour();
            clone.markerToJumpTo = markerToJumpTo;
            clone.conditionalBehaviour = interactionDetector.Resolve(graph.GetResolver());
            clone.action = action;
            clone.condition = condition;
            clone.markerLabel = markerLabel;
            clone.timeToJumpTo = timeToJumpTo;

            return playable;
        }
    }
}