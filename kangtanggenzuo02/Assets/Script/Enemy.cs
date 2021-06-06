using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 5.0f;//敌人的速度；
    public Sprite damagedEnemy;
    public Sprite deadEnemy;
    public int HP = 2;
    public float MinSpinForce = -100f;        
    public float MaxSpinForce = 100f;
    public GameObject UI_100Points;

    private Rigidbody2D enemyBody;//获得敌人的rigidbody
    private Transform frontCheck;
    private bool isDead = false;
    private SpriteRenderer curBody;//存放当前应该渲染的身体；
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Awake()
    {
        enemyBody = GetComponent<Rigidbody2D>();//找到enemy的rigidbody；
        frontCheck = transform.Find("frontCheck");//找到enemy下面的frontcheck；
        curBody = transform.Find("body").GetComponent<SpriteRenderer>();//原来的身体
    }
    private void FixedUpdate()
    {
        enemyBody.velocity = new Vector2(transform.localScale.x * moveSpeed, enemyBody.velocity.y);
        Collider2D[] colliders = Physics2D.OverlapPointAll(frontCheck.position);
        foreach (Collider2D c in colliders)
        {
            // If any of the colliders is an Obstacle...
            if (c.tag == "wall")
            {
                flip();
                break;
            }
        }

        if (HP == 1 && damagedEnemy != null)
            curBody.sprite = damagedEnemy;
        if(HP <= 0 && !isDead)
        {
            death();
            
        }
    }
        public void Hurt()
        {
             HP--;
        }
    void death()
    {
        isDead = true;
        curBody.sprite = deadEnemy;
        Collider2D[] cols = GetComponents<Collider2D>();
        foreach (Collider2D c in cols)
            c.isTrigger = true;
        //给一个随机的旋转的扭矩
        enemyBody.AddTorque(Random.Range(MinSpinForce, MaxSpinForce));

        Vector3 UI100Pos = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
        Instantiate(UI_100Points, UI100Pos, Quaternion.identity);//在敌人的位置上显示UI100points；
    }

    void flip()
    {
        Vector3 enemyScale = transform.localScale;//获取敌人的scale
        enemyScale.x *= -1;//转身
        transform.localScale = enemyScale;
    }
}
