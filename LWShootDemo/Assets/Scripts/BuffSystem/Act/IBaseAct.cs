// using UnityEngine;
//
// namespace LWShootDemo.BuffSystem.Act
// {
//     public interface IBaseAct
//     {
//         bool Enable { get; }
//         bool Disable { get; }
//         IActOwner Owner { get; }
//         IGameEntity Entity { get; }
//         void Ctor(IActOwner e, BaseActProp p);
//         void Start(ActData acd);
//         void OnInputEvt(SkillEvt evt);
//         void Destroy();
//         bool IsValid(long id);
//         BaseActProp NewProp(int type);
//     }
// }