using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontrol : MonoBehaviour
{
    private Rigidbody2D heroBody;
    public float moveForce = 100;
    public float jumpForce = 100;
    private float fInput = 0.0f;
    public float maxSpeed = 5;
  //HideInInspector] [SerializeField]
    public bool bFaceRight = true;
    private bool bGrounded = false;
    Transform mGroundCheck;


    // Start is called before the first frame update
    void Start()
    {
        heroBody = GetComponent<Rigidbody2D>();//得到rigibody2d组件，组件与playercontrol脚本平行
        mGroundCheck = transform.Find("GroundCheck");
    }
        // Update is called once per frame
        void Update()
        {
            fInput = Input.GetAxis("Horizontal");//得到水平方向轴

            if (fInput > 0 && !bFaceRight)

                flip();

            else if (fInput < 0 && bFaceRight)

                flip();
            bGrounded = Physics2D.Linecast(transform.position, mGroundCheck.position, 1 << LayerMask.NameToLayer("Ground"));//射线检测

        }
        
        private void FixedUpdate()
        {
            if (Mathf.Abs(heroBody.velocity.x) < maxSpeed)//取herobody x轴上的绝对值判断是否小于最大速度

                heroBody.AddForce(fInput * moveForce * Vector2.right);
            if (Mathf.Abs(heroBody.velocity.x) > maxSpeed)
                heroBody.velocity = new Vector2(Mathf.Sign(heroBody.velocity.x) * maxSpeed, heroBody.velocity.y);
            bool bJump = false;
            if(bGrounded)
            {
                bJump = Input.GetKeyDown(KeyCode.Space);
                if(bJump)
                heroBody.AddForce(new Vector2(0f, jumpForce));
                bGrounded = false;
                
            }
        }
        void flip()
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            bFaceRight = !bFaceRight;
        }


    }

