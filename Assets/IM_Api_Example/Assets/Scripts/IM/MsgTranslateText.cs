using UnityEngine;
using UnityEngine.UI;
using com.tencent.im.unity.demo.types;
using com.tencent.imsdk.unity;
using com.tencent.imsdk.unity.types;
using com.tencent.imsdk.unity.enums;
using System;
using com.tencent.im.unity.demo.utils;
using EasyUI.Toast;
using System.Collections;
using System.Collections.Generic;
public class MsgTranslateText : MonoBehaviour
{
  public Text Header;
  public Text Result;
  public InputField Input;
  public InputField Source;
  public InputField Target;
  public Button Submit;
  public Button Copy;
  void Start()
  {
    Header = GameObject.Find("HeaderText").GetComponent<Text>();
    Input = GameObject.Find("Input").GetComponent<InputField>();
    Source = GameObject.Find("Source").GetComponent<InputField>();
    Target = GameObject.Find("Target").GetComponent<InputField>();
    Result = GameObject.Find("ResultText").GetComponent<Text>();
    Submit = GameObject.Find("Submit").GetComponent<Button>();
    Copy = GameObject.Find("Copy").GetComponent<Button>();
    Copy.GetComponentInChildren<Text>().text = Utils.t("Copy");
    Submit.onClick.AddListener(MsgTranslateTextSDK);
    Copy.onClick.AddListener(CopyText);
    if (CurrentSceneInfo.info != null)
    {
      Header.text = Utils.IsCn() ? CurrentSceneInfo.info.apiText + " " + CurrentSceneInfo.info.apiName : CurrentSceneInfo.info.apiName;
      Submit.GetComponentInChildren<Text>().text = CurrentSceneInfo.info.apiName;
    }
  }

  void MsgTranslateTextSDK()
  {
    TIMResult res = TencentIMSDK.MsgTranslateText(new List<string>(
      Input.text.Split(',')
    ), Source.text, Target.text, Utils.addAsyncStringDataToScreen(GetResult));
    Result.text = Utils.SynchronizeResult(res);
  }

  void GetResult(params object[] parameters)
  {
    Result.text += (string)parameters[0];
  }

  void CopyText()
  {
    Utils.Copy(Result.text);
  }
  void OnApplicationQuit()
  {
    TencentIMSDK.Uninit();
  }
}