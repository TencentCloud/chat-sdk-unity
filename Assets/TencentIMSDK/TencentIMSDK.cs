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

    private static Delegate RecvNewMsgCallbackStore;

    private static Delegate MsgReadedReceiptCallbackStore;
    private static Delegate MsgRevokeCallbackStore;
    private static Delegate MsgElemUploadProgressCallbackStore;
    private static Delegate GroupTipsEventCallbackStore;

    private static Delegate GroupAttributeChangedCallbackStore;

    private static Delegate ConvEventCallbackStore;

    private static ConvTotalUnreadMessageCountChangedCallback ConvTotalUnreadMessageCountChangedCallbackStore;
    private static NetworkStatusListenerCallback NetworkStatusListenerCallbackStore;
    private static KickedOfflineCallback KickedOfflineCallbackStore;
    private static UserSigExpiredCallback UserSigExpiredCallbackStore;
    private static Delegate OnAddFriendCallbackStore;
    private static Delegate OnDeleteFriendCallbackStore;
    private static Delegate UpdateFriendProfileCallbackStore;
    private static Delegate FriendAddRequestCallbackStore;
    private static Delegate FriendApplicationListDeletedCallbackStore;
    private static FriendApplicationListReadCallback FriendApplicationListReadCallbackStore;
    private static Delegate FriendBlackListAddedCallbackStore;
    private static Delegate FriendBlackListDeletedCallbackStore;
    private static LogCallback LogCallbackStore;
    private static Delegate MsgUpdateCallbackStore;
    private static Delegate MsgGroupMessageReadMemberListCallbackStore;
    private static Delegate MsgGroupMessageReadMemberListCallbackWebStore;
    private static Delegate GroupTopicCreatedCallbackStore;
    private static Delegate GroupTopicDeletedCallbackStore;
    private static Delegate GroupTopicChangedCallbackStore;
    private static Delegate SelfInfoUpdatedCallbackStore;
    private static Delegate UserStatusChangedCallbackStore;
    private static Delegate MsgExtensionsChangedCallbackStore;
    private static Delegate MsgExtensionsDeletedCallbackStore;
    // SetOnDeleteFriendCallback user_data is wrongly transmitted from the Native callback.
    private static string SetOnDeleteFriendCallbackUser_Data;
    private static HashSet<string> IsStringCallbackStore = new HashSet<string>();

    private static bool needLog;

    public static void CallExperimentalAPICallback(int code, string desc, string data, string user_data)
    {
      Utils.Log("Tencent Cloud IM add config success .");
    }

    static void Log(string user_data, params string[] args)
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
        if (args[args.Length - 1] == "tencent-chat-unity-sdk-res")
        {
          prefix = "tencent-chat-unity-sdk-res: ";
          args = args.Take(args.Length - 1).ToArray();
        }
        var param = new ExperimentalAPIReqeustParam
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
    /// ?????????IM SDK
    /// Init IM SDK
    /// </summary>
    /// <param name="sdk_app_id">sdk_app_id??????????????????????????? IM?????????????????????????????? (sdk_app_id is automatically generated after create an IM instance on the IM Console)</param>
    /// <param name="json_sdk_config"><see cref="SdkConfig"/></param>
    /// <param name="need_log">????????????????????????????????? (Need callback log)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult Init(long sdk_app_id, SdkConfig json_sdk_config, bool? need_log = false)
    {
      ExperimentalAPIReqeustParam param = new ExperimentalAPIReqeustParam();

      param.request_internal_operation = TIMInternalOperation.internal_operation_set_ui_platform.ToString();

      param.request_set_ui_platform_param = 5;
      TIMResult res = CallExperimentalAPI(param, CallExperimentalAPICallback);

      string configString = Utils.ToJson(json_sdk_config);
      needLog = (bool)need_log;
      Utils.Log(configString);

      int timSucc = IMNativeSDK.TIMInit(sdk_app_id, Utils.string2intptr(configString));
      return (TIMResult)timSucc;
    }
    /// <summary>
    /// ????????????IM SDK
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
    /// ??????
    /// Login
    /// </summary>
    /// <param name="user_id">??????ID (user ID)</param>
    /// <param name="user_sig">??????sdk_app_id???secret?????????????????? https://cloud.tencent.com/document/product/269/32688 (Generated by sdk_app_id and secret, see https://www.tencentcloud.com/document/product/1047/34385)</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ??????SDK???????????????
    /// Get native SDK version
    /// </summary>
    /// <returns>string version</returns>
    public static string GetSDKVersion()
    {
      IntPtr version = IMNativeSDK.TIMGetSDKVersion();
      return Utils.intptr2string(version);
    }

    /// <summary>
    /// ??????????????????
    /// Set SDK config
    /// </summary>
    /// <param name="config">?????? (Config)</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ??????????????????????????????
    /// Get server time (second)
    /// </summary>
    /// <returns>??????????????? (Server time)</returns>
    public static long GetServerTime()
    {
      return IMNativeSDK.TIMGetServerTime();
    }

    /// <summary>
    /// ????????????
    /// Logout
    /// </summary>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ????????????????????????
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
    /// ????????????????????????ID (WebGL only)
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
    /// ????????????????????????ID
    /// Get login user ID
    /// </summary>
    /// <param name="user_id">???????????????user_id???StringBuilder (StringBuilder for receiving user_id)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult GetLoginUserID(StringBuilder user_id)
    {
      int timSucc = IMNativeSDK.TIMGetLoginUserID(user_id);
      return (TIMResult)timSucc;
    }

    /// <summary>
    /// ??????????????????
    /// Get conversation list
    /// </summary>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ????????????
    /// Delete conversation
    /// </summary>
    /// <param name="conv_id">??????ID???c2c?????????user_id???????????????group_id (Conversation ID, C2C conv: user_id, Group conv: group_id)</param>
    /// <param name="conv_type">???????????? TIMConvType (Conversation type)</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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

    /// <summary>
    /// ??????????????????
    /// Get conversation info
    /// </summary>
    /// <param name="conv_list_param">???????????????????????? ConvParam?????? (List of get conversation info params)</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ??????????????????
    /// Set conversation draft
    /// </summary>
    /// <param name="conv_id">??????ID (Conversation ID)</param>
    /// <param name="conv_type">???????????? TIMConvType (Conversation type)</param>
    /// <param name="param">DraftParam</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult ConvSetDraft(string conv_id, TIMConvType conv_type, DraftParam param)
    {

      int timSucc = IMNativeSDK.TIMConvSetDraft(conv_id, (int)conv_type, Utils.string2intptr(Utils.ToJson(param)));

      return (TIMResult)timSucc;
    }

    /// <summary>
    /// ??????????????????
    /// Cancel conversation draft
    /// </summary>
    /// <param name="conv_id">??????ID (Conversation ID)</param>
    /// <param name="conv_type">???????????? TIMConvType (Conversation type)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult ConvCancelDraft(string conv_id, TIMConvType conv_type)
    {

      int timSucc = IMNativeSDK.TIMConvCancelDraft(conv_id, (int)conv_type);

      return (TIMResult)timSucc;
    }

    /// <summary>
    /// ????????????
    /// Pin conversation
    /// </summary>
    /// <param name="conv_id">??????ID (Conversation ID)</param>
    /// <param name="conv_type">???????????? TIMConvType (Conversation type)</param>
    /// <param name="is_pinned">?????????????????? (Is pinned)</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ???????????????????????????
    /// Get total unread message count
    /// </summary>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ????????????
    /// Send message
    /// </summary>
    /// <param name="conv_id">??????ID (Conversation ID)</param>
    /// <param name="conv_type">???????????? TIMConvType (Conversation type)</param>
    /// <param name="message">????????? Message</param>
    /// <param name="message_id">????????????ID???StringBuilder (StringBuilder for receiving message ID)</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ??????????????????
    /// Cancel sending message
    /// </summary>
    /// <param name="conv_id">??????ID (Conversation ID)</param>
    /// <param name="conv_type">???????????? TIMConvType (Conversation type)</param>
    /// <param name="message_id">??????ID (Message ID)</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ?????????????????????
    /// Search local message
    /// </summary>
    /// <param name="message_id_array">???????????????id?????? (Message ID list for searching)</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ?????????????????? C2C
    /// Report message read (C2C)
    /// </summary>
    /// <param name="conv_id">??????ID (Conversation ID)</param>
    /// <param name="conv_type">???????????? TIMConvType (Conversation type)</param>
    /// <param name="message">????????? Message</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ???????????????????????????
    /// Mark all message as read
    /// </summary>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ????????????
    /// Revoke message
    /// </summary>
    /// <param name="conv_id">??????ID (Conversation ID)</param>
    /// <param name="conv_type">???????????? TIMConvType (Conversation type)</param>
    /// <param name="message">????????? Message</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ????????????
    /// Modify message
    /// <para>???????????????????????????????????????????????????C2C?????????????????????Group??????????????? TIMMsgMessageModifiedCallback ?????????(If success, self and peer (C2C) or group member (Group) will receive TIMMsgMessageModifiedCallback.)</para>
    /// <para>??????????????????????????????????????????????????????????????????cb ????????? ERR_SDK_MSG_MODIFY_CONFLICT ?????????(If the message is modified during modificaion, cb will return error ERR_SDK_MSG_MODIFY_CONFLICT.)</para>
    /// <para>???????????????????????????????????????cb ????????????????????????????????????(Success or fail, cb will return the modified message)</para>
    /// </summary>
    /// <param name="message">????????????????????? (Modified message)</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ?????????????????????????????????
    /// Find Message by locator
    /// </summary>
    /// <param name="conv_id">??????ID (Conversation ID)</param>
    /// <param name="conv_type">???????????? TIMConvType (Conversation type)</param>
    /// <param name="message_locator">??????????????? List<MsgLocator></param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ????????????
    /// Import message list
    /// </summary>
    /// <param name="conv_id">??????ID (Conversation ID)</param>
    /// <param name="conv_type">???????????? TIMConvType (Conversation type)</param>
    /// <param name="message_list">???????????? Message?????? (Message list)</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ????????????
    /// Save message
    /// </summary>
    /// <param name="conv_id">??????ID (Conversation ID)</param>
    /// <param name="conv_type">???????????? TIMConvType (Conversation type)</param>
    /// <param name="message">????????? (Message)</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ????????????????????????
    /// Get history message list
    /// </summary>
    /// <param name="conv_id">??????ID (Conversation ID)</param>
    /// <param name="conv_type">???????????? TIMConvType (Conversation type)</param>
    /// <param name="get_message_list_param">???????????????????????? MsgGetMsgListParam (Get history message list param)</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult MsgGetMsgList(string conv_id, TIMConvType conv_type, MsgGetMsgListParam get_message_list_param, ValueCallback<List<Message>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(get_message_list_param);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<Message>>);
      int timSucc = IMNativeSDK.TIMMsgGetMsgList(conv_id, (int)conv_type, Utils.string2intptr(param), ValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, conv_id, conv_type.ToString(), param);
      return (TIMResult)timSucc;
    }
    public static TIMResult MsgGetMsgList(string conv_id, TIMConvType conv_type, MsgGetMsgListParam get_message_list_param, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(get_message_list_param);
      ValuecallbackStore.Add(user_data, callback);
      int timSucc = IMNativeSDK.TIMMsgGetMsgList(conv_id, (int)conv_type, Utils.string2intptr(param), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, conv_id, conv_type.ToString(), param);
      return (TIMResult)timSucc;
    }

    /// <summary>
    /// ????????????
    /// Delete message
    /// </summary>
    /// <param name="conv_id">??????ID (Conversation ID)</param>
    /// <param name="conv_type">???????????? TIMConvType (Conversation type)</param>
    /// <param name="message_delete_param">?????????????????? MsgDeleteParam (Message deletion param)</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ????????????
    /// Delete message list
    /// </summary>
    /// <param name="conv_id">??????ID (Conversation ID)</param>
    /// <param name="conv_type">???????????? TIMConvType (Conversation type)</param>
    /// <param name="message_list">????????????????????? (Deleted message list)</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ??????????????????
    /// Clear history message
    /// </summary>
    /// <param name="conv_id">??????ID (Conversation ID)</param>
    /// <param name="conv_type">???????????? TIMConvType (Conversation type)</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ?????????????????????
    /// Set C2C receiving message option
    /// </summary>
    /// <param name="user_id_list">??????ID?????? (User ID list)</param>
    /// <param name="opt">?????????????????? TIMReceiveMessageOpt (Receiving message option)</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ??????C2C???????????????
    /// Get C2C receiving message option
    /// </summary>
    /// <param name="user_id_list">??????ID?????? (user ID list)</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ????????????????????????
    /// Set group receiving message option
    /// </summary>
    /// <param name="group_id">??????ID (group ID)</param>
    /// <param name="opt">?????????????????? TIMReceiveMessageOpt (Receiving message option)</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ?????????????????????????????????iOS ??? Android ???????????????
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
    /// APP ????????????????????????????????????????????????????????????????????????????????????????????????????????????iOS ??? Android ??????????????????
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
    /// APP ???????????????????????????????????????????????????iOS ??? Android ??????????????????
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
    /// ?????????????????????
    /// Download message elements
    /// </summary>
    /// <param name="download_param">???????????? DownloadElemParam</param>
    /// <param name="path">???????????? (Local path)</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult MsgDownloadElemToPath(DownloadElemParam download_param, string path, ValueCallback<MsgDownloadElemResult> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
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

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(download_param);

      ValuecallbackStore.Add(user_data, callback);

      int timSucc = IMNativeSDK.TIMMsgDownloadElemToPath(Utils.string2intptr(param), Utils.string2intptr(path), StringValueCallbackInstance, Utils.string2intptr(user_data));

      Log(user_data, param, path);
      return (TIMResult)timSucc;
    }

    /// <summary>
    /// ??????????????????
    /// Download merger message
    /// </summary>
    /// <param name="message">????????? Message</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ??????????????????
    /// Send batch messages
    /// </summary>
    /// <param name="json_batch_send_param">??????????????? MsgBatchSendParam (Batch message param)</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ??????????????????
    /// Search local message
    /// </summary>
    /// <param name="message_search_param">?????????????????? MessageSearchParam (Search message param)</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ????????????????????????
    /// Set local custom data
    /// </summary>
    /// <param name="message">????????? Message</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ?????????
    /// Create group
    /// </summary>
    /// <param name="group">??????????????? CreateGroupParam</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ?????????
    /// Delete group
    /// </summary>
    /// <param name="group_id">???ID (Group ID)</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ?????????
    /// Join group
    /// </summary>
    /// <param name="group_id">???ID (Group ID)</param>
    /// <param name="hello_message">????????????????????? (Greeting message)</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ?????????
    /// Quit group
    /// </summary>
    /// <param name="group_id">???ID (Group ID)</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ??????????????????
    /// Invite to group
    /// </summary>
    /// <param name="param">?????????????????? GroupInviteMemberParam</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ???????????????
    /// Delete member from group
    /// </summary>
    /// <param name="param">?????????????????? GroupDeleteMemberParam</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ??????????????????????????????
    /// Get joined group list
    /// </summary>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ???????????????
    /// Get group info list
    /// </summary>
    /// <param name="group_id_list">???ID?????? (Group ID list)</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ???????????????
    /// Modify group info
    /// </summary>
    /// <param name="json_group_modifyinfo_param">?????????????????? GroupModifyInfoParam</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ?????????????????????
    /// Get member info list
    /// </summary>
    /// <param name="json_group_getmeminfos_param">?????????????????? GroupGetMemberInfoListParam</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ?????????????????????
    /// Modify group member info
    /// </summary>
    /// <param name="json_group_modifymeminfo_param">?????????????????? GroupModifyMemberInfoParam</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ???????????????????????????
    /// Get group pendency list
    /// </summary>
    /// <param name="json_group_getpendence_list_param">?????????????????? GroupPendencyOption</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ???????????????????????????
    /// Report group pendency as read
    /// </summary>
    /// <param name="time_stamp">????????? (Timestamp)</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ?????????????????????
    /// Handle group pendency
    /// </summary>
    /// <param name="json_group_handle_pendency_param">??????????????????????????? GroupHandlePendencyParam</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ????????????????????????
    /// Get group online member count
    /// </summary>
    /// <param name="group_id">???ID (Group ID)</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ??????????????????5.4.666 ?????????????????????????????????????????????????????????
    /// Search group info (^5.4.666, Flagship ver. package only)
    /// </summary>
    /// <param name="json_group_search_groups_param">?????????????????? GroupSearchParam</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ???????????????
    /// Search group members
    /// </summary>
    /// <param name="json_group_search_group_members_param">????????????????????? GroupMemberSearchParam</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult GroupSearchGroupMembers(GroupMemberSearchParam json_group_search_group_members_param, ValueCallback<List<GroupGetOnlineMemberCountResult>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var param = Utils.ToJson(json_group_search_group_members_param);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<GroupGetOnlineMemberCountResult>>);

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
    /// ???????????????????????????
    /// Init group attributes
    /// </summary>
    /// <param name="group_id">???ID (Group ID)</param>
    /// <param name="json_group_atrributes">??????????????? Array<GroupAttributes></param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ???????????????
    /// Set group attributes
    /// <para>?????????????????????????????? value ????????????????????????????????????????????? (Modify value If key is predefined, otherwise create new key-value pair)</para>
    /// </summary>
    /// <param name="group_id">???ID (Group ID)</param>
    /// <param name="json_group_atrributes">??????????????? List<GroupAttributes></param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ????????????????????????
    /// Delete group attributes
    /// </summary>
    /// <param name="group_id">???ID (Group ID)</param>
    /// <param name="json_keys">??????key?????? (Key list)</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ?????????????????????
    /// Get group attributes
    /// </summary>
    /// <param name="group_id">???ID (Group ID)</param>
    /// <param name="json_keys">??????key?????? (Key list)</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ????????????????????????????????????????????????????????????
    /// Get joined coomunity list
    /// </summary>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ????????????
    /// Create topic in community group
    /// <param>????????? topic_id ???????????? (Callback is topic_id as string)</param>
    /// </summary>
    /// <param name="group_id">??? ID (Group ID)</param>
    /// <param name="json_topic_info">???????????? (Topic info)</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ????????????
    /// Delete topic from community
    /// </summary>
    /// <param name="group_id">??? ID (Group ID)</param>
    /// <param name="json_topic_id_array">?????? ID ?????? (Topic ID list)</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ??????????????????
    /// Set topic info
    /// </summary>
    /// <param name="json_topic_info">???????????? (Topic info)</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ??????????????????
    /// Get topic info list
    /// <param>json_topic_id_array ??????????????????????????????????????????????????? (When json_topic_id_array is null, get all topics in this community)</param>
    /// </summary>
    /// <param name="group_id">??? ID (Group ID)</param>
    /// <param name="json_topic_id_array">?????? ID ?????? (Topic ID list)</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult GroupGetTopicInfoList(string group_id, List<string> json_topic_id_array, ValueCallback<List<GroupGetTopicInfoResult>> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();
      var list = Utils.ToJson(json_topic_id_array);

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<List<GroupGetTopicInfoResult>>);

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
    /// ???????????????????????? 6.3 ??????????????????
    /// Get user status ,available ^6.3
    /// <param>???????????????????????????????????????????????????????????????????????? userID ?????? (Input self userID to check self status)</param>
    /// </summary>
    /// <param name="json_identifier_array">?????? ID ?????? (User ID list)</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ??????????????????????????? 6.3 ??????????????????
    /// Set self stauts ,available ^6.3
    /// <param>?????????????????????????????????????????????????????????????????? V2TIMUserStatus.customStatus (Caveat: this can only set SELF status)</param>
    /// </summary>
    /// <param name="json_current_user_status">??????????????????????????? (Current user status)</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ???????????????????????? 6.3 ??????????????????
    /// Subscribe user status, available ^6.3
    /// <param>?????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????? TIMSetUserStatusChangedCallback ??????????????? (After subscription, listen to TIMSetUserStatusChangedCallback to get changing user status (online status, custom status))</param>
    /// <param>?????????????????????????????????????????????????????????????????????????????????????????????????????????????????? (Subscribe friend list status, just turn on it on the IM console, no need for calling this)</param>
    /// <param>?????????????????????????????????????????????????????? TIMSetUserStatusChangedCallback ???????????????????????????????????????????????? (Use TIMSetUserStatusChangedCallback to subscribe self status changing)</param>
    /// <param>???????????????????????????????????????????????????????????????????????????????????? (Subscription has limits, over the limit, the oldest subscription will be deactivated)</param>
    /// </summary>
    /// <param name="json_identifier_array">?????????????????? ID (Subscribed user ID list)</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ?????????????????????????????? 6.3 ??????????????????
    /// Unsubscribe user status
    /// <param>??? userIDList ??????????????????????????????????????? (When userIDList is empty, unsubscribe all)</param>
    /// </summary>
    /// <param name="json_identifier_array">???????????????????????? ID (Unsubscribed user ID list)</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ????????????????????????
    /// Get user profile list
    /// </summary>
    /// <param name="json_get_user_profile_list_param">???????????????????????? FriendShipGetProfileListParam</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ?????????????????????
    /// Modify self user profile
    /// </summary>
    /// <param name="json_modify_self_user_profile_param">???????????????????????? UserProfileItem</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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

    /// <summary>
    /// ????????????????????????
    /// Get friend's profile list
    /// </summary>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ????????????
    /// Add friend
    /// </summary>
    /// <param name="param">?????????????????? FriendshipAddFriendParam</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ??????????????????
    /// Handle friend add request
    /// </summary>
    /// <param name="json_handle_friend_add_param">???????????????????????? FriendResponse</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ??????????????????
    /// Modify friend's profile
    /// </summary>
    /// <param name="json_modify_friend_info_param">???????????????????????? FriendshipModifyFriendProfileParam</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ????????????
    /// Delete friend
    /// </summary>
    /// <param name="json_delete_friend_param">?????????????????? FriendshipDeleteFriendParam</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ??????????????????
    /// Check friend type
    /// </summary>
    /// <param name="json_check_friend_list_param">???????????????????????? FriendshipCheckFriendTypeParam</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ??????????????????
    /// Creagte friend group
    /// </summary>
    /// <param name="json_create_friend_group_param">???????????????????????? CreateFriendGroupInfo</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ????????????????????????
    /// Get friend group list
    /// </summary>
    /// <param name="json_get_friend_group_list_param">?????????????????????friend_group_name??????</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ????????????????????????
    /// Modify friend group
    /// </summary>
    /// <param name="json_modify_friend_group_param">?????????????????? FriendshipModifyFriendGroupParam</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ????????????????????????
    /// Delete friend group
    /// </summary>
    /// <param name="json_delete_friend_group_param">?????????????????????friend_group_name??????</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ???????????????
    /// Add to blacklist
    /// </summary>
    /// <param name="json_add_to_blacklist_param">??????????????? ???userID?????? (User ID list)</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ?????????????????????
    /// Get blacklist
    /// </summary>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ??????????????????
    /// Delete from blacklist
    /// </summary>
    /// <param name="json_delete_from_blacklist_param">userID?????? (User ID list)</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ????????????????????????
    /// Get friend request pendency list
    /// </summary>
    /// <param name="json_get_pendency_list_param">???????????????????????? FriendshipGetPendencyListParam</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ????????????????????????
    /// Delete friend request pendency
    /// </summary>
    /// <param name="json_delete_pendency_param">?????????????????????????????? FriendshipDeletePendencyParam</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ??????????????????????????????
    /// Report friend pendency read
    /// </summary>
    /// <param name="time_stamp">??????????????? (Report timestamp)</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ????????????
    /// Search friend
    /// </summary>
    /// <param name="json_search_friends_param">???????????? FriendSearchParam</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ??????????????????
    /// Get friends' info
    /// </summary>
    /// <param name="json_get_friends_info_param">???????????????????????????userIDs (Friends' user ID list)</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ?????????????????????????????????????????????????????????????????????
    /// Expermental API, not for developers
    /// </summary>
    /// <param name="json_param">????????????????????? ExperimentalAPIReqeustParam</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
    /// <returns><see cref="TIMResult"/></returns>
    public static TIMResult CallExperimentalAPI(ExperimentalAPIReqeustParam json_param, ValueCallback<ReponseInfo> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);
      ValuecallbackDeleStore.Add(user_data, threadOperation<ReponseInfo>);

      int res = IMNativeSDK.callExperimentalAPI(Utils.string2intptr(Utils.ToJson(json_param)), ValueCallbackInstance, Utils.string2intptr(user_data));

      return (TIMResult)res;
    }
    public static TIMResult CallExperimentalAPI(ExperimentalAPIReqeustParam json_param, ValueCallback<string> callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

      string user_data = fn_name + "_" + Utils.getRandomStr();

      ValuecallbackStore.Add(user_data, callback);

      int res = IMNativeSDK.callExperimentalAPI(Utils.string2intptr(Utils.ToJson(json_param)), StringValueCallbackInstance, Utils.string2intptr(user_data));

      return (TIMResult)res;
    }

    /// <summary>
    /// ????????????????????????
    /// Get message read receipt
    /// </summary>
    /// <param name="msg_array">???????????? (Message list)</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ????????????????????????
    /// Send message read receipts
    /// </summary>
    /// <param name="msg_array">???????????? (Message list)</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ?????????????????????6.7 ????????????????????????????????????????????????????????????
    /// Set message extensions (Available on native SDK v6.7 or higher, Flagship only)
    /// </summary>
    /// <param name="message">???????????????????????????????????????1??????????????????????????? supportMessageExtension ??? true???2??????????????????????????????????????????3???????????????????????????Community??????????????????AVChatRoom????????????(Message fulfills: 1. Sending message with true for supportMessageExtension. 2. Message status is sent. 3. Message not for Community and AVChatRoom)</param>
    /// <param name="extensions">????????????????????????????????? key ????????????????????????????????? value ????????????????????? key ???????????????????????????(Message extensions)</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ?????????????????????6.7 ????????????????????????????????????????????????????????????
    /// Get message extensions (Available on native SDK v6.7 or higher, Flagship only)
    /// </summary>
    /// <param name="message">??????(Message)</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ?????????????????????6.7 ????????????????????????????????????????????????????????????
    /// Delete message extensions (Available on native SDK v6.7 or higher, Flagship only)
    /// </summary>
    /// <param name="message">??????(Message)</param>
    /// <param name="extensions">???????????? key ????????????????????????????????? 20 ???????????????????????????????????? ????????????????????????????????? (Extension key array. Maximum 20/call. If this sets to null, it will delete all the message extensions.)</param>
    /// <param name="callback">???????????? (Asynchronous callback)</param>
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
    /// ????????????????????????????????????
    /// Get group message read member list
    /// </summary>
    /// <param name="message">??????????????? (Group message)</param>
    /// <param name="filter">?????????????????????????????????????????? (Group message read member's filter)</param>
    /// <param name="next_seq">????????????????????????????????????????????? 0????????????????????????????????????????????????????????????????????? next_seq (Next seq as page index, default: 0 and use the last next_seq returned from the callback as the next seq)</param>
    /// <param name="count">???????????????????????????????????? 100 ??????(Page size, maximum 100)</param>
    /// <param name="callback">?????? MsgGroupMessageReadMemberListCallback (Callback)</param>
    /// <param name="stringCallback">string ???????????? MsgGroupMessageReadMemberListStringCallback (String data type callback)</param>
    public static TIMResult GetMsgGroupMessageReadMemberList(Message message, TIMGroupMessageReadMembersFilter filter, ulong next_seq, int count, MsgGroupMessageReadMemberListCallback callback = null, MsgGroupMessageReadMemberListStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      if (stringCallback == null || callback != null)
      {
        IsStringCallbackStore.Remove(fn_name);
      }
      else
      {
        IsStringCallbackStore.Add(fn_name);
      }
      if (callback == null && stringCallback == null)
      {
        int timRes = IMNativeSDK.TIMMsgGetGroupMessageReadMemberList(Utils.string2intptr(""), filter, next_seq, count, null, Utils.string2intptr(""));
        return (TIMResult)timRes;
      }
      string user_data = fn_name + "_" + Utils.getRandomStr();
      var msg = Utils.ToJson(message);
      MsgGroupMessageReadMemberListCallbackStore = callback != null ? (Delegate)callback : (Delegate)stringCallback;
      int res = IMNativeSDK.TIMMsgGetGroupMessageReadMemberList(Utils.string2intptr(msg), filter, next_seq, count, TIMMsgGroupMessageReadMemberListCallbackInstance, Utils.string2intptr(user_data));
      Log(user_data, msg, filter.ToString(), next_seq.ToString(), count.ToString());
      return (TIMResult)res;
    }

#if !UNITY_EDITOR && UNITY_WEBGL
    /// <summary>
    /// ????????????????????????????????????
    /// Get group message read member list (Web Only, different callback)
    /// </summary>
    /// <param name="message">??????????????? (Group message)</param>
    /// <param name="filter">?????????????????????????????????????????? (Group message read member's filter)</param>
    /// <param name="next_seq">????????????????????????????????????????????? 0????????????????????????????????????????????????????????????????????? next_seq (Next seq as page index, default: 0 and use the last next_seq returned from the callback as the next seq)</param>
    /// <param name="count">???????????????????????????????????? 100 ??????(Page size, maximum 100)</param>
    /// <param name="callback">?????? MsgGroupMessageReadMemberListCallback (Callback)</param>
    /// <param name="stringCallback">string ???????????? MsgGroupMessageReadMemberListStringCallback (String data type callback)</param>
    public static TIMResult MsgGetGroupMessageReadMemberListWeb(Message message, TIMGroupMessageReadMembersFilter filter, string next_seq, int count, IMWebSDK.MsgGroupMessageReadMemberListCallback callback)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      string user_data = fn_name + "_" + Utils.getRandomStr();
      string msg = Utils.ToJson(message);
      MsgGroupMessageReadMemberListCallbackWebStore = (Delegate)callback;
      int res = IMWebSDK.TIMMsgGetGroupMessageReadMemberListWeb(Utils.string2intptr(msg), filter, Utils.string2intptr(next_seq), count, TIMMsgGroupMessageReadMemberListCallbackInstanceWeb, Utils.string2intptr(user_data));
      return (TIMResult)res;
    }
#endif

    /// <summary>
    /// ???????????????????????????
    /// Add receiving new message callback
    /// <para>??????????????????????????????ImSDK???????????????????????????????????????????????????????????????????????????????????????????????????????????????????????? (If logged in, ImSDK will retrieve messages send to you. PS. It doesn't have to be the unread messages)</para>
    /// <para>????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????(Only messages that aren't stored in the local storage will be retrieved. (Eg. Message read from other platform, and retrieve latest conversation's last message, if it's not in the local storage, the message will appear in here))</para>
    /// <para>????????????????????????ImSDK???????????????????????????????????????????????????????????????????????????????????????????????? (Once logged in, ImSDK will retrieve offline messages. Register this callback before log in to prevent missing messages)</para>
    /// </summary>
    /// <param name="callback">?????? RecvNewMsgCallback</param>
    public static void AddRecvNewMsgCallback(RecvNewMsgCallback callback = null, RecvNewMsgStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      if (stringCallback == null || callback != null)
      {
        IsStringCallbackStore.Remove(fn_name);
      }
      else
      {
        IsStringCallbackStore.Add(fn_name);
      }
      if (callback == null && stringCallback == null)
      {
        IMNativeSDK.TIMAddRecvNewMsgCallback(null, Utils.string2intptr(""));
        return;
      }
      string user_data = fn_name + "_" + Utils.getRandomStr();
      RecvNewMsgCallbackStore = callback != null ? (Delegate)callback : (Delegate)stringCallback;
      IMNativeSDK.TIMAddRecvNewMsgCallback(TIMRecvNewMsgCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// ???????????????????????????
    /// Remove receiving new message callback
    /// </summary>
    public static void RemoveRecvNewMsgCallback()
    {
      IMNativeSDK.TIMRemoveRecvNewMsgCallback(TIMRecvNewMsgCallbackInstance);
    }


    /// <summary>
    /// ??????????????????????????????
    /// Set message read receipt callback
    /// <para>?????????????????????????????????????????????[TIMMsgReportReaded]()?????????????????????????????????ImSDK??????????????????????????????????????? (Sender sends messages, and receiver report read messages by TIMMsgReportReaded, and the sender will be notified via this callback)</para>
    /// </summary>
    /// <param name="callback">?????? MsgReadedReceiptCallback</param>
    public static void SetMsgReadedReceiptCallback(MsgReadedReceiptCallback callback = null, MsgReadedReceiptStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      if (stringCallback == null || callback != null)
      {
        IsStringCallbackStore.Remove(fn_name);
      }
      else
      {
        IsStringCallbackStore.Add(fn_name);
      }
      if (callback == null && stringCallback == null)
      {
        IMNativeSDK.TIMSetMsgReadedReceiptCallback(null, Utils.string2intptr(""));
        return;
      }
      string user_data = fn_name + "_" + Utils.getRandomStr();
      MsgReadedReceiptCallbackStore = callback != null ? (Delegate)callback : (Delegate)stringCallback;
      IMNativeSDK.TIMSetMsgReadedReceiptCallback(TIMMsgReadedReceiptCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// ????????????????????????????????????
    /// Set message revoking callback
    /// <para>???????????????????????????????????????????????????????????????????????????[TIMMsgRevoke]()??????????????????????????????ImSDK??????????????????????????????????????? (Sender sends messages, and revoke the messages by TIMMsgRevoke, and the receiver will be notified via this callback)</para>
    /// </summary>
    /// <param name="callback">?????? MsgRevokeCallback</param>
    public static void SetMsgRevokeCallback(MsgRevokeCallback callback = null, MsgRevokeStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      if (stringCallback == null || callback != null)
      {
        IsStringCallbackStore.Remove(fn_name);
      }
      else
      {
        IsStringCallbackStore.Add(fn_name);
      }
      if (callback == null && stringCallback == null)
      {
        IMNativeSDK.TIMSetMsgRevokeCallback(null, Utils.string2intptr(""));
        return;
      }
      string user_data = fn_name + "_" + Utils.getRandomStr();
      MsgRevokeCallbackStore = callback != null ? (Delegate)callback : (Delegate)stringCallback;
      IMNativeSDK.TIMSetMsgRevokeCallback(TIMMsgRevokeCallbackInstance, Utils.string2intptr(user_data));
    }


    /// <summary>
    /// ???????????????????????????????????????????????????
    /// Set message element uploading progress callback
    /// <para>??????????????????????????????????????????????????????????????????????????????????????????????????????ImSDK????????????????????????????????????????????????????????????????????????????????????????????????????????? (Set message element uploading progress callback. When message element contains image, audio, file and video, the progress is notified via this callback)</para>
    /// </summary>
    /// <param name="callback">?????? MsgElemUploadProgressCallback</param>
    public static void SetMsgElemUploadProgressCallback(MsgElemUploadProgressCallback callback = null, MsgElemUploadProgressStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      if (stringCallback == null || callback != null)
      {
        IsStringCallbackStore.Remove(fn_name);
      }
      else
      {
        IsStringCallbackStore.Add(fn_name);
      }
      if (callback == null && stringCallback == null)
      {
        IMNativeSDK.TIMSetMsgElemUploadProgressCallback(null, Utils.string2intptr(""));
        return;
      }
      string user_data = fn_name + "_" + Utils.getRandomStr();
      MsgElemUploadProgressCallbackStore = callback != null ? (Delegate)callback : (Delegate)stringCallback;
      IMNativeSDK.TIMSetMsgElemUploadProgressCallback(TIMMsgElemUploadProgressCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// ??????????????????????????????
    /// Set group tips event callback
    /// <para>?????????????????????????????? ??????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????? (Group tips event includes: join group, quit group, remove from the group, grant admin, devolve admin, modify group info, modify group member info. This callback is for all the group members)</para>
    /// </summary>
    /// <param name="callback">?????? GroupTipsEventCallback</param>
    public static void SetGroupTipsEventCallback(GroupTipsEventCallback callback = null, GroupTipsEventStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      if (stringCallback == null || callback != null)
      {
        IsStringCallbackStore.Remove(fn_name);
      }
      else
      {
        IsStringCallbackStore.Add(fn_name);
      }
      if (callback == null && stringCallback == null)
      {
        IMNativeSDK.TIMSetGroupTipsEventCallback(null, Utils.string2intptr(""));
        return;
      }
      string user_data = fn_name + "_" + Utils.getRandomStr();
      GroupTipsEventCallbackStore = callback != null ? (Delegate)callback : (Delegate)stringCallback;
      IMNativeSDK.TIMSetGroupTipsEventCallback(TIMGroupTipsEventCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// ??????????????????????????????
    /// Set group attribute changed callback
    /// <para>????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????(If some attributes from a joined group are modified, all members from that group will receive the all the modified attributes)</para>
    /// </summary>
    /// <param name="callback">?????? GroupAttributeChangedCallback</param>
    public static void SetGroupAttributeChangedCallback(GroupAttributeChangedCallback callback = null, GroupAttributeChangedStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      if (stringCallback == null || callback != null)
      {
        IsStringCallbackStore.Remove(fn_name);
      }
      else
      {
        IsStringCallbackStore.Add(fn_name);
      }
      if (callback == null && stringCallback == null)
      {
        IMNativeSDK.TIMSetGroupAttributeChangedCallback(null, Utils.string2intptr(""));
        return;
      }
      string user_data = fn_name + "_" + Utils.getRandomStr();
      GroupAttributeChangedCallbackStore = callback != null ? (Delegate)callback : (Delegate)stringCallback;
      IMNativeSDK.TIMSetGroupAttributeChangedCallback(TIMGroupAttributeChangedCallbackInstance, Utils.string2intptr(user_data));
    }


    /// <summary>
    /// ????????????????????????
    /// Set conversation event callback
    /// <para>????????????????????? (Includes: )</para>
    /// <para>???????????? (New conversation)</para>
    /// <para>???????????? (Delete conversation)</para>
    /// <para>???????????? (Update conversation)</para>
    /// <para>???????????? (Start conversation)</para>
    /// <para>???????????? (End conversation)</para>
    /// <para>???????????????????????????????????????????????????????????????????????????????????????[TIMConvCreate]()????????????????????????????????????????????????????????? (Every new conversation event will trigger new conversation event, eg. create conversation via TIMConvCreate, or receive the first message from unknown conversation and etc.)</para>
    /// <para>?????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????? (Every event on the exist conversation will cause update conversatin event, eg. receiving new message from conversation, message revoked, message read reported)</para>
    /// <para>????????????[TIMConvDelete]()???????????????????????????????????????????????? (Use TIMConvDelete to delete conversation will trigger delete conversation event)</para>
    /// </summary>
    /// <param name="callback">?????? ConvEventCallback</param>
    public static void SetConvEventCallback(ConvEventCallback callback = null, ConvEventStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      if (stringCallback == null || callback != null)
      {
        IsStringCallbackStore.Remove(fn_name);
      }
      else
      {
        IsStringCallbackStore.Add(fn_name);
      }
      if (callback == null && stringCallback == null)
      {
        IMNativeSDK.TIMSetConvEventCallback(null, Utils.string2intptr(""));
        return;
      }
      string user_data = fn_name + "_" + Utils.getRandomStr();
      ConvEventCallbackStore = callback != null ? (Delegate)callback : (Delegate)stringCallback;
      IMNativeSDK.TIMSetConvEventCallback(TIMConvEventCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// ?????????????????????????????????????????????
    /// Set conversation total unread message count changed callback
    /// </summary>
    /// <param name="callback">?????? ConvTotalUnreadMessageCountChangedCallback</param>
    public static void SetConvTotalUnreadMessageCountChangedCallback(ConvTotalUnreadMessageCountChangedCallback callback)
    {
      if (callback != null)
      {
        string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

        string user_data = fn_name + "_" + Utils.getRandomStr();

        ConvTotalUnreadMessageCountChangedCallbackStore = callback;

        IMNativeSDK.TIMSetConvTotalUnreadMessageCountChangedCallback(TIMConvTotalUnreadMessageCountChangedCallbackInstance, Utils.string2intptr(user_data));
      }
      else
      {
        IMNativeSDK.TIMSetConvTotalUnreadMessageCountChangedCallback(null, Utils.string2intptr(""));
      }


    }

    /// <summary>
    /// ????????????????????????????????????
    /// Set network status changed callback
    /// <para>??????????????? Init() ??????ImSDK????????????????????????????????????????????????????????????????????????????????? (When called Init(), ImSDK will connect the server and listen to network staus.)</para>
    /// <para>????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????ImSDK?????????????????????IM???Server???????????? (Network status contains four stages: Connecting, Failed, Connect Success, Connected. The network status is only responsible for the indication of the connection between ImSDK and the IM server.)</para>
    /// <para>????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????? (This is optional callback, if you concern about network status changing, use this callback to inform the conncetion is connected or broke. Besides, you can set callback to retrieve mesasges once reconnected.)</para>
    /// <para>?????????????????????????????????ImSDK???????????????????????????????????????????????? (Once logged in, ImSDK will attemp reconnect automatically)</para>
    /// </summary>
    /// <param name="callback">?????? NetworkStatusListenerCallback</param>
    public static void SetNetworkStatusListenerCallback(NetworkStatusListenerCallback callback)
    {
      if (callback != null)
      {
        string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

        string user_data = fn_name + "_" + Utils.getRandomStr();

        NetworkStatusListenerCallbackStore = callback;

        IMNativeSDK.TIMSetNetworkStatusListenerCallback(TIMNetworkStatusListenerCallbackInstance, Utils.string2intptr(user_data));
      }
      else
      {
        IMNativeSDK.TIMSetNetworkStatusListenerCallback(null, Utils.string2intptr(""));
      }


    }

    /// <summary>
    /// ??????????????????????????????
    /// Set user kicked offline callback
    ///  <para>????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????(Once an account is logged on other platform, the previous login is nullified. And the previous platform will receive this callback. (You may force the previous one exit the APP or rekick))</para>
    ///  <para>???????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????ERR_IMSDK_KICKED_BY_OTHERS???6208????????????????????????????????????????????????????????????????????? (If kicked offline when offline, then the next login will fail and you may give an emphasized error (Error codeERR_IMSDK_KICKED_BY_OTHERS: 6208) or just ignore it)</para>
    ///  <para>???????????????????????????????????????(Online user inter-kick situation:)</para>
    ///  <para>???????????????1??????????????????????????????????????????????????????2?????????????????????????????????1???????????????????????? KickedOfflineCallback ??????(Logged on Device1, then log on Device2, Device1 will get KickedOfflineCallback)</para>
    ///  <para>???????????????1???????????????????????????????????????????????????login?????????????????????2????????????????????????????????????????????? (After receiving the callback, you may allow Device1 to login and force Device2 offline.)</para>
    ///  <para>????????????????????????: (Offline user inter-kick situation:)</para>
    ///  <para>???????????????1?????????????????????logout??????????????????????????????????????????2???????????????????????????????????????????????????????????????(Logged on Device1, go offline without logged out. And you logged on Device2, no callbacks)</para>
    ///  <para>?????????????????????????????????????????????????????????????????????1??????????????????????????????ERR_IMSDK_KICKED_BY_OTHERS???6208??????????????????????????????????????????????????????????????????(When Device1 relogged in, it will receive Error codeERR_IMSDK_KICKED_BY_OTHERS: 6208 to inform it's kicked by another one.)</para>
    ///  <para>??????????????????????????????login?????????????????????2?????????????????????????????? KickedOfflineCallback ?????? (If needed, Device1 login again, and Device2 will receive KickedOfflineCallback)</para>
    /// </summary>
    /// <param name="callback">?????? KickedOfflineCallback</param>
    public static void SetKickedOfflineCallback(KickedOfflineCallback callback)
    {
      if (callback != null)
      {
        string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

        string user_data = fn_name + "_" + Utils.getRandomStr();

        KickedOfflineCallbackStore = callback;

        IMNativeSDK.TIMSetKickedOfflineCallback(TIMKickedOfflineCallbackInstance, Utils.string2intptr(user_data));
      }
      else
      {
        IMNativeSDK.TIMSetKickedOfflineCallback(null, Utils.string2intptr(""));
      }


    }

    /// <summary>
    /// ????????????????????????
    /// Set user signature expired callback
    /// <para>???????????????????????????????????????????????????????????????????????????????????????????????????????????? (User sig may expire and once it happens, this is invoked)</para>
    /// <para>Login()???????????????70001????????????????????????????????????????????????????????????????????????????????? (Login() may return Error Code 70001 to inform you to update the user signature)</para>
    /// </summary>
    /// <param name="callback">?????? UserSigExpiredCallback</param>
    public static void SetUserSigExpiredCallback(UserSigExpiredCallback callback)
    {
      if (callback != null)
      {
        string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

        string user_data = fn_name + "_" + Utils.getRandomStr();

        UserSigExpiredCallbackStore = callback;

        IMNativeSDK.TIMSetUserSigExpiredCallback(TIMUserSigExpiredCallbackInstance, Utils.string2intptr(user_data));
      }
      else
      {
        IMNativeSDK.TIMSetUserSigExpiredCallback(null, Utils.string2intptr(""));
      }


    }

    /// <summary>
    /// ???????????????????????????
    /// Set add friend listener
    /// <para>???????????????????????????????????????A?????????B?????????????????????????????????ImSDK???A????????????????????????B??????ImSDK?????????????????????????????????ImSDK?????????????????????????????? (This is for multi-platform synchronization. Eg. A,B logged with the same account, and A adds a new friend, then B will receive this callback.)</para>
    /// </summary>
    /// <param name="callback">?????? OnAddFriendCallback</param>
    public static void SetOnAddFriendCallback(OnAddFriendCallback callback = null, OnAddFriendStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      if (stringCallback == null || callback != null)
      {
        IsStringCallbackStore.Remove(fn_name);
      }
      else
      {
        IsStringCallbackStore.Add(fn_name);
      }
      if (callback == null && stringCallback == null)
      {
        IMNativeSDK.TIMSetOnAddFriendCallback(null, Utils.string2intptr(""));
        return;
      }
      string user_data = fn_name + "_" + Utils.getRandomStr();
      OnAddFriendCallbackStore = callback != null ? (Delegate)callback : (Delegate)stringCallback;
      IMNativeSDK.TIMSetOnAddFriendCallback(TIMOnAddFriendCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// ???????????????????????????
    /// Set delete friend listener
    /// <para>???????????????????????????????????????A?????????B?????????????????????????????????ImSDK???A????????????????????????B??????ImSDK?????????????????????????????????ImSDK?????????????????????????????? (This is for multi-platform synchronization. Eg. A,B logged with the same account, and A deletes a friend, then B will receive this callback.)</para>
    /// </summary>
    /// <param name="callback">?????? OnDeleteFriendCallback</param>
    public static void SetOnDeleteFriendCallback(OnDeleteFriendCallback callback = null, OnDeleteFriendStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      if (stringCallback == null || callback != null)
      {
        IsStringCallbackStore.Remove(fn_name);
      }
      else
      {
        IsStringCallbackStore.Add(fn_name);
      }
      if (callback == null && stringCallback == null)
      {
        IMNativeSDK.TIMSetOnDeleteFriendCallback(null, Utils.string2intptr(""));
        return;
      }
      string user_data = fn_name + "_" + Utils.getRandomStr();
      SetOnDeleteFriendCallbackUser_Data = user_data;
      OnDeleteFriendCallbackStore = callback != null ? (Delegate)callback : (Delegate)stringCallback;
      IMNativeSDK.TIMSetOnDeleteFriendCallback(TIMOnDeleteFriendCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// ?????????????????????????????????
    /// Set update friend's profile callback
    /// <para>???????????????????????????????????????A?????????B?????????????????????????????????ImSDK???A??????????????????????????????B??????ImSDK???????????????????????????????????????ImSDK?????????????????????????????? (This is for multi-platform synchronization. Eg. A,B logged with the same account, and A updates a friend's profile, then B will receive this callback.)</para>
    /// </summary>
    /// <param name="callback">?????? UpdateFriendProfileCallback</param>
    public static void SetUpdateFriendProfileCallback(UpdateFriendProfileCallback callback = null, UpdateFriendProfileStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      if (stringCallback == null || callback != null)
      {
        IsStringCallbackStore.Remove(fn_name);
      }
      else
      {
        IsStringCallbackStore.Add(fn_name);
      }
      if (callback == null && stringCallback == null)
      {
        IMNativeSDK.TIMSetUpdateFriendProfileCallback(null, Utils.string2intptr(""));
        return;
      }
      string user_data = fn_name + "_" + Utils.getRandomStr();
      UpdateFriendProfileCallbackStore = callback != null ? (Delegate)callback : (Delegate)stringCallback;
      IMNativeSDK.TIMSetUpdateFriendProfileCallback(TIMUpdateFriendProfileCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// ?????????????????????????????????
    /// Set friend request callback
    /// <para>???????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????ImSDK????????????????????????????????????????????????????????????????????????????????????????????????????????? (If current user's friend request needs confirmation, and other applies a friend request to the user, the user will receive thiss callback. If you logged on multi-platforms, each of them will trigger this callback)</para>
    /// </summary>
    /// <param name="callback">?????? FriendAddRequestCallback</param>
    public static void SetFriendAddRequestCallback(FriendAddRequestCallback callback = null, FriendAddRequestStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      if (stringCallback == null || callback != null)
      {
        IsStringCallbackStore.Remove(fn_name);
      }
      else
      {
        IsStringCallbackStore.Add(fn_name);
      }
      if (callback == null && stringCallback == null)
      {
        IMNativeSDK.TIMSetFriendAddRequestCallback(null, Utils.string2intptr(""));
        return;
      }
      string user_data = fn_name + "_" + Utils.getRandomStr();
      FriendAddRequestCallbackStore = callback != null ? (Delegate)callback : (Delegate)stringCallback;
      IMNativeSDK.TIMSetFriendAddRequestCallback(TIMFriendAddRequestCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// ????????????????????????????????????
    /// Set friend application list deleted callback
    /// <para>1. ???????????????????????? (1. Delete friend application)</para>
    /// <para>2. ?????????????????? (2. Reject friend request application)</para>
    /// <para>3. ?????????????????? (3. Grant friend request application)</para>
    /// <para>4. ?????????????????????????????? (4. Apply other friend request and get rejected)</para>
    /// </summary>
    /// <param name="callback">?????? FriendApplicationListDeletedCallback</param>
    public static void SetFriendApplicationListDeletedCallback(FriendApplicationListDeletedCallback callback = null, FriendApplicationListDeletedStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      if (stringCallback == null || callback != null)
      {
        IsStringCallbackStore.Remove(fn_name);
      }
      else
      {
        IsStringCallbackStore.Add(fn_name);
      }
      if (callback == null && stringCallback == null)
      {
        IMNativeSDK.TIMSetFriendApplicationListDeletedCallback(null, Utils.string2intptr(""));
        return;
      }
      string user_data = fn_name + "_" + Utils.getRandomStr();
      FriendApplicationListDeletedCallbackStore = callback != null ? (Delegate)callback : (Delegate)stringCallback;
      IMNativeSDK.TIMSetFriendApplicationListDeletedCallback(TIMFriendApplicationListDeletedCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// ?????????????????????????????????
    /// Set friend application list read callback
    /// <para>???????????? setFriendApplicationRead ????????????????????????????????????????????????????????????????????????????????????(Once use setFriendApplicationRead to set friend application read, this will be triggered)</para>
    /// </summary>
    /// <param name="callback">?????? FriendApplicationListReadCallback</param>
    public static void SetFriendApplicationListReadCallback(FriendApplicationListReadCallback callback)
    {

      if (callback != null)
      {
        string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

        string user_data = fn_name + "_" + Utils.getRandomStr();

        FriendApplicationListReadCallbackStore = callback;

        IMNativeSDK.TIMSetFriendApplicationListReadCallback(TIMFriendApplicationListReadCallbackInstance, Utils.string2intptr(user_data));
      }
      else
      {
        IMNativeSDK.TIMSetFriendApplicationListReadCallback(null, Utils.string2intptr(""));
      }

    }

    /// <summary>
    /// ??????????????????????????????
    /// Set blacklist added new user callback
    /// </summary>
    /// <param name="callback">?????? FriendBlackListAddedCallback</param>
    public static void SetFriendBlackListAddedCallback(FriendBlackListAddedCallback callback = null, FriendBlackListAddedStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      if (stringCallback == null || callback != null)
      {
        IsStringCallbackStore.Remove(fn_name);
      }
      else
      {
        IsStringCallbackStore.Add(fn_name);
      }
      if (callback == null && stringCallback == null)
      {
        IMNativeSDK.TIMSetFriendBlackListAddedCallback(null, Utils.string2intptr(""));
        return;
      }
      string user_data = fn_name + "_" + Utils.getRandomStr();
      FriendBlackListAddedCallbackStore = callback != null ? (Delegate)callback : (Delegate)stringCallback;
      IMNativeSDK.TIMSetFriendBlackListAddedCallback(TIMFriendBlackListAddedCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// ??????????????????????????????
    /// Set blacklist deleted user callback
    /// </summary>
    /// <param name="callback">?????? FriendBlackListDeletedCallback</param>
    public static void SetFriendBlackListDeletedCallback(FriendBlackListDeletedCallback callback = null, FriendBlackListDeletedStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      if (stringCallback == null || callback != null)
      {
        IsStringCallbackStore.Remove(fn_name);
      }
      else
      {
        IsStringCallbackStore.Add(fn_name);
      }
      if (callback == null && stringCallback == null)
      {
        IMNativeSDK.TIMSetFriendBlackListDeletedCallback(null, Utils.string2intptr(""));
        return;
      }
      string user_data = fn_name + "_" + Utils.getRandomStr();
      FriendBlackListDeletedCallbackStore = callback != null ? (Delegate)callback : (Delegate)stringCallback;
      IMNativeSDK.TIMSetFriendBlackListDeletedCallback(TIMFriendBlackListDeletedCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// ??????????????????
    /// Set log callback
    /// <para>????????????????????????????????????ImSDK??????????????????????????????????????????????????? (Once set, ImSDK's internal log will go through this callback)</para>
    /// <para>???????????????????????????SetConfig()?????????????????????????????????????????????????????? (Developer calls SetConfig() to config log level)</para>
    /// </summary>
    /// <param name="callback">?????? LogCallback</param>
    public static void SetLogCallback(LogCallback callback)
    {

      if (callback != null)
      {
        string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;

        string user_data = fn_name + "_" + Utils.getRandomStr();

        LogCallbackStore = callback;

        IMNativeSDK.TIMSetLogCallback(TIMLogCallbackInstance, Utils.string2intptr(user_data));
      }
      else
      {
        IMNativeSDK.TIMSetLogCallback(null, Utils.string2intptr(""));
      }

    }

    /// <summary>
    /// ????????????????????????????????????????????????????????????????????????
    /// Set message updated on the cloud callback
    /// <para> ????????????????????????????????????????????????ImSDK?????????????????????????????? (After you modify a message, ImSDK informs you by this callback)</para>
    /// <para> ????????????????????????????????????????????????????????????IM?????? [???????????????????????????](https://cloud.tencent.com/document/product/269/1632) (You can intercept messages on your own server [Callback Before Sending a One-to-One Message](https://www.tencentcloud.com/document/product/1047/34364))</para>
    /// <para> ?????????????????????????????????IM?????????????????????????????????????????????????????????????????????????????????????????? (If you intercept messages, the IM server will transmit all the message to your server)</para>
    /// <para> ????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????ImSDK?????????????????????????????? (Your server may modify the message (eg. content moderation), if your server modifies the message, ImSDK will notify you via this callback)</para>
    /// </summary>
    /// <param name="callback">?????? MsgUpdateCallback</param>
    public static void SetMsgUpdateCallback(MsgUpdateCallback callback = null, MsgUpdateStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      if (stringCallback == null || callback != null)
      {
        IsStringCallbackStore.Remove(fn_name);
      }
      else
      {
        IsStringCallbackStore.Add(fn_name);
      }
      if (callback == null && stringCallback == null)
      {
        IMNativeSDK.TIMSetMsgUpdateCallback(null, Utils.string2intptr(""));
        return;
      }
      string user_data = fn_name + "_" + Utils.getRandomStr();
      MsgUpdateCallbackStore = callback != null ? (Delegate)callback : (Delegate)stringCallback;
      IMNativeSDK.TIMSetMsgUpdateCallback(TIMMsgUpdateCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// ????????????????????????
    /// Set group topic created callback
    /// </summary>
    /// <param name="callback">?????? GroupTopicCreatedCallback</param>
    public static void SetGroupTopicCreatedCallback(GroupTopicCreatedCallback callback)
    {
      if (callback != null)
      {
        string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
        string user_data = fn_name + "_" + Utils.getRandomStr();
        GroupTopicCreatedCallbackStore = callback;

        IMNativeSDK.TIMSetGroupTopicCreatedCallback(TIMGroupTopicCreatedCallbackInstance, Utils.string2intptr(user_data));
      }
      else
      {
        IMNativeSDK.TIMSetGroupTopicCreatedCallback(null, Utils.string2intptr(""));
      }
    }

    /// <summary>
    /// ???????????????????????????
    /// Set group topic deleted callback
    /// </summary>
    /// <param name="callback">?????? GroupTopicDeletedCallback</param>
    public static void SetGroupTopicDeletedCallback(GroupTopicDeletedCallback callback = null, GroupTopicDeletedStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      if (stringCallback == null || callback != null)
      {
        IsStringCallbackStore.Remove(fn_name);
      }
      else
      {
        IsStringCallbackStore.Add(fn_name);
      }
      if (callback == null && stringCallback == null)
      {
        IMNativeSDK.TIMSetGroupTopicDeletedCallback(null, Utils.string2intptr(""));
        return;
      }
      string user_data = fn_name + "_" + Utils.getRandomStr();
      GroupTopicDeletedCallbackStore = callback != null ? (Delegate)callback : (Delegate)stringCallback;
      IMNativeSDK.TIMSetGroupTopicDeletedCallback(TIMGroupTopicDeletedCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// ????????????????????????
    /// Set group topic updated callback
    /// </summary>
    /// <param name="callback">?????? GroupTopicChangedCallback</param>
    public static void SetGroupTopicChangedCallback(GroupTopicChangedCallback callback = null, GroupTopicChangedStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      if (stringCallback == null || callback != null)
      {
        IsStringCallbackStore.Remove(fn_name);
      }
      else
      {
        IsStringCallbackStore.Add(fn_name);
      }
      if (callback == null && stringCallback == null)
      {
        IMNativeSDK.TIMSetGroupTopicChangedCallback(null, Utils.string2intptr(""));
        return;
      }
      string user_data = fn_name + "_" + Utils.getRandomStr();
      GroupTopicChangedCallbackStore = callback != null ? (Delegate)callback : (Delegate)stringCallback;
      IMNativeSDK.TIMSetGroupTopicChangedCallback(TIMGroupTopicChangedCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// ???????????????????????????????????????????????????
    /// Set self info updated callback
    /// </summary>
    /// <param name="callback">?????? SelfInfoUpdatedCallback</param>
    public static void SetSelfInfoUpdatedCallback(SelfInfoUpdatedCallback callback = null, SelfInfoUpdatedStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      if (stringCallback == null || callback != null)
      {
        IsStringCallbackStore.Remove(fn_name);
      }
      else
      {
        IsStringCallbackStore.Add(fn_name);
      }
      if (callback == null && stringCallback == null)
      {
        IMNativeSDK.TIMSetSelfInfoUpdatedCallback(null, Utils.string2intptr(""));
        return;
      }
      string user_data = fn_name + "_" + Utils.getRandomStr();
      SelfInfoUpdatedCallbackStore = callback != null ? (Delegate)callback : (Delegate)stringCallback;
      IMNativeSDK.TIMSetSelfInfoUpdatedCallback(TIMSelfInfoUpdatedCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// ????????????????????????????????????
    /// Set user status updated callback
    /// <param>????????????????????????(The following cases may trigger this callback:)</param>
    /// <param>1. ?????????????????????????????????????????????????????????????????????????????????????????????????????? (1. Subscribed user status changed (Include online status and custom status))</param>
    /// <param>2. ??? IM ??????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????? (2. Enable friend's status notification on the IM Console. It will trigger this callback even you haven't subscribe it)</param>
    /// <param>3. ?????????????????????????????????????????????????????????????????????????????????????????????????????????????????? (An account logged on multi-platforms, and the status is changed on one of the devices, all platforms will receive this callback)</param>
    /// </summary>
    /// <param name="callback">?????? UserStatusChangedCallback</param>
    public static void SetUserStatusChangedCallback(UserStatusChangedCallback callback = null, UserStatusChangedStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      if (stringCallback == null || callback != null)
      {
        IsStringCallbackStore.Remove(fn_name);
      }
      else
      {
        IsStringCallbackStore.Add(fn_name);
      }
      if (callback == null && stringCallback == null)
      {
        IMNativeSDK.TIMSetUserStatusChangedCallback(null, Utils.string2intptr(""));
        return;
      }
      string user_data = fn_name + "_" + Utils.getRandomStr();
      UserStatusChangedCallbackStore = callback != null ? (Delegate)callback : (Delegate)stringCallback;
      IMNativeSDK.TIMSetUserStatusChangedCallback(TIMUserStatusChangedCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// ???????????????????????????????????????
    /// Set message extensions updated callback
    /// </summary>
    /// <param name="callback">?????? MsgExtensionsChangedCallback</param>
    public static void SetMsgExtensionsChangedCallback(MsgExtensionsChangedCallback callback = null, MsgExtensionsChangedStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      if (stringCallback == null || callback != null)
      {
        IsStringCallbackStore.Remove(fn_name);
      }
      else
      {
        IsStringCallbackStore.Add(fn_name);
      }
      if (callback == null && stringCallback == null)
      {
        IMNativeSDK.TIMSetMsgExtensionsChangedCallback(null, Utils.string2intptr(""));
        return;
      }
      string user_data = fn_name + "_" + Utils.getRandomStr();
      MsgExtensionsChangedCallbackStore = callback != null ? (Delegate)callback : (Delegate)stringCallback;
      IMNativeSDK.TIMSetMsgExtensionsChangedCallback(TIMMsgExtensionsChangedCallbackInstance, Utils.string2intptr(user_data));
    }

    /// <summary>
    /// ???????????????????????????????????????
    /// Set message extensions deleted callback
    /// </summary>
    /// <param name="callback">?????? MsgExtensionsDeletedCallback</param>
    public static void SetMsgExtensionsDeletedCallback(MsgExtensionsDeletedCallback callback = null, MsgExtensionsDeletedStringCallback stringCallback = null)
    {
      string fn_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
      if (stringCallback == null || callback != null)
      {
        IsStringCallbackStore.Remove(fn_name);
      }
      else
      {
        IsStringCallbackStore.Add(fn_name);
      }
      if (callback == null && stringCallback == null)
      {
        IMNativeSDK.TIMSetMsgExtensionsDeletedCallback(null, Utils.string2intptr(""));
        return;
      }
      string user_data = fn_name + "_" + Utils.getRandomStr();
      MsgExtensionsDeletedCallbackStore = callback != null ? (Delegate)callback : (Delegate)stringCallback;
      IMNativeSDK.TIMSetMsgExtensionsDeletedCallback(TIMMsgExtensionsDeletedCallbackInstance, Utils.string2intptr(user_data));
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
              if (ValuecallbackStore.TryGetValue(data.user_data, out Delegate callbackDele))
              {
                // 3 means no need for callback data
                if (callbackDele.GetMethodInfo().GetParameters().Length == 3)
                {
                  callbackDele.DynamicInvoke(data.code, data.desc, data.user_data);
                }
                else
                {
                  var isFoundDele = ValuecallbackDeleStore.Remove(data.user_data);
                  // Debug.Log($"data.user_data: {data.user_data} typeof(T): {typeof(T).Name} FoundDele: {isFoundDele}");
                  if (isFoundDele)
                  {
                    callbackDele.DynamicInvoke(data.code, data.desc, Utils.FromJson<T>(data.data), data.user_data);
                  }
                  else
                  {
                    callbackDele.DynamicInvoke(data.code, data.desc, data.data, data.user_data);
                  }
                }
                ValuecallbackStore.Remove(data.user_data);
              }

            }
            break;
          case "TIMRecvNewMsgCallback":
            if (RecvNewMsgCallbackStore != null)
            {
              if (typeof(T) == typeof(object))
              {
                RecvNewMsgCallbackStore.DynamicInvoke(Utils.FromJson<List<Message>>(data.data), data.user_data);
              }
              else
              {
                RecvNewMsgCallbackStore.DynamicInvoke(data.data, data.user_data);
              }
            }
            break;
          case "TIMMsgReadedReceiptCallback":
            if (MsgReadedReceiptCallbackStore != null)
            {
              if (typeof(T) == typeof(object))
              {
                MsgReadedReceiptCallbackStore.DynamicInvoke(Utils.FromJson<List<MessageReceipt>>(data.data), data.user_data);
              }
              else
              {
                MsgReadedReceiptCallbackStore.DynamicInvoke(data.data, data.user_data);
              }
            }
            break;
          case "TIMMsgRevokeCallback":
            if (MsgRevokeCallbackStore != null)
            {
              if (typeof(T) == typeof(object))
              {
                MsgRevokeCallbackStore.DynamicInvoke(Utils.FromJson<List<MsgLocator>>(data.data), data.user_data);
              }
              else
              {
                MsgRevokeCallbackStore.DynamicInvoke(data.data, data.user_data);
              }
            }
            break;
          case "TIMMsgElemUploadProgressCallback":
            if (MsgElemUploadProgressCallbackStore != null)
            {
              if (typeof(T) == typeof(object))
              {
                MsgElemUploadProgressCallbackStore.DynamicInvoke(Utils.FromJson<Message>(data.data), data.index, data.cur_size, data.total_size, data.user_data);
              }
              else
              {
                MsgElemUploadProgressCallbackStore.DynamicInvoke(data.data, data.index, data.cur_size, data.total_size, data.user_data);
              }

            }
            break;
          case "TIMGroupTipsEventCallback":

            if (GroupTipsEventCallbackStore != null)
            {
              if (typeof(T) == typeof(object))
              {
                GroupTipsEventCallbackStore.DynamicInvoke(Utils.FromJson<GroupTipsElem>(data.data), data.user_data);
              }
              else
              {
                GroupTipsEventCallbackStore.DynamicInvoke(data.data, data.user_data);
              }
            }
            break;
          case "TIMGroupAttributeChangedCallback":

            if (GroupAttributeChangedCallbackStore != null)
            {
              if (typeof(T) == typeof(object))
              {
                GroupAttributeChangedCallbackStore.DynamicInvoke(data.group_id, Utils.FromJson<List<GroupAttributes>>(data.data), data.user_data);
              }
              else
              {
                GroupAttributeChangedCallbackStore.DynamicInvoke(data.group_id, data.data, data.user_data);
              }
            }
            break;
          case "TIMConvEventCallback":

            if (ConvEventCallbackStore != null)
            {
              if (typeof(T) == typeof(object))
              {
                ConvEventCallbackStore.DynamicInvoke((TIMConvEvent)data.conv_event, Utils.FromJson<List<ConvInfo>>(data.data), data.user_data);
              }
              else
              {
                ConvEventCallbackStore.DynamicInvoke((TIMConvEvent)data.conv_event, data.data, data.user_data);
              }
            }


            break;
          case "TIMConvTotalUnreadMessageCountChangedCallback":

            if (ConvTotalUnreadMessageCountChangedCallbackStore != null)
            {
              ConvTotalUnreadMessageCountChangedCallbackStore(data.code, data.user_data);

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
              if (typeof(T) == typeof(object))
              {
                OnAddFriendCallbackStore.DynamicInvoke(Utils.FromJson<List<string>>(data.data), data.user_data);
              }
              else
              {
                OnAddFriendCallbackStore.DynamicInvoke(data.data, data.user_data);
              }
            }
            break;
          case "TIMOnDeleteFriendCallback":
            if (OnDeleteFriendCallbackStore != null)
            {
              if (typeof(T) == typeof(object))
              {
                OnDeleteFriendCallbackStore.DynamicInvoke(Utils.FromJson<List<string>>(data.data), data.user_data);
              }
              else
              {
                OnDeleteFriendCallbackStore.DynamicInvoke(data.data, data.user_data);
              }
            }
            break;
          case "TIMUpdateFriendProfileCallback":

            if (UpdateFriendProfileCallbackStore != null)
            {
              if (typeof(T) == typeof(object))
              {
                UpdateFriendProfileCallbackStore.DynamicInvoke(Utils.FromJson<List<FriendProfileItem>>(data.data), data.user_data);
              }
              else
              {
                UpdateFriendProfileCallbackStore.DynamicInvoke(data.data, data.user_data);
              }
            }
            break;

          case "TIMFriendAddRequestCallback":

            if (FriendAddRequestCallbackStore != null)
            {
              if (typeof(T) == typeof(object))
              {
                FriendAddRequestCallbackStore.DynamicInvoke(Utils.FromJson<List<FriendAddPendency>>(data.data), data.user_data);
              }
              else
              {
                FriendAddRequestCallbackStore.DynamicInvoke(data.data, data.user_data);
              }
            }
            break;
          case "TIMFriendApplicationListDeletedCallback":

            if (FriendApplicationListDeletedCallbackStore != null)
            {
              if (typeof(T) == typeof(object))
              {
                FriendApplicationListDeletedCallbackStore.DynamicInvoke(Utils.FromJson<List<string>>(data.data), data.user_data);
              }
              else
              {
                FriendApplicationListDeletedCallbackStore.DynamicInvoke(data.data, data.user_data);
              }
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
              if (typeof(T) == typeof(object))
              {
                FriendBlackListAddedCallbackStore.DynamicInvoke(Utils.FromJson<List<FriendProfile>>(data.data), data.user_data);
              }
              else
              {
                FriendBlackListAddedCallbackStore.DynamicInvoke(data.data, data.user_data);
              }
            }
            break;
          case "TIMFriendBlackListDeletedCallback":

            if (FriendBlackListDeletedCallbackStore != null)
            {
              if (typeof(T) == typeof(object))
              {
                FriendBlackListDeletedCallbackStore.DynamicInvoke(Utils.FromJson<List<string>>(data.data), data.user_data);
              }
              else
              {
                FriendBlackListDeletedCallbackStore.DynamicInvoke(data.data, data.user_data);
              }
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
              if (typeof(T) == typeof(object))
              {
                MsgUpdateCallbackStore.DynamicInvoke(Utils.FromJson<List<Message>>(data.data), data.user_data);
              }
              else
              {
                MsgUpdateCallbackStore.DynamicInvoke(data.data, data.user_data);
              }
            }
            break;
          case "TIMMsgGroupMessageReadMemberListCallback":

            if (MsgGroupMessageReadMemberListCallbackStore != null)
            {
              if (typeof(T) == typeof(object))
              {
                MsgGroupMessageReadMemberListCallbackStore.DynamicInvoke(Utils.FromJson<List<GroupMemberInfo>>(data.data), data.next_seq, data.is_finished, data.user_data);
              }
              else
              {
                MsgGroupMessageReadMemberListCallbackStore.DynamicInvoke(data.data, data.next_seq, data.is_finished, data.user_data);
              }
            }
            break;
          case "TIMMsgGroupMessageReadMemberListCallbackWeb":
            if (MsgGroupMessageReadMemberListCallbackWebStore != null)
            {
              MsgGroupMessageReadMemberListCallbackWebStore.DynamicInvoke(data.data, data.next_seq.ToString(), data.is_finished, data.user_data);
            }
            break;
          case "TIMGroupTopicCreatedCallback":

            if (GroupTopicCreatedCallbackStore != null)
            {
              GroupTopicCreatedCallbackStore.DynamicInvoke(data.group_id, data.data, data.user_data);
            }
            break;
          case "TIMGroupTopicDeletedCallback":

            if (GroupTopicDeletedCallbackStore != null)
            {
              if (typeof(T) == typeof(object))
              {
                GroupTopicDeletedCallbackStore.DynamicInvoke(data.group_id, Utils.FromJson<List<string>>(data.data), data.user_data);
              }
              else
              {
                GroupTopicDeletedCallbackStore.DynamicInvoke(data.group_id, data.data, data.user_data);
              }
            }
            break;
          case "TIMGroupTopicChangedCallback":

            if (GroupTopicChangedCallbackStore != null)
            {
              if (typeof(T) == typeof(object))
              {
                GroupTopicChangedCallbackStore.DynamicInvoke(data.group_id, Utils.FromJson<GroupTopicInfo>(data.data), data.user_data);
              }
              else
              {
                GroupTopicChangedCallbackStore.DynamicInvoke(data.group_id, data.data, data.user_data);
              }
            }
            break;
          case "TIMSelfInfoUpdatedCallback":

            if (SelfInfoUpdatedCallbackStore != null)
            {
              if (typeof(T) == typeof(object))
              {
                SelfInfoUpdatedCallbackStore.DynamicInvoke(Utils.FromJson<UserStatus>(data.data), data.user_data);
              }
              else
              {
                SelfInfoUpdatedCallbackStore.DynamicInvoke(data.data, data.user_data);
              }
            }
            break;
          case "TIMUserStatusChangedCallback":

            if (UserStatusChangedCallbackStore != null)
            {
              if (typeof(T) == typeof(object))
              {
                UserStatusChangedCallbackStore.DynamicInvoke(Utils.FromJson<List<UserStatus>>(data.data), data.user_data);
              }
              else
              {
                UserStatusChangedCallbackStore.DynamicInvoke(data.data, data.user_data);
              }
            }
            break;
          case "TIMMsgExtensionsChangedCallback":

            if (MsgExtensionsChangedCallbackStore != null)
            {
              if (typeof(T) == typeof(object))
              {
                MsgExtensionsChangedCallbackStore.DynamicInvoke(data.group_id, Utils.FromJson<List<MessageExtension>>(data.data), data.user_data);
              }
              else
              {
                MsgExtensionsChangedCallbackStore.DynamicInvoke(data.group_id, data.data, data.user_data);
              }
            }
            break;
          case "TIMMsgExtensionsDeletedCallback":

            if (MsgExtensionsDeletedCallbackStore != null)
            {
              if (typeof(T) == typeof(object))
              {
                MsgExtensionsDeletedCallbackStore.DynamicInvoke(data.group_id, Utils.FromJson<List<MessageExtension>>(data.data), data.user_data);
              }
              else
              {
                MsgExtensionsDeletedCallbackStore.DynamicInvoke(data.group_id, data.data, data.user_data);
              }
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
        if (!IsStringCallbackStore.Contains(user_data_string.Split('_')[0]))
        {
          mainSyncContext.Post(threadOperation<object>, cc);
        }
        else
        {
          mainSyncContext.Post(threadOperation<string>, cc);
        }
      }
      catch (Exception e)
      {
        Utils.Log("?????????????????????????????????" + e.ToString());
      }

    }


    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMMsgReadedReceiptCallback))]
    private static void TIMMsgReadedReceiptCallbackInstance(IntPtr json_msg_readed_receipt_array, IntPtr user_data)
    {

      string json_msg_readed_receipt_array_string = Utils.intptr2string(json_msg_readed_receipt_array);

      string user_data_string = Utils.intptr2string(user_data);

      CallbackConvert cc = new CallbackConvert { code = 0, type = "TIMMsgReadedReceiptCallback", data = json_msg_readed_receipt_array_string, user_data = user_data_string, desc = "" };
      if (!IsStringCallbackStore.Contains(user_data_string.Split('_')[0]))
      {
        mainSyncContext.Post(threadOperation<object>, cc);
      }
      else
      {
        mainSyncContext.Post(threadOperation<string>, cc);
      }

    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMMsgRevokeCallback))]
    private static void TIMMsgRevokeCallbackInstance(IntPtr json_msg_locator_array, IntPtr user_data)
    {
      string json_msg_locator_array_string = Utils.intptr2string(json_msg_locator_array);

      string user_data_string = Utils.intptr2string(user_data);
      CallbackConvert cc = new CallbackConvert { code = 0, type = "TIMMsgRevokeCallback", data = json_msg_locator_array_string, user_data = user_data_string, desc = "" };
      if (!IsStringCallbackStore.Contains(user_data_string.Split('_')[0]))
      {
        mainSyncContext.Post(threadOperation<object>, cc);
      }
      else
      {
        mainSyncContext.Post(threadOperation<string>, cc);
      }
    }


    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMMsgElemUploadProgressCallback))]
    private static void TIMMsgElemUploadProgressCallbackInstance(IntPtr json_msg, int index, int cur_size, int total_size, IntPtr user_data)
    {
      string json_msg_string = Utils.intptr2string(json_msg);

      string user_data_string = Utils.intptr2string(user_data);


      CallbackConvert cc = new CallbackConvert { code = 0, type = "TIMMsgElemUploadProgressCallback", data = json_msg_string, user_data = user_data_string, desc = "", index = index, cur_size = cur_size, total_size = total_size };
      if (!IsStringCallbackStore.Contains(user_data_string.Split('_')[0]))
      {
        mainSyncContext.Post(threadOperation<object>, cc);
      }
      else
      {
        mainSyncContext.Post(threadOperation<string>, cc);
      }


    }
    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMGroupTipsEventCallback))]
    private static void TIMGroupTipsEventCallbackInstance(IntPtr json_group_tip_array, IntPtr user_data)
    {
      string json_group_tip_array_string = Utils.intptr2string(json_group_tip_array);

      string user_data_string = Utils.intptr2string(user_data);
      CallbackConvert cc = new CallbackConvert { code = 0, type = "TIMGroupTipsEventCallback", data = json_group_tip_array_string, user_data = user_data_string, desc = "" };
      if (!IsStringCallbackStore.Contains(user_data_string.Split('_')[0]))
      {
        mainSyncContext.Post(threadOperation<object>, cc);
      }
      else
      {
        mainSyncContext.Post(threadOperation<string>, cc);
      }
    }


    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMGroupAttributeChangedCallback))]
    private static void TIMGroupAttributeChangedCallbackInstance(IntPtr group_id, IntPtr json_group_attibute_array, IntPtr user_data)
    {
      string json_group_attibute_array_string = Utils.intptr2string(json_group_attibute_array);

      string group_id_string = Utils.intptr2string(group_id);

      string user_data_string = Utils.intptr2string(user_data);

      CallbackConvert cc = new CallbackConvert { code = 0, type = "TIMGroupAttributeChangedCallback", data = json_group_attibute_array_string, user_data = user_data_string, group_id = group_id_string };
      if (!IsStringCallbackStore.Contains(user_data_string.Split('_')[0]))
      {
        mainSyncContext.Post(threadOperation<object>, cc);
      }
      else
      {
        mainSyncContext.Post(threadOperation<string>, cc);
      }
    }
    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMConvEventCallback))]
    private static void TIMConvEventCallbackInstance(int conv_event, IntPtr json_conv_array, IntPtr user_data)
    {
      string json_conv_array_string = Utils.intptr2string(json_conv_array);

      string user_data_string = Utils.intptr2string(user_data);

      CallbackConvert cc = new CallbackConvert { code = 0, type = "TIMConvEventCallback", data = json_conv_array_string, user_data = user_data_string, conv_event = conv_event };
      if (!IsStringCallbackStore.Contains(user_data_string.Split('_')[0]))
      {
        mainSyncContext.Post(threadOperation<object>, cc);
      }
      else
      {
        mainSyncContext.Post(threadOperation<string>, cc);
      }
    }


    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMConvTotalUnreadMessageCountChangedCallback))]
    private static void TIMConvTotalUnreadMessageCountChangedCallbackInstance(int total_unread_count, IntPtr user_data)
    {
      string user_data_string = Utils.intptr2string(user_data);
      mainSyncContext.Post(threadOperation<string>, new CallbackConvert { code = 0, type = "TIMConvTotalUnreadMessageCountChangedCallback", data = total_unread_count.ToString(), user_data = user_data_string });
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
      if (!IsStringCallbackStore.Contains(user_data_string.Split('_')[0]))
      {
        mainSyncContext.Post(threadOperation<object>, cc);
      }
      else
      {
        mainSyncContext.Post(threadOperation<string>, cc);
      }
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMOnDeleteFriendCallback))]
    private static void TIMOnDeleteFriendCallbackInstance(IntPtr json_identifier_array, IntPtr user_data)
    {
      // string user_data_string = Utils.intptr2string(user_data);
      string user_data_string = SetOnDeleteFriendCallbackUser_Data;

      string json_identifier_array_string = Utils.intptr2string(json_identifier_array);

      CallbackConvert cc = new CallbackConvert { code = 0, type = "TIMOnDeleteFriendCallback", data = json_identifier_array_string, user_data = user_data_string };
      if (!IsStringCallbackStore.Contains(user_data_string.Split('_')[0]))
      {
        mainSyncContext.Post(threadOperation<object>, cc);
      }
      else
      {
        mainSyncContext.Post(threadOperation<string>, cc);
      }
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMUpdateFriendProfileCallback))]
    private static void TIMUpdateFriendProfileCallbackInstance(IntPtr json_friend_profile_update_array, IntPtr user_data)
    {
      string user_data_string = Utils.intptr2string(user_data);

      string json_friend_profile_update_array_string = Utils.intptr2string(json_friend_profile_update_array);

      CallbackConvert cc = new CallbackConvert { code = 0, type = "TIMUpdateFriendProfileCallback", data = json_friend_profile_update_array_string, user_data = user_data_string };
      if (!IsStringCallbackStore.Contains(user_data_string.Split('_')[0]))
      {
        mainSyncContext.Post(threadOperation<object>, cc);
      }
      else
      {
        mainSyncContext.Post(threadOperation<string>, cc);
      }
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMFriendAddRequestCallback))]
    private static void TIMFriendAddRequestCallbackInstance(IntPtr json_friend_add_request_pendency_array, IntPtr user_data)
    {
      string user_data_string = Utils.intptr2string(user_data);

      string json_friend_add_request_pendency_array_string = Utils.intptr2string(json_friend_add_request_pendency_array);

      CallbackConvert cc = new CallbackConvert { code = 0, type = "TIMFriendAddRequestCallback", data = json_friend_add_request_pendency_array_string, user_data = user_data_string };
      if (!IsStringCallbackStore.Contains(user_data_string.Split('_')[0]))
      {
        mainSyncContext.Post(threadOperation<object>, cc);
      }
      else
      {
        mainSyncContext.Post(threadOperation<string>, cc);
      }
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMFriendApplicationListDeletedCallback))]
    private static void TIMFriendApplicationListDeletedCallbackInstance(IntPtr json_identifier_array, IntPtr user_data)
    {
      string user_data_string = Utils.intptr2string(user_data);

      string json_identifier_array_string = Utils.intptr2string(json_identifier_array);

      CallbackConvert cc = new CallbackConvert { code = 0, type = "TIMFriendApplicationListDeletedCallback", data = json_identifier_array_string, user_data = user_data_string };
      if (!IsStringCallbackStore.Contains(user_data_string.Split('_')[0]))
      {
        mainSyncContext.Post(threadOperation<object>, cc);
      }
      else
      {
        mainSyncContext.Post(threadOperation<string>, cc);
      }
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
      if (!IsStringCallbackStore.Contains(user_data_string.Split('_')[0]))
      {
        mainSyncContext.Post(threadOperation<object>, cc);
      }
      else
      {
        mainSyncContext.Post(threadOperation<string>, cc);
      }
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMFriendBlackListDeletedCallback))]
    private static void TIMFriendBlackListDeletedCallbackInstance(IntPtr json_identifier_array, IntPtr user_data)
    {
      string user_data_string = Utils.intptr2string(user_data);

      string json_identifier_array_string = Utils.intptr2string(json_identifier_array);

      CallbackConvert cc = new CallbackConvert { code = 0, type = "TIMFriendBlackListDeletedCallback", data = json_identifier_array_string, user_data = user_data_string };
      if (!IsStringCallbackStore.Contains(user_data_string.Split('_')[0]))
      {
        mainSyncContext.Post(threadOperation<object>, cc);
      }
      else
      {
        mainSyncContext.Post(threadOperation<string>, cc);
      }
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
      if (!IsStringCallbackStore.Contains(user_data_string.Split('_')[0]))
      {
        mainSyncContext.Post(threadOperation<object>, cc);
      }
      else
      {
        mainSyncContext.Post(threadOperation<string>, cc);
      }
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMMsgGroupMessageReadMemberListCallback))]
    public static void TIMMsgGroupMessageReadMemberListCallbackInstance(IntPtr json_group_member_array, ulong next_seq, bool is_finished, IntPtr user_data)
    {
      string user_data_string = Utils.intptr2string(user_data);

      string json_group_member_array_string = Utils.intptr2string(json_group_member_array);
      CallbackConvert cc = new CallbackConvert { code = 0, type = "TIMMsgGroupMessageReadMemberListCallback", data = json_group_member_array_string, user_data = user_data_string, next_seq = next_seq, is_finished = is_finished };
      if (!IsStringCallbackStore.Contains(user_data_string.Split('_')[0]))
      {
        mainSyncContext.Post(threadOperation<object>, cc);
      }
      else
      {
        mainSyncContext.Post(threadOperation<string>, cc);
      }
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
      if (!IsStringCallbackStore.Contains(user_data_string.Split('_')[0]))
      {
        mainSyncContext.Post(threadOperation<object>, cc);
      }
      else
      {
        mainSyncContext.Post(threadOperation<string>, cc);
      }
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMGroupTopicChangedCallback))]
    public static void TIMGroupTopicChangedCallbackInstance(IntPtr group_id, IntPtr topic_info, IntPtr user_data)
    {
      string user_data_string = Utils.intptr2string(user_data);
      string json_group_id_string = Utils.intptr2string(group_id);
      string json_topic_info_string = Utils.intptr2string(topic_info);
      CallbackConvert cc = new CallbackConvert { code = 0, type = "TIMGroupTopicChangedCallback", data = json_topic_info_string, user_data = user_data_string, group_id = json_group_id_string };
      if (!IsStringCallbackStore.Contains(user_data_string.Split('_')[0]))
      {
        mainSyncContext.Post(threadOperation<object>, cc);
      }
      else
      {
        mainSyncContext.Post(threadOperation<string>, cc);
      }
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMSelfInfoUpdatedCallback))]
    public static void TIMSelfInfoUpdatedCallbackInstance(IntPtr json_user_profile, IntPtr user_data)
    {
      string user_data_string = Utils.intptr2string(user_data);
      string json_user_profile_string = Utils.intptr2string(json_user_profile);
      CallbackConvert cc = new CallbackConvert { code = 0, type = "TIMSelfInfoUpdatedCallback", data = json_user_profile_string, user_data = user_data_string };
      if (!IsStringCallbackStore.Contains(user_data_string.Split('_')[0]))
      {
        mainSyncContext.Post(threadOperation<object>, cc);
      }
      else
      {
        mainSyncContext.Post(threadOperation<string>, cc);
      }
    }

    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMUserStatusChangedCallback))]
    public static void TIMUserStatusChangedCallbackInstance(IntPtr json_user_status_array, IntPtr user_data)
    {
      string user_data_string = Utils.intptr2string(user_data);
      string json_user_status_array_string = Utils.intptr2string(json_user_status_array);
      CallbackConvert cc = new CallbackConvert { code = 0, type = "TIMUserStatusChangedCallback", data = json_user_status_array_string, user_data = user_data_string };
      if (!IsStringCallbackStore.Contains(user_data_string.Split('_')[0]))
      {
        mainSyncContext.Post(threadOperation<object>, cc);
      }
      else
      {
        mainSyncContext.Post(threadOperation<string>, cc);
      }
    }
    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMMsgExtensionsChangedCallback))]
    public static void TIMMsgExtensionsChangedCallbackInstance(IntPtr message_id, IntPtr message_extension_array, IntPtr user_data)
    {
      string user_data_string = Utils.intptr2string(user_data);
      string msg_id = Utils.intptr2string(message_id);
      string message_extensions = Utils.intptr2string(message_extension_array);
      CallbackConvert cc = new CallbackConvert { code = 0, type = "TIMMsgExtensionsChangedCallback", data = message_extensions, user_data = user_data_string, group_id = msg_id };
      if (!IsStringCallbackStore.Contains(user_data_string.Split('_')[0]))
      {
        mainSyncContext.Post(threadOperation<object>, cc);
      }
      else
      {
        mainSyncContext.Post(threadOperation<string>, cc);
      }
    }
    [MonoPInvokeCallback(typeof(IMNativeSDK.TIMMsgExtensionsDeletedCallback))]
    public static void TIMMsgExtensionsDeletedCallbackInstance(IntPtr message_id, IntPtr message_extension_key_array, IntPtr user_data)
    {
      string user_data_string = Utils.intptr2string(user_data);
      string msg_id = Utils.intptr2string(message_id);
      string message_extensions = Utils.intptr2string(message_extension_key_array);
      CallbackConvert cc = new CallbackConvert { code = 0, type = "TIMMsgExtensionsDeletedCallback", data = message_extensions, user_data = user_data_string, group_id = msg_id };
      if (!IsStringCallbackStore.Contains(user_data_string.Split('_')[0]))
      {
        mainSyncContext.Post(threadOperation<object>, cc);
      }
      else
      {
        mainSyncContext.Post(threadOperation<string>, cc);
      }
    }
  }
}

