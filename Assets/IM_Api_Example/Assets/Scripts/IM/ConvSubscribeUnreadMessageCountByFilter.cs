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
public class ConvSubscribeUnreadMessageCountByFilter : MonoBehaviour
{
  public Text Header;
  public Dropdown SelectedConvType;
  public Text Result;
  public Dropdown SelectedMarkType;
  public InputField GroupName;
  public Button Submit;
  public Button Copy;
  public int[] EnumConvMarkType = (int[])Enum.GetValues(typeof(TIMConversationMarkType));
  void Start()
  {
    Header = GameObject.Find("HeaderText").GetComponent<Text>();
    SelectedConvType = GameObject.Find("Dropdown").GetComponent<Dropdown>();
    foreach (string name in Enum.GetNames(typeof(TIMConvType)))
    {
      Dropdown.OptionData option = new Dropdown.OptionData();
      option.text = name;
      SelectedConvType.options.Add(option);
    }
    SelectedMarkType = GameObject.Find("MarkType").GetComponent<Dropdown>();
    foreach (string name in Enum.GetNames(typeof(TIMConversationMarkType)))
    {
      Dropdown.OptionData option = new Dropdown.OptionData();
      option.text = name;
      SelectedMarkType.options.Add(option);
    }
    GroupName = GameObject.Find("GroupName").GetComponent<InputField>();
    Result = GameObject.Find("ResultText").GetComponent<Text>();
    Submit = GameObject.Find("Submit").GetComponent<Button>();
    Copy = GameObject.Find("Copy").GetComponent<Button>();
    Copy.GetComponentInChildren<Text>().text = Utils.t("Copy");
    Submit.onClick.AddListener(ConvGetConversationListByFilterSDK);
    Copy.onClick.AddListener(CopyText);
    SelectedConvType.interactable = true;
    if (CurrentSceneInfo.info != null)
    {
      Header.text = Utils.IsCn() ? CurrentSceneInfo.info.apiText + " " + CurrentSceneInfo.info.apiName : CurrentSceneInfo.info.apiName;
      Submit.GetComponentInChildren<Text>().text = CurrentSceneInfo.info.apiName;
    }
  }

  void ConvGetConversationListByFilterSDK()
  {
    ConversationListFilter filter = new ConversationListFilter
    {
      conversation_list_filter_conv_type = (TIMConvType)SelectedConvType.value,
      conversation_list_filter_mark_type = (TIMConversationMarkType)EnumConvMarkType[SelectedMarkType.value],
      conversation_list_filter_conversation_group = GroupName.text
    };
    TIMResult res = TencentIMSDK.ConvSubscribeUnreadMessageCountByFilter(filter);
    Result.text = Utils.SynchronizeResult(res);
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