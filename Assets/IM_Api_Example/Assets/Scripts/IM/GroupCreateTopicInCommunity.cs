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
public class GroupCreateTopicInCommunity : MonoBehaviour
{
  string[] Labels = new string[] { "SelectGroupLabel", "TopicNameLabel", "TopicIntroLabel", "TopicNotificationLabel", "TopicFaceUrlLabel", "IsTopicAllMutedLabel", "TopicSelfMuteTimeLabel", "TopicCustomStrLabel", "TopicRecvOptLabel", "TopicDraftLabel" };
  public Text Header;
  public Dropdown SelectedGroup;
  public InputField TopicName;
  public InputField TopicIntro;
  public InputField TopicNotification;
  public InputField TopicFace;
  public Toggle IsAllMuted;
  public InputField TopicMuteTime;
  public InputField CustomKey;
  public Dropdown SelectedRecvOpt;
  public InputField TopicDraft;
  public Text Result;
  public Button Submit;
  public Button Copy;
  List<string> groupIDList;
  void Start()
  {
    foreach (string label in Labels)
    {
      GameObject.Find(label).GetComponent<Text>().text = Utils.t(label);
    }
    Header = GameObject.Find("HeaderText").GetComponent<Text>();
    SelectedGroup = GameObject.Find("GroupID").GetComponent<Dropdown>();
    TopicName = GameObject.Find("TopicName").GetComponent<InputField>();
    TopicIntro = GameObject.Find("TopicIntro").GetComponent<InputField>();
    TopicNotification = GameObject.Find("TopicNotification").GetComponent<InputField>();
    TopicFace = GameObject.Find("TopicFace").GetComponent<InputField>();
    IsAllMuted = GameObject.Find("IsAllMuted").GetComponent<Toggle>();
    TopicMuteTime = GameObject.Find("TopicMuteTime").GetComponent<InputField>();
    CustomKey = GameObject.Find("CustomKey").GetComponent<InputField>();
    SelectedRecvOpt = GameObject.Find("SelectedRecvOpt").GetComponent<Dropdown>();
    foreach (string name in Enum.GetNames(typeof(TIMReceiveMessageOpt)))
    {
      Dropdown.OptionData option = new Dropdown.OptionData();
      option.text = name;
      SelectedRecvOpt.options.Add(option);
    }
    TopicDraft = GameObject.Find("TopicDraft").GetComponent<InputField>();
    Result = GameObject.Find("ResultText").GetComponent<Text>();
    Submit = GameObject.Find("Submit").GetComponent<Button>();
    Copy = GameObject.Find("Copy").GetComponent<Button>();
    Submit.onClick.AddListener(GroupCreateTopicInCommunitySDK);
    Copy.GetComponentInChildren<Text>().text = Utils.t("Copy");
    Copy.onClick.AddListener(CopyText);
    if (CurrentSceneInfo.info != null)
    {
      Header.text = Utils.IsCn() ? CurrentSceneInfo.info.apiText + " " + CurrentSceneInfo.info.apiName : CurrentSceneInfo.info.apiName;
      Submit.GetComponentInChildren<Text>().text = CurrentSceneInfo.info.apiName;
    }
    GroupGetJoinedCommunityListSDK();
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

  void GroupCreateTopicInCommunitySDK()
  {
    if (groupIDList.Count < 1) return;
    GroupTopicInfo param = new GroupTopicInfo {
      group_topic_info_topic_name = TopicName.text,
      group_topic_info_introduction = TopicIntro.text,
      group_topic_info_notification = TopicNotification.text,
      group_topic_info_topic_face_url = TopicFace.text,
      group_topic_info_is_all_muted = IsAllMuted.isOn,
      group_topic_info_self_mute_time = Convert.ToUInt32(TopicMuteTime.text != null ? TopicMuteTime.text : "0"),
      group_topic_info_custom_string = CustomKey.text,
      group_topic_info_recv_opt = (TIMReceiveMessageOpt) SelectedRecvOpt.value,
      group_topic_info_draft_text = TopicDraft.text
    };
    TIMResult res = TencentIMSDK.GroupCreateTopicInCommunity(groupIDList[SelectedGroup.value], param, Utils.addAsyncStringDataToScreen(GetResult));
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