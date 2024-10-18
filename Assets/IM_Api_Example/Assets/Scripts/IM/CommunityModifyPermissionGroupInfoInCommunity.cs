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
public class CommunityModifyPermissionGroupInfoInCommunity : MonoBehaviour
{
  public Text Header;
  public InputField GroupID;
  public InputField PermissionGroupID;
  public Text Result;
  public Button Submit;
  public Button Copy;
  void Start()
  {
    Header = GameObject.Find("HeaderText").GetComponent<Text>();
    GroupID = GameObject.Find("GroupID").GetComponent<InputField>();
    PermissionGroupID = GameObject.Find("PermissionGroupID").GetComponent<InputField>();
    Result = GameObject.Find("ResultText").GetComponent<Text>();
    Submit = GameObject.Find("Submit").GetComponent<Button>();
    Copy = GameObject.Find("Copy").GetComponent<Button>();
    Submit.onClick.AddListener(CommunityModifyPermissionGroupInfoInCommunitySDK);
    Copy.GetComponentInChildren<Text>().text = Utils.t("Copy");
    Copy.onClick.AddListener(CopyText);
    if (CurrentSceneInfo.info != null)
    {
      Header.text = Utils.IsCn() ? CurrentSceneInfo.info.apiText + " " + CurrentSceneInfo.info.apiName : CurrentSceneInfo.info.apiName;
      Submit.GetComponentInChildren<Text>().text = CurrentSceneInfo.info.apiName;
    }
  }

  void CommunityModifyPermissionGroupInfoInCommunitySDK()
  {
    PermissionGroupInfo permissionGroupInfo = new PermissionGroupInfo();
    permissionGroupInfo.community_group_id = GroupID.text;
    permissionGroupInfo.permission_group_id = PermissionGroupID.text;
    permissionGroupInfo.permission_group_name = "permission_group_test modify name";
    permissionGroupInfo.permission_custom_data = "permission group custom data modified";
    permissionGroupInfo.group_permission = 15;
    permissionGroupInfo.permission_group_modify_info_flag = (uint)TIMPermissionGroupModifyInfoFlag.kTIMPermissionGroupModifyInfoFlag_Name|
      (uint)TIMPermissionGroupModifyInfoFlag.kTIMPermissionGroupModifyInfoFlag_GroupPermission|(uint)TIMPermissionGroupModifyInfoFlag.kTIMPermissionGroupModifyInfoFlag_CustomData;

    TIMResult res = TencentIMSDK.CommunityModifyPermissionGroupInfoInCommunity(permissionGroupInfo, Utils.addAsyncStringDataToScreen(GetResult));
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