using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTran;//主角的transform                 //定义一个变量存放hero的位置 ，存位置时不存position，因为positon是一个属性，
                                //一个属性的话是函数调用，函数的返回值是一个临时变量，赋给另外一个值时是值传递，值传递时只能表示开始的值
                                //当再运行时，值不会更新，所以不能用vector，只能用transform（类）
    public float maxDistanceX = 2;  //当主角和摄像机的x方向的位置超过2的时候就移动
    public float maxDistanceY = 2;  //当主角和摄像机的y方向的位置超过2的时候就移动
    public float xSpeed = 4;
    public float ySpeed = 4;
    public Vector2 maxXandY;
    public Vector2 minXandY = new Vector2(-8, 8);

    // Start is called before the first frame update
    private bool NeedMoceX()
    {
        bool bMove = false;
        if (Mathf.Abs(transform.position.x - playerTran.position.x) > maxDistanceX)
            bMove = true;
        else
            bMove = false;
        return bMove;
    }
    private bool NeedMoceY()
    {
        bool bMove = false;
        if (Mathf.Abs(transform.position.y - playerTran.position.y) > maxDistanceY)
            bMove = true;
        else
            bMove = false;
        return bMove;
    }
    void Start()  //找到hero的位置，根据hero和摄像机的差距来移动
    {

    }
    private void Awake()  //初始化工作，在程序运行之前调用
    {

        // playerTran = GameObject.Find("Hero").transform;  //这是直接找到hero对象的transform属性
        playerTran = GameObject.FindGameObjectWithTag("Player").transform;  //gameobject

    }
    // Update is called once per frame
    void Update()
    {

    }
    private void TrackPlayer()
    {
        float newX = transform.position.x;//存放当前摄像机的位置
        if (NeedMoceX())  //先判断是否需要移动
            newX = Mathf.Lerp(transform.position.x, playerTran.position.x, xSpeed * Time.deltaTime);
        newX = Mathf.Clamp(newX, minXandY.x, maxXandY.x);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        float newY = transform.position.y;//存放当前摄像机的位置
        if (NeedMoceY())  //先判断是否需要移动
            newY = Mathf.Lerp(transform.position.y, playerTran.position.y, ySpeed * Time.deltaTime);
        newY = Mathf.Clamp(newY, minXandY.y, maxXandY.y);
        transform.position = new Vector3(newY, transform.position.x, transform.position.z);
    }
    private void FixedUpdate()
    {
        TrackPlayer();
    }
}