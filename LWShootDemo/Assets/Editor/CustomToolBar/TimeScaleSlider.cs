// using UnityEditor;
// using UnityEngine;
//
// namespace UnityToolbarExtender
// {
//     [InitializeOnLoad]
//     public class TimeScaleSlider
//     {
//         private static float targetTimeScale = 1;
//         
//         static TimeScaleSlider()
//         {
//             ToolbarExtender.RightToolbarGUI.Add(OnToolbarGUI);
//         }
//
//         private static void OnToolbarGUI()
//         {
//             EditorGUILayout.LabelField("Time", GUILayout.Width(30));
//             targetTimeScale =
//                 EditorGUILayout.Slider("", targetTimeScale, 0, 10, GUILayout.Width(200 - 30.0f));
//             
//             if (GUILayout.Button("Reset", GUILayout.Width(50)))
//             {
//                 targetTimeScale = 1;
//             }
//
//             if (EditorApplication.isPlaying)
//             {
//                 SoulMono.SetTimeScale(1, float.PositiveInfinity, targetTimeScale, SoulEase.Ease.None, null);
//             }
//         }
//     }
// }
