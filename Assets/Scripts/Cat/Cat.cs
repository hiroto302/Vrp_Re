using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public enum State
    {
        Normal,
        Talk
    }
    public State currentState;
    float rotationSpeed = 1.0f; // 振り向き速度
    [SerializeField]
    Transform playerTransform = null; // 振り向く方向・相手(Player)
    [SerializeField]
    PlayerController playerController = null; // playerのスクリプト

    Animator animator; // animation
    float elapsedTime = 0; // 経過時間
    float currentDirection; // 現在の向き
    float nextDirection; // 一定時間経過後に、向いてる方向

    void Reset()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        currentState = State.Normal;
    }
    void Update()
    {
        // 猫の状態をPlayerの状態と同様にする
        if(playerController.currentState == PlayerController.State.Normal && currentState != State.Normal)
        {
            SetState(State.Normal);
        }
        else if(playerController.currentState == PlayerController.State.Talk && currentState != State.Talk)
        {
            SetState(State.Talk);
        }
        // Talk状態に移行する時の処理
        if(currentState == State.Talk)
        {
            // Playerの方向に向かせる
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z) - transform.position), rotationSpeed * Time.deltaTime);

            // 振り向きのアニメーション制御
            if(elapsedTime < 0.2f)
            {
                currentDirection = transform.localEulerAngles.y;
            }
            elapsedTime += Time.deltaTime;
            if(elapsedTime > 0.2f)
            {
                nextDirection = transform.localEulerAngles.y;
                elapsedTime = 0;
            }
            if(Mathf.Abs(currentDirection -nextDirection) > 0.05f)
            {
                animator.SetBool("TurnAround", true);
            }
            else
            {
                animator.SetBool("TurnAround", false);
            }
        }

    }
    public void SetState(State state)
    {
        currentState = state;
        // animation処理
        if(state == State.Normal)
        {
            animator.SetBool("Talk", false);
        }
        else if(state == State.Talk)
        {
            animator.SetBool("Talk", true);
        }
    }


}
