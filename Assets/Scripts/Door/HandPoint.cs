using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 取っ手に手がふれている時、Player の Hand の位置を取得 対辺を取得するために利用
public class HandPoint : MonoBehaviour
{
    // 初期位置
    Vector3 initialPosition;
    // プロパティ
    public Vector3 InitialPosition
    {
        get{ return initialPosition;}
    }
    void Reset()
    {
        // DoorHandleと同じ位置に配置
        Transform doorHandle = transform.parent.Find("DoorHandle");
        transform.localPosition = doorHandle.localPosition;
    }
    void Awake()
    {
        // ローカル座標の初期位置
        initialPosition = transform.localPosition;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(gameObject.transform.position, 0.1f);
    }
}
