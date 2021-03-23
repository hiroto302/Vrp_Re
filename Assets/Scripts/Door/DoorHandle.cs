using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandle : MonoBehaviour
{
    float dz = 0;         // 対辺の長さ
    float dx;             // 隣辺の長さ
    float radius;         // 半径の長さ
    float deg;            // 角度
    float posteriorAngle; // 移動後の角度

    // ドアに触れている手の位置のオブジェクト 変数群
    [SerializeField]
    protected GameObject handPoint = null;
    protected Transform handPointTransform;
    protected HandPoint handPointScript;
    // 回転の中心軸
    [SerializeField]
    protected Transform centerPoint = null;
    // 回転させるドアのオブジェクト
    [SerializeField]
    protected Transform door = null;

    void Reset()
    {
        handPoint  = transform.parent.Find("HandPoint").gameObject;
        centerPoint = transform.parent.Find("CenterPoint");
        door = transform.root;
    }
    void Start()
    {
        handPointTransform = handPoint.transform;
        handPointScript = handPoint.GetComponent<HandPoint>();
        // 半径の取得・初期化
        Vector3 dir = centerPoint.localPosition - handPointScript.InitialPosition;
        radius = dir.magnitude;
        // 隣辺の初期の長さ
        dx = radius;
    }

    // Handの座標取得・移動条件
    // Colliderの衝突判定したが、HandPoint と Handの距離から算出したりするなど色々開閉の条件なども変えるのもいいかも
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Hand")
        {
            // ワールド座標のHandの位置取得・HandPointの移動
            handPointTransform.position = other.gameObject.transform.position;
        }
    }
    void Update()
    {
        // 対辺の取得
        dz = handPointTransform.localPosition.z - handPointScript.InitialPosition.z;
        // 隣辺を求める
        dx = Mathf.Sqrt(radius * radius - dz * dz);
        // オイラー角を取得(度の表記)
        float rad = Mathf.Atan2(dz, dx);
        // オイラー角をラジアンに変換(πの表記)
        deg = rad * Mathf.Rad2Deg;
        // 手の位置により変化したdzから、degを算出し、それにより生まれた角度分移動
        door.RotateAround(centerPoint.position, Vector3.up, deg);
        // 移動させた後,HandPointをリセットする
        handPointTransform.localPosition = handPointScript.InitialPosition;
    }
}
