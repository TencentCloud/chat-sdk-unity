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
public class CommunitySetTopicInfo : MonoBehaviour
{
  public Text Header;
  public InputField Input;
  public Text Result;
  public Button Submit;
  public Button Copy;
  void Start()
  {
    Header = GameObject.Find("HeaderText").GetComponent<Text>();
    Input = GameObject.Find("Input").GetComponent<InputField>();
    Result = GameObject.Find("ResultText").GetComponent<Text>();
    Submit = GameObject.Find("Submit").GetComponent<Button>();
    Copy = GameObject.Find("Copy").GetComponent<Button>();
    Submit.onClick.AddListener(CommunitySetTopicInfoSDK);
    Copy.GetComponentInChildren<Text>().text = Utils.t("Copy");
    Copy.onClick.AddListener(CopyText);
    if (CurrentSceneInfo.info != null)
    {
      Header.text = Utils.IsCn() ? CurrentSceneInfo.info.apiText + " " + CurrentSceneInfo.info.apiName : CurrentSceneInfo.info.apiName;
      Submit.GetComponentInChildren<Text>().text = CurrentSceneInfo.info.apiName;
    }
  }

  void CommunitySetTopicInfoSDK()
  {
    TopicInfo topicInfo = new TopicInfo();
    topicInfo.group_topic_info_topic_id = Input.text;
    topicInfo.group_modify_info_param_modify_flag = (ulong)TIMCommunityTopicModifyInfoFlag.kTIMCommunityTopicModifyInfoFlag_DefaultPermissions;
    topicInfo.default_permissions = (ulong)V2TIMTopicPermissionValue.V2TIM_TOPIC_PERMISSION_MUTE_MEMBER | (ulong)V2TIMTopicPermissionValue.V2TIM_TOPIC_PERMISSION_SEND_MESSAGE;

    TIMResult res = TencentIMSDK.CommunitySetTopicInfo(topicInfo, Utils.addAsyncStringDataToScreen(GetResult));
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