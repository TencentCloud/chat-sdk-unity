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
using System.Text;
using System.Collections.Generic;
public class CommunityDeletePermissionGroupFromCommunity : MonoBehaviour
{
  public Text Header;
  public InputField GroupID;
  public InputField PermissionGroupIDs;
  public Text Result;
  public Button Submit;
  public Button Copy;
  void Start()
  {
    string label = "PermissionGroupIDsInputTips";
    GameObject.Find(label).GetComponent<Text>().text = Utils.t(label);
    Header = GameObject.Find("HeaderText").GetComponent<Text>();
    GroupID = GameObject.Find("GroupID").GetComponent<InputField>();
    PermissionGroupIDs = GameObject.Find("PermissionGroupIDs").GetComponent<InputField>();
    Result = GameObject.Find("ResultText").GetComponent<Text>();
    Submit = GameObject.Find("Submit").GetComponent<Button>();
    Copy = GameObject.Find("Copy").GetComponent<Button>();
    Submit.onClick.AddListener(CommunityDeletePermissionGroupFromCommunitySDK);
    Copy.GetComponentInChildren<Text>().text = Utils.t("Copy");
    Copy.onClick.AddListener(CopyText);
    if (CurrentSceneInfo.info != null)
    {
      Header.text = Utils.IsCn() ? CurrentSceneInfo.info.apiText + " " + CurrentSceneInfo.info.apiName : CurrentSceneInfo.info.apiName;
      Submit.GetComponentInChildren<Text>().text = CurrentSceneInfo.info.apiName;
    }
  }

  void CommunityDeletePermissionGroupFromCommunitySDK()
  {
    var permissionGroupIDList = string.IsNullOrEmpty(PermissionGroupIDs.text) ? null : new List<string>(PermissionGroupIDs.text.Split(' ', StringSplitOptions.RemoveEmptyEntries));
    TIMResult res = TencentIMSDK.CommunityDeletePermissionGroupFromCommunity(GroupID.text, permissionGroupIDList, Utils.addAsyncStringDataToScreen(GetResult));
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