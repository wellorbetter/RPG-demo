using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    protected float skillTimer; // 各种技能的计时器
    [SerializeField] protected float cooldown;  // 技能的冷却时间
    protected Player player;
    protected virtual void Start()
    {
        player = PlayerManager.instance.player;
    }
    protected virtual void Update()
    {
        skillTimer -= Time.deltaTime;
    }

    public virtual bool CanUseSkill()
    {
        // 这种判断一直都是判断Timer的，而不是cooldown
        // 之前这里写成了cooldown < 0，谨记切勿再犯
        return true;
    }

    public virtual void UseSkill()
    {
        // 使用技能 具体的逻辑代码
    }
}
