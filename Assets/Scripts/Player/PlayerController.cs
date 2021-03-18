using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Playerのスクリプト
// オキュラスクエスト用 transform.TranslateによるPlayerの移動方法

// Translate の 移動法でカクツク原因は, time.deltaTimeを利用すると起こるのが一つの原因であった
// 現在の解決方法
// 1. FPSが約40であるため、その乗数をかける
//    ＝＞ 対象機種がオキュラスクエストであってもデバイスによる性能差が出る場合が問題である

/* 代表的なコード順序
    フィールド
    コンストラクタ・デストラクタ
    デリゲート
    イベント
    列挙体
    プロパティ
    メソッド
*/

public class PlayerController : MonoBehaviour
{
    // フィールド
    private float angleSpeed = 45.0f;       // 角速度
    private float moveSpeed = 2.0f;         // 移動速度
    private float speedMultiplier = 0.02f;  // 速度の乗数
    // コントローラーからの入力値
    private float x, y;
    Vector3 move = Vector3.zero;
    // 顔が向いてる方向
    [SerializeField]
    Transform moveTarget = null;

    // 列挙体
    // Playerの状態
    public enum State
    {
        Normal,
        Talk
    }
    public State currentState; // 現在の状態

    // イベント関数
    void Awake()
    {
        SetState(State.Normal); // playerの初期状態 Normal
    }
    void Reset()
    {
        if(!moveTarget)
        {
            moveTarget = GetComponentInChildren<OVRCameraRig>().transform.Find("TrackingSpace/CenterEyeAnchor");
        }
    }

    void Update()
    {
        // Normal状態の時,移動可能
        if(currentState == State.Normal)
        {
            // 左スティックの入力 角度・旋回
            Vector2 leftStick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
            transform.Rotate(new Vector3(0, leftStick.x, 0) * angleSpeed * speedMultiplier);
            // 右スティックの入力 方向・移動
            Vector2 rightStick = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
            x = rightStick.x;
            y = rightStick.y;
            move = (x * moveTarget.right.normalized + y * moveTarget.forward.normalized) * moveSpeed * speedMultiplier;
            transform.Translate(move, Space.World);
        }

    }
    // Playerの状態を変更するメソッド
    public void SetState(State state)
    {
        currentState = state;
    }
}
