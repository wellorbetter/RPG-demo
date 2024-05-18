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

    [Header("Bounce Info")]
    [SerializeField] private float bounceSpeed;
    // 是否可以来回弹
    private bool isBouncing;
    // 有几次弹的机会
    private int amoutOfBounce;
    // 第一次击中之后，周围的敌人
    private List<Transform> enemyTarget;
    private int targetIndex;

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
        enemyTarget = new List<Transform>();
    }

    // 设置成isKinematic就只会受到transform的影响，不会受到物理引擎的影响
    // 直接让剑通过transform的方式回到玩家手中
    public void ReturnSword()
    {
        // RigidbodyConstraints2D.FreezeAll 
        // 这里感觉效果一样
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        //rb.isKinematic = true; // 这里只要触发回收了，就不要受gravity影响了
        // transform.parent设置为null，就会脱离父对象，成为独立的对象
        transform.parent = null;
        // 正在返回
        isReturning = true;
    }

    // 强制对象的右方向与其速度方向对齐。这意味着无论对象向哪个方向移动，它都会自动面朝该方向
    private void FixedUpdate()
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
            // 这里改了，因为用Update莫名其妙的卡一卡的，这里应当是没有什么计算的
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, returnSpeed * Time.fixedDeltaTime);
            // 通过transform回收剑，如果距离比较近了，就回收剑
            if (Vector2.Distance(transform.position, player.transform.position) < 1)
            {
                player.CatchTheSword();
            }
        }

        BounceLogic();
    }

    private void BounceLogic()
    {
        // 如果可以弹 而且有敌人（这个是不是有可以弹的敌人会在第一次击中敌人的时候算出来）
        if (isBouncing && enemyTarget.Count > 0)
        {
            // 如果在弹的过程中，按下了回收剑的键，就回收剑
            if (isReturning)
            {
                ReturnSword();
                return;
            }
            // 使剑弹向可以弹的敌人
            transform.position = Vector2.MoveTowards(transform.position, enemyTarget[targetIndex].position, bounceSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, enemyTarget[targetIndex].position) < .1f)
            {
                // 弹下一个
                targetIndex++;
                amoutOfBounce--;
                // 没法弹了就回来
                if (amoutOfBounce == 0)
                {
                    isBouncing = false;
                    isReturning = true;
                }
                // 如果弹完了，重新去从第一个开始弹
                if (targetIndex >= enemyTarget.Count)
                {
                    targetIndex = 0;
                }
            }
        }
    }

    // 被卡住了就不旋转了
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 如果正在回收的时候，就暂时先不管下面的
        if (isReturning)
            return;
        // 同时，只要没有可以弹的敌人，那就不会弹，只会卡住

        // collision 是碰撞到的对象 如果它有Enemy组件，就说明是敌人
        if (collision.GetComponent<Enemy>() != null)
        {
            if (isBouncing && enemyTarget.Count <= 0)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10);
                foreach (var hit in colliders)
                {
                    if (hit.GetComponent<Enemy>() != null)
                    {
                        enemyTarget.Add(hit.transform);
                    }
                }
            }
        }

        StuckInto(collision);
    }

    // 和某个物体碰撞的时候，就把剑固定在那个物体上
    private void StuckInto(Collider2D collision)
    {
        canRotate = false;
        cd.enabled = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        // 如果可以弹，就不会卡住
        // 如果有可以弹的敌人，就不会卡住 当然可以弹但是没人的时候就会卡住了
        if (isBouncing && enemyTarget.Count > 0)
            return;
        anim.SetBool("Rotation", false);
        transform.parent = collision.transform;
    }

    public void SetupBounce(bool _isBouncing, int _amountOfBounce)
    {
        isBouncing = _isBouncing;
        amoutOfBounce = _amountOfBounce;
    }
}
