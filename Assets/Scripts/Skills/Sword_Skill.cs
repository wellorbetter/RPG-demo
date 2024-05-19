using System;
using UnityEngine;

public enum SwordType
{
    Regular,
    Bounce,
    Pierce,
    Spin
}
public class Sword_Skill : Skill
{
    public SwordType swordType = SwordType.Regular;

    [Header("Bounce Info")]
    [SerializeField] private int bounceAmount;
    [SerializeField] private float bounceGravity;

    [Header("Pierce Info")]
    [SerializeField] private int pierceAmount;
    [SerializeField] private float pierceGravity;


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
        SetupGravity();
    }

    private void SetupGravity()
    {
        if (swordType == SwordType.Bounce)
        {
            swordGravity = bounceGravity;
        }
        else if (swordType == SwordType.Pierce)
        {
            swordGravity = pierceGravity;
        }
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
    // Ͷ����������ĳһ֡�ᴥ����������Ȼ���һ���ٶȷɳ�ȥ
    public void CreateSword()
    {
        // ���ɽ� �ǵúúò�һ�����Instantiate
        // �ӵ�ǰPlayerλ������һ���µĽ��������transform.rotationû���壬��Ϊ�˵���������������rotation��0 0 0 1
        GameObject newSword = Instantiate(swordPrefab, player.transform.position, transform.rotation);
        Sword_Skill_Controller swordController = newSword.GetComponent<Sword_Skill_Controller>();
        
        if (swordType == SwordType.Bounce)
        {
            swordController.SetupBounce(true, bounceAmount);
        } else if(swordType == SwordType.Pierce)
        {
            swordController.SetupPierce(pierceAmount);
        }
        
        swordController.SetupSword(finalDir, swordGravity);
        player.AssignNewSword(newSword);
        DotsActive(false);
    }
    #region Aim region
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
        for (int i = 0; i < dots.Length; i++)
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
            // �����dotsParent��һ���յ�GameObject�����ڴ�� AimDots����ʱû��������ɶ��
            dots[i] = Instantiate(dotPrefab, player.transform.position, Quaternion.identity, dostsParent);
            dots[i].SetActive(false);
        }
    }

    private Vector2 DotsPosition(float t)
    {
        // λ�� = ���λ�� + ������� * ����� * t + 0.5 * ���� * t * t Ҳ�����������ߵĹ�ʽ vt + 1 / 2 gt^2
        Vector2 position = (Vector2)player.transform.position + new Vector2(
            AimDirection().normalized.x * launchForce.x,
            AimDirection().normalized.y * launchForce.y) * t + 0.5f * (Physics2D.gravity * swordGravity) * t * t;
        return position;
    }
    #endregion
}
