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
public class SignalingInvite : MonoBehaviour
{
  public Text Header;
  public InputField UserID;
  public InputField Timeout;
  public Toggle OnlineUserOnly;
  public Text Result;
  public Button Submit;
  public Button Copy;
  void Start()
  {
    Header = GameObject.Find("HeaderText").GetComponent<Text>();
    UserID = GameObject.Find("UserID").GetComponent<InputField>();
    Timeout = GameObject.Find("Timeout").GetComponent<InputField>();
    OnlineUserOnly = GameObject.Find("OnlineUserOnly").GetComponent<Toggle>();
    Result = GameObject.Find("ResultText").GetComponent<Text>();
    Submit = GameObject.Find("Submit").GetComponent<Button>();
    Copy = GameObject.Find("Copy").GetComponent<Button>();
    Submit.onClick.AddListener(SignalingInviteSDK);
    Copy.GetComponentInChildren<Text>().text = Utils.t("Copy");
    Copy.onClick.AddListener(CopyText);
    if (CurrentSceneInfo.info != null)
    {
      Header.text = Utils.IsCn() ? CurrentSceneInfo.info.apiText + " " + CurrentSceneInfo.info.apiName : CurrentSceneInfo.info.apiName;
      Submit.GetComponentInChildren<Text>().text = CurrentSceneInfo.info.apiName;
    }
  }

  void SignalingInviteSDK()
  { 
    int timeout = 5;
    int inputVal = 5;
    if (!string.IsNullOrWhiteSpace(Timeout.text) && int.TryParse(Timeout.text, out inputVal))
    {
      timeout = inputVal;
    }

    StringBuilder inviteIDBuffer = new StringBuilder(128);

    TIMResult res = TencentIMSDK.SignalingInvite(UserID.text, "", OnlineUserOnly.isOn, new OfflinePushConfig(), timeout, inviteIDBuffer, Utils.addAsyncStringDataToScreen(GetResult));
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