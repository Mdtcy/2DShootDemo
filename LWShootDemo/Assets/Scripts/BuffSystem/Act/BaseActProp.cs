// using UnityEngine;
// using System;
// using Sirenix.OdinInspector;
//
// namespace LWShootDemo.BuffSystem.Act
// {
//
//     /// <summary>
//     /// 操作
//     /// </summary>
//     [Serializable]
//     public abstract class BaseActProp
//     {
//         [HideIf(nameof(Hide))]
//         [HorizontalGroup("@labelName/1", Width = 0.6f)]
//         [ReadOnly]
//         [HideLabel]
//         [TitleGroup("@labelName", alignment: TitleAlignments.Centered)]
//         [GUIColor(nameof(GetTypeColor))]
//         [ShowInInspector]
//         [PropertyOrder(-1)]
//         public EventActType EvtType
//         {
//             get { return EventAct.GetEvtActType(evtType); }
//             set { evtType = (long) value; }
//         }
//
//         [HideInInspector] [SerializeField] public long evtType;
//
//         [HideIf(nameof(Hide))]
//         [HorizontalGroup("@labelName/1", width: 0.2f)]
//         [HideLabel]
//         [GUIColor(nameof(GetStateColor))]
//         public ActState state;
//
//
//         string m_labelName;
//
//         string labelName
//         {
//             get
//             {
//                 if (string.IsNullOrEmpty(m_labelName))
//                 {
//                     m_labelName = OdinTool.GetLabelText(EvtType, EvtType.ToString());
//                 }
//
//                 return m_labelName;
//             }
//         }
//
//         bool Hide
//         {
//             get { return evtType == 0; }
//         }
//
//         private Color GetTypeColor()
//         {
//             //76 41 76 ; 68 50 68
//             return new Color(238 / 255f * 1.5f, 130 / 255f * 0.3f, 238 / 255f * 1.5f, 1);
//         }
//
//         private Color GetStateColor()
//         {
//             switch (state)
//             {
//                 case ActState.Disable:
//                     return Color.gray;
//                 case ActState.Single:
//                     return Color.red;
//                 case ActState.Enable:
//                 default:
//                     return Color.green;
//             }
//         }
//     }
// }