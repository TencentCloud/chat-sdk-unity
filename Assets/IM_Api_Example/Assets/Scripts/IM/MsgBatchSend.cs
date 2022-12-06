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
public class MsgBatchSend : MonoBehaviour
{
  string[] Labels = new string[] { "MessageLabel", "SelectUserLabel", "SelectPriorityLabel", "IsOnlineLabel", "IsUnreadLabel", "needReceiptLabel" };
  public Text Header;
  public InputField Input;
  public Dropdown SelectedPriority;
  public Toggle IsOnline;
  public Toggle IsUnread;
  public Toggle Receipt;
  public Text Result;
  public Button Submit;
  public Button Copy;
  HashSet<string> SelectedUser = new HashSet<string>();
  List<FriendProfile> UserList = new List<FriendProfile>();
  void Start()
  {
    foreach (string label in Labels)
    {
      GameObject.Find(label).GetComponent<Text>().text = Utils.t(label);
    }
    Header = GameObject.Find("HeaderText").GetComponent<Text>();
    Input = GameObject.Find("Message").GetComponent<InputField>();
    SelectedPriority = GameObject.Find("Priority").GetComponent<Dropdown>();
    foreach (string name in Enum.GetNames(typeof(TIMMsgPriority)))
    {
      Dropdown.OptionData option = new Dropdown.OptionData();
      option.text = name;
      SelectedPriority.options.Add(option);
    }
    IsOnline = GameObject.Find("Online").GetComponent<Toggle>();
    IsUnread = GameObject.Find("Unread").GetComponent<Toggle>();
    Receipt = GameObject.Find("Receipt").GetComponent<Toggle>();
    Result = GameObject.Find("ResultText").GetComponent<Text>();
    Submit = GameObject.Find("Submit").GetComponent<Button>();
    Copy = GameObject.Find("Copy").GetComponent<Button>();
    Submit.onClick.AddListener(MsgBatchSendSDK);
    Copy.GetComponentInChildren<Text>().text = Utils.t("Copy");
    Copy.onClick.AddListener(CopyText);
    if (CurrentSceneInfo.info != null)
    {
      Header.text = Utils.IsCn() ? CurrentSceneInfo.info.apiText + " " + CurrentSceneInfo.info.apiName : CurrentSceneInfo.info.apiName;
      Submit.GetComponentInChildren<Text>().text = CurrentSceneInfo.info.apiName;
    }
    FriendshipGetFriendProfileList();
  }

  void FriendshipGetFriendProfileList()
  {
    var cb = Utils.addAsyncStringDataToScreen(SetUserList);
    TIMResult res = TencentIMSDK.FriendshipGetFriendProfileList(cb);
  }

  void SetUserList(params object[] parameters)
  {
    try
    {
      string text = (string)parameters[1];
      print(text);
      List<FriendProfile> List = Utils.FromJson<List<FriendProfile>>(text);
      UserList.AddRange(List);
      GenerateToggle();
    }
    catch (Exception ex)
    {
      print(ex);
      Toast.Show(Utils.t("getFriendListFailed"));
    }
  }

  void GenerateToggle()
  {
    var Parent = GameObject.Find("ToggleContent");
    var Toggler = GameObject.Find("Toggler").GetComponent<Toggle>();
    foreach (FriendProfile user in UserList)
    {
      var obj = Instantiate(Toggler, Parent.transform);
      obj.GetComponentInChildren<Text>().text = "userID:" + user.friend_profile_identifier;
      obj.isOn = false;
      obj.onValueChanged.AddListener(delegate
    {
      ToggleValueChanged(obj);
    });
    }
  }

  void ToggleValueChanged(Toggle change)
  {
    string userID = change.GetComponentInChildren<Text>().text.Split(':')[1];
    if (change.isOn)
    {
      SelectedUser.Add(userID);
    }
    else
    {
      SelectedUser.Remove(userID);
    }
  }

  void MsgBatchSendSDK()
  {
    var message = new Message
    {
      message_conv_type = TIMConvType.kTIMConv_C2C,
      message_cloud_custom_str = "unity local text data",
      message_elem_array = new List<Elem>{new Elem
      {
        elem_type = TIMElemType.kTIMElem_Text,
        text_elem_content = Input.text
      }},
      message_need_read_receipt = Receipt.isOn,
      message_priority = (TIMMsgPriority)SelectedPriority.value,
      message_is_excluded_from_unread_count = IsUnread.isOn,
      message_is_online_msg = IsOnline.isOn
    };
    List<string> user_list = new List<string>(SelectedUser);
    var param = new MsgBatchSendParam
    {
      msg_batch_send_param_identifier_array = user_list,
      msg_batch_send_param_msg = message
    };
    TIMResult res = TencentIMSDK.MsgBatchSend(param, Utils.addAsyncStringDataToScreen(GetResult));
    Result.text = Utils.SynchronizeResult(res);
    print(IsOnline.isOn);
    print(IsUnread.isOn);
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