using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    protected float skillTimer; // ���ּ��ܵļ�ʱ��
    [SerializeField] protected float cooldown;  // ���ܵ���ȴʱ��
    protected Player player;
    protected virtual void Start()
    {
        player = PlayerManager.instance.player;
    }
    protected virtual void Update()
    {
        skillTimer -= Time.deltaTime;
    }

    public virtual bool CanUseSkill()
    {
        // �����ж�һֱ�����ж�Timer�ģ�������cooldown
        // ֮ǰ����д����cooldown < 0�����������ٷ�
        return true;
    }

    public virtual void UseSkill()
    {
        // ʹ�ü��� ������߼�����
    }
}
