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
public class SignalingInviteInGroup : MonoBehaviour
{
  public Text Header;
  public InputField GroupID;
  public InputField UserIDs;
  public InputField Timeout;
  public Toggle OnlineUserOnly;
  public Text Result;
  public Button Submit;
  public Button Copy;
  void Start()
  {
    string label = "UserIDsInputTips";
    GameObject.Find(label).GetComponent<Text>().text = Utils.t(label);
    Header = GameObject.Find("HeaderText").GetComponent<Text>();
    GroupID = GameObject.Find("GroupID").GetComponent<InputField>();
    UserIDs = GameObject.Find("UserIDs").GetComponent<InputField>();
    Timeout = GameObject.Find("Timeout").GetComponent<InputField>();
    OnlineUserOnly = GameObject.Find("OnlineUserOnly").GetComponent<Toggle>();
    Result = GameObject.Find("ResultText").GetComponent<Text>();
    Submit = GameObject.Find("Submit").GetComponent<Button>();
    Copy = GameObject.Find("Copy").GetComponent<Button>();
    Submit.onClick.AddListener(SignalingInviteInGroupSDK);
    Copy.GetComponentInChildren<Text>().text = Utils.t("Copy");
    Copy.onClick.AddListener(CopyText);
    if (CurrentSceneInfo.info != null)
    {
      Header.text = Utils.IsCn() ? CurrentSceneInfo.info.apiText + " " + CurrentSceneInfo.info.apiName : CurrentSceneInfo.info.apiName;
      Submit.GetComponentInChildren<Text>().text = CurrentSceneInfo.info.apiName;
    }
  }

  void SignalingInviteInGroupSDK()
  { 
    int timeout = 5;
    int inputVal = 5;
    if (!string.IsNullOrWhiteSpace(Timeout.text) && int.TryParse(Timeout.text, out inputVal))
    {
      timeout = inputVal;
    }

    var userIDList = string.IsNullOrEmpty(UserIDs.text) ? null : new List<string>(UserIDs.text.Split(' ', StringSplitOptions.RemoveEmptyEntries));
    StringBuilder inviteIDBuffer = new StringBuilder(128);

    TIMResult res = TencentIMSDK.SignalingInviteInGroup(GroupID.text, userIDList, "", OnlineUserOnly.isOn, timeout, inviteIDBuffer, Utils.addAsyncStringDataToScreen(GetResult));
    Result.text = Utils.SynchronizeResult(res);
    Result.text += "\ninviteID:" + inviteIDBuffer.ToString() + "\n";
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