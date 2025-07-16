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
public class SearchUsers : MonoBehaviour
{
  public Text Header;
  public InputField Input;
  public InputField Count;
  public Text Result;
  public Button Submit;
  public Button Copy;
  void Start()
  {
    string label = "KeywordInputTips";
    GameObject.Find(label).GetComponent<Text>().text = Utils.t(label);
    Header = GameObject.Find("HeaderText").GetComponent<Text>();
    Input = GameObject.Find("Input").GetComponent<InputField>();
    Count = GameObject.Find("Count").GetComponent<InputField>();
    Result = GameObject.Find("ResultText").GetComponent<Text>();
    Submit = GameObject.Find("Submit").GetComponent<Button>();
    Copy = GameObject.Find("Copy").GetComponent<Button>();
    Submit.onClick.AddListener(SearchUsersSDK);
    Copy.GetComponentInChildren<Text>().text = Utils.t("Copy");
    Copy.onClick.AddListener(CopyText);
    if (CurrentSceneInfo.info != null)
    {
      Header.text = Utils.IsCn() ? CurrentSceneInfo.info.apiText + " " + CurrentSceneInfo.info.apiName : CurrentSceneInfo.info.apiName;
      Submit.GetComponentInChildren<Text>().text = CurrentSceneInfo.info.apiName;
    }
  }

  void SearchUsersSDK()
  {
    var keywordList = string.IsNullOrEmpty(Input.text) ? null : new List<string>(Input.text.Split(' ', StringSplitOptions.RemoveEmptyEntries));
    UserSearchParam userSearchParam = new UserSearchParam();
    userSearchParam.user_search_param_keyword_list = keywordList;
    userSearchParam.user_search_param_search_count = string.IsNullOrEmpty(Count.text) ? 10 : uint.Parse(Count.text) > 0 ? uint.Parse(Count.text) : 10;
    TIMResult res = TencentIMSDK.SearchUsers(userSearchParam, Utils.addAsyncStringDataToScreen(GetResult));
    Result.text = Utils.SynchronizeResult(res);
  }

  void GetResult(params object[] parameters)
  {
    Result.text += (string)parameters[0];
    var result = Utils.FromJson<UserSearchResult>((string)parameters[1]);
    if (result ==  null) {
      UnityEngine.Debug.Log("UserSearchResult convert error!");
    }
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