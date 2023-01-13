using com.tencent.imsdk.unity.types;
using com.tencent.imsdk.unity.enums;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System;

namespace com.tencent.imsdk.unity.callback
{

  /// <summary>
  /// 异步方法回调 (Common async value callback)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void ValueCallback<T>(int code, string desc, T data, string user_data);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void NullValueCallback(int code, string desc, string user_data);

  /// <summary>
  /// 新消息回调 (Callback for receiving new message)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void RecvNewMsgCallback(List<Message> message, string user_data);

  /// <summary>
  /// 消息已读回调 (Callback for message read receipt)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void MsgReadedReceiptCallback(List<MessageReceipt> message_receipt, string user_data);

  /// <summary>
  /// 消息被撤回回调 (Callback for message revoking)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void MsgRevokeCallback(List<MsgLocator> msg_locator, string user_data);

  /// <summary>
  /// 消息内元素相关文件上传进度回调 (Callback for message element uploading progress)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void MsgElemUploadProgressCallback(Message message, int index, int cur_size, int total_size, string user_data);

  /// <summary>
  /// 群事件回调 (Callback for group tips event)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void GroupTipsEventCallback(GroupTipsElem message, string user_data);

  /// <summary>
  /// 群属性变更回调 (Callback for group attributes changing)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void GroupAttributeChangedCallback(string group_id, List<GroupAttributes> group_attributes, string user_data);

  /// <summary>
  /// 群计数器变更的回调 (Callback for group counter changing)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void GroupCounterChangedCallback(string group_id, string group_counter_key, ulong group_counter_new_value, string user_data);

  /// <summary>
  /// 会话信息回调 (Callback for conversation event)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void ConvEventCallback(TIMConvEvent conv_event, List<ConvInfo> conv_list, string user_data);

  /// <summary>
  /// 会话分组被创建 (Callback for conversation group created)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void ConvConversationGroupCreatedCallback(string group_name, List<string> conversation_array, string user_data);

  /// <summary>
  /// 会话分组被删除 (Callback for conversation group deleted)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void ConvConversationGroupDeletedCallback(string group_name, string user_data);

  /// <summary>
  /// 会话分组名变更 (Callback for conversation group name changed)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void ConvConversationGroupNameChangedCallback(string old_name, string new_name, string user_data);

  /// <summary>
  /// 会话分组新增会话 (Callback for conversation added to a group)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void ConvConversationsAddedToGroupCallback(string group_name, List<string> conversation_array, string user_data);

  /// <summary>
  /// 会话分组删除会话 (Callback for conversation deleted from the group)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void ConvConversationsDeletedFromGroupCallback(string group_name, List<string> conversation_array, string user_data);

  /// <summary>
  /// 全部未读数改变 (Callback for conversation total unread message count changing)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void ConvTotalUnreadMessageCountChangedCallback(int total_unread_count, string user_data);

  /// <summary>
  /// 按会话 filter 过滤的未读消息总数变化 (Callback for conversation by filter unread message count changing)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void ConvTotalUnreadMessageCountChangedByFilterCallback(ConversationListFilter filter, int total_unread_count, string user_data);

  /// <summary>
  /// 网络状态回调 (Callback for network status changing)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void NetworkStatusListenerCallback(TIMNetworkStatus status, int code, string desc, string user_data);

  /// <summary>
  /// 被踢下线回调 (Callback for user kicked offline)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void KickedOfflineCallback(string user_data);

  /// <summary>
  /// 登录票据过期回调 (Callback for user signature expired)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void UserSigExpiredCallback(string user_data);

  /// <summary>
  /// 添加好友回调 (Callback for adding friend)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void OnAddFriendCallback(List<string> userids, string user_data);

  /// <summary>
  /// 删除好友回调 (Callback for deleting friend)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void OnDeleteFriendCallback(List<string> userids, string user_data);

  /// <summary>
  /// 好友资料改变回调 (Callback for updating friend's profile)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void UpdateFriendProfileCallback(List<FriendProfileUpdate> friend_profile_update_array, string user_data);

  /// <summary>
  /// 申请加好友回调 (Callback for friend's adding request)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void FriendAddRequestCallback(List<FriendAddPendency> friend_add_request_pendency_array, string user_data);

  /// <summary>
  /// 好友申请被删除回调 (Callback for friend request list deleted)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void FriendApplicationListDeletedCallback(List<string> userids, string user_data);

  /// <summary>
  /// 好友申请已读回调 (Callback for friend request read)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void FriendApplicationListReadCallback(string user_data);

  /// <summary>
  /// 黑名单添加回调 (Callback for appending blacklist)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void FriendBlackListAddedCallback(List<FriendProfile> friend_black_added_array, string user_data);

  /// <summary>
  /// 黑名单删除回调 (Callback for deleing member from blacklist)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void FriendBlackListDeletedCallback(List<string> userids, string user_data);

  /// <summary>
  /// 日志回调 (Callback for logs)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void LogCallback(TIMLogLevel log_level, string log, string user_data);

  /// <summary>
  /// 消息被修改回调 (Callback for message updated)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void MsgUpdateCallback(List<Message> message_list, string user_data);

  /// <summary>
  /// 获取群消息已读群成员列表回调 (Callback for group message read member list)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void MsgGroupMessageReadMemberListCallback(List<GroupMemberInfo> json_group_member_array, ulong next_seq, bool is_finished, string user_data);

  /// <summary>
  /// 话题创建回调 (Callback for group topic created)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void GroupTopicCreatedCallback(string group_id, string topic_id, string user_data);

  /// <summary>
  /// 话题被删除回调 (Callback for group topic deleted)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void GroupTopicDeletedCallback(string group_id, List<string> topic_id_array, string user_data);

  /// <summary>
  /// 话题更新回调 (Callback for group topic changed)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void GroupTopicChangedCallback(string group_id, GroupTopicInfo topic_info, string user_data);

  /// <summary>
  /// 当前用户的资料发生更新回调 (Callback for self info updated)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void SelfInfoUpdatedCallback(UserProfile json_user_profile, string user_data);

  /// <summary>
  /// 用户状态变更通知回调 (Callback for user status changed)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void UserStatusChangedCallback(List<UserStatus> json_user_status_array, string user_data);

  /// <summary>
  /// 设置消息扩展信息更新的回调 (Callback for message extensions updated)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void MsgExtensionsChangedCallback(string message_id, List<MessageExtension> message_extension_array, string user_data);

  /// <summary>
  /// 设置消息扩展信息删除的回调 (Callback for message extensions deleted)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void MsgExtensionsDeletedCallback(string message_id, List<MessageExtension> message_extension_key_array, string user_data);

  /// ----------------------------------------------------------------

  /// String 类型回调 (String type data callback)
  /// <summary>
  /// 新消息回调 (Callback for receiving new message)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void RecvNewMsgStringCallback(string message, string user_data);

  /// <summary>
  /// 消息已读回调 (Callback for message read receipt)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void MsgReadedReceiptStringCallback(string message_receipt, string user_data);

  /// <summary>
  /// 消息被撤回回调 (Callback for message revoking)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void MsgRevokeStringCallback(string msg_locator, string user_data);

  /// <summary>
  /// 消息内元素相关文件上传进度回调 (Callback for message element uploading progress)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void MsgElemUploadProgressStringCallback(string message, int index, int cur_size, int total_size, string user_data);

  /// <summary>
  /// 群事件回调 (Callback for group tips event)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void GroupTipsEventStringCallback(string message, string user_data);

  /// <summary>
  /// 群属性变更回调 (Callback for group attributes changing)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void GroupAttributeChangedStringCallback(string group_id, string group_attributes, string user_data);

  /// <summary>
  /// 会话信息回调 (Callback for conversation event)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void ConvEventStringCallback(TIMConvEvent conv_event, string conv_list, string user_data);

  /// <summary>
  /// 会话分组被创建 (Callback for conversation group created)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void ConvConversationGroupCreatedStringCallback(string group_name, string conversation_array, string user_data);

  /// <summary>
  /// 会话分组新增会话 (Callback for conversation added to a group)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void ConvConversationsAddedToGroupStringCallback(string group_name, string conversation_array, string user_data);

  /// <summary>
  /// 会话分组删除会话 (Callback for conversation deleted from the group)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void ConvConversationsDeletedFromGroupStringCallback(string group_name, string conversation_array, string user_data);

  /// <summary>
  /// 按会话 filter 过滤的未读消息总数变化 (Callback for conversation by filter unread message count changing)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void ConvTotalUnreadMessageCountChangedByFilterStringCallback(string filter, int total_unread_count, string user_data);

  /// <summary>
  /// 添加好友回调 (Callback for adding friend)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void OnAddFriendStringCallback(string userids, string user_data);

  /// <summary>
  /// 删除好友回调 (Callback for deleting friend)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void OnDeleteFriendStringCallback(string userids, string user_data);

  /// <summary>
  /// 好友资料改变回调 (Callback for updating friend's profile)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void UpdateFriendProfileStringCallback(string friend_profile_update_array, string user_data);

  /// <summary>
  /// 申请加好友回调 (Callback for friend's adding request)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void FriendAddRequestStringCallback(string friend_add_request_pendency_array, string user_data);

  /// <summary>
  /// 好友申请被删除回调 (Callback for friend request list deleted)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void FriendApplicationListDeletedStringCallback(string userids, string user_data);

  /// <summary>
  /// 黑名单添加回调 (Callback for appending blacklist)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void FriendBlackListAddedStringCallback(string friend_black_added_array, string user_data);

  /// <summary>
  /// 黑名单删除回调
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void FriendBlackListDeletedStringCallback(string userids, string user_data);

  /// <summary>
  /// 消息被修改回调 (Callback for deleing member from blacklist)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void MsgUpdateStringCallback(string message_list, string user_data);

  /// <summary>
  /// 获取群消息已读群成员列表回调 (Callback for group message read member list)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void MsgGroupMessageReadMemberListStringCallback(string json_group_member_array, ulong next_seq, bool is_finished, string user_data);

  /// <summary>
  /// 话题被删除回调 (Callback for group topic deleted)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void GroupTopicDeletedStringCallback(string group_id, string topic_id_array, string user_data);

  /// <summary>
  /// 话题更新回调 (Callback for group topic changed)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void GroupTopicChangedStringCallback(string group_id, string topic_info, string user_data);

  /// <summary>
  /// 当前用户的资料发生更新回调 (Callback for self info updated)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void SelfInfoUpdatedStringCallback(string json_user_profile, string user_data);

  /// <summary>
  /// 用户状态变更通知回调 (Callback for user status changed)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void UserStatusChangedStringCallback(string json_user_status_array, string user_data);

  /// <summary>
  /// 设置消息扩展信息更新的回调 (Callback for message extensions updated)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void MsgExtensionsChangedStringCallback(string message_id, string message_extension_array, string user_data);

  /// <summary>
  /// 设置消息扩展信息删除的回调 (Callback for message extensions deleted)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void MsgExtensionsDeletedStringCallback(string message_id, string message_extension_key_array, string user_data);

}