using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Skill_Controller : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private Collider2D cd;
    private Player player;

    // 这里不能用start,rb在start里面获取不到，更新之后就是这样，有空研究研究
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<Collider2D>();
        Debug.Log(anim);
        Debug.Log(rb);
        Debug.Log(cd);

    }

    public void SetupSword(Vector2 _dir, float _gravityScale)
    {
        rb.velocity = _dir;
        rb.gravityScale = _gravityScale;
    }
}
