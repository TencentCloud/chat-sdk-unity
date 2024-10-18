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
public class CommunityRemoveCommunityMembersFromPermissionGroup : MonoBehaviour
{
  public Text Header;
  public InputField GroupID;
  public InputField PermissionGroupID;
  public InputField UserIDs;
  public Text Result;
  public Button Submit;
  public Button Copy;
  void Start()
  {
    string label = "UserIDsInputTips";
    GameObject.Find(label).GetComponent<Text>().text = Utils.t(label);
    Header = GameObject.Find("HeaderText").GetComponent<Text>();
    GroupID = GameObject.Find("GroupID").GetComponent<InputField>();
    PermissionGroupID = GameObject.Find("PermissionGroupID").GetComponent<InputField>();
    UserIDs = GameObject.Find("UserIDs").GetComponent<InputField>();
    Result = GameObject.Find("ResultText").GetComponent<Text>();
    Submit = GameObject.Find("Submit").GetComponent<Button>();
    Copy = GameObject.Find("Copy").GetComponent<Button>();
    Submit.onClick.AddListener(CommunityRemoveCommunityMembersFromPermissionGroupSDK);
    Copy.GetComponentInChildren<Text>().text = Utils.t("Copy");
    Copy.onClick.AddListener(CopyText);
    if (CurrentSceneInfo.info != null)
    {
      Header.text = Utils.IsCn() ? CurrentSceneInfo.info.apiText + " " + CurrentSceneInfo.info.apiName : CurrentSceneInfo.info.apiName;
      Submit.GetComponentInChildren<Text>().text = CurrentSceneInfo.info.apiName;
    }
  }

  void CommunityRemoveCommunityMembersFromPermissionGroupSDK()
  {
    var userIDList = string.IsNullOrEmpty(UserIDs.text) ? null : new List<string>(UserIDs.text.Split(' ', StringSplitOptions.RemoveEmptyEntries));
    TIMResult res = TencentIMSDK.CommunityRemoveCommunityMembersFromPermissionGroup(GroupID.text, PermissionGroupID.text, userIDList, Utils.addAsyncStringDataToScreen(GetResult));
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