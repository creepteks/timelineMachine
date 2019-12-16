using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace neo.timelineExtensions
{
    [CustomEditor(typeof(TimeMachineClip))]
    public class TimeMachineClipInspector : Editor
    {
        private SerializedProperty actionProp, conditionProp;

        private void OnEnable()
        {
            actionProp = serializedObject.FindProperty("action");
            conditionProp = serializedObject.FindProperty("condition");
        }

        public override void OnInspectorGUI()
        {
            bool isMarker = false; //if it's a marker we don't need to draw any Condition or parameters

            //Action
            EditorGUILayout.PropertyField(actionProp);

            //change the int into an enum
            int index = actionProp.enumValueIndex;
            TimeMachineBehaviour.TimeMachineAction actionType = (TimeMachineBehaviour.TimeMachineAction)index;

            //Draws only the appropriate information based on the Action Type
            switch (actionType)
            {
                case TimeMachineBehaviour.TimeMachineAction.Marker:
                    isMarker = true;
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("markerLabel"));
                    break;

                case TimeMachineBehaviour.TimeMachineAction.JumpToMarker:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("markerToJumpTo"));
                    break;

                case TimeMachineBehaviour.TimeMachineAction.JumpToTime:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("timeToJumpTo"));
                    break;
            }


            if (!isMarker)
            {
                //Condition
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Logic", EditorStyles.boldLabel);

                //change the int into an enum
                index = conditionProp.enumValueIndex;
                TimeMachineCondition conditionType = (TimeMachineCondition)index;

                //Draws only the appropriate information based on the Condition type
                switch (conditionType)
                {
                    case TimeMachineCondition.Always:
                        EditorGUILayout.HelpBox("The above action will always be executed.", MessageType.Warning);
                        EditorGUILayout.PropertyField(conditionProp);
                        break;

                    case TimeMachineCondition.Never:
                        EditorGUILayout.HelpBox("The above action will never be executed. Practically, it's as if clip was disabled.", MessageType.Warning);
                        EditorGUILayout.PropertyField(conditionProp);
                        break;
                    case TimeMachineCondition.ConditionalNotMet:
                        EditorGUILayout.HelpBox("Select the interaction detector to check if timeline should rewind or move forward", MessageType.Info);
                        EditorGUILayout.PropertyField(conditionProp);
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("interactionDetector"));
                        break;

                    case TimeMachineCondition.ConditionalMet:
                        EditorGUILayout.HelpBox("select the interaction detector to check if the condition is met and timeline should move forward", MessageType.Info);
                        EditorGUILayout.PropertyField(conditionProp);
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("interactionDetector"));
                        break;


                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }

}