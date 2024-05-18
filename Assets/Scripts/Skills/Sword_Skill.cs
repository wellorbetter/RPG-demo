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
            // AimDirection().normalized ��һ�� �����ķ��򲻱䣬���ȱ�Ϊ1
            finalDir = new Vector2(AimDirection().normalized.x * launchForce.x, AimDirection().normalized.y * launchForce.y);
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            // ���� AimDots ��λ��
            for (int i = 0; i < numberOfDots; i++)
            {
                // DotsPosition����������������ϵ�λ�ã���Ϊ����Ҫ���ߵ�λ��������Ҫ��һ�ξ���������һ����
                // �������ÿ��spaceBetweenDots(X��)ȡһ����
                dots[i].transform.position = DotsPosition(i * spaceBetweenDots);
            }
        }
    }
    public void CreateSword()
    {
        // ���ɽ� �ǵúúò�һ�����Instantiate
        GameObject newSword = Instantiate(swordPrefab, player.transform.position, transform.rotation);
        Sword_Skill_Controller swordController = newSword.GetComponent<Sword_Skill_Controller>();
        swordController.SetupSword(finalDir, swordGravity);
        DotsActive(false);
    }

    // ���������ָ���λ����׼�������
    public Vector2 AimDirection()
    {   // Camera.main ���������ScreenToWorldPoint ��������Ļ����ת��Ϊ�������ꡣ��Ļ�����ǻ�����Ļ�ֱ��ʵ�����λ�ã����������ǻ�����Ϸ����ĵ�λλ�á�
        // Input.mousePosition �������ָ������Ļ����ϵ�е�λ�ã����½��� (0, 0)�����Ͻ��� (Screen.width, Screen.height)
        // mousePosition - playerPosition ��������������������λ��ָ�����λ�õ�������
        Vector2 playerPosition = player.transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - playerPosition;
        return direction;
    }

    // ���ÿ��غ���
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
            // ʵ���� AimDots 
            // Instantiate ���������ڳ���������һ���µĶ���
            // ��һ��������Ҫ���ɵĶ��󣬵ڶ������������ɶ����λ�ã����������������ɶ������ת�Ƕȣ����ĸ����������ɶ���ĸ�����
            // Quaternion.identity ��ʾû����ת�������ɶ������ת�Ƕ�Ϊ 0��
            dots[i] = Instantiate(dotPrefab, player.transform.position, Quaternion.identity, dostsParent);
            dots[i].SetActive(false);
        }
    }

    private Vector2 DotsPosition(float t)
    {
        // λ�� = ���λ�� + ������� * ����� * t + 0.5 * ���� * t * t Ҳ�����������ߵĹ�ʽ vt + 1 / 2 gt^2
        Vector2 position = (Vector2) player.transform.position + new Vector2(
            AimDirection().normalized.x * launchForce.x, 
            AimDirection().normalized.y * launchForce.y) * t + 0.5f * (Physics2D.gravity * swordGravity) * t * t;
        return position;
    }
}
