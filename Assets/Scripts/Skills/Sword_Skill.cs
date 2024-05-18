using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Skill : Skill
{
    [Header("Skill Info")]
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private Vector2 launchForce;
    [SerializeField] private float swordGravity;

    private Vector2 finalDir;

    [Header("Aim dots")]
    [SerializeField] private int numberOfDots;
    [SerializeField] private float spaceBetweenDots;
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private Transform dostsParent;

    private GameObject[] dots;

    protected override void Start()
    {
        base.Start();
        GenerateDots();
    }
    protected override void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            // AimDirection().normalized 归一化 向量的方向不变，长度变为1
            finalDir = new Vector2(AimDirection().normalized.x * launchForce.x, AimDirection().normalized.y * launchForce.y);
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            // 计算 AimDots 的位置
            for (int i = 0; i < numberOfDots; i++)
            {
                // DotsPosition可以算出来抛物线上的位置，因为是需要虚线点位，所以需要隔一段距离来生成一个点
                // 这里就是每隔spaceBetweenDots(X轴)取一个点
                dots[i].transform.position = DotsPosition(i * spaceBetweenDots);
            }
        }
    }
    public void CreateSword()
    {
        // 生成剑 记得好好查一查这个Instantiate
        GameObject newSword = Instantiate(swordPrefab, player.transform.position, transform.rotation);
        Sword_Skill_Controller swordController = newSword.GetComponent<Sword_Skill_Controller>();
        swordController.SetupSword(finalDir, swordGravity);
        DotsActive(false);
    }

    // 够根据鼠标指针的位置瞄准射击方向
    public Vector2 AimDirection()
    {   // Camera.main 是主相机，ScreenToWorldPoint 方法将屏幕坐标转换为世界坐标。屏幕坐标是基于屏幕分辨率的像素位置，世界坐标是基于游戏世界的单位位置。
        // Input.mousePosition 返回鼠标指针在屏幕坐标系中的位置，左下角是 (0, 0)，右上角是 (Screen.width, Screen.height)
        // mousePosition - playerPosition 是向量减法，计算从玩家位置指向鼠标位置的向量。
        Vector2 playerPosition = player.transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - playerPosition;
        return direction;
    }

    // 设置开关函数
    public void DotsActive(bool _isActive)
    {
        for (int i = 0; i < dots.Length; i ++ )
        {
            dots[i].SetActive(_isActive);
        }
    }

    private void GenerateDots()
    {
        dots = new GameObject[numberOfDots];
        for (int i = 0; i < numberOfDots; i++)
        {
            // 实例化 AimDots 
            // Instantiate 方法用于在场景中生成一个新的对象。
            // 第一个参数是要生成的对象，第二个参数是生成对象的位置，第三个参数是生成对象的旋转角度，第四个参数是生成对象的父对象。
            // Quaternion.identity 表示没有旋转，即生成对象的旋转角度为 0。
            dots[i] = Instantiate(dotPrefab, player.transform.position, Quaternion.identity, dostsParent);
            dots[i].SetActive(false);
        }
    }

    private Vector2 DotsPosition(float t)
    {
        // 位置 = 玩家位置 + 射击方向 * 射击力 * t + 0.5 * 重力 * t * t 也就是算抛物线的公式 vt + 1 / 2 gt^2
        Vector2 position = (Vector2) player.transform.position + new Vector2(
            AimDirection().normalized.x * launchForce.x, 
            AimDirection().normalized.y * launchForce.y) * t + 0.5f * (Physics2D.gravity * swordGravity) * t * t;
        return position;
    }
}
