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
        // distanceToMove是相机移动的距离乘以视差效果参数
        float distanceToMove = cam.transform.position.x * parallaxEffect;
        // 最后的效果就是背景的移动速度比相机慢，实现了视差效果
        // 这里的transform是指背景的transform
        transform.position = new Vector3(xPositon + distanceToMove, transform.position.y);
    }
}
