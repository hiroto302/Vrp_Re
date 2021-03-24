using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleDoorHandle : MonoBehaviour
{
    // 扉の状態
    public enum State
    {
        Open,
        Close,
        Fixed
    }
    // 現在の扉の状態
    public State currentState;
    // 回転させる扉
    [SerializeField]
    Transform door = null;
    // 回転の中心軸
    [SerializeField]
    Transform centerPoint = null;
    // 扉の角速度
    float anglerVelocity = -45.0f;
    // 扉がしめきられる角度
    float closedValue = 155.0f;

    void Reset()
    {
        door = transform.root;
        centerPoint = transform.parent.Find("CenterPoint");
    }
    void Start()
    {
        SetState(State.Open);
    }
    void Update()
    {
        // 閉めることが可能な状態に変更
        if(Input.GetKeyDown(KeyCode.C))
        {
            SetState(State.Close);
        }
        Debug.Log(door.right.normalized);
        if(currentState == State.Close && Input.GetKey(KeyCode.D))
        {
            // door.RotateAround(centerPoint.position, door.right.normalized, anglerVelocity * Time.deltaTime);
            door.RotateAround(centerPoint.position, door.forward.normalized, anglerVelocity * Time.deltaTime);
            closedValue += anglerVelocity * Time.deltaTime;
            if(closedValue < 0)
            {
                SetState(State.Fixed);
            }
        }

    }
    public void SetState(State state)
    {
        currentState = state;
    }

}
