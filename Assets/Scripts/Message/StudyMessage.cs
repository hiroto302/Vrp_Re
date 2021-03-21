using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

// 指定した文字を、Panelのtextに表示するクラス
// SetMessagePanelメソッドで、SetMessageに指定したルール(splitStringなど)で表示したい文字列を記述
// MessageStartでセットした文字の表示開始    void Start()
public class StudyMessage : MonoBehaviour
{
    // メッセージUI
    private Text messageText;
    // 表示するメッセージ
    [SerializeField]
    [TextArea(1, 10)]
    private string allMessage =
                "一番目\n"
                + "Text入力例\n<>"
                + "allMessageの二番目に表示されるもの";
    // 使用する分割文字
    // 会話の内容を分割する時の分割文字列
    [SerializeField]
    public string splitString = "<>";
    // 分割した文字列を格納する配列
    private string[] splitMessage;
    // 分割したメッセージが何番目のものであるか
    private int messageNum;
    // テキストスピード
    [SerializeField]
    private float textSpeed = 0.05f;
    // 経過時間
    private float elapsedTime = 0;
    // 現在表示しているmessageにおいて、今見ている何番目であるか表す番号
    private int nowTextNum = 0;
    // マウスクッリクを促すアイコン
    private Image clickIcon;
    // クリックアイコンの点滅秒数
    [SerializeField]
    private float clickFlashTime = 0.2f;
    // 一回分のメッセージを表示したか
    private bool isOneMessage = false;
    // 全てのメッセージを表示したか
    private bool isEndMessage = false;
    // メッセージスタート
    public bool isStartMessage = false;

    private int[] arrayTest = {123, 456, 789};
    int ch;
    void Start()
    {
        // クリックアイコンの取得
        clickIcon = transform.Find("MessagePanel/Image").GetComponent<Image>();
        // クリックアイコン非表示
        clickIcon.enabled = false;
        // textの取得
        messageText = transform.Find("MessagePanel/Text").GetComponent<Text>();
        messageText.text = "";
        // Panel 非表示
        transform.GetChild(0).gameObject.SetActive(false);

        SetMessagePanel(TestMessage());
        allMessage = "abccc<>def<>ghi";
        splitMessage = Regex.Split(allMessage, @"\s*" + splitString + @"\s*", RegexOptions.IgnorePatternWhitespace);
        messageNum = 0;
        // messageText.text += splitMessage[0][0];
        // messageText.text += splitMessage[1][0];
        // messageText.text += (splitMessage[0][0] + splitMessage[1][0]);
        // messageText.text += splitMessage[0].Substring(1);
        // MessageStart();
        // int ch = allMessage[0] + allMessage[3]; // ある文字列からインデックスで指定した同士の足し算 char型 + char型 = 数字(Int32)
        messageText.text = allMessage[0].ToString() + allMessage[3].ToString(); // char型を文字列に変換
    }
    void Update()
    {
        // Debug.Log(splitMessage[0].Substring(1));
        // Debug.Log("a" + allMessage[3]);           // ある文字列からインデックスで指定した文字と、別の文字列の足し算は可能 string型 + char型 = 文字列
        Debug.Log((allMessage[0].ToString() + allMessage[3].ToString()).GetType()); 
        // Debug.Log(allMessage[3].GetType()); //char型
        // 会話フラグ
        if(!isStartMessage)
        {
            return;
        }
        else if(isStartMessage)
        {
            // メッセージが終わっているか、ない場合会話終了
            if( isEndMessage || allMessage == null)
            {
                isStartMessage = false;
                MessageEnd();
                return;
            }
            // allMessageを分割した一つのmessageが、全ての文字列を表示しきれていない時
            if (!isOneMessage)
            {
                // 一定時間の経過後、文字を追加
                if (elapsedTime >= textSpeed)
                {
                    messageText.text += splitMessage[messageNum][nowTextNum];
                    // 値の更新
                    nowTextNum++;
                    elapsedTime = 0;
                    // メッセージを全部表示・または行数が最大数表示された時
                    if (nowTextNum >= splitMessage[messageNum].Length)
                    {
                        isOneMessage = true; // 分割した内の一つのmessage 表示完了
                    }
                }
                elapsedTime += Time.deltaTime;

                // メッセージを表示中にマウスの左ボタンを押した時
                if (Input.GetMouseButtonDown(0))
                {
                    // 全てのテキストを表示
                    messageText.text += splitMessage[messageNum].Substring(nowTextNum);
                    isOneMessage = true;
                }
            }
            // 一回に表示するメッセージを表示した後の処理
            else
            {
                elapsedTime += Time.deltaTime;
                // クリックアイコの点滅
                if (elapsedTime >= clickFlashTime)
                {
                    clickIcon.enabled = !clickIcon.enabled;
                    elapsedTime = 0;
                }
                // マウスがクリックされたら次の文字表示処理の準備
                if (Input.GetMouseButtonDown(0))
                {
                    nowTextNum = 0;
                    messageNum++;
                    messageText.text = "";
                    clickIcon.enabled = false;
                    elapsedTime = 0;
                    isOneMessage = false;
                    // 全てのメッセージが表示された後の処理
                    if (messageNum >= splitMessage.Length)
                    {
                        isEndMessage = true;
                        transform.GetChild(0).gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    // 新しいメッセージの設定
    // 全ての会話文を引数で受け取り、分割して表示していくために配列にする
    void SetMessage(string message)
    {
        this.allMessage = message;
        // 分割文字列で一回に表示するメッセージを分割
        // Regex.Splitメソッド使用し、allMessage(第一引数)を正規表現パターン(第二引数)で分割し、戻り値であるstringの配列を得る
        // 分割文字列はsplitStringの文字と前後に\s*を付けて『空白文字列splitString空白文字列』というパターンを分割文字列とする
        // \s* : 0個以上の空白文字と一致
        // 第３引数のオプションで空白無視
        splitMessage = Regex.Split(allMessage, @"\s*" + splitString + @"\s*", RegexOptions.IgnorePatternWhitespace);
        nowTextNum = 0;
        messageNum = 0;
        messageText.text = "";
        isOneMessage = false;
        isEndMessage = false;
    }

    // 他のスクリプトから新しいメッセージを設定しUIをアクティブにする
    public void SetMessagePanel(string message)
    {
        SetMessage(message);
        transform.GetChild(0).gameObject.SetActive(true);
    }

    // 下記の2つの処理は、このクラスを継承したもので,メッセージ開始と終わり時に行いたい処理を記述する
    // message の表示開始
    void MessageStart()
    {
        isStartMessage = true;
    }
    // message終了後の処理
    void MessageEnd()
    {
    }


    // Debug用
    public string TestMessage()
    {
        string catMessage = "１ページ目１段落目\n１ページ目２段落目<>"
                            + "2ページ目<>"
                            + "3ページ目";
        return catMessage;
    }



}
