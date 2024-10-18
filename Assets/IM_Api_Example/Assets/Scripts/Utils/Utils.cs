using System;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using com.tencent.imsdk.unity.enums;
using com.tencent.imsdk.unity.callback;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using com.tencent.im.unity.demo.types;
using com.tencent.imsdk.unity.web;
using EasyUI.Toast;
namespace com.tencent.im.unity.demo.utils
{
  public class Utils
  {
    public static string SynchronizeResult(TIMResult res)
    {
      return "Synchronize return: " + ((int)res).ToString();
    }
    public static ValueCallback<string> addAsyncStringDataToScreen(Callback cb)
    {
      var callback = cb;
      return (int code, string desc, string callbackData, string user_data) =>
    {
      string head = "\n" + user_data + "Asynchronous return:\n\n";
      var isObj = callbackData.StartsWith("{") || callbackData.StartsWith("[");
      string body;
      if (isObj)
      {
        body = @"{""code"":" + code.ToString() + @",""desc"":""" + desc + @""",""json_param"":" + (string.IsNullOrEmpty(callbackData) ? "null" : callbackData) + "}";
      }
      else
      {
        body = @"{""code"":" + code.ToString() + @",""desc"":""" + desc + @""",""json_param"":""" + (string.IsNullOrEmpty(callbackData) ? "null" : callbackData) + @"""}";
      }
      JObject json = JObject.Parse(body);
      string formatted = SyntaxHighlightJson(json.ToString());
      callback(head + formatted, callbackData);
    };
    }
    public static NullValueCallback addAsyncNullDataToScreen(Callback cb)
    {
      var callback = cb;
      return (int code, string desc, string user_data) =>
    {
      string head = "\n" + user_data + "Asynchronous return:\n\n";
      string body = @"{""code"":" + code.ToString() + @",""desc"":""" + desc + @""",""json_param"":" + "null" + "}";
      JObject json = JObject.Parse(body);
      string formatted = SyntaxHighlightJson(json.ToString());
      callback(head + formatted);
    };
    }
    public delegate void Callback(params string[] parameters);

    public static MsgGroupMessageReadMemberListStringCallback SetMsgGroupMessageReadMemberListCallback(Callback cb)
    {
      var callback = cb;
      return (string json_group_member_array, ulong next_seq, bool is_finished, string user_data) =>
        {
          string head = "\n" + user_data + "Asynchronous return:\n\n";
          string body = @"{""json_group_member_array"":" + json_group_member_array + "}";
          JObject json = JObject.Parse(body);
          string formatted = SyntaxHighlightJson(json.ToString());
          callback(head + formatted, json_group_member_array, next_seq.ToString(), is_finished.ToString());
        };
    }

    public static IMWebSDK.MsgGroupMessageReadMemberListCallback SetMsgGroupMessageReadMemberListCallbackWeb(Callback cb)
    {
      var callback = cb;
      return (string json_group_member_array, string next_seq, bool is_finished, string user_data) =>
        {
          string head = "\n" + user_data + "Asynchronous return:\n\n";
          string body = @"{""json_group_member_array"":" + json_group_member_array + "}";
          JObject json = JObject.Parse(body);
          string formatted = SyntaxHighlightJson(json.ToString());
          callback(head + formatted, json_group_member_array, next_seq, is_finished.ToString());
        };
    }

    // EventCallback  ...

    public static RecvNewMsgStringCallback RecvNewMsgCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string message, string user_data) =>
    {
      string head = "\n" + user_data + "Asynchronous return:\n\n";
      string body = @"{""message"":" + (string.IsNullOrEmpty(message) ? "null" : message) + "}";
      JObject json = JObject.Parse(body);
      string formatted = SyntaxHighlightJson(json.ToString());
      callback(eventInfo, head + formatted, message);
    };
    }

    public static MsgReactionsChangedStringCallback setMsgReactionsChangedCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo){
      var callback = cb;
      return (string message_reaction_change_info_array, string user_data) =>
    {
      string head = "\n" + user_data + "Asynchronous return:\n\n";
      string body = @"{""message_reaction_change_info_array"":" + (string.IsNullOrEmpty(message_reaction_change_info_array) ? "null" : message_reaction_change_info_array) + "}";
      JObject json = JObject.Parse(body);
      string formatted = SyntaxHighlightJson(json.ToString());
      callback(eventInfo, head + formatted, message_reaction_change_info_array);
    };
    }

    public static MsgAllMessageReceiveOptionStringCallback setMsgAllMessageReceiveOptionCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo){
      var callback = cb;
      return (string json_receive_message_option_info, string user_data) =>
    {
      string head = "\n" + user_data + "Asynchronous return:\n\n";
      string body = @"{""json_receive_message_option_info"":" + (string.IsNullOrEmpty(json_receive_message_option_info) ? "null" : json_receive_message_option_info) + "}";
      JObject json = JObject.Parse(body);
      string formatted = SyntaxHighlightJson(json.ToString());
      callback(eventInfo, head + formatted, json_receive_message_option_info);
    };
    }

    public static MsgReadedReceiptStringCallback SetMsgReadedReceiptCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string message_receipt, string user_data) =>
    {
      string head = "\n" + user_data + "Asynchronous return:\n\n";
      string body = @"{""message_receipt"":" + (string.IsNullOrEmpty(message_receipt) ? "null" : message_receipt) + "}";
      JObject json = JObject.Parse(body);
      string formatted = SyntaxHighlightJson(json.ToString());
      callback(eventInfo, head + formatted, message_receipt);
    };
    }
    public static MsgRevokeStringCallback SetMsgRevokeCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string msg_locator, string user_data) =>
    {
      string head = "\n" + user_data + "Asynchronous return:\n\n";
      string body = @"{""msg_locator"":" + (string.IsNullOrEmpty(msg_locator) ? "null" : msg_locator) + "}";
      JObject json = JObject.Parse(body);
      string formatted = SyntaxHighlightJson(json.ToString());
      callback(eventInfo, head + formatted, msg_locator);
    };
    }
    public static GroupTipsEventStringCallback SetGroupTipsEventCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string json_group_tip_array, string user_data) =>
    {
      string head = "\n" + user_data + "Asynchronous return:\n\n";
      string body = @"{""json_group_tip_array"":" + (string.IsNullOrEmpty(json_group_tip_array) ? "null" : json_group_tip_array) + "}";
      JObject json = JObject.Parse(body);
      string formatted = SyntaxHighlightJson(json.ToString());
      callback(eventInfo, head + formatted, json_group_tip_array);
    };
    }
    public static MsgElemUploadProgressStringCallback SetMsgElemUploadProgressCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string message, int index, int cur_size, int total_size, string user_data) =>
    {
      string head = "\n" + user_data + "Asynchronous return:\n\n";
      string body = @"{""message"":" + (string.IsNullOrEmpty(message) ? "null" : message) + @",""index"":""" + index + @",""cur_size"":""" + cur_size + @",""total_size"":""" + total_size + "}";
      JObject json = JObject.Parse(body);
      string formatted = SyntaxHighlightJson(json.ToString());
      callback(eventInfo, head + formatted, message);
    };
    }
    public static GroupAttributeChangedStringCallback SetGroupAttributeChangedCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string group_id, string group_attributes, string user_data) =>
    {
      string head = "\n" + user_data + "Asynchronous return:\n\n";
      string body = @"{""group_id"":" + group_id + @"{""group_attributes"":" + (string.IsNullOrEmpty(group_attributes) ? "null" : group_attributes) + "}";
      JObject json = JObject.Parse(body);
      string formatted = SyntaxHighlightJson(json.ToString());
      callback(eventInfo, head + formatted);
    };
    }
    public static ConvEventStringCallback SetConvEventCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (TIMConvEvent conv_event, string conv_list, string user_data) =>
    {
      string head = "\n" + user_data + "Asynchronous return:\n\n";
      string body = @"{""conv_event"":""" + conv_event + @""",""conv_list"":" + (string.IsNullOrEmpty(conv_list) ? "null" : conv_list) + "}";
      JObject json = JObject.Parse(body);
      string formatted = SyntaxHighlightJson(json.ToString());
      callback(eventInfo, head + formatted);
    };
    }
    public static ConvTotalUnreadMessageCountChangedCallback SetConvTotalUnreadMessageCountChangedCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (int total_unread_count, string user_data) =>
    {
      string head = "\n" + user_data + "Asynchronous return:\n\n";
      string body = @"{""total_unread_count"":" + total_unread_count + "}";
      JObject json = JObject.Parse(body);
      string formatted = SyntaxHighlightJson(json.ToString());
      callback(eventInfo, head + formatted);
    };
    }
    public static NetworkStatusListenerCallback SetNetworkStatusListenerCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (TIMNetworkStatus status, int code, string desc, string user_data) =>
    {
      string head = "\n" + user_data + "Asynchronous return:\n\n";
      string body = @"{""status"":""" + status + @""",""code"":" + code + @",""desc"":""" + desc + @"""}";
      JObject json = JObject.Parse(body);
      string formatted = SyntaxHighlightJson(json.ToString());
      callback(eventInfo, head + formatted);
    };
    }
    public static KickedOfflineCallback SetKickedOfflineCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string user_data) =>
    {
      string head = "\n" + user_data + "Asynchronous return:\n\n";
      callback(eventInfo, head);
    };
    }
    public static UserSigExpiredCallback SetUserSigExpiredCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string user_data) =>
    {
      string head = "\n" + user_data + "Asynchronous return:\n\n";
      callback(eventInfo, head);
    };
    }
    public static OnAddFriendStringCallback SetOnAddFriendCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string userids, string user_data) =>
    {
      string head = "\n" + user_data + "Asynchronous return:\n\n";
      string body = @"{""user_ids"":" + userids + "}";
      JObject json = JObject.Parse(body);
      string formatted = SyntaxHighlightJson(json.ToString());
      callback(eventInfo, head + formatted);
    };
    }
    public static OnDeleteFriendStringCallback SetOnDeleteFriendCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string userids, string user_data) =>
    {
      string head = "\n" + user_data + "Asynchronous return:\n\n";
      string body = @"{""user_ids"":" + userids + "}";
      JObject json = JObject.Parse(body);
      string formatted = SyntaxHighlightJson(json.ToString());
      callback(eventInfo, head + formatted);
    };
    }
    public static UpdateFriendProfileStringCallback SetUpdateFriendProfileCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string friend_profile_update_array, string user_data) =>
    {
      string head = "\n" + user_data + "Asynchronous return:\n\n";
      string body = @"{""friend_profile_update_array"":" + friend_profile_update_array + "}";
      JObject json = JObject.Parse(body);
      string formatted = SyntaxHighlightJson(json.ToString());
      callback(eventInfo, head + formatted);
    };
    }
    public static FriendAddRequestStringCallback SetFriendAddRequestCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string friend_add_request_pendency_array, string user_data) =>
    {
      string head = "\n" + user_data + "Asynchronous return:\n\n";
      string body = @"{""friend_add_request_pendency_array"":" + friend_add_request_pendency_array + "}";
      JObject json = JObject.Parse(body);
      string formatted = SyntaxHighlightJson(json.ToString());
      callback(eventInfo, head + formatted);
    };
    }
    public static FriendApplicationListDeletedStringCallback SetFriendApplicationListDeletedCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string userids, string user_data) =>
    {
      string head = "\n" + user_data + "Asynchronous return:\n\n";
      string body = @"{""user_ids"":" + userids + "}";
      JObject json = JObject.Parse(body);
      string formatted = SyntaxHighlightJson(json.ToString());
      callback(eventInfo, head + formatted);
    };
    }
    public static FriendApplicationListReadCallback SetFriendApplicationListReadCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string user_data) =>
    {
      string head = "\n" + user_data + "Asynchronous return:\n\n";
      callback(eventInfo, head);
    };
    }
    public static FriendBlackListAddedStringCallback SetFriendBlackListAddedCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string friend_black_added_array, string user_data) =>
    {
      string head = "\n" + user_data + "Asynchronous return:\n\n";
      string body = @"{""friend_black_added_array"":" + friend_black_added_array + "}";
      JObject json = JObject.Parse(body);
      string formatted = SyntaxHighlightJson(json.ToString());
      callback(eventInfo, head + formatted);
    };
    }
    public static FriendBlackListDeletedStringCallback SetFriendBlackListDeletedCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string userids, string user_data) =>
    {
      string head = "\n" + user_data + "Asynchronous return:\n\n";
      string body = @"{""user_ids"":" + userids + "}";
      JObject json = JObject.Parse(body);
      string formatted = SyntaxHighlightJson(json.ToString());
      callback(eventInfo, head + formatted);
    };
    }

    public static LogCallback SetLogCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (TIMLogLevel log_level, string log, string user_data) =>
    {
      string head = "\n" + user_data + "Asynchronous return:\n\n";
      string body = @"{""log_level"":""" + log_level + @""",""log"":""" + log + @"""}";
      JObject json = JObject.Parse(body);
      string formatted = SyntaxHighlightJson(json.ToString());
      callback(eventInfo, head + formatted);
    };
    }

    public static MsgUpdateStringCallback SetMsgUpdateCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string message_list, string user_data) =>
    {
      string head = "\n" + user_data + "Asynchronous return:\n\n";
      string body = @"{""message_list"":" + message_list + "}";
      JObject json = JObject.Parse(body);
      string formatted = SyntaxHighlightJson(json.ToString());
      callback(eventInfo, head + formatted);
    };
    }

    public static GroupTopicCreatedCallback SetGroupTopicCreatedCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string group_id, string topic_id, string user_data) =>
    {
      string head = "\n" + user_data + "Asynchronous return:\n\n";
      string body = @"{""group_id"":""" + group_id + @""",""topic_id"":""" + topic_id + @"""}";
      JObject json = JObject.Parse(body);
      string formatted = SyntaxHighlightJson(json.ToString());
      callback(eventInfo, head + formatted);
    };
    }

    public static GroupTopicDeletedStringCallback SetGroupTopicDeletedCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string group_id, string topic_id_array, string user_data) =>
    {
      string head = "\n" + user_data + "Asynchronous return:\n\n";
      string body = @"{""group_id"":""" + group_id + @""",""topic_id_array"":" + topic_id_array + "}";
      JObject json = JObject.Parse(body);
      string formatted = SyntaxHighlightJson(json.ToString());
      callback(eventInfo, head + formatted);
    };
    }

    public static GroupTopicChangedStringCallback SetGroupTopicChangedCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string group_id, string topic_info, string user_data) =>
    {
      string head = "\n" + user_data + "Asynchronous return:\n\n";
      string body = @"{""group_id"":""" + group_id + @""",""topic_info"":" + topic_info + "}";
      JObject json = JObject.Parse(body);
      string formatted = SyntaxHighlightJson(json.ToString());
      callback(eventInfo, head + formatted);
    };
    }

    public static SelfInfoUpdatedStringCallback SetSelfInfoUpdatedCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string json_user_profile, string user_data) =>
    {
      string head = "\n" + user_data + "Asynchronous return:\n\n";
      string body = @"{""json_user_profile"":" + json_user_profile + "}";
      JObject json = JObject.Parse(body);
      string formatted = SyntaxHighlightJson(json.ToString());
      callback(eventInfo, head + formatted);
    };
    }

    public static UserStatusChangedStringCallback SetUserStatusChangedCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string json_user_status_array, string user_data) =>
    {
      string head = "\n" + user_data + "Asynchronous return:\n\n";
      string body = @"{""json_user_status_array"":" + json_user_status_array + "}";
      JObject json = JObject.Parse(body);
      string formatted = SyntaxHighlightJson(json.ToString());
      callback(eventInfo, head + formatted);
    };
    }

    public static UserInfoChangedStringCallback SetUserInfoChangedCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo){
      var callback = cb;
      return (string json_user_info_array, string user_data) =>
    {
      string head = "\n" + user_data + "Asynchronous return:\n\n";
      string body = @"{""json_user_info_array"":" + json_user_info_array + "}";
      JObject json = JObject.Parse(body);
      string formatted = SyntaxHighlightJson(json.ToString());
      callback(eventInfo, head + formatted);
    };
    }

    public static MsgExtensionsChangedStringCallback SetMsgExtensionsChangedCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string message_id, string message_extension_array, string user_data) =>
    {
      string head = "\n" + user_data + "Asynchronous return:\n\n";
      string body = @"{""message_id"":""" + message_id + @""",""message_extension_array"":" + message_extension_array + "}";
      JObject json = JObject.Parse(body);
      string formatted = SyntaxHighlightJson(json.ToString());
      callback(eventInfo, head + formatted);
    };
    }

    public static MsgExtensionsDeletedStringCallback SetMsgExtensionsDeletedCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string message_id, string message_extension_key_array, string user_data) =>
    {
      string head = "\n" + user_data + "Asynchronous return:\n\n";
      string body = @"{""message_id"":""" + message_id + @""",""message_extension_key_array"":" + message_extension_key_array + "}";
      JObject json = JObject.Parse(body);
      string formatted = SyntaxHighlightJson(json.ToString());
      callback(eventInfo, head + formatted);
    };
    }

    public static MsgGroupPinnedMessageChangedStringCallback SetMsgGroupPinnedMessageChangedCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string group_id, string json_message, bool is_pinned, string op_user, string user_data) =>
      {
        string head = "\n" + user_data + "Asynchronous return:\n\n";
        string body = @"{""group_id"":""" + group_id + @""",""json_message"":" + json_message + @",""is_pinned"":""" + is_pinned + @""",""op_user"":" + op_user + "}";
        JObject json = JObject.Parse(body);
        string formatted = SyntaxHighlightJson(json.ToString());
        callback(eventInfo, head + formatted);
      };
    }

    public static FriendGroupCreatedStringCallback SetFriendGroupCreatedCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string group_name, string json_friend_info_array, string user_data) =>
      {
        string head = "\n" + user_data + "Asynchronous return:\n\n";
        string body = @"{""group_name"":""" + group_name + @""",""json_friend_info_array"":" + json_friend_info_array + "}";
        JObject json = JObject.Parse(body);
        string formatted = SyntaxHighlightJson(json.ToString());
        callback(eventInfo, head + formatted);
      };
    }

    public static FriendGroupDeletedStringCallback SetFriendGroupDeletedCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string json_group_name_array, string user_data) =>
      {
        string head = "\n" + user_data + "Asynchronous return:\n\n";
        string body = @"{""json_group_name_array"":" + json_group_name_array + "}";
        JObject json = JObject.Parse(body);
        string formatted = SyntaxHighlightJson(json.ToString());
        callback(eventInfo, head + formatted);
      };
    }

    public static FriendGroupNameChangedStringCallback SetFriendGroupNameChangedCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string old_group_name, string new_group_name, string user_data) =>
      {
        string head = "\n" + user_data + "Asynchronous return:\n\n";
        string body = @"{""old_group_name"":""" + old_group_name + @""",""new_group_name"":""" + new_group_name + @"""}";
        JObject json = JObject.Parse(body);
        string formatted = SyntaxHighlightJson(json.ToString());
        callback(eventInfo, head + formatted);
      };
    }

    public static FriendsAddedToGroupStringCallback SetFriendsAddedToGroupCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string group_name, string json_friend_info_array, string user_data) =>
      {
        string head = "\n" + user_data + "Asynchronous return:\n\n";
        string body = @"{""group_name"":""" + group_name + @""",""json_friend_info_array"":" + json_friend_info_array + "}";
        JObject json = JObject.Parse(body);
        string formatted = SyntaxHighlightJson(json.ToString());
        callback(eventInfo, head + formatted);
      };
    }

    public static FriendsDeletedFromGroupStringCallback SetFriendsDeletedFromGroupCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string group_name, string json_friend_id_array, string user_data) =>
      {
        string head = "\n" + user_data + "Asynchronous return:\n\n";
        string body = @"{""group_name"":""" + group_name + @""",""json_friend_id_array"":" + json_friend_id_array + "}";
        JObject json = JObject.Parse(body);
        string formatted = SyntaxHighlightJson(json.ToString());
        callback(eventInfo, head + formatted);
      };
    }

    public static OfficialAccountSubscribedStringCallback SetOfficialAccountSubscribedCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string json_official_account_info, string user_data) =>
      {
        string head = "\n" + user_data + "Asynchronous return:\n\n";
        string body = @"{""json_official_account_info"":" + json_official_account_info + "}";
        JObject json = JObject.Parse(body);
        string formatted = SyntaxHighlightJson(json.ToString());
        callback(eventInfo, head + formatted);
      };
    }

    public static OfficialAccountUnsubscribedStringCallback SetOfficialAccountUnsubscribedCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string official_account_id, string user_data) =>
      {
        string head = "\n" + user_data + "Asynchronous return:\n\n";
        string body = @"{""official_account_id"":""" + official_account_id + @"""}";
        JObject json = JObject.Parse(body);
        string formatted = SyntaxHighlightJson(json.ToString());
        callback(eventInfo, head + formatted);
      };
    }

    public static OfficialAccountDeletedStringCallback SetOfficialAccountDeletedCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string official_account_id, string user_data) =>
      {
        string head = "\n" + user_data + "Asynchronous return:\n\n";
        string body = @"{""official_account_id"":""" + official_account_id + @"""}";
        JObject json = JObject.Parse(body);
        string formatted = SyntaxHighlightJson(json.ToString());
        callback(eventInfo, head + formatted);
      };
    }

    public static OfficialAccountInfoChangedStringCallback SetOfficialAccountInfoChangedCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string json_official_account_info, string user_data) =>
      {
        string head = "\n" + user_data + "Asynchronous return:\n\n";
        string body = @"{""json_official_account_info"":" + json_official_account_info + "}";
        JObject json = JObject.Parse(body);
        string formatted = SyntaxHighlightJson(json.ToString());
        callback(eventInfo, head + formatted);
      };
    }

    public static MyFollowingListChangedStringCallback SetMyFollowingListChangedCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string json_user_info_list, bool is_add, string user_data) =>
      {
        string head = "\n" + user_data + "Asynchronous return:\n\n";
        string body = @"{""json_user_info_list"":" + json_user_info_list + @",""is_add"":""" + is_add + @"""}";
        JObject json = JObject.Parse(body);
        string formatted = SyntaxHighlightJson(json.ToString());
        callback(eventInfo, head + formatted);
      };
    }

    public static MyFollowersListChangedStringCallback SetMyFollowersListChangedCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string json_user_info_list, bool is_add, string user_data) =>
      {
        string head = "\n" + user_data + "Asynchronous return:\n\n";
        string body = @"{""json_user_info_list"":" + json_user_info_list + @",""is_add"":""" + is_add + @"""}";
        JObject json = JObject.Parse(body);
        string formatted = SyntaxHighlightJson(json.ToString());
        callback(eventInfo, head + formatted);
      };
    }

    public static MutualFollowersListChangedStringCallback SetMutualFollowersListChangedCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string json_user_info_list, bool is_add, string user_data) =>
      {
        string head = "\n" + user_data + "Asynchronous return:\n\n";
        string body = @"{""json_user_info_list"":" + json_user_info_list + @",""is_add"":""" + is_add + @"""}";
        JObject json = JObject.Parse(body);
        string formatted = SyntaxHighlightJson(json.ToString());
        callback(eventInfo, head + formatted);
      };
    }

    public static SignalingReceiveNewInvitationStringCallback SetSignalingReceiveNewInvitationCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string invite_id, string inviter, string group_id, string json_invitee_list, string data, string user_data) =>
      {
        string head = "\n" + user_data + "Asynchronous return:\n\n";
        string body = @"{""invite_id"":""" + invite_id + @""",""inviter"":""" + inviter + @""",""group_id"":""" + group_id + @""",""json_invitee_list"":" + json_invitee_list + @""",""data"":""" + data + @"""}";
        JObject json = JObject.Parse(body);
        string formatted = SyntaxHighlightJson(json.ToString());
        callback(eventInfo, head + formatted);
      };
    }
    public static SignalingInvitationCancelledStringCallback SetSignalingInvitationCancelledCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string invite_id, string inviter, string data, string user_data) =>
      {
        string head = "\n" + user_data + "Asynchronous return:\n\n";
        string body = @"{""invite_id"":""" + invite_id + @""",""inviter"":""" + inviter + @""",""data"":""" + data + @"""}";
        JObject json = JObject.Parse(body);
        string formatted = SyntaxHighlightJson(json.ToString());
        callback(eventInfo, head + formatted);
      };
    }

    public static SignalingInviteeAcceptedStringCallback SetSignalingInviteeAcceptedCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string invite_id, string invitee, string data, string user_data) =>
      {
        string head = "\n" + user_data + "Asynchronous return:\n\n";
        string body = @"{""invite_id"":""" + invite_id + @""",""invitee"":""" + invitee + @""",""data"":""" + data + @"""}";
        JObject json = JObject.Parse(body);
        string formatted = SyntaxHighlightJson(json.ToString());
        callback(eventInfo, head + formatted);
      };
    }

    public static SignalingInviteeRejectedStringCallback SetSignalingInviteeRejectedCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string invite_id, string invitee, string data, string user_data) =>
      {
        string head = "\n" + user_data + "Asynchronous return:\n\n";
        string body = @"{""invite_id"":""" + invite_id + @""",""invitee"":""" + invitee + @""",""data"":""" + data + @"""}";
        JObject json = JObject.Parse(body);
        string formatted = SyntaxHighlightJson(json.ToString());
        callback(eventInfo, head + formatted);
      };
    }

    public static SignalingInvitationTimeoutStringCallback SetSignalingInvitationTimeoutCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string invite_id, string json_invitee_list, string user_data) =>
      {
        string head = "\n" + user_data + "Asynchronous return:\n\n";
        string body = @"{""invite_id"":""" + invite_id + @""",""json_invitee_list"":" + json_invitee_list + "}";
        JObject json = JObject.Parse(body);
        string formatted = SyntaxHighlightJson(json.ToString());
        callback(eventInfo, head + formatted);
      };
    }

    public static SignalingInvitationModifiedStringCallback SetSignalingInvitationModifiedCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string invite_id, string data, string user_data) =>
      {
        string head = "\n" + user_data + "Asynchronous return:\n\n";
        string body = @"{""invite_id"":""" + invite_id + @""",""data"":""" + data + @"""}";
        JObject json = JObject.Parse(body);
        string formatted = SyntaxHighlightJson(json.ToString());
        callback(eventInfo, head + formatted);
      };
    }

    public static CommunityCreateTopicStringCallback SetCommunityCreateTopicCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string group_id, string topic_id, string user_data) =>
      {
        string head = "\n" + user_data + "Asynchronous return:\n\n";
        string body = @"{""group_id"":""" + group_id + @""",""topic_id"":""" + topic_id + @"""}";
        JObject json = JObject.Parse(body);
        string formatted = SyntaxHighlightJson(json.ToString());
        callback(eventInfo, head + formatted);
      };
    }

    public static CommunityDeleteTopicStringCallback SetCommunityDeleteTopicCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string group_id, string json_topic_id_array, string user_data) =>
      {
        string head = "\n" + user_data + "Asynchronous return:\n\n";
        string body = @"{""group_id"":""" + group_id + @""",""json_topic_id_array"":" + json_topic_id_array + "}";
        JObject json = JObject.Parse(body);
        string formatted = SyntaxHighlightJson(json.ToString());
        callback(eventInfo, head + formatted);
      };
    }

    public static CommunityChangeTopicInfoStringCallback SetCommunityChangeTopicInfoCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string group_id, string json_topic_info, string user_data) =>
      {
        string head = "\n" + user_data + "Asynchronous return:\n\n";
        string body = @"{""group_id"":""" + group_id + @""",""json_topic_info"":" + json_topic_info + "}";
        JObject json = JObject.Parse(body);
        string formatted = SyntaxHighlightJson(json.ToString());
        callback(eventInfo, head + formatted);
      };
    }

    public static CommunityReceiveTopicRESTCustomDataStringCallback SetCommunityReceiveTopicRESTCustomDataCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string topic_id, string custom_data, string user_data) =>
      {
        string head = "\n" + user_data + "Asynchronous return:\n\n";
        string body = @"{""topic_id"":""" + topic_id + @""",""custom_data"":""" + custom_data + @"""}";
        JObject json = JObject.Parse(body);
        string formatted = SyntaxHighlightJson(json.ToString());
        callback(eventInfo, head + formatted);
      };
    }

    public static CommunityCreatePermissionGroupStringCallback SetCommunityCreatePermissionGroupCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string group_id, string json_permission_group_id_array, string user_data) =>
      {
        string head = "\n" + user_data + "Asynchronous return:\n\n";
        string body = @"{""group_id"":""" + group_id + @""",""json_permission_group_id_array"":" + json_permission_group_id_array + "}";
        JObject json = JObject.Parse(body);
        string formatted = SyntaxHighlightJson(json.ToString());
        callback(eventInfo, head + formatted);
      };
    }

    public static CommunityDeletePermissionGroupStringCallback SetCommunityDeletePermissionGroupCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string group_id, string json_permission_group_info, string user_data) =>
      {
        string head = "\n" + user_data + "Asynchronous return:\n\n";
        string body = @"{""group_id"":""" + group_id + @""",""json_permission_group_info"":" + json_permission_group_info + "}";
        JObject json = JObject.Parse(body);
        string formatted = SyntaxHighlightJson(json.ToString());
        callback(eventInfo, head + formatted);
      };
    }

    public static CommunityChangePermissionGroupInfoStringCallback SetCommunityChangePermissionGroupInfoCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string group_id, string json_permission_group_info, string user_data) =>
      {
        string head = "\n" + user_data + "Asynchronous return:\n\n";
        string body = @"{""group_id"":""" + group_id + @""",""json_permission_group_info"":" + json_permission_group_info + "}";
        JObject json = JObject.Parse(body);
        string formatted = SyntaxHighlightJson(json.ToString());
        callback(eventInfo, head + formatted);
      };
    }

    public static CommunityAddMembersToPermissionGroupStringCallback SetCommunityAddMembersToPermissionGroupCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string group_id, string json_result, string user_data) =>
      {
        string head = "\n" + user_data + "Asynchronous return:\n\n";
        string body = @"{""group_id"":""" + group_id + @""",""json_result"":" + json_result + "}";
        JObject json = JObject.Parse(body);
        string formatted = SyntaxHighlightJson(json.ToString());
        callback(eventInfo, head + formatted);
      };
    }

    public static CommunityRemoveMembersFromPermissionGroupStringCallback SetCommunityRemoveMembersFromPermissionGroupCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string group_id, string json_result, string user_data) =>
      {
        string head = "\n" + user_data + "Asynchronous return:\n\n";
        string body = @"{""group_id"":""" + group_id + @""",""json_result"":" + json_result + "}";
        JObject json = JObject.Parse(body);
        string formatted = SyntaxHighlightJson(json.ToString());
        callback(eventInfo, head + formatted);
      };
    }

    public static CommunityAddTopicPermissionStringCallback SetCommunityAddTopicPermissionCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string group_id, string json_result, string user_data) =>
      {
        string head = "\n" + user_data + "Asynchronous return:\n\n";
        string body = @"{""group_id"":""" + group_id + @""",""json_result"":" + json_result + "}";
        JObject json = JObject.Parse(body);
        string formatted = SyntaxHighlightJson(json.ToString());
        callback(eventInfo, head + formatted);
      };
    }

    public static CommunityDeleteTopicPermissionStringCallback SetCommunityDeleteTopicPermissionCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string group_id, string json_result, string user_data) =>
      {
        string head = "\n" + user_data + "Asynchronous return:\n\n";
        string body = @"{""group_id"":""" + group_id + @""",""json_result"":" + json_result + "}";
        JObject json = JObject.Parse(body);
        string formatted = SyntaxHighlightJson(json.ToString());
        callback(eventInfo, head + formatted);
      };
    }

    public static CommunityModifyTopicPermissionStringCallback SetCommunityModifyTopicPermissionCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string group_id, string json_result, string user_data) =>
      {
        string head = "\n" + user_data + "Asynchronous return:\n\n";
        string body = @"{""group_id"":""" + group_id + @""",""json_result"":" + json_result + "}";
        JObject json = JObject.Parse(body);
        string formatted = SyntaxHighlightJson(json.ToString());
        callback(eventInfo, head + formatted);
      };
    }

    public static ExperimentalNotifyStringCallback SetExperimentalNotifyCallback(EventCallback cb, EventListenerInfo.EventInfo eventInfo)
    {
      var callback = cb;
      return (string key, string data, string user_data) =>
      {
        string head = "\n" + user_data + "Asynchronous return:\n\n";
        string body = @"{""key"":""" + key + @""",""data"":" + data + "}";
        JObject json = JObject.Parse(body);
        string formatted = SyntaxHighlightJson(json.ToString());
        callback(eventInfo, head + formatted);
      };
    }


    public delegate void EventCallback(EventListenerInfo.EventInfo eventInfo, params string[] parameters);

    public static string PrefixEventCallbackData(string eventName, string data)
    {
      return "<color=\"#757575\">" + $"//{eventName}" + "</color>\n\n" + data;
    }
    public static string SyntaxHighlightJson(string original)
    {
      // From http://joelabrahamsson.com/syntax-highlighting-json-with-c/
      // ¤ = Placeholder for " which gets replaced at the end, avoids needing to escape "
      return Regex.Replace(
        original,
        @"(¤(\\u[a-zA-Z0-9]{4}|\\[^u]|[^\\¤])*¤(\s*:)?|\b(true|false|null)\b|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?)".Replace('¤', '"'),
        match =>
        {
          var cls = "55b6c9"; // number
          if (Regex.IsMatch(match.Value, @"^¤".Replace('¤', '"')))
          {
            if (Regex.IsMatch(match.Value, ":$"))
            {
              cls = "e9de8e"; // key
            }
            else
            {
              cls = "e9de8e"; // string
            }
          }
          else if (Regex.IsMatch(match.Value, "true|false"))
          {
            cls = "ce466f"; // boolean
          }
          else if (Regex.IsMatch(match.Value, "null"))
          {
            cls = "ce466f"; // null
          }
          return "<color=\"#" + cls + "\">" + match + "</color>";
        });
    }
    public static string ToJson(object pData)
    {
      try
      {
        var setting = new JsonSerializerSettings();
        setting.NullValueHandling = NullValueHandling.Ignore;

        return Newtonsoft.Json.JsonConvert.SerializeObject(pData, setting);
      }
      catch (System.Exception error)
      {
        Debug.LogError(error);
      }
      return null;
    }
    public static T FromJson<T>(string pJson)
    {
      if (typeof(T) == typeof(string))
      {
        return (T)(object)pJson;
      }
      if (string.IsNullOrEmpty(pJson)) return default(T);
      try
      {
        JsonSerializerSettings settings = new JsonSerializerSettings();
        settings.MissingMemberHandling = MissingMemberHandling.Ignore;
        T ret = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(pJson, settings);
        return ret;
      }
      catch (System.Exception error)
      {
        Debug.LogError(error);
      }
      return default(T);
    }

#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
    public static extern void CopyText(string str);
#endif

    public static void Copy(string text)
    {
      var copyContent = text;
#if UNITY_WEBGL
        copyContent = Regex.Replace(text, @"\<(.*?color.*?)\>", string.Empty);
#endif
#if UNITY_WEBGL && !UNITY_EDITOR
        CopyText(copyContent);
#else
      TextEditor editor = new TextEditor();
      editor.text = copyContent;
      editor.SelectAll();
      editor.Copy();
#endif
      Toast.Show(Utils.t("Copied"));
    }
    public static bool IsCn()
    {
      string lang = PlayerPrefs.GetString("Language", "cn:0");
      return lang.StartsWith("cn");
    }

    public static string t(string key)
    {
      string lang = PlayerPrefs.GetString("Language", "cn:0").Split(':')[0];
      if (I18n.dict.TryGetValue(key, out I18nData i18nData))
      {
        switch (lang)
        {
          case "cn": return i18nData.cn;
          case "en": return i18nData.en ?? key;
          default: return i18nData.en ?? key;
        }
      }
      return key;
    }

    public delegate void PickFileCallback(string path);

    public static void PickImage(PickFileCallback cb)
    {
      NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
      {
        Debug.Log("Image path: " + path);
        if (path != null)
        {
          cb(path);
          // Create Texture from selected image
          // Texture2D texture = NativeGallery.LoadImageAtPath(path, maxSize);
        }
      });

      Debug.Log("Permission result: " + permission);
    }
    public static void PickVideo(PickFileCallback cb)
    {
      NativeGallery.Permission permission = NativeGallery.GetVideoFromGallery((path) =>
      {
        Debug.Log("Video path: " + path);
        if (path != null)
        {
          cb(path);
        }
      });

      Debug.Log("Permission result: " + permission);
    }
    public static void PickFile(PickFileCallback cb)
    {
      string[] allowedFileTypes = new string[] { "*" };
      NativeFilePicker.Permission permission = NativeFilePicker.PickFile((path) =>
      {
        Debug.Log("File path: " + path);
        if (path != null)
        {
          cb(path);
        }
      }, allowedFileTypes);

      Debug.Log("Permission result: " + permission);
    }

    public static bool IsValidConvID(string id)
    {
      return id.StartsWith("c2c_") || id.StartsWith("group_") || id.StartsWith("system_");
    }

    public static string SetConvIDPrefix(string id, TIMConvType conv_type)
    {
      if (conv_type == TIMConvType.kTIMConv_C2C)
      {
        return "c2c_" + id;
      }
      if (conv_type == TIMConvType.kTIMConv_Group)
      {
        return "group_" + id;
      }
      if (conv_type == TIMConvType.kTIMConv_System)
      {
        return "system_" + id;
      }
      return id;
    }
  }
}