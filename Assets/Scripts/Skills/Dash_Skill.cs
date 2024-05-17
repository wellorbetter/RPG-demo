using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Alexdev喜欢把技能的逻辑放在Skill里面
// 这样就不用进行强制类型转换，但是就会很乱，我感觉还是得分开
public class Dash_Skill : Skill
{
    // 冲刺技能集成到这里之后，Plyaer里面的dashTImer就不用了 cooldown也是
    // 但是怎么说呢，我其实感觉这个技能具体的逻辑代码实际上应该放在Skill里面
    // 还是等以后学好设计模式再来优化吧
    // 还是优化一下，感觉有蚂蚁在爬
    private void Awake()
    {
        // 可以在这里设置冷却时间，也可以直接在unity里面设置，看个人喜好
    }
    public override void UseSkill()
    {
        base.UseSkill();
    }
    public override bool CanUseSkill()
    {
        if (skillTimer < 0)
        {
            UseSkill();
            skillTimer = cooldown;
            return true;
        }
        return false;
    }
}
