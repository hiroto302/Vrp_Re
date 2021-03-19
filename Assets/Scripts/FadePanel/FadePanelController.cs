using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// シーン遷移時などに画面を徐々に暗転させる処理(フェードアウト）
// 暗転した画面を徐々に明るくする処理（フェードイン）の機能実装
public abstract class FadePanelController : MonoBehaviour
{
    [SerializeField]
    protected Image image = null; // 制御するPanelのImage
    float red, green, blue, alfa; // Imageの各色の値 alfa 0:完全に透明 255:完全に不透明
    float fadeAlfa; // fadeAlfaの値
    public float fadeOutSpeed; // 透明度が減少するスピード
    public float fadeInSpeed;  // 透明度が増加するスピード
    float elapsedTime = 0;
    float fadeOutTime, fadeInTime; // fadeにかかる時間
    public bool fadeOut = false; // フラグ
    public bool fadeIn = false;

    void Awake()
    {
        // 初期の色
        InitialImageColor();
        // 各色の値の取得
        red = image.color.r;
        green = image.color.g;
        blue = image.color.b;
        alfa = image.color.a;
    }
    void Reset()
    {
        image = GetComponent<Image>();
    }

    // fadeにかける時間メソッドを記述
    public abstract void Start();
    
    void Update()
    {
        if(fadeOut)
        {
            if(elapsedTime == 0)
            {
                // fadeAlfaの初期化
                fadeAlfa = alfa;
                // fadeSpeed取得
                SetFadeOutSpeed(fadeOutTime, fadeAlfa);
            }
            elapsedTime += Time.deltaTime;
            // fadeOut実行
            FadeOut(elapsedTime);
        }
        if(fadeIn)
        {
            if(elapsedTime == 0)
            {
                fadeAlfa = alfa;
                SetFadeInSpeed(fadeInTime, fadeAlfa);
            }
            elapsedTime += Time.deltaTime;
            FadeIn(elapsedTime);
        }
    }
    // 初期のImageColor設定
    public abstract void InitialImageColor();
    // fadeOut開始するメソッド
    public void FadeOutStart()
    {
        fadeOut = true;
    }
    protected void SetFadeOutTime(float time)
    {
        fadeOutTime = time;
    }
    // fadeOutにかける時間・スピードの決定
    void SetFadeOutSpeed(float time, float fadeAlfa)
    {
        fadeOutSpeed = 1.0f / time -fadeAlfa / time;
    }
    // fadeOutの機能
    void FadeOut(float elapsedTime)
    {
        // imageの表示
        if(image.enabled == false)
        {
            image.enabled = true;
        }
        // 透明度の上昇
        alfa = fadeAlfa + fadeOutSpeed * elapsedTime;
        // fadeの終了
        if(alfa > 1.0f)
        {
            fadeOut = false;
            this.elapsedTime = 0;
        }
        // 新たな色の反映
        SetAlfa();
    }
    // fadeIn機能
    public void FadeInStart()
    {
        fadeIn = true;
    }
    protected void SetFadeInSpeed(float time)
    {
        fadeInTime = time;
    }
    void SetFadeInSpeed(float fadeInTime, float fadeAlfa)
    {
        fadeInSpeed = fadeAlfa / fadeInTime;
    }
    void FadeIn(float elapsedTime)
    {
        if(image.enabled == false)
        {
            image.enabled = true;
        }
        alfa = 1.0f - fadeInSpeed * elapsedTime;
        if(alfa < 0)
        {
            fadeIn = false;
            this.elapsedTime = 0;
        }
        SetAlfa();
    }
    // 色を反映させるメソッド
    void SetAlfa()
    {
        image.color = new Color(red, green, blue, alfa);
    }
    // 黒色
    protected Color BlackColor()
    {
        return new Color(0, 0, 0 , 1);
    }
    // 黒の透明色
    protected new Color TransparentBlackColor()
    {
        return new Color(0, 0, 0, 0);
    }
}
