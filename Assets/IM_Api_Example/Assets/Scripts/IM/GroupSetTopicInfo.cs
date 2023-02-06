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
public class GroupSetTopicInfo : MonoBehaviour
{
  string[] Labels = new string[] { "SelectGroupLabel", "TopicIDLabel", "TopicNameLabel", "TopicIntroLabel", "TopicNotificationLabel", "TopicFaceUrlLabel", "IsTopicAllMutedLabel", "TopicCustomStrLabel" };
  public Text Header;
  public Dropdown SelectedGroup;
  public Dropdown SelectedTopicID;
  public InputField TopicName;
  public InputField TopicIntro;
  public InputField TopicNotification;
  public InputField TopicFace;
  public Toggle IsAllMuted;
  public InputField CustomKey;
  public Text Result;
  public Button Submit;
  public Button Copy;
  List<string> groupIDList;
  List<string> topicIDList;
  void Start()
  {
    foreach (string label in Labels)
    {
      GameObject.Find(label).GetComponent<Text>().text = Utils.t(label);
    }
    Header = GameObject.Find("HeaderText").GetComponent<Text>();
    SelectedGroup = GameObject.Find("GroupID").GetComponent<Dropdown>();
    SelectedGroup.onValueChanged.AddListener(delegate
    {
      GroupDropdownValueChanged(SelectedGroup);
    });
    SelectedTopicID = GameObject.Find("TopicID").GetComponent<Dropdown>();
    TopicName = GameObject.Find("TopicName").GetComponent<InputField>();
    TopicIntro = GameObject.Find("TopicIntro").GetComponent<InputField>();
    TopicNotification = GameObject.Find("TopicNotification").GetComponent<InputField>();
    TopicFace = GameObject.Find("TopicFace").GetComponent<InputField>();
    IsAllMuted = GameObject.Find("IsAllMuted").GetComponent<Toggle>();
    CustomKey = GameObject.Find("CustomKey").GetComponent<InputField>();
    Result = GameObject.Find("ResultText").GetComponent<Text>();
    Submit = GameObject.Find("Submit").GetComponent<Button>();
    Copy = GameObject.Find("Copy").GetComponent<Button>();
    Submit.onClick.AddListener(GroupSetTopicInfoSDK);
    Copy.GetComponentInChildren<Text>().text = Utils.t("Copy");
    Copy.onClick.AddListener(CopyText);
    if (CurrentSceneInfo.info != null)
    {
      Header.text = Utils.IsCn() ? CurrentSceneInfo.info.apiText + " " + CurrentSceneInfo.info.apiName : CurrentSceneInfo.info.apiName;
      Submit.GetComponentInChildren<Text>().text = CurrentSceneInfo.info.apiName;
    }
    GroupGetJoinedCommunityListSDK();
  }
  void GetTopicInfoList(params object[] parameters)
  {
    try
    {
      topicIDList = new List<string>();
      SelectedTopicID.ClearOptions();
      string text = (string)parameters[1];
      List<GroupTopicInfoResult> List = Utils.FromJson<List<GroupTopicInfoResult>>(text);
      foreach (GroupTopicInfoResult item in List)
      {
        string topic_id = item.group_topic_info_result_topic_info.group_topic_info_topic_id;
        print(topic_id);
        topicIDList.Add(topic_id);
        Dropdown.OptionData option = new Dropdown.OptionData();
        option.text = topic_id;
        SelectedTopicID.options.Add(option);
      }
      if (List.Count > 0)
      {
        SelectedTopicID.captionText.text = List[SelectedTopicID.value].group_topic_info_result_topic_info.group_topic_info_topic_id;
      }
      else
      {
        SelectedTopicID.captionText.text = "";
      }
    }
    catch (Exception ex)
    {
      Toast.Show(Utils.t("getTopicInfoListFailed"));
    }
  }
  void GroupGetTopicInfoListSDK()
  {
    if (groupIDList.Count < 1) return;
    string group_id = groupIDList[SelectedGroup.value];
    TIMResult res = TencentIMSDK.GroupGetTopicInfoList(group_id, null, Utils.addAsyncStringDataToScreen(GetTopicInfoList));
    print($"GroupGetTopicInfoListSDK {res}");
  }
  void GroupDropdownValueChanged(Dropdown change)
  {
    GroupGetTopicInfoListSDK();
  }

  void GetGroupList(params object[] parameters)
  {
    try
    {
      groupIDList = new List<string>();
      SelectedGroup.ClearOptions();
      string text = (string)parameters[1];
      List<GroupInfo> List = Utils.FromJson<List<GroupInfo>>(text);
      foreach (GroupInfo item in List)
      {
        print(item.group_base_info_group_id);
        groupIDList.Add(item.group_base_info_group_id);
        Dropdown.OptionData option = new Dropdown.OptionData();
        option.text = item.group_base_info_group_id;
        SelectedGroup.options.Add(option);
      }
      if (List.Count > 0)
      {
        SelectedGroup.captionText.text = List[SelectedGroup.value].group_base_info_group_id;
      }
      else
      {
        SelectedGroup.captionText.text = "";
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

  void GroupSetTopicInfoSDK()
  {
    if (topicIDList.Count < 1) return;
    GroupTopicInfo param = new GroupTopicInfo
    {
      group_topic_info_topic_id = topicIDList[SelectedTopicID.value],
    };
    var flag = TIMGroupModifyInfoFlag.kTIMGroupModifyInfoFlag_None;
    if (!string.IsNullOrEmpty(TopicName.text))
    {
      param.group_topic_info_topic_name = TopicName.text;
      flag |= TIMGroupModifyInfoFlag.kTIMGroupModifyInfoFlag_Name;
    }
    if (!string.IsNullOrEmpty(TopicIntro.text))
    {
      param.group_topic_info_introduction = TopicIntro.text;
      flag |= TIMGroupModifyInfoFlag.kTIMGroupModifyInfoFlag_Introduction;
    }
    if (!string.IsNullOrEmpty(TopicNotification.text))
    {
      param.group_topic_info_notification = TopicNotification.text;
      flag |= TIMGroupModifyInfoFlag.kTIMGroupModifyInfoFlag_Notification;
    }
    if (!string.IsNullOrEmpty(TopicFace.text))
    {
      param.group_topic_info_topic_face_url = TopicFace.text;
      flag |= TIMGroupModifyInfoFlag.kTIMGroupModifyInfoFlag_FaceUrl;
    }
    if (!string.IsNullOrEmpty(CustomKey.text))
    {
      param.group_topic_info_custom_string = CustomKey.text;
      flag |= TIMGroupModifyInfoFlag.kTIMGroupModifyInfoFlag_Custom;
    }
    param.group_topic_info_is_all_muted = IsAllMuted.isOn;
    flag |= TIMGroupModifyInfoFlag.kTIMGroupModifyInfoFlag_ShutupAll;
    param.group_modify_info_param_modify_flag = flag;
    TIMResult res = TencentIMSDK.GroupSetTopicInfo(param, Utils.addAsyncStringDataToScreen(GetResult));
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