using UnityEngine;
using UnityEngine.UI;
using com.tencent.im.unity.demo.types;
using com.tencent.imsdk.unity;
using com.tencent.imsdk.unity.types;
using com.tencent.imsdk.unity.enums;
using EasyUI.Toast;
using com.tencent.im.unity.demo.utils;

public class GetServerTime : MonoBehaviour
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
    Submit.onClick.AddListener(GetServerTimeSDK);
    Copy.onClick.AddListener(CopyText);
  }
  public void GetServerTimeSDK()
  {
    long time = TencentIMSDK.GetServerTime();
    Result.text = time.ToString();
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