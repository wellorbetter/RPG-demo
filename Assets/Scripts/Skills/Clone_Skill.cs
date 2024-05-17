using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone_Skill : Skill
{
    [Header("Clone Info")]
    // 复的Prefab，这个Prefab里面有很多东西，比如动画，位置之类的
    [SerializeField] private GameObject clonePrefab;
    // 复制物体的持续时间，过了就会开始消失
    [SerializeField] private float cloneDuration;

    public void CreateClone(Transform _clonePosition)
    {
        // Instantiate 会根据传入的Prefab生成一个新的GameObject
        // 后面还有参数阔用设置位置信息，全局坐标和局部坐标，没仔细看，用到了再看
        // 懂了，第二个参数是父物体的位置，第三个参数是是否保持世界坐标
        // 这个还真不能用Transform
        GameObject newClone = Instantiate(clonePrefab);
        newClone.GetComponent<Clone_Skill_Controller>().SetupClone(_clonePosition, cloneDuration);
        // 感觉没必要用Controller，直接在这里设置位置就行了
        /*newClone.transform.position = _clonePosition.position;*/
        // 还真有必要，每次都会new一个新的clone，那么这些clone怎么控制
        // 如果我要操作它们的话，就得有一个Controller来控制
        // 不然这里面很难对每个clone进行精细的操作
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
