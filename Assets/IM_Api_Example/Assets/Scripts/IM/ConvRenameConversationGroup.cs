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
public class ConvRenameConversationGroup : MonoBehaviour
{
  public Text Header;
  public Text Result;
  public InputField OldGroupName;
  public InputField NewGroupName;
  public Button Submit;
  public Button Copy;

  private List<ConvInfo> ConvList;
  void Start()
  {
    Header = GameObject.Find("HeaderText").GetComponent<Text>();
    OldGroupName = GameObject.Find("OldGroupName").GetComponent<InputField>();
    NewGroupName = GameObject.Find("NewGroupName").GetComponent<InputField>();
    Result = GameObject.Find("ResultText").GetComponent<Text>();
    Submit = GameObject.Find("Submit").GetComponent<Button>();
    Copy = GameObject.Find("Copy").GetComponent<Button>();
    Copy.GetComponentInChildren<Text>().text = Utils.t("Copy");
    Submit.onClick.AddListener(ConvRenameConversationGroupSDK);
    Copy.onClick.AddListener(CopyText);
    if (CurrentSceneInfo.info != null)
    {
      Header.text = Utils.IsCn() ? CurrentSceneInfo.info.apiText + " " + CurrentSceneInfo.info.apiName : CurrentSceneInfo.info.apiName;
      Submit.GetComponentInChildren<Text>().text = CurrentSceneInfo.info.apiName;
    }
  }

  void ConvRenameConversationGroupSDK()
  {
    TIMResult res = TencentIMSDK.ConvRenameConversationGroup(OldGroupName.text, NewGroupName.text, Utils.addAsyncNullDataToScreen(GetResult));
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