using UnityEngine;

namespace GameMain
{
    public class BuffEvent概念
    {
        // 1.
        // 每个Event都可以被添加一组可以执行的Action

        // Event会在某些时机被触发，触发时会执行这个Event上的所有Action
        
        // 每个Action都会在执行时传入这个Event带有的执行Action特定的参数 : XXEventActArgs 不同Event的参数是不同的
        
        // 这些XXEventActArgs都会继承自一个相同的基类 : EventActArgsBase 这个类里有一些所有Event执行Action时都可以用到的一些参数

        // 因为同一事件的Action参数相同，所以不同的Event可以被添加的Action是不同的
        
        // 但是同时，也有一些Action仅需要一些通用的参数，所以他们可以被添加进不同的Event
        
        // Buff类是一个包含多个Event的类，他可以添加不同的Event，并且监听这些Event的触发，当这些Event触发时，Event对应的参数会被执行
        
        // 2.        
        // 我希望这些Event的命名更改为BuffEvent的样式，因为他们是专属于Buff的Event
        
        // 每个Buff应该有一个存储数据的部分 : BuffData，这个BuffData应该是一个ScriptableObject，这样可以在编辑器中创建
        
        // 我希望在BuffData上配置这个Buff的所有Event，和所有的Action
        
        // 3.
        // 我希望每个Event都只能在BuffData的Inspector上添加Event对应的Action，你可以使用Odin来进行辅助，我希望这个BuffData显示的数据清晰，样式美观
        
        // 每种Event都拥有一个执行事件的参数，而他对应的Action也都只能接收这种事件参数，我希望在BuffData上操作这些Event的时候，往这些Event里增加Action的时候只添加可以接收这种事件参数的Action
        
        // 4. 
        // 现在的BuffData上只有一组DamageEvent 我希望的是每种BuffData上可以动态的添加各种类型的BuffEvent 每种Event只需要有一个就够了
        // 我希望可以用Odin写一些支持，可以在BuffData上动态的添加BuffEvent，然后在BuffEvent上动态的添加Action，最好是一个下拉框
        
        // 5. 
        // 我希望每个Action上也有一些自定义的参数， 例如DamageAction上可能有一个Damage参数，这个参数可以在BuffData上配置，然后在BuffData上配置的时候，可以在DamageAction上看到这个参数，并且对他进行配置
        
        // 6.
        // 我希望这个Action列表可以显示得更美观一点 并且每个Action可以选择启用或者禁用，并且需要有一些颜色上的区分
    }
}