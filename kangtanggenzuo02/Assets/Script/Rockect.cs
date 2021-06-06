using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rockect : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject explosion;
    void Start()
    {
        Destroy(gameObject,2);//2秒之内没有碰撞到物体，自动销毁
        //explosion = Resources.Load("explosion") as GameObject;//加载文件，类型强制转换
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        float rotationZ = Random.Range(0,360);//取随机数
        if(collision.tag != "Player")//碰到自己不爆炸
        {
            Instantiate(explosion, transform.position, Quaternion.Euler(new Vector3(0, 0, rotationZ)));//实列化（对象，位置，旋转）
            Destroy(gameObject);//销毁炮弹
            //Debug.Log(collision.name);
        }
      if(collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().Hurt();
        }
    }
    void Update()
    {
        
    }
}
