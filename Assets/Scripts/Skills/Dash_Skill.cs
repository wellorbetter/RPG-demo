using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Alexdevϲ���Ѽ��ܵ��߼�����Skill����
// �����Ͳ��ý���ǿ������ת�������Ǿͻ���ң��Ҹо����ǵ÷ֿ�
public class Dash_Skill : Skill
{
    // ��̼��ܼ��ɵ�����֮��Plyaer�����dashTImer�Ͳ����� cooldownҲ��
    // ������ô˵�أ�����ʵ�о�������ܾ�����߼�����ʵ����Ӧ�÷���Skill����
    // ���ǵ��Ժ�ѧ�����ģʽ�����Ż���
    // �����Ż�һ�£��о�����������
    private void Awake()
    {
        // ����������������ȴʱ�䣬Ҳ����ֱ����unity�������ã�������ϲ��
    }
    public override void UseSkill()
    {
        base.UseSkill();
    }
    public override bool CanUseSkill()
    {
        if (skillTimer < 0)
        {
            UseSkill();
            skillTimer = cooldown;
            return true;
        }
        return false;
    }
}
