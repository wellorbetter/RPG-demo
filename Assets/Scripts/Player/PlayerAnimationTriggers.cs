using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();
    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }
    private void AttackTrigger()
    {
        //Physics2D.OverlapCircleAll 会
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);
        foreach(var hit in colliders)
        {
            // 检查碰撞体是否有Enemy组件
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy != null)
            {
                // 对敌人造成伤害
                enemy.Damage();
            }
        }
    }
    private void ThrowSword()
    {
        SkillManager.instance.sword.CreateSword();
    }
}
