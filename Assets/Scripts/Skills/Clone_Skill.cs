using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone_Skill : Skill
{
    [SerializeField] private GameObject clonePrefab;
    private float cloneTimer;

    public void CreateClone(Transform _clonePosition)
    {
        // Instantiate ����ݴ����Prefab����һ���µ�GameObject
        // ���滹�в�����������λ����Ϣ��ȫ������;ֲ����꣬û��ϸ�����õ����ٿ�
        // ���ˣ��ڶ��������Ǹ������λ�ã��������������Ƿ񱣳���������
        // ������治����Transform
        GameObject newClone = Instantiate(clonePrefab);
        /*newClone.GetComponent<Clone_Skill_Controller>().SetupClone(_clonePosition);*/
        // �о�û��Ҫ��Controller��ֱ������������λ�þ�����
        newClone.transform.position = _clonePosition.position;
    }
}
