using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    // 这种组件一般情况下是不会在外部直接赋值的
    public Dash_Skill dash { get; private set; }
    public Clone_Skill clone { get; private set;}
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    private void Start()
    {
        // 一般在Awake里面创建某些对象和需要的资源
        // Start里面通过GetComponent来获取组件
        dash = GetComponent<Dash_Skill>();
        clone = GetComponent<Clone_Skill>();
    }
}
