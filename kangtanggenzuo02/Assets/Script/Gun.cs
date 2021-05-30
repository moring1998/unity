using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Rigidbody2D rocket;
    public float fSpeed = 10;
    PlayerContrl playerCtrl;
    private AudioSource ac;
    private Animator anim;
    // Start is called before the first frame update
    private void Awake()
    {
        //anim = gameObject
    }
    void Start()
    {
        playerCtrl = transform.root.GetComponent<PlayerContrl>();
        //rocket = Resources.Load("part_rocket") as Rigidbody2D;//类型加载，强制转换类型
        ac = GetComponent<AudioSource>();
        anim = transform.root.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(0, 0, 0);
        //if(Input.GetKeyDown(KeyCode.Mouse0))//鼠标按键
        if (Input.GetButtonDown("Fire1"))//名称
        {
            ac.Play();//进行Fire就播放声音
            if(playerCtrl.bFaceRight){
                Rigidbody2D RockectInstance = Instantiate(rocket, transform.position, Quaternion.Euler(direction));
                RockectInstance.velocity = new Vector2(fSpeed, 0);
                anim.SetTrigger("Shoot");
            }
            else
            {
                direction.z = 180;//位置旋转
                Rigidbody2D RockectInstance = Instantiate(rocket, transform.position, Quaternion.Euler(direction));
                RockectInstance.velocity = new Vector2(-fSpeed, 0);//反方向发射速度

            }
        }
    }
}
