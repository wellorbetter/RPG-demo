using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Clone_Skill_Controller : MonoBehaviour
{
    private SpriteRenderer sr;
    // 失去透明度的速度
    [SerializeField] private float colorLosingSpeed;
    // 在这个时间之后，clone就会逐渐消失
    // 也就是duration，持续时间
    private float cloneTimer;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
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
    public void SetupClone(Transform _clonePosition, float _cloneDuration)
    {
        transform.position = _clonePosition.position;
        cloneTimer = _cloneDuration;
    }
}