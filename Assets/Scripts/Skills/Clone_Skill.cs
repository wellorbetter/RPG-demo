using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone_Skill : Skill
{
    [SerializeField] private GameObject clonePrefab;
    private float cloneTimer;

    public void CreateClone(Transform _clonePosition)
    {
        // Instantiate 会根据传入的Prefab生成一个新的GameObject
        // 后面还有参数阔用设置位置信息，全局坐标和局部坐标，没仔细看，用到了再看
        // 懂了，第二个参数是父物体的位置，第三个参数是是否保持世界坐标
        // 这个还真不能用Transform
        GameObject newClone = Instantiate(clonePrefab);
        /*newClone.GetComponent<Clone_Skill_Controller>().SetupClone(_clonePosition);*/
        // 感觉没必要用Controller，直接在这里设置位置就行了
        newClone.transform.position = _clonePosition.position;
    }
}
