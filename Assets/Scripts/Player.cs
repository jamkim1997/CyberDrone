using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public static Player instance;

    private float SPEED = 10f;
    
    private Rigidbody2D playerRigidbody2D;
    private Vector3 moveDir;
    private State state;
    public Animator animator;
    bool isBoost;

    private enum State {
        Normal,
    }

    private void Awake() {
        instance = this;
        playerRigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        SetStateNormal();
        isBoost = false;
    }

    private void Update() {
        switch (state) {
        case State.Normal:
            HandleMovement();
            break;
        }
    }


    
    private void SetStateNormal() {
        state = State.Normal;
    }

    private void HandleMovement() {
        float moveX = 0f;
        float moveY = 0f;
        
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            moveY = +1f;
            FindObjectOfType<Audio_Manager>().UnPause("DroneFly");
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            moveY = -1f;
            FindObjectOfType<Audio_Manager>().UnPause("DroneFly");

        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            moveX = -1f;
            FindObjectOfType<Audio_Manager>().UnPause("DroneFly");

        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            moveX = +1f;
            FindObjectOfType<Audio_Manager>().UnPause("DroneFly");

        }
        if(moveX == 0f && moveY == 0f){
            FindObjectOfType<Audio_Manager>().Pause("DroneFly");
        }

        moveDir = new Vector3(moveX, moveY).normalized;
    }

    private void FixedUpdate() {
        bool isIdle = moveDir.x == 0 && moveDir.y == 0;
        if (isIdle) {
            animator.Play("Anim_Drone_Idle"); //anim idle
        } else {
            animator.Play("Anim_Drone_Movement"); //move anim

            //transform.position += moveDir * SPEED * Time.deltaTime;
            playerRigidbody2D.MovePosition(transform.position + moveDir * SPEED * Time.fixedDeltaTime);
        }
        
    }

    public Vector3 GetPosition() {
        return transform.position;
    }

    private void PlayerCaught()
    {
        FindObjectOfType<GameManager>().EndGame();
         
    }

    public void changeSpeed()
    {
        if (isBoost)
        {
            isBoost = false;
            SPEED = 10f;
        }
        else
        {
            isBoost = true;
            SPEED = 20f;
        }
    }
  
}
