using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{

    public Transform[] backGrounds;//声明一个transforme类型的数组
    public float fParallax = 0.4f;//声明一个float遍历，赋值
    public float layerFraction = 5f;//声明一个float遍历，赋值
    public float fSmooth = 5f;
    Transform cam;
    Vector3 previousCamPos;//声明变量，用于存上一帧的位置
                                    // Start is called before the first frame update
    private void Awake()
    {
        cam = Camera.main.transform;//获取摄像机位置
        previousCamPos = cam.position;//存取上一帧的位置
    }

    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        float fParallaxX = (previousCamPos.x - cam.position.x) * fParallax;//摄像机整体运动量
      for(int i=0; i<backGrounds.Length;i++)
        {
            float fNewX = backGrounds[i].position.x + fParallaxX * (1 + i * layerFraction);//新的位置
            Vector3 newPos = new Vector3(fNewX, backGrounds[i].position.x, backGrounds[i].position.z);//新的摄像机位置
            backGrounds[i].position = Vector3.Lerp(backGrounds[i].position,newPos,fSmooth * Time.deltaTime);//使用Lerp平滑从原始位置过渡到新的位置
        }
        previousCamPos = cam.position;
    }
}
