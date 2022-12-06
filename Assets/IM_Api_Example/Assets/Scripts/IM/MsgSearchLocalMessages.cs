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
public class MsgSearchLocalMessages : MonoBehaviour
{
  string[] Labels = new string[] { "KeywordLabel", "KeywordPlaceHolder", "SelectElemTypeLabel" };
  public Text Header;
  public InputField Input;
  public InputField ConvID;
  public Dropdown ConvType;
  public InputField Time;
  public InputField Period;
  public InputField PageIndex;
  public InputField PageSize;
  public Dropdown MatchType;
  public InputField UserIDs;
  public Text Result;
  public Button Submit;
  public Button Copy;
  List<string> elemTypes = new List<string>();
  void Start()
  {
    foreach (string label in Labels)
    {
      GameObject.Find(label).GetComponent<Text>().text = Utils.t(label);
    }
    GameObject.Find("KeywordPlaceHolder2").GetComponent<Text>().text = Utils.t("KeywordPlaceHolder");
    Header = GameObject.Find("HeaderText").GetComponent<Text>();
    Input = GameObject.Find("Keyword").GetComponent<InputField>();
    ConvID = GameObject.Find("ConvID").GetComponent<InputField>();
    ConvType = GameObject.Find("ConvType").GetComponent<Dropdown>();
    foreach (string name in Enum.GetNames(typeof(TIMConvType)))
    {
      Dropdown.OptionData option = new Dropdown.OptionData();
      option.text = name;
      ConvType.options.Add(option);
    }
    Time = GameObject.Find("Time").GetComponent<InputField>();
    Period = GameObject.Find("Period").GetComponent<InputField>();
    PageIndex = GameObject.Find("PageIndex").GetComponent<InputField>();
    PageSize = GameObject.Find("PageSize").GetComponent<InputField>();
    MatchType = GameObject.Find("MatchType").GetComponent<Dropdown>();
    foreach (string name in Enum.GetNames(typeof(TIMKeywordListMatchType)))
    {
      Dropdown.OptionData option = new Dropdown.OptionData();
      option.text = name;
      MatchType.options.Add(option);
    }
    UserIDs = GameObject.Find("UserIDs").GetComponent<InputField>();
    Result = GameObject.Find("ResultText").GetComponent<Text>();
    Submit = GameObject.Find("Submit").GetComponent<Button>();
    Copy = GameObject.Find("Copy").GetComponent<Button>();
    Submit.onClick.AddListener(MsgSearchLocalMessagesSDK);
    Copy.GetComponentInChildren<Text>().text = Utils.t("Copy");
    Copy.onClick.AddListener(CopyText);
    if (CurrentSceneInfo.info != null)
    {
      Header.text = Utils.IsCn() ? CurrentSceneInfo.info.apiText + " " + CurrentSceneInfo.info.apiName : CurrentSceneInfo.info.apiName;
      Submit.GetComponentInChildren<Text>().text = CurrentSceneInfo.info.apiName;
    }
    GenerateToggle();
  }

  void ToggleValueChanged(Toggle change)
  {
    string type = change.GetComponentInChildren<Text>().text;
    if (change.isOn)
    {
      elemTypes.Add(type);
    }
    else
    {
      elemTypes.Remove(type);
    }
  }

  void GenerateToggle()
  {
    var Parent = GameObject.Find("ToggleContent");
    var Toggler = GameObject.Find("Toggler").GetComponent<Toggle>();
    var obj2 = Instantiate(Toggler, Parent.transform);
    obj2.GetComponentInChildren<Text>().text = "kTIMElem_All";
    obj2.isOn = false;
    obj2.onValueChanged.AddListener(delegate
  {
    ToggleValueChanged(obj2);
  });
    foreach (string name in Enum.GetNames(typeof(TIMElemType)))
    {
      if (name != "kTIMElem_GroupTips" && name != "kTIMElem_Face")
      {
        var obj = Instantiate(Toggler, Parent.transform);
        obj.GetComponentInChildren<Text>().text = name;
        obj.isOn = false;
        obj.onValueChanged.AddListener(delegate
      {
        ToggleValueChanged(obj);
      });
      }
    }
  }

  List<TIMElemType> GetAllElemType(List<string> strList)
  {
    if (strList.Contains("kTIMElem_All"))
    {
      return null;
    }
    List<TIMElemType> result = new List<TIMElemType>();
    foreach (string str in strList)
    {
      result.Add((TIMElemType) Enum.Parse(typeof(TIMElemType), str));
    }
    return result;
  }
  void MsgSearchLocalMessagesSDK()
  {
    MessageSearchParam param = new MessageSearchParam
    {
      msg_search_param_keyword_array = string.IsNullOrEmpty(Input.text) ? null : new List<string>(Input.text.Split(',')),
      msg_search_param_message_type_array = GetAllElemType(elemTypes),
      msg_search_param_conv_id = ConvID.text,
      msg_search_param_conv_type = (TIMConvType)ConvType.value,
      msg_search_param_search_time_position = string.IsNullOrEmpty(Time.text) ? 0 : Convert.ToUInt64(Time.text),
      msg_search_param_search_time_period = string.IsNullOrEmpty(Period.text) ? 0 : Convert.ToUInt64(Period.text),
      msg_search_param_page_index = string.IsNullOrEmpty(PageIndex.text) ? 0 : Convert.ToUInt32(PageIndex.text),
      msg_search_param_page_size = string.IsNullOrEmpty(PageSize.text) ? 0 : Convert.ToUInt32(PageSize.text),
      msg_search_param_keyword_list_match_type = (TIMKeywordListMatchType)MatchType.value,
      msg_search_param_send_indentifier_array = string.IsNullOrEmpty(UserIDs.text) ? null : new List<string>(UserIDs.text.Split(','))
    };
    TIMResult res = TencentIMSDK.MsgSearchLocalMessages(param, Utils.addAsyncStringDataToScreen(GetResult));
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