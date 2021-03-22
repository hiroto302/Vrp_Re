using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// 値により位置を変更する
public class ChaliceScale : MonoBehaviour
{
    // 合計値
    public float totalWeight = 0;
    // 移動箇所を格納している親オブジェクト
    [SerializeField]
    Transform scalePointsParent = null;
    // 移動する位置
    Transform[] scalePoints;
    // 移動させる基準点
    [SerializeField]
    Transform standardPoint = null;
    // 現在の位置
    Vector3 currentPosition;
    // 次の移動位置
    Vector3 nextPosition;
    // 移動距離(0.2間隔)
    float distance;
    // 移動にかける時間
    float second;
    // 移動スピード
    // float speed = 0.002f;
    float speed = 0.1f;
    // 符号 正(up) or 負(down)
    float sign;
    // [SerializeField]
    // SE se = null;
    // bool playSE = true;
    // 移動のフラグ
    bool move = false;
    // 移動した距離
    bool moveDistance = false;

    void Start()
    {
        // 移動位置の初期化
        scalePoints = new Transform[scalePointsParent.transform.childCount];

        for(int i = 0; i < scalePointsParent.transform.childCount; i++ )
        {
            scalePoints[i] = scalePointsParent.transform.GetChild(i);
        }
        // 現在位置の初期化
        currentPosition = scalePoints[(int)totalWeight].position;
        standardPoint.position = scalePoints[Mathf.CeilToInt(totalWeight)].position;
    }

    void Update()
    {
        // Debug.Log(sign);
        // Debug.Log(totalWeight);
        // Debug.Log(scalePoints[0].ToString()); // 出力結果 ScalePoint0
        // Debug.Log(Mathf.CeilToInt(0.4f));  // 出力結果 0
        // Debug.Log(currentPosition.y); // 0.06
        // Debug.Log(scalePoints[1].position.y); // 0.04 = nextPosition
        // Debug.Log(currentPosition.y - scalePoints[1].position.y); // 0.2
        // Debug.Log(scalePoints[1].position.y - currentPosition.y); // -0.2
        // Debug.Log(currentPosition - scalePoints[1].position); // (0.0, 0.2, 0.0)
        // Debug.Log((currentPosition - scalePoints[1].position).magnitude); // 0.2
        // Debug.Log(Mathf.Round(0.2f * 10.0f) * 0.1f); // distance = 0.2f
        // Debug.Log(0.2f / speed); // second = 100 Updateが呼べ出される回数

        // Debug.Log(Mathf.CeilToInt(1.5f)); // 2


        if(Input.GetKey(KeyCode.U))
        {
            sign = 1.0f;
        }
        if(Input.GetKey(KeyCode.D))
        {
            sign = -1.0f;
        }
        if(Input.GetKey(KeyCode.E))
        {
            totalWeight = 0;
        }
        if(Input.GetKey(KeyCode.R))
        {
            totalWeight = 1.0f;
        }
        if(Input.GetKey(KeyCode.T))
        {
            totalWeight = 1.5f;
        }
        if(Input.GetKey(KeyCode.Y))
        {
            totalWeight = 3.0f;
        }

        // totalWeightの変化に伴う移動
        if(totalWeight <= 3.0f)
        {
            nextPosition = scalePoints[Mathf.CeilToInt(totalWeight)].position;
        }

        if(currentPosition != nextPosition)
        {
            // 距離の取得 (0.2の倍数で取得するために下記の処理を行う)
            // Round f に最も近い整数を返す。数が .5 で終わる場合はふたつの整数（偶数と奇数）の中間に位置している時、偶数が返される。10.5 => 10, 11.5 =>12
            // 常に正の数を出力するためにmagnitudeで距離を取得
            distance = Mathf.Round((nextPosition - currentPosition).magnitude * 10.0f) * 0.1f;
            // 移動にかかる時間の取得 移動にかかるフレームの回数
            // second = distance / speed; // Updateがsecond回呼び出されてdistaceを移動する
            second = distance / 0.1f; // Updateがsecond回呼び出されてdistaceを移動する
            // 現在位置の更新
            currentPosition = nextPosition;
            move = true;
        }

        // 測りの移動
        // if(second > 0)
        // {
        //     standardPoint.Translate(new Vector3(0, sign * speed, 0)); // ローカル座標における移動
        //     second -= 1.0f;
        // }
        if(move)
        {
            standardPoint.Translate(new Vector3(0, sign * speed * Time.deltaTime, 0)); // ローカル座標における移動 0.1ｆ仮のspeed
            distance -= speed * Time.deltaTime;
            // もし移動完了したら
            if(distance <= 0)
            {
                move = false;
            }
        }
        // SE 重さが3以上の時の位置に移動が完了したことを知らせる音 (小数点以下の僅かな差が生まれるので下記の処理を行う)
        // if( Mathf.Abs(scalePoints[3].position.y - standardPoint.position.y) < 0.01f )
        // {
        //     if(playSE)
        //     {
        //         se.PlaySE(1);
        //         playSE = false;
        //     }
        // }
    }
}
