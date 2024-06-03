using UnityEngine;
using UnityEngine.UI;
using com.tencent.im.unity.demo.types;
using com.tencent.imsdk.unity;
using com.tencent.imsdk.unity.types;
using com.tencent.imsdk.unity.enums;
using System;
using System.Linq;
using com.tencent.im.unity.demo.utils;
using EasyUI.Toast;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;
using com.tencent.imsdk.unity.callback;
public class MsgGetMsgList : MonoBehaviour
{
  public Text Header;

  public Text LastMessageID;
  public Dropdown SelectedFriend;
  public Dropdown SelectedGroup;
  public Text Result;
  public Button Submit;
  public Button Copy;
  private List<string> GroupList;
  private List<string> FriendList;
  private Message LastMessage;
  public List<string> ResultText;
  private string Data;
  string[] Labels = new string[] { "SelectFriendLabel", "SelectGroupLabel" };
  void Start()
  {
    foreach (string label in Labels)
    {
      GameObject.Find(label).GetComponent<Text>().text = Utils.t(label);
    }
    GroupGetJoinedGroupListSDK();
    FriendshipGetFriendProfileListSDK();
    Header = GameObject.Find("HeaderText").GetComponent<Text>();
    LastMessageID = GameObject.Find("LastMessageID").GetComponent<Text>();
    SelectedFriend = GameObject.Find("Friend").GetComponent<Dropdown>();
    SelectedGroup = GameObject.Find("Group").GetComponent<Dropdown>();
    SelectedGroup.onValueChanged.AddListener(delegate
    {
      GroupDropdownValueChanged(SelectedGroup);
    });
    SelectedFriend.onValueChanged.AddListener(delegate
    {
      FriendDropdownValueChanged(SelectedFriend);
    });
    Result = GameObject.Find("ResultText").GetComponent<Text>();
    Submit = GameObject.Find("Submit").GetComponent<Button>();
    Copy = GameObject.Find("Copy").GetComponent<Button>();
    Copy.GetComponentInChildren<Text>().text = Utils.t("Copy");
    Submit.onClick.AddListener(MsgGetMsgListSDK);
    Copy.onClick.AddListener(CopyText);
    if (CurrentSceneInfo.info != null)
    {
      Header.text = Utils.IsCn() ? CurrentSceneInfo.info.apiText + " " + CurrentSceneInfo.info.apiName : CurrentSceneInfo.info.apiName;
      Submit.GetComponentInChildren<Text>().text = CurrentSceneInfo.info.apiName;
    }
  }

  void GroupDropdownValueChanged(Dropdown change)
  {
    if (change.value > 0)
    {
      if (SelectedFriend.value > 0) {
        LastMessage = null;
        LastMessageID.text = "";
        SelectedFriend.value = 0;
      }
    }
  }

  void FriendDropdownValueChanged(Dropdown change)
  {
    if (change.value > 0)
    {
      if (SelectedGroup.value > 0) {
        LastMessage = null;
        LastMessageID.text = "";
        SelectedGroup.value = 0;
      }
    }
  }

  void GetGroupList(params object[] parameters)
  {
    try
    {
      GroupList = new List<string>();
      SelectedGroup.ClearOptions();
      string text = (string)parameters[1];
      List<GroupBaseInfo> List = Utils.FromJson<List<GroupBaseInfo>>(text);
      Dropdown.OptionData option = new Dropdown.OptionData();
      GroupList.Add("");
      option.text = "";
      SelectedGroup.options.Add(option);
      foreach (GroupBaseInfo item in List)
      {
        print(item.group_base_info_group_id);
        GroupList.Add(item.group_base_info_group_id);
        option = new Dropdown.OptionData();
        option.text = item.group_base_info_group_id;
        SelectedGroup.options.Add(option);
      }
    }
    catch (Exception ex)
    {
      Toast.Show(Utils.t("getGroupListFailed"));
    }
  }

  void GetFriendList(params object[] parameters)
  {
    try
    {
      FriendList = new List<string>();
      SelectedFriend.ClearOptions();
      string text = (string)parameters[1];
      List<FriendProfile> List = Utils.FromJson<List<FriendProfile>>(text);
      Dropdown.OptionData option = new Dropdown.OptionData();
      FriendList.Add("");
      option.text = "";
      SelectedFriend.options.Add(option);
      foreach (FriendProfile item in List)
      {
        print(item.friend_profile_identifier);
        FriendList.Add(item.friend_profile_identifier);
        option = new Dropdown.OptionData();
        option.text = item.friend_profile_identifier;
        SelectedFriend.options.Add(option);
      }
    }
    catch (Exception ex)
    {
      Toast.Show(Utils.t("getFriendListFailed"));
    }
  }

  void GroupGetJoinedGroupListSDK()
  {
    TIMResult res = TencentIMSDK.GroupGetJoinedGroupList(Utils.addAsyncStringDataToScreen(GetGroupList));
    print($"GroupGetJoinedGroupListSDK {res}");
  }

  void FriendshipGetFriendProfileListSDK()
  {
    TIMResult res = TencentIMSDK.FriendshipGetFriendProfileList(Utils.addAsyncStringDataToScreen(GetFriendList));
    print($"FriendshipGetFriendProfileListSDK {res}");
  }

  void MsgGetMsgListSDK()
  {
    var get_message_list_param = new MsgGetMsgListParam
    {
      msg_getmsglist_param_count = 20,
      msg_getmsglist_param_is_ramble = true
    };
    if (LastMessage != null)
    {
      print("lastmessage not null");
      print("notnull " + JsonUtility.ToJson(LastMessage));
      // get_message_list_param.msg_getmsglist_param_last_msg = LastMessage;
      get_message_list_param.msg_last_msg_id = LastMessage.message_msg_id;
      print("client time: "+LastMessage.message_client_time);
    }
    var Parent = GameObject.Find("ResultPanel");
    foreach (Transform child in Parent.transform)
    {
      GameObject.Destroy(child.gameObject);
    }
    Result = Instantiate(Result, Parent.transform);
    Result.enabled = true;
    if (SelectedGroup.value > 0)
    {
      print(GroupList[SelectedGroup.value]);
      // get_message_list_param.msg_last_msg_id = "123123";
      // TIMResult res = TencentIMSDK.MsgGetMsgList(GroupList[SelectedGroup.value], TIMConvType.kTIMConv_Group, get_message_list_param, Utils.addAsyncStringDataToScreen(GetResult));
      TIMResult res = TencentIMSDK.MsgGetMsgList(GroupList[SelectedGroup.value], TIMConvType.kTIMConv_Group, get_message_list_param,(int code, string desc, List<Message> callbackData, string user_data) =>{
        print("code "+ code + desc);
        print("callbackdata "+ callbackData);
        print("data "+ JsonConvert.SerializeObject(callbackData));
        print("lenth "+ callbackData.Count);
        LastMessage = callbackData[callbackData.Count - 1];
        LastMessageID.text = callbackData[callbackData.Count - 1].message_msg_id;
        
        Result.text+=callbackData[0].message_client_time +"last time" +callbackData[callbackData.Count - 1].message_client_time +"\n";
      });
      Result.text = Utils.SynchronizeResult(res);
    }
    else if (SelectedFriend.value > 0)
    {
      print(FriendList[SelectedFriend.value]);
      print("lastmessage "+ get_message_list_param);
      print("lastmessage json "+ JsonConvert.SerializeObject(get_message_list_param));
      
      TIMResult res = TencentIMSDK.MsgGetMsgList(FriendList[SelectedFriend.value], TIMConvType.kTIMConv_C2C, get_message_list_param, Utils.addAsyncStringDataToScreen(GetResult));
    //   TIMResult res = TencentIMSDK.MsgGetMsgList(FriendList[SelectedFriend.value], TIMConvType.kTIMConv_C2C, get_message_list_param,(int code, string desc, List<Message> callbackData, string user_data) =>{
    //   print("callbackdata "+ callbackData);
    //   print("data "+ JsonConvert.SerializeObject(callbackData));
    //   print("lenth "+ callbackData.Count);
    //   LastMessage = callbackData[callbackData.Count - 1];
    //   LastMessageID.text = callbackData[callbackData.Count - 1].message_msg_id;
    //   // Result.text+=code+"data" + JsonConvert.SerializeObject(callbackData);
    // });
      Result.text = Utils.SynchronizeResult(res);
    }
  }

    void GenerateResultText()
  {
    var Parent = GameObject.Find("ResultPanel");
    foreach (string resultText in ResultText)
    {
      var obj = Instantiate(Result, Parent.transform);
      obj.text = resultText;
    }
  }

  void GetResult(params object[] parameters)
  {
    List<Message> messages = Utils.FromJson<List<Message>>((string)parameters[1]);
    if (messages.Count > 0)
    {
      LastMessage = messages[messages.Count - 1];
      LastMessageID.text = messages[messages.Count - 1].message_msg_id;
      print("有lastMsg "+ LastMessage.message_msg_id);
    }
    else
    {
      LastMessage = null;
      LastMessageID.text = "";
    }
    ResultText = new List<string>();
    // ArgumentException: Mesh can not have more than 65000 vertices
    // Deal with a single Text cannot render too many words issue
    string CallbackData = (string)parameters[0];
    string[] DataList = CallbackData.Split('\n');
    int count = 0;
    while (count < DataList.Length)
    {
      // Every 100 lines render a new Text
      int end = count + 100;
      if (end > DataList.Length)
      {
        end = DataList.Length;
      }
      string[] textList = DataList.Skip(count).Take(end - count).ToArray();
      ResultText.Add(string.Join("\n", textList));
      count = end;
    }
    Data = (string)parameters[0];
    GenerateResultText();
  }

  void CopyText()
  {
    Utils.Copy(Data);
  }
  void OnApplicationQuit()
  {
    TencentIMSDK.Uninit();
  }
}