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
    }

    // ���ó�isKinematic��ֻ���ܵ�transform��Ӱ�죬�����ܵ����������Ӱ��
    // ֱ���ý�ͨ��transform�ķ�ʽ�ص��������
    public void ReturnSword()
    {
        rb.isKinematic = false;
        // transform.parent����Ϊnull���ͻ����븸���󣬳�Ϊ�����Ķ���
        transform.parent = null;
        // ���ڷ���
        isReturning = true;
    }

    // ǿ�ƶ�����ҷ��������ٶȷ�����롣����ζ�����۶������ĸ������ƶ����������Զ��泯�÷���
    private void Update()
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
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, returnSpeed * Time.deltaTime);
            // ͨ��transform���ս����������ȽϽ��ˣ��ͻ��ս�
            if (Vector2.Distance(transform.position, player.transform.position) < 1)
            {
                player.ClearTheSword();
            }
        }
    }

    // ����ס�˾Ͳ���ת��
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
