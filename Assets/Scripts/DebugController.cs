using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour
{
    // 変数群
    Rigidbody rb;
    private float angleSpeed = 45.0f; // 角速度
    private float moveSpeed = 2.25f;  // 移動速度
    float x = 0; // 左右
    float z = 0; // 前後
    // 動かす対象
    [SerializeField]
    Transform moveTarget = null;
    // 動かす対象の取得
    void Reset()
    {
        if(!moveTarget)
        {
            moveTarget = GetComponentInChildren<OVRCameraRig>().transform.Find("TrackingSpace/CenterEyeAnchor");
        }
    }
    void Start()
    {
        // rbの取得
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Playerの回転 y軸基軸
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(new Vector3(0, 1.0f, 0) * angleSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(new Vector3(0, -1.0f, 0) * angleSpeed * Time.deltaTime);
        }
        // Playerの移動
        // 左右の入力値
        if (Input.GetKey(KeyCode.D))
        {
            x = 1.0f;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            x = -1.0f;
        }
        else
        {
            x = 0;
        }
        // 前後の入力値
        if (Input.GetKey(KeyCode.W))
        {
            z = 1.0f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            z = -1.0f;
        }
        else
        {
            z = 0;
        }
        // 移動する値
        Vector3 move = (x * moveTarget.transform.right.normalized + z * moveTarget.transform.forward.normalized) * moveSpeed * Time.deltaTime;
        // world座標系に対して相対的に移動
        // CenterEyeAnchorのtransformを対象に移動するので、第二引数をworld座標系に指定
        transform.Translate(move, Space.World);
    }
}
