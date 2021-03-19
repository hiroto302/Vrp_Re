using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPanelController : FadePanelController
{
    public override void InitialImageColor()
    {
        // 初期 透明
        // image.color = TransparentBlackColor();
        image.color = BlackColor();
    }
    public override void Start()
    {
        // 5秒かけてFadeOut
        SetFadeOutTime(5.0f);
        SetFadeInSpeed(5.0f);
    }
}
