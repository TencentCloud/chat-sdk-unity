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
public class CommunityCreate : MonoBehaviour
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
    Submit.onClick.AddListener(CommunityCreateSDK);
    Copy.GetComponentInChildren<Text>().text = Utils.t("Copy");
    Copy.onClick.AddListener(CopyText);
    if (CurrentSceneInfo.info != null)
    {
      Header.text = Utils.IsCn() ? CurrentSceneInfo.info.apiText + " " + CurrentSceneInfo.info.apiName : CurrentSceneInfo.info.apiName;
      Submit.GetComponentInChildren<Text>().text = CurrentSceneInfo.info.apiName;
    }
  }

  void CommunityCreateSDK()
  {
    CreateGroupParam param = new CreateGroupParam();
    param.create_group_param_group_name = Input.text;
    param.create_group_param_group_type = TIMGroupType.kTIMGroup_Community;
    param.create_group_param_is_support_topic = true;
    param.create_group_param_add_option = TIMGroupAddOption.kTIMGroupAddOpt_Any;
    param.create_group_param_notification = "create_group_param_notification";
    param.create_group_param_introduction = "create_group_param_introduction";
    param.create_group_param_face_url = "https://yq.sukeni.com/Logo.jpg";
    param.create_group_param_enable_permission_group = true;
    param.create_group_param_default_permissions = (ulong)V2TIMCommunityPermissionValue.V2TIM_COMMUNITY_PERMISSION_MANAGE_GROUP_INFO | (ulong)V2TIMCommunityPermissionValue.V2TIM_COMMUNITY_PERMISSION_MANAGE_PERMISSION_GROUP_INFO;

    TIMResult res = TencentIMSDK.CommunityCreate(param, Utils.addAsyncStringDataToScreen(GetResult));
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