using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone_Skill : Skill
{
    [Header("Clone Info")]
    // ����Prefab�����Prefab�����кܶණ�������綯����λ��֮���
    [SerializeField] private GameObject clonePrefab;
    // ��������ĳ���ʱ�䣬���˾ͻῪʼ��ʧ
    [SerializeField] private float cloneDuration;

    public void CreateClone(Transform _clonePosition)
    {
        // Instantiate ����ݴ����Prefab����һ���µ�GameObject
        // ���滹�в�����������λ����Ϣ��ȫ������;ֲ����꣬û��ϸ�����õ����ٿ�
        // ���ˣ��ڶ��������Ǹ������λ�ã��������������Ƿ񱣳���������
        // ������治����Transform
        GameObject newClone = Instantiate(clonePrefab);
        newClone.GetComponent<Clone_Skill_Controller>().SetupClone(_clonePosition, cloneDuration);
        // �о�û��Ҫ��Controller��ֱ������������λ�þ�����
        /*newClone.transform.position = _clonePosition.position;*/
        // �����б�Ҫ��ÿ�ζ���newһ���µ�clone����ô��Щclone��ô����
        // �����Ҫ�������ǵĻ����͵���һ��Controller������
        // ��Ȼ��������Ѷ�ÿ��clone���о�ϸ�Ĳ���
    }
    protected override void Update()
    {
        base.Update();
        if (skillTimer < 0)
        {
            skillTimer = cooldown;
        }
    }
    public override void UseSkill()
    {
        base.UseSkill();
        
    }
}
