using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    // �������һ��������ǲ������ⲿֱ�Ӹ�ֵ��
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
        // һ����Awake���洴��ĳЩ�������Ҫ����Դ
        // Start����ͨ��GetComponent����ȡ���
        dash = GetComponent<Dash_Skill>();
        clone = GetComponent<Clone_Skill>();
    }
}
