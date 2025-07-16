using com.tencent.imsdk.unity.native;
using com.tencent.imsdk.unity.web;
using com.tencent.imsdk.unity.utils;
using com.tencent.imsdk.unity.callback;
using com.tencent.imsdk.unity.enums;
using com.tencent.imsdk.unity.types;
using UnityEngine;
using AOT;
using System.Text;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Reflection;
using System.Linq;

namespace com.tencent.imsdk.unity
{
  public class TencentIMSDK
  {
    private static SynchronizationContext mainSyncContext = SynchronizationContext.Current;

    private static Dictionary<string, Delegate> ValuecallbackStore = new Dictionary<string, Delegate>();
    private static Dictionary<string, SendOrPostCallback> ValuecallbackDeleStore = new Dictionary<string, SendOrPostCallback>();
    // private static Dictionary<string, StringBuilder> StrBuilderStore = new Dictionary<string, StringBuilder>();
    private static RecvNewMsgCallback RecvNewMsgCallbackStore;
    private static MsgReactionsChangedCallback MsgReactionsChangedCallbackStore;
    private static MsgAllMessageReceiveOptionCallback MsgAllMessageReceiveOptionCallbackStore;
    private static MsgReadedReceiptCallback MsgReadedReceiptCallbackStore;
    private static MsgRevokeCallback MsgRevokeCallbackStore;
    private static MsgElemUploadProgressCallback MsgElemUploadProgressCallbackStore;
    private static GroupTipsEventCallback GroupTipsEventCallbackStore;
    private static GroupAttributeChangedCallback GroupAttributeChangedCallbackStore;
    private static GroupCounterChangedCallback GroupCounterChangedCallbackStore;
    private static ConvEventCallback ConvEventCallbackStore;
    private static ConvConversationGroupCreatedCallback ConvConversationGroupCreatedCallbackStore;
    private static ConvConversationGroupDeletedCallback ConvConversationGroupDeletedCallbackStore;
    private static ConvConversationGroupNameChangedCallback ConvConversationGroupNameChangedCallbackStore;
    private static ConvConversationsAddedToGroupCallback ConvConversationsAddedToGroupCallbackStore;
    private static ConvConversationsDeletedFromGroupCallback ConvConversationsDeletedFromGroupCallbackStore;
    private static ConvTotalUnreadMessageCountChangedCallback ConvTotalUnreadMessageCountChangedCallbackStore;
    private static ConvTotalUnreadMessageCountChangedByFilterCallback ConvUnreadMessageCountChangedByFilterCallbackStore;
    private static NetworkStatusListenerCallback NetworkStatusListenerCallbackStore;
    private static KickedOfflineCallback KickedOfflineCallbackStore;
    private static UserSigExpiredCallback UserSigExpiredCallbackStore;
    private static OnAddFriendCallback OnAddFriendCallbackStore;
    private static OnDeleteFriendCallback OnDeleteFriendCallbackStore;
    private static UpdateFriendProfileCallback UpdateFriendProfileCallbackStore;
    private static FriendAddRequestCallback FriendAddRequestCallbackStore;
    private static FriendApplicationListDeletedCallback FriendApplicationListDeletedCallbackStore;
    private static FriendApplicationListReadCallback FriendApplicationListReadCallbackStore;
    private static FriendBlackListAddedCallback FriendBlackListAddedCallbackStore;
    private static FriendBlackListDeletedCallback FriendBlackListDeletedCallbackStore;
    private static LogCallback LogCallbackStore;
    private static MsgUpdateCallback MsgUpdateCallbackStore;
    private static MsgGroupPinnedMessageChangedCallback MsgGroupPinnedMessageChangedCallbackStore;
    private static MsgGroupMessageReadMemberListCallback MsgGroupMessageReadMemberListCallbackStore;
    private static MsgExtensionsChangedCallback MsgExtensionsChangedCallbackStore;
    private static MsgExtensionsDeletedCallback MsgExtensionsDeletedCallbackStore;
    private static IMWebSDK.MsgGroupMessageReadMemberListCallback MsgGroupMessageReadMemberListCallbackWebStore;
    private static GroupTopicCreatedCallback GroupTopicCreatedCallbackStore;
    private static GroupTopicDeletedCallback GroupTopicDeletedCallbackStore;
    private static GroupTopicChangedCallback GroupTopicChangedCallbackStore;
    private static SelfInfoUpdatedCallback SelfInfoUpdatedCallbackStore;
    private static UserStatusChangedCallback UserStatusChangedCallbackStore;
    private static UserInfoChangedCallback UserInfoChangedCallbackStore;
    private static FriendGroupCreatedCallback FriendGroupCreatedCallbackStore;
    private static FriendGroupDeletedCallback FriendGroupDeletedCallbackStore;
    private static FriendGroupNameChangedCallback FriendGroupNameChangedCallbackStore;
    private static FriendsAddedToGroupCallback FriendsAddedToGroupCallbackStore;
    private static FriendsDeletedFromGroupCallback FriendsDeletedFromGroupCallbackStore;
    private static OfficialAccountSubscribedCallback OfficialAccountSubscribedCallbackStore;
    private static OfficialAccountUnsubscribedCallback OfficialAccountUnsubscribedCallbackStore;
    private static OfficialAccountDeletedCallback OfficialAccountDeletedCallbackStore;
    private static OfficialAccountInfoChangedCallback OfficialAccountInfoChangedCallbackStore;
    private static MyFollowingListChangedCallback MyFollowingListChangedCallbackStore;
    private static MyFollowersListChangedCallback MyFollowersListChangedCallbackStore;
    private static MutualFollowersListChangedCallback MutualFollowersListChangedCallbackStore;
    private static SignalingReceiveNewInvitationCallback SignalingReceiveNewInvitationCallbackStore;
    private static SignalingInvitationCancelledCallback SignalingInvitationCancelledCallbackStore;
    private static SignalingInviteeAcceptedCallback SignalingInviteeAcceptedCallbackStore;
    private static SignalingInviteeRejectedCallback SignalingInviteeRejectedCallbackStore;
    private static SignalingInvitationTimeoutCallback SignalingInvitationTimeoutCallbackStore;
    private static SignalingInvitationModifiedCallback SignalingInvitationModifiedCallbackStore;
    private static CommunityCreateTopicCallback CommunityCreateTopicCallbackStore;
    private static CommunityDeleteTopicCallback CommunityDeleteTopicCallbackStore;
    private static CommunityChangeTopicInfoCallback CommunityChangeTopicInfoCallbackStore;
    private static CommunityReceiveTopicRESTCustomDataCallback CommunityReceiveTopicRESTCustomDataCallbackStore;
    private static CommunityCreatePermissionGroupCallback CommunityCreatePermissionGroupCallbackStore;
    private static CommunityDeletePermissionGroupCallback CommunityDeletePermissionGroupCallbackStore;
    private static CommunityChangePermissionGroupInfoCallback CommunityChangePermissionGroupInfoCallbackStore;
    private static CommunityAddMembersToPermissionGroupCallback CommunityAddMembersToPermissionGroupCallbackStore;
    private static CommunityRemoveMembersFromPermissionGroupCallback CommunityRemoveMembersFromPermissionGroupCallbackStore;
    private static CommunityAddTopicPermissionCallback CommunityAddTopicPermissionCallbackStore;
    private static CommunityDeleteTopicPermissionCallback CommunityDeleteTopicPermissionCallbackStore;
    private static CommunityModifyTopicPermissionCallback CommunityModifyTopicPermissionCallbackStore;
    private static ExperimentalNotifyCallback ExperimentalNotifyCallbackStore;


    private static RecvNewMsgStringCallback RecvNewMsgStringCallbackStore;
    private static MsgReactionsChangedStringCallback MsgReactionsChangedStringCallbackStore;
    private static MsgAllMessageReceiveOptionStringCallback MsgAllMessageReceiveOptionStringCallbackStore;
    private static MsgReadedReceiptStringCallback MsgReadedReceiptStringCallbackStore;
    private static MsgRevokeStringCallback MsgRevokeStringCallbackStore;
    private static MsgElemUploadProgressStringCallback MsgElemUploadProgressStringCallbackStore;
    private static GroupTipsEventStringCallback GroupTipsEventStringCallbackStore;
    private static GroupAttributeChangedStringCallback GroupAttributeChangedStringCallbackStore;
    private static ConvEventStringCallback ConvEventStringCallbackStore;
    private static ConvConversationGroupCreatedStringCallback ConvConversationGroupCreatedStringCallbackStore;
    private static ConvConversationsAddedToGroupStringCallback ConvConversationsAddedToGroupStringCallbackStore;
    private static ConvConversationsDeletedFromGroupStringCallback ConvConversationsDeletedFromGroupStringCallbackStore;
    private static ConvTotalUnreadMessageCountChangedByFilterStringCallback ConvUnreadMessageCountChangedByFilterStringCallbackStore;
    private static OnAddFriendStringCallback OnAddFriendStringCallbackStore;
    private static OnDeleteFriendStringCallback OnDeleteFriendStringCallbackStore;
    private static UpdateFriendProfileStringCallback UpdateFriendProfileStringCallbackStore;
    private static FriendAddRequestStringCallback FriendAddRequestStringCallbackStore;
    private static FriendApplicationListDeletedStringCallback FriendApplicationListDeletedStringCallbackStore;
    private static FriendBlackListAddedStringCallback FriendBlackListAddedStringCallbackStore;
    private static FriendBlackListDeletedStringCallback FriendBlackListDeletedStringCallbackStore;
    private static MsgUpdateStringCallback MsgUpdateStringCallbackStore;
    private static MsgGroupPinnedMessageChangedStringCallback MsgGroupPinnedMessageChangedStringCallbackStore;
    private static MsgGroupMessageReadMemberListStringCallback MsgGroupMessageReadMemberListStringCallbackStore;
    private static MsgExtensionsChangedStringCallback MsgExtensionsChangedStringCallbackStore;
    private static MsgExtensionsDeletedStringCallback MsgExtensionsDeletedStringCallbackStore;
    private static GroupTopicDeletedStringCallback GroupTopicDeletedStringCallbackStore;
    private static GroupTopicChangedStringCallback GroupTopicChangedStringCallbackStore;
    private static SelfInfoUpdatedStringCallback SelfInfoUpdatedStringCallbackStore;
    private static UserStatusChangedStringCallback UserStatusChangedStringCallbackStore;
    private static UserInfoChangedStringCallback UserInfoChangedStringCallbackStore;
    private static FriendGroupCreatedStringCallback FriendGroupCreatedStringCallbackStore;
    private static FriendGroupDeletedStringCallback FriendGroupDeletedStringCallbackStore;
    private static FriendGroupNameChangedStringCallback FriendGroupNameChangedStringCallbackStore;
    private static FriendsAddedToGroupStringCallback FriendsAddedToGroupStringCallbackStore;
    private static FriendsDeletedFromGroupStringCallback FriendsDeletedFromGroupStringCallbackStore;
    private static OfficialAccountSubscribedStringCallback OfficialAccountSubscribedStringCallbackStore;
    private static OfficialAccountUnsubscribedStringCallback OfficialAccountUnsubscribedStringCallbackStore;
    private static OfficialAccountDeletedStringCallback OfficialAccountDeletedStringCallbackStore;
    private static OfficialAccountInfoChangedStringCallback OfficialAccountInfoChangedStringCallbackStore;
    private static MyFollowingListChangedStringCallback MyFollowingListChangedStringCallbackStore;
    private static MyFollowersListChangedStringCallback MyFollowersListChangedStringCallbackStore;
    private static MutualFollowersListChangedStringCallback MutualFollowersListChangedStringCallbackStore;
    private static SignalingReceiveNewInvitationStringCallback SignalingReceiveNewInvitationStringCallbackStore;
    private static SignalingInvitationCancelledStringCallback SignalingInvitationCancelledStringCallbackStore;
    private static SignalingInviteeAcceptedStringCallback SignalingInviteeAcceptedStringCallbackStore;
    private static SignalingInviteeRejectedStringCallback SignalingInviteeRejectedStringCallbackStore;
    private static SignalingInvitationTimeoutStringCallback SignalingInvitationTimeoutStringCallbackStore;
    private static SignalingInvitationModifiedStringCallback SignalingInvitationModifiedStringCallbackStore;
    private static CommunityCreateTopicStringCallback CommunityCreateTopicStringCallbackStore;
    private static CommunityDeleteTopicStringCallback CommunityDeleteTopicStringCallbackStore;
    private static CommunityChangeTopicInfoStringCallback CommunityChangeTopicInfoStringCallbackStore;
    private static CommunityReceiveTopicRESTCustomDataStringCallback CommunityReceiveTopicRESTCustomDataStringCallbackStore;
    private static CommunityCreatePermissionGroupStringCallback CommunityCreatePermissionGroupStringCallbackStore;
    private static CommunityDeletePermissionGroupStringCallback CommunityDeletePermissionGroupStringCallbackStore;
    private static CommunityChangePermissionGroupInfoStringCallback CommunityChangePermissionGroupInfoStringCallbackStore;
    private static CommunityAddMembersToPermissionGroupStringCallback CommunityAddMembersToPermissionGroupStringCallbackStore;
    private static CommunityRemoveMembersFromPermissionGroupStringCallback CommunityRemoveMembersFromPermissionGroupStringCallbackStore;
    private static CommunityAddTopicPermissionStringCallback CommunityAddTopicPermissionStringCallbackStore;
    private static CommunityDeleteTopicPermissionStringCallback CommunityDeleteTopicPermissionStringCallbackStore;
    private static CommunityModifyTopicPermissionStringCallback CommunityModifyTopicPermissionStringCallbackStore;
    private static ExperimentalNotifyStringCallback ExperimentalNotifyStringCallbackStore;


    // SetOnDeleteFriendCallback user_data is wrongly transmitted from the Native callback.
    private static string SetOnDeleteFriendCallbackUser_Data;
    private static bool needLog;

    public static void CallExperimentalAPICallback(int code, string desc, string data, string user_data)
    {
      Utils.Log("Tencent Cloud IM add config success .");
    }

    static void Log(string user_data, params object[] args)
    {
      if (!needLog)
      {
        return;
      }
      if (args.Length == 0)
      {
        return;
      }
      try
      {
        var prefix = "tencent-chat-unity-sdk: ";
        if (args[args.Length - 1].ToString() == "tencent-chat-unity-sdk-res")
        {
          prefix = "tencent-chat-unity-sdk-res: ";
          args = args.Take(args.Length - 1).ToArray();
        }

        // Convert obj to string
        args = args.Select(arg => arg.ToString()).ToArray();

        var param = new ExperimentalAPIRequestParam
        {
          request_internal_operation = TIMInternalOperation.internal_operation_write_log.ToString(),
          request_write_log_log_level_param = (int)TIMLogLevel.kTIMLog_Info,
          request_write_log_log_content_param = String.Join(",", args),
          request_write_log_func_name_param = prefix + user_data
        };
        int res = IMNativeSDK.callExperimentalAPI(Utils.string2intptr(Utils.ToJson(param)), null, Utils.string2intptr(""));
      }
      catch (Exception e)
      {
        Utils.Log("Log Error" + e.ToString());
      }
    }

    /// <summary>
    /// 初始化IM SDK
    /// Init IM SDK
    /// </summary>
    /// <param name="sdk_app_id">sdk_app_id，在腾讯云即时通信 IM控制台创建应用后获得 (sdk_app_id is automatically generated after create an IM instance on the IM Console)</param>
    /// <param name="json_sdk_config"><see cref="SdkConfig"/></param>
    /// <param name="need_log">是否打印出如参数进日志 (Need callback log)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult Init(long sdk_app_id, SdkConfig json_sdk_config, bool? need_log = false)
    {
      ExperimentalAPIRequestParam param = new ExperimentalAPIRequestParam();

      param.request_internal_operation = TIMInternalOperation.internal_operation_set_ui_platform.ToString();

      param.request_set_ui_platform_param = 5;
      TIMResult res = CallExperimentalAPI(param, CallExperimentalAPICallback);

      string configString = Utils.ToJson(json_sdk_config);
      needLog = (bool)need_log;
      Utils.Log(configString);

      string sdkVersion = GetSDKVersion();

      Utils.Log("Current Native SDK Version: " + sdkVersion);

      int timSucc = IMNativeSDK.TIMInit(sdk_app_id, Utils.string2intptr(configString));
      return (TIMResult)timSucc;
    }
    /// <summary>
    /// 反初始化IM SDK
    /// Uninit IM SDK
    /// </summary>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult Uninit()
    {


      RemoveRecvNewMsgCallback();
      SetConvEventCallback(null);

      SetConvTotalUnreadMessageCountChangedCallback(null);

      SetFriendAddRequestCallback(null);

      SetFriendApplicationListDeletedCallback(null);

      SetFriendApplicationListReadCallback(null);

      SetFriendBlackListAddedCallback(null);

      SetFriendBlackListDeletedCallback(null);

      SetGroupAttributeChangedCallback(null);

      SetGroupTipsEventCallback(null);

      SetKickedOfflineCallback(null);

      SetLogCallback(null);

      SetMsgElemUploadProgressCallback(null);

      SetMsgReadedReceiptCallback(null);
      SetMsgReactionsChangedCallback(null);
      SetMsgAllMessageReceiveOptionCallback(null);

      SetMsgRevokeCallback(null);

      SetMsgUpdateCallback(null);

      SetNetworkStatusListenerCallback(null);

      SetOnAddFriendCallback(null);

      SetOnDeleteFriendCallback(null);

      SetUpdateFriendProfileCallback(null);

      SetUserSigExpiredCallback(null);

      int timSucc = IMNativeSDK.TIMUninit();

      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 登陆
    /// Login
    /// </summary>
    /// <param name="user_id">用户ID (user ID)</param>
    /// <param name="user_sig">通过sdk_app_id与secret生成，可参考 https://cloud.tencent.com/document/product/269/32688 (Generated by sdk_app_id and secret, see https://www.tencentcloud.com/document/product/1047/34385)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult Login(string user_id, string user_sig, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      int res = IMNativeSDK.TIMLogin(Utils.string2intptr(user_id), Utils.string2intptr(user_sig), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, user_id, user_sig);
      return (TIMResult)res;
    }
    public static TIMResult Login(string user_id, string user_sig, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMLogin(Utils.string2intptr(user_id), Utils.string2intptr(user_sig), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, user_id, user_sig);
      return (TIMResult)res;
    }

    /// <summary>
    /// 获取SDK底层库版本
    /// Get native SDK version
    /// </summary>
    /// <returns>string version</returns>
    public static string GetSDKVersion()
    {
      IntPtr version = IMNativeSDK.TIMGetSDKVersion();
      return Utils.intptr2string(version);
    }

    /// <summary>
    /// 设置全局配置
    /// Set SDK config
    /// </summary>
    /// <param name="config">配置 (Config)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult SetConfig(SetConfig config, ValueCallback<SetConfig> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var configStr = Utils.ToJson(config);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<SetConfig>);

      int res = IMNativeSDK.TIMSetConfig(Utils.string2intptr(configStr), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, configStr);
      return (TIMResult)res;
    }
    public static TIMResult SetConfig(SetConfig config, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var configStr = Utils.ToJson(config);

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMSetConfig(Utils.string2intptr(configStr), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, configStr);
      return (TIMResult)res;
    }

    /// <summary>
    /// 获取服务端时间（秒）
    /// Get server time (second)
    /// </summary>
    /// <returns>服务器时间 (Server time)</returns>
    public static long GetServerTime()
    {
      return IMNativeSDK.TIMGetServerTime();
    }

    /// <summary>
    /// 退出登陆
    /// Logout
    /// </summary>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult Logout(NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      int timSucc = IMNativeSDK.TIMLogout(ValueCallbackInstance, Utils.string2intptr(user_data));

      return (TIMResult)timSucc;
    }
    public static TIMResult Logout(ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      int timSucc = IMNativeSDK.TIMLogout(StringValueCallbackInstance, Utils.string2intptr(user_data));

      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 获取当前登陆状态
    /// Get login status
    /// </summary>
    /// <returns>TIMLoginStatus</returns>
    public static TIMLoginStatus GetLoginStatus()
    {
      int timSucc = IMNativeSDK.TIMGetLoginStatus();
      return (TIMLoginStatus)timSucc;
    }

#if !UNITY_EDITOR && UNITY_WEBGL
    /// <summary>
    /// 获取当前登陆用户ID (WebGL only)
    /// Get login user ID (WebGL only)
    /// </summary>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult GetLoginUserIDWeb(ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();
      ValuecallbackStore.Add(user_data, callback);
      var timSucc = IMWebSDK.TIMGetLoginUserIDWeb(StringValueCallbackInstance, Utils.string2intptr(user_data));
      return (TIMResult)timSucc;
    }
#endif
    /// <summary>
    /// 获取当前登陆用户ID
    /// Get login user ID
    /// </summary>
    /// <param name="user_id">用户与接收user_id的StringBuilder (StringBuilder for receiving user_id)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult GetLoginUserID(StringBuilder user_id)
    {
      int timSucc = IMNativeSDK.TIMGetLoginUserID(user_id);
      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 获取会话列表
    /// Get conversation list
    /// </summary>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult ConvGetConvList(ValueCallback<List<ConvInfo>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<ConvInfo>>);

      int timSucc = IMNativeSDK.TIMConvGetConvList(ValueCallbackInstance, Utils.string2intptr(user_data));

      return (TIMResult)timSucc;
    }
    public static TIMResult ConvGetConvList(ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      int timSucc = IMNativeSDK.TIMConvGetConvList(StringValueCallbackInstance, Utils.string2intptr(user_data));

      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 删除会话
    /// Delete conversation
    /// </summary>
    /// <param name="conv_id">会话ID，c2c会话为user_id，群会话为group_id (Conversation ID, C2C conv: user_id, Group conv: group_id)</param>
    /// <param name="conv_type">会话类型 TIMConvType (Conversation type)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult ConvDelete(string conv_id, TIMConvType conv_type, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      int timSucc = IMNativeSDK.TIMConvDelete(Utils.string2intptr(conv_id), (int)conv_type, ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, conv_id, conv_type.ToString());
      return (TIMResult)timSucc;
    }
    public static TIMResult ConvDelete(string conv_id, TIMConvType conv_type, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      int timSucc = IMNativeSDK.TIMConvDelete(Utils.string2intptr(conv_id), (int)conv_type, StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, conv_id, conv_type.ToString());
      return (TIMResult)timSucc;
    }

    ///<summary>
    ///删除会话列表
    /// </summary>
    ///<param name="conversation_id_array">会话 ID 列表(list of conversation id)</param>
    ///<param name="clearMessage">是否删除会话中的消息；设置为 false 时，保留会话消息；设置为 true 时，本地和服务器的消息会一起删除，并且不可恢复(Whether to delete messages in the session; when set to false, session messages are retained; when set to true, local and server messages will be deleted together and cannot be recovered)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult ConvDeleteConversationList(List<string> conversation_id_array, bool clearMessage, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(conversation_id_array);
      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      int timSucc = IMNativeSDK.TIMConvDeleteConversationList(Utils.string2intptr(param), (bool)clearMessage, ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, conversation_id_array.ToString(), clearMessage.ToString());
      return (TIMResult)timSucc;
    }

    public static TIMResult ConvDeleteConversationList(List<string> conversation_id_array, bool clearMessage, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(conversation_id_array);
      ValuecallbackStore.Add(user_data, callback);

      int timSucc = IMNativeSDK.TIMConvDeleteConversationList(Utils.string2intptr(param), (bool)clearMessage, StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, conversation_id_array.ToString(), clearMessage.ToString());
      return (TIMResult)timSucc;
    }
    /// <summary>
    /// 获取会话信息
    /// Get conversation info
    /// </summary>
    /// <param name="conv_list_param">获取会话列表参数 ConvParam列表 (List of get conversation info params)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult ConvGetConvInfo(List<ConvParam> conv_list_param, ValueCallback<List<ConvInfo>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(conv_list_param);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<ConvInfo>>);

      int timSucc = IMNativeSDK.TIMConvGetConvInfo(Utils.string2intptr(param), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)timSucc;
    }
    public static TIMResult ConvGetConvInfo(List<ConvParam> conv_list_param, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(conv_list_param);

      ValuecallbackStore.Add(user_data, callback);

      int timSucc = IMNativeSDK.TIMConvGetConvInfo(Utils.string2intptr(param), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 设置会话草稿
    /// Set conversation draft
    /// </summary>
    /// <param name="conv_id">会话ID (Conversation ID)</param>
    /// <param name="conv_type">会话类型 TIMConvType (Conversation type)</param>
    /// <param name="param">DraftParam</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult ConvSetDraft(string conv_id, TIMConvType conv_type, DraftParam param)
    {

      int timSucc = IMNativeSDK.TIMConvSetDraft(conv_id, (int)conv_type, Utils.string2intptr(Utils.ToJson(param)));

      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 取消会话草稿
    /// Cancel conversation draft
    /// </summary>
    /// <param name="conv_id">会话ID (Conversation ID)</param>
    /// <param name="conv_type">会话类型 TIMConvType (Conversation type)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult ConvCancelDraft(string conv_id, TIMConvType conv_type)
    {

      int timSucc = IMNativeSDK.TIMConvCancelDraft(conv_id, (int)conv_type);

      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 会话置顶
    /// Pin conversation
    /// </summary>
    /// <param name="conv_id">会话ID (Conversation ID)</param>
    /// <param name="conv_type">会话类型 TIMConvType (Conversation type)</param>
    /// <param name="is_pinned">是否置顶标记 (Is pinned)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult ConvPinConversation(string conv_id, TIMConvType conv_type, bool is_pinned, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      int timSucc = IMNativeSDK.TIMConvPinConversation(conv_id, (int)conv_type, is_pinned, ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, conv_id, conv_type.ToString(), is_pinned.ToString());
      return (TIMResult)timSucc;
    }
    public static TIMResult ConvPinConversation(string conv_id, TIMConvType conv_type, bool is_pinned, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      int timSucc = IMNativeSDK.TIMConvPinConversation(conv_id, (int)conv_type, is_pinned, StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, conv_id, conv_type.ToString(), is_pinned.ToString());
      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 获取全部会话未读数
    /// Get total unread message count
    /// </summary>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult ConvGetTotalUnreadMessageCount(ValueCallback<GetTotalUnreadNumberResult> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<GetTotalUnreadNumberResult>);

      int timSucc = IMNativeSDK.TIMConvGetTotalUnreadMessageCount(ValueCallbackInstance, Utils.string2intptr(user_data));

      return (TIMResult)timSucc;
    }
    public static TIMResult ConvGetTotalUnreadMessageCount(ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      int timSucc = IMNativeSDK.TIMConvGetTotalUnreadMessageCount(StringValueCallbackInstance, Utils.string2intptr(user_data));

      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 根据 filter 获取未读总数（7.0 及以上版本支持）
    /// Get unread message count by filter (^7.0)
    /// </summary>
    /// <param name="filter">会话 filter (Conversation list filter)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult ConvGetUnreadMessageCountByFilter(ConversationListFilter filter, ValueCallback<GetTotalUnreadNumberResult> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      return HandleGetUnreadMessageCountByFilter(filter, fn_name, callback, null);
    }
    public static TIMResult ConvGetUnreadMessageCountByFilter(ConversationListFilter filter, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      return HandleGetUnreadMessageCountByFilter(filter, fn_name, null, callback);
    }

    private static TIMResult HandleGetUnreadMessageCountByFilter(ConversationListFilter filter, string fn_name, ValueCallback<GetTotalUnreadNumberResult> callback = null, ValueCallback<string> strCallback = null)
    {
      string user_data = Utils.GetUserDataFromFnName(fn_name);
      int timSucc;
      var filterStr = Utils.ToJson(filter);
      var filterPtr = Utils.string2intptr(filterStr);

      if (callback != null)
      {
        ValuecallbackDeleStore.Add(user_data, threadOperation<GetTotalUnreadNumberResult>);
        ValuecallbackStore.Add(user_data, callback);
        timSucc = IMNativeSDK.TIMConvGetUnreadMessageCountByFilter(filterPtr, ValueCallbackInstance, Utils.string2intptr(user_data));
      }
      else
      {
        ValuecallbackStore.Add(user_data, strCallback);
        timSucc = IMNativeSDK.TIMConvGetUnreadMessageCountByFilter(filterPtr, StringValueCallbackInstance, Utils.string2intptr(user_data));
      }

      Log(user_data, filterStr);
      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 注册监听指定 filter 的会话未读总数变化（7.0 及以上版本支持）
    /// Subscribe unread message count by filter (^7.0)
    /// </summary>
    /// <param name="filter">会话 filter (Conversation list filter)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult ConvSubscribeUnreadMessageCountByFilter(ConversationListFilter filter)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string filterStr = Utils.ToJson(filter);

      var filterPtr = Utils.string2intptr(filterStr);

      int timSucc = IMNativeSDK.TIMConvSubscribeUnreadMessageCountByFilter(filterPtr);

      Log(fn_name, filterStr);

      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 取消监听指定 filter 的会话未读总数变化（7.0 及以上版本支持）
    /// Unsubscribe unread message count by filter (^7.0)
    /// </summary>
    /// <param name="filter">会话 filter (Conversation list filter)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult ConvUnsubscribeUnreadMessageCountByFilter(ConversationListFilter filter)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string filterStr = Utils.ToJson(filter);

      var filterPtr = Utils.string2intptr(filterStr);

      int timSucc = IMNativeSDK.TIMConvUnsubscribeUnreadMessageCountByFilter(filterPtr);

      Log(fn_name, filterStr);

      return (TIMResult)timSucc;
    }

    /// <summary>
    /// </summary>
    public static TIMResult ConvCleanConversationUnreadMessageCount(string conversation_id, ulong clean_timestamp, ulong clean_sequence, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      int timSucc = IMNativeSDK.TIMConvCleanConversationUnreadMessageCount(Utils.string2intptr(conversation_id), clean_timestamp, clean_sequence, ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, conversation_id, clean_timestamp, clean_sequence);
      return (TIMResult)timSucc;
    }
    public static TIMResult ConvCleanConversationUnreadMessageCount(string conversation_id, ulong clean_timestamp, ulong clean_sequence, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      ValuecallbackStore.Add(user_data, callback);

      int timSucc = IMNativeSDK.TIMConvCleanConversationUnreadMessageCount(Utils.string2intptr(conversation_id), clean_timestamp, clean_sequence, StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, conversation_id, clean_timestamp, clean_sequence);
      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 创建会话分组（从 6.5 版本开始支持，需要您购买旗舰版套餐）
    /// Create conversation group (^6.5, Premium ver. package only)
    /// </summary>
    /// <param name="group_name">分组名 (Group name)</param>
    /// <param name="conversation_id_array">会话 ID 列表 (Conversation ID array)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult ConvCreateConversationGroup(string group_name, List<string> conversation_id_array, ValueCallback<List<ConversationOperationResult>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      return HandleCreateConvGroup(group_name, conversation_id_array, fn_name, callback, null);
    }
    public static TIMResult ConvCreateConversationGroup(string group_name, List<string> conversation_id_array, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      return HandleCreateConvGroup(group_name, conversation_id_array, fn_name, null, callback);
    }

    private static TIMResult HandleCreateConvGroup(string group_name, List<string> conversation_id_array, string fn_name, ValueCallback<List<ConversationOperationResult>> callback = null, ValueCallback<string> strCallback = null)
    {
      string user_data = Utils.GetUserDataFromFnName(fn_name);
      int timSucc;
      var groupNamePtr = Utils.string2intptr(group_name);
      var conversationIdArrayStr = Utils.ToJson(conversation_id_array);
      var conversationIdArrayPtr = Utils.string2intptr(conversationIdArrayStr);

      if (callback != null)
      {
        ValuecallbackDeleStore.Add(user_data, threadOperation<List<ConversationOperationResult>>);
        ValuecallbackStore.Add(user_data, callback);
        timSucc = IMNativeSDK.TIMConvCreateConversationGroup(groupNamePtr, conversationIdArrayPtr, ValueCallbackInstance, Utils.string2intptr(user_data));
      }
      else
      {
        ValuecallbackStore.Add(user_data, strCallback);
        timSucc = IMNativeSDK.TIMConvCreateConversationGroup(groupNamePtr, conversationIdArrayPtr, StringValueCallbackInstance, Utils.string2intptr(user_data));
      }

      Log(user_data, group_name, conversationIdArrayStr);
      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 添加会话到一个会话分组（从 6.5 版本开始支持，需要您购买旗舰版套餐）
    /// Add the conversation to an group (^6.5, Premium ver. package only)
    /// </summary>
    /// <param name="group_name">分组名 (Group name)</param>
    /// <param name="conversation_id_array">会话 ID 列表 (Conversation ID array)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult ConvAddConversationsToGroup(string group_name, List<string> conversation_id_array, ValueCallback<List<ConversationOperationResult>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      return HandleAddConvToGroup(group_name, conversation_id_array, fn_name, callback, null);
    }
    public static TIMResult ConvAddConversationsToGroup(string group_name, List<string> conversation_id_array, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      return HandleAddConvToGroup(group_name, conversation_id_array, fn_name, null, callback);
    }

    private static TIMResult HandleAddConvToGroup(string group_name, List<string> conversation_id_array, string fn_name, ValueCallback<List<ConversationOperationResult>> callback = null, ValueCallback<string> strCallback = null)
    {
      string user_data = Utils.GetUserDataFromFnName(fn_name);
      int timSucc;
      var groupNamePtr = Utils.string2intptr(group_name);
      var conversationIdArrayStr = Utils.ToJson(conversation_id_array);
      var conversationIdArrayPtr = Utils.string2intptr(conversationIdArrayStr);

      if (callback != null)
      {
        ValuecallbackDeleStore.Add(user_data, threadOperation<List<ConversationOperationResult>>);
        ValuecallbackStore.Add(user_data, callback);
        timSucc = IMNativeSDK.TIMConvAddConversationsToGroup(groupNamePtr, conversationIdArrayPtr, ValueCallbackInstance, Utils.string2intptr(user_data));
      }
      else
      {
        ValuecallbackStore.Add(user_data, strCallback);
        timSucc = IMNativeSDK.TIMConvAddConversationsToGroup(groupNamePtr, conversationIdArrayPtr, StringValueCallbackInstance, Utils.string2intptr(user_data));
      }

      Log(user_data, group_name, conversationIdArrayStr);
      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 从会话分组中删除多个会话（从 6.5 版本开始支持，需要您购买旗舰版套餐）
    /// Delete conversations from an group (^6.5, Premium ver. package only)
    /// </summary>
    /// <param name="group_name">分组名 (Group name)</param>
    /// <param name="conversation_id_array">会话 ID 列表 (Conversation ID array)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult ConvDeleteConversationsFromGroup(string group_name, List<string> conversation_id_array, ValueCallback<List<ConversationOperationResult>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      return HandleDeleteConvsFromGroup(group_name, conversation_id_array, fn_name, callback, null);
    }
    public static TIMResult ConvDeleteConversationsFromGroup(string group_name, List<string> conversation_id_array, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      return HandleDeleteConvsFromGroup(group_name, conversation_id_array, fn_name, null, callback);
    }

    private static TIMResult HandleDeleteConvsFromGroup(string group_name, List<string> conversation_id_array, string fn_name, ValueCallback<List<ConversationOperationResult>> callback = null, ValueCallback<string> strCallback = null)
    {
      string user_data = Utils.GetUserDataFromFnName(fn_name);
      int timSucc;
      var groupNamePtr = Utils.string2intptr(group_name);
      var conversationIdArrayStr = Utils.ToJson(conversation_id_array);
      var conversationIdArrayPtr = Utils.string2intptr(conversationIdArrayStr);

      if (callback != null)
      {
        ValuecallbackDeleStore.Add(user_data, threadOperation<List<ConversationOperationResult>>);
        ValuecallbackStore.Add(user_data, callback);
        timSucc = IMNativeSDK.TIMConvDeleteConversationsFromGroup(groupNamePtr, conversationIdArrayPtr, ValueCallbackInstance, Utils.string2intptr(user_data));
      }
      else
      {
        ValuecallbackStore.Add(user_data, strCallback);
        timSucc = IMNativeSDK.TIMConvDeleteConversationsFromGroup(groupNamePtr, conversationIdArrayPtr, StringValueCallbackInstance, Utils.string2intptr(user_data));
      }

      Log(user_data, group_name, conversationIdArrayStr);
      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 删除会话分组（从 6.5 版本开始支持，需要您购买旗舰版套餐）
    /// Delete conversation group (^6.5, Premium ver. package only)
    /// </summary>
    /// <param name="group_name">分组名 (Group name)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult ConvDeleteConversationGroup(string group_name, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      return HandleDeleteConvGroup(group_name, fn_name, callback, null);
    }
    public static TIMResult ConvDeleteConversationGroup(string group_name, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      return HandleDeleteConvGroup(group_name, fn_name, null, callback);
    }

    private static TIMResult HandleDeleteConvGroup(string group_name, string fn_name, NullValueCallback callback = null, ValueCallback<string> strCallback = null)
    {
      string user_data = Utils.GetUserDataFromFnName(fn_name);
      int timSucc;
      var groupNamePtr = Utils.string2intptr(group_name);

      if (callback != null)
      {
        ValuecallbackDeleStore.Add(user_data, threadOperation<object>);
        ValuecallbackStore.Add(user_data, callback);
        timSucc = IMNativeSDK.TIMConvDeleteConversationGroup(groupNamePtr, ValueCallbackInstance, Utils.string2intptr(user_data));
      }
      else
      {
        ValuecallbackStore.Add(user_data, strCallback);
        timSucc = IMNativeSDK.TIMConvDeleteConversationGroup(groupNamePtr, StringValueCallbackInstance, Utils.string2intptr(user_data));
      }

      Log(user_data, group_name);
      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 获取会话分组列表（从 6.5 版本开始支持，需要您购买旗舰版套餐）
    /// Get conversation group (^6.5, Premium ver. package only)
    /// </summary>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult ConvGetConversationGroupList(ValueCallback<List<string>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      return HandleGetConvGroupList(fn_name, callback, null);
    }
    public static TIMResult ConvGetConversationGroupList(ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      return HandleGetConvGroupList(fn_name, null, callback);
    }

    private static TIMResult HandleGetConvGroupList(string fn_name, ValueCallback<List<string>> callback = null, ValueCallback<string> strCallback = null)
    {
      string user_data = Utils.GetUserDataFromFnName(fn_name);
      int timSucc;

      if (callback != null)
      {
        ValuecallbackDeleStore.Add(user_data, threadOperation<List<string>>);
        ValuecallbackStore.Add(user_data, callback);
        timSucc = IMNativeSDK.TIMConvGetConversationGroupList(ValueCallbackInstance, Utils.string2intptr(user_data));
      }
      else
      {
        ValuecallbackStore.Add(user_data, strCallback);
        timSucc = IMNativeSDK.TIMConvGetConversationGroupList(StringValueCallbackInstance, Utils.string2intptr(user_data));
      }

      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 获取会话列表高级接口（从 6.5 版本开始支持）
    /// Get conversation list by filter (^6.5)
    /// </summary>
    /// <param name="filter">获取会话列表高级接口的 filter (Get conversation filter)</param>
    /// <param name="next_seq">分页拉取的游标 (Next seq)</param>
    /// <param name="count">分页拉取的个数 (Page count)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult ConvGetConversationListByFilter(ConversationListFilter filter, ulong next_seq, uint count, ValueCallback<ConversationListResult> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      return HandleGetConvListByFilter(filter, next_seq, count, fn_name, callback, null);
    }
    public static TIMResult ConvGetConversationListByFilter(ConversationListFilter filter, ulong next_seq, uint count, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      return HandleGetConvListByFilter(filter, next_seq, count, fn_name, null, callback);
    }

    private static TIMResult HandleGetConvListByFilter(ConversationListFilter filter, ulong next_seq, uint count, string fn_name, ValueCallback<ConversationListResult> callback = null, ValueCallback<string> strCallback = null)
    {
      string user_data = Utils.GetUserDataFromFnName(fn_name);
      int timSucc;
      var filterStr = Utils.ToJson(filter);
      var filterPtr = Utils.string2intptr(filterStr);

      if (callback != null)
      {
        ValuecallbackDeleStore.Add(user_data, threadOperation<ConversationListResult>);
        ValuecallbackStore.Add(user_data, callback);
        timSucc = IMNativeSDK.TIMConvGetConversationListByFilter(filterPtr, next_seq, count, ValueCallbackInstance, Utils.string2intptr(user_data));
      }
      else
      {
        ValuecallbackStore.Add(user_data, strCallback);
        timSucc = IMNativeSDK.TIMConvGetConversationListByFilter(filterPtr, next_seq, count, StringValueCallbackInstance, Utils.string2intptr(user_data));
      }

      Log(user_data, filterStr);
      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 标记会话（从 6.5 版本开始支持，需要您购买旗舰版套餐）
    /// Mark conversation (^6.5, Premium ver. package only)
    /// </summary>
    /// <param name="conversation_id_array">会话 ID 列表 (Conversation ID array)</param>
    /// <param name="mark_type">会话标记类型 (Mark type)</param>
    /// <param name="enable_mark">true：设置标记 false：取消标记 (Is enable mark)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult ConvMarkConversation(List<string> conversation_id_array, TIMConversationMarkType mark_type, bool enable_mark, ValueCallback<List<ConversationOperationResult>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      return HandleMarkConv(conversation_id_array, mark_type, enable_mark, fn_name, callback, null);
    }
    public static TIMResult ConvMarkConversation(List<string> conversation_id_array, TIMConversationMarkType mark_type, bool enable_mark, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      return HandleMarkConv(conversation_id_array, mark_type, enable_mark, fn_name, null, callback);
    }

    private static TIMResult HandleMarkConv(List<string> conversation_id_array, TIMConversationMarkType mark_type, bool enable_mark, string fn_name, ValueCallback<List<ConversationOperationResult>> callback = null, ValueCallback<string> strCallback = null)
    {
      string user_data = Utils.GetUserDataFromFnName(fn_name);
      int timSucc;
      var conversationIdArrayStr = Utils.ToJson(conversation_id_array);
      var conversationIdArrayPtr = Utils.string2intptr(conversationIdArrayStr);

      if (callback != null)
      {
        ValuecallbackDeleStore.Add(user_data, threadOperation<List<ConversationOperationResult>>);
        ValuecallbackStore.Add(user_data, callback);
        timSucc = IMNativeSDK.TIMConvMarkConversation(conversationIdArrayPtr, (long)mark_type, enable_mark, ValueCallbackInstance, Utils.string2intptr(user_data));
      }
      else
      {
        ValuecallbackStore.Add(user_data, strCallback);
        timSucc = IMNativeSDK.TIMConvMarkConversation(conversationIdArrayPtr, (long)mark_type, enable_mark, StringValueCallbackInstance, Utils.string2intptr(user_data));
      }

      Log(user_data, conversationIdArrayStr, mark_type.ToString(), enable_mark.ToString());
      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 重命名会话分组（从 6.5 版本开始支持，需要您购买旗舰版套餐）
    /// Rename conversation group (^6.5, Premium ver. package only)
    /// </summary>
    /// <param name="old_name">旧分组名 (Old group name)</param>
    /// <param name="new_name">新分组名 (New group name)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult ConvRenameConversationGroup(string old_name, string new_name, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      return HandleRenameConvGroup(old_name, new_name, fn_name, callback, null);
    }
    public static TIMResult ConvRenameConversationGroup(string old_name, string new_name, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      return HandleRenameConvGroup(old_name, new_name, fn_name, null, callback);
    }

    private static TIMResult HandleRenameConvGroup(string old_name, string new_name, string fn_name, NullValueCallback callback = null, ValueCallback<string> strCallback = null)
    {
      string user_data = Utils.GetUserDataFromFnName(fn_name);
      int timSucc;
      var oldNamePtr = Utils.string2intptr(old_name);
      var newNamePtr = Utils.string2intptr(new_name);

      if (callback != null)
      {
        ValuecallbackDeleStore.Add(user_data, threadOperation<object>);
        ValuecallbackStore.Add(user_data, callback);
        timSucc = IMNativeSDK.TIMConvRenameConversationGroup(oldNamePtr, newNamePtr, ValueCallbackInstance, Utils.string2intptr(user_data));
      }
      else
      {
        ValuecallbackStore.Add(user_data, strCallback);
        timSucc = IMNativeSDK.TIMConvRenameConversationGroup(oldNamePtr, newNamePtr, StringValueCallbackInstance, Utils.string2intptr(user_data));
      }

      Log(user_data, old_name, new_name);
      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 设置会话自定义数据（从 6.5 版本开始支持）
    /// Set conversation custom data (^6.5)
    /// </summary>
    /// <param name="conversation_id_array">会话 ID 列表 (Conversation ID array)</param>
    /// <param name="custom_data">自定义数据，最大支持 256 bytes (Custom data, maximum 256 bytes)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult ConvSetConversationCustomData(List<string> conversation_id_array, string custom_data, ValueCallback<List<ConversationOperationResult>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      return HandleSetConvCustomData(conversation_id_array, custom_data, fn_name, callback, null);
    }
    public static TIMResult ConvSetConversationCustomData(List<string> conversation_id_array, string custom_data, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      return HandleSetConvCustomData(conversation_id_array, custom_data, fn_name, null, callback);
    }

    private static TIMResult HandleSetConvCustomData(List<string> conversation_id_array, string custom_data, string fn_name, ValueCallback<List<ConversationOperationResult>> callback = null, ValueCallback<string> strCallback = null)
    {
      string user_data = Utils.GetUserDataFromFnName(fn_name);
      int timSucc;
      var customDataPtr = Utils.string2intptr(custom_data);
      var conversationIdArrayStr = Utils.ToJson(conversation_id_array);
      var conversationIdArrayPtr = Utils.string2intptr(conversationIdArrayStr);

      if (callback != null)
      {
        ValuecallbackDeleStore.Add(user_data, threadOperation<List<ConversationOperationResult>>);
        ValuecallbackStore.Add(user_data, callback);
        timSucc = IMNativeSDK.TIMConvSetConversationCustomData(conversationIdArrayPtr, customDataPtr, ValueCallbackInstance, Utils.string2intptr(user_data));
      }
      else
      {
        ValuecallbackStore.Add(user_data, strCallback);
        timSucc = IMNativeSDK.TIMConvSetConversationCustomData(conversationIdArrayPtr, customDataPtr, StringValueCallbackInstance, Utils.string2intptr(user_data));
      }

      Log(user_data, conversationIdArrayStr, custom_data);
      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 发送消息
    /// Send message
    /// </summary>
    /// <param name="conv_id">会话ID (Conversation ID)</param>
    /// <param name="conv_type">会话类型 TIMConvType (Conversation type)</param>
    /// <param name="message">消息体 Message</param>
    /// <param name="message_id">承接消息ID的StringBuilder (StringBuilder for receiving message ID)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult MsgSendMessage(string conv_id, TIMConvType conv_type, Message message, StringBuilder message_id, ValueCallback<Message> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var msg = Utils.ToJson(message);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<Message>);

      int timSucc = IMNativeSDK.TIMMsgSendMessage(conv_id, (int)conv_type, Utils.string2intptr(msg), message_id, ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, conv_id, conv_type.ToString(), msg);
      return (TIMResult)timSucc;
    }
    public static TIMResult MsgSendMessage(string conv_id, TIMConvType conv_type, Message message, StringBuilder message_id, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var msg = Utils.ToJson(message);

      ValuecallbackStore.Add(user_data, callback);
      int timSucc = IMNativeSDK.TIMMsgSendMessage(conv_id, (int)conv_type, Utils.string2intptr(msg), message_id, StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, conv_id, conv_type.ToString(), msg);
      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 取消消息发送
    /// Cancel sending message
    /// </summary>
    /// <param name="conv_id">会话ID (Conversation ID)</param>
    /// <param name="conv_type">会话类型 TIMConvType (Conversation type)</param>
    /// <param name="message_id">消息ID (Message ID)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult MsgCancelSend(string conv_id, TIMConvType conv_type, string message_id, ValueCallback<Message> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<Message>);

      int timSucc = IMNativeSDK.TIMMsgCancelSend(conv_id, (int)conv_type, Utils.string2intptr(message_id), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, conv_id, conv_type.ToString(), message_id);
      return (TIMResult)timSucc;
    }
    public static TIMResult MsgCancelSend(string conv_id, TIMConvType conv_type, string message_id, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      int timSucc = IMNativeSDK.TIMMsgCancelSend(conv_id, (int)conv_type, Utils.string2intptr(message_id), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, conv_id, conv_type.ToString(), message_id);
      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 从本地查找消息
    /// Search local message
    /// </summary>
    /// <param name="message_id_array">查找消息的id列表 (Message ID list for searching)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult MsgFindMessages(List<string> message_id_array, ValueCallback<List<Message>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var list = Utils.ToJson(message_id_array);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<Message>>);

      int timSucc = IMNativeSDK.TIMMsgFindMessages(Utils.string2intptr(list), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, list);
      return (TIMResult)timSucc;
    }
    public static TIMResult MsgFindMessages(List<string> message_id_array, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var list = Utils.ToJson(message_id_array);

      ValuecallbackStore.Add(user_data, callback);

      int timSucc = IMNativeSDK.TIMMsgFindMessages(Utils.string2intptr(list), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, list);
      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 消息已读上报 C2C
    /// Report message read (C2C)
    /// </summary>
    /// <param name="conv_id">会话ID (Conversation ID)</param>
    /// <param name="conv_type">会话类型 TIMConvType (Conversation type)</param>
    /// <param name="message">消息体 Message</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult MsgReportReaded(string conv_id, TIMConvType conv_type, Message message, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var msg = Utils.ToJson(message);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      int timSucc = IMNativeSDK.TIMMsgReportReaded(conv_id, (int)conv_type, Utils.string2intptr(msg), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, conv_id, conv_type.ToString(), msg);
      return (TIMResult)timSucc;
    }
    public static TIMResult MsgReportReaded(string conv_id, TIMConvType conv_type, Message message, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var msg = Utils.ToJson(message);

      ValuecallbackStore.Add(user_data, callback);

      int timSucc = IMNativeSDK.TIMMsgReportReaded(conv_id, (int)conv_type, Utils.string2intptr(msg), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, conv_id, conv_type.ToString(), msg);
      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 标记所有消息为已读
    /// Mark all message as read
    /// </summary>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult MsgMarkAllMessageAsRead(NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      int timSucc = IMNativeSDK.TIMMsgMarkAllMessageAsRead(ValueCallbackInstance, Utils.string2intptr(user_data));

      return (TIMResult)timSucc;
    }
    public static TIMResult MsgMarkAllMessageAsRead(ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      int timSucc = IMNativeSDK.TIMMsgMarkAllMessageAsRead(StringValueCallbackInstance, Utils.string2intptr(user_data));

      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 消息撤回
    /// Revoke message
    /// </summary>
    /// <param name="conv_id">会话ID (Conversation ID)</param>
    /// <param name="conv_type">会话类型 TIMConvType (Conversation type)</param>
    /// <param name="message">消息体 Message</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult MsgRevoke(string conv_id, TIMConvType conv_type, Message message, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var msg = Utils.ToJson(message);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      int timSucc = IMNativeSDK.TIMMsgRevoke(conv_id, (int)conv_type, Utils.string2intptr(msg), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, conv_id, conv_type.ToString(), msg);
      return (TIMResult)timSucc;
    }
    public static TIMResult MsgRevoke(string conv_id, TIMConvType conv_type, Message message, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var msg = Utils.ToJson(message);

      ValuecallbackStore.Add(user_data, callback);

      int timSucc = IMNativeSDK.TIMMsgRevoke(conv_id, (int)conv_type, Utils.string2intptr(msg), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, conv_id, conv_type.ToString(), msg);
      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 消息变更
    /// Modify message
    /// <para>如果消息修改成功，自己和对端用户（C2C）或群组成员（Group）都会收到 TIMMsgMessageModifiedCallback 回调。(If success, self and peer (C2C) or group member (Group) will receive TIMMsgMessageModifiedCallback.)</para>
    /// <para>如果在修改消息过程中，消息已经被其他人修改，cb 会返回 ERR_SDK_MSG_MODIFY_CONFLICT 错误。(If the message is modified during modificaion, cb will return error ERR_SDK_MSG_MODIFY_CONFLICT.)</para>
    /// <para>消息无论修改成功或则失败，cb 都会返回最新的消息对象。(Success or fail, cb will return the modified message)</para>
    /// </summary>
    /// <param name="message">需要变更的消息 (Modified message)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult MsgModifyMessage(Message message, ValueCallback<Message> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var msg = Utils.ToJson(message);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<Message>);

      int timSucc = IMNativeSDK.TIMMsgModifyMessage(Utils.string2intptr(msg), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, msg);
      return (TIMResult)timSucc;
    }
    public static TIMResult MsgModifyMessage(Message message, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var msg = Utils.ToJson(message);

      ValuecallbackStore.Add(user_data, callback);

      int timSucc = IMNativeSDK.TIMMsgModifyMessage(Utils.string2intptr(msg), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, msg);
      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 通过消息定位符查找消息
    /// Find Message by locator
    /// </summary>
    /// <param name="conv_id">会话ID (Conversation ID)</param>
    /// <param name="conv_type">会话类型 TIMConvType (Conversation type)</param>
    /// <param name="message_locator">消息定位符 List<MsgLocator></param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult MsgFindByMsgLocatorList(string conv_id, TIMConvType conv_type, List<MsgLocator> message_locator, ValueCallback<List<Message>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var loc = Utils.ToJson(message_locator);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<Message>>);

      int timSucc = IMNativeSDK.TIMMsgFindByMsgLocatorList(conv_id, (int)conv_type, Utils.string2intptr(loc), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, conv_id, conv_type.ToString(), loc);
      return (TIMResult)timSucc;
    }
    public static TIMResult MsgFindByMsgLocatorList(string conv_id, TIMConvType conv_type, List<MsgLocator> message_locator, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var loc = Utils.ToJson(message_locator);

      ValuecallbackStore.Add(user_data, callback);

      int timSucc = IMNativeSDK.TIMMsgFindByMsgLocatorList(conv_id, (int)conv_type, Utils.string2intptr(loc), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, conv_id, conv_type.ToString(), loc);
      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 导入消息
    /// Import message list
    /// </summary>
    /// <param name="conv_id">会话ID (Conversation ID)</param>
    /// <param name="conv_type">会话类型 TIMConvType (Conversation type)</param>
    /// <param name="message_list">消息列表 Message列表 (Message list)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult MsgImportMsgList(string conv_id, TIMConvType conv_type, List<Message> message_list, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var list = Utils.ToJson(message_list);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      int timSucc = IMNativeSDK.TIMMsgImportMsgList(conv_id, (int)conv_type, Utils.string2intptr(list), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, conv_id, conv_type.ToString(), list);
      return (TIMResult)timSucc;
    }
    public static TIMResult MsgImportMsgList(string conv_id, TIMConvType conv_type, List<Message> message_list, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var list = Utils.ToJson(message_list);

      ValuecallbackStore.Add(user_data, callback);

      int timSucc = IMNativeSDK.TIMMsgImportMsgList(conv_id, (int)conv_type, Utils.string2intptr(list), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, conv_id, conv_type.ToString(), list);
      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 保存消息
    /// Save message
    /// </summary>
    /// <param name="conv_id">会话ID (Conversation ID)</param>
    /// <param name="conv_type">会话类型 TIMConvType (Conversation type)</param>
    /// <param name="message">消息体 (Message)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult MsgSaveMsg(string conv_id, TIMConvType conv_type, Message message, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var msg = Utils.ToJson(message);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      int timSucc = IMNativeSDK.TIMMsgSaveMsg(conv_id, (int)conv_type, Utils.string2intptr(msg), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, conv_id, conv_type.ToString(), msg);
      return (TIMResult)timSucc;
    }
    public static TIMResult MsgSaveMsg(string conv_id, TIMConvType conv_type, Message message, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var msg = Utils.ToJson(message);

      ValuecallbackStore.Add(user_data, callback);

      int timSucc = IMNativeSDK.TIMMsgSaveMsg(conv_id, (int)conv_type, Utils.string2intptr(msg), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, conv_id, conv_type.ToString(), msg);
      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 获取历史消息列表
    /// Get history message list
    /// </summary>
    /// <param name="conv_id">会话ID (Conversation ID)</param>
    /// <param name="conv_type">会话类型 TIMConvType (Conversation type)</param>
    /// <param name="get_message_list_param">获取历史消息参数 MsgGetMsgListParam (Get history message list param)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult MsgGetMsgList(string conv_id, TIMConvType conv_type, MsgGetMsgListParam get_message_list_param, ValueCallback<List<Message>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data_getMsgList = fn_name + "_" + Utils.getRandomStr();

      if (get_message_list_param.msg_last_msg_id == null || get_message_list_param.msg_last_msg_id == "")
      {
        var param = Utils.ToJson(get_message_list_param);
        Log(user_data_getMsgList, "get_message_list_param", param);
        // UnityEngine.Debug.Log("List param in MsgGetMsgList here: "+ param);
        ValuecallbackStore.Add(user_data_getMsgList, callback);
        ValuecallbackDeleStore.Add(user_data_getMsgList, threadOperation<List<Message>>);
        int timSucc = IMNativeSDK.TIMMsgGetMsgList(conv_id, (int)conv_type, Utils.string2intptr(param), ValueCallbackInstance, Utils.string2intptr(user_data_getMsgList));

        Log(user_data_getMsgList, conv_id, conv_type.ToString(), param);
        return (TIMResult)timSucc;
      }
      List<string> messageID = new List<string>();
      messageID.Add(get_message_list_param.msg_last_msg_id);
      // UnityEngine.Debug.Log("List last message ID "+ get_message_list_param.msg_last_msg_id);
      Log(user_data_getMsgList, "last_message_id", get_message_list_param.msg_last_msg_id);
      TIMResult res = TencentIMSDK.MsgFindMessages(messageID, (int code, string desc, string callbackData, string user_data) =>
      {
        ValuecallbackStore.Add(user_data_getMsgList, callback);
        ValuecallbackDeleStore.Add(user_data_getMsgList, threadOperation<List<Message>>);
        if (callbackData != "[]")
        {

          // test.message_sender = "callback_here";
          // get_message_list_param.msg_getmsglist_param_last_msg = null;
          var param = Utils.ToJson(get_message_list_param);
          Log(user_data_getMsgList, "get_message_list_param", param);
          //  UnityEngine.Debug.Log("param1 "+param);
          string output = callbackData.Substring(1, callbackData.Length - 2);
          // UnityEngine.Debug.Log("output "+output);
          Log(user_data_getMsgList, "find_callbackData", callbackData);
          param = param.Substring(0, param.Length - 1);
          // UnityEngine.Debug.Log("substring "+param);
          param = param + ",\"msg_getmsglist_param_last_msg\":" + output + "}";
          // UnityEngine.Debug.Log("param2 "+param);
          Log(user_data_getMsgList, "getMsgList param with lastMessage", param);
          // UnityEngine.Debug.Log("List param in MsgGetMsgList here: "+ param);
          int timSucc = IMNativeSDK.TIMMsgGetMsgList(conv_id, (int)conv_type, Utils.string2intptr(param), ValueCallbackInstance, Utils.string2intptr(user_data_getMsgList));

          Log(user_data_getMsgList, conv_id, conv_type.ToString(), param);
        }
        else
        {
          // UnityEngine.Debug.Log("List no found "+ code);
          if (ValuecallbackDeleStore.TryGetValue(user_data_getMsgList, out SendOrPostCallback dele))
          {
            mainSyncContext.Post(dele, new CallbackConvert { code = code, type = "ValueCallback", data = "", user_data = user_data_getMsgList, desc = "message not found" });
          }
          Log(user_data_getMsgList, "message not found", code, desc, get_message_list_param.msg_last_msg_id);
        }

      });
      return (TIMResult)res;

    }
    public static TIMResult MsgGetMsgList(string conv_id, TIMConvType conv_type, MsgGetMsgListParam get_message_list_param, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data_getMsgList = fn_name + "_" + Utils.getRandomStr();
      if (get_message_list_param.msg_last_msg_id == null || get_message_list_param.msg_last_msg_id == "")
      {
        var param = Utils.ToJson(get_message_list_param);
        ValuecallbackStore.Add(user_data_getMsgList, callback);
        Log(user_data_getMsgList, "get_message_list_param", param);
        // UnityEngine.Debug.Log("param in MsgGetMsgList "+ param);
        int timSucc = IMNativeSDK.TIMMsgGetMsgList(conv_id, (int)conv_type, Utils.string2intptr(param), StringValueCallbackInstance, Utils.string2intptr(user_data_getMsgList));

        Log(user_data_getMsgList, conv_id, conv_type.ToString(), param);
        return (TIMResult)timSucc;
      }
      List<string> messageID = new List<string>();
      messageID.Add(get_message_list_param.msg_last_msg_id);
      // UnityEngine.Debug.Log("last message ID "+ get_message_list_param.msg_last_msg_id);
      Log(user_data_getMsgList, "last_message_id", get_message_list_param.msg_last_msg_id);
      TIMResult res = TencentIMSDK.MsgFindMessages(messageID, (int code, string desc, string callbackData, string user_data) =>
      {
        ValuecallbackStore.Add(user_data_getMsgList, callback);
        if (callbackData != "[]")
        {
          // UnityEngine.Debug.Log("callbackData "+ callbackData[0]);
          Log(user_data_getMsgList, "last_message", callbackData);
          var param = Utils.ToJson(get_message_list_param);
          Log(user_data_getMsgList, "get_message_list_param", param);
          string output = callbackData.Substring(1, callbackData.Length - 2);
          param = param.Substring(0, param.Length - 1);
          param = param + ",\"msg_getmsglist_param_last_msg\":" + output + "}";
          Log(user_data_getMsgList, "getMsgList param with lastMessage", param);

          // UnityEngine.Debug.Log("param in MsgGetMsgList(haslastmessage) "+ param);
          int timSucc = IMNativeSDK.TIMMsgGetMsgList(conv_id, (int)conv_type, Utils.string2intptr(param), StringValueCallbackInstance, Utils.string2intptr(user_data_getMsgList));

          Log(user_data_getMsgList, conv_id, conv_type.ToString(), param);
        }
        else
        {
          mainSyncContext.Post(threadOperation<string>, new CallbackConvert { code = code, type = "ValueCallback", data = "", user_data = user_data_getMsgList, desc = "message not found" });
          Log(user_data_getMsgList, "message not found", code, desc, get_message_list_param.msg_last_msg_id);
        }

      });
      return (TIMResult)res;
    }

    /// <summary>
    /// 消息删除
    /// Delete message
    /// </summary>
    /// <param name="conv_id">会话ID (Conversation ID)</param>
    /// <param name="conv_type">会话类型 TIMConvType (Conversation type)</param>
    /// <param name="message_delete_param">删除消息参数 MsgDeleteParam (Message deletion param)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult MsgDelete(string conv_id, TIMConvType conv_type, MsgDeleteParam message_delete_param, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(message_delete_param);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      int timSucc = IMNativeSDK.TIMMsgDelete(conv_id, (int)conv_type, Utils.string2intptr(param), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, conv_id, conv_type.ToString(), param);
      return (TIMResult)timSucc;
    }
    public static TIMResult MsgDelete(string conv_id, TIMConvType conv_type, MsgDeleteParam message_delete_param, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(message_delete_param);

      ValuecallbackStore.Add(user_data, callback);

      int timSucc = IMNativeSDK.TIMMsgDelete(conv_id, (int)conv_type, Utils.string2intptr(param), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, conv_id, conv_type.ToString(), param);
      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 消息删除
    /// Delete message list
    /// </summary>
    /// <param name="conv_id">会话ID (Conversation ID)</param>
    /// <param name="conv_type">会话类型 TIMConvType (Conversation type)</param>
    /// <param name="message_list">被删除消息列表 (Deleted message list)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult MsgListDelete(string conv_id, TIMConvType conv_type, List<Message> message_list, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var list = Utils.ToJson(message_list);


      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      int timSucc = IMNativeSDK.TIMMsgListDelete(conv_id, (int)conv_type, Utils.string2intptr(list), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, conv_id, conv_type.ToString(), list);
      return (TIMResult)timSucc;
    }
    public static TIMResult MsgListDelete(string conv_id, TIMConvType conv_type, List<Message> message_list, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var list = Utils.ToJson(message_list);


      ValuecallbackStore.Add(user_data, callback);

      int timSucc = IMNativeSDK.TIMMsgListDelete(conv_id, (int)conv_type, Utils.string2intptr(list), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, conv_id, conv_type.ToString(), list);
      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 清除历史消息
    /// Clear history message
    /// </summary>
    /// <param name="conv_id">会话ID (Conversation ID)</param>
    /// <param name="conv_type">会话类型 TIMConvType (Conversation type)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult MsgClearHistoryMessage(string conv_id, TIMConvType conv_type, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      int timSucc = IMNativeSDK.TIMMsgClearHistoryMessage(conv_id, (int)conv_type, ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, conv_id, conv_type.ToString());
      return (TIMResult)timSucc;
    }
    public static TIMResult MsgClearHistoryMessage(string conv_id, TIMConvType conv_type, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      int timSucc = IMNativeSDK.TIMMsgClearHistoryMessage(conv_id, (int)conv_type, StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, conv_id, conv_type.ToString());
      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 设置收消息选项
    /// Set C2C receiving message option
    /// </summary>
    /// <param name="user_id_list">用户ID列表 (User ID list)</param>
    /// <param name="opt">接收消息选项 TIMReceiveMessageOpt (Receiving message option)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult MsgSetC2CReceiveMessageOpt(List<string> user_id_list, TIMReceiveMessageOpt opt, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var list = Utils.ToJson(user_id_list);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      int timSucc = IMNativeSDK.TIMMsgSetC2CReceiveMessageOpt(Utils.string2intptr(list), (int)opt, ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, list, opt.ToString());
      return (TIMResult)timSucc;
    }
    public static TIMResult MsgSetC2CReceiveMessageOpt(List<string> user_id_list, TIMReceiveMessageOpt opt, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var list = Utils.ToJson(user_id_list);

      ValuecallbackStore.Add(user_data, callback);

      int timSucc = IMNativeSDK.TIMMsgSetC2CReceiveMessageOpt(Utils.string2intptr(list), (int)opt, StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, list, opt.ToString());
      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 获取C2C收消息选项
    /// Get C2C receiving message option
    /// </summary>
    /// <param name="user_id_list">用户ID列表 (user ID list)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult MsgGetC2CReceiveMessageOpt(List<string> user_id_list, ValueCallback<List<GetC2CRecvMsgOptResult>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var list = Utils.ToJson(user_id_list);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<GetC2CRecvMsgOptResult>>);

      int timSucc = IMNativeSDK.TIMMsgGetC2CReceiveMessageOpt(Utils.string2intptr(list), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, list);
      return (TIMResult)timSucc;
    }
    public static TIMResult MsgGetC2CReceiveMessageOpt(List<string> user_id_list, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var list = Utils.ToJson(user_id_list);

      ValuecallbackStore.Add(user_data, callback);

      int timSucc = IMNativeSDK.TIMMsgGetC2CReceiveMessageOpt(Utils.string2intptr(list), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, list);
      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 设置群收消息选项
    /// Set group receiving message option
    /// </summary>
    /// <param name="group_id">群组ID (group ID)</param>
    /// <param name="opt">接收消息选项 TIMReceiveMessageOpt (Receiving message option)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult MsgSetGroupReceiveMessageOpt(string group_id, TIMReceiveMessageOpt opt, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      int timSucc = IMNativeSDK.TIMMsgSetGroupReceiveMessageOpt(Utils.string2intptr(group_id), (int)opt, ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, group_id, opt.ToString());
      return (TIMResult)timSucc;
    }
    public static TIMResult MsgSetGroupReceiveMessageOpt(string group_id, TIMReceiveMessageOpt opt, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      int timSucc = IMNativeSDK.TIMMsgSetGroupReceiveMessageOpt(Utils.string2intptr(group_id), (int)opt, StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, group_id, opt.ToString());
      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 设置登录用户全局消息接收选项
    /// Set receiving message option
    /// </summary>
    /// <param name="opt">全局消息接收选项设置TIMReceiveMessageOpt (Receiving message option)</param>
    /// <param name="start_hour">免打扰开始时间：小时，取值范围[0 - 23](Do not disturb start time: hours)</param>
    /// <param name="start_minute">免打扰开始时间：分钟，取值范围[0 - 59](Do not disturb start time: minute)</param>
    /// <param name="start_second">免打扰开始时间：秒，取值范围[0 - 59](Do not disturb start time: second)</param>
    /// <param name="duration">免打扰持续时长：单位：秒，取值范围 [0 - 24*60*60](DND duration: unit: seconds)</param>
    /// <param name="callback"></param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult MsgSetAllReceiveMessageOpt(TIMReceiveMessageOpt opt, int start_hour, int start_minute, int start_second, int duration, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);
      int timSucc = IMNativeSDK.TIMMsgSetAllReceiveMessageOpt((int)opt, start_hour, start_minute, start_second, duration, ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, opt.ToString());
      return (TIMResult)timSucc;
    }

    public static TIMResult MsgSetAllReceiveMessageOpt(TIMReceiveMessageOpt opt, int start_hour, int start_minute, int start_second, int duration, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      int timSucc = IMNativeSDK.TIMMsgSetAllReceiveMessageOpt((int)opt, start_hour, start_minute, start_second, duration, StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, opt.ToString());
      return (TIMResult)timSucc;
    }
    /// <summary>
    /// 设置登录用户全局消息接收选项
    /// Set receiving message option
    /// </summary>
    /// <param name="opt">全局消息接收选项设置TIMReceiveMessageOpt (Receiving message option)</param>
    /// <param name="start_time_stamp">免打扰开始时间，UTC 时间戳，单位：秒(DND start time, UTC timestamp, unit: seconds)</param>
    /// <param name="duration">免打扰持续时长：单位：秒(Do not disturb duration: unit: seconds)</param>
    /// <param name="callback"></param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult MsgSetAllReceiveMessageOpt2(TIMReceiveMessageOpt opt, int start_time_stamp, int duration, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);
      int timSucc = IMNativeSDK.TIMMsgSetAllReceiveMessageOpt2((int)opt, start_time_stamp, duration, ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, opt.ToString());
      return (TIMResult)timSucc;
    }

    public static TIMResult MsgSetAllReceiveMessageOpt2(TIMReceiveMessageOpt opt, int start_time_stamp, int duration, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      int timSucc = IMNativeSDK.TIMMsgSetAllReceiveMessageOpt2((int)opt, start_time_stamp, duration, StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, opt.ToString());
      return (TIMResult)timSucc;
    }

    public static TIMResult MsgGetAllReceiveMessageOpt(ValueCallback<List<ReceiveMessageOptInfo>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<ReceiveMessageOptInfo>>);
      int timSucc = IMNativeSDK.TIMMsgGetAllReceiveMessageOpt(ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)timSucc;
    }
    public static TIMResult MsgGetAllReceiveMessageOpt(ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      int timSucc = IMNativeSDK.TIMMsgGetAllReceiveMessageOpt(StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 设置离线推送配置信息（iOS 和 Android 平台专用）
    /// Set offline push token (Only for iOS and Android)
    /// </summary>
    /// <param name="json_token">OfflinePushToken</param>
    /// <param name="callback">ValueCallback</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult MsgSetOfflinePushToken(OfflinePushToken json_token, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_token);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      int timSucc = IMNativeSDK.TIMMsgSetOfflinePushToken(Utils.string2intptr(param), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)timSucc;
    }
    public static TIMResult MsgSetOfflinePushToken(OfflinePushToken json_token, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_token);

      ValuecallbackStore.Add(user_data, callback);

      int timSucc = IMNativeSDK.TIMMsgSetOfflinePushToken(Utils.string2intptr(param), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)timSucc;
    }

    /// <summary>
    /// APP 检测到应用退后台时可以调用此接口，可以用作桌面应用角标的初始化未读数量（iOS 和 Android 平台专用）。
    /// Call this when APP works in background, you can set unread count for your APP (iOS & Android only)
    /// </summary>
    /// <param name="unread_count">unread_count</param>
    /// <param name="callback">ValueCallback</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult MsgDoBackground(int unread_count, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      int timSucc = IMNativeSDK.TIMMsgDoBackground(unread_count, ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, unread_count.ToString());
      return (TIMResult)timSucc;
    }
    public static TIMResult MsgDoBackground(int unread_count, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      int timSucc = IMNativeSDK.TIMMsgDoBackground(unread_count, StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, unread_count.ToString());
      return (TIMResult)timSucc;
    }
    /// <summary>
    /// APP 检测到应用进前台时可以调用此接口（iOS 和 Android 平台专用）。
    /// Call this when APP returns to the foreground (iOS & Android only)
    /// </summary>
    /// <param name="callback">ValueCallback</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult MsgDoForeground(NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      int timSucc = IMNativeSDK.TIMMsgDoForeground(ValueCallbackInstance, Utils.string2intptr(user_data));

      return (TIMResult)timSucc;
    }
    public static TIMResult MsgDoForeground(ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      int timSucc = IMNativeSDK.TIMMsgDoForeground(StringValueCallbackInstance, Utils.string2intptr(user_data));

      return (TIMResult)timSucc;
    }
    /// <summary>
    /// 下载多媒体消息
    /// Download message elements
    /// </summary>
    /// <param name="download_param">下载参数 DownloadElemParam</param>
    /// <param name="path">本地路径 (Local path)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult MsgDownloadElemToPath(DownloadElemParam download_param, string path, ValueCallback<MsgDownloadElemResult> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr() + "_downloadElem";
      var param = Utils.ToJson(download_param);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<MsgDownloadElemResult>);
      int timSucc = IMNativeSDK.TIMMsgDownloadElemToPath(Utils.string2intptr(param), Utils.string2intptr(path), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param, path);
      return (TIMResult)timSucc;
    }
    public static TIMResult MsgDownloadElemToPath(DownloadElemParam download_param, string path, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr() + "_downloadElem";
      var param = Utils.ToJson(download_param);
      ValuecallbackStore.Add(user_data, callback);
      int timSucc = IMNativeSDK.TIMMsgDownloadElemToPath(Utils.string2intptr(param), Utils.string2intptr(path), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param, path);
      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 下载合并消息
    /// Download merger message
    /// </summary>
    /// <param name="message">消息体 Message</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult MsgDownloadMergerMessage(Message message, ValueCallback<List<Message>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var msg = Utils.ToJson(message);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<Message>>);

      int timSucc = IMNativeSDK.TIMMsgDownloadMergerMessage(Utils.string2intptr(msg), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, msg);
      return (TIMResult)timSucc;
    }
    public static TIMResult MsgDownloadMergerMessage(Message message, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var msg = Utils.ToJson(message);

      ValuecallbackStore.Add(user_data, callback);

      int timSucc = IMNativeSDK.TIMMsgDownloadMergerMessage(Utils.string2intptr(msg), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, msg);
      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 批量发送消息
    /// Send batch messages
    /// </summary>
    /// <param name="json_batch_send_param">批量消息体 MsgBatchSendParam (Batch message param)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult MsgBatchSend(MsgBatchSendParam json_batch_send_param, ValueCallback<List<MsgBatchSendResult>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_batch_send_param);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<MsgBatchSendResult>>);

      int timSucc = IMNativeSDK.TIMMsgBatchSend(Utils.string2intptr(param), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)timSucc;
    }
    public static TIMResult MsgBatchSend(MsgBatchSendParam json_batch_send_param, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_batch_send_param);

      ValuecallbackStore.Add(user_data, callback);

      int timSucc = IMNativeSDK.TIMMsgBatchSend(Utils.string2intptr(param), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 搜索本地消息
    /// Search local message
    /// </summary>
    /// <param name="message_search_param">搜索消息参数 MessageSearchParam (Search message param)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult MsgSearchLocalMessages(MessageSearchParam message_search_param, ValueCallback<MessageSearchResult> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(message_search_param);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<MessageSearchResult>);

      int timSucc = IMNativeSDK.TIMMsgSearchLocalMessages(Utils.string2intptr(param), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)timSucc;
    }
    public static TIMResult MsgSearchLocalMessages(MessageSearchParam message_search_param, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(message_search_param);

      ValuecallbackStore.Add(user_data, callback);

      int timSucc = IMNativeSDK.TIMMsgSearchLocalMessages(Utils.string2intptr(param), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)timSucc;
    }
    /// <summary>
    /// 搜索云端消息
    /// Search cloud message
    /// </summary>
    /// <param name="message_search_param">搜索消息参数 MessageSearchParam (Search message param)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult MsgSearchCloudMessages(MessageSearchParam message_search_param, ValueCallback<MessageSearchResult> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(message_search_param);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<MessageSearchResult>);

      int timSucc = IMNativeSDK.TIMMsgSearchCloudMessages(Utils.string2intptr(param), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)timSucc;
    }
    public static TIMResult MsgSearchCloudMessages(MessageSearchParam message_search_param, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(message_search_param);

      ValuecallbackStore.Add(user_data, callback);

      int timSucc = IMNativeSDK.TIMMsgSearchCloudMessages(Utils.string2intptr(param), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 设置消息本地数据
    /// Set local custom data
    /// </summary>
    /// <param name="message">消息体 Message</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult MsgSetLocalCustomData(Message message, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var msg = Utils.ToJson(message);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      int timSucc = IMNativeSDK.TIMMsgSetLocalCustomData(Utils.string2intptr(msg), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, msg);
      return (TIMResult)timSucc;
    }
    public static TIMResult MsgSetLocalCustomData(Message message, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var msg = Utils.ToJson(message);

      ValuecallbackStore.Add(user_data, callback);

      int timSucc = IMNativeSDK.TIMMsgSetLocalCustomData(Utils.string2intptr(msg), StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, msg);
      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 创建群
    /// Create group
    /// </summary>
    /// <param name="group">创建群信息 CreateGroupParam</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult GroupCreate(CreateGroupParam group, ValueCallback<CreateGroupResult> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(group);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<CreateGroupResult>);

      int timSucc = IMNativeSDK.TIMGroupCreate(Utils.string2intptr(param), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)timSucc;
    }
    public static TIMResult GroupCreate(CreateGroupParam group, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(group);

      ValuecallbackStore.Add(user_data, callback);

      int timSucc = IMNativeSDK.TIMGroupCreate(Utils.string2intptr(param), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 删除群
    /// Delete group
    /// </summary>
    /// <param name="group_id">群ID (Group ID)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult GroupDelete(string group_id, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      int timSucc = IMNativeSDK.TIMGroupDelete(Utils.string2intptr(group_id), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, group_id);
      return (TIMResult)timSucc;
    }
    public static TIMResult GroupDelete(string group_id, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      int timSucc = IMNativeSDK.TIMGroupDelete(Utils.string2intptr(group_id), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, group_id);
      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 加入群
    /// Join group
    /// </summary>
    /// <param name="group_id">群ID (Group ID)</param>
    /// <param name="hello_message">进群打招呼信息 (Greeting message)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult GroupJoin(string group_id, string hello_message, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      int res = IMNativeSDK.TIMGroupJoin(Utils.string2intptr(group_id), Utils.string2intptr(hello_message), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, group_id, hello_message);
      return (TIMResult)res;
    }
    public static TIMResult GroupJoin(string group_id, string hello_message, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMGroupJoin(Utils.string2intptr(group_id), Utils.string2intptr(hello_message), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, group_id, hello_message);
      return (TIMResult)res;
    }

    /// <summary>
    /// 退出群
    /// Quit group
    /// </summary>
    /// <param name="group_id">群ID (Group ID)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult GroupQuit(string group_id, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      int res = IMNativeSDK.TIMGroupQuit(Utils.string2intptr(group_id), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, group_id);
      return (TIMResult)res;
    }
    public static TIMResult GroupQuit(string group_id, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMGroupQuit(Utils.string2intptr(group_id), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, group_id);
      return (TIMResult)res;
    }

    /// <summary>
    /// 邀请用户进群
    /// Invite to group
    /// </summary>
    /// <param name="param">邀请人员信息 GroupInviteMemberParam</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult GroupInviteMember(GroupInviteMemberParam param, ValueCallback<List<GroupInviteMemberResult>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var paramStr = Utils.ToJson(param);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<GroupInviteMemberResult>>);

      int res = IMNativeSDK.TIMGroupInviteMember(Utils.string2intptr(paramStr), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, paramStr);
      return (TIMResult)res;
    }
    public static TIMResult GroupInviteMember(GroupInviteMemberParam param, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var paramStr = Utils.ToJson(param);

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMGroupInviteMember(Utils.string2intptr(paramStr), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, paramStr);
      return (TIMResult)res;
    }

    /// <summary>
    /// 踢出群成员
    /// Delete member from group
    /// </summary>
    /// <param name="param">删除人员信息 GroupDeleteMemberParam</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult GroupDeleteMember(GroupDeleteMemberParam param, ValueCallback<List<GroupDeleteMemberResult>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var paramStr = Utils.ToJson(param);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<GroupDeleteMemberResult>>);

      int res = IMNativeSDK.TIMGroupDeleteMember(Utils.string2intptr(paramStr), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, paramStr);
      return (TIMResult)res;
    }
    public static TIMResult GroupDeleteMember(GroupDeleteMemberParam param, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var paramStr = Utils.ToJson(param);

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMGroupDeleteMember(Utils.string2intptr(paramStr), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, paramStr);
      return (TIMResult)res;
    }

    /// <summary>
    /// 获取已加入的群组列表
    /// Get joined group list
    /// </summary>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult GroupGetJoinedGroupList(ValueCallback<List<GroupBaseInfo>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<GroupBaseInfo>>);

      int res = IMNativeSDK.TIMGroupGetJoinedGroupList(ValueCallbackInstance, Utils.string2intptr(user_data));

      return (TIMResult)res;
    }
    public static TIMResult GroupGetJoinedGroupList(ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMGroupGetJoinedGroupList(StringValueCallbackInstance, Utils.string2intptr(user_data));

      return (TIMResult)res;
    }

    /// <summary>
    /// 获取群信息
    /// Get group info list
    /// </summary>
    /// <param name="group_id_list">群ID列表 (Group ID list)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult GroupGetGroupInfoList(List<string> group_id_list, ValueCallback<List<GetGroupInfoResult>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var list = Utils.ToJson(group_id_list);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<GetGroupInfoResult>>);

      int res = IMNativeSDK.TIMGroupGetGroupInfoList(Utils.string2intptr(list), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, list);
      return (TIMResult)res;
    }
    public static TIMResult GroupGetGroupInfoList(List<string> group_id_list, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var list = Utils.ToJson(group_id_list);

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMGroupGetGroupInfoList(Utils.string2intptr(list), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, list);
      return (TIMResult)res;
    }

    /// <summary>
    /// 修改群信息
    /// Modify group info
    /// </summary>
    /// <param name="json_group_modifyinfo_param">修改信息参数 GroupModifyInfoParam</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult GroupModifyGroupInfo(GroupModifyInfoParam json_group_modifyinfo_param, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_group_modifyinfo_param);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      int res = IMNativeSDK.TIMGroupModifyGroupInfo(Utils.string2intptr(param), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)res;
    }
    public static TIMResult GroupModifyGroupInfo(GroupModifyInfoParam json_group_modifyinfo_param, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_group_modifyinfo_param);

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMGroupModifyGroupInfo(Utils.string2intptr(param), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)res;
    }

    /// <summary>
    /// 获取群成员信息
    /// Get member info list
    /// </summary>
    /// <param name="json_group_getmeminfos_param">修改信息参数 GroupGetMemberInfoListParam</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult GroupGetMemberInfoList(GroupGetMemberInfoListParam json_group_getmeminfos_param, ValueCallback<GroupGetMemberInfoListResult> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_group_getmeminfos_param);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<GroupGetMemberInfoListResult>);

      int res = IMNativeSDK.TIMGroupGetMemberInfoList(Utils.string2intptr(param), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)res;
    }
    public static TIMResult GroupGetMemberInfoList(GroupGetMemberInfoListParam json_group_getmeminfos_param, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_group_getmeminfos_param);

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMGroupGetMemberInfoList(Utils.string2intptr(param), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)res;
    }

    /// <summary>
    /// 修改群成员信息
    /// Modify group member info
    /// </summary>
    /// <param name="json_group_modifymeminfo_param">修改信息参数 GroupModifyMemberInfoParam</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult GroupModifyMemberInfo(GroupModifyMemberInfoParam json_group_modifymeminfo_param, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_group_modifymeminfo_param);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      int res = IMNativeSDK.TIMGroupModifyMemberInfo(Utils.string2intptr(param), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)res;
    }
    public static TIMResult GroupModifyMemberInfo(GroupModifyMemberInfoParam json_group_modifymeminfo_param, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_group_modifymeminfo_param);

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMGroupModifyMemberInfo(Utils.string2intptr(param), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)res;
    }

    /// <summary>
    /// 标记群成员（从 6.6 版本开始支持，需要您购买旗舰版套餐）
    /// Mark group member (^6.6, Premium ver. package only)
    /// </summary>
    /// <param name="group_id">群 ID (Group ID)</param>
    /// <param name="member_array">群成员 ID 列表 (Group member ID array)</param>
    /// <param name="mark_type">标记类型 (Mark type)</param>
    /// <param name="enable_mark">true：设置标记 false：取消标记 (Is enable mark)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult GroupMarkGroupMemberList(string group_id, List<string> member_array, int mark_type, bool enable_mark, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      return HandleMarkGroupMemberList(group_id, member_array, mark_type, enable_mark, fn_name, callback, null);
    }
    public static TIMResult GroupMarkGroupMemberList(string group_id, List<string> member_array, int mark_type, bool enable_mark, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      return HandleMarkGroupMemberList(group_id, member_array, mark_type, enable_mark, fn_name, null, callback);
    }

    private static TIMResult HandleMarkGroupMemberList(string group_id, List<string> member_array, int mark_type, bool enable_mark, string fn_name, NullValueCallback callback = null, ValueCallback<string> strCallback = null)
    {
      string user_data = Utils.GetUserDataFromFnName(fn_name);
      int timSucc;
      var memberArrayStr = Utils.ToJson(member_array);
      var memberArrayPtr = Utils.string2intptr(memberArrayStr);
      var groupIdPtr = Utils.string2intptr(group_id);

      if (callback != null)
      {
        ValuecallbackDeleStore.Add(user_data, threadOperation<object>);
        ValuecallbackStore.Add(user_data, callback);
        timSucc = IMNativeSDK.TIMGroupMarkGroupMemberList(groupIdPtr, memberArrayPtr, mark_type, enable_mark, ValueCallbackInstance, Utils.string2intptr(user_data));
      }
      else
      {
        ValuecallbackStore.Add(user_data, strCallback);
        timSucc = IMNativeSDK.TIMGroupMarkGroupMemberList(groupIdPtr, memberArrayPtr, mark_type, enable_mark, StringValueCallbackInstance, Utils.string2intptr(user_data));
      }

      Log(user_data, group_id, memberArrayStr, mark_type.ToString(), enable_mark.ToString());
      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 获取群未决信息列表
    /// Get group pendency list
    /// </summary>
    /// <param name="json_group_getpendence_list_param">修改信息参数 GroupPendencyOption</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult GroupGetPendencyList(GroupPendencyOption json_group_getpendence_list_param, ValueCallback<GroupPendencyResult> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_group_getpendence_list_param);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<GroupPendencyResult>);

      int res = IMNativeSDK.TIMGroupGetPendencyList(Utils.string2intptr(param), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)res;
    }
    public static TIMResult GroupGetPendencyList(GroupPendencyOption json_group_getpendence_list_param, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_group_getpendence_list_param);

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMGroupGetPendencyList(Utils.string2intptr(param), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)res;
    }

    /// <summary>
    /// 上报群未决信息已读
    /// Report group pendency as read
    /// </summary>
    /// <param name="time_stamp">时间戳 (Timestamp)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult GroupReportPendencyReaded(long time_stamp, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      int res = IMNativeSDK.TIMGroupReportPendencyReaded(time_stamp, ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, time_stamp.ToString());
      return (TIMResult)res;
    }
    public static TIMResult GroupReportPendencyReaded(long time_stamp, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMGroupReportPendencyReaded(time_stamp, StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, time_stamp.ToString());
      return (TIMResult)res;
    }

    /// <summary>
    /// 处理群未决信息
    /// Handle group pendency
    /// </summary>
    /// <param name="json_group_handle_pendency_param">处理群未决信息参数 GroupHandlePendencyParam</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult GroupHandlePendency(GroupHandlePendencyParam json_group_handle_pendency_param, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_group_handle_pendency_param);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      int res = IMNativeSDK.TIMGroupHandlePendency(Utils.string2intptr(param), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)res;
    }
    public static TIMResult GroupHandlePendency(GroupHandlePendencyParam json_group_handle_pendency_param, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_group_handle_pendency_param);

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMGroupHandlePendency(Utils.string2intptr(param), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)res;
    }

    /// <summary>
    /// 获取群在线用户数
    /// Get group online member count
    /// </summary>
    /// <param name="group_id">群ID (Group ID)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult GroupGetOnlineMemberCount(string group_id, ValueCallback<GroupGetOnlineMemberCountResult> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<GroupGetOnlineMemberCountResult>);

      int res = IMNativeSDK.TIMGroupGetOnlineMemberCount(Utils.string2intptr(group_id), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, group_id);
      return (TIMResult)res;
    }
    public static TIMResult GroupGetOnlineMemberCount(string group_id, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMGroupGetOnlineMemberCount(Utils.string2intptr(group_id), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, group_id);
      return (TIMResult)res;
    }

    /// <summary>
    /// 设置群计数器（7.0 及其以上版本支持）
    /// Set group counters (^7.0)
    /// </summary>
    /// <param name="group_id">群 ID (Group ID)</param>
    /// <param name="group_counter_array">群计数器信息列表 (Group counter array)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult GroupSetGroupCounters(string group_id, List<GroupCounter> group_counter_array, ValueCallback<List<GroupCounter>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      return HandleSetGroupCounters(group_id, group_counter_array, fn_name, callback, null);
    }
    public static TIMResult GroupSetGroupCounters(string group_id, List<GroupCounter> group_counter_array, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      return HandleSetGroupCounters(group_id, group_counter_array, fn_name, null, callback);
    }

    private static TIMResult HandleSetGroupCounters(string group_id, List<GroupCounter> group_counter_array, string fn_name, ValueCallback<List<GroupCounter>> callback = null, ValueCallback<string> strCallback = null)
    {
      string user_data = Utils.GetUserDataFromFnName(fn_name);
      int timSucc;
      var groupIdPtr = Utils.string2intptr(group_id);
      var counterArrStr = Utils.ToJson(group_counter_array);
      var counterArrPtr = Utils.string2intptr(counterArrStr);

      if (callback != null)
      {
        ValuecallbackDeleStore.Add(user_data, threadOperation<List<GroupCounter>>);
        ValuecallbackStore.Add(user_data, callback);
        timSucc = IMNativeSDK.TIMGroupSetGroupCounters(groupIdPtr, counterArrPtr, ValueCallbackInstance, Utils.string2intptr(user_data));
      }
      else
      {
        ValuecallbackStore.Add(user_data, strCallback);
        timSucc = IMNativeSDK.TIMGroupSetGroupCounters(groupIdPtr, counterArrPtr, StringValueCallbackInstance, Utils.string2intptr(user_data));
      }

      Log(user_data, group_id, counterArrStr);
      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 获取群计数器（7.0 及其以上版本支持）
    /// Get group counters (^7.0)
    /// </summary>
    /// <param name="group_id">群 ID (Group ID)</param>
    /// <param name="group_counter_key_array">群计数器信息 key 列表 (Group counter key array)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult GroupGetGroupCounters(string group_id, List<string> group_counter_key_array, ValueCallback<List<GroupCounter>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      return HandleGetGroupCounters(group_id, group_counter_key_array, fn_name, callback, null);
    }
    public static TIMResult GroupGetGroupCounters(string group_id, List<string> group_counter_key_array, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      return HandleGetGroupCounters(group_id, group_counter_key_array, fn_name, null, callback);
    }

    private static TIMResult HandleGetGroupCounters(string group_id, List<string> group_counter_key_array, string fn_name, ValueCallback<List<GroupCounter>> callback = null, ValueCallback<string> strCallback = null)
    {
      string user_data = Utils.GetUserDataFromFnName(fn_name);
      int timSucc;
      var groupIdPtr = Utils.string2intptr(group_id);
      var counterArrStr = Utils.ToJson(group_counter_key_array);
      var counterArrPtr = Utils.string2intptr(counterArrStr);

      if (callback != null)
      {
        ValuecallbackDeleStore.Add(user_data, threadOperation<List<GroupCounter>>);
        ValuecallbackStore.Add(user_data, callback);
        timSucc = IMNativeSDK.TIMGroupGetGroupCounters(groupIdPtr, counterArrPtr, ValueCallbackInstance, Utils.string2intptr(user_data));
      }
      else
      {
        ValuecallbackStore.Add(user_data, strCallback);
        timSucc = IMNativeSDK.TIMGroupGetGroupCounters(groupIdPtr, counterArrPtr, StringValueCallbackInstance, Utils.string2intptr(user_data));
      }

      Log(user_data, group_id, counterArrStr);
      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 递增群计数器（7.0 及其以上版本支持）
    /// Increase group counter (^7.0)
    /// </summary>
    /// <param name="group_id">群 ID (Group ID)</param>
    /// <param name="group_counter_key">群计数器的 key (Group counter key)</param>
    /// <param name="group_counter_value">群计数器的递增变化量 value (Group counter value)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult GroupIncreaseGroupCounter(string group_id, string group_counter_key, ulong group_counter_value, ValueCallback<List<GroupCounter>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      return HandleIncreaseGroupCounter(group_id, group_counter_key, group_counter_value, fn_name, callback, null);
    }
    public static TIMResult GroupIncreaseGroupCounter(string group_id, string group_counter_key, ulong group_counter_value, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      return HandleIncreaseGroupCounter(group_id, group_counter_key, group_counter_value, fn_name, null, callback);
    }

    private static TIMResult HandleIncreaseGroupCounter(string group_id, string group_counter_key, ulong group_counter_value, string fn_name, ValueCallback<List<GroupCounter>> callback = null, ValueCallback<string> strCallback = null)
    {
      string user_data = Utils.GetUserDataFromFnName(fn_name);
      int timSucc;
      var groupIdPtr = Utils.string2intptr(group_id);
      var counterKeyPtr = Utils.string2intptr(group_counter_key);

      if (callback != null)
      {
        ValuecallbackDeleStore.Add(user_data, threadOperation<List<GroupCounter>>);
        ValuecallbackStore.Add(user_data, callback);
        timSucc = IMNativeSDK.TIMGroupIncreaseGroupCounter(groupIdPtr, counterKeyPtr, group_counter_value, ValueCallbackInstance, Utils.string2intptr(user_data));
      }
      else
      {
        ValuecallbackStore.Add(user_data, strCallback);
        timSucc = IMNativeSDK.TIMGroupIncreaseGroupCounter(groupIdPtr, counterKeyPtr, group_counter_value, StringValueCallbackInstance, Utils.string2intptr(user_data));
      }

      Log(user_data, group_id, group_counter_key, group_counter_value);
      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 递减群计数器（7.0 及其以上版本支持）
    /// Decrease group counter (^7.0)
    /// </summary>
    /// <param name="group_id">群 ID (Group ID)</param>
    /// <param name="group_counter_key">群计数器的 key (Group counter key)</param>
    /// <param name="group_counter_value">群计数器的递减变化量 value (Group counter value)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult GroupDecreaseGroupCounter(string group_id, string group_counter_key, ulong group_counter_value, ValueCallback<List<GroupCounter>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      return HandleDecreaseGroupCounter(group_id, group_counter_key, group_counter_value, fn_name, callback, null);
    }
    public static TIMResult GroupDecreaseGroupCounter(string group_id, string group_counter_key, ulong group_counter_value, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      return HandleDecreaseGroupCounter(group_id, group_counter_key, group_counter_value, fn_name, null, callback);
    }

    private static TIMResult HandleDecreaseGroupCounter(string group_id, string group_counter_key, ulong group_counter_value, string fn_name, ValueCallback<List<GroupCounter>> callback = null, ValueCallback<string> strCallback = null)
    {
      string user_data = Utils.GetUserDataFromFnName(fn_name);
      int timSucc;
      var groupIdPtr = Utils.string2intptr(group_id);
      var counterKeyPtr = Utils.string2intptr(group_counter_key);

      if (callback != null)
      {
        ValuecallbackDeleStore.Add(user_data, threadOperation<List<GroupCounter>>);
        ValuecallbackStore.Add(user_data, callback);
        timSucc = IMNativeSDK.TIMGroupDecreaseGroupCounter(groupIdPtr, counterKeyPtr, group_counter_value, ValueCallbackInstance, Utils.string2intptr(user_data));
      }
      else
      {
        ValuecallbackStore.Add(user_data, strCallback);
        timSucc = IMNativeSDK.TIMGroupDecreaseGroupCounter(groupIdPtr, counterKeyPtr, group_counter_value, StringValueCallbackInstance, Utils.string2intptr(user_data));
      }

      Log(user_data, group_id, group_counter_key, group_counter_value);
      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 搜索群资料（5.4.666 及以上版本支持，需要您购买旗舰版套餐）
    /// Search group info (^5.4.666, Premium ver. package only)
    /// </summary>
    /// <param name="json_group_search_groups_param">群列表的参数 GroupSearchParam</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult GroupSearchGroups(GroupSearchParam json_group_search_groups_param, ValueCallback<List<GroupDetailInfo>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_group_search_groups_param);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<GroupDetailInfo>>);

      int res = IMNativeSDK.TIMGroupSearchGroups(Utils.string2intptr(param), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)res;
    }
    public static TIMResult GroupSearchGroups(GroupSearchParam json_group_search_groups_param, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_group_search_groups_param);

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMGroupSearchGroups(Utils.string2intptr(param), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)res;
    }

    /// <summary>
    /// 搜索云端群资料（8.4 及以上版本支持）
    /// Search cloud groups (^5.3, ^8.4)
    /// </summary>
    /// <param name="json_group_search_groups_param">群列表的参数 array ，Json Key 请参考 @ref GroupSearchParam</param>
    /// <param name="callback">搜索群列表回调。回调函数定义请参考 @ref TIMCommCallback</param>
    /// <param name="user_data">用户自定义数据，ImSDK只负责传回给回调函数cb，不做任何处理</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult GroupSearchCloudGroups(GroupSearchParam json_group_search_groups_param, ValueCallback<GroupSearchResult> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_group_search_groups_param);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<GroupSearchResult>);

      int res = IMNativeSDK.TIMGroupSearchCloudGroups(Utils.string2intptr(param), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)res;
    }
    public static TIMResult GroupSearchCloudGroups(GroupSearchParam json_group_search_groups_param, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_group_search_groups_param);

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMGroupSearchCloudGroups(Utils.string2intptr(param), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)res;
    }

    /// <summary>
    /// 搜索群成员
    /// Search group members
    /// </summary>
    /// <param name="json_group_search_group_members_param">搜索群成员参数 GroupMemberSearchParam</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult GroupSearchGroupMembers(GroupMemberSearchParam json_group_search_group_members_param, ValueCallback<List<GroupSearchGroupMembersResult>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_group_search_group_members_param);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<GroupSearchGroupMembersResult>>);

      int res = IMNativeSDK.TIMGroupSearchGroupMembers(Utils.string2intptr(param), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)res;
    }
    public static TIMResult GroupSearchGroupMembers(GroupMemberSearchParam json_group_search_group_members_param, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_group_search_group_members_param);

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMGroupSearchGroupMembers(Utils.string2intptr(param), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)res;
    }

    /// <summary>
    /// 搜索云端群成员资料（8.4 及以上版本支持）
    /// Search cloud group members (^8.4)
    /// </summary>
    /// <param name="json_group_search_cloud_group_members_param">群列表的参数 array ，Json Key 请参考 @ref GroupMemberSearchParam</param>
    /// <param name="callback">搜索群列表回调。回调函数定义请参考 @ref TIMCommCallback</param>
    /// <param name="user_data">用户自定义数据，ImSDK只负责传回给回调函数cb，不做任何处理</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult GroupSearchCloudGroupMembers(GroupMemberSearchParam json_group_search_cloud_group_members_param, ValueCallback<GroupMemberSearchResult> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_group_search_cloud_group_members_param);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<GroupMemberSearchResult>);

      int res = IMNativeSDK.TIMGroupSearchCloudGroupMembers(Utils.string2intptr(param), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)res;
    }
    public static TIMResult GroupSearchCloudGroupMembers(GroupMemberSearchParam json_group_search_cloud_group_members_param, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_group_search_cloud_group_members_param);

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMGroupSearchCloudGroupMembers(Utils.string2intptr(param), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)res;
    }

    /// <summary>
    /// 初始化群自定义属性
    /// Init group attributes
    /// </summary>
    /// <param name="group_id">群ID (Group ID)</param>
    /// <param name="json_group_atrributes">群属性参数 Array<GroupAttributes></param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult GroupInitGroupAttributes(string group_id, List<GroupAttributes> json_group_atrributes, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var list = Utils.ToJson(json_group_atrributes);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      int res = IMNativeSDK.TIMGroupInitGroupAttributes(Utils.string2intptr(group_id), Utils.string2intptr(list), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, group_id, list);
      return (TIMResult)res;
    }
    public static TIMResult GroupInitGroupAttributes(string group_id, List<GroupAttributes> json_group_atrributes, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var list = Utils.ToJson(json_group_atrributes);

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMGroupInitGroupAttributes(Utils.string2intptr(group_id), Utils.string2intptr(list), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, group_id, list);
      return (TIMResult)res;
    }

    /// <summary>
    /// 设置群属性
    /// Set group attributes
    /// <para>已有该群属性则更新其 value 值，没有该群属性则添加该群属性 (Modify value If key is predefined, otherwise create new key-value pair)</para>
    /// </summary>
    /// <param name="group_id">群ID (Group ID)</param>
    /// <param name="json_group_atrributes">群属性参数 List<GroupAttributes></param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult GroupSetGroupAttributes(string group_id, List<GroupAttributes> json_group_atrributes, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var list = Utils.ToJson(json_group_atrributes);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      int res = IMNativeSDK.TIMGroupSetGroupAttributes(Utils.string2intptr(group_id), Utils.string2intptr(list), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, group_id, list);
      return (TIMResult)res;
    }
    public static TIMResult GroupSetGroupAttributes(string group_id, List<GroupAttributes> json_group_atrributes, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var list = Utils.ToJson(json_group_atrributes);

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMGroupSetGroupAttributes(Utils.string2intptr(group_id), Utils.string2intptr(list), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, group_id, list);
      return (TIMResult)res;
    }

    /// <summary>
    /// 删除群自定义属性
    /// Delete group attributes
    /// </summary>
    /// <param name="group_id">群ID (Group ID)</param>
    /// <param name="json_keys">属性key列表 (Key list)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult GroupDeleteGroupAttributes(string group_id, List<string> json_keys, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var list = Utils.ToJson(json_keys);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      int res = IMNativeSDK.TIMGroupDeleteGroupAttributes(Utils.string2intptr(group_id), Utils.string2intptr(list), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, group_id, list);
      return (TIMResult)res;
    }
    public static TIMResult GroupDeleteGroupAttributes(string group_id, List<string> json_keys, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var list = Utils.ToJson(json_keys);

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMGroupDeleteGroupAttributes(Utils.string2intptr(group_id), Utils.string2intptr(list), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, group_id, list);
      return (TIMResult)res;
    }

    /// <summary>
    /// 获取群指定属性
    /// Get group attributes
    /// </summary>
    /// <param name="group_id">群ID (Group ID)</param>
    /// <param name="json_keys">属性key列表 (Key list)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult GroupGetGroupAttributes(string group_id, List<string> json_keys, ValueCallback<List<GroupAttributes>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var list = Utils.ToJson(json_keys);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<GroupAttributes>>);

      int res = IMNativeSDK.TIMGroupGetGroupAttributes(Utils.string2intptr(group_id), Utils.string2intptr(list), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, group_id, list);
      return (TIMResult)res;
    }
    public static TIMResult GroupGetGroupAttributes(string group_id, List<string> json_keys, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var list = Utils.ToJson(json_keys);

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMGroupGetGroupAttributes(Utils.string2intptr(group_id), Utils.string2intptr(list), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, group_id, list);
      return (TIMResult)res;
    }

    /// <summary>
    /// 获取当前用户已经加入的支持话题的社群列表
    /// Get joined coomunity list
    /// </summary>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult GroupGetJoinedCommunityList(ValueCallback<List<GroupInfo>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<GroupInfo>>);

      int res = IMNativeSDK.TIMGroupGetJoinedCommunityList(ValueCallbackInstance, Utils.string2intptr(user_data));

      return (TIMResult)res;
    }
    public static TIMResult GroupGetJoinedCommunityList(ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMGroupGetJoinedCommunityList(StringValueCallbackInstance, Utils.string2intptr(user_data));

      return (TIMResult)res;
    }

    /// <summary>
    /// 创建话题
    /// Create topic in community group
    /// <param>回调为 topic_id 的字符串 (Callback is topic_id as string)</param>
    /// </summary>
    /// <param name="group_id">群 ID (Group ID)</param>
    /// <param name="json_topic_info">话题信息 (Topic info)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult GroupCreateTopicInCommunity(string group_id, GroupTopicInfo json_topic_info, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_topic_info);

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMGroupCreateTopicInCommunity(Utils.string2intptr(group_id), Utils.string2intptr(param), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, group_id, param);
      return (TIMResult)res;
    }

    /// <summary>
    /// 删除话题
    /// Delete topic from community
    /// </summary>
    /// <param name="group_id">群 ID (Group ID)</param>
    /// <param name="json_topic_id_array">话题 ID 列表 (Topic ID list)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult GroupDeleteTopicFromCommunity(string group_id, List<string> json_topic_id_array, ValueCallback<List<GroupTopicOperationResult>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var list = Utils.ToJson(json_topic_id_array);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<GroupTopicOperationResult>>);

      int res = IMNativeSDK.TIMGroupDeleteTopicFromCommunity(Utils.string2intptr(group_id), Utils.string2intptr(list), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, group_id, list);
      return (TIMResult)res;
    }
    public static TIMResult GroupDeleteTopicFromCommunity(string group_id, List<string> json_topic_id_array, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var list = Utils.ToJson(json_topic_id_array);

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMGroupDeleteTopicFromCommunity(Utils.string2intptr(group_id), Utils.string2intptr(list), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, group_id, list);
      return (TIMResult)res;
    }

    /// <summary>
    /// 修改话题信息
    /// Set topic info
    /// </summary>
    /// <param name="json_topic_info">话题信息 (Topic info)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult GroupSetTopicInfo(GroupTopicInfo json_topic_info, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_topic_info);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      int res = IMNativeSDK.TIMGroupSetTopicInfo(Utils.string2intptr(param), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)res;
    }
    public static TIMResult GroupSetTopicInfo(GroupTopicInfo json_topic_info, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_topic_info);

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMGroupSetTopicInfo(Utils.string2intptr(param), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)res;
    }

    /// <summary>
    /// 获取话题列表
    /// Get topic info list
    /// <param>json_topic_id_array 传空时，获取此社群下的所有话题列表 (When json_topic_id_array is null, get all topics in this community)</param>
    /// </summary>
    /// <param name="group_id">群 ID (Group ID)</param>
    /// <param name="json_topic_id_array">话题 ID 列表 (Topic ID list)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult GroupGetTopicInfoList(string group_id, List<string> json_topic_id_array, ValueCallback<List<GroupTopicInfoResult>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var list = Utils.ToJson(json_topic_id_array);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<GroupTopicInfoResult>>);

      int res = IMNativeSDK.TIMGroupGetTopicInfoList(Utils.string2intptr(group_id), Utils.string2intptr(list), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, group_id, list);
      return (TIMResult)res;
    }
    public static TIMResult GroupGetTopicInfoList(string group_id, List<string> json_topic_id_array, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var list = Utils.ToJson(json_topic_id_array);
      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMGroupGetTopicInfoList(Utils.string2intptr(group_id), Utils.string2intptr(list), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, group_id, list);
      return (TIMResult)res;
    }

    /// <summary>
    /// 搜索云端用户资料（8.4 及以上版本支持）
    /// Search users in cloud
    /// </summary>
    /// <param name="user_search_param">用户搜索参数，(User search parameters)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult SearchUsers(UserSearchParam user_search_param, ValueCallback<UserSearchResult> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(user_search_param);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<UserSearchResult>);

      int res = IMNativeSDK.TIMSearchUsers(Utils.string2intptr(param), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)res;
    }
    public static TIMResult SearchUsers(UserSearchParam user_search_param, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(user_search_param);

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMSearchUsers(Utils.string2intptr(param), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)res;
    }

    /// <summary>
    /// 查询用户状态，从 6.3 版本开始支持
    /// Get user status ,available ^6.3
    /// <param>如果您想查询自己的自定义状态，您只需要传入自己的 userID 即可 (Input self userID to check self status)</param>
    /// </summary>
    /// <param name="json_identifier_array">用户 ID 列表 (User ID list)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult GetUserStatus(List<string> json_identifier_array, ValueCallback<List<UserStatus>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var list = Utils.ToJson(json_identifier_array);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<UserStatus>>);

      int res = IMNativeSDK.TIMGetUserStatus(Utils.string2intptr(list), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, list);
      return (TIMResult)res;
    }
    public static TIMResult GetUserStatus(List<string> json_identifier_array, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var list = Utils.ToJson(json_identifier_array);

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMGetUserStatus(Utils.string2intptr(list), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, list);
      return (TIMResult)res;
    }

    /// <summary>
    /// 设置自己的状态，从 6.3 版本开始支持
    /// Set self stauts ,available ^6.3
    /// <param>请注意，该接口只支持设置自己的自定义状态，即 V2TIMUserStatus.customStatus (Caveat: this can only set SELF status)</param>
    /// </summary>
    /// <param name="json_current_user_status">待设置的自定义状态 (Current user status)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult SetSelfStatus(UserStatus json_current_user_status, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_current_user_status);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      int res = IMNativeSDK.TIMSetSelfStatus(Utils.string2intptr(param), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)res;
    }
    public static TIMResult SetSelfStatus(UserStatus json_current_user_status, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_current_user_status);

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMSetSelfStatus(Utils.string2intptr(param), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)res;
    }

    /// <summary>
    /// 订阅用户状态，从 6.3 版本开始支持
    /// Subscribe user status, available ^6.3
    /// <param>当成功订阅用户状态后，当对方的状态（包含在线状态、自定义状态）发生变更后，您可以监听 TIMSetUserStatusChangedCallback 回调来感知 (After subscription, listen to TIMSetUserStatusChangedCallback to get changing user status (online status, custom status))</param>
    /// <param>如果您需要订阅好友列表的状态，您只需要在控制台上打开开关即可，无需调用该接口 (Subscribe friend list status, just turn on it on the IM console, no need for calling this)</param>
    /// <param>该接口不支持订阅自己，您可以通过监听 TIMSetUserStatusChangedCallback 回调来感知自身的自定义状态的变更 (Use TIMSetUserStatusChangedCallback to subscribe self status changing)</param>
    /// <param>订阅列表有个数限制，超过限制后，会自动淘汰最先订阅的用户 (Subscription has limits, over the limit, the oldest subscription will be deactivated)</param>
    /// </summary>
    /// <param name="json_identifier_array">待订阅的用户 ID (Subscribed user ID list)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult SubscribeUserStatus(List<string> json_identifier_array, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var list = Utils.ToJson(json_identifier_array);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      int res = IMNativeSDK.TIMSubscribeUserStatus(Utils.string2intptr(list), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, list);
      return (TIMResult)res;
    }
    public static TIMResult SubscribeUserStatus(List<string> json_identifier_array, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var list = Utils.ToJson(json_identifier_array);

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMSubscribeUserStatus(Utils.string2intptr(list), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, list);
      return (TIMResult)res;
    }

    /// <summary>
    /// 取消订阅用户状态，从 6.3 版本开始支持
    /// Unsubscribe user status
    /// <param>当 userIDList 为空时，取消当前所有的订阅 (When userIDList is empty, unsubscribe all)</param>
    /// </summary>
    /// <param name="json_identifier_array">待取消订阅的用户 ID (Unsubscribed user ID list)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult UnsubscribeUserStatus(List<string> json_identifier_array, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var list = Utils.ToJson(json_identifier_array);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      int res = IMNativeSDK.TIMUnsubscribeUserStatus(Utils.string2intptr(list), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, list);
      return (TIMResult)res;
    }
    public static TIMResult UnsubscribeUserStatus(List<string> json_identifier_array, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var list = Utils.ToJson(json_identifier_array);

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMUnsubscribeUserStatus(Utils.string2intptr(list), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, list);
      return (TIMResult)res;
    }

    /// <summary>
    /// 获取用户信息列表
    /// Get user profile list
    /// </summary>
    /// <param name="json_get_user_profile_list_param">用户信息列表参数 FriendShipGetProfileListParam</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult ProfileGetUserProfileList(FriendShipGetProfileListParam json_get_user_profile_list_param, ValueCallback<List<UserProfile>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_get_user_profile_list_param);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<UserProfile>>);

      int res = IMNativeSDK.TIMProfileGetUserProfileList(Utils.string2intptr(param), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)res;
    }
    public static TIMResult ProfileGetUserProfileList(FriendShipGetProfileListParam json_get_user_profile_list_param, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_get_user_profile_list_param);

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMProfileGetUserProfileList(Utils.string2intptr(param), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)res;
    }

    /// <summary>
    /// 修改自己的信息
    /// Modify self user profile
    /// </summary>
    /// <param name="json_modify_self_user_profile_param">用户信息列表参数 UserProfileItem</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult ProfileModifySelfUserProfile(UserProfileItem json_modify_self_user_profile_param, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_modify_self_user_profile_param);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      int res = IMNativeSDK.TIMProfileModifySelfUserProfile(Utils.string2intptr(param), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)res;
    }
    public static TIMResult ProfileModifySelfUserProfile(UserProfileItem json_modify_self_user_profile_param, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_modify_self_user_profile_param);

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMProfileModifySelfUserProfile(Utils.string2intptr(param), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)res;
    }

    ///<summary>
    /// 订阅用户资料
    ///</summary> 
    /// /// <param name="json_user_id_list">待订阅的用户 ID 列表</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult SubscribeUserInfo(List<string> json_user_id_list, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_user_id_list);
      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);
      int res = IMNativeSDK.TIMSubscribeUserInfo(Utils.string2intptr(param), ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, param);
      return (TIMResult)res;
    }
    public static TIMResult SubscribeUserInfo(List<string> json_user_id_list, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_user_id_list);
      ValuecallbackStore.Add(user_data, callback);
      int res = IMNativeSDK.TIMSubscribeUserInfo(Utils.string2intptr(param), StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, param);
      return (TIMResult)res;
    }

    ///<summary>
    /// 取消订阅用户资料
    ///</summary> 
    /// /// <param name="json_user_id_list">待取消订阅的用户 ID 列表</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult UnsubscribeUserInfo(List<string> json_user_id_list, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_user_id_list);
      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);
      int res = IMNativeSDK.TIMUnsubscribeUserInfo(Utils.string2intptr(param), ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, param);
      return (TIMResult)res;
    }
    public static TIMResult UnsubscribeUserInfo(List<string> json_user_id_list, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_user_id_list);
      ValuecallbackStore.Add(user_data, callback);
      int res = IMNativeSDK.TIMUnsubscribeUserInfo(Utils.string2intptr(param), StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, param);
      return (TIMResult)res;
    }

    /// <summary>
    /// 获取好友列表信息
    /// Get friend's profile list
    /// </summary>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult FriendshipGetFriendProfileList(ValueCallback<List<FriendProfile>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<FriendProfile>>);

      int res = IMNativeSDK.TIMFriendshipGetFriendProfileList(ValueCallbackInstance, Utils.string2intptr(user_data));

      return (TIMResult)res;
    }
    public static TIMResult FriendshipGetFriendProfileList(ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMFriendshipGetFriendProfileList(StringValueCallbackInstance, Utils.string2intptr(user_data));

      return (TIMResult)res;
    }

    /// <summary>
    /// 添加好友
    /// Add friend
    /// </summary>
    /// <param name="param">添加好友参数 FriendshipAddFriendParam</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult FriendshipAddFriend(FriendshipAddFriendParam param, ValueCallback<FriendResult> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var paramStr = Utils.ToJson(param);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<FriendResult>);

      int res = IMNativeSDK.TIMFriendshipAddFriend(Utils.string2intptr(paramStr), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, paramStr);
      return (TIMResult)res;
    }
    public static TIMResult FriendshipAddFriend(FriendshipAddFriendParam param, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var paramStr = Utils.ToJson(param);

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMFriendshipAddFriend(Utils.string2intptr(paramStr), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, paramStr);
      return (TIMResult)res;
    }

    /// <summary>
    /// 处理好友申请
    /// Handle friend add request
    /// </summary>
    /// <param name="json_handle_friend_add_param">处理好友申请参数 FriendResponse</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult FriendshipHandleFriendAddRequest(FriendResponse json_handle_friend_add_param, ValueCallback<FriendResult> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_handle_friend_add_param);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<FriendResult>);

      int res = IMNativeSDK.TIMFriendshipHandleFriendAddRequest(Utils.string2intptr(param), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)res;
    }
    public static TIMResult FriendshipHandleFriendAddRequest(FriendResponse json_handle_friend_add_param, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_handle_friend_add_param);

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMFriendshipHandleFriendAddRequest(Utils.string2intptr(param), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)res;
    }

    /// <summary>
    /// 修改好友信息
    /// Modify friend's profile
    /// </summary>
    /// <param name="json_modify_friend_info_param">修改好友信息参数 FriendshipModifyFriendProfileParam</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult FriendshipModifyFriendProfile(FriendshipModifyFriendProfileParam json_modify_friend_info_param, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_modify_friend_info_param);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);


      int res = IMNativeSDK.TIMFriendshipModifyFriendProfile(Utils.string2intptr(param), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)res;
    }
    public static TIMResult FriendshipModifyFriendProfile(FriendshipModifyFriendProfileParam json_modify_friend_info_param, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_modify_friend_info_param);

      ValuecallbackStore.Add(user_data, callback);


      int res = IMNativeSDK.TIMFriendshipModifyFriendProfile(Utils.string2intptr(param), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)res;
    }

    /// <summary>
    /// 删除好友
    /// Delete friend
    /// </summary>
    /// <param name="json_delete_friend_param">删除好友参数 FriendshipDeleteFriendParam</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult FriendshipDeleteFriend(FriendshipDeleteFriendParam json_delete_friend_param, ValueCallback<List<FriendResult>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_delete_friend_param);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<FriendResult>>);

      int res = IMNativeSDK.TIMFriendshipDeleteFriend(Utils.string2intptr(param), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)res;
    }
    public static TIMResult FriendshipDeleteFriend(FriendshipDeleteFriendParam json_delete_friend_param, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_delete_friend_param);

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMFriendshipDeleteFriend(Utils.string2intptr(param), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)res;
    }

    /// <summary>
    /// 检测好友关系
    /// Check friend type
    /// </summary>
    /// <param name="json_check_friend_list_param">检测好友关系参数 FriendshipCheckFriendTypeParam</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult FriendshipCheckFriendType(FriendshipCheckFriendTypeParam json_check_friend_list_param, ValueCallback<List<FriendshipCheckFriendTypeResult>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_check_friend_list_param);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<FriendshipCheckFriendTypeResult>>);

      int res = IMNativeSDK.TIMFriendshipCheckFriendType(Utils.string2intptr(param), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)res;
    }
    public static TIMResult FriendshipCheckFriendType(FriendshipCheckFriendTypeParam json_check_friend_list_param, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_check_friend_list_param);

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMFriendshipCheckFriendType(Utils.string2intptr(param), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)res;
    }

    /// <summary>
    /// 创建好友分组
    /// Creagte friend group
    /// </summary>
    /// <param name="json_create_friend_group_param">创建好友分组参数 CreateFriendGroupInfo</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult FriendshipCreateFriendGroup(CreateFriendGroupInfo json_create_friend_group_param, ValueCallback<List<FriendResult>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_create_friend_group_param);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<FriendResult>>);

      int res = IMNativeSDK.TIMFriendshipCreateFriendGroup(Utils.string2intptr(param), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)res;
    }
    public static TIMResult FriendshipCreateFriendGroup(CreateFriendGroupInfo json_create_friend_group_param, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_create_friend_group_param);

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMFriendshipCreateFriendGroup(Utils.string2intptr(param), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)res;
    }

    /// <summary>
    /// 获取好友分组列表
    /// Get friend group list
    /// </summary>
    /// <param name="json_get_friend_group_list_param">获取好友分组，friend_group_name列表</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult FriendshipGetFriendGroupList(List<string> json_get_friend_group_list_param, ValueCallback<List<FriendGroupInfo>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_get_friend_group_list_param);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<FriendGroupInfo>>);

      int res = IMNativeSDK.TIMFriendshipGetFriendGroupList(Utils.string2intptr(param), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)res;
    }
    public static TIMResult FriendshipGetFriendGroupList(List<string> json_get_friend_group_list_param, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_get_friend_group_list_param);

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMFriendshipGetFriendGroupList(Utils.string2intptr(param), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)res;
    }

    /// <summary>
    /// 修改好友分组列表
    /// Modify friend group
    /// </summary>
    /// <param name="json_modify_friend_group_param">修改好友分组 FriendshipModifyFriendGroupParam</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult FriendshipModifyFriendGroup(FriendshipModifyFriendGroupParam json_modify_friend_group_param, ValueCallback<List<FriendResult>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_modify_friend_group_param);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<FriendResult>>);

      int res = IMNativeSDK.TIMFriendshipModifyFriendGroup(Utils.string2intptr(param), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)res;
    }
    public static TIMResult FriendshipModifyFriendGroup(FriendshipModifyFriendGroupParam json_modify_friend_group_param, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_modify_friend_group_param);

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMFriendshipModifyFriendGroup(Utils.string2intptr(param), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)res;
    }

    /// <summary>
    /// 删除好友分组列表
    /// Delete friend group
    /// </summary>
    /// <param name="json_delete_friend_group_param">删除好友分组，friend_group_name列表</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult FriendshipDeleteFriendGroup(List<string> json_delete_friend_group_param, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var list = Utils.ToJson(json_delete_friend_group_param);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      int res = IMNativeSDK.TIMFriendshipDeleteFriendGroup(Utils.string2intptr(list), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, list);
      return (TIMResult)res;
    }
    public static TIMResult FriendshipDeleteFriendGroup(List<string> json_delete_friend_group_param, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var list = Utils.ToJson(json_delete_friend_group_param);

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMFriendshipDeleteFriendGroup(Utils.string2intptr(list), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, list);
      return (TIMResult)res;
    }

    /// <summary>
    /// 添加黑名单
    /// Add to blacklist
    /// </summary>
    /// <param name="json_add_to_blacklist_param">添加黑名单 ，userID列表 (User ID list)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult FriendshipAddToBlackList(List<string> json_add_to_blacklist_param, ValueCallback<List<FriendResult>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var list = Utils.ToJson(json_add_to_blacklist_param);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<FriendResult>>);

      int res = IMNativeSDK.TIMFriendshipAddToBlackList(Utils.string2intptr(list), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, list);
      return (TIMResult)res;
    }
    public static TIMResult FriendshipAddToBlackList(List<string> json_add_to_blacklist_param, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var list = Utils.ToJson(json_add_to_blacklist_param);

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMFriendshipAddToBlackList(Utils.string2intptr(list), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, list);
      return (TIMResult)res;
    }

    /// <summary>
    /// 获取黑名单列表
    /// Get blacklist
    /// </summary>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult FriendshipGetBlackList(ValueCallback<List<FriendProfile>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<FriendProfile>>);

      int res = IMNativeSDK.TIMFriendshipGetBlackList(ValueCallbackInstance, Utils.string2intptr(user_data));

      return (TIMResult)res;
    }
    public static TIMResult FriendshipGetBlackList(ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMFriendshipGetBlackList(StringValueCallbackInstance, Utils.string2intptr(user_data));

      return (TIMResult)res;
    }

    /// <summary>
    /// 从黑名单删除
    /// Delete from blacklist
    /// </summary>
    /// <param name="json_delete_from_blacklist_param">userID列表 (User ID list)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult FriendshipDeleteFromBlackList(List<string> json_delete_from_blacklist_param, ValueCallback<List<FriendResult>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var list = Utils.ToJson(json_delete_from_blacklist_param);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<FriendResult>>);

      int res = IMNativeSDK.TIMFriendshipDeleteFromBlackList(Utils.string2intptr(list), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, list);
      return (TIMResult)res;
    }
    public static TIMResult FriendshipDeleteFromBlackList(List<string> json_delete_from_blacklist_param, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var list = Utils.ToJson(json_delete_from_blacklist_param);

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMFriendshipDeleteFromBlackList(Utils.string2intptr(list), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, list);
      return (TIMResult)res;
    }

    /// <summary>
    /// 获取好友申请未决
    /// Get friend request pendency list
    /// </summary>
    /// <param name="json_get_pendency_list_param">好友申请未决参数 FriendshipGetPendencyListParam</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult FriendshipGetPendencyList(FriendshipGetPendencyListParam json_get_pendency_list_param, ValueCallback<PendencyPage> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_get_pendency_list_param);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<PendencyPage>);

      int res = IMNativeSDK.TIMFriendshipGetPendencyList(Utils.string2intptr(param), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)res;
    }
    public static TIMResult FriendshipGetPendencyList(FriendshipGetPendencyListParam json_get_pendency_list_param, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_get_pendency_list_param);

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMFriendshipGetPendencyList(Utils.string2intptr(param), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)res;
    }

    /// <summary>
    /// 删除好友申请未决
    /// Delete friend request pendency
    /// </summary>
    /// <param name="json_delete_pendency_param">删除好友申请未决参数 FriendshipDeletePendencyParam</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult FriendshipDeletePendency(FriendshipDeletePendencyParam json_delete_pendency_param, ValueCallback<List<FriendResult>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_delete_pendency_param);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<FriendResult>>);

      int res = IMNativeSDK.TIMFriendshipDeletePendency(Utils.string2intptr(param), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)res;
    }
    public static TIMResult FriendshipDeletePendency(FriendshipDeletePendencyParam json_delete_pendency_param, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_delete_pendency_param);

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMFriendshipDeletePendency(Utils.string2intptr(param), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)res;
    }

    /// <summary>
    /// 上报好友申请未决已读
    /// Report friend pendency read
    /// </summary>
    /// <param name="time_stamp">上报时间戳 (Report timestamp)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult FriendshipReportPendencyReaded(ulong time_stamp, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      int res = IMNativeSDK.TIMFriendshipReportPendencyReaded(time_stamp, ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, time_stamp.ToString());
      return (TIMResult)res;
    }
    public static TIMResult FriendshipReportPendencyReaded(ulong time_stamp, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMFriendshipReportPendencyReaded(time_stamp, StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, time_stamp.ToString());
      return (TIMResult)res;
    }

    /// <summary>
    /// 搜索好友
    /// Search friend
    /// </summary>
    /// <param name="json_search_friends_param">搜索参数 FriendSearchParam</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult FriendshipSearchFriends(FriendSearchParam json_search_friends_param, ValueCallback<List<FriendInfoGetResult>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_search_friends_param);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<FriendInfoGetResult>>);

      int res = IMNativeSDK.TIMFriendshipSearchFriends(Utils.string2intptr(param), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)res;
    }
    public static TIMResult FriendshipSearchFriends(FriendSearchParam json_search_friends_param, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_search_friends_param);

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMFriendshipSearchFriends(Utils.string2intptr(param), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param);
      return (TIMResult)res;
    }

    /// <summary>
    /// 获取好友信息
    /// Get friends' info
    /// </summary>
    /// <param name="json_get_friends_info_param">获取好友信息，好友userIDs (Friends' user ID list)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult FriendshipGetFriendsInfo(List<string> json_get_friends_info_param, ValueCallback<List<FriendInfoGetResult>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var list = Utils.ToJson(json_get_friends_info_param);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<FriendInfoGetResult>>);

      int res = IMNativeSDK.TIMFriendshipGetFriendsInfo(Utils.string2intptr(list), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, list);
      return (TIMResult)res;
    }
    public static TIMResult FriendshipGetFriendsInfo(List<string> json_get_friends_info_param, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var list = Utils.ToJson(json_get_friends_info_param);

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMFriendshipGetFriendsInfo(Utils.string2intptr(list), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, list);
      return (TIMResult)res;
    }

    /// <summary>
    /// 实验性接口，开发者一般使用不到，例如私有化等等
    /// Experimental API, not for developers
    /// </summary>
    /// <param name="json_param">实验性接口参数 ExperimentalAPIRequestParam</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult CallExperimentalAPI(ExperimentalAPIRequestParam json_param, ValueCallback<ResponseInfo> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<ResponseInfo>);

      int res = IMNativeSDK.callExperimentalAPI(Utils.string2intptr(Utils.ToJson(json_param)), ValueCallbackInstance, Utils.string2intptr(user_data));

      return (TIMResult)res;
    }
    public static TIMResult CallExperimentalAPI(ExperimentalAPIRequestParam json_param, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.callExperimentalAPI(Utils.string2intptr(Utils.ToJson(json_param)), StringValueCallbackInstance, Utils.string2intptr(user_data));

      return (TIMResult)res;
    }

    /// <summary>
    /// 获取消息已读回执
    /// Get message read receipt
    /// </summary>
    /// <param name="msg_array">消息列表 (Message list)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult MsgGetMessageReadReceipts(List<Message> msg_array, ValueCallback<List<MessageReceipt>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var list = Utils.ToJson(msg_array);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<MessageReceipt>>);
      int res = IMNativeSDK.TIMMsgGetMessageReadReceipts(Utils.string2intptr(list), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, list);
      return (TIMResult)res;
    }
    public static TIMResult MsgGetMessageReadReceipts(List<Message> msg_array, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var list = Utils.ToJson(msg_array);

      ValuecallbackStore.Add(user_data, callback);
      int res = IMNativeSDK.TIMMsgGetMessageReadReceipts(Utils.string2intptr(list), StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, list);
      return (TIMResult)res;
    }

    /// <summary>
    /// 发送消息已读回执
    /// Send message read receipts
    /// </summary>
    /// <param name="msg_array">消息列表 (Message list)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult MsgSendMessageReadReceipts(List<Message> msg_array, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var list = Utils.ToJson(msg_array);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      int res = IMNativeSDK.TIMMsgSendMessageReadReceipts(Utils.string2intptr(list), ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, list);
      return (TIMResult)res;
    }
    public static TIMResult MsgSendMessageReadReceipts(List<Message> msg_array, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var list = Utils.ToJson(msg_array);

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMMsgSendMessageReadReceipts(Utils.string2intptr(list), StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, list);
      return (TIMResult)res;
    }

    /// <summary>
    /// 设置消息扩展（6.7 及其以上版本支持，需要您购买旗舰版套餐）
    /// Set message extensions (Available on native SDK v6.7 or higher, Premium only)
    /// </summary>
    /// <param name="message">消息，消息需满足三个条件：1、消息发送前需设置 supportMessageExtension 为 true，2、消息必须是发送成功的状态，3、消息不能是社群（Community）和直播群（AVChatRoom）消息。(Message fulfills: 1. Sending message with true for supportMessageExtension. 2. Message status is sent. 3. Message not for Community and AVChatRoom)</param>
    /// <param name="extensions">消息扩展信息，如果扩展 key 已经存在，则修改扩展的 value 信息，如果扩展 key 不存在，则新增扩展(Message extensions)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult MsgSetMessageExtensions(Message message, List<MessageExtension> extensions, ValueCallback<List<MessageExtensionResult>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var msg = Utils.ToJson(message);
      var list = Utils.ToJson(extensions);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<MessageExtensionResult>>);

      int res = IMNativeSDK.TIMMsgSetMessageExtensions(Utils.string2intptr(msg), Utils.string2intptr(list), ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, msg, list);
      return (TIMResult)res;
    }

    public static TIMResult MsgSetMessageExtensions(Message message, List<MessageExtension> extensions, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var msg = Utils.ToJson(message);
      var list = Utils.ToJson(extensions);

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMMsgSetMessageExtensions(Utils.string2intptr(msg), Utils.string2intptr(list), StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, msg, list);
      return (TIMResult)res;
    }

    /// <summary>
    /// 获取消息扩展（6.7 及其以上版本支持，需要您购买旗舰版套餐）
    /// Get message extensions (Available on native SDK v6.7 or higher, Premium only)
    /// </summary>
    /// <param name="message">消息(Message)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult MsgGetMessageExtensions(Message message, ValueCallback<List<MessageExtension>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var msg = Utils.ToJson(message);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<MessageExtension>>);

      int res = IMNativeSDK.TIMMsgGetMessageExtensions(Utils.string2intptr(msg), ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, msg);
      return (TIMResult)res;
    }

    public static TIMResult MsgGetMessageExtensions(Message message, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var msg = Utils.ToJson(message);

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMMsgGetMessageExtensions(Utils.string2intptr(msg), StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, msg);
      return (TIMResult)res;
    }
    /// <summary>
    /// 删除消息扩展（6.7 及其以上版本支持，需要您购买旗舰版套餐）
    /// Delete message extensions (Available on native SDK v6.7 or higher, Premium only)
    /// </summary>
    /// <param name="message">消息(Message)</param>
    /// <param name="extensions">扩展信息 key 列表，单次最大支持删除 20 个消息扩展，如果设置为空 ，表示删除消息所有扩展 (Extension key array. Maximum 20/call. If this sets to null, it will delete all the message extensions.)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult MsgDeleteMessageExtensions(Message message, List<MessageExtension> extensions, ValueCallback<List<MessageExtensionResult>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var msg = Utils.ToJson(message);
      var list = Utils.ToJson(extensions);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<MessageExtensionResult>>);

      int res = IMNativeSDK.TIMMsgDeleteMessageExtensions(Utils.string2intptr(msg), Utils.string2intptr(list), ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, msg, list);
      return (TIMResult)res;
    }

    public static TIMResult MsgDeleteMessageExtensions(Message message, List<MessageExtension> extensions, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var msg = Utils.ToJson(message);
      var list = Utils.ToJson(extensions);

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMMsgDeleteMessageExtensions(Utils.string2intptr(msg), Utils.string2intptr(list), StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, msg, list);
      return (TIMResult)res;
    }

    /// <summary>
    /// 添加消息回应 (add message reaction)
    /// </summary>
    /// <param name="message">消息 json 字符串 (message json)</param>
    /// <param name="reaction_id">消息回应 ID，在表情回应场景，reaction_id 为表情 ID，单条消息最大支持 10 个 Reaction，单个 Reaction 最大支持 100 个用户。(Message response ID. In the emoticon response scenario, reaction_id is the emoticon ID. A single message supports a maximum of 10 reactions, and a single reaction supports a maximum of 100 users.)</param>
    /// <param name="callback"></param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult MsgAddMessageReaction(Message message, string reaction_id, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var msg = Utils.ToJson(message);
      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);
      int res = IMNativeSDK.TIMMsgAddMessageReaction(Utils.string2intptr(msg), Utils.string2intptr(reaction_id), ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, msg, reaction_id);
      return (TIMResult)res;
    }

    public static TIMResult MsgAddMessageReaction(Message message, string reaction_id, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var msg = Utils.ToJson(message);
      ValuecallbackStore.Add(user_data, callback);
      int res = IMNativeSDK.TIMMsgAddMessageReaction(Utils.string2intptr(msg), Utils.string2intptr(reaction_id), StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, msg, reaction_id);
      return (TIMResult)res;
    }
    /// <summary>
    /// 删除消息回应 (remove message reaction)
    /// </summary>
    /// <param name="message">消息 json 字符串 (message json)</param>
    /// <param name="reaction_id">消息回应 ID，在表情回应场景，reaction_id 为表情 ID，单条消息最大支持 10 个 Reaction，单个 Reaction 最大支持 100 个用户。(Message response ID. In the emoticon response scenario, reaction_id is the emoticon ID. A single message supports a maximum of 10 reactions, and a single reaction supports a maximum of 100 users.)</param>
    /// <param name="callback"></param>
    /// <returns><see cref="TIMResult"/></returns> 
    public static TIMResult MsgRemoveMessageReaction(Message message, string reaction_id, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();
      var msg = Utils.ToJson(message);
      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);
      int res = IMNativeSDK.TIMMsgRemoveMessageReaction(Utils.string2intptr(msg), Utils.string2intptr(reaction_id), ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, msg, reaction_id);
      return (TIMResult)res;
    }

    public static TIMResult MsgRemoveMessageReaction(Message message, string reaction_id, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var msg = Utils.ToJson(message);
      ValuecallbackStore.Add(user_data, callback);
      int res = IMNativeSDK.TIMMsgRemoveMessageReaction(Utils.string2intptr(msg), Utils.string2intptr(reaction_id), StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, msg, reaction_id);
      return (TIMResult)res;
    }
    /// <summary>
    /// 批量拉取多条消息回应信息
    /// Get message reactions 
    /// </summary>
    /// <param name="message">消息列表，一次最大支持 20 条消息，消息必须属于同一个会话。(Message list supports up to 20 messages at a time, and the messages must belong to the same conversation.)</param>
    /// <param name="max_user_count_per_reaction">取值范围 【0,10】，每个 Reaction 最多只返回前 10 个用户信息，如需更多用户信息，可以按需调用 TIMMsgGetMessageReactionUserList 接口分页拉取。(The value range is [0,10]. Each Reaction only returns the first 10 user information at most. If you need more user information, you can call the TIMMsgGetMessageReactionUserList interface to pull it in pages as needed.)</param>
    /// <param name="callback"></param>
    /// <returns><see cref="TIMResult"/></returns>

    public static TIMResult MsgGetMessageReactions(List<Message> message, int max_user_count_per_reaction, ValueCallback<List<MessageReactionResult>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var msg = Utils.ToJson(message);
      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<MessageReactionResult>>);
      int timSucc = IMNativeSDK.TIMMsgGetMessageReactions(Utils.string2intptr(msg), max_user_count_per_reaction, ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)timSucc;
    }
    public static TIMResult MsgGetMessageReactions(List<Message> message, int max_user_count_per_reaction, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var msg = Utils.ToJson(message);
      ValuecallbackStore.Add(user_data, callback);
      int timSucc = IMNativeSDK.TIMMsgGetMessageReactions(Utils.string2intptr(msg), max_user_count_per_reaction, StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 分页拉取使用指定消息回应用户信息
    /// Get all user list of message reaction
    /// </summary>
    /// <param name="message">消息 json 字符串 (message object)</param>
    /// <param name="reaction_id">消息回应 ID，在表情回复场景，reaction_id 为表情 ID。(Message reaction ID)</param>
    /// <param name="next_seq">分页拉取的游标，第一次传 0，后续分页传 succ 返回的 nextSeq。(	The next pulling-by-page cursor, pass 0 for the first time, and pass the nextSeq returned by succ for subsequent pagination.)</param>
    /// <param name="count">一次分页最大拉取个数，最大支持 100 个。(The maximum count of users fetched per page, up to 100.)</param>
    /// <param name="callback"></param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult MsgGetAllUserListOfMessageReaction(Message message, string reaction_id, ulong next_seq, int count, ValueCallback<MessageReactionUserResult> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var msg = Utils.ToJson(message);
      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<MessageReactionUserResult>);
      int timSucc = IMNativeSDK.TIMMsgGetAllUserListOfMessageReaction(Utils.string2intptr(msg), Utils.string2intptr(reaction_id), next_seq, count, ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)timSucc;
    }
    public static TIMResult MsgGetAllUserListOfMessageReaction(Message message, string reaction_id, ulong next_seq, int count, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();
  
      ValuecallbackStore.Add(user_data, callback);

      var msg = Utils.ToJson(message);
      int timSucc = IMNativeSDK.TIMMsgGetAllUserListOfMessageReaction(Utils.string2intptr(msg), Utils.string2intptr(reaction_id), next_seq, count, StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 语音转文本
    /// Convert voice to text
    /// </summary>
    /// <param name="url">语音文件 url (Voice file url)</param>
    /// <param name="language">语言 (Language)</param>
    /// <param name="callback"></param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult MsgConvertVoiceToText(string url, string language, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMMsgConvertVoiceToText(Utils.string2intptr(url), Utils.string2intptr(language), StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, url, language);
      return (TIMResult)res;
    }

    /// <summary>
    /// 设置群消息置顶
    /// Set pinned group message
    /// </summary>
    /// <param name="group_id">群 ID (Group ID)</param>
    /// <param name="message">消息体 (Message object)</param>
    /// <param name="is_pinned">是否置顶 (Whether to pin)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult PinGroupMessage(string group_id, Message message, bool is_pinned, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      string json_msg = Utils.ToJson(message);
      int res = IMNativeSDK.TIMPinGroupMessage(Utils.string2intptr(group_id), Utils.string2intptr(json_msg), is_pinned, ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    public static TIMResult PinGroupMessage(string group_id, Message message, bool is_pinned, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      string json_msg = Utils.ToJson(message);
      int res = IMNativeSDK.TIMPinGroupMessage(Utils.string2intptr(group_id), Utils.string2intptr(json_msg), is_pinned, StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    /// <summary>
    /// 获取置顶群消息列表
    /// Get pinned group message list
    /// </summary>
    /// <param name="group_id">群 ID (Group ID)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult GetPinnedGroupMessageList(string group_id, ValueCallback<List<Message>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<Message>>);

      int res = IMNativeSDK.TIMGetPinnedGroupMessageList(Utils.string2intptr(group_id), ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    public static TIMResult GetPinnedGroupMessageList(string group_id, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMGetPinnedGroupMessageList(Utils.string2intptr(group_id), StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    /// <summary>
    /// 翻译文本消息
    /// Translate text
    /// </summary>
    /// <param name="json_source_text_array">待翻译文本数组 (Source text array)</param>
    /// <param name="source_language">源语言 (Source language)</param>
    /// <param name="target_language">目标语言 (Target language)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult MsgTranslateText(List<string> json_source_text_array, string source_language, string target_language, ValueCallback<List<MessageTranslateTextResult>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      return HandleTranslateText(json_source_text_array, source_language, target_language, fn_name, callback, null);
    }
    public static TIMResult MsgTranslateText(List<string> json_source_text_array, string source_language, string target_language, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      return HandleTranslateText(json_source_text_array, source_language, target_language, fn_name, null, callback);
    }

    private static TIMResult HandleTranslateText(List<string> json_source_text_array, string source_language, string target_language, string fn_name, ValueCallback<List<MessageTranslateTextResult>> callback = null, ValueCallback<string> strCallback = null)
    {
      string user_data = Utils.GetUserDataFromFnName(fn_name);
      int timSucc;
      var textArrayStr = Utils.ToJson(json_source_text_array);
      var textArrayPtr = Utils.string2intptr(textArrayStr);
      var sourceLangPtr = Utils.string2intptr(source_language);
      var targetLangPtr = Utils.string2intptr(target_language);

      if (callback != null)
      {
        ValuecallbackDeleStore.Add(user_data, threadOperation<List<MessageTranslateTextResult>>);
        ValuecallbackStore.Add(user_data, callback);
        timSucc = IMNativeSDK.TIMMsgTranslateText(textArrayPtr, sourceLangPtr, targetLangPtr, ValueCallbackInstance, Utils.string2intptr(user_data));
      }
      else
      {
        ValuecallbackStore.Add(user_data, strCallback);
        timSucc = IMNativeSDK.TIMMsgTranslateText(textArrayPtr, sourceLangPtr, targetLangPtr, StringValueCallbackInstance, Utils.string2intptr(user_data));
      }

      Log(user_data, textArrayStr, source_language, target_language);
      return (TIMResult)timSucc;
    }

    /// <summary>
    /// 获取群消息已读群成员列表
    /// Get group message read member list
    /// </summary>
    /// <param name="message">单条群消息 (Group message)</param>
    /// <param name="filter">指定拉取已读或未读群成员列表 (Group message read member's filter)</param>
    /// <param name="next_seq">分页拉取的游标，第一次默认取传 0，后续分页拉取时，传上一次分页拉取成功回调里的 next_seq (Next seq as page index, default: 0 and use the last next_seq returned from the callback as the next seq)</param>
    /// <param name="count">分页拉取的个数，最大支持 100 个。(Page size, maximum 100)</param>
    /// <param name="callback">回调 MsgGroupMessageReadMemberListCallback (Callback)</param>
    /// <param name="stringCallback">string 类型回调 MsgGroupMessageReadMemberListStringCallback (String data type callback)</param>
    public static TIMResult GetMsgGroupMessageReadMemberList(Message message, TIMGroupMessageReadMembersFilter filter, ulong next_seq, int count, MsgGroupMessageReadMemberListCallback callback = null, MsgGroupMessageReadMemberListStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        MsgGroupMessageReadMemberListCallbackStore = callback;
      }

      if (stringCallback != null)
      {
        MsgGroupMessageReadMemberListStringCallbackStore = stringCallback;
      }

      var msg = Utils.ToJson(message);

      int res = IMNativeSDK.TIMMsgGetGroupMessageReadMemberList(Utils.string2intptr(msg), filter, next_seq, count, TIMMsgGroupMessageReadMemberListCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, msg, filter.ToString(), next_seq.ToString(), count.ToString());
      return (TIMResult)res;
    }

#if !UNITY_EDITOR && UNITY_WEBGL
    /// <summary>
    /// 获取群消息已读群成员列表
    /// Get group message read member list (Web Only, different callback)
    /// </summary>
    /// <param name="message">单条群消息 (Group message)</param>
    /// <param name="filter">指定拉取已读或未读群成员列表 (Group message read member's filter)</param>
    /// <param name="next_seq">分页拉取的游标，第一次默认取传 0，后续分页拉取时，传上一次分页拉取成功回调里的 next_seq (Next seq as page index, default: 0 and use the last next_seq returned from the callback as the next seq)</param>
    /// <param name="count">分页拉取的个数，最大支持 100 个。(Page size, maximum 100)</param>
    /// <param name="callback">回调 MsgGroupMessageReadMemberListCallback (Callback)</param>
    /// <param name="stringCallback">string 类型回调 MsgGroupMessageReadMemberListStringCallback (String data type callback)</param>
    public static TIMResult MsgGetGroupMessageReadMemberListWeb(Message message, TIMGroupMessageReadMembersFilter filter, string next_seq, int count, IMWebSDK.MsgGroupMessageReadMemberListCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();
      string msg = Utils.ToJson(message);
      MsgGroupMessageReadMemberListCallbackWebStore = callback;
      int res = IMWebSDK.TIMMsgGetGroupMessageReadMemberListWeb(Utils.string2intptr(msg), filter, Utils.string2intptr(next_seq), count, TIMMsgGroupMessageReadMemberListCallbackInstanceWeb, Utils.string2intptr(user_data));
      return (TIMResult)res;
    }
#endif

    /// <summary>
    /// 关注用户
    /// Follow user
    /// </summary>
    /// <param name="user_id_list">用户 ID 列表 (User ID list)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult FollowUser(List<string> user_id_list, ValueCallback<FollowOperationResult> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<FollowOperationResult>>);

      var user_list = Utils.ToJson(user_id_list);
      int res = IMNativeSDK.TIMFollowUser(Utils.string2intptr(user_list), ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    public static TIMResult FollowUser(List<string> user_id_list, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      var user_list = Utils.ToJson(user_id_list);
      int res = IMNativeSDK.TIMFollowUser(Utils.string2intptr(user_list), StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    /// <summary>
    /// 取消关注用户
    /// Unfollow user
    /// </summary>
    /// <param name="user_id_list">用户 ID 列表 (User ID list)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult UnfollowUser(List<string> user_id_list, ValueCallback<FollowOperationResult> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<FollowOperationResult>>);

      var user_list = Utils.ToJson(user_id_list);
      int res = IMNativeSDK.TIMUnfollowUser(Utils.string2intptr(user_list), ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    public static TIMResult UnfollowUser(List<string> user_id_list, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      var user_list = Utils.ToJson(user_id_list);
      int res = IMNativeSDK.TIMUnfollowUser(Utils.string2intptr(user_list), StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    /// <summary>
    /// 获取我关注的用户列表
    /// Get my following users list
    /// </summary>
    /// <param name="next_cursor">分页拉取的游标 (Pulling-by-page flag)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult GetMyFollowingList(string next_cursor, ValueCallback<FollowListResult> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<FollowListResult>);

      int res = IMNativeSDK.TIMGetMyFollowingList(Utils.string2intptr(next_cursor), ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    public static TIMResult GetMyFollowingList(string next_cursor, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMGetMyFollowingList(Utils.string2intptr(next_cursor), StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    /// <summary>
    /// 获取我的粉丝用户列表
    /// Get my followers list
    /// </summary>
    /// <param name="next_cursor">分页拉取的游标 (Pulling-by-page flag)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult GetMyFollowersList(string next_cursor, ValueCallback<FollowListResult> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<FollowListResult>);

      int res = IMNativeSDK.TIMGetMyFollowersList(Utils.string2intptr(next_cursor), ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    public static TIMResult GetMyFollowersList(string next_cursor, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMGetMyFollowersList(Utils.string2intptr(next_cursor), StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    /// <summary>
    /// 获取我的互关用户列表
    /// Get my mutual followers list
    /// </summary>
    /// <param name="next_cursor">分页拉取的游标 (Pulling-by-page flag)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult GetMutualFollowersList(string next_cursor, ValueCallback<FollowListResult> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<FollowListResult>);

      int res = IMNativeSDK.TIMGetMutualFollowersList(Utils.string2intptr(next_cursor), ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    public static TIMResult GetMutualFollowersList(string next_cursor, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMGetMutualFollowersList(Utils.string2intptr(next_cursor), StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    /// <summary>
    /// 获取用户关注的数量信息
    /// Get users' following count information
    /// </summary>
    /// <param name="user_id_list">用户 ID 列表 (User ID list)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult GetUserFollowInfo(List<string> user_id_list, ValueCallback<FollowInfo> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<FollowInfo>);

      var user_list = Utils.ToJson(user_id_list);
      int res = IMNativeSDK.TIMGetUserFollowInfo(Utils.string2intptr(user_list), ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    public static TIMResult GetUserFollowInfo(List<string> user_id_list, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      var user_list = Utils.ToJson(user_id_list);
      int res = IMNativeSDK.TIMGetUserFollowInfo(Utils.string2intptr(user_list), StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    /// <summary>
    /// 检查用户关注类型
    /// Check users' following type
    /// </summary>
    /// <param name="user_id_list">用户 ID 列表 (User ID list)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult CheckFollowType(List<string> user_id_list, ValueCallback<FollowTypeCheckResult> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<FollowTypeCheckResult>);

      var user_list = Utils.ToJson(user_id_list);
      int res = IMNativeSDK.TIMCheckFollowType(Utils.string2intptr(user_list), ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    public static TIMResult CheckFollowType(List<string> user_id_list, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      var user_list = Utils.ToJson(user_id_list);
      int res = IMNativeSDK.TIMCheckFollowType(Utils.string2intptr(user_list), StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    /// <summary>
    /// 订阅公众号
    /// Subscribe to an official account
    /// </summary>
    /// <param name="official_account_id">公众号 ID (Official account ID)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult SubscribeOfficialAccount(string official_account_id, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      int res = IMNativeSDK.TIMSubscribeOfficialAccount(Utils.string2intptr(official_account_id), ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    public static TIMResult SubscribeOfficialAccount(string official_account_id, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMSubscribeOfficialAccount(Utils.string2intptr(official_account_id), StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    /// <summary>
    /// 取消订阅公众号
    /// Unsubscribe to an official account
    /// </summary>
    /// <param name="official_account_id">公众号 ID (Official account ID)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult UnsubscribeOfficialAccount(string official_account_id, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      int res = IMNativeSDK.TIMUnsubscribeOfficialAccount(Utils.string2intptr(official_account_id), ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    public static TIMResult UnsubscribeOfficialAccount(string official_account_id, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMUnsubscribeOfficialAccount(Utils.string2intptr(official_account_id), StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    /// <summary>
    /// 获取公众号信息
    /// Get official account information
    /// </summary>
    /// <param name="official_account_id_list">公众号 ID 列表 (Official account ID list)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult GetOfficialAccountsInfo(List<string> official_account_id_list, ValueCallback<GetOfficialAccountInfoResult> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<GetOfficialAccountInfoResult>);

      var json_param = Utils.ToJson(official_account_id_list);
      int res = IMNativeSDK.TIMGetOfficialAccountsInfo(Utils.string2intptr(json_param), ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    public static TIMResult GetOfficialAccountsInfo(List<string> official_account_id_list, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      var json_param = Utils.ToJson(official_account_id_list);
      int res = IMNativeSDK.TIMGetOfficialAccountsInfo(Utils.string2intptr(json_param), StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    /// <summary>
    /// 邀请某人
    /// Invite someone
    /// </summary>
    /// <param name="invitee">被邀请用户 ID (Invitee user ID)</param>
    /// <param name="data">自定义数据 (Custom data)</param>
    /// <param name="online_user_only">是否只邀请在线用户 (Whether the message can be received only by online users)</param>
    /// <param name="json_offline_push_info">离线推送信息，其中 offline_push_config_desc 字段为必填 (Offline push information, the offline_push_config_desc field is required.)</param>
    /// <param name="timeout">超时时间 (Timeout)</param>
    /// <param name="invite_id_buffer">承接邀请信令的 ID 的 StringBuilder, 容量不低于 128 字节 (StringBuilder for receiving invitation ID, the capacity cannot be less than 128 bytes)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult SignalingInvite(string invitee, string data, bool online_user_only, OfflinePushConfig offline_push_info, int timeout, StringBuilder invite_id_buffer, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
            ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      var json_offline_push_info = Utils.ToJson(offline_push_info);
      int res = IMNativeSDK.TIMSignalingInvite(Utils.string2intptr(invitee), Utils.string2intptr(data), online_user_only, Utils.string2intptr(json_offline_push_info), timeout, invite_id_buffer, ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    public static TIMResult SignalingInvite(string invitee, string data, bool online_user_only, OfflinePushConfig offline_push_info, int timeout, StringBuilder invite_id_buffer, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      var json_offline_push_info = Utils.ToJson(offline_push_info);
      int res = IMNativeSDK.TIMSignalingInvite(Utils.string2intptr(invitee), Utils.string2intptr(data), online_user_only, Utils.string2intptr(json_offline_push_info), timeout, invite_id_buffer, StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    /// <summary>
    /// 邀请群内的某些人
    /// Invite certain users in the group
    /// </summary>
    /// <param name="group_id">群组 ID (Group ID)</param>
    /// <param name="invitee_list">被邀请的用户列表 (Invitee list)</param>
    /// <param name="data">自定义数据 (Custom data)</param>
    /// <param name="online_user_only">是否只邀请在线用户 (Whether the message can be received only by online users)</param>
    /// <param name="timeout">超时时间 (Timeout)</param>
    /// <param name="invite_id_buffer">承接邀请信令的 ID 的 StringBuilder, 容量不低于 128 字节 (StringBuilder for receiving invitation ID, the capacity cannot be less than 128 bytes)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult SignalingInviteInGroup(string group_id, List<string> invitee_list, string data, bool online_user_only, int timeout, StringBuilder invite_id_buffer, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      var json_invitee_array = Utils.ToJson(invitee_list);
      int res = IMNativeSDK.TIMSignalingInviteInGroup(Utils.string2intptr(group_id), Utils.string2intptr(json_invitee_array), Utils.string2intptr(data), online_user_only, timeout, invite_id_buffer, ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    public static TIMResult SignalingInviteInGroup(string group_id, List<string> invitee_list, string data, bool online_user_only, int timeout, StringBuilder invite_id_buffer, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      var json_invitee_array = Utils.ToJson(invitee_list);
      int res = IMNativeSDK.TIMSignalingInviteInGroup(Utils.string2intptr(group_id), Utils.string2intptr(json_invitee_array), Utils.string2intptr(data), online_user_only, timeout, invite_id_buffer, StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    /// <summary>
    /// 取消邀请
    /// Cancel an invitation (called by the inviter)
    /// </summary>
    /// <param name="invite_id">邀请信令的 ID (Invitation ID)</param>
    /// <param name="data">自定义数据 (Custom data)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult SignalingCancel(string invite_id, string data, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      int res = IMNativeSDK.TIMSignalingCancel(Utils.string2intptr(invite_id), Utils.string2intptr(data), ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    public static TIMResult SignalingCancel(string invite_id, string data, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMSignalingCancel(Utils.string2intptr(invite_id), Utils.string2intptr(data), StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    /// <summary>
    /// 接受邀请
    /// The invitee accepts the invitation
    /// </summary>
    /// <param name="invite_id">邀请信令的 ID (Invitation ID)</param>
    /// <param name="data">自定义数据 (Custom data)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult SignalingAccept(string invite_id, string data, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      int res = IMNativeSDK.TIMSignalingAccept(Utils.string2intptr(invite_id), Utils.string2intptr(data), ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    public static TIMResult SignalingAccept(string invite_id, string data, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMSignalingAccept(Utils.string2intptr(invite_id), Utils.string2intptr(data), StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    /// <summary>
    /// 拒绝邀请
    /// The invitee rejects the invitation
    /// </summary>
    /// <param name="invite_id">邀请信令的 ID (Invitation ID)</param>
    /// <param name="data">自定义数据 (Custom data)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult SignalingReject(string invite_id, string data, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      int res = IMNativeSDK.TIMSignalingReject(Utils.string2intptr(invite_id), Utils.string2intptr(data), ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    public static TIMResult SignalingReject(string invite_id, string data, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMSignalingReject(Utils.string2intptr(invite_id), Utils.string2intptr(data), StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    /// <summary>
    /// 获取邀请信息
    /// Get the signaling information
    /// </summary>
    /// <param name="message">消息体 (Message object)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult GetSignalingInfo(Message message, ValueCallback<SignalingInfo> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<SignalingInfo>);

      var json_msg = Utils.ToJson(message);
      int res = IMNativeSDK.TIMGetSignalingInfo(Utils.string2intptr(json_msg), ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    public static TIMResult GetSignalingInfo(Message msg,  ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      var json_msg = Utils.ToJson(msg);
      int res = IMNativeSDK.TIMGetSignalingInfo(Utils.string2intptr(json_msg), StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    /// <summary>
    /// 修改邀请信息
    /// Modify the invitation information
    /// </summary>
    /// <param name="invite_id">邀请信令的 ID (Invitation ID)</param>
    /// <param name="data">修改的内容 (Modification content)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult SignalingModifyInvitation(string invite_id, string data, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      int res = IMNativeSDK.TIMSignalingModifyInvitation(Utils.string2intptr(invite_id), Utils.string2intptr(data), ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    public static TIMResult SignalingModifyInvitation(string invite_id, string data, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMSignalingModifyInvitation(Utils.string2intptr(invite_id), Utils.string2intptr(data), StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    /// <summary>
    /// 创建社群
    /// Create a community
    /// </summary>
    /// <param name="community_create_param">创建社群的参数 (Create community parameters)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult CommunityCreate(CreateGroupParam community_create_param, ValueCallback<CreateGroupResult> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<CreateGroupResult>);

      var json_community_create_param = Utils.ToJson(community_create_param);
      int res = IMNativeSDK.TIMCommunityCreate(Utils.string2intptr(json_community_create_param), ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    public static TIMResult CommunityCreate(CreateGroupParam community_create_param, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      var json_community_create_param = Utils.ToJson(community_create_param);
      int res = IMNativeSDK.TIMCommunityCreate(Utils.string2intptr(json_community_create_param), StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    /// <summary>
    /// 获取已加入的社群列表
    /// Get joined community list
    /// </summary>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult CommunityGetJoinedCommunityList(ValueCallback<GroupBaseInfo> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<GroupBaseInfo>);

      int res = IMNativeSDK.TIMCommunityGetJoinedCommunityList(ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    public static TIMResult CommunityGetJoinedCommunityList(ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMCommunityGetJoinedCommunityList(StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    /// <summary>
    /// 在社群中创建话题
    /// Create a topic in the community
    /// </summary>
    /// <param name="group_id">群 ID (Group ID)</param>
    /// <param name="topic_info">创建话题的参数 (Create topic parameters)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    public static TIMResult CommunityCreateTopicInCommunity(string group_id, TopicInfo topic_info, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      var json_topic_info = Utils.ToJson(topic_info);
      int res = IMNativeSDK.TIMCommunityCreateTopicInCommunity(Utils.string2intptr(group_id), Utils.string2intptr(json_topic_info), StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    /// <summary>
    /// 从社群中删除话题
    /// Delete a topic from the community
    /// </summary>
    /// <param name="group_id">群 ID (Group ID)</param>
    /// <param name="topic_id_list">话题 ID 列表 (Topic ID list)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult CommunityDeleteTopicFromCommunity(string group_id, List<string> topic_id_list, ValueCallback<GroupTopicOperationResult> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<GroupTopicOperationResult>);

      var json_topic_id_array = Utils.ToJson(topic_id_list);
      int res = IMNativeSDK.TIMCommunityDeleteTopicFromCommunity(Utils.string2intptr(group_id), Utils.string2intptr(json_topic_id_array), ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    public static TIMResult CommunityDeleteTopicFromCommunity(string group_id, List<string> topic_id_list, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      var json_topic_id_array = Utils.ToJson(topic_id_list);
      int res = IMNativeSDK.TIMCommunityDeleteTopicFromCommunity(Utils.string2intptr(group_id), Utils.string2intptr(json_topic_id_array), StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    /// <summary>
    /// 设置话题信息
    /// Set topic information
    /// </summary>
    /// <param name="topic_info">话题信息 (Topic information)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult CommunitySetTopicInfo(TopicInfo topic_info, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      var json_topic_info = Utils.ToJson(topic_info);
      int res = IMNativeSDK.TIMCommunitySetTopicInfo(Utils.string2intptr(json_topic_info), ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    public static TIMResult CommunitySetTopicInfo(TopicInfo topic_info, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      var json_topic_info = Utils.ToJson(topic_info);
      int res = IMNativeSDK.TIMCommunitySetTopicInfo(Utils.string2intptr(json_topic_info), StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    /// <summary>
    /// 获取话题列表
    /// Get topic list
    /// </summary>
    /// <param name="group_id">群 ID (Group ID)</param>
    /// <param name="topic_id_list">话题 ID 列表 (Topic ID list)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult CommunityGetTopicInfoList(string group_id, List<string> topic_id_list, ValueCallback<List<TopicInfo>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<TopicInfo>>);

      var json_topic_id_array = Utils.ToJson(topic_id_list);
      int res = IMNativeSDK.TIMCommunityGetTopicInfoList(Utils.string2intptr(group_id), Utils.string2intptr(json_topic_id_array), ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    public static TIMResult CommunityGetTopicInfoList(string group_id, List<string> topic_id_list, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      var json_topic_id_array = Utils.ToJson(topic_id_list);
      int res = IMNativeSDK.TIMCommunityGetTopicInfoList(Utils.string2intptr(group_id), Utils.string2intptr(json_topic_id_array), StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    /// <summary>
    /// 设置话题中的消息接收选项是否继承社群
    /// Set whether the message receiving option in the topic is inherited from the community
    /// </summary>
    /// <param name="topic_id">话题 ID (Topic ID)</param>
    /// <param name="isInherit">是否继承 (Whether to inherit community message receive option)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult CommunitySetTopicInheritMessageReceiveOptionFromCommunity(string topic_id, bool isInherit, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      int res = IMNativeSDK.TIMCommunitySetTopicInheritMessageReceiveOptionFromCommunity(Utils.string2intptr(topic_id), isInherit, ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    public static TIMResult CommunitySetTopicInheritMessageReceiveOptionFromCommunity(string topic_id, bool isInherit, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMCommunitySetTopicInheritMessageReceiveOptionFromCommunity(Utils.string2intptr(topic_id), isInherit, StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    /// <summary>
    /// 创建权限组
    /// Create permission group
    /// </summary>
    /// <param name="permission_group_info">权限组信息 (Permission group info)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult CommunityCreatePermissionGroupInCommunity(PermissionGroupInfo permission_group_info, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      var json_permission_group_info = Utils.ToJson(permission_group_info);

      int res = IMNativeSDK.TIMCommunityCreatePermissionGroupInCommunity(Utils.string2intptr(json_permission_group_info), StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    /// <summary>
    /// 删除权限组
    /// Delete permission group
    /// </summary>
    /// <param name="group_id">社群 ID (Community ID)</param>
    /// <param name="permission_group_id_list">权限组 ID 列表 (Permission group ID list)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult CommunityDeletePermissionGroupFromCommunity(string group_id, List<string> permission_group_id_list, ValueCallback<List<PermissionGroupOperationResult>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<PermissionGroupOperationResult>>);

      var json_permission_group_id_array = Utils.ToJson(permission_group_id_list);
      int res = IMNativeSDK.TIMCommunityDeletePermissionGroupFromCommunity(Utils.string2intptr(group_id), Utils.string2intptr(json_permission_group_id_array), StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    public static TIMResult CommunityDeletePermissionGroupFromCommunity(string group_id, List<string> permission_group_id_list, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      var json_permission_group_id_array = Utils.ToJson(permission_group_id_list);
      int res = IMNativeSDK.TIMCommunityDeletePermissionGroupFromCommunity(Utils.string2intptr(group_id), Utils.string2intptr(json_permission_group_id_array), StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    /// <summary>
    /// 修改权限组信息
    /// Modify permission group info
    /// </summary>
    /// <param name="permission_group_info">权限组信息 (Permission group info)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult CommunityModifyPermissionGroupInfoInCommunity(PermissionGroupInfo permission_group_info, NullValueCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<object>);

      var json_permission_group_info = Utils.ToJson(permission_group_info);
      int res = IMNativeSDK.TIMCommunityModifyPermissionGroupInfoInCommunity(Utils.string2intptr(json_permission_group_info), ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    public static TIMResult CommunityModifyPermissionGroupInfoInCommunity(PermissionGroupInfo permission_group_info, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      var json_permission_group_info = Utils.ToJson(permission_group_info);
      int res = IMNativeSDK.TIMCommunityModifyPermissionGroupInfoInCommunity(Utils.string2intptr(json_permission_group_info), StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    /// <summary>
    /// 获取已加入的权限组列表
    /// Get joined permission group list
    /// </summary>
    /// <param name="group_id">群 ID (Group ID)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult CommunityGetJoinedPermissionGroupListInCommunity(string group_id, ValueCallback<List<PermissionGroupInfoResult>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<PermissionGroupInfoResult>>);

      int res = IMNativeSDK.TIMCommunityGetJoinedPermissionGroupListInCommunity(Utils.string2intptr(group_id), ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    public static TIMResult CommunityGetJoinedPermissionGroupListInCommunity(string group_id,  ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMCommunityGetJoinedPermissionGroupListInCommunity(Utils.string2intptr(group_id), StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    /// <summary>
    /// 获取权限组列表
    /// Get permission group list
    /// </summary>
    /// <param name="group_id">社群 ID (Community ID)</param>
    /// <param name="permission_group_id_list">权限组 ID 列表 (Permission group ID list)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult CommunityGetPermissionGroupListInCommunity(string group_id, List<string> permission_group_id_list, ValueCallback<List<PermissionGroupInfoResult>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<PermissionGroupInfoResult>>);

      var json_permission_group_id_array = Utils.ToJson(permission_group_id_list);
      int res = IMNativeSDK.TIMCommunityGetPermissionGroupListInCommunity(Utils.string2intptr(group_id), Utils.string2intptr(json_permission_group_id_array), ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    public static TIMResult CommunityGetPermissionGroupListInCommunity(string group_id, List<string> permission_group_id_list, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      var json_permission_group_id_array = Utils.ToJson(permission_group_id_list);
      int res = IMNativeSDK.TIMCommunityGetPermissionGroupListInCommunity(Utils.string2intptr(group_id), Utils.string2intptr(json_permission_group_id_array), StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    /// <summary>
    /// 添加社群成员到权限组
    /// Add community members to permission group
    /// </summary>
    /// <param name="group_id">群 ID (Group ID)</param>
    /// <param name="permission_group_id">权限组 ID (Permission group ID)</param>
    /// <param name="member_id_list">成员 ID 列表 (Member ID list)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult CommunityAddCommunityMembersToPermissionGroup(string group_id, string permission_group_id, List<string> member_id_list, ValueCallback<List<PermissionGroupMemberOperationResult>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<PermissionGroupMemberOperationResult>>);

      var json_member_id_array = Utils.ToJson(member_id_list);
      int res = IMNativeSDK.TIMCommunityAddCommunityMembersToPermissionGroup(Utils.string2intptr(group_id), Utils.string2intptr(permission_group_id), Utils.string2intptr(json_member_id_array), ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    public static TIMResult CommunityAddCommunityMembersToPermissionGroup(string group_id, string permission_group_id, List<string> member_id_list, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      var json_member_id_array = Utils.ToJson(member_id_list);
      int res = IMNativeSDK.TIMCommunityAddCommunityMembersToPermissionGroup(Utils.string2intptr(group_id), Utils.string2intptr(permission_group_id), Utils.string2intptr(json_member_id_array), StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    /// <summary>
    /// 从权限组移除社群成员
    /// Remove community members from permission group
    /// </summary>
    /// <param name="group_id">群 ID (Group ID)</param>
    /// <param name="permission_group_id">权限组 ID (Permission group ID)</param>
    /// <param name="member_id_list">成员 ID 列表 (Member ID list)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult CommunityRemoveCommunityMembersFromPermissionGroup(string group_id, string permission_group_id, List<string> member_id_list, ValueCallback<List<PermissionGroupMemberOperationResult>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<PermissionGroupMemberOperationResult>>);

      var json_member_id_array = Utils.ToJson(member_id_list);
      int res = IMNativeSDK.TIMCommunityRemoveCommunityMembersFromPermissionGroup(Utils.string2intptr(group_id), Utils.string2intptr(permission_group_id), Utils.string2intptr(json_member_id_array), ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    public static TIMResult CommunityRemoveCommunityMembersFromPermissionGroup(string group_id, string permission_group_id, List<string> member_id_list, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      var json_member_id_array = Utils.ToJson(member_id_list);
      int res = IMNativeSDK.TIMCommunityRemoveCommunityMembersFromPermissionGroup(Utils.string2intptr(group_id), Utils.string2intptr(permission_group_id), Utils.string2intptr(json_member_id_array), StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    /// <summary>
    /// 获取权限组内社群成员列表
    /// Get community member list in permission group
    /// </summary>
    /// <param name="group_id">群 ID (Group ID)</param>
    /// <param name="permission_group_id">权限组 ID (Permission group ID)</param>
    /// <param name="next_cursor">分页拉取的游标 (Pulling-by-page flag)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult CommunityGetCommunityMemberListInPermissionGroup(string group_id, string permission_group_id, string next_cursor, ValueCallback<List<PermissionGroupMemberInfoResult>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<PermissionGroupMemberInfoResult>>);

      int res = IMNativeSDK.TIMCommunityGetCommunityMemberListInPermissionGroup(Utils.string2intptr(group_id), Utils.string2intptr(permission_group_id), Utils.string2intptr(next_cursor), ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    public static TIMResult CommunityGetCommunityMemberListInPermissionGroup(string group_id, string permission_group_id, string next_cursor, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.TIMCommunityGetCommunityMemberListInPermissionGroup(Utils.string2intptr(group_id), Utils.string2intptr(permission_group_id), Utils.string2intptr(next_cursor), StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    /// <summary>
    /// 添加话题权限到权限组
    /// Add topic permission to permission group
    /// </summary>
    /// <param name="group_id">群 ID (Group ID)</param>
    /// <param name="permission_group_id">权限组 ID (Permission group ID)</param>
    /// <param name="topic_permission_list">话题权限列表 (Topic permission list)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult CommunityAddTopicPermissionToPermissionGroup(string group_id, string permission_group_id, List<TopicPermission> topic_permission_list, ValueCallback<List<GroupTopicOperationResult>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<GroupTopicOperationResult>>);

      string json_topic_permission_array = Utils.ToJson(topic_permission_list);
      int res = IMNativeSDK.TIMCommunityAddTopicPermissionToPermissionGroup(Utils.string2intptr(group_id), Utils.string2intptr(permission_group_id), Utils.string2intptr(json_topic_permission_array), ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    public static TIMResult CommunityAddTopicPermissionToPermissionGroup(string group_id, string permission_group_id, List<TopicPermission> topic_permission_list, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      string json_topic_permission_array = Utils.ToJson(topic_permission_list);
      int res = IMNativeSDK.TIMCommunityAddTopicPermissionToPermissionGroup(Utils.string2intptr(group_id), Utils.string2intptr(permission_group_id), Utils.string2intptr(json_topic_permission_array), StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    /// <summary>
    /// 从权限组中删除话题权限
    /// Delete topic permission from permission group
    /// </summary>
    /// <param name="group_id">群 ID (Group ID)</param>
    /// <param name="permission_group_id">权限组 ID (Permission group ID)</param>
    /// <param name="topic_id_list">话题 ID 列表 (Topic ID list)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult CommunityDeleteTopicPermissionFromPermissionGroup(string group_id, string permission_group_id, List<string> topic_id_list, ValueCallback<List<GroupTopicOperationResult>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<GroupTopicOperationResult>>);

      string json_topic_id_array = Utils.ToJson(topic_id_list);
      int res = IMNativeSDK.TIMCommunityDeleteTopicPermissionFromPermissionGroup(Utils.string2intptr(group_id), Utils.string2intptr(permission_group_id), Utils.string2intptr(json_topic_id_array), ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    public static TIMResult CommunityDeleteTopicPermissionFromPermissionGroup(string group_id, string permission_group_id, List<string> topic_id_list, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      string json_topic_id_array = Utils.ToJson(topic_id_list);
      int res = IMNativeSDK.TIMCommunityDeleteTopicPermissionFromPermissionGroup(Utils.string2intptr(group_id), Utils.string2intptr(permission_group_id), Utils.string2intptr(json_topic_id_array), StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    /// <summary>
    /// 修改权限组中的话题权限
    /// Modify topic permission in permission group
    /// </summary>
    /// <param name="group_id">群 ID (Group ID)</param>
    /// <param name="permission_group_id">权限组 ID (Permission group ID)</param>
    /// <param name="topic_permission_list">话题权限列表 (Topic permission list)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult CommunityModifyTopicPermissionInPermissionGroup(string group_id, string permission_group_id, List<TopicPermission> topic_permission_list, ValueCallback<List<GroupTopicOperationResult>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<GroupTopicOperationResult>>);

      string json_topic_permission_array = Utils.ToJson(topic_permission_list); 
      int res = IMNativeSDK.TIMCommunityModifyTopicPermissionInPermissionGroup(Utils.string2intptr(group_id), Utils.string2intptr(permission_group_id), Utils.string2intptr(json_topic_permission_array), ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    public static TIMResult CommunityModifyTopicPermissionInPermissionGroup(string group_id, string permission_group_id, List<TopicPermission> topic_permission_list, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      string json_topic_permission_array = Utils.ToJson(topic_permission_list);
      int res = IMNativeSDK.TIMCommunityModifyTopicPermissionInPermissionGroup(Utils.string2intptr(group_id), Utils.string2intptr(permission_group_id), Utils.string2intptr(json_topic_permission_array), StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    /// <summary>
    /// 获取权限组中的话题权限
    /// Get topic permission in permission group
    /// </summary>
    /// <param name="group_id">群 ID (Group ID)</param>
    /// <param name="permission_group_id">权限组 ID (Permission group ID)</param>
    /// <param name="topic_id_list">话题 ID 列表 (Topic ID list)</param>
    /// <param name="callback">异步回调 (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult CommunityGetTopicPermissionInPermissionGroup(string group_id, string permission_group_id, List<string> topic_id_list, ValueCallback<List<TopicPermissionResult>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<TopicPermissionResult>>);

      string json_topic_id_array = Utils.ToJson(topic_id_list);
      int res = IMNativeSDK.TIMCommunityGetTopicPermissionInPermissionGroup(Utils.string2intptr(group_id), Utils.string2intptr(permission_group_id), Utils.string2intptr(json_topic_id_array), ValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }

    public static TIMResult CommunityGetTopicPermissionInPermissionGroup(string group_id, string permission_group_id, List<string> topic_id_list, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      string json_topic_id_array = Utils.ToJson(topic_id_list);
      int res = IMNativeSDK.TIMCommunityGetTopicPermissionInPermissionGroup(Utils.string2intptr(group_id), Utils.string2intptr(permission_group_id), Utils.string2intptr(json_topic_id_array), StringValueCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, fn_name);
      return (TIMResult)res;
    }



    /// <summary>
    /// 注册收到新消息回调
    /// Add receiving new message callback
    /// <para>如果用户是登陆状态，ImSDK收到新消息会通过此接口设置的回调抛出，另外需要注意，抛出的消息不一定是未读的消息 (If logged in, ImSDK will retrieve messages send to you. PS. It doesn't have to be the unread messages)</para>
    /// <para>只是本地曾经没有过的消息（例如在另外一个终端已读，拉取最近联系人消息时可以获取会话最后一条消息，如果本地没有，会通过此方法抛出）(Only messages that aren't stored in the local storage will be retrieved. (Eg. Message read from other platform, and retrieve latest conversation's last message, if it's not in the local storage, the message will appear in here))</para>
    /// <para>在用户登陆之后，ImSDK会拉取离线消息，为了不漏掉消息通知，需要在登陆之前注册新消息通知 (Once logged in, ImSDK will retrieve offline messages. Register this callback before log in to prevent missing messages)</para>
    /// </summary>
    /// <param name="callback">回调 RecvNewMsgCallback</param>
    public static void AddRecvNewMsgCallback(RecvNewMsgCallback callback = null, RecvNewMsgStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        RecvNewMsgCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        RecvNewMsgStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveRecvNewMsgCallback();
        return;
      }

      IMNativeSDK.TIMAddRecvNewMsgCallback(TIMRecvNewMsgCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除收到新消息回调
    /// Remove receiving new message callback
    /// </summary>
    /// <param name="callback">回调 RecvNewMsgCallback</param>
    /// <param name="stringCallback">回调 RecvNewMsgStringCallback</param>
    public static void RemoveRecvNewMsgCallback(RecvNewMsgCallback callback = null, RecvNewMsgStringCallback stringCallback = null)
    {
      if (callback != null && RecvNewMsgCallbackStore != null)
      {
        RecvNewMsgCallbackStore -= callback;
      }

      if (stringCallback != null && RecvNewMsgStringCallbackStore != null)
      {
        RecvNewMsgStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RecvNewMsgCallbackStore = null;
        RecvNewMsgStringCallbackStore = null;
      }

      if (RecvNewMsgCallbackStore == null && RecvNewMsgStringCallbackStore == null)
      {
        IMNativeSDK.TIMRemoveRecvNewMsgCallback(TIMRecvNewMsgCallbackInstance);
      }
    }

    public static void SetMsgReactionsChangedCallback(MsgReactionsChangedCallback callback = null, MsgReactionsChangedStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        MsgReactionsChangedCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        MsgReactionsChangedStringCallbackStore += stringCallback;
      }
      if (callback == null && stringCallback == null)
      {
        RemoveMsgReactionsChangedCallback();
        return;
      }
      IMNativeSDK.TIMSetMsgReactionsChangedCallback(TIMMsgReactionsChangedCallbackInstance, Utils.string2intptr(user_data));
    }

    public static void RemoveMsgReactionsChangedCallback(MsgReactionsChangedCallback callback = null, MsgReactionsChangedStringCallback stringCallback = null)
    {
      if (callback != null && MsgReactionsChangedCallbackStore != null)
      {
        MsgReactionsChangedCallbackStore -= callback;
      }

      if (stringCallback != null && MsgReactionsChangedStringCallbackStore != null)
      {
        MsgReactionsChangedStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        MsgReactionsChangedCallbackStore = null;
        MsgReactionsChangedStringCallbackStore = null;
      }

      if (MsgReactionsChangedCallbackStore == null && MsgReactionsChangedStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetMsgReactionsChangedCallback(null, Utils.string2intptr(""));
      }
    }

    public static void SetMsgAllMessageReceiveOptionCallback(MsgAllMessageReceiveOptionCallback callback = null, MsgAllMessageReceiveOptionStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        MsgAllMessageReceiveOptionCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        MsgAllMessageReceiveOptionStringCallbackStore += stringCallback;
      }
      if (callback == null && stringCallback == null)
      {
        RemoveMsgAllMessageReceiveOptionCallback();
        return;
      }
      IMNativeSDK.TIMSetMsgAllMessageReceiveOptionCallback(TIMMsgAllMessageReceiveOptionCallbackInstance, Utils.string2intptr(user_data));
    }

    public static void RemoveMsgAllMessageReceiveOptionCallback(MsgAllMessageReceiveOptionCallback callback = null, MsgAllMessageReceiveOptionStringCallback stringCallback = null)
    {
      if (callback != null && MsgAllMessageReceiveOptionCallbackStore != null)
      {
        MsgAllMessageReceiveOptionCallbackStore -= callback;
      }

      if (stringCallback != null && MsgAllMessageReceiveOptionStringCallbackStore != null)
      {
        MsgAllMessageReceiveOptionStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        MsgAllMessageReceiveOptionCallbackStore = null;
        MsgAllMessageReceiveOptionStringCallbackStore = null;
      }

      if (MsgAllMessageReceiveOptionCallbackStore == null && MsgAllMessageReceiveOptionStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetMsgAllMessageReceiveOptionCallback(null, Utils.string2intptr(""));
      }
    }
    /// <summary>
    /// 设置消息已读回执回调
    /// Set message read receipt callback
    /// <para>发送方发送消息，接收方调用接口[TIMMsgReportReaded]()上报该消息已读，发送方ImSDK会通过此接口设置的回调抛出 (Sender sends messages, and receiver report read messages by TIMMsgReportReaded, and the sender will be notified via this callback)</para>
    /// </summary>
    /// <param name="callback">回调 MsgReadedReceiptCallback</param>
    /// <param name="stringCallback">回调 MsgReadedReceiptStringCallback</param>
    public static void SetMsgReadedReceiptCallback(MsgReadedReceiptCallback callback = null, MsgReadedReceiptStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        MsgReadedReceiptCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        MsgReadedReceiptStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveMsgReadedReceiptCallback();
        return;
      }

      IMNativeSDK.TIMSetMsgReadedReceiptCallback(TIMMsgReadedReceiptCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除消息已读回执回调
    /// Remove message read receipt callback
    /// </summary>
    /// <param name="callback">回调 MsgReadedReceiptCallback</param>
    /// <param name="stringCallback">回调 MsgReadedReceiptStringCallback</param>
    public static void RemoveMsgReadedReceiptCallback(MsgReadedReceiptCallback callback = null, MsgReadedReceiptStringCallback stringCallback = null)
    {
      if (callback != null && MsgReadedReceiptCallbackStore != null)
      {
        MsgReadedReceiptCallbackStore -= callback;
      }

      if (stringCallback != null && MsgReadedReceiptStringCallbackStore != null)
      {
        MsgReadedReceiptStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        MsgReadedReceiptCallbackStore = null;
        MsgReadedReceiptStringCallbackStore = null;
      }

      if (MsgReadedReceiptCallbackStore == null && MsgReadedReceiptStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetMsgReadedReceiptCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置接收的消息被撤回回调
    /// Set message revoking callback
    /// <para>发送方发送消息，接收方收到消息。此时发送方调用接口[TIMMsgRevoke]()撤回该消息，接收方的ImSDK会通过此接口设置的回调抛出 (Sender sends messages, and revoke the messages by TIMMsgRevoke, and the receiver will be notified via this callback)</para>
    /// </summary>
    /// <param name="callback">回调 MsgRevokeCallback</param>
    /// <param name="stringCallback">回调 MsgRevokeStringCallback</param>
    public static void SetMsgRevokeCallback(MsgRevokeCallback callback = null, MsgRevokeStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        MsgRevokeCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        MsgRevokeStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveMsgRevokeCallback();
        return;
      }

      IMNativeSDK.TIMSetMsgRevokeCallback(TIMMsgRevokeCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除接收的消息被撤回回调
    /// Remove message revoking callback
    /// </summary>
    /// <param name="callback">回调 MsgRevokeCallback</param>
    /// <param name="stringCallback">回调 MsgRevokeStringCallback</param>
    public static void RemoveMsgRevokeCallback(MsgRevokeCallback callback = null, MsgRevokeStringCallback stringCallback = null)
    {
      if (callback != null && MsgRevokeCallbackStore != null)
      {
        MsgRevokeCallbackStore -= callback;
      }

      if (stringCallback != null && MsgRevokeStringCallbackStore != null)
      {
        MsgRevokeStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        MsgRevokeCallbackStore = null;
        MsgRevokeStringCallbackStore = null;
      }

      if (MsgRevokeCallbackStore == null && MsgRevokeStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetMsgRevokeCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置消息内元素相关文件上传进度回调
    /// Set message element uploading progress callback
    /// <para>设置消息元素上传进度回调。当消息内包含图片、声音、文件、视频元素时，ImSDK会上传这些文件，并触发此接口设置的回调，用户可以根据回调感知上传的进度 (Set message element uploading progress callback. When message element contains image, audio, file and video, the progress is notified via this callback)</para>
    /// </summary>
    /// <param name="callback">回调 MsgElemUploadProgressCallback</param>
    /// <param name="stringCallback">回调 MsgElemUploadProgressStringCallback</param>
    public static void SetMsgElemUploadProgressCallback(MsgElemUploadProgressCallback callback = null, MsgElemUploadProgressStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        MsgElemUploadProgressCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        MsgElemUploadProgressStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveMsgElemUploadProgressCallback();
        return;
      }

      IMNativeSDK.TIMSetMsgElemUploadProgressCallback(TIMMsgElemUploadProgressCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除消息内元素相关文件上传进度回调
    /// Remove message element uploading progress callback
    /// </summary>
    /// <param name="callback">回调 MsgElemUploadProgressCallback</param>
    /// <param name="stringCallback">回调 MsgElemUploadProgressStringCallback</param>
    public static void RemoveMsgElemUploadProgressCallback(MsgElemUploadProgressCallback callback = null, MsgElemUploadProgressStringCallback stringCallback = null)
    {
      if (callback != null && MsgElemUploadProgressCallbackStore != null)
      {
        MsgElemUploadProgressCallbackStore -= callback;
      }

      if (stringCallback != null && MsgElemUploadProgressStringCallbackStore != null)
      {
        MsgElemUploadProgressStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        MsgElemUploadProgressCallbackStore = null;
        MsgElemUploadProgressStringCallbackStore = null;
      }

      if (MsgElemUploadProgressCallbackStore == null && MsgElemUploadProgressStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetMsgElemUploadProgressCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置群组系统消息回调
    /// Set group tips event callback
    /// <para>群组系统消息事件包括 加入群、退出群、踢出群、设置管理员、取消管理员、群资料变更、群成员资料变更。此消息是针对所有群组成员下发的 (Group tips event includes: join group, quit group, remove from the group, grant admin, devolve admin, modify group info, modify group member info. This callback is for all the group members)</para>
    /// </summary>
    /// <param name="callback">回调 GroupTipsEventCallback</param>
    /// <param name="stringCallback">回调 GroupTipsEventStringCallback</param>
    public static void SetGroupTipsEventCallback(GroupTipsEventCallback callback = null, GroupTipsEventStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        GroupTipsEventCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        GroupTipsEventStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveGroupTipsEventCallback();
        return;
      }

      IMNativeSDK.TIMSetGroupTipsEventCallback(TIMGroupTipsEventCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除群组系统消息回调
    /// Remove group tips event callback
    /// </summary>
    /// <param name="callback">回调 GroupTipsEventCallback</param>
    /// <param name="stringCallback">回调 GroupTipsEventStringCallback</param>
    public static void RemoveGroupTipsEventCallback(GroupTipsEventCallback callback = null, GroupTipsEventStringCallback stringCallback = null)
    {
      if (callback != null && GroupTipsEventCallbackStore != null)
      {
        GroupTipsEventCallbackStore -= callback;
      }

      if (stringCallback != null && GroupTipsEventStringCallbackStore != null)
      {
        GroupTipsEventStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        GroupTipsEventCallbackStore = null;
        GroupTipsEventStringCallbackStore = null;
      }

      if (GroupTipsEventCallbackStore == null && GroupTipsEventStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetGroupTipsEventCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置群组属性变更回调
    /// Set group attribute changed callback
    /// <para>某个已加入的群的属性被修改了，会返回所在群组的所有属性（该群所有的成员都能收到）(If some attributes from a joined group are modified, all members from that group will receive the all the modified attributes)</para>
    /// </summary>
    /// <param name="callback">回调 GroupAttributeChangedCallback</param>
    /// <param name="stringCallback">回调 GroupAttributeChangedStringCallback</param>
    public static void SetGroupAttributeChangedCallback(GroupAttributeChangedCallback callback = null, GroupAttributeChangedStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        GroupAttributeChangedCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        GroupAttributeChangedStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveGroupAttributeChangedCallback();
        return;
      }

      IMNativeSDK.TIMSetGroupAttributeChangedCallback(TIMGroupAttributeChangedCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 设置群计数器变更回调
    /// Set group counter changed callback
    /// </summary>
    /// <param name="callback">回调 GroupCounterChangedCallback</param>
    public static void SetGroupCounterChangedCallback(GroupCounterChangedCallback callback)
    {
      if (callback != null)
      {
        string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

        string user_data = fn_name + "_" + Utils.getRandomStr();

        GroupCounterChangedCallbackStore += callback;

        IMNativeSDK.TIMSetGroupCounterChangedCallback(TIMGroupCounterChangedCallbackInstance, Utils.string2intptr(user_data));
      }
      else
      {
        RemoveGroupCounterChangedCallback();
      }
    }

    /// <summary>
    /// 移除群计数器变更回调
    /// Remove group counter changed callback
    /// </summary>
    /// <param name="callback">回调 GroupCounterChangedCallback</param>
    public static void RemoveGroupCounterChangedCallback(GroupCounterChangedCallback callback = null)
    {
      if (callback != null && GroupCounterChangedCallbackStore != null)
      {
        GroupCounterChangedCallbackStore -= callback;
      }
      else
      {
        GroupCounterChangedCallbackStore = null;
      }

      if (GroupCounterChangedCallbackStore == null)
      {
        IMNativeSDK.TIMSetGroupCounterChangedCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 移除群组属性变更回调
    /// Remove group attribute changed callback
    /// </summary>
    /// <param name="callback">回调 GroupAttributeChangedCallback</param>
    /// <param name="stringCallback">回调 GroupAttributeChangedStringCallback</param>
    public static void RemoveGroupAttributeChangedCallback(GroupAttributeChangedCallback callback = null, GroupAttributeChangedStringCallback stringCallback = null)
    {
      if (callback != null && GroupAttributeChangedCallbackStore != null)
      {
        GroupAttributeChangedCallbackStore -= callback;
      }

      if (stringCallback != null && GroupAttributeChangedStringCallbackStore != null)
      {
        GroupAttributeChangedStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        GroupAttributeChangedCallbackStore = null;
        GroupAttributeChangedStringCallbackStore = null;
      }

      if (GroupAttributeChangedCallbackStore == null && GroupAttributeChangedStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetGroupAttributeChangedCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置会话事件回调
    /// Set conversation event callback
    /// <para>会话事件包括： (Includes: )</para>
    /// <para>会话新增 (New conversation)</para>
    /// <para>会话删除 (Delete conversation)</para>
    /// <para>会话更新 (Update conversation)</para>
    /// <para>会话开始 (Start conversation)</para>
    /// <para>会话结束 (End conversation)</para>
    /// <para>任何产生一个新会话的操作都会触发会话新增事件，例如调用接口[TIMConvCreate]()创建会话，接收到未知会话的第一条消息等 (Every new conversation event will trigger new conversation event, eg. create conversation via TIMConvCreate, or receive the first message from unknown conversation and etc.)</para>
    /// <para>任何已有会话变化的操作都会触发会话更新事件，例如收到会话新消息，消息撤回，已读上报等 (Every event on the exist conversation will cause update conversatin event, eg. receiving new message from conversation, message revoked, message read reported)</para>
    /// <para>调用接口[TIMConvDelete]()删除会话成功时会触发会话删除事件 (Use TIMConvDelete to delete conversation will trigger delete conversation event)</para>
    /// </summary>
    /// <param name="callback">回调 ConvEventCallback</param>
    /// <param name="stringCallback">回调 ConvEventStringCallback</param>
    public static void SetConvEventCallback(ConvEventCallback callback = null, ConvEventStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        ConvEventCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        ConvEventStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveConvEventCallback();
        return;
      }

      IMNativeSDK.TIMSetConvEventCallback(TIMConvEventCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除会话事件回调
    /// Remove conversation event callback
    /// </summary>
    /// <param name="callback">回调 ConvEventCallback</param>
    /// <param name="stringCallback">回调 ConvEventStringCallback</param>
    public static void RemoveConvEventCallback(ConvEventCallback callback = null, ConvEventStringCallback stringCallback = null)
    {
      if (callback != null && ConvEventCallbackStore != null)
      {
        ConvEventCallbackStore -= callback;
      }

      if (stringCallback != null && ConvEventStringCallbackStore != null)
      {
        ConvEventStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        ConvEventCallbackStore = null;
        ConvEventStringCallbackStore = null;
      }

      if (ConvEventCallbackStore == null && ConvEventStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetConvEventCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置会话未读消息总数变更的回调
    /// Set conversation total unread message count changed callback
    /// </summary>
    /// <param name="callback">回调 ConvTotalUnreadMessageCountChangedCallback</param>
    public static void SetConvTotalUnreadMessageCountChangedCallback(ConvTotalUnreadMessageCountChangedCallback callback)
    {
      if (callback != null)
      {
        string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

        string user_data = fn_name + "_" + Utils.getRandomStr();

        ConvTotalUnreadMessageCountChangedCallbackStore += callback;

        IMNativeSDK.TIMSetConvTotalUnreadMessageCountChangedCallback(TIMConvTotalUnreadMessageCountChangedCallbackInstance, Utils.string2intptr(user_data));
      }
      else
      {
        RemoveConvTotalUnreadMessageCountChangedCallback();
      }
    }

    /// <summary>
    /// 移除会话未读消息总数变更的回调
    /// Remove conversation total unread message count changed callback
    /// </summary>
    /// <param name="callback">回调 ConvTotalUnreadMessageCountChangedCallback</param>
    public static void RemoveConvTotalUnreadMessageCountChangedCallback(ConvTotalUnreadMessageCountChangedCallback callback = null)
    {
      if (callback != null && ConvTotalUnreadMessageCountChangedCallbackStore != null)
      {
        ConvTotalUnreadMessageCountChangedCallbackStore -= callback;
      }
      else
      {
        ConvTotalUnreadMessageCountChangedCallbackStore = null;
      }

      if (ConvTotalUnreadMessageCountChangedCallbackStore == null )
      {
        IMNativeSDK.TIMSetConvTotalUnreadMessageCountChangedCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置按会话 filter 过滤的未读消息总数变更的回调
    /// Set filtered conversation unread message count changed callback
    /// </summary>
    /// <param name="callback">回调 ConvTotalUnreadMessageCountChangedByFilterCallback</param>
    /// <param name="stringCallback">回调 ConvTotalUnreadMessageCountChangedByFilterStringCallback</param>
    public static void SetConvUnreadMessageCountChangedByFilterCallback(ConvTotalUnreadMessageCountChangedByFilterCallback callback = null, ConvTotalUnreadMessageCountChangedByFilterStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        ConvUnreadMessageCountChangedByFilterCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        ConvUnreadMessageCountChangedByFilterStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveConvUnreadMessageCountChangedByFilterCallback();
        return;
      }

      IMNativeSDK.TIMSetConvUnreadMessageCountChangedByFilterCallback(TIMConvUnreadMessageCountChangedByFilterCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除按会话 filter 过滤的未读消息总数变更的回调
    /// Remove filtered conversation unread message count changed callback
    /// </summary>
    /// <param name="callback">回调 ConvTotalUnreadMessageCountChangedByFilterCallback</param>
    /// <param name="stringCallback">回调 ConvTotalUnreadMessageCountChangedByFilterStringCallback</param>
    public static void RemoveConvUnreadMessageCountChangedByFilterCallback(ConvTotalUnreadMessageCountChangedByFilterCallback callback = null, ConvTotalUnreadMessageCountChangedByFilterStringCallback stringCallback = null)
    {
      if (callback != null && ConvUnreadMessageCountChangedByFilterCallbackStore != null)
      {
        ConvUnreadMessageCountChangedByFilterCallbackStore -= callback;
      }

      if (stringCallback != null && ConvUnreadMessageCountChangedByFilterStringCallbackStore != null)
      {
        ConvUnreadMessageCountChangedByFilterStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        ConvUnreadMessageCountChangedByFilterCallbackStore = null;
        ConvUnreadMessageCountChangedByFilterStringCallbackStore = null;
      }

      if (ConvUnreadMessageCountChangedByFilterCallbackStore == null && ConvUnreadMessageCountChangedByFilterStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetConvUnreadMessageCountChangedByFilterCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置会话分组被创建回调
    /// Set conversation been created callback
    /// </summary>
    /// <param name="callback">回调 ConvConversationGroupCreatedCallback</param>
    /// <param name="stringCallback">回调 ConvConversationGroupCreatedStringCallback</param>
    public static void SetConvConversationGroupCreatedCallback(ConvConversationGroupCreatedCallback callback = null, ConvConversationGroupCreatedStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        ConvConversationGroupCreatedCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        ConvConversationGroupCreatedStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveConvConversationGroupCreatedCallback();
        return;
      }

      IMNativeSDK.TIMSetConvConversationGroupCreatedCallback(TIMConvConversationGroupCreatedCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除会话分组被创建回调
    /// Remove conversation been created callback
    /// </summary>
    /// <param name="callback">回调 ConvConversationGroupCreatedCallback</param>
    /// <param name="stringCallback">回调 ConvConversationGroupCreatedStringCallback</param>
    public static void RemoveConvConversationGroupCreatedCallback(ConvConversationGroupCreatedCallback callback = null, ConvConversationGroupCreatedStringCallback stringCallback = null)
    {
      if (callback != null && ConvConversationGroupCreatedCallbackStore != null)
      {
        ConvConversationGroupCreatedCallbackStore -= callback;
      }

      if (stringCallback != null && ConvConversationGroupCreatedStringCallbackStore != null)
      {
        ConvConversationGroupCreatedStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        ConvConversationGroupCreatedCallbackStore = null;
        ConvConversationGroupCreatedStringCallbackStore = null;
      }

      if (ConvConversationGroupCreatedCallbackStore == null && ConvConversationGroupCreatedStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetConvConversationGroupCreatedCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置会话分组被删除的回调
    /// Set conversation group deleted callback
    /// </summary>
    /// <param name="callback">回调 ConvConversationGroupDeletedCallback</param>
    public static void SetConvConversationGroupDeletedCallback(ConvConversationGroupDeletedCallback callback)
    {
      if (callback != null)
      {
        string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

        string user_data = fn_name + "_" + Utils.getRandomStr();

        ConvConversationGroupDeletedCallbackStore += callback;

        IMNativeSDK.TIMSetConvConversationGroupDeletedCallback(TIMConvConversationGroupDeletedCallbackInstance, Utils.string2intptr(user_data));
      }
      else
      {
        RemoveConvConversationGroupDeletedCallback();
      }
    }

    /// <summary>
    /// 设置会话分组名变更回调
    /// Set conversation group name changed callback
    /// </summary>
    /// <param name="callback">回调 ConvConversationGroupNameChangedCallback</param>
    public static void SetConvConversationGroupNameChangedCallback(ConvConversationGroupNameChangedCallback callback)
    {
      if (callback != null)
      {
        string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

        string user_data = fn_name + "_" + Utils.getRandomStr();

        ConvConversationGroupNameChangedCallbackStore += callback;

        IMNativeSDK.TIMSetConvConversationGroupNameChangedCallback(TIMConvConversationGroupNameChangedCallbackInstance, Utils.string2intptr(user_data));
      }
      else
      {
        RemoveConvConversationGroupNameChangedCallback();
      }
    }

    /// <summary>
    /// 移除会话分组名变更回调
    /// Remove conversation group name changed callback
    /// </summary>
    /// <param name="callback">回调 ConvConversationGroupNameChangedCallback</param>
    public static void RemoveConvConversationGroupNameChangedCallback(ConvConversationGroupNameChangedCallback callback = null)
    {
      if (callback != null && ConvConversationGroupNameChangedCallbackStore != null)
      {
        ConvConversationGroupNameChangedCallbackStore -= callback;
      }
      else
      {
        ConvConversationGroupNameChangedCallbackStore = null;
      }

      if (ConvConversationGroupNameChangedCallbackStore == null)
      {
        IMNativeSDK.TIMSetConvConversationGroupNameChangedCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 移除会话分组被删除的回调
    /// Remove conversation group deleted callback
    /// </summary>
    /// <param name="callback">回调 ConvConversationGroupDeletedCallback</param>
    public static void RemoveConvConversationGroupDeletedCallback(ConvConversationGroupDeletedCallback callback = null)
    {
      if (callback != null && ConvConversationGroupDeletedCallbackStore != null)
      {
        ConvConversationGroupDeletedCallbackStore -= callback;
      }
      else
      {
        ConvConversationGroupDeletedCallbackStore = null;
      }

      if (ConvConversationGroupDeletedCallbackStore == null)
      {
        IMNativeSDK.TIMSetConvConversationGroupDeletedCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置会话分组新增会话回调
    /// Set conversation added to group callback
    /// </summary>
    /// <param name="callback">回调 ConvConversationsAddedToGroupCallback</param>
    /// <param name="stringCallback">回调 ConvConversationsAddedToGroupStringCallback</param>
    public static void SetConvConversationsAddedToGroupCallback(ConvConversationsAddedToGroupCallback callback = null, ConvConversationsAddedToGroupStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        ConvConversationsAddedToGroupCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        ConvConversationsAddedToGroupStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveConvConversationsAddedToGroupCallback();
        return;
      }

      IMNativeSDK.TIMSetConvConversationsAddedToGroupCallback(TIMConvConversationsAddedToGroupCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除会话分组新增会话回调
    /// Remove conversation added to group callback
    /// </summary>
    /// <param name="callback">回调 ConvConversationsAddedToGroupCallback</param>
    /// <param name="stringCallback">回调 ConvConversationsAddedToGroupStringCallback</param>
    public static void RemoveConvConversationsAddedToGroupCallback(ConvConversationsAddedToGroupCallback callback = null, ConvConversationsAddedToGroupStringCallback stringCallback = null)
    {
      if (callback != null && ConvConversationsAddedToGroupCallbackStore != null)
      {
        ConvConversationsAddedToGroupCallbackStore -= callback;
      }

      if (stringCallback != null && ConvConversationsAddedToGroupStringCallbackStore != null)
      {
        ConvConversationsAddedToGroupStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        ConvConversationsAddedToGroupCallbackStore = null;
        ConvConversationsAddedToGroupStringCallbackStore = null;
      }

      if (ConvConversationsAddedToGroupCallbackStore == null && ConvConversationsAddedToGroupStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetConvConversationsAddedToGroupCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置会话分组删除会话回调
    /// Set conversation been deleted from group callback
    /// </summary>
    /// <param name="callback">回调 ConvConversationsDeletedFromGroupCallback</param>
    /// <param name="stringCallback">回调 ConvConversationsDeletedFromGroupStringCallback</param>
    public static void SetConvConversationsDeletedFromGroupCallback(ConvConversationsDeletedFromGroupCallback callback = null, ConvConversationsDeletedFromGroupStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        ConvConversationsDeletedFromGroupCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        ConvConversationsDeletedFromGroupStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveConvConversationsDeletedFromGroupCallback();
        return;
      }

      IMNativeSDK.TIMSetConvConversationsDeletedFromGroupCallback(TIMConvConversationsDeletedFromGroupCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除会话分组删除会话回调
    /// Remove conversation been deleted from group callback
    /// </summary>
    /// <param name="callback">回调 ConvConversationsDeletedFromGroupCallback</param>
    /// <param name="stringCallback">回调 ConvConversationsDeletedFromGroupStringCallback</param>
    public static void RemoveConvConversationsDeletedFromGroupCallback(ConvConversationsDeletedFromGroupCallback callback = null, ConvConversationsDeletedFromGroupStringCallback stringCallback = null)
    {
      if (callback != null && ConvConversationsDeletedFromGroupCallbackStore != null)
      {
        ConvConversationsDeletedFromGroupCallbackStore -= callback;
      }

      if (stringCallback != null && ConvConversationsDeletedFromGroupStringCallbackStore != null)
      {
        ConvConversationsDeletedFromGroupStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        ConvConversationsDeletedFromGroupCallbackStore = null;
        ConvConversationsDeletedFromGroupStringCallbackStore = null;
      }

      if (ConvConversationsDeletedFromGroupCallbackStore == null && ConvConversationsDeletedFromGroupStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetConvConversationsDeletedFromGroupCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置网络连接状态监听回调
    /// Set network status changed callback
    /// <para>当调用接口 Init() 时，ImSDK会去连接云后台。此接口设置的回调用于监听网络连接的状态 (When called Init(), ImSDK will connect the server and listen to network staus.)</para>
    /// <para>网络连接状态包含四个：正在连接、连接失败、连接成功、已连接。这里的网络事件不表示用户本地网络状态，仅指明ImSDK是否与即时通信IM云Server连接状态 (Network status contains four stages: Connecting, Failed, Connect Success, Connected. The network status is only responsible for the indication of the connection between ImSDK and the IM server.)</para>
    /// <para>可选设置，如果要用户感知是否已经连接服务器，需要设置此回调，用于通知调用者跟通讯后台链接的连接和断开事件，另外，如果断开网络，等网络恢复后会自动重连，自动拉取消息通知用户，用户无需关心网络状态，仅作通知之用 (This is optional callback, if you concern about network status changing, use this callback to inform the conncetion is connected or broke. Besides, you can set callback to retrieve mesasges once reconnected.)</para>
    /// <para>只要用户处于登陆状态，ImSDK内部会进行断网重连，用户无需关心 (Once logged in, ImSDK will attemp reconnect automatically)</para>
    /// </summary>
    /// <param name="callback">回调 NetworkStatusListenerCallback</param>
    public static void SetNetworkStatusListenerCallback(NetworkStatusListenerCallback callback)
    {
      if (callback != null)
      {
        string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

        string user_data = fn_name + "_" + Utils.getRandomStr();

        NetworkStatusListenerCallbackStore += callback;

        IMNativeSDK.TIMSetNetworkStatusListenerCallback(TIMNetworkStatusListenerCallbackInstance, Utils.string2intptr(user_data));
      }
      else
      {
        RemoveNetworkStatusListenerCallback();
      }
    }

    /// <summary>
    /// 移除网络连接状态监听回调
    /// Remove network status changed callback
    /// </summary>
    /// <param name="callback">回调 NetworkStatusListenerCallback</param>
    public static void RemoveNetworkStatusListenerCallback(NetworkStatusListenerCallback callback = null)
    {
      if (callback != null && NetworkStatusListenerCallbackStore != null)
      {
        NetworkStatusListenerCallbackStore -= callback;
      }
      else
      {
        NetworkStatusListenerCallbackStore = null;
      }

      if (NetworkStatusListenerCallbackStore == null)
      {
        IMNativeSDK.TIMSetNetworkStatusListenerCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置被踢下线通知回调
    /// Set user kicked offline callback
    ///  <para>用户如果在其他终端登陆，会被踢下线，这时会收到用户被踢下线的通知，出现这种情况常规的做法是提示用户进行操作（退出，或者再次把对方踢下线）(Once an account is logged on other platform, the previous login is nullified. And the previous platform will receive this callback. (You may force the previous one exit the APP or rekick))</para>
    ///  <para>用户如果在离线状态下被踢，下次登陆将会失败，可以给用户一个非常强的提醒（登陆错误码ERR_IMSDK_KICKED_BY_OTHERS：6208），开发者也可以选择忽略这次错误，再次登陆即可 (If kicked offline when offline, then the next login will fail and you may give an emphasized error (Error codeERR_IMSDK_KICKED_BY_OTHERS: 6208) or just ignore it)</para>
    ///  <para>用户在线情况下的互踢情况：(Online user inter-kick situation:)</para>
    ///  <para>用户在设备1登陆，保持在线状态下，该用户又在设备2登陆，这时用户会在设备1上强制下线，收到 KickedOfflineCallback 回调(Logged on Device1, then log on Device2, Device1 will get KickedOfflineCallback)</para>
    ///  <para>用户在设备1上收到回调后，提示用户，可继续调用login上线，强制设备2下线。这里是在线情况下互踢过程 (After receiving the callback, you may allow Device1 to login and force Device2 offline.)</para>
    ///  <para>用户离线状态互踢: (Offline user inter-kick situation:)</para>
    ///  <para>用户在设备1登陆，没有进行logout情况下进程退出。该用户在设备2登陆，此时由于用户不在线，无法感知此事件，(Logged on Device1, go offline without logged out. And you logged on Device2, no callbacks)</para>
    ///  <para>为了显式提醒用户，避免无感知的互踢，用户在设备1重新登陆时，会返回（ERR_IMSDK_KICKED_BY_OTHERS：6208）错误码，表明之前被踢，是否需要把对方踢下线(When Device1 relogged in, it will receive Error codeERR_IMSDK_KICKED_BY_OTHERS: 6208 to inform it's kicked by another one.)</para>
    ///  <para>如果需要，则再次调用login强制上线，设备2的登陆的实例将会收到 KickedOfflineCallback 回调 (If needed, Device1 login again, and Device2 will receive KickedOfflineCallback)</para>
    /// </summary>
    /// <param name="callback">回调 KickedOfflineCallback</param>
    public static void SetKickedOfflineCallback(KickedOfflineCallback callback)
    {
      if (callback != null)
      {
        string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

        string user_data = fn_name + "_" + Utils.getRandomStr();

        KickedOfflineCallbackStore += callback;

        IMNativeSDK.TIMSetKickedOfflineCallback(TIMKickedOfflineCallbackInstance, Utils.string2intptr(user_data));
      }
      else
      {
        RemoveKickedOfflineCallback();
      }
    }

    /// <summary>
    /// 移除被踢下线通知回调
    /// Remove user kicked offline callback
    /// </summary>
    /// <param name="callback">回调 KickedOfflineCallback</param>
    public static void RemoveKickedOfflineCallback(KickedOfflineCallback callback = null)
    {
      if (callback != null && KickedOfflineCallbackStore != null)
      {
        KickedOfflineCallbackStore -= callback;
      }
      else
      {
        KickedOfflineCallbackStore = null;
      }

      if (KickedOfflineCallbackStore == null)
      {
        IMNativeSDK.TIMSetKickedOfflineCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置票据过期回调
    /// Set user signature expired callback
    /// <para>用户票据，可能会存在过期的情况，如果用户票据过期，此接口设置的回调会调用 (User sig may expire and once it happens, this is invoked)</para>
    /// <para>Login()也将会返回70001错误码。开发者可根据错误码或者票据过期回调进行票据更换 (Login() may return Error Code 70001 to inform you to update the user signature)</para>
    /// </summary>
    /// <param name="callback">回调 UserSigExpiredCallback</param>
    public static void SetUserSigExpiredCallback(UserSigExpiredCallback callback)
    {
      if (callback != null)
      {
        string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

        string user_data = fn_name + "_" + Utils.getRandomStr();

        UserSigExpiredCallbackStore += callback;

        IMNativeSDK.TIMSetUserSigExpiredCallback(TIMUserSigExpiredCallbackInstance, Utils.string2intptr(user_data));
      }
      else
      {
        RemoveUserSigExpiredCallback();
      }
    }

    /// <summary>
    /// 移除票据过期回调
    /// Remove user signature expired callback
    /// </summary>
    /// <param name="callback">回调 UserSigExpiredCallback</param>
    public static void RemoveUserSigExpiredCallback(UserSigExpiredCallback callback = null)
    {
      if (callback != null && UserSigExpiredCallbackStore != null)
      {
        UserSigExpiredCallbackStore -= callback;
      }
      else
      {
        UserSigExpiredCallbackStore = null;
      }

      if (UserSigExpiredCallbackStore == null)
      {
        IMNativeSDK.TIMSetUserSigExpiredCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置添加好友的回调
    /// Set add friend listener
    /// <para>此回调为了多终端同步。例如A设备、B设备都登陆了同一帐号的ImSDK，A设备添加了好友，B设备ImSDK会收到添加好友的推送，ImSDK通过此回调告知开发者 (This is for multi-platform synchronization. Eg. A,B logged with the same account, and A adds a new friend, then B will receive this callback.)</para>
    /// </summary>
    /// <param name="callback">回调 OnAddFriendCallback</param>
    /// <param name="stringCallback">回调 OnAddFriendStringCallback</param>
    public static void SetOnAddFriendCallback(OnAddFriendCallback callback = null, OnAddFriendStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        OnAddFriendCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        OnAddFriendStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveOnAddFriendCallback();
        return;
      }

      IMNativeSDK.TIMSetOnAddFriendCallback(TIMOnAddFriendCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除添加好友的回调
    /// Remove add friend listener
    /// </summary>
    /// <param name="callback">回调 OnAddFriendCallback</param>
    /// <param name="stringCallback">回调 OnAddFriendStringCallback</param>
    public static void RemoveOnAddFriendCallback(OnAddFriendCallback callback = null, OnAddFriendStringCallback stringCallback = null)
    {
      if (callback != null && OnAddFriendCallbackStore != null)
      {
        OnAddFriendCallbackStore -= callback;
      }

      if (stringCallback != null && OnAddFriendStringCallbackStore != null)
      {
        OnAddFriendStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        OnAddFriendCallbackStore = null;
        OnAddFriendStringCallbackStore = null;
      }

      if (OnAddFriendCallbackStore == null && OnAddFriendStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetOnAddFriendCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置删除好友的回调
    /// Set delete friend listener
    /// <para>此回调为了多终端同步。例如A设备、B设备都登陆了同一帐号的ImSDK，A设备删除了好友，B设备ImSDK会收到删除好友的推送，ImSDK通过此回调告知开发者 (This is for multi-platform synchronization. Eg. A,B logged with the same account, and A deletes a friend, then B will receive this callback.)</para>
    /// </summary>
    /// <param name="callback">回调 OnDeleteFriendCallback</param>
    /// <param name="stringCallback">回调 OnDeleteFriendStringCallback</param>
    public static void SetOnDeleteFriendCallback(OnDeleteFriendCallback callback = null, OnDeleteFriendStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        OnDeleteFriendCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        OnDeleteFriendStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveOnDeleteFriendCallback();
        return;
      }

      SetOnDeleteFriendCallbackUser_Data = user_data;

      IMNativeSDK.TIMSetOnDeleteFriendCallback(TIMOnDeleteFriendCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除删除好友的回调
    /// Remove delete friend listener
    /// </summary>
    /// <param name="callback">回调 OnDeleteFriendCallback</param>
    /// <param name="stringCallback">回调 OnDeleteFriendStringCallback</param>
    public static void RemoveOnDeleteFriendCallback(OnDeleteFriendCallback callback = null, OnDeleteFriendStringCallback stringCallback = null)
    {
      if (callback != null && OnDeleteFriendCallbackStore != null)
      {
        OnDeleteFriendCallbackStore -= callback;
      }

      if (stringCallback != null && OnDeleteFriendStringCallbackStore != null)
      {
        OnDeleteFriendStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        OnDeleteFriendCallbackStore = null;
        OnDeleteFriendStringCallbackStore = null;
      }

      if (OnDeleteFriendCallbackStore == null && OnDeleteFriendStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetOnDeleteFriendCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置更新好友资料的回调
    /// Set update friend's profile callback
    /// <para>此回调为了多终端同步。例如A设备、B设备都登陆了同一帐号的ImSDK，A设备更新了好友资料，B设备ImSDK会收到更新好友资料的推送，ImSDK通过此回调告知开发者 (This is for multi-platform synchronization. Eg. A,B logged with the same account, and A updates a friend's profile, then B will receive this callback.)</para>
    /// </summary>
    /// <param name="callback">回调 UpdateFriendProfileCallback</param>
    /// <param name="stringCallback">回调 UpdateFriendProfileStringCallback</param>
    public static void SetUpdateFriendProfileCallback(UpdateFriendProfileCallback callback = null, UpdateFriendProfileStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        UpdateFriendProfileCallbackStore += callback;
      }

      if (callback != null)
      {
        UpdateFriendProfileStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveUpdateFriendProfileCallback();
        return;
      }

      IMNativeSDK.TIMSetUpdateFriendProfileCallback(TIMUpdateFriendProfileCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除更新好友资料的回调
    /// Remove update friend's profile callback
    /// </summary>
    /// <param name="callback">回调 UpdateFriendProfileCallback</param>
    /// <param name="stringCallback">回调 UpdateFriendProfileStringCallback</param>
    public static void RemoveUpdateFriendProfileCallback(UpdateFriendProfileCallback callback = null, UpdateFriendProfileStringCallback stringCallback = null)
    {
      if (callback != null && UpdateFriendProfileCallbackStore != null)
      {
        UpdateFriendProfileCallbackStore -= callback;
      }

      if (stringCallback != null && UpdateFriendProfileStringCallbackStore != null)
      {
        UpdateFriendProfileStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        UpdateFriendProfileCallbackStore = null;
        UpdateFriendProfileStringCallbackStore = null;
      }

      if (UpdateFriendProfileCallbackStore == null && UpdateFriendProfileStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetUpdateFriendProfileCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置好友添加请求的回调
    /// Set friend request callback
    /// <para>当前登入用户设置添加好友需要确认时，如果有用户请求加当前登入用户为好友，会收到好友添加请求的回调，ImSDK通过此回调告知开发者。如果多终端登入同一帐号，每个终端都会收到这个回调 (If current user's friend request needs confirmation, and other applies a friend request to the user, the user will receive thiss callback. If you logged on multi-platforms, each of them will trigger this callback)</para>
    /// </summary>
    /// <param name="callback">回调 FriendAddRequestCallback</param>
    /// <param name="stringCallback">回调 FriendAddRequestStringCallback</param>
    public static void SetFriendAddRequestCallback(FriendAddRequestCallback callback = null, FriendAddRequestStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        FriendAddRequestCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        FriendAddRequestStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveFriendAddRequestCallback();
        return;
      }

      IMNativeSDK.TIMSetFriendAddRequestCallback(TIMFriendAddRequestCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除好友添加请求的回调
    /// Remove friend request callback
    /// </summary>
    /// <param name="callback">回调 FriendAddRequestCallback</param>
    /// <param name="stringCallback">回调 FriendAddRequestStringCallback</param>
    public static void RemoveFriendAddRequestCallback(FriendAddRequestCallback callback = null, FriendAddRequestStringCallback stringCallback = null)
    {
      if (callback != null && FriendAddRequestCallbackStore != null)
      {
        FriendAddRequestCallbackStore -= callback;
      }

      if (stringCallback != null && FriendAddRequestStringCallbackStore != null)
      {
        FriendAddRequestStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        FriendAddRequestCallbackStore = null;
        FriendAddRequestStringCallbackStore = null;
      }

      if (FriendAddRequestCallbackStore == null && FriendAddRequestStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetFriendAddRequestCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置好友申请被删除的回调
    /// Set friend application list deleted callback
    /// <para>1. 主动删除好友申请 (1. Delete friend application)</para>
    /// <para>2. 拒绝好友申请 (2. Reject friend request application)</para>
    /// <para>3. 同意好友申请 (3. Grant friend request application)</para>
    /// <para>4. 申请加别人好友被拒绝 (4. Apply other friend request and get rejected)</para>
    /// </summary>
    /// <param name="callback">回调 FriendApplicationListDeletedCallback</param>
    /// <param name="stringCallback">回调 FriendApplicationListDeletedStringCallback</param>
    public static void SetFriendApplicationListDeletedCallback(FriendApplicationListDeletedCallback callback = null, FriendApplicationListDeletedStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        FriendApplicationListDeletedCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        FriendApplicationListDeletedStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveFriendApplicationListDeletedCallback();
        return;
      }

      IMNativeSDK.TIMSetFriendApplicationListDeletedCallback(TIMFriendApplicationListDeletedCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除好友申请被删除的回调
    /// Remove friend application list deleted callback
    /// </summary>
    /// <param name="callback">回调 FriendApplicationListDeletedCallback</param>
    /// <param name="stringCallback">回调 FriendApplicationListDeletedStringCallback</param>
    public static void RemoveFriendApplicationListDeletedCallback(FriendApplicationListDeletedCallback callback = null, FriendApplicationListDeletedStringCallback stringCallback = null)
    {
      if (callback != null && FriendApplicationListDeletedCallbackStore != null)
      {
        FriendApplicationListDeletedCallbackStore -= callback;
      }

      if (stringCallback != null && FriendApplicationListDeletedStringCallbackStore != null)
      {
        FriendApplicationListDeletedStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        FriendApplicationListDeletedCallbackStore = null;
        FriendApplicationListDeletedStringCallbackStore = null;
      }

      if (FriendApplicationListDeletedCallbackStore == null && FriendApplicationListDeletedStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetFriendApplicationListDeletedCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置好友申请已读的回调
    /// Set friend application list read callback
    /// <para>如果调用 setFriendApplicationRead 设置好友申请列表已读，会收到这个回调（主要用于多端同步）(Once use setFriendApplicationRead to set friend application read, this will be triggered)</para>
    /// </summary>
    /// <param name="callback">回调 FriendApplicationListReadCallback</param>
    public static void SetFriendApplicationListReadCallback(FriendApplicationListReadCallback callback)
    {
      if (callback != null)
      {
        string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

        string user_data = fn_name + "_" + Utils.getRandomStr();

        FriendApplicationListReadCallbackStore += callback;

        IMNativeSDK.TIMSetFriendApplicationListReadCallback(TIMFriendApplicationListReadCallbackInstance, Utils.string2intptr(user_data));
      }
      else
      {
        RemoveFriendApplicationListReadCallback();
      }
    }

    /// <summary>
    /// 移除好友申请已读的回调
    /// Remove friend application list read callback
    /// </summary>
    /// <param name="callback">回调 FriendApplicationListReadCallback</param>
    public static void RemoveFriendApplicationListReadCallback(FriendApplicationListReadCallback callback = null)
    {
      if (callback != null && FriendApplicationListReadCallbackStore != null)
      {
        FriendApplicationListReadCallbackStore -= callback;
      }
      else
      {
        FriendApplicationListReadCallbackStore = null;
      }

      if (FriendApplicationListReadCallbackStore == null)
      {
        IMNativeSDK.TIMSetFriendApplicationListReadCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置黑名单新增的回调
    /// Set blacklist added new user callback
    /// </summary>
    /// <param name="callback">回调 FriendBlackListAddedCallback</param>
    /// <param name="stringCallback">回调 FriendBlackListAddedStringCallback</param>
    public static void SetFriendBlackListAddedCallback(FriendBlackListAddedCallback callback = null, FriendBlackListAddedStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        FriendBlackListAddedCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        FriendBlackListAddedStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveFriendBlackListAddedCallback();
        return;
      }

      IMNativeSDK.TIMSetFriendBlackListAddedCallback(TIMFriendBlackListAddedCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除黑名单新增的回调
    /// Remove blacklist added new user callback
    /// </summary>
    /// <param name="callback">回调 FriendBlackListAddedCallback</param>
    /// <param name="stringCallback">回调 FriendBlackListAddedStringCallback</param>
    public static void RemoveFriendBlackListAddedCallback(FriendBlackListAddedCallback callback = null, FriendBlackListAddedStringCallback stringCallback = null)
    {
      if (callback != null && FriendBlackListAddedCallbackStore != null)
      {
        FriendBlackListAddedCallbackStore -= callback;
      }

      if (stringCallback != null && FriendBlackListAddedStringCallbackStore != null)
      {
        FriendBlackListAddedStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        FriendBlackListAddedCallbackStore = null;
        FriendBlackListAddedStringCallbackStore = null;
      }

      if (FriendBlackListAddedCallbackStore == null && FriendBlackListAddedStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetFriendBlackListAddedCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置黑名单删除的回调
    /// Set blacklist deleted user callback
    /// </summary>
    /// <param name="callback">回调 FriendBlackListDeletedCallback</param>
    /// <param name="stringCallback">回调 FriendBlackListDeletedStringCallback</param>
    public static void SetFriendBlackListDeletedCallback(FriendBlackListDeletedCallback callback = null, FriendBlackListDeletedStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        FriendBlackListDeletedCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        FriendBlackListDeletedStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveFriendBlackListDeletedCallback();
        return;
      }

      IMNativeSDK.TIMSetFriendBlackListDeletedCallback(TIMFriendBlackListDeletedCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除黑名单删除的回调
    /// Remove blacklist deleted user callback
    /// </summary>
    /// <param name="callback">回调 FriendBlackListDeletedCallback</param>
    /// <param name="stringCallback">回调 FriendBlackListDeletedStringCallback</param>
    public static void RemoveFriendBlackListDeletedCallback(FriendBlackListDeletedCallback callback = null, FriendBlackListDeletedStringCallback stringCallback = null)
    {
      if (callback != null && FriendBlackListDeletedCallbackStore != null)
      {
        FriendBlackListDeletedCallbackStore -= callback;
      }

      if (stringCallback != null && FriendBlackListDeletedStringCallbackStore != null)
      {
        FriendBlackListDeletedStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        FriendBlackListDeletedCallbackStore = null;
        FriendBlackListDeletedStringCallbackStore = null;
      }

      if (FriendBlackListDeletedCallbackStore == null && FriendBlackListDeletedStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetFriendBlackListDeletedCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置日志回调
    /// Set log callback
    /// <para>设置日志监听的回调之后，ImSDK内部的日志会回传到此接口设置的回调 (Once set, ImSDK's internal log will go through this callback)</para>
    /// <para>开发者可以通过接口SetConfig()配置哪些日志级别的日志回传到回调函数 (Developer calls SetConfig() to config log level)</para>
    /// </summary>
    /// <param name="callback">回调 LogCallback</param>
    public static void SetLogCallback(LogCallback callback)
    {
      if (callback != null)
      {
        string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

        string user_data = fn_name + "_" + Utils.getRandomStr();

        LogCallbackStore += callback;

        IMNativeSDK.TIMSetLogCallback(TIMLogCallbackInstance, Utils.string2intptr(user_data));
      }
      else
      {
        RemoveLogCallback();
      }
    }

    /// <summary>
    /// 移除日志回调
    /// Remove log callback
    /// </summary>
    /// <param name="callback">回调 LogCallback</param>
    public static void RemoveLogCallback(LogCallback callback = null)
    {
      if (callback != null && LogCallbackStore != null)
      {
        LogCallbackStore -= callback;
      }
      else
      {
        LogCallbackStore = null;
      }

      if (LogCallbackStore == null)
      {
        IMNativeSDK.TIMSetLogCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置消息在云端被修改后回传回来的消息更新通知回调
    /// Set message updated on the cloud callback
    /// <para> 当您发送的消息在服务端被修改后，ImSDK会通过该回调通知给您 (After you modify a message, ImSDK informs you by this callback)</para>
    /// <para> 您可以在您自己的服务器上拦截所有即时通信IM消息 [发单聊消息之前回调](https://cloud.tencent.com/document/product/269/1632) (You can intercept messages on your own server [Callback Before Sending a One-to-One Message](https://www.tencentcloud.com/document/product/1047/34364))</para>
    /// <para> 设置成功之后，即时通信IM服务器会将您的用户发送的每条消息都同步地通知给您的业务服务器 (If you intercept messages, the IM server will transmit all the message to your server)</para>
    /// <para> 您的业务服务器可以对该条消息进行修改（例如过滤敏感词），如果您的服务器对消息进行了修改，ImSDK就会通过此回调通知您 (Your server may modify the message (eg. content moderation), if your server modifies the message, ImSDK will notify you via this callback)</para>
    /// </summary>
    /// <param name="callback">回调 MsgUpdateCallback</param>
    /// <param name="stringCallback">回调 MsgUpdateStringCallback</param>
    public static void SetMsgUpdateCallback(MsgUpdateCallback callback = null, MsgUpdateStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        MsgUpdateCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        MsgUpdateStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveMsgUpdateCallback();
        return;
      }

      IMNativeSDK.TIMSetMsgUpdateCallback(TIMMsgUpdateCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除消息在云端被修改后回传回来的消息更新通知回调
    /// Remove message updated on the cloud callback
    /// </summary>
    /// <param name="callback">回调 MsgUpdateCallback</param>
    /// <param name="stringCallback">回调 MsgUpdateStringCallback</param>
    public static void RemoveMsgUpdateCallback(MsgUpdateCallback callback = null, MsgUpdateStringCallback stringCallback = null)
    {
      if (callback != null && MsgUpdateCallbackStore != null)
      {
        MsgUpdateCallbackStore -= callback;
      }

      if (stringCallback != null && MsgUpdateStringCallbackStore != null)
      {
        MsgUpdateStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        MsgUpdateCallbackStore = null;
        MsgUpdateStringCallbackStore = null;
      }

      if (MsgUpdateCallbackStore == null && MsgUpdateStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetMsgUpdateCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置消息扩展信息更新的回调
    /// Set message extensions updated callback
    /// </summary>
    /// <param name="callback">回调 MsgExtensionsChangedCallback</param>
    /// <param name="stringCallback">回调 MsgExtensionsChangedStringCallback</param>
    public static void SetMsgExtensionsChangedCallback(MsgExtensionsChangedCallback callback = null, MsgExtensionsChangedStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        MsgExtensionsChangedCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        MsgExtensionsChangedStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveMsgExtensionsChangedCallback();
        return;
      }

      IMNativeSDK.TIMSetMsgExtensionsChangedCallback(TIMMsgExtensionsChangedCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除消息扩展信息更新的回调
    /// Remove message extensions updated callback
    /// </summary>
    /// <param name="callback">回调 MsgExtensionsChangedCallback</param>
    /// <param name="stringCallback">回调 MsgExtensionsChangedStringCallback</param>
    public static void RemoveMsgExtensionsChangedCallback(MsgExtensionsChangedCallback callback = null, MsgExtensionsChangedStringCallback stringCallback = null)
    {
      if (callback != null && MsgExtensionsChangedCallbackStore != null)
      {
        MsgExtensionsChangedCallbackStore -= callback;
      }

      if (stringCallback != null && MsgExtensionsChangedStringCallbackStore != null)
      {
        MsgExtensionsChangedStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        MsgExtensionsChangedCallbackStore = null;
        MsgExtensionsChangedStringCallbackStore = null;
      }

      if (MsgExtensionsChangedCallbackStore == null && MsgExtensionsChangedStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetMsgExtensionsChangedCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置消息扩展信息删除的回调
    /// Set message extensions deleted callback
    /// </summary>
    /// <param name="callback">回调 MsgExtensionsDeletedCallback</param>
    /// <param name="stringCallback">回调 MsgExtensionsDeletedStringCallback</param>
    public static void SetMsgExtensionsDeletedCallback(MsgExtensionsDeletedCallback callback = null, MsgExtensionsDeletedStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        MsgExtensionsDeletedCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        MsgExtensionsDeletedStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveMsgExtensionsDeletedCallback();
        return;
      }

      IMNativeSDK.TIMSetMsgExtensionsDeletedCallback(TIMMsgExtensionsDeletedCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除消息扩展信息删除的回调
    /// Remove message extensions deleted callback
    /// </summary>
    /// <param name="callback">回调 MsgExtensionsDeletedCallback</param>
    /// <param name="stringCallback">回调 MsgExtensionsDeletedStringCallback</param>
    public static void RemoveMsgExtensionsDeletedCallback(MsgExtensionsDeletedCallback callback = null, MsgExtensionsDeletedStringCallback stringCallback = null)
    {
      if (callback != null && MsgExtensionsDeletedCallbackStore != null)
      {
        MsgExtensionsDeletedCallbackStore -= callback;
      }

      if (stringCallback != null && MsgExtensionsDeletedStringCallbackStore != null)
      {
        MsgExtensionsDeletedStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        MsgExtensionsDeletedCallbackStore = null;
        MsgExtensionsDeletedStringCallbackStore = null;
      }

      if (MsgExtensionsDeletedCallbackStore == null && MsgExtensionsDeletedStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetMsgExtensionsDeletedCallback(null, Utils.string2intptr(""));
      }
    }
  
    /// <summary>
    /// 设置置顶消息变更的回调
    /// Set group pinned message changed callback
    /// </summary>
    /// <param name="callback">回调 MsgGroupPinnedMessageChangedCallback</param>
    /// <param name="stringCallback">回调 MsgGroupPinnedMessageChangedStringCallback</param>
    public static void SetMsgGroupPinnedMessageChangedCallback(MsgGroupPinnedMessageChangedCallback callback = null, MsgGroupPinnedMessageChangedStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        MsgGroupPinnedMessageChangedCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        MsgGroupPinnedMessageChangedStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveMsgGroupPinnedMessageChangedCallback();
        return;
      }

      IMNativeSDK.TIMSetMsgGroupPinnedMessageChangedCallback(TIMMsgGroupPinnedMessageChangedCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除置顶消息变更的回调
    /// Remove group pinned message changed callback
    /// </summary>
    /// <param name="callback">回调 MsgGroupPinnedMessageChangedCallback</param>
    /// <param name="stringCallback">回调 MsgGroupPinnedMessageChangedStringCallback</param>
    public static void RemoveMsgGroupPinnedMessageChangedCallback(MsgGroupPinnedMessageChangedCallback callback = null, MsgGroupPinnedMessageChangedStringCallback stringCallback = null)
    {
      if (callback != null && MsgGroupPinnedMessageChangedCallbackStore != null)
      {
        MsgGroupPinnedMessageChangedCallbackStore -= callback;
      }

      if (stringCallback != null && MsgGroupPinnedMessageChangedStringCallbackStore != null)
      {
        MsgGroupPinnedMessageChangedStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        MsgGroupPinnedMessageChangedCallbackStore = null;
        MsgGroupPinnedMessageChangedStringCallbackStore = null;
      }

      if (MsgGroupPinnedMessageChangedCallbackStore == null && MsgGroupPinnedMessageChangedStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetMsgGroupPinnedMessageChangedCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置话题创建回调
    /// Set group topic created callback
    /// </summary>
    /// <param name="callback">回调 GroupTopicCreatedCallback</param>
    public static void SetGroupTopicCreatedCallback(GroupTopicCreatedCallback callback)
    {
      if (callback != null)
      {
        string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

        string user_data = fn_name + "_" + Utils.getRandomStr();

        GroupTopicCreatedCallbackStore += callback;

        IMNativeSDK.TIMSetGroupTopicCreatedCallback(TIMGroupTopicCreatedCallbackInstance, Utils.string2intptr(user_data));
      }
      else
      {
        RemoveGroupTopicCreatedCallback();
      }
    }

    /// <summary>
    /// 移除话题创建回调
    /// Remove group topic created callback
    /// </summary>
    /// <param name="callback">回调 GroupTopicCreatedCallback</param>
    public static void RemoveGroupTopicCreatedCallback(GroupTopicCreatedCallback callback = null)
    {
      if (callback != null && GroupTopicCreatedCallbackStore != null)
      {
        GroupTopicCreatedCallbackStore -= callback;
      }
      else
      {
        GroupTopicCreatedCallbackStore = null;
      }

      if (GroupTopicCreatedCallbackStore == null)
      {
        IMNativeSDK.TIMSetGroupTopicCreatedCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置话题被删除回调
    /// Set group topic deleted callback
    /// </summary>
    /// <param name="callback">回调 GroupTopicDeletedCallback</param>
    /// <param name="stringCallback">回调 GroupTopicDeletedStringCallback</param>
    public static void SetGroupTopicDeletedCallback(GroupTopicDeletedCallback callback = null, GroupTopicDeletedStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        GroupTopicDeletedCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        GroupTopicDeletedStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveGroupTopicDeletedCallback();
        return;
      }

      IMNativeSDK.TIMSetGroupTopicDeletedCallback(TIMGroupTopicDeletedCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除话题被删除回调
    /// Remove group topic deleted callback
    /// </summary>
    /// <param name="callback">回调 GroupTopicDeletedCallback</param>
    /// <param name="stringCallback">回调 GroupTopicDeletedStringCallback</param>
    public static void RemoveGroupTopicDeletedCallback(GroupTopicDeletedCallback callback = null, GroupTopicDeletedStringCallback stringCallback = null)
    {
      if (callback != null && GroupTopicDeletedCallbackStore != null)
      {
        GroupTopicDeletedCallbackStore -= callback;
      }

      if (stringCallback != null && GroupTopicDeletedStringCallbackStore != null)
      {
        GroupTopicDeletedStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        GroupTopicDeletedCallbackStore = null;
        GroupTopicDeletedStringCallbackStore = null;
      }

      if (GroupTopicDeletedCallbackStore == null && GroupTopicDeletedStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetGroupTopicDeletedCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置话题更新回调
    /// Set group topic updated callback
    /// </summary>
    /// <param name="callback">回调 GroupTopicChangedCallback</param>
    /// <param name="stringCallback">回调 GroupTopicChangedStringCallback</param>
    public static void SetGroupTopicChangedCallback(GroupTopicChangedCallback callback = null, GroupTopicChangedStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        GroupTopicChangedCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        GroupTopicChangedStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveGroupTopicChangedCallback();
        return;
      }

      IMNativeSDK.TIMSetGroupTopicChangedCallback(TIMGroupTopicChangedCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除话题更新回调
    /// Remove group topic updated callback
    /// </summary>
    /// <param name="callback">回调 GroupTopicChangedCallback</param>
    /// <param name="stringCallback">回调 GroupTopicChangedStringCallback</param>
    public static void RemoveGroupTopicChangedCallback(GroupTopicChangedCallback callback = null, GroupTopicChangedStringCallback stringCallback = null)
    {
      if (callback != null && GroupTopicChangedCallbackStore != null)
      {
        GroupTopicChangedCallbackStore -= callback;
      }

      if (stringCallback != null && GroupTopicChangedStringCallbackStore != null)
      {
        GroupTopicChangedStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        GroupTopicChangedCallbackStore = null;
        GroupTopicChangedStringCallbackStore = null;
      }

      if (GroupTopicChangedCallbackStore == null && GroupTopicChangedStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetGroupTopicChangedCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置当前用户的资料发生更新时的回调
    /// Set self info updated callback
    /// </summary>
    /// <param name="callback">回调 SelfInfoUpdatedCallback</param>
    /// <param name="stringCallback">回调 SelfInfoUpdatedStringCallback</param>
    public static void SetSelfInfoUpdatedCallback(SelfInfoUpdatedCallback callback = null, SelfInfoUpdatedStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        SelfInfoUpdatedCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        SelfInfoUpdatedStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveSelfInfoUpdatedCallback();
        return;
      }

      IMNativeSDK.TIMSetSelfInfoUpdatedCallback(TIMSelfInfoUpdatedCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除当前用户的资料发生更新时的回调
    /// Remove self info updated callback
    /// </summary>
    /// <param name="callback">回调 SelfInfoUpdatedCallback</param>
    /// <param name="stringCallback">回调 SelfInfoUpdatedStringCallback</param>
    public static void RemoveSelfInfoUpdatedCallback(SelfInfoUpdatedCallback callback = null, SelfInfoUpdatedStringCallback stringCallback = null)
    {
      if (callback != null && SelfInfoUpdatedCallbackStore != null)
      {
        SelfInfoUpdatedCallbackStore -= callback;
      }

      if (stringCallback != null && SelfInfoUpdatedStringCallbackStore != null)
      {
        SelfInfoUpdatedStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        SelfInfoUpdatedCallbackStore = null;
        SelfInfoUpdatedStringCallbackStore = null;
      }

      if (SelfInfoUpdatedCallbackStore == null && SelfInfoUpdatedStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetSelfInfoUpdatedCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置用户状态变更通知回调
    /// Set user status updated callback
    /// <param>收到通知的情况：(The following cases may trigger this callback:)</param>
    /// <param>1. 订阅过的用户发生了状态变更（包括在线状态和自定义状态），会触发该回调 (1. Subscribed user status changed (Include online status and custom status))</param>
    /// <param>2. 在 IM 控制台打开了好友状态通知开关，即使未主动订阅，当好友状态发生变更时，也会触发该回调 (2. Enable friend's status notification on the IM Console. It will trigger this callback even you haven't subscribe it)</param>
    /// <param>3. 同一个账号多设备登陆，当其中一台设备修改了自定义状态，所有设备都会收到该回调 (An account logged on multi-platforms, and the status is changed on one of the devices, all platforms will receive this callback)</param>
    /// </summary>
    /// <param name="callback">回调 UserStatusChangedCallback</param>
    /// <param name="stringCallback">回调 UserStatusChangedStringCallback</param>
    public static void SetUserStatusChangedCallback(UserStatusChangedCallback callback = null, UserStatusChangedStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        UserStatusChangedCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        UserStatusChangedStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveUserStatusChangedCallback();
        return;
      }

      IMNativeSDK.TIMSetUserStatusChangedCallback(TIMUserStatusChangedCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除用户状态变更通知回调
    /// Remove user status updated callback
    /// </summary>
    /// <param name="callback">回调 UserStatusChangedCallback</param>
    /// <param name="stringCallback">回调 UserStatusChangedStringCallback</param>
    public static void RemoveUserStatusChangedCallback(UserStatusChangedCallback callback = null, UserStatusChangedStringCallback stringCallback = null)
    {
      if (callback != null && UserStatusChangedCallbackStore != null)
      {
        UserStatusChangedCallbackStore -= callback;
      }

      if (stringCallback != null && UserStatusChangedStringCallbackStore != null)
      {
        UserStatusChangedStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        UserStatusChangedCallbackStore = null;
        UserStatusChangedStringCallbackStore = null;
      }

      if (UserStatusChangedCallbackStore == null && UserStatusChangedStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetUserStatusChangedCallback(null, Utils.string2intptr(""));
      }
    }
    /// <summary>
    /// 订阅用户资料变更的回调
    /// user info updated callback
    /// </summary>
    /// <param name="callback">回调 UserInfoChangedCallback</param>
    /// <param name="stringCallback">回调 UserInfoChangedStringCallback</param>
    public static void SetUserInfoChangedCallback(UserInfoChangedCallback callback = null, UserInfoChangedStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        UserInfoChangedCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        UserInfoChangedStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveUserInfoChangedCallback();
        return;
      }

      IMNativeSDK.TIMSetUserInfoChangedCallback(TIMUserInfoChangedCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除订阅用户资料变更的回调
    /// Remove user info updated callback
    /// </summary>
    /// <param name="callback">回调 UserInfoChangedCallback</param>
    /// <param name="stringCallback">回调 UserInfoChangedStringCallback</param>
    public static void RemoveUserInfoChangedCallback(UserInfoChangedCallback callback = null, UserInfoChangedStringCallback stringCallback = null)
    {
      if (callback != null && UserInfoChangedCallbackStore != null)
      {
        UserInfoChangedCallbackStore -= callback;
      }

      if (stringCallback != null && UserInfoChangedStringCallbackStore != null)
      {
        UserInfoChangedStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        UserInfoChangedCallbackStore = null;
        UserInfoChangedStringCallbackStore = null;
      }

      if (UserInfoChangedCallbackStore == null && UserInfoChangedStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetUserInfoChangedCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置好友分组创建的回调
    /// Set friend group created callback
    /// </summary>
    /// <param name="callback">回调 FriendGroupCreatedCallback</param>
    /// <param name="stringCallback">回调 FriendGroupCreatedStringCallback</param>
    public static void SetFriendGroupCreatedCallback(FriendGroupCreatedCallback callback = null, FriendGroupCreatedStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        FriendGroupCreatedCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        FriendGroupCreatedStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveFriendGroupCreatedCallback();
        return;
      }

      IMNativeSDK.TIMSetFriendGroupCreatedCallback(TIMFriendGroupCreatedCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除好友分组创建的回调
    /// Remove friend group created callback
    /// </summary>
    /// <param name="callback">回调 FriendGroupCreatedCallback</param>
    /// <param name="stringCallback">回调 FriendGroupCreatedStringCallback</param>
    public static void RemoveFriendGroupCreatedCallback(FriendGroupCreatedCallback callback = null, FriendGroupCreatedStringCallback stringCallback = null)
    {
      if (callback != null && FriendGroupCreatedCallbackStore != null)
      {
        FriendGroupCreatedCallbackStore -= callback;
      }

      if (stringCallback != null && FriendGroupCreatedStringCallbackStore != null)
      {
        FriendGroupCreatedStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        FriendGroupCreatedCallbackStore = null;
        FriendGroupCreatedStringCallbackStore = null;
      }

      if (FriendGroupCreatedCallbackStore == null && FriendGroupCreatedStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetFriendGroupCreatedCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置好友分组删除的回调
    /// Set friend group deleted callback
    /// </summary>
    /// <param name="callback">回调 FriendGroupDeletedCallback</param>
    /// <param name="stringCallback">回调 FriendGroupDeletedStringCallback</param>
    public static void SetFriendGroupDeletedCallback(FriendGroupDeletedCallback callback = null, FriendGroupDeletedStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        FriendGroupDeletedCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        FriendGroupDeletedStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveFriendGroupDeletedCallback();
        return;
      }

      IMNativeSDK.TIMSetFriendGroupDeletedCallback(TIMFriendGroupDeletedCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除好友分组删除的回调
    /// Remove friend group deleted callback
    /// </summary>
    /// <param name="callback">回调 FriendGroupDeletedCallback</param>
    /// <param name="stringCallback">回调 FriendGroupDeletedStringCallback</param>
    public static void RemoveFriendGroupDeletedCallback(FriendGroupDeletedCallback callback = null, FriendGroupDeletedStringCallback stringCallback = null)
    {
      if (callback != null && FriendGroupDeletedCallbackStore != null)
      {
        FriendGroupDeletedCallbackStore -= callback;
      }

      if (stringCallback != null && FriendGroupDeletedStringCallbackStore != null)
      {
        FriendGroupDeletedStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        FriendGroupDeletedCallbackStore = null;
        FriendGroupDeletedStringCallbackStore = null;
      }

      if (FriendGroupDeletedCallbackStore == null && FriendGroupDeletedStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetFriendGroupDeletedCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置好友分组名称变更的回调
    /// Set friend group name changed callback
    /// </summary>
    /// <param name="callback">回调 FriendGroupNameChangedCallback</param>
    /// <param name="stringCallback">回调 FriendGroupNameChangedStringCallback</param>
    public static void SetFriendGroupNameChangedCallback(FriendGroupNameChangedCallback callback = null, FriendGroupNameChangedStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        FriendGroupNameChangedCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        FriendGroupNameChangedStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveFriendGroupNameChangedCallback();
        return;
      }

      IMNativeSDK.TIMSetFriendGroupNameChangedCallback(TIMFriendGroupNameChangedCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除好友分组名称变更的回调
    /// Remove friend group name changed callback
    /// </summary>
    /// <param name="callback">回调 FriendGroupNameChangedCallback</param>
    /// <param name="stringCallback">回调 FriendGroupNameChangedStringCallback</param>
    public static void RemoveFriendGroupNameChangedCallback(FriendGroupNameChangedCallback callback = null, FriendGroupNameChangedStringCallback stringCallback = null)
    {
      if (callback != null && FriendGroupNameChangedCallbackStore != null)
      {
        FriendGroupNameChangedCallbackStore -= callback;
      }

      if (stringCallback != null && FriendGroupNameChangedStringCallbackStore != null)
      {
        FriendGroupNameChangedStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        FriendGroupNameChangedCallbackStore = null;
        FriendGroupNameChangedStringCallbackStore = null;
      }

      if (FriendGroupNameChangedCallbackStore == null && FriendGroupNameChangedStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetFriendGroupNameChangedCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置向好友分组中添加好友的通知回调
    /// Set callback for adding friends to a friend group
    /// </summary>
    /// <param name="callback">回调 FriendsAddedToGroupCallback</param>
    /// <param name="stringCallback">回调 FriendsAddedToGroupStringCallback</param>
    public static void SetFriendsAddedToGroupCallback(FriendsAddedToGroupCallback callback = null, FriendsAddedToGroupStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        FriendsAddedToGroupCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        FriendsAddedToGroupStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveFriendsAddedToGroupCallback();
        return;
      }

      IMNativeSDK.TIMSetFriendsAddedToGroupCallback(TIMFriendsAddedToGroupCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除向好友分组中添加好友的通知回调
    /// Remove callback for adding friends to a friend group
    /// </summary>
    /// <param name="callback">回调 FriendsAddedToGroupCallback</param>
    /// <param name="stringCallback">回调 FriendsAddedToGroupStringCallback</param>
    public static void RemoveFriendsAddedToGroupCallback(FriendsAddedToGroupCallback callback = null, FriendsAddedToGroupStringCallback stringCallback = null)
    {
      if (callback != null && FriendsAddedToGroupCallbackStore != null)
      {
        FriendsAddedToGroupCallbackStore -= callback;
      }

      if (stringCallback != null && FriendsAddedToGroupStringCallbackStore != null)
      {
        FriendsAddedToGroupStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        FriendsAddedToGroupCallbackStore = null;
        FriendsAddedToGroupStringCallbackStore = null;
      }

      if (FriendsAddedToGroupCallbackStore == null && FriendsAddedToGroupStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetFriendsAddedToGroupCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置从好友分组删除好友的通知回调
    /// Set callback for deleting friends from a friend group
    /// </summary>
    /// <param name="callback">回调 FriendsDeletedFromGroupCallback</param>
    /// <param name="stringCallback">回调 FriendsDeletedFromGroupStringCallback</param>
    public static void SetFriendsDeletedFromGroupCallback(FriendsDeletedFromGroupCallback callback = null, FriendsDeletedFromGroupStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        FriendsDeletedFromGroupCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        FriendsDeletedFromGroupStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveFriendsDeletedFromGroupCallback();
        return;
      }

      IMNativeSDK.TIMSetFriendsDeletedFromGroupCallback(TIMFriendsDeletedFromGroupCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除从好友分组删除好友的通知回调
    /// Remove callback for deleting friends from a friend group
    /// </summary>
    /// <param name="callback">回调 FriendsDeletedFromGroupCallback</param>
    /// <param name="stringCallback">回调 FriendsDeletedFromGroupStringCallback</param>
    public static void RemoveFriendsDeletedFromGroupCallback(FriendsDeletedFromGroupCallback callback = null, FriendsDeletedFromGroupStringCallback stringCallback = null)
    {
      if (callback != null && FriendsDeletedFromGroupCallbackStore != null)
      {
        FriendsDeletedFromGroupCallbackStore -= callback;
      }

      if (stringCallback != null && FriendsDeletedFromGroupStringCallbackStore != null)
      {
        FriendsDeletedFromGroupStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        FriendsDeletedFromGroupCallbackStore = null;
        FriendsDeletedFromGroupStringCallbackStore = null;
      }

      if (FriendsDeletedFromGroupCallbackStore == null && FriendsDeletedFromGroupStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetFriendsDeletedFromGroupCallback(null, Utils.string2intptr(""));
      }
    }

    /// </summary>
    /// 设置我的关注列表变更通知回调
    /// Set my following list changed callback
    /// <param name="callback">回调 MyFollowingListChangedCallback</param>
    /// <param name="stringCallback">回调 MyFollowingListChangedStringCallback</param>
    public static void SetMyFollowingListChangedCallback(MyFollowingListChangedCallback callback = null, MyFollowingListChangedStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        MyFollowingListChangedCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        MyFollowingListChangedStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveMyFollowingListChangedCallback();
        return;
      }

      IMNativeSDK.TIMSetMyFollowingListChangedCallback(TIMMyFollowingListChangedCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除我的关注列表变更通知回调
    /// Remove my following list changed callback
    /// </summary>
    /// <param name="callback">回调 MyFollowingListChangedCallback</param>
    /// <param name="stringCallback">回调 MyFollowingListChangedStringCallback</param>
    public static void RemoveMyFollowingListChangedCallback(MyFollowingListChangedCallback callback = null, MyFollowingListChangedStringCallback stringCallback = null)
    {
      if (callback != null && MyFollowingListChangedCallbackStore != null)
      {
        MyFollowingListChangedCallbackStore -= callback;
      }

      if (stringCallback != null && MyFollowingListChangedStringCallbackStore != null)
      {
        MyFollowingListChangedStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        MyFollowingListChangedCallbackStore = null;
        MyFollowingListChangedStringCallbackStore = null;
      }

      if (MyFollowingListChangedCallbackStore == null && MyFollowingListChangedStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetMyFollowingListChangedCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置我的粉丝列表变更通知回调
    /// Set my followers list changed callback
    /// </summary>
    /// <param name="callback">回调 MyFollowersListChangedCallback</param>
    /// <param name="stringCallback">回调 MyFollowersListChangedStringCallback</param>
    public static void SetMyFollowersListChangedCallback(MyFollowersListChangedCallback callback = null, MyFollowersListChangedStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        MyFollowersListChangedCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        MyFollowersListChangedStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveMyFollowersListChangedCallback();
        return;
      }

      IMNativeSDK.TIMSetMyFollowersListChangedCallback(TIMMyFollowersListChangedCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除我的粉丝列表变更通知回调
    /// Remove my followers list changed callback
    /// </summary>
    /// <param name="callback">回调 MyFollowersListChangedCallback</param>
    /// <param name="stringCallback">回调 MyFollowersListChangedStringCallback</param>
    public static void RemoveMyFollowersListChangedCallback(MyFollowersListChangedCallback callback = null, MyFollowersListChangedStringCallback stringCallback = null)
    {
      if (callback != null && MyFollowersListChangedCallbackStore != null)
      {
        MyFollowersListChangedCallbackStore -= callback;
      }

      if (stringCallback != null && MyFollowersListChangedStringCallbackStore != null)
      {
        MyFollowersListChangedStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        MyFollowersListChangedCallbackStore = null;
        MyFollowersListChangedStringCallbackStore = null;
      }

      if (MyFollowersListChangedCallbackStore == null && MyFollowersListChangedStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetMyFollowersListChangedCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置互粉列表变更通知回调
    /// Set mutual followers list changed callback
    /// </summary>
    /// <param name="callback">回调 MutualFollowersListChangedCallback</param>
    /// <param name="stringCallback">回调 MutualFollowersListChangedStringCallback</param>
    public static void SetMutualFollowersListChangedCallback(MutualFollowersListChangedCallback callback = null, MutualFollowersListChangedStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        MutualFollowersListChangedCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        MutualFollowersListChangedStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveMutualFollowersListChangedCallback();
        return;
      }

      IMNativeSDK.TIMSetMutualFollowersListChangedCallback(TIMMutualFollowersListChangedCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除互粉列表变更通知回调
    /// Remove mutual followers list changed callback
    /// </summary>
    /// <param name="callback">回调 MutualFollowersListChangedCallback</param>
    /// <param name="stringCallback">回调 MutualFollowersListChangedStringCallback</param>
    public static void RemoveMutualFollowersListChangedCallback(MutualFollowersListChangedCallback callback = null, MutualFollowersListChangedStringCallback stringCallback = null)
    {
      if (callback != null && MutualFollowersListChangedCallbackStore != null)
      {
        MutualFollowersListChangedCallbackStore -= callback;
      }

      if (stringCallback != null && MutualFollowersListChangedStringCallbackStore != null)
      {
        MutualFollowersListChangedStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        MutualFollowersListChangedCallbackStore = null;
        MutualFollowersListChangedStringCallbackStore = null;
      }

      if (MutualFollowersListChangedCallbackStore == null && MutualFollowersListChangedStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetMutualFollowersListChangedCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置收到信令邀请通知回调
    /// Set callback for receiving new invitation
    /// </summary>
    /// <param name="callback">回调 SignalingReceiveNewInvitationCallback</param>
    /// <param name="stringCallback">回调 SignalingReceiveNewInvitationStringCallback</param>
    public static void SetSignalingReceiveNewInvitationCallback(SignalingReceiveNewInvitationCallback callback = null, SignalingReceiveNewInvitationStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        SignalingReceiveNewInvitationCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        SignalingReceiveNewInvitationStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveSignalingReceiveNewInvitationCallback();
        return;
      }

      IMNativeSDK.TIMSetSignalingReceiveNewInvitationCallback(TIMSignalingReceiveNewInvitationCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除收到信令邀请通知回调
    /// Remove callback for receiving new invitation
    /// </summary>
    /// <param name="callback">回调 SignalingReceiveNewInvitationCallback</param>
    /// <param name="stringCallback">回调 SignalingReceiveNewInvitationStringCallback</param>
    public static void RemoveSignalingReceiveNewInvitationCallback(SignalingReceiveNewInvitationCallback callback = null, SignalingReceiveNewInvitationStringCallback stringCallback = null)
    {
      if (callback != null && SignalingReceiveNewInvitationCallbackStore != null)
      {
        SignalingReceiveNewInvitationCallbackStore -= callback;
      }

      if (stringCallback != null && SignalingReceiveNewInvitationStringCallbackStore != null)
      {
        SignalingReceiveNewInvitationStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        SignalingReceiveNewInvitationCallbackStore = null;
        SignalingReceiveNewInvitationStringCallbackStore = null;
      }

      if (SignalingReceiveNewInvitationCallbackStore == null && SignalingReceiveNewInvitationStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetSignalingReceiveNewInvitationCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置被邀请者同意邀请的通知回调
    /// Set callback for the invitation is accepted by the invitee
    /// </summary>
    /// <param name="callback">回调 SignalingInviteeAcceptedCallback</param>
    /// <param name="stringCallback">回调 SignalingInviteeAcceptedStringCallback</param>
    public static void SetSignalingInviteeAcceptedCallback(SignalingInviteeAcceptedCallback callback = null, SignalingInviteeAcceptedStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        SignalingInviteeAcceptedCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        SignalingInviteeAcceptedStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveSignalingInviteeAcceptedCallback();
        return;
      }

      IMNativeSDK.TIMSetSignalingInviteeAcceptedCallback(TIMSignalingInviteeAcceptedCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除被邀请者同意邀请的通知回调
    /// Remove callback for the invitation is accepted by the invitee
    /// </summary>
    /// <param name="callback">回调 SignalingInviteeAcceptedCallback</param>
    /// <param name="stringCallback">回调 SignalingInviteeAcceptedStringCallback</param>
    public static void RemoveSignalingInviteeAcceptedCallback(SignalingInviteeAcceptedCallback callback = null, SignalingInviteeAcceptedStringCallback stringCallback = null)
    {
      if (callback != null && SignalingInviteeAcceptedCallbackStore != null)
      {
        SignalingInviteeAcceptedCallbackStore -= callback;
      }

      if (stringCallback != null && SignalingInviteeAcceptedStringCallbackStore != null)
      {
        SignalingInviteeAcceptedStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        SignalingInviteeAcceptedCallbackStore = null;
        SignalingInviteeAcceptedStringCallbackStore = null;
      }

      if (SignalingInviteeAcceptedCallbackStore == null && SignalingInviteeAcceptedStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetSignalingInviteeAcceptedCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置被邀请者拒绝邀请的通知回调
    /// Set callback for the invitation is rejected by the invitee
    /// </summary>
    /// <param name="callback">回调 SignalingInviteeRejectedCallback</param>
    /// <param name="stringCallback">回调 SignalingInviteeRejectedStringCallback</param>
    public static void SetSignalingInviteeRejectedCallback(SignalingInviteeRejectedCallback callback = null, SignalingInviteeRejectedStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        SignalingInviteeRejectedCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        SignalingInviteeRejectedStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveSignalingInviteeRejectedCallback();
        return;
      }

      IMNativeSDK.TIMSetSignalingInviteeRejectedCallback(TIMSignalingInviteeRejectedCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除被邀请者拒绝邀请的通知回调
    /// Remove callback for the invitation is rejected by the invitee
    /// </summary>
    /// <param name="callback">回调 SignalingInviteeRejectedCallback</param>
    /// <param name="stringCallback">回调 SignalingInviteeRejectedStringCallback</param>
    public static void RemoveSignalingInviteeRejectedCallback(SignalingInviteeRejectedCallback callback = null, SignalingInviteeRejectedStringCallback stringCallback = null)
    {
      if (callback != null && SignalingInviteeRejectedCallbackStore != null)
      {
        SignalingInviteeRejectedCallbackStore -= callback;
      }

      if (stringCallback != null && SignalingInviteeRejectedStringCallbackStore != null)
      {
        SignalingInviteeRejectedStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        SignalingInviteeRejectedCallbackStore = null;
        SignalingInviteeRejectedStringCallbackStore = null;
      }

      if (SignalingInviteeRejectedCallbackStore == null && SignalingInviteeRejectedStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetSignalingInviteeRejectedCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置邀请被取消的通知回调
    /// Set invitation cancelled callback
    /// </summary>
    /// <param name="callback">回调 SignalingInvitationCancelledCallback</param>
    /// <param name="stringCallback">回调 SignalingInvitationCancelledStringCallback</param>
    public static void SetSignalingInvitationCancelledCallback(SignalingInvitationCancelledCallback callback = null, SignalingInvitationCancelledStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        SignalingInvitationCancelledCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        SignalingInvitationCancelledStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveSignalingInvitationCancelledCallback();
        return;
      }

      IMNativeSDK.TIMSetSignalingInvitationCancelledCallback(TIMSignalingInvitationCancelledCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除邀请被取消的通知回调
    /// Remove invitation cancelled callback
    /// </summary>
    /// <param name="callback">回调 SignalingInvitationCancelledCallback</param>
    /// <param name="stringCallback">回调 SignalingInvitationCancelledStringCallback</param>
    public static void RemoveSignalingInvitationCancelledCallback(SignalingInvitationCancelledCallback callback = null, SignalingInvitationCancelledStringCallback stringCallback = null)
    {
      if (callback != null && SignalingInvitationCancelledCallbackStore != null)
      {
        SignalingInvitationCancelledCallbackStore -= callback;
      }

      if (stringCallback != null && SignalingInvitationCancelledStringCallbackStore != null)
      {
        SignalingInvitationCancelledStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        SignalingInvitationCancelledCallbackStore = null;
        SignalingInvitationCancelledStringCallbackStore = null;
      }

      if (SignalingInvitationCancelledCallbackStore == null && SignalingInvitationCancelledStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetSignalingInvitationCancelledCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置邀请超时的通知回调
    /// Set invitation timeout callback
    /// </summary>
    /// <param name="callback">回调 SignalingInvitationTimeoutCallback</param>
    /// <param name="stringCallback">回调 SignalingInvitationTimeoutStringCallback</param>
    public static void SetSignalingInvitationTimeoutCallback(SignalingInvitationTimeoutCallback callback = null, SignalingInvitationTimeoutStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        SignalingInvitationTimeoutCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        SignalingInvitationTimeoutStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveSignalingInvitationTimeoutCallback();
        return;
      }

      IMNativeSDK.TIMSetSignalingInvitationTimeoutCallback(TIMSignalingInvitationTimeoutCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除邀请超时的通知回调  
    /// Remove invitation timeout callback
    /// </summary>
    /// <param name="callback">回调 SignalingInvitationTimeoutCallback</param>
    /// <param name="stringCallback">回调 SignalingInvitationTimeoutStringCallback</param>
    public static void RemoveSignalingInvitationTimeoutCallback(SignalingInvitationTimeoutCallback callback = null, SignalingInvitationTimeoutStringCallback stringCallback = null)
    {
      if (callback != null && SignalingInvitationTimeoutCallbackStore != null)
      {
        SignalingInvitationTimeoutCallbackStore -= callback;
      }

      if (stringCallback != null && SignalingInvitationTimeoutStringCallbackStore != null)
      {
        SignalingInvitationTimeoutStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        SignalingInvitationTimeoutCallbackStore = null;
        SignalingInvitationTimeoutStringCallbackStore = null;
      }

      if (SignalingInvitationTimeoutCallbackStore == null && SignalingInvitationTimeoutStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetSignalingInvitationTimeoutCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置邀请被修改的通知回调
    /// Set invitation modified callback
    /// </summary>
    /// <param name="callback">回调 SignalingInvitationModifiedCallback</param>
    /// <param name="stringCallback">回调 SignalingInvitationModifiedStringCallback</param>
    public static void SetSignalingInvitationModifiedCallback(SignalingInvitationModifiedCallback callback = null, SignalingInvitationModifiedStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        SignalingInvitationModifiedCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        SignalingInvitationModifiedStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveSignalingInvitationModifiedCallback();
        return;
      }

      IMNativeSDK.TIMSetSignalingInvitationModifiedCallback(TIMSignalingInvitationModifiedCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除邀请被修改的通知回调
    /// Remove invitation modified callback
    /// </summary>
    /// <param name="callback">回调 SignalingInvitationModifiedCallback</param>
    /// <param name="stringCallback">回调 SignalingInvitationModifiedStringCallback</param>
    public static void RemoveSignalingInvitationModifiedCallback(SignalingInvitationModifiedCallback callback = null, SignalingInvitationModifiedStringCallback stringCallback = null)
    {
      if (callback != null && SignalingInvitationModifiedCallbackStore != null)
      {
        SignalingInvitationModifiedCallbackStore -= callback;
      }

      if (stringCallback != null && SignalingInvitationModifiedStringCallbackStore != null)
      {
        SignalingInvitationModifiedStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        SignalingInvitationModifiedCallbackStore = null;
        SignalingInvitationModifiedStringCallbackStore = null;
      }

      if (SignalingInvitationModifiedCallbackStore == null && SignalingInvitationModifiedStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetSignalingInvitationModifiedCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置订阅公众号通知回调
    /// Set official account subscribed callback
    /// </summary>
    /// <param name="callback">回调 OfficialAccountSubscribedCallback</param>
    /// <param name="stringCallback">回调 OfficialAccountSubscribedStringCallback</param>
    public static void SetOfficialAccountSubscribedCallback(OfficialAccountSubscribedCallback callback = null, OfficialAccountSubscribedStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        OfficialAccountSubscribedCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        OfficialAccountSubscribedStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveOfficialAccountSubscribedCallback();
        return;
      }

      IMNativeSDK.TIMSetOfficialAccountSubscribedCallback(TIMOfficialAccountSubscribedCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除订阅公众号通知回调
    /// Remove official account subscribed callback
    /// </summary>
    /// <param name="callback">回调 OfficialAccountSubscribedCallback</param>
    /// <param name="stringCallback">回调 OfficialAccountSubscribedStringCallback</param>
    public static void RemoveOfficialAccountSubscribedCallback(OfficialAccountSubscribedCallback callback = null, OfficialAccountSubscribedStringCallback stringCallback = null)
    {
      if (callback != null && OfficialAccountSubscribedCallbackStore != null)
      {
        OfficialAccountSubscribedCallbackStore -= callback;
      }

      if (stringCallback != null && OfficialAccountSubscribedStringCallbackStore != null)
      {
        OfficialAccountSubscribedStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        OfficialAccountSubscribedCallbackStore = null;
        OfficialAccountSubscribedStringCallbackStore = null;
      }

      if (OfficialAccountSubscribedCallbackStore == null && OfficialAccountSubscribedStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetOfficialAccountSubscribedCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置取消订阅公众号通知回调
    /// Set official account unsubscribed callback
    /// </summary>
    /// <param name="callback">回调 OfficialAccountUnsubscribedCallback</param>
    /// <param name="stringCallback">回调 OfficialAccountUnsubscribedStringCallback</param>
    public static void SetOfficialAccountUnsubscribedCallback(OfficialAccountUnsubscribedCallback callback = null, OfficialAccountUnsubscribedStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        OfficialAccountUnsubscribedCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        OfficialAccountUnsubscribedStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveOfficialAccountUnsubscribedCallback();
        return;
      }

      IMNativeSDK.TIMSetOfficialAccountUnsubscribedCallback(TIMOfficialAccountUnsubscribedCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除取消订阅公众号通知回调
    /// Remove official account unsubscribed callback
    /// </summary>
    /// <param name="callback">回调 OfficialAccountUnsubscribedCallback</param>
    /// <param name="stringCallback">回调 OfficialAccountUnsubscribedStringCallback</param>
    public static void RemoveOfficialAccountUnsubscribedCallback(OfficialAccountUnsubscribedCallback callback = null, OfficialAccountUnsubscribedStringCallback stringCallback = null)
    {
      if (callback != null && OfficialAccountUnsubscribedCallbackStore != null)
      {
        OfficialAccountUnsubscribedCallbackStore -= callback;
      }

      if (stringCallback != null && OfficialAccountUnsubscribedStringCallbackStore != null)
      {
        OfficialAccountUnsubscribedStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        OfficialAccountUnsubscribedCallbackStore = null;
        OfficialAccountUnsubscribedStringCallbackStore = null;
      }

      if (OfficialAccountUnsubscribedCallbackStore == null && OfficialAccountUnsubscribedStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetOfficialAccountUnsubscribedCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置删除公众号通知回调
    /// Set official account deleted callback
    /// </summary>
    /// <param name="callback">回调 OfficialAccountDeletedCallback</param>
    /// <param name="stringCallback">回调 OfficialAccountDeletedStringCallback</param>
    public static void SetOfficialAccountDeletedCallback(OfficialAccountDeletedCallback callback = null, OfficialAccountDeletedStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        OfficialAccountDeletedCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        OfficialAccountDeletedStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveOfficialAccountDeletedCallback();
        return;
      }

      IMNativeSDK.TIMSetOfficialAccountDeletedCallback(TIMOfficialAccountDeletedCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除取消订阅公众号通知回调
    /// Remove official account unsubscribed callback
    /// </summary>
    /// <param name="callback">回调 OfficialAccountDeletedCallback</param>
    /// <param name="stringCallback">回调 OfficialAccountDeletedStringCallback</param>
    public static void RemoveOfficialAccountDeletedCallback(OfficialAccountDeletedCallback callback = null, OfficialAccountDeletedStringCallback stringCallback = null)
    {
      if (callback != null && OfficialAccountDeletedCallbackStore != null)
      {
        OfficialAccountDeletedCallbackStore -= callback;
      }

      if (stringCallback != null && OfficialAccountDeletedStringCallbackStore != null)
      {
        OfficialAccountDeletedStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        OfficialAccountDeletedCallbackStore = null;
        OfficialAccountDeletedStringCallbackStore = null;
      }

      if (OfficialAccountDeletedCallbackStore == null && OfficialAccountDeletedStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetOfficialAccountDeletedCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置公众号信息变更通知回调
    /// Set official account info changed callback
    /// </summary>
    /// <param name="callback">回调 OfficialAccountInfoChangedCallback</param>
    /// <param name="stringCallback">回调 OfficialAccountInfoChangedStringCallback</param>
    public static void SetOfficialAccountInfoChangedCallback(OfficialAccountInfoChangedCallback callback = null, OfficialAccountInfoChangedStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        OfficialAccountInfoChangedCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        OfficialAccountInfoChangedStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveOfficialAccountInfoChangedCallback();
        return;
      }

      IMNativeSDK.TIMSetOfficialAccountInfoChangedCallback(TIMOfficialAccountInfoChangedCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除公众号信息变更通知回调
    /// Remove official account info changed callback
    /// </summary>
    /// <param name="callback">回调 OfficialAccountInfoChangedCallback</param>
    /// <param name="stringCallback">回调 OfficialAccountInfoChangedStringCallback</param>
    public static void RemoveOfficialAccountInfoChangedCallback(OfficialAccountInfoChangedCallback callback = null, OfficialAccountInfoChangedStringCallback stringCallback = null)
    {
      if (callback != null && OfficialAccountInfoChangedCallbackStore != null)
      {
        OfficialAccountInfoChangedCallbackStore -= callback;
      }

      if (stringCallback != null && OfficialAccountInfoChangedStringCallbackStore != null)
      {
        OfficialAccountInfoChangedStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        OfficialAccountInfoChangedCallbackStore = null;
        OfficialAccountInfoChangedStringCallbackStore = null;
      }

      if (OfficialAccountInfoChangedCallbackStore == null && OfficialAccountInfoChangedStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetOfficialAccountInfoChangedCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置话题创建通知回调
    /// Set topic created callback
    /// </summary>
    /// <param name="callback">回调 CommunityCreateTopicCallback</param>
    /// <param name="stringCallback">回调 CommunityCreateTopicStringCallback</param>
    public static void SetCommunityCreateTopicCallback(CommunityCreateTopicCallback callback = null, CommunityCreateTopicStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        CommunityCreateTopicCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        CommunityCreateTopicStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveCommunityCreateTopicCallback();
        return;
      }

      IMNativeSDK.TIMSetCommunityCreateTopicCallback(TIMCommunityCreateTopicCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除话题创建通知回调
    /// Remove topic created callback
    /// </summary>
    /// <param name="callback">回调 CommunityCreateTopicCallback</param>
    /// <param name="stringCallback">回调 CommunityCreateTopicStringCallback</param>
    public static void RemoveCommunityCreateTopicCallback(CommunityCreateTopicCallback callback = null, CommunityCreateTopicStringCallback stringCallback = null)
    {
      if (callback != null && CommunityCreateTopicCallbackStore != null)
      {
        CommunityCreateTopicCallbackStore -= callback;
      }

      if (stringCallback != null && CommunityCreateTopicStringCallbackStore != null)
      {
        CommunityCreateTopicStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        CommunityCreateTopicCallbackStore = null;
        CommunityCreateTopicStringCallbackStore = null;
      }

      if (CommunityCreateTopicCallbackStore == null && CommunityCreateTopicStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetCommunityCreateTopicCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置话题删除通知回调
    /// Set topic deleted callback
    /// </summary>
    /// <param name="callback">回调 CommunityDeleteTopicCallback</param>
    /// <param name="stringCallback">回调 CommunityDeleteTopicStringCallback</param>
    public static void SetCommunityDeleteTopicCallback(CommunityDeleteTopicCallback callback = null, CommunityDeleteTopicStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        CommunityDeleteTopicCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        CommunityDeleteTopicStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveCommunityDeleteTopicCallback();
        return;
      }

      IMNativeSDK.TIMSetCommunityDeleteTopicCallback(TIMCommunityDeleteTopicCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除话题删除通知回调
    /// Remove topic deleted callback
    /// </summary>
    /// <param name="callback">回调 CommunityDeleteTopicCallback</param>
    /// <param name="stringCallback">回调 CommunityDeleteTopicStringCallback</param>
    public static void RemoveCommunityDeleteTopicCallback(CommunityDeleteTopicCallback callback = null, CommunityDeleteTopicStringCallback stringCallback = null)
    {
      if (callback != null && CommunityDeleteTopicCallbackStore != null)
      {
        CommunityDeleteTopicCallbackStore -= callback;
      }

      if (stringCallback != null && CommunityDeleteTopicStringCallbackStore != null)
      {
        CommunityDeleteTopicStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        CommunityDeleteTopicCallbackStore = null;
        CommunityDeleteTopicStringCallbackStore = null;
      }

      if (CommunityDeleteTopicCallbackStore == null && CommunityDeleteTopicStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetCommunityDeleteTopicCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置话题信息变更通知回调
    /// Set topic info changed callback
    /// </summary>
    /// <param name="callback">回调 CommunityChangeTopicInfoCallback</param>
    /// <param name="stringCallback">回调 CommunityChangeTopicInfoStringCallback</param>
    public static void SetCommunityChangeTopicInfoCallback(CommunityChangeTopicInfoCallback callback = null, CommunityChangeTopicInfoStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        CommunityChangeTopicInfoCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        CommunityChangeTopicInfoStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveCommunityChangeTopicInfoCallback();
        return;
      }

      IMNativeSDK.TIMSetCommunityChangeTopicInfoCallback(TIMCommunityChangeTopicInfoCallbackInstance, Utils.string2intptr(user_data));
    }


    /// <summary>
    /// 移除话题信息变更通知回调
    /// Remove topic info changed callback
    /// </summary>
    /// <param name="callback">回调 CommunityChangeTopicInfoCallback</param>
    /// <param name="stringCallback">回调 CommunityChangeTopicInfoStringCallback</param>
    public static void RemoveCommunityChangeTopicInfoCallback(CommunityChangeTopicInfoCallback callback = null, CommunityChangeTopicInfoStringCallback stringCallback = null)
    {
      if (callback != null && CommunityChangeTopicInfoCallbackStore != null)
      {
        CommunityChangeTopicInfoCallbackStore -= callback;
      }

      if (stringCallback != null && CommunityChangeTopicInfoStringCallbackStore != null)
      {
        CommunityChangeTopicInfoStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        CommunityChangeTopicInfoCallbackStore = null;
        CommunityChangeTopicInfoStringCallbackStore = null;
      }

      if (CommunityChangeTopicInfoCallbackStore == null && CommunityChangeTopicInfoStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetCommunityChangeTopicInfoCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置话题自定义系统通知回调
    /// Set topic custom system notification callback
    /// </summary>
    /// <param name="callback">回调 CommunityReceiveTopicRESTCustomDataCallback</param>
    /// <param name="stringCallback">回调 CommunityReceiveTopicRESTCustomDataStringCallback</param>
    public static void SetCommunityReceiveTopicRESTCustomDataCallback(CommunityReceiveTopicRESTCustomDataCallback callback = null, CommunityReceiveTopicRESTCustomDataStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        CommunityReceiveTopicRESTCustomDataCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        CommunityReceiveTopicRESTCustomDataStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveCommunityReceiveTopicRESTCustomDataCallback();
        return;
      }

      IMNativeSDK.TIMSetCommunityReceiveTopicRESTCustomDataCallback(TIMCommunityReceiveTopicRESTCustomDataCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除话题自定义系统通知回调
    /// Remove topic custom system notification callback
    /// </summary>
    /// <param name="callback">回调 CommunityReceiveTopicRESTCustomDataCallback</param>
    /// <param name="stringCallback">回调 CommunityReceiveTopicRESTCustomDataStringCallback</param>
    public static void RemoveCommunityReceiveTopicRESTCustomDataCallback(CommunityReceiveTopicRESTCustomDataCallback callback = null, CommunityReceiveTopicRESTCustomDataStringCallback stringCallback = null)
    {
      if (callback != null && CommunityReceiveTopicRESTCustomDataCallbackStore != null)
      {
        CommunityReceiveTopicRESTCustomDataCallbackStore -= callback;
      }

      if (stringCallback != null && CommunityReceiveTopicRESTCustomDataStringCallbackStore != null)
      {
        CommunityReceiveTopicRESTCustomDataStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        CommunityReceiveTopicRESTCustomDataCallbackStore = null;
        CommunityReceiveTopicRESTCustomDataStringCallbackStore = null;
      }

      if (CommunityReceiveTopicRESTCustomDataCallbackStore == null && CommunityReceiveTopicRESTCustomDataStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetCommunityReceiveTopicRESTCustomDataCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置权限组创建通知回调
    /// Set permission group created callback
    /// </summary>
    /// <param name="callback">回调 CommunityCreatePermissionGroupCallback</param>
    /// <param name="stringCallback">回调 CommunityCreatePermissionGroupStringCallback</param>
    public static void SetCommunityCreatePermissionGroupCallback(CommunityCreatePermissionGroupCallback callback = null, CommunityCreatePermissionGroupStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        CommunityCreatePermissionGroupCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        CommunityCreatePermissionGroupStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveCommunityCreatePermissionGroupCallback();
        return;
      }

      IMNativeSDK.TIMSetCommunityCreatePermissionGroupCallback(TIMCommunityCreatePermissionGroupCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除权限组创建通知回调
    /// Remove permission group created callback
    /// </summary>
    /// <param name="callback">回调 CommunityCreatePermissionGroupCallback</param>
    /// <param name="stringCallback">回调 CommunityCreatePermissionGroupStringCallback</param>
    public static void RemoveCommunityCreatePermissionGroupCallback(CommunityCreatePermissionGroupCallback callback = null, CommunityCreatePermissionGroupStringCallback stringCallback = null)
    {
      if (callback != null && CommunityCreatePermissionGroupCallbackStore != null)
      {
        CommunityCreatePermissionGroupCallbackStore -= callback;
      }

      if (stringCallback != null && CommunityCreatePermissionGroupStringCallbackStore != null)
      {
        CommunityCreatePermissionGroupStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        CommunityCreatePermissionGroupCallbackStore = null;
        CommunityCreatePermissionGroupStringCallbackStore = null;
      }

      if (CommunityCreatePermissionGroupCallbackStore == null && CommunityCreatePermissionGroupStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetCommunityCreatePermissionGroupCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置权限组删除通知回调
    /// Set permission group deleted callback
    /// </summary>
    /// <param name="callback">回调 CommunityDeletePermissionGroupCallback</param>
    /// <param name="stringCallback">回调 CommunityDeletePermissionGroupStringCallback</param>
    public static void SetCommunityDeletePermissionGroupCallback(CommunityDeletePermissionGroupCallback callback = null, CommunityDeletePermissionGroupStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        CommunityDeletePermissionGroupCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        CommunityDeletePermissionGroupStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveCommunityDeletePermissionGroupCallback();
        return;
      }

      IMNativeSDK.TIMSetCommunityDeletePermissionGroupCallback(TIMCommunityDeletePermissionGroupCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除权限组删除通知回调
    /// Remove permission group deleted callback
    /// </summary>
    /// <param name="callback">回调 CommunityDeletePermissionGroupCallback</param>
    /// <param name="stringCallback">回调 CommunityDeletePermissionGroupStringCallback</param>
    public static void RemoveCommunityDeletePermissionGroupCallback(CommunityDeletePermissionGroupCallback callback = null, CommunityDeletePermissionGroupStringCallback stringCallback = null)
    {
      if (callback != null && CommunityDeletePermissionGroupCallbackStore != null)
      {
        CommunityDeletePermissionGroupCallbackStore -= callback;
      }

      if (stringCallback != null && CommunityDeletePermissionGroupStringCallbackStore != null)
      {
        CommunityDeletePermissionGroupStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        CommunityDeletePermissionGroupCallbackStore = null;
        CommunityDeletePermissionGroupStringCallbackStore = null;
      }

      if (CommunityDeletePermissionGroupCallbackStore == null && CommunityDeletePermissionGroupStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetCommunityDeletePermissionGroupCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置权限组变更通知回调
    /// Set permission group info changed callback
    /// </summary>
    /// <param name="callback">回调 CommunityChangePermissionGroupInfoCallback</param>
    /// <param name="stringCallback">回调 CommunityChangePermissionGroupInfoStringCallback</param>
    public static void SetCommunityChangePermissionGroupInfoCallback(CommunityChangePermissionGroupInfoCallback callback = null, CommunityChangePermissionGroupInfoStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        CommunityChangePermissionGroupInfoCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        CommunityChangePermissionGroupInfoStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveCommunityChangePermissionGroupInfoCallback();
        return;
      }

      IMNativeSDK.TIMSetCommunityChangePermissionGroupInfoCallback(TIMCommunityChangePermissionGroupInfoCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除权限组变更通知回调
    /// Remove permission group info changed callback
    /// </summary>
    /// <param name="callback">回调 CommunityChangePermissionGroupInfoCallback</param>
    /// <param name="stringCallback">回调 CommunityChangePermissionGroupInfoStringCallback</param>
    public static void RemoveCommunityChangePermissionGroupInfoCallback(CommunityChangePermissionGroupInfoCallback callback = null, CommunityChangePermissionGroupInfoStringCallback stringCallback = null)
    {
      if (callback != null && CommunityChangePermissionGroupInfoCallbackStore != null)
      {
        CommunityChangePermissionGroupInfoCallbackStore -= callback;
      }

      if (stringCallback != null && CommunityChangePermissionGroupInfoStringCallbackStore != null)
      {
        CommunityChangePermissionGroupInfoStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        CommunityChangePermissionGroupInfoCallbackStore = null;
        CommunityChangePermissionGroupInfoStringCallbackStore = null;
      }

      if (CommunityChangePermissionGroupInfoCallbackStore == null && CommunityChangePermissionGroupInfoStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetCommunityChangePermissionGroupInfoCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置向权限组添加成员通知回调
    /// Set callback for adding members to a permission group
    /// </summary>
    /// <param name="callback">回调 CommunityAddMembersToPermissionGroupCallback</param>
    /// <param name="stringCallback">回调 CommunityAddMembersToPermissionGroupStringCallback</param>
    public static void SetCommunityAddMembersToPermissionGroupCallback(CommunityAddMembersToPermissionGroupCallback callback = null, CommunityAddMembersToPermissionGroupStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        CommunityAddMembersToPermissionGroupCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        CommunityAddMembersToPermissionGroupStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveCommunityAddMembersToPermissionGroupCallback();
        return;
      }

      IMNativeSDK.TIMSetCommunityAddMembersToPermissionGroupCallback(TIMCommunityAddMembersToPermissionGroupCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除向权限组添加成员通知回调
    /// Remove callback for adding members to a permission group
    /// </summary>
    /// <param name="callback">回调 CommunityAddMembersToPermissionGroupCallback</param>
    /// <param name="stringCallback">回调 CommunityAddMembersToPermissionGroupStringCallback</param>
    public static void RemoveCommunityAddMembersToPermissionGroupCallback(CommunityAddMembersToPermissionGroupCallback callback = null, CommunityAddMembersToPermissionGroupStringCallback stringCallback = null)
    {
      if (callback != null && CommunityAddMembersToPermissionGroupCallbackStore != null)
      {
        CommunityAddMembersToPermissionGroupCallbackStore -= callback;
      }

      if (stringCallback != null && CommunityAddMembersToPermissionGroupStringCallbackStore != null)
      {
        CommunityAddMembersToPermissionGroupStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        CommunityAddMembersToPermissionGroupCallbackStore = null;
        CommunityAddMembersToPermissionGroupStringCallbackStore = null;
      }

      if (CommunityAddMembersToPermissionGroupCallbackStore == null && CommunityAddMembersToPermissionGroupStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetCommunityAddMembersToPermissionGroupCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置从权限组删除成员通知回调
    /// Set callback for deleting members from a permission group
    /// </summary>
    /// <param name="callback">回调 CommunityRemoveMembersFromPermissionGroupCallback</param>
    /// <param name="stringCallback">回调 CommunityRemoveMembersFromPermissionGroupStringCallback</param>
    public static void SetCommunityRemoveMembersFromPermissionGroupCallback(CommunityRemoveMembersFromPermissionGroupCallback callback = null, CommunityRemoveMembersFromPermissionGroupStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        CommunityRemoveMembersFromPermissionGroupCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        CommunityRemoveMembersFromPermissionGroupStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveCommunityRemoveMembersFromPermissionGroupCallback();
        return;
      }

      IMNativeSDK.TIMSetCommunityRemoveMembersFromPermissionGroupCallback(TIMCommunityRemoveMembersFromPermissionGroupCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除从权限组删除成员通知回调
    /// Remove callback for deleting members from a permission group
    /// </summary>
    /// <param name="callback">回调 CommunityRemoveMembersFromPermissionGroupCallback</param>
    /// <param name="stringCallback">回调 CommunityRemoveMembersFromPermissionGroupStringCallback</param>
    public static void RemoveCommunityRemoveMembersFromPermissionGroupCallback(CommunityRemoveMembersFromPermissionGroupCallback callback = null, CommunityRemoveMembersFromPermissionGroupStringCallback stringCallback = null)
    {
      if (callback != null && CommunityRemoveMembersFromPermissionGroupCallbackStore != null)
      {
        CommunityRemoveMembersFromPermissionGroupCallbackStore -= callback;
      }

      if (stringCallback != null && CommunityRemoveMembersFromPermissionGroupStringCallbackStore != null)
      {
        CommunityRemoveMembersFromPermissionGroupStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        CommunityRemoveMembersFromPermissionGroupCallbackStore = null;
        CommunityRemoveMembersFromPermissionGroupStringCallbackStore = null;
      }

      if (CommunityRemoveMembersFromPermissionGroupCallbackStore == null && CommunityRemoveMembersFromPermissionGroupStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetCommunityRemoveMembersFromPermissionGroupCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置向权限组中增加话题权限通知回调
    /// Set callback for adding a topic permission to a permission group
    /// </summary>
    /// <param name="callback">回调 CommunityAddTopicPermissionCallback</param>
    /// <param name="stringCallback">回调 CommunityAddTopicPermissionStringCallback</param>
    public static void SetCommunityAddTopicPermissionCallback(CommunityAddTopicPermissionCallback callback = null, CommunityAddTopicPermissionStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        CommunityAddTopicPermissionCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        CommunityAddTopicPermissionStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveCommunityAddTopicPermissionCallback();
        return;
      }

      IMNativeSDK.TIMSetCommunityAddTopicPermissionCallback(TIMCommunityAddTopicPermissionCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除向权限组中增加话题权限通知回调
    /// Remove callback for adding a topic permission to a permission group
    /// </summary>
    /// <param name="callback">回调 CommunityAddTopicPermissionCallback</param>
    /// <param name="stringCallback">回调 CommunityAddTopicPermissionStringCallback</param>
    public static void RemoveCommunityAddTopicPermissionCallback(CommunityAddTopicPermissionCallback callback = null, CommunityAddTopicPermissionStringCallback stringCallback = null)
    {
      if (callback != null && CommunityAddTopicPermissionCallbackStore != null)
      {
        CommunityAddTopicPermissionCallbackStore -= callback;
      }

      if (stringCallback != null && CommunityAddTopicPermissionStringCallbackStore != null)
      {
        CommunityAddTopicPermissionStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        CommunityAddTopicPermissionCallbackStore = null;
        CommunityAddTopicPermissionStringCallbackStore = null;
      }

      if (CommunityAddTopicPermissionCallbackStore == null && CommunityAddTopicPermissionStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetCommunityAddTopicPermissionCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置从权限组中删除话题权限通知回调
    /// Set callback for deleting a topic permission from a permission group
    /// </summary>
    /// <param name="callback">回调 CommunityDeleteTopicPermissionCallback</param>
    /// <param name="stringCallback">回调 CommunityDeleteTopicPermissionStringCallback</param>
    public static void SetCommunityDeleteTopicPermissionCallback(CommunityDeleteTopicPermissionCallback callback = null, CommunityDeleteTopicPermissionStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        CommunityDeleteTopicPermissionCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        CommunityDeleteTopicPermissionStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveCommunityDeleteTopicPermissionCallback();
        return;
      }

      IMNativeSDK.TIMSetCommunityDeleteTopicPermissionCallback(TIMCommunityDeleteTopicPermissionCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除从权限组中删除话题权限通知回调
    /// Remove callback for deleting a topic permission from a permission group
    /// </summary>
    /// <param name="callback">回调 CommunityDeleteTopicPermissionCallback</param>
    /// <param name="stringCallback">回调 CommunityDeleteTopicPermissionStringCallback</param>
    public static void RemoveCommunityDeleteTopicPermissionCallback(CommunityDeleteTopicPermissionCallback callback = null, CommunityDeleteTopicPermissionStringCallback stringCallback = null)
    {
      if (callback != null && CommunityDeleteTopicPermissionCallbackStore != null)
      {
        CommunityDeleteTopicPermissionCallbackStore -= callback;
      }

      if (stringCallback != null && CommunityDeleteTopicPermissionStringCallbackStore != null)
      {
        CommunityDeleteTopicPermissionStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        CommunityDeleteTopicPermissionCallbackStore = null;
        CommunityDeleteTopicPermissionStringCallbackStore = null;
      }

      if (CommunityDeleteTopicPermissionCallbackStore == null && CommunityDeleteTopicPermissionStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetCommunityDeleteTopicPermissionCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置修改话题权限通知回调
    /// Set callback for modifying a topic permission
    /// </summary>
    /// <param name="callback">回调 CommunityModifyTopicPermissionCallback</param>
    /// <param name="stringCallback">回调 CommunityModifyTopicPermissionStringCallback</param>
    public static void SetCommunityModifyTopicPermissionCallback(CommunityModifyTopicPermissionCallback callback = null, CommunityModifyTopicPermissionStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        CommunityModifyTopicPermissionCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        CommunityModifyTopicPermissionStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveCommunityModifyTopicPermissionCallback();
        return;
      }

      IMNativeSDK.TIMSetCommunityModifyTopicPermissionCallback(TIMCommunityModifyTopicPermissionCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除修改话题权限通知回调
    /// Remove callback for modifying a topic permission
    /// </summary>
    /// <param name="callback">回调 CommunityModifyTopicPermissionCallback</param>
    /// <param name="stringCallback">回调 CommunityModifyTopicPermissionStringCallback</param>
    public static void RemoveCommunityModifyTopicPermissionCallback(CommunityModifyTopicPermissionCallback callback = null, CommunityModifyTopicPermissionStringCallback stringCallback = null)
    {
      if (callback != null && CommunityModifyTopicPermissionCallbackStore != null)
      {
        CommunityModifyTopicPermissionCallbackStore -= callback;
      }

      if (stringCallback != null && CommunityModifyTopicPermissionStringCallbackStore != null)
      {
        CommunityModifyTopicPermissionStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        CommunityModifyTopicPermissionCallbackStore = null;
        CommunityModifyTopicPermissionStringCallbackStore = null;
      }

      if (CommunityModifyTopicPermissionCallbackStore == null && CommunityModifyTopicPermissionStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetCommunityModifyTopicPermissionCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// 设置实验性通知回调
    /// Set callback for experimental notifications
    /// </summary>
    /// <param name="callback">回调 ExperimentalNotifyCallback</param>
    public static void SetExperimentalNotifyCallback(ExperimentalNotifyCallback callback = null, ExperimentalNotifyStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      if (callback != null)
      {
        ExperimentalNotifyCallbackStore += callback;
      }

      if (stringCallback != null)
      {
        ExperimentalNotifyStringCallbackStore += stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        RemoveExperimentalNotifyCallback();
        return;
      }

      IMNativeSDK.TIMSetExperimentalNotifyCallback(TIMExperimentalNotifyCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// 移除实验性通知回调
    /// Remove callback for experimental notifications
    /// </summary>
    /// <param name="callback">回调 ExperimentalNotifyCallback</param>
    public static void RemoveExperimentalNotifyCallback(ExperimentalNotifyCallback callback = null, ExperimentalNotifyStringCallback stringCallback = null)
    {
      if (callback != null && ExperimentalNotifyCallbackStore != null)
      {
        ExperimentalNotifyCallbackStore -= callback;
      }

      if (stringCallback != null && ExperimentalNotifyStringCallbackStore != null)
      {
        ExperimentalNotifyStringCallbackStore -= stringCallback;
      }

      if (callback == null && stringCallback == null)
      {
        ExperimentalNotifyCallbackStore = null;
        ExperimentalNotifyStringCallbackStore = null;
      }

      if (ExperimentalNotifyCallbackStore == null && ExperimentalNotifyStringCallbackStore == null)
      {
        IMNativeSDK.TIMSetExperimentalNotifyCallback(null, Utils.string2intptr(""));
      }
    }



    [MonoPInvokeCallback(typeof(IMNativeSDK.CommonValueCallback))]
    private static void ValueCallbackInstance(int code, IntPtr desc, IntPtr json_param, IntPtr user_data)
    {
      string user_data_string = Utils.intptr2string(user_data);
      string desc_string = Utils.intptr2string(desc);
      string param = Utils.intptr2string(json_param);
      // Debug.Log("code: " + code + " desc: " + desc + " desc_string: " + desc_string + " json_param: " + json_param + " json_param_string: " + param + " user_data: " + user_data + " user_data_string: " + user_data_string);
      if (ValuecallbackDeleStore.TryGetValue(user_data_string, out SendOrPostCallback dele))
      {
        mainSyncContext.Post(dele, new CallbackConvert { code = code, type = "ValueCallback", data = param, user_data = user_data_string, desc = desc_string });
      }
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.CommonValueCallback))]
    private static void StringValueCallbackInstance(int code, IntPtr desc, IntPtr json_param, IntPtr user_data)
    {
      string user_data_string = Utils.intptr2string(user_data);
      string desc_string = Utils.intptr2string(desc);
      string param = Utils.intptr2string(json_param);
      mainSyncContext.Post(threadOperation<string>, new CallbackConvert { code = code, type = "ValueCallback", data = param, user_data = user_data_string, desc = desc_string });
    }
    static private void threadOperation<T>(object obj)
    {
      CallbackConvert data = (CallbackConvert)obj;
      try
      {
        switch (data.type)
        {
          case "ValueCallback":
            if (ValuecallbackStore.ContainsKey(data.user_data))
            {
              Utils.Log("string value callback contains key " + data.user_data);
              bool downloadElemFlag = false;
              int index = data.user_data.LastIndexOf("_downloadElem");
              if (index != -1)
              {
                downloadElemFlag = true;
              }
              if (ValuecallbackStore.TryGetValue(data.user_data, out Delegate callbackDele))
              {
                Utils.Log("try get value " + downloadElemFlag);
                // 3 means no need for callback data
                if (callbackDele.GetMethodInfo().GetParameters().Length == 3)
                {
                  callbackDele.DynamicInvoke(data.code, data.desc, data.user_data);
                }
                else
                {
                  var isFoundDele = false;
                  if (!(downloadElemFlag && data.desc == "downloading"))
                  {
                    isFoundDele = ValuecallbackDeleStore.Remove(data.user_data);
                  }
                  else
                  {
                    isFoundDele = ValuecallbackDeleStore.ContainsKey(data.user_data);
                  }

                  // Debug.Log($"data.user_data: {data.user_data} typeof(T): {typeof(T).Name} FoundDele: {isFoundDele}");
                  Utils.Log("isfounddel " + isFoundDele + " " + data.user_data);
                  if (isFoundDele)
                  {
                    callbackDele.DynamicInvoke(data.code, data.desc, Utils.FromJson<T>(data.data), data.user_data);
                  }
                  else
                  {
                    callbackDele.DynamicInvoke(data.code, data.desc, data.data, data.user_data);
                  }
                }
                if (!(downloadElemFlag && data.desc == "downloading"))
                {
                  ValuecallbackStore.Remove(data.user_data);
                }

              }

            }

            break;
          case "TIMRecvNewMsgCallback":
            if (RecvNewMsgCallbackStore != null)
            {
              RecvNewMsgCallbackStore(Utils.FromJson<List<Message>>(data.data), data.user_data);
            }

            if (RecvNewMsgStringCallbackStore != null)
            {
              RecvNewMsgStringCallbackStore(data.data, data.user_data);
            }

            break;
          case "TIMMsgReactionsChangedCallback":
            if (MsgReactionsChangedCallbackStore != null)
            {
              MsgReactionsChangedCallbackStore(Utils.FromJson<List<MessageReactionChangeInfo>>(data.data), data.user_data);
            }
            if (MsgReactionsChangedStringCallbackStore != null)
            {
              MsgReactionsChangedStringCallbackStore(data.data, data.user_data);
            }
            break;
          case "TIMMsgAllMessageReceiveOptionCallback":
            if (MsgAllMessageReceiveOptionCallbackStore != null)
            {
              MsgAllMessageReceiveOptionCallbackStore(Utils.FromJson<ReceiveMessageOptInfo>(data.data), data.user_data);
            }
            if (MsgAllMessageReceiveOptionStringCallbackStore != null)
            {
              MsgAllMessageReceiveOptionStringCallbackStore(data.data, data.user_data);
            }
            break;
          case "TIMMsgReadedReceiptCallback":
            if (MsgReadedReceiptCallbackStore != null)
            {
              MsgReadedReceiptCallbackStore(Utils.FromJson<List<MessageReceipt>>(data.data), data.user_data);
            }

            if (MsgReadedReceiptStringCallbackStore != null)
            {
              MsgReadedReceiptStringCallbackStore(data.data, data.user_data);
            }

            break;

          case "TIMMsgRevokeCallback":
            if (MsgRevokeCallbackStore != null)
            {
              MsgRevokeCallbackStore(Utils.FromJson<List<MsgLocator>>(data.data), data.user_data);
            }

            if (MsgRevokeStringCallbackStore != null)
            {
              MsgRevokeStringCallbackStore(data.data, data.user_data);
            }

            break;

          case "TIMMsgElemUploadProgressCallback":
            if (MsgElemUploadProgressCallbackStore != null)
            {
              MsgElemUploadProgressCallbackStore(Utils.FromJson<Message>(data.data), data.index, data.cur_size, data.total_size, data.user_data);
            }

            if (MsgElemUploadProgressStringCallbackStore != null)
            {
              MsgElemUploadProgressStringCallbackStore(data.data, data.index, data.cur_size, data.total_size, data.user_data);
            }

            break;

          case "TIMGroupTipsEventCallback":
            if (GroupTipsEventCallbackStore != null)
            {
              GroupTipsEventCallbackStore(Utils.FromJson<GroupTipsElem>(data.data), data.user_data);
            }

            if (GroupTipsEventStringCallbackStore != null)
            {
              GroupTipsEventStringCallbackStore(data.data, data.user_data);
            }

            break;

          case "TIMGroupAttributeChangedCallback":
            if (GroupAttributeChangedCallbackStore != null)
            {
              GroupAttributeChangedCallbackStore(data.group_id, Utils.FromJson<List<GroupAttributes>>(data.data), data.user_data);
            }

            if (GroupAttributeChangedStringCallbackStore != null)
            {
              GroupAttributeChangedStringCallbackStore(data.group_id, data.data, data.user_data);
            }

            break;

          case "TIMGroupCounterChangedCallbackInstance":
            if (GroupCounterChangedCallbackStore != null)
            {
              GroupCounterChangedCallbackStore(data.group_id, data.data, data.next_seq, data.user_data);
            }

            break;

          case "TIMConvEventCallback":
            if (ConvEventCallbackStore != null)
            {
              ConvEventCallbackStore((TIMConvEvent)data.conv_event, data.data.ToString(), data.user_data);

            }

            if (ConvEventStringCallbackStore != null)
            {
              ConvEventStringCallbackStore((TIMConvEvent)data.conv_event, data.data, data.user_data);
            }

            break;

          case "TIMConvTotalUnreadMessageCountChangedCallback":

            if (ConvTotalUnreadMessageCountChangedCallbackStore != null)
            {
              ConvTotalUnreadMessageCountChangedCallbackStore(Int32.Parse(data.data), data.user_data);
            }

            break;

          case "TIMConvUnreadMessageCountChangedByFilterCallback":

            if (ConvUnreadMessageCountChangedByFilterCallbackStore != null)
            {
              ConvUnreadMessageCountChangedByFilterCallbackStore(Utils.FromJson<ConversationListFilter>(data.data), data.total_size, data.user_data);
            }

            if (ConvUnreadMessageCountChangedByFilterStringCallbackStore != null)
            {
              ConvUnreadMessageCountChangedByFilterStringCallbackStore(data.data, data.total_size, data.user_data);
            }

            break;

          case "TIMSetConvConversationGroupCreatedCallback":
            if (ConvConversationGroupCreatedCallbackStore != null)
            {
              ConvConversationGroupCreatedCallbackStore(data.group_name, Utils.FromJson<List<string>>(data.data), data.user_data);
            }

            if (ConvConversationGroupCreatedStringCallbackStore != null)
            {
              ConvConversationGroupCreatedStringCallbackStore(data.group_name, data.data, data.user_data);
            }

            break;

          case "TIMSetConvConversationGroupDeletedCallback":

            if (ConvConversationGroupDeletedCallbackStore != null)
            {
              ConvConversationGroupDeletedCallbackStore(data.data, data.user_data);
            }

            break;

          case "TIMSetConvConversationGroupNameChangedCallback":

            if (ConvConversationGroupNameChangedCallbackStore != null)
            {
              ConvConversationGroupNameChangedCallbackStore(data.group_name, data.data, data.user_data);
            }

            break;

          case "TIMSetConvConversationsAddedToGroupCallback":
            if (ConvConversationsAddedToGroupCallbackStore != null)
            {
              ConvConversationsAddedToGroupCallbackStore(data.group_name, Utils.FromJson<List<string>>(data.data), data.user_data);
            }

            if (ConvConversationsAddedToGroupStringCallbackStore != null)
            {
              ConvConversationsAddedToGroupStringCallbackStore(data.group_name, data.data, data.user_data);
            }

            break;

          case "TIMSetConvConversationsDeletedFromGroupCallback":
            if (ConvConversationsDeletedFromGroupCallbackStore != null)
            {
              ConvConversationsDeletedFromGroupCallbackStore(data.group_name, Utils.FromJson<List<string>>(data.data), data.user_data);
            }

            if (ConvConversationsDeletedFromGroupStringCallbackStore != null)
            {
              ConvConversationsDeletedFromGroupStringCallbackStore(data.group_name, data.data, data.user_data);
            }

            break;

          case "TIMNetworkStatusListenerCallback":

            if (NetworkStatusListenerCallbackStore != null)
            {
              NetworkStatusListenerCallbackStore((TIMNetworkStatus)data.code, data.index, data.desc, data.user_data);
            }

            break;

          case "TIMKickedOfflineCallback":

            if (KickedOfflineCallbackStore != null)
            {
              KickedOfflineCallbackStore(data.user_data);
            }

            break;

          case "TIMUserSigExpiredCallback":

            if (UserSigExpiredCallbackStore != null)
            {
              UserSigExpiredCallbackStore(data.user_data);
            }

            break;

          case "TIMOnAddFriendCallback":

            if (OnAddFriendCallbackStore != null)
            {
              OnAddFriendCallbackStore(Utils.FromJson<List<string>>(data.data), data.user_data);
            }

            if (OnAddFriendStringCallbackStore != null)
            {
              OnAddFriendStringCallbackStore(data.data, data.user_data);
            }

            break;

          case "TIMOnDeleteFriendCallback":
            if (OnDeleteFriendCallbackStore != null)
            {
              OnDeleteFriendCallbackStore(Utils.FromJson<List<string>>(data.data), data.user_data);
            }

            if (OnDeleteFriendStringCallbackStore != null)
            {
              OnDeleteFriendStringCallbackStore(data.data, data.user_data);
            }

            break;

          case "TIMUpdateFriendProfileCallback":
            if (UpdateFriendProfileCallbackStore != null)
            {
              UpdateFriendProfileCallbackStore(Utils.FromJson<List<FriendProfileUpdate>>(data.data), data.user_data);
            }

            if (UpdateFriendProfileStringCallbackStore != null)
            {
              UpdateFriendProfileStringCallbackStore(data.data, data.user_data);
            }

            break;

          case "TIMFriendAddRequestCallback":
            if (FriendAddRequestCallbackStore != null)
            {
              FriendAddRequestCallbackStore(Utils.FromJson<List<FriendAddPendency>>(data.data), data.user_data);
            }

            if (FriendAddRequestStringCallbackStore != null)
            {
              FriendAddRequestStringCallbackStore(data.data, data.user_data);
            }

            break;

          case "TIMFriendApplicationListDeletedCallback":
            if (FriendApplicationListDeletedCallbackStore != null)
            {
              FriendApplicationListDeletedCallbackStore(Utils.FromJson<List<string>>(data.data), data.user_data);
            }

            if (FriendApplicationListDeletedStringCallbackStore != null)
            {
              FriendApplicationListDeletedStringCallbackStore(data.data, data.user_data);
            }

            break;

          case "TIMFriendApplicationListReadCallback":

            if (FriendApplicationListReadCallbackStore != null)
            {
              FriendApplicationListReadCallbackStore(data.user_data);
            }

            break;

          case "TIMFriendBlackListAddedCallback":
            if (FriendBlackListAddedCallbackStore != null)
            {
              FriendBlackListAddedCallbackStore(Utils.FromJson<List<FriendProfile>>(data.data), data.user_data);
            }

            if (FriendBlackListAddedStringCallbackStore != null)
            {
              FriendBlackListAddedStringCallbackStore(data.data, data.user_data);
            }

            break;

          case "TIMFriendBlackListDeletedCallback":
            if (FriendBlackListDeletedCallbackStore != null)
            {
              FriendBlackListDeletedCallbackStore(Utils.FromJson<List<string>>(data.data), data.user_data);
            }

            if (FriendBlackListDeletedStringCallbackStore != null)
            {
              FriendBlackListDeletedStringCallbackStore(data.data, data.user_data);
            }

            break;

          case "TIMLogCallback":

            if (LogCallbackStore != null)
            {
              LogCallbackStore((TIMLogLevel)data.code, data.data, data.user_data);
            }

            break;

          case "TIMMsgUpdateCallback":
            if (MsgUpdateCallbackStore != null)
            {
              MsgUpdateCallbackStore(Utils.FromJson<List<Message>>(data.data), data.user_data);
            }

            if (MsgUpdateStringCallbackStore != null)
            {
              MsgUpdateStringCallbackStore(data.data, data.user_data);
            }

            break;

          case "TIMMsgGroupMessageReadMemberListCallback":
            if (MsgGroupMessageReadMemberListCallbackStore != null)
            {
              MsgGroupMessageReadMemberListCallbackStore(Utils.FromJson<List<GroupMemberInfo>>(data.data), data.next_seq, data.is_finished, data.user_data);
            }

            if (MsgGroupMessageReadMemberListStringCallbackStore != null)
            {
              MsgGroupMessageReadMemberListStringCallbackStore(data.data, data.next_seq, data.is_finished, data.user_data);
            }

            break;

          case "TIMMsgGroupMessageReadMemberListCallbackWeb":
            if (MsgGroupMessageReadMemberListCallbackWebStore != null)
            {
              MsgGroupMessageReadMemberListCallbackWebStore(data.data, data.next_seq.ToString(), data.is_finished, data.user_data);
            }

            break;

          case "TIMGroupTopicCreatedCallback":

            if (GroupTopicCreatedCallbackStore != null)
            {
              GroupTopicCreatedCallbackStore(data.group_id, data.data, data.user_data);
            }

            break;

          case "TIMGroupTopicDeletedCallback":

            if (GroupTopicDeletedCallbackStore != null)
            {
              GroupTopicDeletedCallbackStore(data.group_id, Utils.FromJson<List<string>>(data.data), data.user_data);
            }

            if (GroupTopicDeletedStringCallbackStore != null)
            {
              GroupTopicDeletedStringCallbackStore(data.group_id, data.data, data.user_data);
            }

            break;

          case "TIMGroupTopicChangedCallback":
            if (GroupTopicChangedCallbackStore != null)
            {
              GroupTopicChangedCallbackStore(data.group_id, Utils.FromJson<GroupTopicInfo>(data.data), data.user_data);
            }

            if (GroupTopicChangedStringCallbackStore != null)
            {
              GroupTopicChangedStringCallbackStore(data.group_id, data.data, data.user_data);
            }

            break;

          case "TIMSelfInfoUpdatedCallback":
            if (SelfInfoUpdatedCallbackStore != null)
            {
              SelfInfoUpdatedCallbackStore(Utils.FromJson<UserProfile>(data.data), data.user_data);
            }

            if (SelfInfoUpdatedStringCallbackStore != null)
            {
              SelfInfoUpdatedStringCallbackStore(data.data, data.user_data);
            }

            break;

          case "TIMUserStatusChangedCallback":
            if (UserStatusChangedCallbackStore != null)
            {
              UserStatusChangedCallbackStore(Utils.FromJson<List<UserStatus>>(data.data), data.user_data);
            }

            if (UserStatusChangedStringCallbackStore != null)
            {
              UserStatusChangedStringCallbackStore(data.data, data.user_data);
            }

            break;

          case "TIMUserInfoChangedCallback":
            if (UserInfoChangedCallbackStore != null)
            {
              UserInfoChangedCallbackStore(Utils.FromJson<List<UserProfile>>(data.data), data.user_data);
            }
            if (UserInfoChangedStringCallbackStore != null)
            {
              UserInfoChangedStringCallbackStore(data.data, data.user_data);
            }

            break;

          case "TIMMsgExtensionsChangedCallback":
            if (MsgExtensionsChangedCallbackStore != null)
            {
              MsgExtensionsChangedCallbackStore(data.group_id, Utils.FromJson<List<MessageExtension>>(data.data), data.user_data);
            }

            if (MsgExtensionsChangedStringCallbackStore != null)
            {
              MsgExtensionsChangedStringCallbackStore(data.group_id, data.data, data.user_data);
            }

            break;

          case "TIMMsgExtensionsDeletedCallback":
            if (MsgExtensionsDeletedCallbackStore != null)
            {
              MsgExtensionsDeletedCallbackStore(data.group_id, Utils.FromJson<List<MessageExtension>>(data.data), data.user_data);
            }

            if (MsgExtensionsDeletedStringCallbackStore != null)
            {
              MsgExtensionsDeletedStringCallbackStore(data.group_id, data.data, data.user_data);
            }

            break;
        }
        if (!data.user_data.StartsWith("CallExperimentalAPI") && !data.user_data.StartsWith("SetLogCallback"))
        {
          Log(data.user_data, data.data, "tencent-chat-unity-sdk-res");
        }
      }
      catch (System.Exception error)
      {
        UnityEngine.Debug.LogError(error);
      }

    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMRecvNewMsgCallback))]
    private static void TIMRecvNewMsgCallbackInstance(IntPtr json_msg_array, IntPtr user_data)
    {

      try
      {
        string json_msg_array_string = Utils.intptr2string(json_msg_array);

        string user_data_string = Utils.intptr2string(user_data);
        CallbackConvert cc = new CallbackConvert { code = 0, type = "TIMRecvNewMsgCallback", data = json_msg_array_string, user_data = user_data_string, desc = "" };
        mainSyncContext.Post(threadOperation<object>, cc);
      }
      catch (Exception e)
      {
        Utils.Log("重点关注，回调解析失败" + e.ToString());
      }

    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMMsgReactionsChangedCallback))]
    private static void TIMMsgReactionsChangedCallbackInstance(IntPtr message_reaction_change_info_array, IntPtr user_data)
    {
      try
      {
        string message_reaction_change_info_array_string = Utils.intptr2string(message_reaction_change_info_array);
        string user_data_string = Utils.intptr2string(user_data);
        CallbackConvert cc = new CallbackConvert { code = 0, type = "TIMMsgReactionsChangedCallback", data = message_reaction_change_info_array_string, user_data = user_data_string, desc = "" };
        mainSyncContext.Post(threadOperation<object>, cc);
      }
      catch (Exception e)
      {
        Utils.Log("重点关注，回调解析失败" + e.ToString());
      }
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMMsgAllMessageReceiveOptionCallback))]
    private static void TIMMsgAllMessageReceiveOptionCallbackInstance(IntPtr json_receive_message_option_info, IntPtr user_data)
    {
      try
      {
        string json_receive_message_option_info_string = Utils.intptr2string(json_receive_message_option_info);
        string user_data_string = Utils.intptr2string(user_data);
        CallbackConvert cc = new CallbackConvert { code = 0, type = "TIMMsgAllMessageReceiveOptionCallback", data = json_receive_message_option_info_string, user_data = user_data_string, desc = "" };
        mainSyncContext.Post(threadOperation<object>, cc);
      }
      catch (Exception e)
      {
        Utils.Log("重点关注，回调解析失败" + e.ToString());
      }
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMMsgReadedReceiptCallback))]
    private static void TIMMsgReadedReceiptCallbackInstance(IntPtr json_msg_readed_receipt_array, IntPtr user_data)
    {

      string json_msg_readed_receipt_array_string = Utils.intptr2string(json_msg_readed_receipt_array);

      string user_data_string = Utils.intptr2string(user_data);

      CallbackConvert cc = new CallbackConvert { code = 0, type = "TIMMsgReadedReceiptCallback", data = json_msg_readed_receipt_array_string, user_data = user_data_string, desc = "" };

      mainSyncContext.Post(threadOperation<object>, cc);

    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMMsgRevokeCallback))]
    private static void TIMMsgRevokeCallbackInstance(IntPtr json_msg_locator_array, IntPtr user_data)
    {
      string json_msg_locator_array_string = Utils.intptr2string(json_msg_locator_array);

      string user_data_string = Utils.intptr2string(user_data);

      CallbackConvert cc = new CallbackConvert { code = 0, type = "TIMMsgRevokeCallback", data = json_msg_locator_array_string, user_data = user_data_string, desc = "" };

      mainSyncContext.Post(threadOperation<object>, cc);
    }


    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMMsgElemUploadProgressCallback))]
    private static void TIMMsgElemUploadProgressCallbackInstance(IntPtr json_msg, int index, int cur_size, int total_size, IntPtr user_data)
    {
      string json_msg_string = Utils.intptr2string(json_msg);

      string user_data_string = Utils.intptr2string(user_data);

      CallbackConvert cc = new CallbackConvert { code = 0, type = "TIMMsgElemUploadProgressCallback", data = json_msg_string, user_data = user_data_string, desc = "", index = index, cur_size = cur_size, total_size = total_size };

      mainSyncContext.Post(threadOperation<object>, cc);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMGroupTipsEventCallback))]
    private static void TIMGroupTipsEventCallbackInstance(IntPtr json_group_tip_array, IntPtr user_data)
    {
      string json_group_tip_array_string = Utils.intptr2string(json_group_tip_array);

      string user_data_string = Utils.intptr2string(user_data);

      CallbackConvert cc = new CallbackConvert { code = 0, type = "TIMGroupTipsEventCallback", data = json_group_tip_array_string, user_data = user_data_string, desc = "" };

      mainSyncContext.Post(threadOperation<object>, cc);
    }


    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMGroupAttributeChangedCallback))]
    private static void TIMGroupAttributeChangedCallbackInstance(IntPtr group_id, IntPtr json_group_attribute_array, IntPtr user_data)
    {
      string json_group_attribute_array_string = Utils.intptr2string(json_group_attribute_array);

      string group_id_string = Utils.intptr2string(group_id);

      string user_data_string = Utils.intptr2string(user_data);

      CallbackConvert cc = new CallbackConvert { code = 0, type = "TIMGroupAttributeChangedCallback", data = json_group_attribute_array_string, user_data = user_data_string, group_id = group_id_string };

      mainSyncContext.Post(threadOperation<object>, cc);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMGroupCounterChangedCallback))]
    private static void TIMGroupCounterChangedCallbackInstance(IntPtr group_id, IntPtr group_counter_key, ulong group_counter_new_value, IntPtr user_data)
    {
      string group_id_string = Utils.intptr2string(group_id);
      string group_counter_key_string = Utils.intptr2string(group_counter_key);
      string user_data_string = Utils.intptr2string(user_data);

      mainSyncContext.Post(threadOperation<string>, new CallbackConvert { code = 0, type = "TIMGroupCounterChangedCallbackInstance", data = group_counter_key_string, group_id = group_id_string, next_seq = group_counter_new_value, user_data = user_data_string });
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMConvEventCallback))]
    private static void TIMConvEventCallbackInstance(int conv_event, IntPtr json_conv_array, IntPtr user_data)
    {
      string json_conv_array_string = Utils.intptr2string(json_conv_array);

      string user_data_string = Utils.intptr2string(user_data);

      CallbackConvert cc = new CallbackConvert { code = 0, type = "TIMConvEventCallback", data = json_conv_array_string, user_data = user_data_string, conv_event = conv_event };

      mainSyncContext.Post(threadOperation<object>, cc);
    }


    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMConvTotalUnreadMessageCountChangedCallback))]
    private static void TIMConvTotalUnreadMessageCountChangedCallbackInstance(int total_unread_count, IntPtr user_data)
    {
      string user_data_string = Utils.intptr2string(user_data);
      mainSyncContext.Post(threadOperation<string>, new CallbackConvert { code = 0, type = "TIMConvTotalUnreadMessageCountChangedCallback", data = total_unread_count.ToString(), user_data = user_data_string });
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMConvTotalUnreadMessageCountChangedByFilterCallback))]
    private static void TIMConvUnreadMessageCountChangedByFilterCallbackInstance(IntPtr filter, int total_unread_count, IntPtr user_data)
    {
      string filter_string = Utils.intptr2string(filter);

      string user_data_string = Utils.intptr2string(user_data);

      CallbackConvert cc = new CallbackConvert { code = 0, type = "TIMConvUnreadMessageCountChangedByFilterCallback", data = filter_string, user_data = user_data_string, total_size = total_unread_count };

      mainSyncContext.Post(threadOperation<object>, cc);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMConvConversationGroupCreatedCallback))]
    private static void TIMConvConversationGroupCreatedCallbackInstance(IntPtr group_name, IntPtr conversation_array, IntPtr user_data)
    {
      string group_name_string = Utils.intptr2string(group_name);
      string conversation_array_string = Utils.intptr2string(conversation_array);
      string user_data_string = Utils.intptr2string(user_data);

      CallbackConvert cc = new CallbackConvert { code = 0, type = "TIMSetConvConversationGroupCreatedCallback", data = conversation_array_string, user_data = user_data_string, group_name = group_name_string };

      mainSyncContext.Post(threadOperation<object>, cc);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMConvConversationGroupDeletedCallback))]
    private static void TIMConvConversationGroupDeletedCallbackInstance(IntPtr group_name, IntPtr user_data)
    {
      string group_name_string = Utils.intptr2string(group_name);
      string user_data_string = Utils.intptr2string(user_data);

      mainSyncContext.Post(threadOperation<string>, new CallbackConvert { code = 0, type = "TIMSetConvConversationGroupDeletedCallback", data = group_name_string, user_data = user_data_string });
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMConvConversationGroupNameChangedCallback))]
    private static void TIMConvConversationGroupNameChangedCallbackInstance(IntPtr old_name, IntPtr new_name, IntPtr user_data)
    {
      string old_name_string = Utils.intptr2string(old_name);
      string new_name_string = Utils.intptr2string(new_name);
      string user_data_string = Utils.intptr2string(user_data);

      mainSyncContext.Post(threadOperation<string>, new CallbackConvert { code = 0, type = "TIMSetConvConversationGroupNameChangedCallback", data = new_name_string, user_data = user_data_string, group_name = old_name_string });
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMConvConversationsAddedToGroupCallback))]
    private static void TIMConvConversationsAddedToGroupCallbackInstance(IntPtr group_name, IntPtr conversation_array, IntPtr user_data)
    {
      string group_name_string = Utils.intptr2string(group_name);
      string conversation_array_string = Utils.intptr2string(conversation_array);
      string user_data_string = Utils.intptr2string(user_data);

      CallbackConvert cc = new CallbackConvert { code = 0, type = "TIMSetConvConversationsAddedToGroupCallback", data = conversation_array_string, user_data = user_data_string, group_name = group_name_string };

      mainSyncContext.Post(threadOperation<object>, cc);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMConvConversationsDeletedFromGroupCallback))]
    private static void TIMConvConversationsDeletedFromGroupCallbackInstance(IntPtr group_name, IntPtr conversation_array, IntPtr user_data)
    {
      string group_name_string = Utils.intptr2string(group_name);
      string conversation_array_string = Utils.intptr2string(conversation_array);
      string user_data_string = Utils.intptr2string(user_data);

      CallbackConvert cc = new CallbackConvert { code = 0, type = "TIMSetConvConversationsDeletedFromGroupCallback", data = conversation_array_string, user_data = user_data_string, group_name = group_name_string };

      mainSyncContext.Post(threadOperation<object>, cc);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMNetworkStatusListenerCallback))]
    private static void TIMNetworkStatusListenerCallbackInstance(int status, int code, IntPtr desc, IntPtr user_data)
    {
      string user_data_string = Utils.intptr2string(user_data);
      string desc_string = Utils.intptr2string(desc);
      mainSyncContext.Post(threadOperation<string>, new CallbackConvert { code = status, type = "TIMNetworkStatusListenerCallback", data = "", user_data = user_data_string, desc = desc_string, index = code });
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMKickedOfflineCallback))]
    private static void TIMKickedOfflineCallbackInstance(IntPtr user_data)
    {
      string user_data_string = Utils.intptr2string(user_data);
      mainSyncContext.Post(threadOperation<string>, new CallbackConvert { code = 0, type = "TIMKickedOfflineCallback", data = "", user_data = user_data_string });
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMUserSigExpiredCallback))]
    private static void TIMUserSigExpiredCallbackInstance(IntPtr user_data)
    {
      string user_data_string = Utils.intptr2string(user_data);
      mainSyncContext.Post(threadOperation<string>, new CallbackConvert { code = 0, type = "TIMUserSigExpiredCallback", data = "", user_data = user_data_string });
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMOnAddFriendCallback))]
    private static void TIMOnAddFriendCallbackInstance(IntPtr json_identifier_array, IntPtr user_data)
    {
      string user_data_string = Utils.intptr2string(user_data);

      string json_identifier_array_string = Utils.intptr2string(json_identifier_array);

      CallbackConvert cc = new CallbackConvert { code = 0, type = "TIMOnAddFriendCallback", data = json_identifier_array_string, user_data = user_data_string };

      mainSyncContext.Post(threadOperation<object>, cc);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMOnDeleteFriendCallback))]
    private static void TIMOnDeleteFriendCallbackInstance(IntPtr json_identifier_array, IntPtr user_data)
    {
      // string user_data_string = Utils.intptr2string(user_data);
      string user_data_string = SetOnDeleteFriendCallbackUser_Data;

      string json_identifier_array_string = Utils.intptr2string(json_identifier_array);

      CallbackConvert cc = new CallbackConvert { code = 0, type = "TIMOnDeleteFriendCallback", data = json_identifier_array_string, user_data = user_data_string };

      mainSyncContext.Post(threadOperation<object>, cc);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMUpdateFriendProfileCallback))]
    private static void TIMUpdateFriendProfileCallbackInstance(IntPtr json_friend_profile_update_array, IntPtr user_data)
    {
      string user_data_string = Utils.intptr2string(user_data);

      string json_friend_profile_update_array_string = Utils.intptr2string(json_friend_profile_update_array);

      CallbackConvert cc = new CallbackConvert { code = 0, type = "TIMUpdateFriendProfileCallback", data = json_friend_profile_update_array_string, user_data = user_data_string };

      mainSyncContext.Post(threadOperation<object>, cc);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMFriendAddRequestCallback))]
    private static void TIMFriendAddRequestCallbackInstance(IntPtr json_friend_add_request_pendency_array, IntPtr user_data)
    {
      string user_data_string = Utils.intptr2string(user_data);

      string json_friend_add_request_pendency_array_string = Utils.intptr2string(json_friend_add_request_pendency_array);

      CallbackConvert cc = new CallbackConvert { code = 0, type = "TIMFriendAddRequestCallback", data = json_friend_add_request_pendency_array_string, user_data = user_data_string };

      mainSyncContext.Post(threadOperation<object>, cc);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMFriendApplicationListDeletedCallback))]
    private static void TIMFriendApplicationListDeletedCallbackInstance(IntPtr json_identifier_array, IntPtr user_data)
    {
      string user_data_string = Utils.intptr2string(user_data);

      string json_identifier_array_string = Utils.intptr2string(json_identifier_array);

      CallbackConvert cc = new CallbackConvert { code = 0, type = "TIMFriendApplicationListDeletedCallback", data = json_identifier_array_string, user_data = user_data_string };

      mainSyncContext.Post(threadOperation<object>, cc);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMFriendApplicationListReadCallback))]
    private static void TIMFriendApplicationListReadCallbackInstance(IntPtr user_data)
    {
      string user_data_string = Utils.intptr2string(user_data);
      mainSyncContext.Post(threadOperation<string>, new CallbackConvert { code = 0, type = "TIMFriendApplicationListReadCallback", data = "", user_data = user_data_string });
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMFriendBlackListAddedCallback))]
    private static void TIMFriendBlackListAddedCallbackInstance(IntPtr json_friend_black_added_array, IntPtr user_data)
    {
      string user_data_string = Utils.intptr2string(user_data);

      string json_friend_black_added_array_string = Utils.intptr2string(json_friend_black_added_array);

      CallbackConvert cc = new CallbackConvert { code = 0, type = "TIMFriendBlackListAddedCallback", data = json_friend_black_added_array_string, user_data = user_data_string };

      mainSyncContext.Post(threadOperation<object>, cc);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMFriendBlackListDeletedCallback))]
    private static void TIMFriendBlackListDeletedCallbackInstance(IntPtr json_identifier_array, IntPtr user_data)
    {
      string user_data_string = Utils.intptr2string(user_data);

      string json_identifier_array_string = Utils.intptr2string(json_identifier_array);

      CallbackConvert cc = new CallbackConvert { code = 0, type = "TIMFriendBlackListDeletedCallback", data = json_identifier_array_string, user_data = user_data_string };

      mainSyncContext.Post(threadOperation<object>, cc);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMLogCallback))]
    private static void TIMLogCallbackInstance(int level, IntPtr log, IntPtr user_data)
    {
      string user_data_string = Utils.intptr2string(user_data);
      string log_string = Utils.intptr2string(log);
      mainSyncContext.Post(threadOperation<string>, new CallbackConvert { code = level, type = "TIMLogCallback", data = log_string, user_data = user_data_string });
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMMsgUpdateCallback))]
    public static void TIMMsgUpdateCallbackInstance(IntPtr json_msg_array, IntPtr user_data)
    {
      string user_data_string = Utils.intptr2string(user_data);

      string json_msg_array_string = Utils.intptr2string(json_msg_array);

      CallbackConvert cc = new CallbackConvert { code = 0, type = "TIMMsgUpdateCallback", data = json_msg_array_string, user_data = user_data_string };

      mainSyncContext.Post(threadOperation<object>, cc);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMMsgGroupPinnedMessageChangedCallback))]
    public static void TIMMsgGroupPinnedMessageChangedCallbackInstance(IntPtr group_id, IntPtr json_msg, bool is_pinned, IntPtr op_user, IntPtr user_data)
    {
      string group_id_string = Utils.intptr2string(group_id);
      string json_msg_string = Utils.intptr2string(json_msg);
      string op_user_string = Utils.intptr2string(op_user);
      string user_data_string = Utils.intptr2string(user_data);

      mainSyncContext.Post(_ => {
        MsgGroupPinnedMessageChangedCallbackStore?.Invoke(group_id_string, Utils.FromJson<Message>(json_msg_string), is_pinned, op_user_string, user_data_string);

        MsgGroupPinnedMessageChangedStringCallbackStore?.Invoke(group_id_string, json_msg_string, is_pinned, op_user_string, user_data_string);
      }, null);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMMsgGroupMessageReadMemberListCallback))]
    public static void TIMMsgGroupMessageReadMemberListCallbackInstance(IntPtr json_group_member_array, ulong next_seq, bool is_finished, IntPtr user_data)
    {
      string user_data_string = Utils.intptr2string(user_data);

      string json_group_member_array_string = Utils.intptr2string(json_group_member_array);

      CallbackConvert cc = new CallbackConvert { code = 0, type = "TIMMsgGroupMessageReadMemberListCallback", data = json_group_member_array_string, user_data = user_data_string, next_seq = next_seq, is_finished = is_finished };

      mainSyncContext.Post(threadOperation<object>, cc);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMMsgExtensionsChangedCallback))]
    public static void TIMMsgExtensionsChangedCallbackInstance(IntPtr message_id, IntPtr message_extension_array, IntPtr user_data)
    {
      string user_data_string = Utils.intptr2string(user_data);
      string msg_id = Utils.intptr2string(message_id);
      string message_extensions = Utils.intptr2string(message_extension_array);

      CallbackConvert cc = new CallbackConvert { code = 0, type = "TIMMsgExtensionsChangedCallback", data = message_extensions, user_data = user_data_string, group_id = msg_id };

      mainSyncContext.Post(threadOperation<object>, cc);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMMsgExtensionsDeletedCallback))]
    public static void TIMMsgExtensionsDeletedCallbackInstance(IntPtr message_id, IntPtr message_extension_key_array, IntPtr user_data)
    {
      string user_data_string = Utils.intptr2string(user_data);
      string msg_id = Utils.intptr2string(message_id);
      string message_extensions = Utils.intptr2string(message_extension_key_array);

      CallbackConvert cc = new CallbackConvert { code = 0, type = "TIMMsgExtensionsDeletedCallback", data = message_extensions, user_data = user_data_string, group_id = msg_id };

      mainSyncContext.Post(threadOperation<object>, cc);
    }

    [MonoPInvokeCallback(typeof(IMWebSDK.TIMMsgGroupMessageReadMemberListCallback))]
    public static void TIMMsgGroupMessageReadMemberListCallbackInstanceWeb(IntPtr json_group_member_array, IntPtr next_seq_str, IntPtr is_finished_str, IntPtr user_data)
    {
      string user_data_string = Utils.intptr2string(user_data);
      string json_group_member_array_string = Utils.intptr2string(json_group_member_array);
      ulong next_seq = Convert.ToUInt64(Utils.intptr2string(next_seq_str));
      bool is_finished = Convert.ToBoolean(Utils.intptr2string(is_finished_str));

      CallbackConvert cc = new CallbackConvert { code = 0, type = "TIMMsgGroupMessageReadMemberListCallbackWeb", data = json_group_member_array_string, user_data = user_data_string, next_seq = next_seq, is_finished = is_finished };
      mainSyncContext.Post(threadOperation<string>, cc);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMGroupTopicCreatedCallback))]
    public static void TIMGroupTopicCreatedCallbackInstance(IntPtr group_id, IntPtr topic_id, IntPtr user_data)
    {
      string user_data_string = Utils.intptr2string(user_data);
      string json_group_id_string = Utils.intptr2string(group_id);
      string json_topic_id_string = Utils.intptr2string(topic_id);

      CallbackConvert cc = new CallbackConvert { code = 0, type = "TIMGroupTopicCreatedCallback", data = json_group_id_string, user_data = user_data_string, group_id = json_group_id_string };
      mainSyncContext.Post(threadOperation<string>, cc);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMGroupTopicDeletedCallback))]
    public static void TIMGroupTopicDeletedCallbackInstance(IntPtr group_id, IntPtr topic_id_array, IntPtr user_data)
    {
      string user_data_string = Utils.intptr2string(user_data);
      string json_group_id_string = Utils.intptr2string(group_id);
      string json_topic_id_array_string = Utils.intptr2string(topic_id_array);

      CallbackConvert cc = new CallbackConvert { code = 0, type = "TIMGroupTopicDeletedCallback", data = json_topic_id_array_string, user_data = user_data_string, group_id = json_group_id_string };

      mainSyncContext.Post(threadOperation<object>, cc);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMGroupTopicChangedCallback))]
    public static void TIMGroupTopicChangedCallbackInstance(IntPtr group_id, IntPtr topic_info, IntPtr user_data)
    {
      string user_data_string = Utils.intptr2string(user_data);
      string json_group_id_string = Utils.intptr2string(group_id);
      string json_topic_info_string = Utils.intptr2string(topic_info);

      CallbackConvert cc = new CallbackConvert { code = 0, type = "TIMGroupTopicChangedCallback", data = json_topic_info_string, user_data = user_data_string, group_id = json_group_id_string };

      mainSyncContext.Post(threadOperation<object>, cc);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMSelfInfoUpdatedCallback))]
    public static void TIMSelfInfoUpdatedCallbackInstance(IntPtr json_user_profile, IntPtr user_data)
    {
      string user_data_string = Utils.intptr2string(user_data);
      string json_user_profile_string = Utils.intptr2string(json_user_profile);

      CallbackConvert cc = new CallbackConvert { code = 0, type = "TIMSelfInfoUpdatedCallback", data = json_user_profile_string, user_data = user_data_string };

      mainSyncContext.Post(threadOperation<object>, cc);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMUserStatusChangedCallback))]
    public static void TIMUserStatusChangedCallbackInstance(IntPtr json_user_status_array, IntPtr user_data)
    {
      string user_data_string = Utils.intptr2string(user_data);
      string json_user_status_array_string = Utils.intptr2string(json_user_status_array);

      CallbackConvert cc = new CallbackConvert { code = 0, type = "TIMUserStatusChangedCallback", data = json_user_status_array_string, user_data = user_data_string };

      mainSyncContext.Post(threadOperation<object>, cc);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMUserInfoChangedCallback))]
    public static void TIMUserInfoChangedCallbackInstance(IntPtr json_user_info_array, IntPtr user_data)
    {
      string user_data_string = Utils.intptr2string(user_data);
      string json_user_info_array_string = Utils.intptr2string(json_user_info_array);

      CallbackConvert cc = new CallbackConvert { code = 0, type = "TIMUserInfoChangedCallback", data = json_user_info_array_string, user_data = user_data_string };

      mainSyncContext.Post(threadOperation<object>, cc);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMFriendGroupCreatedCallback))]
    public static void TIMFriendGroupCreatedCallbackInstance(IntPtr group_name, IntPtr json_friend_info_array, IntPtr user_data)
    {
      string group_name_string = Utils.intptr2string(group_name);
      string json_friend_info_array_string = Utils.intptr2string(json_friend_info_array);
      string user_data_string = Utils.intptr2string(user_data);

      mainSyncContext.Post(_ => {
        FriendGroupCreatedCallbackStore?.Invoke(group_name_string, Utils.FromJson<List<FriendProfile>>(json_friend_info_array_string), user_data_string);

        FriendGroupCreatedStringCallbackStore?.Invoke(group_name_string, json_friend_info_array_string, user_data_string);
      }, null);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMFriendGroupDeletedCallback))]
    public static void TIMFriendGroupDeletedCallbackInstance(IntPtr json_group_name_array, IntPtr user_data)
    {
      string json_group_name_array_string = Utils.intptr2string(json_group_name_array);
      string user_data_string = Utils.intptr2string(user_data);

      mainSyncContext.Post(_ => {
        FriendGroupDeletedCallbackStore?.Invoke(Utils.FromJson<List<string>>(json_group_name_array_string), user_data_string);

        FriendGroupDeletedStringCallbackStore?.Invoke(json_group_name_array_string, user_data_string);
      }, null);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMFriendGroupNameChangedCallback))]
    public static void TIMFriendGroupNameChangedCallbackInstance(IntPtr old_group_name, IntPtr new_group_name, IntPtr user_data)
    {
      string old_group_name_string = Utils.intptr2string(old_group_name);
      string new_group_name_string = Utils.intptr2string(new_group_name);
      string user_data_string = Utils.intptr2string(user_data);

      mainSyncContext.Post(_ => {
        FriendGroupNameChangedCallbackStore?.Invoke(old_group_name_string, new_group_name_string, user_data_string);

        FriendGroupNameChangedStringCallbackStore?.Invoke(old_group_name_string, new_group_name_string, user_data_string);
      }, null);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMFriendsAddedToGroupCallback))]
    public static void TIMFriendsAddedToGroupCallbackInstance(IntPtr group_name, IntPtr json_friend_info_array, IntPtr user_data)
    {
      string group_name_string = Utils.intptr2string(group_name);
      string json_friend_info_array_string = Utils.intptr2string(json_friend_info_array);
      string user_data_string = Utils.intptr2string(user_data);

      mainSyncContext.Post(_ => {
        FriendsAddedToGroupCallbackStore?.Invoke(group_name_string, Utils.FromJson<List<FriendProfile>>(json_friend_info_array_string), user_data_string);

        FriendsAddedToGroupStringCallbackStore?.Invoke(group_name_string, json_friend_info_array_string, user_data_string);
      }, null);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMFriendsDeletedFromGroupCallback))]
    public static void TIMFriendsDeletedFromGroupCallbackInstance(IntPtr group_name, IntPtr json_friend_id_array, IntPtr user_data)
    {
      string group_name_string = Utils.intptr2string(group_name);
      string json_friend_id_array_string = Utils.intptr2string(json_friend_id_array);
      string user_data_string = Utils.intptr2string(user_data);

      mainSyncContext.Post(_ => {
        FriendsDeletedFromGroupCallbackStore?.Invoke(group_name_string, Utils.FromJson<List<string>>(json_friend_id_array_string), user_data_string);

        FriendsDeletedFromGroupStringCallbackStore?.Invoke(group_name_string, json_friend_id_array_string, user_data_string);
      }, null);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMOfficialAccountSubscribedCallback))]
    public static void TIMOfficialAccountSubscribedCallbackInstance(IntPtr json_official_account_info, IntPtr user_data)
    {
      string json_official_account_info_string = Utils.intptr2string(json_official_account_info);
      string user_data_string = Utils.intptr2string(user_data);

      mainSyncContext.Post(_ => {
        OfficialAccountSubscribedCallbackStore?.Invoke(Utils.FromJson<OfficialAccountInfo>(json_official_account_info_string), user_data_string);

        OfficialAccountSubscribedStringCallbackStore?.Invoke(json_official_account_info_string, user_data_string);
      }, null);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMOfficialAccountUnsubscribedCallback))]
    public static void TIMOfficialAccountUnsubscribedCallbackInstance(IntPtr official_account_id, IntPtr user_data)
    {
      string official_account_id_string = Utils.intptr2string(official_account_id);
      string user_data_string = Utils.intptr2string(user_data);

      mainSyncContext.Post(_ => {
        OfficialAccountUnsubscribedCallbackStore?.Invoke(official_account_id_string, user_data_string);

        OfficialAccountUnsubscribedStringCallbackStore?.Invoke(official_account_id_string, user_data_string);
      }, null);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMOfficialAccountDeletedCallback))]
    public static void TIMOfficialAccountDeletedCallbackInstance(IntPtr official_account_id, IntPtr user_data)
    {
      string official_account_id_string = Utils.intptr2string(official_account_id);
      string user_data_string = Utils.intptr2string(user_data);

      mainSyncContext.Post(_ => {
        OfficialAccountDeletedCallbackStore?.Invoke(official_account_id_string, user_data_string);

        OfficialAccountDeletedStringCallbackStore?.Invoke(official_account_id_string, user_data_string);
      }, null);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMOfficialAccountInfoChangedCallback))]
    public static void TIMOfficialAccountInfoChangedCallbackInstance(IntPtr json_official_account_info, IntPtr user_data)
    {
      string json_official_account_info_string = Utils.intptr2string(json_official_account_info);
      string user_data_string = Utils.intptr2string(user_data);
      
      mainSyncContext.Post(_ => {
        OfficialAccountInfoChangedCallbackStore?.Invoke(Utils.FromJson<OfficialAccountInfo>(json_official_account_info_string), user_data_string);

        OfficialAccountInfoChangedStringCallbackStore?.Invoke(json_official_account_info_string, user_data_string);
      }, null);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMMyFollowingListChangedCallback))]
    public static void TIMMyFollowingListChangedCallbackInstance(IntPtr json_user_info_list, bool is_add, IntPtr user_data)
    {
      string json_user_info_list_string = Utils.intptr2string(json_user_info_list);
      string user_data_string = Utils.intptr2string(user_data);
      
      mainSyncContext.Post(_ => {
        MyFollowingListChangedCallbackStore?.Invoke(Utils.FromJson<List<UserProfile>>(json_user_info_list_string), is_add, user_data_string);

        MyFollowingListChangedStringCallbackStore?.Invoke(json_user_info_list_string, is_add, user_data_string);
      }, null);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMMyFollowersListChangedCallback))]
    public static void TIMMyFollowersListChangedCallbackInstance(IntPtr json_user_info_list, bool is_add, IntPtr user_data)
    {
      string json_user_info_list_string = Utils.intptr2string(json_user_info_list);
      string user_data_string = Utils.intptr2string(user_data);

      mainSyncContext.Post(_ => {
        MyFollowersListChangedCallbackStore?.Invoke(Utils.FromJson<List<UserProfile>>(json_user_info_list_string), is_add, user_data_string);

        MyFollowersListChangedStringCallbackStore?.Invoke(json_user_info_list_string, is_add, user_data_string);
      }, null);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMMutualFollowersListChangedCallback))]
    public static void TIMMutualFollowersListChangedCallbackInstance(IntPtr json_user_info_list, bool is_add, IntPtr user_data)
    {
      string json_user_info_list_string = Utils.intptr2string(json_user_info_list);
      string user_data_string = Utils.intptr2string(user_data);

      mainSyncContext.Post(_ => {
        MutualFollowersListChangedCallbackStore?.Invoke(Utils.FromJson<List<UserProfile>>(json_user_info_list_string), is_add, user_data_string);

        MutualFollowersListChangedStringCallbackStore?.Invoke(json_user_info_list_string, is_add, user_data_string);
      }, null);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMSignalingReceiveNewInvitationCallback))]
    public static void TIMSignalingReceiveNewInvitationCallbackInstance(IntPtr invite_id, IntPtr inviter, IntPtr group_id, IntPtr json_invitee_list, IntPtr data, IntPtr user_data)
    {
      string invite_id_string = Utils.intptr2string(invite_id);
      string inviter_string = Utils.intptr2string(inviter);
      string group_id_string = Utils.intptr2string(group_id);
      string json_invitee_list_string = Utils.intptr2string(json_invitee_list);
      string data_string = Utils.intptr2string(data);
      string user_data_string = Utils.intptr2string(user_data);

      mainSyncContext.Post(_ => {
        SignalingReceiveNewInvitationCallbackStore?.Invoke(invite_id_string, inviter_string, group_id_string, Utils.FromJson<List<string>>(json_invitee_list_string), data_string, user_data_string);

        SignalingReceiveNewInvitationStringCallbackStore?.Invoke(invite_id_string, inviter_string, group_id_string, json_invitee_list_string, data_string, user_data_string);
      }, null);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMSignalingInvitationCancelledCallback))]
    public static void TIMSignalingInvitationCancelledCallbackInstance(IntPtr invite_id, IntPtr inviter, IntPtr data, IntPtr user_data)
    {
      string invite_id_string = Utils.intptr2string(invite_id);
      string inviter_string = Utils.intptr2string(inviter);
      string data_string = Utils.intptr2string(data);
      string user_data_string = Utils.intptr2string(user_data);

      mainSyncContext.Post(_ => {
        SignalingInvitationCancelledCallbackStore?.Invoke(invite_id_string, inviter_string, data_string, user_data_string);

        SignalingInvitationCancelledStringCallbackStore?.Invoke(invite_id_string, inviter_string, data_string, user_data_string);
      }, null);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMSignalingInviteeAcceptedCallback))]
    public static void TIMSignalingInviteeAcceptedCallbackInstance(IntPtr invite_id, IntPtr invitee, IntPtr data, IntPtr user_data)
    {
      string invite_id_string = Utils.intptr2string(invite_id);
      string invitee_string = Utils.intptr2string(invitee);
      string data_string = Utils.intptr2string(data);
      string user_data_string = Utils.intptr2string(user_data);

      mainSyncContext.Post(_ => {
        SignalingInviteeAcceptedCallbackStore?.Invoke(invite_id_string, invitee_string, data_string, user_data_string);

        SignalingInviteeAcceptedStringCallbackStore?.Invoke(invite_id_string, invitee_string, data_string, user_data_string);
      }, null);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMSignalingInviteeRejectedCallback))]
    public static void TIMSignalingInviteeRejectedCallbackInstance(IntPtr invite_id, IntPtr invitee, IntPtr data, IntPtr user_data)
    {
      string invite_id_string = Utils.intptr2string(invite_id);
      string invitee_string = Utils.intptr2string(invitee);
      string data_string = Utils.intptr2string(data);
      string user_data_string = Utils.intptr2string(user_data);

      mainSyncContext.Post(_ => {
        SignalingInviteeRejectedCallbackStore?.Invoke(invite_id_string, invitee_string, data_string, user_data_string);

        SignalingInviteeRejectedStringCallbackStore?.Invoke(invite_id_string, invitee_string, data_string, user_data_string);
      }, null);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMSignalingInvitationTimeoutCallback))]
    public static void TIMSignalingInvitationTimeoutCallbackInstance(IntPtr invite_id, IntPtr json_invitee_list, IntPtr user_data)
    {
      string invite_id_string = Utils.intptr2string(invite_id);
      string json_invitee_list_string = Utils.intptr2string(json_invitee_list);
      string user_data_string = Utils.intptr2string(user_data);

      mainSyncContext.Post(_ => {
        SignalingInvitationTimeoutCallbackStore?.Invoke(invite_id_string, Utils.FromJson<List<string>>(json_invitee_list_string), user_data_string);

        SignalingInvitationTimeoutStringCallbackStore?.Invoke(invite_id_string, json_invitee_list_string, user_data_string);
      }, null);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMSignalingInvitationModifiedCallback))]
    public static void TIMSignalingInvitationModifiedCallbackInstance(IntPtr invite_id, IntPtr data, IntPtr user_data)
    {
      string invite_id_string = Utils.intptr2string(invite_id);
      string data_string = Utils.intptr2string(data);
      string user_data_string = Utils.intptr2string(user_data);

      mainSyncContext.Post(_ => {
        SignalingInvitationModifiedCallbackStore?.Invoke(invite_id_string, data_string, user_data_string);

        SignalingInvitationModifiedStringCallbackStore?.Invoke(invite_id_string, data_string, user_data_string);
      }, null);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMCommunityCreateTopicCallback))]
    public static void TIMCommunityCreateTopicCallbackInstance(IntPtr group_id, IntPtr topic_id, IntPtr user_data)
    {
      string group_id_string = Utils.intptr2string(group_id);
      string topic_id_string = Utils.intptr2string(topic_id);
      string user_data_string = Utils.intptr2string(user_data);

      mainSyncContext.Post(_ => {
        CommunityCreateTopicCallbackStore?.Invoke(group_id_string, topic_id_string, user_data_string);

        CommunityCreateTopicStringCallbackStore?.Invoke(group_id_string, topic_id_string, user_data_string);
      }, null);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMCommunityDeleteTopicCallback))]
    public static void TIMCommunityDeleteTopicCallbackInstance(IntPtr group_id, IntPtr json_topic_id_array, IntPtr user_data)
    {
      string group_id_string = Utils.intptr2string(group_id);
      string json_topic_id_array_string = Utils.intptr2string(json_topic_id_array);
      string user_data_string = Utils.intptr2string(user_data);

      mainSyncContext.Post(_ => {
        CommunityDeleteTopicCallbackStore?.Invoke(group_id_string, Utils.FromJson<List<string>>(json_topic_id_array_string), user_data_string);

        CommunityDeleteTopicStringCallbackStore?.Invoke(group_id_string, json_topic_id_array_string, user_data_string);
      }, null);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMCommunityChangeTopicInfoCallback))]
    public static void TIMCommunityChangeTopicInfoCallbackInstance(IntPtr group_id, IntPtr json_topic_info, IntPtr user_data)
    {
      string group_id_string = Utils.intptr2string(group_id);
      string json_topic_info_string = Utils.intptr2string(json_topic_info);
      string user_data_string = Utils.intptr2string(user_data);

      mainSyncContext.Post(_ => {
        CommunityChangeTopicInfoCallbackStore?.Invoke(group_id_string, Utils.FromJson<TopicInfo>(json_topic_info_string), user_data_string);

        CommunityChangeTopicInfoStringCallbackStore?.Invoke(group_id_string, json_topic_info_string, user_data_string);
      }, null);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMCommunityReceiveTopicRESTCustomDataCallback))]
    public static void TIMCommunityReceiveTopicRESTCustomDataCallbackInstance(IntPtr topic_id, IntPtr custom_data, IntPtr user_data)
    {
      string topic_id_string = Utils.intptr2string(topic_id);
      string custom_data_string = Utils.intptr2string(custom_data);
      string user_data_string = Utils.intptr2string(user_data);

      mainSyncContext.Post(_ => {
        CommunityReceiveTopicRESTCustomDataCallbackStore?.Invoke(topic_id_string, custom_data_string, user_data_string);

        CommunityReceiveTopicRESTCustomDataStringCallbackStore?.Invoke(topic_id_string, custom_data_string, user_data_string);
      }, null);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMCommunityCreatePermissionGroupCallback))]
    public static void TIMCommunityCreatePermissionGroupCallbackInstance(IntPtr group_id, IntPtr json_permission_group_info, IntPtr user_data)
    {
      string group_id_string = Utils.intptr2string(group_id);
      string json_permission_group_info_string = Utils.intptr2string(json_permission_group_info);
      string user_data_string = Utils.intptr2string(user_data);

      mainSyncContext.Post(_ => {
        CommunityCreatePermissionGroupCallbackStore?.Invoke(group_id_string, Utils.FromJson<PermissionGroupInfo>(json_permission_group_info_string), user_data_string);

        CommunityCreatePermissionGroupStringCallbackStore?.Invoke(group_id_string, json_permission_group_info_string, user_data_string);
      }, null);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMCommunityDeletePermissionGroupCallback))]
    public static void TIMCommunityDeletePermissionGroupCallbackInstance(IntPtr group_id, IntPtr json_permission_group_id_array, IntPtr user_data)
    {
      string group_id_string = Utils.intptr2string(group_id);
      string json_permission_group_id_array_string = Utils.intptr2string(json_permission_group_id_array);
      string user_data_string = Utils.intptr2string(user_data);

      mainSyncContext.Post(_ => {
        CommunityDeletePermissionGroupCallbackStore?.Invoke(group_id_string, Utils.FromJson<List<string>>(json_permission_group_id_array_string), user_data_string);

        CommunityDeletePermissionGroupStringCallbackStore?.Invoke(group_id_string, json_permission_group_id_array_string, user_data_string);
      }, null);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMCommunityChangePermissionGroupInfoCallback))]
    public static void TIMCommunityChangePermissionGroupInfoCallbackInstance(IntPtr group_id, IntPtr json_permission_group_info, IntPtr user_data)
    {
      string group_id_string = Utils.intptr2string(group_id);
      string json_permission_group_info_string = Utils.intptr2string(json_permission_group_info);
      string user_data_string = Utils.intptr2string(user_data);

      mainSyncContext.Post(_ => {
        CommunityChangePermissionGroupInfoCallbackStore?.Invoke(group_id_string, Utils.FromJson<PermissionGroupInfo>(json_permission_group_info_string), user_data_string);

        CommunityChangePermissionGroupInfoStringCallbackStore?.Invoke(group_id_string, json_permission_group_info_string, user_data_string);
      }, null);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMCommunityAddMembersToPermissionGroupCallback))]
    public static void TIMCommunityAddMembersToPermissionGroupCallbackInstance(IntPtr group_id, IntPtr json_result, IntPtr user_data)
    {
      string group_id_string = Utils.intptr2string(group_id);
      string json_result_string = Utils.intptr2string(json_result);
      string user_data_string = Utils.intptr2string(user_data);

      mainSyncContext.Post(_ => {
        CommunityAddMembersToPermissionGroupCallbackStore?.Invoke(group_id_string, Utils.FromJson<PermissionGroupCallback>(json_result_string), user_data_string);

        CommunityAddMembersToPermissionGroupStringCallbackStore?.Invoke(group_id_string, json_result_string, user_data_string);
      }, null);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMCommunityRemoveMembersFromPermissionGroupCallback))]
    public static void TIMCommunityRemoveMembersFromPermissionGroupCallbackInstance(IntPtr group_id, IntPtr json_result, IntPtr user_data)
    {
      string group_id_string = Utils.intptr2string(group_id);
      string json_result_string = Utils.intptr2string(json_result);
      string user_data_string = Utils.intptr2string(user_data);

      mainSyncContext.Post(_ => {
        CommunityRemoveMembersFromPermissionGroupCallbackStore?.Invoke(group_id_string, Utils.FromJson<PermissionGroupCallback>(json_result_string), user_data_string);

        CommunityRemoveMembersFromPermissionGroupStringCallbackStore?.Invoke(group_id_string, json_result_string, user_data_string);
      }, null);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMCommunityAddTopicPermissionCallback))]
    public static void TIMCommunityAddTopicPermissionCallbackInstance(IntPtr group_id, IntPtr json_result, IntPtr user_data)
    {
      string group_id_string = Utils.intptr2string(group_id);
      string json_result_string = Utils.intptr2string(json_result);
      string user_data_string = Utils.intptr2string(user_data);

      mainSyncContext.Post(_ => {
        CommunityAddTopicPermissionCallbackStore?.Invoke(group_id_string, Utils.FromJson<PermissionGroupCallback>(json_result_string), user_data_string);

        CommunityAddTopicPermissionStringCallbackStore?.Invoke(group_id_string, json_result_string, user_data_string);
      }, null);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMCommunityDeleteTopicPermissionCallback))]
    public static void TIMCommunityDeleteTopicPermissionCallbackInstance(IntPtr group_id, IntPtr json_result, IntPtr user_data)
    {
      string group_id_string = Utils.intptr2string(group_id);
      string json_result_string = Utils.intptr2string(json_result);
      string user_data_string = Utils.intptr2string(user_data);

      mainSyncContext.Post(_ => {
        CommunityDeleteTopicPermissionCallbackStore?.Invoke(group_id_string, Utils.FromJson<PermissionGroupCallback>(json_result_string), user_data_string);

        CommunityDeleteTopicPermissionStringCallbackStore?.Invoke(group_id_string, json_result_string, user_data_string);
      }, null);
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMCommunityModifyTopicPermissionCallback))]
    public static void TIMCommunityModifyTopicPermissionCallbackInstance(IntPtr group_id, IntPtr json_result, IntPtr user_data)
    {
      string group_id_string = Utils.intptr2string(group_id);
      string json_result_string = Utils.intptr2string(json_result);
      string user_data_string = Utils.intptr2string(user_data);

      mainSyncContext.Post(_ => {
        CommunityModifyTopicPermissionCallbackStore?.Invoke(group_id_string, Utils.FromJson<PermissionGroupCallback>(json_result_string), user_data_string);

        CommunityModifyTopicPermissionStringCallbackStore?.Invoke(group_id_string, json_result_string, user_data_string);
      }, null);
    }
  
    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMExperimentalNotifyCallback))]
    public static void TIMExperimentalNotifyCallbackInstance(IntPtr key, IntPtr data, IntPtr user_data)
    {
      string key_string = Utils.intptr2string(key);
      string data_string = Utils.intptr2string(data);
      string user_data_string = Utils.intptr2string(user_data);

      mainSyncContext.Post(_ => {
        ExperimentalNotifyCallbackStore?.Invoke(key_string, data_string, user_data_string);

        ExperimentalNotifyStringCallbackStore?.Invoke(key_string, data_string, user_data_string);
      }, null);
    }
  }
}
