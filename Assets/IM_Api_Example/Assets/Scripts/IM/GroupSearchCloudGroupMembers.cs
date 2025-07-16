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
public class GroupSearchCloudGroupMembers : MonoBehaviour
{
  public Text Header;
  public InputField Input;
  public InputField GroupIDs;
  public InputField Count;
  public Text Result;
  public Button Submit;
  public Button Copy;
  void Start()
  {
    string label = "KeywordInputTips";
    string label2 = "GroupIDsInputTips";
    GameObject.Find(label).GetComponent<Text>().text = Utils.t(label);
    GameObject.Find(label2).GetComponent<Text>().text = Utils.t(label2);
    Header = GameObject.Find("HeaderText").GetComponent<Text>();
    Input = GameObject.Find("Input").GetComponent<InputField>();
    GroupIDs = GameObject.Find("GroupIDs").GetComponent<InputField>();
    Count = GameObject.Find("Count").GetComponent<InputField>();
    Result = GameObject.Find("ResultText").GetComponent<Text>();
    Submit = GameObject.Find("Submit").GetComponent<Button>();
    Copy = GameObject.Find("Copy").GetComponent<Button>();
    Submit.onClick.AddListener(GroupSearchCloudGroupMembersSDK);
    Copy.GetComponentInChildren<Text>().text = Utils.t("Copy");
    Copy.onClick.AddListener(CopyText);
    if (CurrentSceneInfo.info != null)
    {
      Header.text = Utils.IsCn() ? CurrentSceneInfo.info.apiText + " " + CurrentSceneInfo.info.apiName : CurrentSceneInfo.info.apiName;
      Submit.GetComponentInChildren<Text>().text = CurrentSceneInfo.info.apiName;
    }
  }

  void GroupSearchCloudGroupMembersSDK()
  {
    var keywordList = string.IsNullOrEmpty(Input.text) ? null : new List<string>(Input.text.Split(' ', StringSplitOptions.RemoveEmptyEntries));
    var groupIDs = string.IsNullOrEmpty(GroupIDs.text) ? null : new List<string>(GroupIDs.text.Split(' ', StringSplitOptions.RemoveEmptyEntries));
    GroupMemberSearchParam groupMemberSearchParam = new GroupMemberSearchParam();
    groupMemberSearchParam.group_search_member_params_keyword_list = keywordList;
    groupMemberSearchParam.group_search_member_params_groupid_list = groupIDs;
    groupMemberSearchParam.group_member_search_params_search_count = string.IsNullOrEmpty(Count.text) ? 10 : uint.Parse(Count.text) > 0 ? uint.Parse(Count.text) : 10;
    TIMResult res = TencentIMSDK.GroupSearchCloudGroupMembers(groupMemberSearchParam, Utils.addAsyncStringDataToScreen(GetResult));
    Result.text = Utils.SynchronizeResult(res);
  }

  void GetResult(params object[] parameters)
  {
    Result.text += (string)parameters[0];
    var result = Utils.FromJson<GroupMemberSearchResult>((string)parameters[1]);
    if (result ==  null) {
      UnityEngine.Debug.Log("GroupMemberSearchResult convert error!");
    }
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