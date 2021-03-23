using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField]
    Transform centerPoint = null; // 回転の中心軸
    float anglerVelocity = 10.0f; // 角速度
    float openAngle = 30.0f; // 回転させる角度
    public bool OpenDoor = false; // 開閉のフラグ
    float elapsedTime = 0; // 経過時間


    void Reset()
    {
        // Transform.Find」は、子オブジェクトから指定した「Transform」を取得する関数
        // centerPoint = transform.GetChild(0).Find("CenterPoint");
        centerPoint = transform.Find("DoorParts/CenterPoint");
    }

    // Update is called once per frame
    void Update()
    {
        if(OpenDoor && openAngle > 0)
        {
            elapsedTime += Time.deltaTime;
            gameObject.transform.RotateAround(centerPoint.position, Vector3.up, anglerVelocity * Time.deltaTime);
            openAngle -= anglerVelocity * Time.deltaTime;
        }
    }
}
