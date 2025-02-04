﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;//施加力需要得到刚体
    
    

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();//获取组件
        int number = Random.Range(0,2);// generate 0 or 1
        //调用小球移动方法
        GoBall();
    }

    void Update() {
        //保证x横轴的速度不小于9
        Vector2 velocity = rigidbody2D.velocity;//得到当前速度
        if(velocity.x<9 && velocity.x>-9 && velocity.x!=0){
            if(velocity.x>0){
                velocity.x=10;
            }else{
                velocity.x=-10;
            }
            rigidbody2D.velocity = velocity;//速度赋值回去
        }
    }

    //碰撞方法
    void OnCollisionEnter2D(Collision2D col) {
        //通过tag判断碰撞到的是不是player
        if(col.gameObject.tag == "Player"){
            Vector2 velocity = rigidbody2D.velocity;//当前速度
            //访问到player的刚体速度，直接得到y方向上的速度
            //为了防止越撞越快，要加上碰撞速度/2
            velocity.y = velocity.y +col.collider.GetComponent<Rigidbody2D>().velocity.y/2;
            /*velocity.y = velocity.y/2f + col.rigidbody2D.velocity.y/2;
            是视频的col不能直接访问到rigibody2D，过时的API
            */
            rigidbody2D.velocity = velocity;//赋值给小球新的速度
        }
        //Debug.Log(rigidbody2D.velocity); 检测当前速度

        //判断小球有没有碰墙
        if(col.gameObject.name=="rightWall" || col.gameObject.name=="leftWall"){
            //调用gameManager里的修改分数方法
            GameManager.Instance.ChangeScore(col.gameObject.name);//把名字传回去
        }
    }

    //重置小球位置方法
    public void Reset(){
        transform.position = Vector3.zero;
        GoBall();
    }

    //小球位置方法
    void GoBall(){
        int number = Random.Range(0,2);// generate 0 or 1
        if(number==1){
            rigidbody2D.AddForce(new Vector2(100,0));//往x方向施加100的力，也就是右边
        }else{
            rigidbody2D.AddForce(new Vector2(-100,0));//左
        }
    }


}
