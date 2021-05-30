using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;//主角的Transform
    public float maxDistanceX = 2;//X轴最大移动距离
    public float maxDistanceY = 2;//Y轴最大移动距离
    public float xSpeed = 4;//X轴最大速度
    public float ySpeed = 4;//Y轴最大速度
    public Vector2 maxXandY;
    public Vector2 minXandY = new Vector2(-8,8);
    // Start is called before the first frame update


private void Awake()
    {
       playerTransform = GameObject.Find("Hero").transform;//获取hero的transform属性
       //playerTransform = GameObject.FindGameObjectsWithTag("Player").transform;//用标签获取对象组件

    }
    private bool NeedMoveX() 
    {
        bool bMove = false;
        if (Mathf.Abs(transform.position.x - playerTransform.position.x) > maxDistanceX)//hero位置-摄像机位置>最大距离
            bMove = true;//移动
        else
            bMove = false;//不移动
        return bMove;//返回值
}
    private bool NeedMoveY()
    {
        bool bMove = false;
        if (Mathf.Abs(transform.position.y - playerTransform.position.y) > maxDistanceY)//hero位置-摄像机位置>最大距离
            bMove = true;//移动
        else
            bMove = false;//不移动
        return bMove;//返回值
    }
void Start()
    {
        
    }
 

    // Update is called once per frame
    void Update()
    {
        
    }
private void TrackPlayer()
{
    float newX = transform.position.x;//当前摄像机X轴方向的位置
    float newY = transform.position.y;//当前摄像机y轴方向的位置
        if (NeedMoveX())
            newX = Mathf.Lerp(transform.position.x, playerTransform.position.x, xSpeed * Time.deltaTime);//顺滑的移动（插值：Lerp）:原始位置，目的地位置，插值
        if (NeedMoveY())
            newY = Mathf.Lerp(transform.position.y, playerTransform.position.y, ySpeed * Time.deltaTime);
        newY = Mathf.Clamp(newY, minXandY.y, maxXandY.y);
        transform.position = new Vector3(newX, newY, transform.position.z);

    }
    


private void FixedUpdate()
{
    TrackPlayer();
}
}
