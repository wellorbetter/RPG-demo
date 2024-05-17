using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private GameObject cam;

    [SerializeField] private float parallaxEffect;
    private float xPositon;
    void Start()
    {
        cam = GameObject.Find("Main Camera");
        xPositon = cam.transform.position.x;
    }
    void Update()
    {
        // distanceToMove������ƶ��ľ�������Ӳ�Ч������
        float distanceToMove = cam.transform.position.x * parallaxEffect;
        // ����Ч�����Ǳ������ƶ��ٶȱ��������ʵ�����Ӳ�Ч��
        // �����transform��ָ������transform
        transform.position = new Vector3(xPositon + distanceToMove, transform.position.y);
    }
}
