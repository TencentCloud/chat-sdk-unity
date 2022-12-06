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
public class GroupGetTopicInfoList : MonoBehaviour
{
  public Text Header;
  public Dropdown SelectedGroup;
  public Text Result;
  public InputField TopicID;
  public Button Submit;
  public Button Copy;

  private List<GroupInfo> GroupList;
  void Start()
  {
    GameObject.Find("GroupIDLabel").GetComponent<Text>().text = Utils.t("GroupIDLabel");
    GameObject.Find("TopicIDLabel").GetComponent<Text>().text = Utils.t("TopicIDLabel");
    GroupGetJoinedCommunityListSDK();
    Header = GameObject.Find("HeaderText").GetComponent<Text>();
    SelectedGroup = GameObject.Find("Dropdown").GetComponent<Dropdown>();
    TopicID = GameObject.Find("TopicID").GetComponent<InputField>();
    Result = GameObject.Find("ResultText").GetComponent<Text>();
    Submit = GameObject.Find("Submit").GetComponent<Button>();
    Copy = GameObject.Find("Copy").GetComponent<Button>();
    Copy.GetComponentInChildren<Text>().text = Utils.t("Copy");
    Submit.onClick.AddListener(GroupGetTopicInfoListSDK);
    Copy.onClick.AddListener(CopyText);
    SelectedGroup.interactable = true;
    if (CurrentSceneInfo.info != null)
    {
      Header.text = Utils.IsCn() ? CurrentSceneInfo.info.apiText + " " + CurrentSceneInfo.info.apiName : CurrentSceneInfo.info.apiName;
      Submit.GetComponentInChildren<Text>().text = CurrentSceneInfo.info.apiName;
    }
  }

  void GetGroupList(params object[] parameters)
  {
    try
    {
      GroupList = new List<GroupInfo>();
      SelectedGroup.ClearOptions();
      string text = (string)parameters[1];
      List<GroupInfo> List = Utils.FromJson<List<GroupInfo>>(text);
      foreach (GroupInfo item in List)
      {
        print(item.group_base_info_group_id);
        GroupList.Add(item);
        Dropdown.OptionData option = new Dropdown.OptionData();
        option.text = item.group_base_info_group_id;
        SelectedGroup.options.Add(option);
      }
      if (List.Count > 0)
      {
        SelectedGroup.captionText.text = List[SelectedGroup.value].group_base_info_group_id;
      }
    }
    catch (Exception ex)
    {
      Toast.Show(Utils.t("getGroupListFailed"));
    }
  }

  void GroupGetJoinedCommunityListSDK()
  {
    TIMResult res = TencentIMSDK.GroupGetJoinedCommunityList(Utils.addAsyncStringDataToScreen(GetGroupList));
    print($"GroupGetJoinedCommunityListSDK {res}");
  }

  void GroupGetTopicInfoListSDK()
  {
    print(GroupList[SelectedGroup.value].group_base_info_group_id);
    string group_base_info_group_id = GroupList[SelectedGroup.value].group_base_info_group_id;
    List<string> topicIDs = string.IsNullOrEmpty(TopicID.text) ? null : new List<string>(TopicID.text.Split(','));
    TIMResult res = TencentIMSDK.GroupGetTopicInfoList(group_base_info_group_id, topicIDs, Utils.addAsyncStringDataToScreen(GetResult));
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