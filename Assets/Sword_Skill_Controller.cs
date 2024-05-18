using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Skill_Controller : MonoBehaviour
{
    [SerializeField] private float returnSpeed = 12;
    private Animator anim;
    private Rigidbody2D rb;
    private CircleCollider2D cd;
    private Player player;


    private bool canRotate = true;
    private bool isReturning;

    // 这里不能用start,rb在start里面获取不到，更新之后就是这样，有空研究研究
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CircleCollider2D>();
    }

    public void SetupSword(Vector2 _dir, float _gravityScale)
    {
        player = PlayerManager.instance.player;
        rb.velocity = _dir;
        rb.gravityScale = _gravityScale;
        // 创建剑的时候，让剑旋转
        anim.SetBool("Rotation", true);
    }

    // 设置成isKinematic就只会受到transform的影响，不会受到物理引擎的影响
    // 直接让剑通过transform的方式回到玩家手中
    public void ReturnSword()
    {
        rb.isKinematic = false;
        // transform.parent设置为null，就会脱离父对象，成为独立的对象
        transform.parent = null;
        // 正在返回
        isReturning = true;
    }

    // 强制对象的右方向与其速度方向对齐。这意味着无论对象向哪个方向移动，它都会自动面朝该方向
    private void Update()
    {
        if (canRotate)
        {
            transform.right = rb.velocity;
        }
        // 正在回收剑
        if (isReturning)
        {
            // MoveTowards 会使物体向目标移动，直到它到达目标位置
            // 参数：当前位置，目标位置，移动速度
            // Time.deltaTime 是每帧的时间间隔
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, returnSpeed * Time.deltaTime);
            // 通过transform回收剑，如果距离比较近了，就回收剑
            if (Vector2.Distance(transform.position, player.transform.position) < 1)
            {
                player.ClearTheSword();
            }
        }
    }

    // 被卡住了就不旋转了
    private void OnTriggerEnter2D(Collider2D collision)
    {
        anim.SetBool("Rotation", false);
        canRotate = false;
        cd.enabled = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.parent = collision.transform;
    }
}
