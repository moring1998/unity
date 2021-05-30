using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Transform playerTransform;
    public Vector3 offset = new Vector3(0, 1, 0);//初始化偏移值
    // Start is called before the first frame update
    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;//获取hero对象的transform
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTransform.position + offset;//获取血条位置+偏移量
    }
}
