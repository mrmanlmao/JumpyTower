using System;
using UnityEngine;
using TMPro;

public class CharacterMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public GameController game;
    public int y=0;

    float horizontalMove;
    public float movementSpeed;
    bool jump;
    public float boost = 0;
    public Rigidbody2D player;
    public int combo;
    float jumpBoost;
    public TextMeshProUGUI floor;

    public Animator animator;


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Floor")
        {
            floor.text = "Floor: " + Convert.ToString(game.currentFloor);
            game.score(game.currentFloor - y);
            y = game.currentFloor;
        }
        
    }


    void FixedUpdate()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("speed", Mathf.Abs(player.velocity.x));
        animator.SetFloat("verticalSpeed", player.velocity.y);
        jump = false;
        if (Input.GetKey(KeyCode.Space))
        {
            jump = true;
        }


        if (Mathf.Abs(player.velocity.x) > 15)
        {
            jumpBoost = 5f;
        }
        else if (Mathf.Abs(player.velocity.x) > 14)
        {
            jumpBoost = 4f;
        }
        else if (Mathf.Abs(player.velocity.x) > 11.5)
        {
            jumpBoost = 2.5f;
        }
        else
        {
            jumpBoost = 0;
        }
        controller.jumpForce = 3f + jumpBoost;
        /*****************************************************************/
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) && !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)))
        {
            if (boost < 0)
            {
                boost *= -0.2f;
            }
            boost += 0.5f;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) && !(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
        {
            if (boost > 0)
            {
                boost *= -0.2f;
            }
            boost -= 0.5f;
        }
        else
        {
            boost = 0;
        }

        controller.Move(((horizontalMove * movementSpeed + boost) * Time.deltaTime), jump);
    }
}
