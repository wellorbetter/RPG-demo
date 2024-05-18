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
    // �Ƿ�������ص�
    private bool isBouncing;
    // �м��ε��Ļ���
    private int amoutOfBounce;
    // ��һ�λ���֮����Χ�ĵ���
    private List<Transform> enemyTarget;
    private int targetIndex;

    // ���ﲻ����start,rb��start�����ȡ����������֮������������п��о��о�
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
        // ��������ʱ���ý���ת
        anim.SetBool("Rotation", true);
        enemyTarget = new List<Transform>();
    }

    // ���ó�isKinematic��ֻ���ܵ�transform��Ӱ�죬�����ܵ����������Ӱ��
    // ֱ���ý�ͨ��transform�ķ�ʽ�ص��������
    public void ReturnSword()
    {
        // RigidbodyConstraints2D.FreezeAll 
        // ����о�Ч��һ��
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        //rb.isKinematic = true; // ����ֻҪ���������ˣ��Ͳ�Ҫ��gravityӰ����
        // transform.parent����Ϊnull���ͻ����븸���󣬳�Ϊ�����Ķ���
        transform.parent = null;
        // ���ڷ���
        isReturning = true;
    }

    // ǿ�ƶ�����ҷ��������ٶȷ�����롣����ζ�����۶������ĸ������ƶ����������Զ��泯�÷���
    private void FixedUpdate()
    {
        if (canRotate)
        {
            transform.right = rb.velocity;
        }
        // ���ڻ��ս�
        if (isReturning)
        {
            // MoveTowards ��ʹ������Ŀ���ƶ���ֱ��������Ŀ��λ��
            // ��������ǰλ�ã�Ŀ��λ�ã��ƶ��ٶ�
            // Time.deltaTime ��ÿ֡��ʱ����
            // ������ˣ���Ϊ��UpdateĪ������Ŀ�һ���ģ�����Ӧ����û��ʲô�����
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, returnSpeed * Time.fixedDeltaTime);
            // ͨ��transform���ս����������ȽϽ��ˣ��ͻ��ս�
            if (Vector2.Distance(transform.position, player.transform.position) < 1)
            {
                player.CatchTheSword();
            }
        }

        BounceLogic();
    }

    private void BounceLogic()
    {
        // ������Ե� �����е��ˣ�����ǲ����п��Ե��ĵ��˻��ڵ�һ�λ��е��˵�ʱ���������
        if (isBouncing && enemyTarget.Count > 0)
        {
            // ����ڵ��Ĺ����У������˻��ս��ļ����ͻ��ս�
            if (isReturning)
            {
                ReturnSword();
                return;
            }
            // ʹ��������Ե��ĵ���
            transform.position = Vector2.MoveTowards(transform.position, enemyTarget[targetIndex].position, bounceSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, enemyTarget[targetIndex].position) < .1f)
            {
                // ����һ��
                targetIndex++;
                amoutOfBounce--;
                // û�����˾ͻ���
                if (amoutOfBounce == 0)
                {
                    isBouncing = false;
                    isReturning = true;
                }
                // ��������ˣ�����ȥ�ӵ�һ����ʼ��
                if (targetIndex >= enemyTarget.Count)
                {
                    targetIndex = 0;
                }
            }
        }
    }

    // ����ס�˾Ͳ���ת��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ������ڻ��յ�ʱ�򣬾���ʱ�Ȳ��������
        if (isReturning)
            return;
        // ͬʱ��ֻҪû�п��Ե��ĵ��ˣ��ǾͲ��ᵯ��ֻ�Ῠס

        // collision ����ײ���Ķ��� �������Enemy�������˵���ǵ���
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

    // ��ĳ��������ײ��ʱ�򣬾Ͱѽ��̶����Ǹ�������
    private void StuckInto(Collider2D collision)
    {
        canRotate = false;
        cd.enabled = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        // ������Ե����Ͳ��Ῠס
        // ����п��Ե��ĵ��ˣ��Ͳ��Ῠס ��Ȼ���Ե�����û�˵�ʱ��ͻῨס��
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
