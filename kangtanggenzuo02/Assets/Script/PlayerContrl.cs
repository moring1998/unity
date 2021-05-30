using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContrl : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D heroBody;//声明了一个私有对象
    public float moveForce = 100;//声明一个公有变量force 
    private float  fInput = 0.0f;
    public float maxSpeed = 5;
    [HideInInspector]
    public bool bFaceRight = true;
    Transform mGroundCheck;
    //[SerializeField]//私有显示
    private bool bGrounded = false;
    public float jumpForce = 500;//声明一个跳跃的力
    private Animator anim;


    void Start()
    {
        heroBody = GameObject.Find("Hero").GetComponent<Rigidbody2D>();
        // heroBody = GetComponent<Rigidbody2D>();//获取unity中的刚体
        mGroundCheck = transform.Find("GroundCheck");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()//每一帧都调用
    {
        fInput = Input.GetAxis("Horizontal");//声明一个浮点型变量，获取水平方向轴，实现刚体左右运动GetAxis(string anisName（轴线名）)
        //if (Input.GetAxis("Horifzontal")) ;
        //heroBody.AddForce(Vector2.right*fInput * moveForce);//给刚体的力，左右移动 （-1--1）
        //Debug.Log("sdd");
        if (fInput < 0 && bFaceRight)
            flip();
        else if (fInput > 0 && !bFaceRight)
            flip();
       bGrounded = Physics2D.Linecast(transform.position, mGroundCheck.position,1<<LayerMask.NameToLayer("Ground"));//射线检测
        //if(heroBody.velocity.x>0.1)
       // {
            anim.SetFloat("Speed", Mathf.Abs(fInput));//设置动画控制器中的speed
        //}
    }
    private void FixedUpdate()//固定帧刷新
    {
        if (Mathf.Abs(heroBody.velocity.x) < maxSpeed)//如果heroBody向X轴的速度取绝对值后<最大速度
            heroBody.AddForce(fInput * moveForce * Vector2.right);//hero向前的速度为：（（-1，1）*100*向量）
        if (Mathf.Abs(heroBody.velocity.x) > maxSpeed)//如果heroBody向X轴的速度取绝对值后>最大速度
            heroBody.velocity = new Vector2(Mathf.Sign(heroBody.velocity.x) * maxSpeed,//
               heroBody.velocity.y);
        bool bJump = false;
        if (bGrounded)
        {
            bJump = Input.GetKeyDown(KeyCode.Space);//是否按下空格
            Vector2 upForce = new Vector2(0, 1);
            if(bJump)
            {
                heroBody.AddForce(upForce * jumpForce);//给heroBody一个向上的力，（向上的力*跳跃）
                anim.SetTrigger("Jump");
            }
                
            bGrounded = false;
            
           // bJump = false;
        }
    }
    void flip()
    {
        Vector3 theScale = transform.localScale;//声明一个theScale三维变量，赋值=变换的局部
        theScale.x *= -1;//变换符号
        transform.localScale = theScale;//重新赋值，进行方向的转换
        bFaceRight = !bFaceRight;
    }
}
