using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{

    public Animator anim;
    public Rigidbody2D rb;

    public float moveSpeed=1f;

    public Vector2 movement=Vector2.zero;

    public void Move(){
        rb.MovePosition(rb.position+movement*moveSpeed*Time.deltaTime);

        if(movement*moveSpeed!=Vector2.zero){
            anim.SetBool("IsWalking", true);
        }else{
            anim.SetBool("IsWalking", false);
        }
        if(movement.x>0)
        {
            this.transform.localScale=new Vector3(-1,1,1);
        }if(movement.x<0)
        {
            this.transform.localScale=new Vector3(1,1,1);
        }

    }

}
