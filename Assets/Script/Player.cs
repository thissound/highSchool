using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //live
    public int live = 3;

    //Speed value
    public float moveSpeed = 5f;
    public float jumpSpeed = 10f;

    //GetComponent
    private Animator ani;
    private SpriteRenderer render;
    private Rigidbody2D rigid;

    //Only one Jump
    private int jumpCount = 1;
    private bool isGround =true;

    //Respawn
    public Transform FirstPos;

    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Jump();
        Dead();
    }

    void Dead()
    {
        if(live == 0)
        {
            SceneManager.LoadScene("Over");
        }
    }

    void Move()
    {
        if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            render.flipX = false;
            ani.SetBool("isRun", true);
        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            render.flipX = true;
            ani.SetBool("isRun", true);
        }
        else
        {
            ani.SetBool("isRun", false);
        }
    }

    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(jumpCount == 1)
            {
                rigid.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
                isGround = false;
                jumpCount = 0;
                ani.SetBool("isJump", true);
            }
            
        }
        else
        {
            ani.SetBool("isJump", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ground")
        {
            isGround = true;
            jumpCount = 1;
        }

        if(other.gameObject.tag == "Saw")
        {
            live -= 1;
            gameObject.transform.SetPositionAndRotation(FirstPos.transform.position, Quaternion.identity);
        }

        if(other.gameObject.tag == "Fall")
        {
            live -= 1;
            gameObject.transform.SetPositionAndRotation(FirstPos.transform.position, Quaternion.identity);
        }

        if(other.gameObject.tag == "Clear")
        {
            SceneManager.LoadScene("Clear");
        }
    }
}
