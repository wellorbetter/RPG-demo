using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Skill : Skill
{
    [Header("Skill Info")]
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private Vector2 launchDir;
    [SerializeField] private float swordGravity;

    public void CreateSword()
    {
        // 生成剑 记得好好查一查这个Instantiate
        GameObject newSword = Instantiate(swordPrefab, player.transform.position, transform.rotation);
        Sword_Skill_Controller swordController = newSword.GetComponent<Sword_Skill_Controller>();
        swordController.SetupSword(launchDir, swordGravity);
    }
}
