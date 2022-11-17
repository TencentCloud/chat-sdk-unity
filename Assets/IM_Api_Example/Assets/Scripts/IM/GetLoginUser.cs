using UnityEngine;
using UnityEngine.UI;
using com.tencent.im.unity.demo.types;
using com.tencent.imsdk.unity;
using com.tencent.imsdk.unity.types;
using com.tencent.imsdk.unity.enums;
using EasyUI.Toast;
using com.tencent.im.unity.demo.utils;
using System.Text;

public class GetLoginUser : MonoBehaviour
{

  public Text Header;
  public Text Result;
  public Button Submit;
  public Button Copy;

  void Start()
  {
    Result = GameObject.Find("ResultText").GetComponent<Text>();
    Header = GameObject.Find("HeaderText").GetComponent<Text>();
    Submit = GameObject.Find("Submit").GetComponent<Button>();
    if (CurrentSceneInfo.info != null)
    {
      Header.text = Utils.IsCn() ? CurrentSceneInfo.info.apiText + " " + CurrentSceneInfo.info.apiName : CurrentSceneInfo.info.apiName;
      Submit.GetComponentInChildren<Text>().text = CurrentSceneInfo.info.apiName;
    }
    Copy = GameObject.Find("Copy").GetComponent<Button>();
    Copy.GetComponentInChildren<Text>().text = Utils.t("Copy");
    Submit.onClick.AddListener(GetLoginUserSDK);
    Copy.onClick.AddListener(CopyText);
  }

#if UNITY_WEBGL && !UNITY_EDITOR
  void GetResult(params object[] parameters)
  {
    Result.text += (string)parameters[0];
  }

  public void GetLoginUserSDK()
  {
    TIMResult res = TencentIMSDK.GetLoginUserIDWeb(Utils.addAsyncStringDataToScreen(GetResult));
    Result.text = Utils.SynchronizeResult(res);
  }
#else
  public void GetLoginUserSDK()
  {
    StringBuilder userId = new StringBuilder(128);
    TIMResult res = TencentIMSDK.GetLoginUserID(userId);
    Result.text = Utils.SynchronizeResult(res);
    Result.text += "\nLoginUser: " + userId.ToString();
  }
#endif

  void CopyText()
  {
    Utils.Copy(Result.text);
  }
  void OnApplicationQuit()
  {
    TencentIMSDK.Uninit();
  }
}