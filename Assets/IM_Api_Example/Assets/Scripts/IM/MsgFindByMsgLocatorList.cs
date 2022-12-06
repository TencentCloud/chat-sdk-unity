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
public class MsgFindByMsgLocatorList : MonoBehaviour
{
  string[] Labels = new string[] { "SelectConvLabel" };

  public Text Header;
  public Dropdown SelectedConv;
  public InputField Time;
  public InputField Seq;
  public InputField Rand;
  public InputField MsgID;
  public Toggle IsSelf;
  public Toggle IsRevoked;
  public Text Result;
  public Button Submit;
  public Button Copy;

  private List<ConvInfo> ConvList;
  void Start()
  {
    foreach (string label in Labels)
    {
      GameObject.Find(label).GetComponent<Text>().text = Utils.t(label);
    }
    ConvGetConvListSDK();
    Header = GameObject.Find("HeaderText").GetComponent<Text>();
    SelectedConv = GameObject.Find("Dropdown").GetComponent<Dropdown>();
    Time = GameObject.Find("Time").GetComponent<InputField>();
    Seq = GameObject.Find("Seq").GetComponent<InputField>();
    Rand = GameObject.Find("Rand").GetComponent<InputField>();
    MsgID = GameObject.Find("MsgID").GetComponent<InputField>();
    IsSelf = GameObject.Find("IsSelf").GetComponent<Toggle>();
    IsRevoked = GameObject.Find("IsRevoked").GetComponent<Toggle>();
    Result = GameObject.Find("ResultText").GetComponent<Text>();
    Submit = GameObject.Find("Submit").GetComponent<Button>();
    Copy = GameObject.Find("Copy").GetComponent<Button>();
    Copy.GetComponentInChildren<Text>().text = Utils.t("Copy");
    Submit.onClick.AddListener(MsgFindByMsgLocatorListSDK);
    Copy.onClick.AddListener(CopyText);
    SelectedConv.interactable = true;
    if (CurrentSceneInfo.info != null)
    {
      Header.text = Utils.IsCn() ? CurrentSceneInfo.info.apiText + " " + CurrentSceneInfo.info.apiName : CurrentSceneInfo.info.apiName;
      Submit.GetComponentInChildren<Text>().text = CurrentSceneInfo.info.apiName;
    }
  }

  void GetConvList(params object[] parameters)
  {
    try
    {
      ConvList = new List<ConvInfo>();
      SelectedConv.ClearOptions();
      string text = (string)parameters[1];
      List<ConvInfo> List = Utils.FromJson<List<ConvInfo>>(text);
      foreach (ConvInfo item in List)
      {
        print(item.conv_id);
        ConvList.Add(item);
        Dropdown.OptionData option = new Dropdown.OptionData();
        option.text = item.conv_id;
        SelectedConv.options.Add(option);
      }
      SelectedConv.value = 0;
      if (List.Count > 0)
      {
        SelectedConv.captionText.text = List[0].conv_id;
      }
      else
      {
        SelectedConv.captionText.text = "";
      }
    }
    catch (Exception ex)
    {
      Toast.Show(Utils.t("getConvListFailed"));
    }
  }

  void ConvGetConvListSDK()
  {
    TIMResult res = TencentIMSDK.ConvGetConvList(Utils.addAsyncStringDataToScreen(GetConvList));
    print($"ConvGetConvListSDK {res}");
  }

  void MsgFindByMsgLocatorListSDK()
  {
    if (ConvList.Count < 1)
    {
      return;
    }
    print(ConvList[SelectedConv.value].conv_id);
    string conv_id = ConvList[SelectedConv.value].conv_id;
    TIMConvType conv_type = ConvList[SelectedConv.value].conv_type;
    List<MsgLocator> param = new List<MsgLocator>();
    MsgLocator locator = new MsgLocator
    {
      message_locator_conv_id = conv_id,
      message_locator_conv_type = conv_type,
      message_locator_is_revoked = IsRevoked.isOn,
      message_locator_is_self = IsSelf.isOn,
    };
    if (!string.IsNullOrEmpty(Time.text)) {
      locator.message_locator_time = Convert.ToUInt64(Time.text);
    }
    if (!string.IsNullOrEmpty(Seq.text)) {
      locator.message_locator_seq = Convert.ToUInt64(Seq.text);
    }
    if (!string.IsNullOrEmpty(Rand.text)) {
      locator.message_locator_rand = Convert.ToUInt64(Rand.text);
    }
    if (!string.IsNullOrEmpty(MsgID.text)) {
      locator.message_locator_unique_id = Convert.ToUInt64(MsgID.text);
    }
    param.Add(locator);
    TIMResult res = TencentIMSDK.MsgFindByMsgLocatorList(conv_id, conv_type, param, Utils.addAsyncStringDataToScreen(GetResult));
    Result.text = Utils.SynchronizeResult(res);
  }

  void GetResult(params object[] parameters)
  {
    ConvGetConvListSDK();
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