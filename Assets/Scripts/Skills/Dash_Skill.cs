using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Alexdevϲ���Ѽ��ܵ��߼�����Skill����
// �����Ͳ��ý���ǿ������ת�������Ǿͻ���ң��Ҹо����ǵ÷ֿ�
public class Dash_Skill : Skill
{
    [SerializeField] protected float cooldown;
    protected float cooldownTimer;
    // ��̼��ܼ��ɵ�����֮��Plyaer�����dashTImer�Ͳ����� cooldownҲ��
    // ������ô˵�أ�����ʵ�о�������ܾ�����߼�����ʵ����Ӧ�÷���Skill����
    // ���ǵ��Ժ�ѧ�����ģʽ�����Ż���
    public override void UseSkill()
    {
        base.UseSkill();
    }
    public override bool CanUseSkill()
    {
        if (cooldownTimer < 0)
        {
            UseSkill();
            cooldownTimer = cooldown;
            return true;
        }
        return false;
    }
    override protected void Update()
    {
        base.Update();
        cooldownTimer -= Time.deltaTime;
    }
}
