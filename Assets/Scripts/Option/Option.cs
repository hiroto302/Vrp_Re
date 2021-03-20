using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 抽象クラスOption
public abstract class Option : MonoBehaviour
{
    [SerializeField]
    Text optionText = null;
    // 選択肢の名称
    public string optionName;
    // 選択肢の名称を記述
    public abstract string OptionName();
    // 選択を識別するためのMaterial
    [SerializeField]
    protected MeshRenderer meshRenderer = null;
    // 非選択時の色
    protected Color color1 = new Color(250.0f / 255.0f, 250.0f / 255.0f, 250.0f / 255.0f);
    // 選択時の色
    protected Color color2 = new Color(0 / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f);
    // Optionを表示しているPanelを操作するスクリプト
    [SerializeField]
    protected OptionPanelController optionPanelcontroller = null;

    void Reset()
    {
        // Textの取得
        optionText = transform.GetChild(0).gameObject.GetComponent<Text>();
        // 選択肢の名称書き換え
        optionText.text = OptionName();
        // Materialの取得
        meshRenderer = GetComponent<MeshRenderer>();
        SetEmissionColor(color1);
        // OptionPanelControllerの取得
        optionPanelcontroller = transform.root.gameObject.GetComponentInChildren<OptionPanelController>();
    }
    void Start()
    {
        meshRenderer.material.EnableKeyword("_EMISSION");
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.R))
        {
            SetEmissionColor(color2);
        }
        if(Input.GetKeyUp(KeyCode.R))
        {
            SetEmissionColor(color1);
        }
    }
    // 選択された時に実行するメソッド
    public abstract void OptionExecution();
    // Emissionの色を変更するメソッド
    protected void SetEmissionColor(Color color)
    {
        meshRenderer.material.SetColor("_EmissionColor", color);
    }
}
