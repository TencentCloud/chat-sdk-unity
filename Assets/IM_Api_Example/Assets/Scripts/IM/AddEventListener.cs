
using UnityEngine;
using UnityEngine.UI;
using com.tencent.im.unity.demo.types;
using com.tencent.imsdk.unity;
using com.tencent.imsdk.unity.types;
using com.tencent.imsdk.unity.enums;
using com.tencent.im.unity.demo.utils;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using com.tencent.im.unity.demo.config.EventListenerList;
public class AddEventListener : MonoBehaviour
{
  public Text Header;
  public Transform ButtonArea;
  public List<EventListenerData> dataList;

  void Start()
  {
    Header = GameObject.Find("HeaderText").GetComponent<Text>();
    ButtonArea = GameObject.Find("FormPanel").GetComponent<Transform>();
    if (CurrentSceneInfo.info != null)
    {
      Header.text = Utils.IsCn() ? CurrentSceneInfo.info.apiText + " " + CurrentSceneInfo.info.apiName : CurrentSceneInfo.info.apiName;
    }
    var Parent = GameObject.Find("Form");
    dataList = Utils.FromJson<List<EventListenerData>>(EventListenerList.EventListenerListStr);
    for (int i = 0; i < dataList.Count; i++)
    {
      var obj = Instantiate(ButtonArea, Parent.transform);
      Button btn = obj.GetComponentInChildren<Button>();
      string eventName = dataList[i].eventName;
      btn.name = eventName;
      btn.GetComponentInChildren<Text>().text = "注册 " + Utils.t(dataList[i].eventName);
      RenderButton(eventName, btn);
    }
  }

  void RenderButton(string eventName, Button btn)
  {
    bool hasEvent = EventListenerInfo.Info.ContainsKey(eventName);
    if (hasEvent)
    {
      btn.onClick.RemoveAllListeners();
      btn.onClick.AddListener(() => RemoveEventListenerSDK(eventName));
      btn.GetComponentInChildren<Text>().text = Utils.t("remove") + " " + btn.GetComponentInChildren<Text>().text.Split(' ')[1];
      btn.GetComponentInChildren<Image>().color = new Color(1, 0.345f, 0.298f, 1);
    }
    else
    {
      btn.onClick.RemoveAllListeners();
      btn.onClick.AddListener(() => AddEventListenerSDK(eventName));
      btn.GetComponentInChildren<Text>().text = Utils.t("register") + " " + btn.GetComponentInChildren<Text>().text.Split(' ')[1];
      btn.GetComponentInChildren<Image>().color = new Color(0.192f, 0.345f, 0.533f, 1);
    }
  }

  void RemoveEventListenerSDK(string eventName)
  {
    switch (eventName)
    {
      case "AddRecvNewMsgCallback":
        {
          TencentIMSDK.RemoveRecvNewMsgCallback();
          break;
        }
      case "SetMsgReadedReceiptCallback":
        {
          TencentIMSDK.SetMsgReadedReceiptCallback();
          break;
        }
      case "SetMsgReactionsChangedCallback":
      {
        TencentIMSDK.SetMsgReactionsChangedCallback();
        break;
      }
      case "SetMsgAllMessageReceiveOptionCallback":
      {
        TencentIMSDK.SetMsgAllMessageReceiveOptionCallback();
        break;
      }
      case "SetMsgRevokeCallback":
        {
          TencentIMSDK.SetMsgRevokeCallback();
          break;
        }
      case "SetGroupTipsEventCallback":
        {
          TencentIMSDK.SetGroupTipsEventCallback();
          break;
        }
      case "SetMsgElemUploadProgressCallback":
        {
          TencentIMSDK.SetMsgElemUploadProgressCallback();
          break;
        }
      case "SetGroupAttributeChangedCallback":
        {
          TencentIMSDK.SetGroupAttributeChangedCallback();
          break;
        }
      case "SetConvEventCallback":
        {
          TencentIMSDK.SetConvEventCallback();
          break;
        }
      case "SetConvTotalUnreadMessageCountChangedCallback":
        {
          TencentIMSDK.SetConvTotalUnreadMessageCountChangedCallback(null);
          break;
        }
      case "SetNetworkStatusListenerCallback":
        {
          TencentIMSDK.SetNetworkStatusListenerCallback(null);
          break;
        }
      case "SetKickedOfflineCallback":
        {
          TencentIMSDK.SetKickedOfflineCallback(null);
          break;
        }
      case "SetUserSigExpiredCallback":
        {
          TencentIMSDK.SetUserSigExpiredCallback(null);
          break;
        }
      case "SetOnAddFriendCallback":
        {
          TencentIMSDK.SetOnAddFriendCallback(null);
          break;
        }
      case "SetOnDeleteFriendCallback":
        {
          TencentIMSDK.SetOnDeleteFriendCallback(null);
          break;
        }
      case "SetUpdateFriendProfileCallback":
        {
          TencentIMSDK.SetUpdateFriendProfileCallback(null);
          break;
        }
      case "SetFriendAddRequestCallback":
        {
          TencentIMSDK.SetFriendAddRequestCallback(null);
          break;
        }
      case "SetFriendApplicationListDeletedCallback":
        {
          TencentIMSDK.SetFriendApplicationListDeletedCallback(null);
          break;
        }
      case "SetFriendApplicationListReadCallback":
        {
          TencentIMSDK.SetFriendApplicationListReadCallback(null);
          break;
        }
      case "SetFriendBlackListAddedCallback":
        {
          TencentIMSDK.SetFriendBlackListAddedCallback(null);
          break;
        }
      case "SetFriendBlackListDeletedCallback":
        {
          TencentIMSDK.SetFriendBlackListDeletedCallback(null);
          break;
        }
      case "SetLogCallback":
        {
          TencentIMSDK.SetLogCallback(null);
          break;
        }
      case "SetMsgUpdateCallback":
        {
          TencentIMSDK.SetMsgUpdateCallback(null);
          break;
        }
      case "SetGroupTopicCreatedCallback":
        {
          TencentIMSDK.SetGroupTopicCreatedCallback(null);
          break;
        }
      case "SetGroupTopicDeletedCallback":
        {
          TencentIMSDK.SetGroupTopicDeletedCallback(null);
          break;
        }
      case "SetGroupTopicChangedCallback":
        {
          TencentIMSDK.SetGroupTopicChangedCallback(null);
          break;
        }
      case "SetSelfInfoUpdatedCallback":
        {
          TencentIMSDK.SetSelfInfoUpdatedCallback(null);
          break;
        }
      case "SetUserStatusChangedCallback":
        {
          TencentIMSDK.SetUserStatusChangedCallback(null);
          break;
        }
      case "SetUserInfoChangedCallback":
      {
        TencentIMSDK.SetUserInfoChangedCallback(null);
        break;
      }
      case "SetMsgExtensionsChangedCallback":
        {
          TencentIMSDK.SetMsgExtensionsChangedCallback(null);
          break;
        }
      case "SetMsgExtensionsDeletedCallback":
        {
          TencentIMSDK.SetMsgExtensionsDeletedCallback(null);
          break;
        }
      case "SetMsgGroupPinnedMessageChangedCallback":
        {
          TencentIMSDK.SetMsgGroupPinnedMessageChangedCallback(null);
          break;
        }
      case "SetFriendGroupCreatedCallback":
        {
          TencentIMSDK.SetFriendGroupCreatedCallback(null);
          break;
        }
      case "SetFriendGroupDeletedCallback":
        {
          TencentIMSDK.SetFriendGroupDeletedCallback(null);
          break;
        }
      case "SetFriendGroupNameChangedCallback":
        {
          TencentIMSDK.SetFriendGroupNameChangedCallback(null);
          break;
        }
      case "SetFriendsAddedToGroupCallback":
        {
          TencentIMSDK.SetFriendsAddedToGroupCallback(null);
          break;
        }
      case "SetFriendsDeletedFromGroupCallback":
        {
          TencentIMSDK.SetFriendsDeletedFromGroupCallback(null);
          break;
        }
      case "SetOfficialAccountSubscribedCallback":
        {
          TencentIMSDK.SetOfficialAccountSubscribedCallback(null);
          break;
        }
      case "SetOfficialAccountUnsubscribedCallback":
        {
          TencentIMSDK.SetOfficialAccountUnsubscribedCallback(null);
          break;
        }
      case "SetOfficialAccountDeletedCallback":
        {
          TencentIMSDK.SetOfficialAccountDeletedCallback(null);
          break;
        }
      case "SetOfficialAccountInfoChangedCallback":
        {
          TencentIMSDK.SetOfficialAccountInfoChangedCallback(null);
          break;
        }
      case "SetMyFollowingListChangedCallback":
        {
          TencentIMSDK.SetMyFollowingListChangedCallback(null);
          break;
        }
      case "SetMyFollowersListChangedCallback":
        {
          TencentIMSDK.SetMyFollowersListChangedCallback(null);
          break;
        }
      case "SetMutualFollowersListChangedCallback":
        {
          TencentIMSDK.SetMutualFollowersListChangedCallback(null);
          break;
        }
      case "SetSignalingReceiveNewInvitationCallback":
        {
          TencentIMSDK.SetSignalingReceiveNewInvitationCallback(null);
          break;
        }
      case "SetSignalingInvitationCancelledCallback":
        {
          TencentIMSDK.SetSignalingInvitationCancelledCallback(null);
          break;
        }
      case "SetSignalingInviteeAcceptedCallback":
        {
          TencentIMSDK.SetSignalingInviteeAcceptedCallback(null);
          break;
        }
      case "SetSignalingInviteeRejectedCallback":
        {
          TencentIMSDK.SetSignalingInviteeRejectedCallback(null);
          break;
        }
      case "SetSignalingInvitationTimeoutCallback":
        {
          TencentIMSDK.SetSignalingInvitationTimeoutCallback(null);
          break;
        }
      case "SetSignalingInvitationModifiedCallback":
        {
          TencentIMSDK.SetSignalingInvitationModifiedCallback(null);
          break;
        }
      case "SetCommunityCreateTopicCallback":
        {
          TencentIMSDK.SetCommunityCreateTopicCallback(null);
          break;
        }
      case "SetCommunityDeleteTopicCallback":
        {
          TencentIMSDK.SetCommunityDeleteTopicCallback(null);
          break;
        }
      case "SetCommunityChangeTopicInfoCallback":
        {
        TencentIMSDK.SetCommunityChangeTopicInfoCallback(null);
          break;
        }
      case "SetCommunityReceiveTopicRESTCustomDataCallback":
        {
          TencentIMSDK.SetCommunityReceiveTopicRESTCustomDataCallback(null);
          break;
        }
      case "SetCommunityCreatePermissionGroupCallback":
        {
          TencentIMSDK.SetCommunityCreatePermissionGroupCallback(null);
          break;
        }
      case "SetCommunityDeletePermissionGroupCallback":
        {
          TencentIMSDK.SetCommunityDeletePermissionGroupCallback(null);
          break;
        }
      case "SetCommunityChangePermissionGroupInfoCallback":
        {
          TencentIMSDK.SetCommunityChangePermissionGroupInfoCallback(null);
          break;
        }
      case "SetCommunityAddMembersToPermissionGroupCallback":
        {
          TencentIMSDK.SetCommunityAddMembersToPermissionGroupCallback(null);
          break;
        }
      case "SetCommunityRemoveMembersFromPermissionGroupCallback":
        {
          TencentIMSDK.SetCommunityRemoveMembersFromPermissionGroupCallback(null);
          break;
        }
      case "SetCommunityAddTopicPermissionCallback":
        {
          TencentIMSDK.SetCommunityAddTopicPermissionCallback(null);
          break;
        }
      case "SetCommunityDeleteTopicPermissionCallback":
        {
          TencentIMSDK.SetCommunityDeleteTopicPermissionCallback(null);
          break;
        }
      case "SetCommunityModifyTopicPermissionCallback":
        {
          TencentIMSDK.SetCommunityModifyTopicPermissionCallback(null);
          break;
        }
      case "SetExperimentalNotifyCallback":
        {
          TencentIMSDK.SetExperimentalNotifyCallback(null);
          break;
        }
      default:
        {
          print($"Unknown event {eventName}");
          break;
        }
    }
    EventListenerInfo.Info.Remove(eventName);
    Button btn = GameObject.Find(eventName).GetComponent<Button>();
    RenderButton(eventName, btn);
  }
  void AddEventListenerSDK(string eventName)
  {
    var eventInfo = new EventListenerInfo.EventInfo();
    EventListenerInfo.Info.Add(eventName, eventInfo);
    switch (eventName)
    {
      case "AddRecvNewMsgCallback":
        {
          TencentIMSDK.AddRecvNewMsgCallback(null, Utils.RecvNewMsgCallback(GetResult, eventInfo));
          break;
        }
      case "SetMsgReactionsChangedCallback":
        {
          TencentIMSDK.SetMsgReactionsChangedCallback(null, Utils.setMsgReactionsChangedCallback(GetResult, eventInfo));
          break;
        }
        case "SetMsgAllMessageReceiveOptionCallback":
        {
          TencentIMSDK.SetMsgAllMessageReceiveOptionCallback(null, Utils.setMsgAllMessageReceiveOptionCallback(GetResult, eventInfo));
          break;
        }
      case "SetMsgReadedReceiptCallback":
        {
          TencentIMSDK.SetMsgReadedReceiptCallback(null, Utils.SetMsgReadedReceiptCallback(GetResult, eventInfo));
          break;
        }
      case "SetMsgRevokeCallback":
        {
          TencentIMSDK.SetMsgRevokeCallback(null, Utils.SetMsgRevokeCallback(GetResult, eventInfo));
          break;
        }
      case "SetGroupTipsEventCallback":
        {
          TencentIMSDK.SetGroupTipsEventCallback(null, Utils.SetGroupTipsEventCallback(GetResult, eventInfo));
          break;
        }
      case "SetMsgElemUploadProgressCallback":
        {
          TencentIMSDK.SetMsgElemUploadProgressCallback(null, Utils.SetMsgElemUploadProgressCallback(GetResult, eventInfo));
          break;
        }
      case "SetGroupAttributeChangedCallback":
        {
          TencentIMSDK.SetGroupAttributeChangedCallback(null, Utils.SetGroupAttributeChangedCallback(GetResult, eventInfo));
          break;
        }
      case "SetConvEventCallback":
        {
          TencentIMSDK.SetConvEventCallback(null, Utils.SetConvEventCallback(GetResult, eventInfo));
          break;
        }
      case "SetConvTotalUnreadMessageCountChangedCallback":
        {
          TencentIMSDK.SetConvTotalUnreadMessageCountChangedCallback(Utils.SetConvTotalUnreadMessageCountChangedCallback(GetResult, eventInfo));
          break;
        }
      case "SetNetworkStatusListenerCallback":
        {
          TencentIMSDK.SetNetworkStatusListenerCallback(Utils.SetNetworkStatusListenerCallback(GetResult, eventInfo));
          break;
        }
      case "SetKickedOfflineCallback":
        {
          TencentIMSDK.SetKickedOfflineCallback(Utils.SetKickedOfflineCallback(GetResult, eventInfo));
          break;
        }
      case "SetUserSigExpiredCallback":
        {
          TencentIMSDK.SetUserSigExpiredCallback(Utils.SetUserSigExpiredCallback(GetResult, eventInfo));
          break;
        }
      case "SetOnAddFriendCallback":
        {
          TencentIMSDK.SetOnAddFriendCallback(null, Utils.SetOnAddFriendCallback(GetResult, eventInfo));
          break;
        }
      case "SetOnDeleteFriendCallback":
        {
          TencentIMSDK.SetOnDeleteFriendCallback(null, Utils.SetOnDeleteFriendCallback(GetResult, eventInfo));
          break;
        }
      case "SetUpdateFriendProfileCallback":
        {
          TencentIMSDK.SetUpdateFriendProfileCallback(null, Utils.SetUpdateFriendProfileCallback(GetResult, eventInfo));
          break;
        }
      case "SetFriendAddRequestCallback":
        {
          TencentIMSDK.SetFriendAddRequestCallback(null, Utils.SetFriendAddRequestCallback(GetResult, eventInfo));
          break;
        }
      case "SetFriendApplicationListDeletedCallback":
        {
          TencentIMSDK.SetFriendApplicationListDeletedCallback(null, Utils.SetFriendApplicationListDeletedCallback(GetResult, eventInfo));
          break;
        }
      case "SetFriendApplicationListReadCallback":
        {
          TencentIMSDK.SetFriendApplicationListReadCallback(Utils.SetFriendApplicationListReadCallback(GetResult, eventInfo));
          break;
        }
      case "SetFriendBlackListAddedCallback":
        {
          TencentIMSDK.SetFriendBlackListAddedCallback(null, Utils.SetFriendBlackListAddedCallback(GetResult, eventInfo));
          break;
        }
      case "SetFriendBlackListDeletedCallback":
        {
          TencentIMSDK.SetFriendBlackListDeletedCallback(null, Utils.SetFriendBlackListDeletedCallback(GetResult, eventInfo));
          break;
        }
      case "SetLogCallback":
        {
          TencentIMSDK.SetLogCallback(Utils.SetLogCallback(GetResult, eventInfo));
          break;
        }
      case "SetMsgUpdateCallback":
        {
          TencentIMSDK.SetMsgUpdateCallback(null, Utils.SetMsgUpdateCallback(GetResult, eventInfo));
          break;
        }
      case "SetGroupTopicCreatedCallback":
        {
          TencentIMSDK.SetGroupTopicCreatedCallback(Utils.SetGroupTopicCreatedCallback(GetResult, eventInfo));
          break;
        }
      case "SetGroupTopicDeletedCallback":
        {
          TencentIMSDK.SetGroupTopicDeletedCallback(null, Utils.SetGroupTopicDeletedCallback(GetResult, eventInfo));
          break;
        }
      case "SetGroupTopicChangedCallback":
        {
          TencentIMSDK.SetGroupTopicChangedCallback(null, Utils.SetGroupTopicChangedCallback(GetResult, eventInfo));
          break;
        }
      case "SetSelfInfoUpdatedCallback":
        {
          TencentIMSDK.SetSelfInfoUpdatedCallback(null, Utils.SetSelfInfoUpdatedCallback(GetResult, eventInfo));
          break;
        }
      case "SetUserStatusChangedCallback":
        {
          TencentIMSDK.SetUserStatusChangedCallback(null, Utils.SetUserStatusChangedCallback(GetResult, eventInfo));
          break;
        }
      case "SetUserInfoChangedCallback":
      {
        TencentIMSDK.SetUserInfoChangedCallback(null,Utils.SetUserInfoChangedCallback(GetResult,eventInfo));
        break;
      }
      case "SetMsgExtensionsChangedCallback":
        {
          TencentIMSDK.SetMsgExtensionsChangedCallback(null, Utils.SetMsgExtensionsChangedCallback(GetResult, eventInfo));
          break;
        }
      case "SetMsgExtensionsDeletedCallback":
        {
          TencentIMSDK.SetMsgExtensionsDeletedCallback(null, Utils.SetMsgExtensionsDeletedCallback(GetResult, eventInfo));
          break;
        }
      case "SetMsgGroupPinnedMessageChangedCallback":
        {
          TencentIMSDK.SetMsgGroupPinnedMessageChangedCallback(null, Utils.SetMsgGroupPinnedMessageChangedCallback(GetResult, eventInfo));
          break;
        }
      case "SetFriendGroupCreatedCallback":
        {
          TencentIMSDK.SetFriendGroupCreatedCallback(null, Utils.SetFriendGroupCreatedCallback(GetResult, eventInfo));
          break;
        }
      case "SetFriendGroupDeletedCallback":
        {
          TencentIMSDK.SetFriendGroupDeletedCallback(null, Utils.SetFriendGroupDeletedCallback(GetResult, eventInfo));
          break;
        }
      case "SetFriendGroupNameChangedCallback":
        {
          TencentIMSDK.SetFriendGroupNameChangedCallback(null, Utils.SetFriendGroupNameChangedCallback(GetResult, eventInfo));
          break;
        }
      case "SetFriendsAddedToGroupCallback":
        {
          TencentIMSDK.SetFriendsAddedToGroupCallback(null, Utils.SetFriendsAddedToGroupCallback(GetResult, eventInfo));
          break;
        }
      case "SetFriendsDeletedFromGroupCallback":
        {
          TencentIMSDK.SetFriendsDeletedFromGroupCallback(null, Utils.SetFriendsDeletedFromGroupCallback(GetResult, eventInfo));
          break;
        }
      case "SetOfficialAccountSubscribedCallback":
        {
          TencentIMSDK.SetOfficialAccountSubscribedCallback(null, Utils.SetOfficialAccountSubscribedCallback(GetResult, eventInfo));
          break;
        }
      case "SetOfficialAccountUnsubscribedCallback":
        {
          TencentIMSDK.SetOfficialAccountUnsubscribedCallback(null, Utils.SetOfficialAccountUnsubscribedCallback(GetResult, eventInfo));
          break;
        }
      case "SetOfficialAccountDeletedCallback":
        {
          TencentIMSDK.SetOfficialAccountDeletedCallback(null, Utils.SetOfficialAccountDeletedCallback(GetResult, eventInfo));
          break;
        }
      case "SetOfficialAccountInfoChangedCallback":
        {
          TencentIMSDK.SetOfficialAccountInfoChangedCallback(null, Utils.SetOfficialAccountInfoChangedCallback(GetResult, eventInfo));
          break;
        }
      case "SetMyFollowingListChangedCallback":
        {
          TencentIMSDK.SetMyFollowingListChangedCallback(null, Utils.SetMyFollowingListChangedCallback(GetResult, eventInfo));
          break;
        }
      case "SetMyFollowersListChangedCallback":
        {
          TencentIMSDK.SetMyFollowersListChangedCallback(null, Utils.SetMyFollowersListChangedCallback(GetResult, eventInfo));
          break;
        }
      case "SetMutualFollowersListChangedCallback":
        {
          TencentIMSDK.SetMutualFollowersListChangedCallback(null, Utils.SetMutualFollowersListChangedCallback(GetResult, eventInfo));
          break;
        }
      case "SetSignalingReceiveNewInvitationCallback":
        {
          TencentIMSDK.SetSignalingReceiveNewInvitationCallback(null, Utils.SetSignalingReceiveNewInvitationCallback(GetResult, eventInfo));
          break;
        }
      case "SetSignalingInvitationCancelledCallback":
        {
          TencentIMSDK.SetSignalingInvitationCancelledCallback(null, Utils.SetSignalingInvitationCancelledCallback(GetResult, eventInfo));
          break;
        }
      case "SetSignalingInviteeAcceptedCallback":
        {
          TencentIMSDK.SetSignalingInviteeAcceptedCallback(null, Utils.SetSignalingInviteeAcceptedCallback(GetResult, eventInfo));
          break;
        }
      case "SetSignalingInviteeRejectedCallback":
        {
          TencentIMSDK.SetSignalingInviteeRejectedCallback(null, Utils.SetSignalingInviteeRejectedCallback(GetResult, eventInfo));
          break;
        }
      case "SetSignalingInvitationTimeoutCallback":
        {
          TencentIMSDK.SetSignalingInvitationTimeoutCallback(null, Utils.SetSignalingInvitationTimeoutCallback(GetResult, eventInfo));
          break;
        }
      case "SetSignalingInvitationModifiedCallback":
        {
          TencentIMSDK.SetSignalingInvitationModifiedCallback(null, Utils.SetSignalingInvitationModifiedCallback(GetResult, eventInfo));
          break;
        }
      case "SetCommunityCreateTopicCallback":
        {
          TencentIMSDK.SetCommunityCreateTopicCallback(null, Utils.SetCommunityCreateTopicCallback(GetResult, eventInfo));
          break;
        }
      case "SetCommunityDeleteTopicCallback":
        {
          TencentIMSDK.SetCommunityDeleteTopicCallback(null, Utils.SetCommunityDeleteTopicCallback(GetResult, eventInfo));
          break;
        }
      case "SetCommunityChangeTopicInfoCallback":
        {
          TencentIMSDK.SetCommunityChangeTopicInfoCallback(null, Utils.SetCommunityChangeTopicInfoCallback(GetResult, eventInfo));
          break;
        }
      case "SetCommunityReceiveTopicRESTCustomDataCallback":
        {
          TencentIMSDK.SetCommunityReceiveTopicRESTCustomDataCallback(null, Utils.SetCommunityReceiveTopicRESTCustomDataCallback(GetResult, eventInfo));
          break;
        }
      case "SetCommunityCreatePermissionGroupCallback":
        {
          TencentIMSDK.SetCommunityCreatePermissionGroupCallback(null, Utils.SetCommunityCreatePermissionGroupCallback(GetResult, eventInfo));
          break;
        }
      case "SetCommunityDeletePermissionGroupCallback":
        {
          TencentIMSDK.SetCommunityDeletePermissionGroupCallback(null, Utils.SetCommunityDeletePermissionGroupCallback(GetResult, eventInfo));
          break;
        }
      case "SetCommunityChangePermissionGroupInfoCallback":
        {
          TencentIMSDK.SetCommunityChangePermissionGroupInfoCallback(null, Utils.SetCommunityChangePermissionGroupInfoCallback(GetResult, eventInfo));
          break;
        }
      case "SetCommunityAddMembersToPermissionGroupCallback":
        {
          TencentIMSDK.SetCommunityAddMembersToPermissionGroupCallback(null, Utils.SetCommunityAddMembersToPermissionGroupCallback(GetResult, eventInfo));
          break;
        }
      case "SetCommunityRemoveMembersFromPermissionGroupCallback":
        {
          TencentIMSDK.SetCommunityRemoveMembersFromPermissionGroupCallback(null, Utils.SetCommunityRemoveMembersFromPermissionGroupCallback(GetResult, eventInfo));
          break;
        }
      case "SetCommunityAddTopicPermissionCallback":
        {
          TencentIMSDK.SetCommunityAddTopicPermissionCallback(null, Utils.SetCommunityAddTopicPermissionCallback(GetResult, eventInfo));
          break;
        }
      case "SetCommunityDeleteTopicPermissionCallback":
        {
          TencentIMSDK.SetCommunityDeleteTopicPermissionCallback(null, Utils.SetCommunityDeleteTopicPermissionCallback(GetResult, eventInfo));
          break;
        }
      case "SetCommunityModifyTopicPermissionCallback":
        {
          TencentIMSDK.SetCommunityModifyTopicPermissionCallback(null, Utils.SetCommunityModifyTopicPermissionCallback(GetResult, eventInfo));
          break;
        }
      case "SetExperimentalNotifyCallback":
        {
          TencentIMSDK.SetExperimentalNotifyCallback(null, Utils.SetExperimentalNotifyCallback(GetResult, eventInfo));
          break;
        }
      default:
        {
          print($"Unknown event {eventName}");
          break;
        }
    }
    Button btn = GameObject.Find(eventName).GetComponent<Button>();
    RenderButton(eventName, btn);
  }

  void GetResult(EventListenerInfo.EventInfo eventInfo, params object[] parameters)
  {
    string CallbackData = (string)parameters[0];
    eventInfo.Result = CallbackData;
  }

  void OnApplicationQuit()
  {
    TencentIMSDK.Uninit();
  }
}