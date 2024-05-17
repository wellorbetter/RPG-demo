using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] protected float cooldown;
    protected float cooldownTimer;

    protected virtual void Update()
    {
        cooldownTimer -= Time.deltaTime;
    }

    public virtual bool CanUseSkill()
    {
        if (cooldown < 0)
        {
            UseSkill();
            cooldownTimer = cooldown;
            return true;
        }
        return false; 
    }

    public virtual void UseSkill()
    {
        // 使用技能 具体的逻辑代码
    }
}
