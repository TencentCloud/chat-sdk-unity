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
public class PinGroupMessage : MonoBehaviour
{
  public Text Header;
  public InputField GroupID;
  public InputField MessageID;
  public Toggle IsPin;
  public Text Result;
  public Button Submit;
  public Button Copy;
  void Start()
  {
    Header = GameObject.Find("HeaderText").GetComponent<Text>();
    GroupID = GameObject.Find("GroupID").GetComponent<InputField>();
    MessageID = GameObject.Find("MessageID").GetComponent<InputField>();
    IsPin = GameObject.Find("IsPin").GetComponent<Toggle>();
    Result = GameObject.Find("ResultText").GetComponent<Text>();
    Submit = GameObject.Find("Submit").GetComponent<Button>();
    Copy = GameObject.Find("Copy").GetComponent<Button>();
    Submit.onClick.AddListener(MsgFindMessage);
    Copy.GetComponentInChildren<Text>().text = Utils.t("Copy");
    Copy.onClick.AddListener(CopyText);
    if (CurrentSceneInfo.info != null)
    {
      Header.text = Utils.IsCn() ? CurrentSceneInfo.info.apiText + " " + CurrentSceneInfo.info.apiName : CurrentSceneInfo.info.apiName;
      Submit.GetComponentInChildren<Text>().text = CurrentSceneInfo.info.apiName;
    }
  }

  void MsgFindMessage()
  {
    TIMResult res = TencentIMSDK.MsgFindMessages(new List<string> { MessageID.text }, Utils.addAsyncStringDataToScreen(GetMessage));
    print("MsgFindMessage: " + res);
  }

  void GetMessage(params object[] parameters)
  {
    string text = (string)parameters[1];
    print(text);
    var list = Utils.FromJson<List<Message>>(text);
    if (list.Count > 0)
    {
      PinGroupMessageSDK(list[0]);
    }
  }

  void PinGroupMessageSDK(Message msg)
  {
    TIMResult res = TencentIMSDK.PinGroupMessage(GroupID.text, msg, IsPin.isOn, Utils.addAsyncStringDataToScreen(GetResult));
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