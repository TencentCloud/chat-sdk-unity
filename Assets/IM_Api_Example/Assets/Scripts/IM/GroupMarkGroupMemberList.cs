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
public class GroupMarkGroupMemberList : MonoBehaviour
{
  public Text Header;
  public Text Result;
  public InputField Members;
  public InputField GroupID;
  public InputField MarkType;
  public Toggle IsEnable;
  public Button Submit;
  public Button Copy;
  void Start()
  {
    Header = GameObject.Find("HeaderText").GetComponent<Text>();
    Members = GameObject.Find("Members").GetComponent<InputField>();
    GroupID = GameObject.Find("GroupID").GetComponent<InputField>();
    MarkType = GameObject.Find("MarkType").GetComponent<InputField>();
    IsEnable = GameObject.Find("IsEnable").GetComponent<Toggle>();
    Result = GameObject.Find("ResultText").GetComponent<Text>();
    Submit = GameObject.Find("Submit").GetComponent<Button>();
    Copy = GameObject.Find("Copy").GetComponent<Button>();
    Copy.GetComponentInChildren<Text>().text = Utils.t("Copy");
    Submit.onClick.AddListener(GroupMarkGroupMemberListSDK);
    Copy.onClick.AddListener(CopyText);
    if (CurrentSceneInfo.info != null)
    {
      Header.text = Utils.IsCn() ? CurrentSceneInfo.info.apiText + " " + CurrentSceneInfo.info.apiName : CurrentSceneInfo.info.apiName;
      Submit.GetComponentInChildren<Text>().text = CurrentSceneInfo.info.apiName;
    }
  }

  void GroupMarkGroupMemberListSDK()
  {
    TIMResult res = TencentIMSDK.GroupMarkGroupMemberList(GroupID.text, new List<string>(
      Members.text.Split(',')
    ), Convert.ToInt32(MarkType.text), IsEnable.isOn, Utils.addAsyncNullDataToScreen(GetResult));
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