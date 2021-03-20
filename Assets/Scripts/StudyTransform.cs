using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudyTransform : MonoBehaviour
{
    Transform myTransform;
    /* 5.1
    Tilt the player using the R keys. When the R keys are released
    the player will be rotated back to the center using Slerp.
    */
    float smooth = 4.0f;     // 回転速度
    float tiltAngle = 60.0f; // 傾斜角
    /* 6.1
    90度回転しているplayerを猫がいる方向に向かせる
    */
    float rotationSpeed = 1.0f;
    Transform catTransform;
    void Start()
    {
        // myTransform = gameObject.GetComponent<Transform>();
        // myTransform.eulerAngles += new Vector3(0, 10.0f, 0);     3.ワールド座標のy軸を基準に10度回転
        transform.Rotate(0, 90.0f, 0, Space.World);            // 4.ワールド座標を基準とした回転
        catTransform = GameObject.Find("Cat").GetComponent<Transform>();

    }
    void Update()
    {
        // Debug.Log(myTransform.eulerAngles);        1.ワールド座標を基準にした角度取得
        // Debug.Log(myTransform.localEulerAngles);   2.ローカル座標を基準にした角度取得

        //5.2 Smoothly tilts a transform towards a target rotation.
        // float tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle;
        // float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;

        //5.3 Rotate the cube by converting the angles into a quaternion.
        // Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);

        //5.4 Dampen towards the target rotation
        // transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * smooth);

        //6.2
        // transform.rotation = Quaternion.Slerp(transform.rotation, catTransform.rotation, rotationSpeed * Time.deltaTime); //第一引数 開始位置(from), 第二引数 終わりの位置(to), 第三引数 現在の位置(0~1)
        // transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(catTransform.position.x, transform.position.y, catTransform.position.z) - transform.position), rotationSpeed * Time.deltaTime);
    }
}
