// using LWShootDemo.BuffSystem.Event;
// using UnityEngine;
// using UnityEditor;
//
// [CustomEditor(typeof(ActionData), true)] // true参数表示这个Editor也应用于所有的子类
// public class ActionDataEditor : Editor
// {
//     public override void OnInspectorGUI()
//     {
//         var actionData = target as ActionData;
//
//         // 检查对象的实际类型是否是泛型，并且泛型有两个参数
//         if (actionData.GetType().IsGenericType && actionData.GetType().GetGenericArguments().Length == 2)
//         {
//             var args = actionData.GetType().GetGenericArguments();
//             // 在Editor界面上显示TAct的类型名称
//             EditorGUILayout.LabelField("Action Type: " + args[1].Name);
//         }
//
//         DrawDefaultInspector(); // 绘制默认的Inspector界面
//     }
// }