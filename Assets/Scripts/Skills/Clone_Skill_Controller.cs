using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Clone_Skill_Controller : MonoBehaviour
{
    private SpriteRenderer sr;
    private Animator anim;
    // 失去透明度的速度
    [SerializeField] private float colorLosingSpeed;
    // 在这个时间之后，clone就会逐渐消失
    // 也就是duration，持续时间
    private float cloneTimer;
    [SerializeField] private Transform attackCheck;
    [SerializeField] private float attackCheckRadius = .8f;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        cloneTimer -= Time.deltaTime;
        if (cloneTimer < 0)
        {
            sr.color = new Color(1, 1, 1, sr.color.a - colorLosingSpeed * Time.deltaTime);
        }
        // 如果这个clone的透明度小于0，那么就销毁这个clone
        if (sr.color.a < 0)
        {
            Destroy(gameObject);
        }
    }
    public void SetupClone(Transform _clonePosition, float _cloneDuration, bool _canAttack)
    {
        if (_canAttack)
        {
            anim.SetInteger("AttackNumber", Random.Range(1, 4));
        }
        transform.position = _clonePosition.position;
        cloneTimer = _cloneDuration;
    }

    private void AnimationTrigger()
    {
        cloneTimer = -1f;
    }
    private void AttackTrigger()
    {
        //Physics2D.OverlapCircleAll 会
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);
        foreach (var hit in colliders)
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
}