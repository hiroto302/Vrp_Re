using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 猫のOption1
public class CatOption1 : Option
{
    public override string OptionName()
    {
        optionName = "説明をきく";
        return optionName;
    }
  public override void OptionExecution()
  {
    SetEmissionColor(color1);
  }
}
