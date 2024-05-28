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
public class MsgDownloadElemToPath : MonoBehaviour
{

  public Text Header;
  public InputField MsgID;
  public Text Result;
  public Button Submit;
  public Button Copy;
  void Start()
  {
    Header = GameObject.Find("HeaderText").GetComponent<Text>();
    MsgID = GameObject.Find("MsgID").GetComponent<InputField>();
    Result = GameObject.Find("ResultText").GetComponent<Text>();
    Submit = GameObject.Find("Submit").GetComponent<Button>();
    Copy = GameObject.Find("Copy").GetComponent<Button>();
    Submit.onClick.AddListener(MsgFindMessages);
    Copy.GetComponentInChildren<Text>().text = Utils.t("Copy");
    Copy.onClick.AddListener(CopyText);
    if (CurrentSceneInfo.info != null)
    {
      Header.text = Utils.IsCn() ? CurrentSceneInfo.info.apiText + " " + CurrentSceneInfo.info.apiName : CurrentSceneInfo.info.apiName;
      Submit.GetComponentInChildren<Text>().text = CurrentSceneInfo.info.apiName;
    }
  }
  void MsgFindMessages()
  {
    TIMResult res = TencentIMSDK.MsgFindMessages(new List<string> { MsgID.text }, Utils.addAsyncStringDataToScreen(GetMessage));
    print("MsgFindMessages: " + res);
  }

  void MsgDownloadElemToPathSDK(Message msg)
  {
    if (msg.message_elem_array.Count < 1)
    {
      return;
    }
    Elem el = msg.message_elem_array[0];
    var param = new DownloadElemParam();
    bool shouldDownload = false;
    string path = Application.persistentDataPath + "/";
    if (el.elem_type == TIMElemType.kTIMElem_Sound)
    {
      // sound
      param = new DownloadElemParam
      {
        msg_download_elem_param_flag = (uint)el.sound_elem_download_flag,
        msg_download_elem_param_type = TIMDownloadType.kTIMDownload_Sound,
        msg_download_elem_param_id = el.sound_elem_file_id,
        msg_download_elem_param_business_id = (uint)el.sound_elem_business_id,
        msg_download_elem_param_url = el.sound_elem_url
      };
      shouldDownload = true;
      path += el.sound_elem_file_id;
    }
    if (el.elem_type == TIMElemType.kTIMElem_Video)
    {
      // video
      param = new DownloadElemParam
      {
        msg_download_elem_param_flag = (uint)el.video_elem_video_download_flag,
        msg_download_elem_param_type = TIMDownloadType.kTIMDownload_Video,
        msg_download_elem_param_id = el.video_elem_video_id,
        msg_download_elem_param_business_id = (uint)el.video_elem_business_id,
        msg_download_elem_param_url = el.video_elem_video_url
      };
      shouldDownload = true;
      path += el.video_elem_video_id;
    }
    if (el.elem_type == TIMElemType.kTIMElem_File)
    {
      // file
      param = new DownloadElemParam
      {
        msg_download_elem_param_flag = (uint)el.file_elem_download_flag,
        msg_download_elem_param_type = TIMDownloadType.kTIMDownload_File,
        msg_download_elem_param_id = el.file_elem_file_id,
        msg_download_elem_param_business_id = (uint)el.file_elem_business_id,
        msg_download_elem_param_url = el.file_elem_url
      };
      shouldDownload = true;
      path += el.file_elem_file_id;
    }
    if (!shouldDownload)
    {
      return;
    }
    TIMResult res = TencentIMSDK.MsgDownloadElemToPath(param, path,  (int code, string desc, string callbackData, string user_data) =>{
      print( callbackData);
      Result.text+=code+"desc" + desc;
    });
    Result.text = Utils.SynchronizeResult(res);
  }

  void GetMessage(params object[] parameters)
  {
    string text = (string)parameters[1];
    print(text);
    var list = Utils.FromJson<List<Message>>(text);
    if (list.Count > 0)
    {
      MsgDownloadElemToPathSDK(list[0]);
    }
  }
  void GetResult(params object[] parameters)
  {
    print("GetResult: " + parameters[0]);
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