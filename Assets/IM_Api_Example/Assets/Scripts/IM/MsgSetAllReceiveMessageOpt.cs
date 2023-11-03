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
public class MsgSetAllReceiveMessageOpt : MonoBehaviour
{
  string[] Labels = new string[] { "MsgRecvOptLabel" };
  public Text Header;
  public InputField Input;
  public Dropdown SelectedRecvOpt;
  public Text Result;
  public Button Submit;
  public Button Copy;
  void Start()
  {
    foreach (string label in Labels)
    {
      GameObject.Find(label).GetComponent<Text>().text = Utils.t(label);
    }
    Header = GameObject.Find("HeaderText").GetComponent<Text>();
    Input = GameObject.Find("Input").GetComponent<InputField>();
    SelectedRecvOpt = GameObject.Find("SelectedRecvOpt").GetComponent<Dropdown>();
    foreach (string name in Enum.GetNames(typeof(TIMReceiveMessageOpt)))
    {
      Dropdown.OptionData option = new Dropdown.OptionData();
      option.text = name;
      SelectedRecvOpt.options.Add(option);
    }
    Result = GameObject.Find("ResultText").GetComponent<Text>();
    Submit = GameObject.Find("Submit").GetComponent<Button>();
    Copy = GameObject.Find("Copy").GetComponent<Button>();
    Submit.onClick.AddListener(MsgSetGroupReceiveMessageOptSDK);
    Copy.GetComponentInChildren<Text>().text = Utils.t("Copy");
    Copy.onClick.AddListener(CopyText);
    if (CurrentSceneInfo.info != null)
    {
      Header.text = Utils.IsCn() ? CurrentSceneInfo.info.apiText + " " + CurrentSceneInfo.info.apiName : CurrentSceneInfo.info.apiName;
      Submit.GetComponentInChildren<Text>().text = CurrentSceneInfo.info.apiName;
    }
  }

  void MsgSetGroupReceiveMessageOptSDK()
  {
    TIMResult res = TencentIMSDK.MsgSetAllReceiveMessageOpt((TIMReceiveMessageOpt) SelectedRecvOpt.value, 17,5,0,180,Utils.addAsyncStringDataToScreen(GetResult));
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