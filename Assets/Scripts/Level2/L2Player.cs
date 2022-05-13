using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2Player : MonoBehaviour
{

    public static Player instance;

    private int speed = 10;

    private Rigidbody2D playerRigidbody2D;
    private Vector3 moveDir;
    private State state;
    private AudioSource audioSource;
    private Animator animator;
    public AudioClip[] sounds;
    private bool isInVent;
    private bool isMoving;

    private enum State
    {
        Normal,
    }

    private void Awake()
    {
        playerRigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        SetStateNormal();
    }

    private void Update()
    {
        switch (state)
        {
            case State.Normal:
                HandleMovement();
                break;
        }
    }



    private void SetStateNormal()
    {
        state = State.Normal;
    }

    private void HandleMovement()
    {
        float moveX = 0f;
        float moveY = 0f;
        isMoving = false;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            moveY = +1f;
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            moveY = -1f;
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveX = -1f;
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveX = +1f;
            isMoving = true;
        }
        if (isMoving)
        {
            if (isInVent)
            {
                SoundEffect("vent");
            }
        }

        if (moveX == 0f && moveY == 0f)
        {
            audioSource.Pause();
        }

        moveDir = new Vector3(moveX, moveY).normalized;
    }

    private void FixedUpdate()
    {
        bool isIdle = moveDir.x == 0 && moveDir.y == 0;
        if (isIdle)
        {
            animator.Play("Anim_Drone_Idle"); //anim idle
        }
        else
        {
            animator.Play("Anim_Drone_Movement"); //move anim

            //transform.position += moveDir * SPEED * Time.deltaTime;
            playerRigidbody2D.MovePosition(transform.position + moveDir * speed * Time.fixedDeltaTime);
        }

    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    private void PlayerCaught()
    {
        FindObjectOfType<GameManager>().EndGame();

    }

    private void SoundEffect(string name)
    {
        switch (name)
        {
            case "vent":
                audioSource.clip = sounds[0];
                break;
        }

        if (audioSource.isPlaying)
        {
            audioSource.UnPause();
        }
        else
        {
            audioSource.Play();
        }
        
    }

    public void GetIntoTheVent()
    {
        isInVent = true;
    }
    public void GetOutTheVent()
    {
        audioSource.Stop();
        isInVent = false;
    }

}
