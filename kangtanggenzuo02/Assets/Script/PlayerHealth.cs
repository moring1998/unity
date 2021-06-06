using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float hurtBloodPont = 20f;
    public float damageRepeat = 0.5f;//受伤间隔时间
    public AudioClip[] ouchClips;
    public float hurtForce = 100f;//反弹力
    private float lastHurt;
    private Animator anim;
    float health = 100f;
    SpriteRenderer healthbar;
    Vector3 healthBarScale;
    //public Transform body;
    // Start is called before the first frame update
    void Start()
    {
        healthbar = GameObject.Find("HealthBar").GetComponent<SpriteRenderer>();//获取heatthBar下面的组件中的这个方法
        healthBarScale = healthbar.transform.localScale;//找到本地的scale
        anim = GetComponent<Animator>();
        

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy") //是否碰撞到敌人
        {
            if(Time.time>lastHurt + damageRepeat)//判断时间是否大于再一次受伤害的时间
            {


            if (health > 0)//判断血量
            {
                TakeDamage(collision.gameObject.transform);//减血
                //更新血条状态
                lastHurt = Time.time;//当前游戏时间
            }
            else
            if(health<=0)//血量小于等于0的判断
            {
                //Debug.Log("die");
                anim.SetTrigger("Die");//播放死亡动画
                                       //body.GetComponent<CapsuleCollider2D>().enabled = false;
                                       //掉落河中
                    Collider2D[] colliders = GetComponents<Collider2D>();//获取hero身上的所有碰撞体
                    foreach (Collider2D c in colliders)//遍历所有的碰撞体
                        c.isTrigger = true;//将碰撞体里的Trigger设置成true
                    //死亡后掉落的时候hero在背景图层前面
                    SpriteRenderer[] sp = GetComponentsInChildren<SpriteRenderer>();//获取
                    foreach (SpriteRenderer s in sp)//遍历
                        s.sortingLayerName = "UI";//
                    GetComponent<PlayerContrl>().enabled = false; //控制脚本失效
                    GetComponentInChildren<Gun>().enabled = false;//Gun脚本失效
            }
            }
        }
    }
        void TakeDamage(Transform enemy)
        {
            health -= hurtBloodPont;//血量减少
            UpdateHealthBar();//血条更新
            Vector3 hurtVector = transform.position - enemy.position + Vector3.up;//碰到敌人，反弹(垂直方向的力)
            //Vector2 hurtV2 = new Vector2(hurtVector.x, hurtVector.y);//
            GetComponent<Rigidbody2D>().AddForce(hurtForce * hurtVector);
            int i = Random.Range(0, ouchClips.Length);//获取随机数
            AudioSource.PlayClipAtPoint(ouchClips[i], transform.position);//播放声音（播放声音，播放位置）
        }
        void UpdateHealthBar()
        {
            healthbar.material.color = Color.Lerp(Color.green, Color.red, 1 - health * 0.01f);//血量颜色的变化，Lerp函数（green,red,插值）
            healthbar.transform.localScale = new Vector3(health*0.01f, 1, 1);//血条长度变化
        }
        // Update is called once per frame
        void Update()
        {

        }
    }
