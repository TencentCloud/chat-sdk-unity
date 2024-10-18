using com.tencent.imsdk.unity.utils;
using com.tencent.imsdk.unity.types;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using com.tencent.imsdk.unity.enums;
using com.tencent.imsdk.unity.callback;
using UnityEngine;
using AOT;
using System.Text;

namespace com.tencent.imsdk.unity.native
{
  public class IMNativeSDK
  {

    #region DllImport
#if UNITY_EDITOR
#if UNITY_EDITOR_OSX
                    public const string MyLibName = "libImSDKForMac_C";
#else
                    public const string MyLibName = "ImSDK";
#endif
#else
#if UNITY_IPHONE
                    public const string MyLibName = "__Internal";
#elif UNITY_ANDROID
                    public const string MyLibName = "ImSDK";
                   
#elif UNITY_STANDALONE_WIN
                    public const string MyLibName = "ImSDK";
#elif UNITY_STANDALONE_OSX
                    public const string MyLibName = "libImSDKForMac_C";
                  
#elif UNITY_WEBGL
                    public const string MyLibName = "__Internal";
#else
    public const string MyLibName = "ImSDK";
#endif
#endif

    #endregion

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMInit(long sdk_app_id, IntPtr json_sdk_config);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMUninit();

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMLogin(IntPtr user_id, IntPtr user_sig, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMLogout(CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMGetLoginStatus();

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMGetLoginUserID(StringBuilder user_id_buffer);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMSetConfig(IntPtr json_config, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr TIMGetSDKVersion();

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern long TIMGetServerTime();



    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMConvGetConvList(CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMConvDelete(IntPtr conv_id, int conv_type, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMConvDeleteConversationList(IntPtr conversation_id_array,bool clearMessage,CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMConvGetConvInfo(IntPtr json_get_conv_list_param, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMConvSetDraft(string conv_id, int conv_type, IntPtr draft_param);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMConvCancelDraft(string conv_id, int conv_type);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMConvPinConversation(string conv_id, int conv_type, bool is_pinned, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMConvGetTotalUnreadMessageCount(CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMConvGetUnreadMessageCountByFilter(IntPtr filter, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMConvSubscribeUnreadMessageCountByFilter(IntPtr filter);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMConvUnsubscribeUnreadMessageCountByFilter(IntPtr filter);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMConvCleanConversationUnreadMessageCount(IntPtr conversation_id,ulong clean_timestamp,ulong clean_sequence, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMConvAddConversationsToGroup(IntPtr group_name, IntPtr conversation_id_array, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMConvCreateConversationGroup(IntPtr group_name, IntPtr conversation_id_array, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMConvDeleteConversationGroup(IntPtr group_name, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMConvDeleteConversationsFromGroup(IntPtr group_name, IntPtr conversation_id_array, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMConvGetConversationGroupList(CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMConvGetConversationListByFilter(IntPtr filter, ulong next_seq, uint count, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMConvMarkConversation(IntPtr conversation_id_array, long mark_type, bool enable_mark, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMConvRenameConversationGroup(IntPtr old_name, IntPtr new_name, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMConvSetConversationCustomData(IntPtr conversation_id_array, IntPtr custom_data, CommonValueCallback cb, IntPtr user_data);



    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMMsgSendMessage(string conv_id, int conv_type, IntPtr message_param, StringBuilder message_id, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMMsgCancelSend(string conv_id, int conv_type, IntPtr message_id, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMMsgFindMessages(IntPtr message_id_array, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMMsgReportReaded(string conv_id, int conv_type, IntPtr message_param, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMMsgSetOfflinePushToken(IntPtr json_token, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMMsgDoBackground(int unread_count, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMMsgDoForeground(CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMMsgMarkAllMessageAsRead(CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMMsgRevoke(string conv_id, int conv_type, IntPtr message_param, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMMsgModifyMessage(IntPtr message_param, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMMsgFindByMsgLocatorList(string conv_id, int conv_type, IntPtr message_locator_param, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMMsgImportMsgList(string conv_id, int conv_type, IntPtr message_list_param, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMMsgSaveMsg(string conv_id, int conv_type, IntPtr message_param, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMMsgGetMsgList(string conv_id, int conv_type, IntPtr message_param, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMMsgDelete(string conv_id, int conv_type, IntPtr json_msgdel_param, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMMsgListDelete(string conv_id, int conv_type, IntPtr message_list, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMMsgClearHistoryMessage(string conv_id, int conv_type, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMMsgSetC2CReceiveMessageOpt(IntPtr json_identifier_array, int opt, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMMsgGetC2CReceiveMessageOpt(IntPtr json_identifier_array, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMMsgSetGroupReceiveMessageOpt(IntPtr group_id, int opt, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMMsgSetAllReceiveMessageOpt(int opt,int start_hour,int start_minute,int start_second,int duration,CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMMsgSetAllReceiveMessageOpt2(int opt,int start_time_stamp,int duration,CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMMsgGetAllReceiveMessageOpt(CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMMsgDownloadElemToPath(IntPtr json_download_elem_param, IntPtr path, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMMsgDownloadMergerMessage(IntPtr json_single_msg, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMMsgBatchSend(IntPtr json_batch_send_param, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMMsgSearchLocalMessages(IntPtr json_search_message_param, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMMsgSearchCloudMessages(IntPtr json_search_message_param, CommonValueCallback cb, IntPtr user_data);
  
    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMMsgSetLocalCustomData(IntPtr json_msg_param, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMMsgSendMessageReadReceipts(IntPtr json_msg_array, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMMsgSetMessageExtensions(IntPtr json_msg, IntPtr json_extension_array, CommonValueCallback cb, IntPtr user_data);
  
    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMMsgGetMessageExtensions(IntPtr json_msg, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMMsgDeleteMessageExtensions(IntPtr json_msg, IntPtr json_extension_key_array, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMMsgAddMessageReaction(IntPtr json_msg, IntPtr reaction_id, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMMsgRemoveMessageReaction(IntPtr json_msg, IntPtr reaction_id, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMMsgGetMessageReactions(IntPtr json_msg_array, int max_user_count_per_reaction,CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMMsgGetAllUserListOfMessageReaction(IntPtr json_msg, IntPtr reaction_id, ulong next_seq, int count,CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMMsgConvertVoiceToText(IntPtr url, IntPtr language, CommonValueCallback cb, IntPtr user_data);
    
    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMMsgTranslateText(IntPtr json_source_text_array, IntPtr source_language, IntPtr target_language, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMMsgGetMessageReadReceipts(IntPtr json_msg_array, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMMsgGetGroupMessageReadMemberList(IntPtr json_msg, TIMGroupMessageReadMembersFilter filter, ulong next_seq, int count, TIMMsgGroupMessageReadMemberListCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMPinGroupMessage(IntPtr group_id, IntPtr json_msg, bool is_pinned, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMGetPinnedGroupMessageList(IntPtr group_id, CommonValueCallback cb, IntPtr user_data);



    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMGroupCreate(IntPtr json_group_create_param, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMGroupDelete(IntPtr group_id, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMGroupJoin(IntPtr group_id, IntPtr hello_message, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMGroupQuit(IntPtr group_id, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMGroupInviteMember(IntPtr json_group_invite_param, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMGroupDeleteMember(IntPtr json_group_delete_param, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMGroupGetJoinedGroupList(CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMGroupGetGroupInfoList(IntPtr group_id_list, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]

    public static extern int TIMGroupModifyGroupInfo(IntPtr json_group_modifyinfo_param, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMGroupGetMemberInfoList(IntPtr json_group_getmeminfos_param, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMGroupMarkGroupMemberList(IntPtr group_id, IntPtr member_array, int mark_type, bool enable_mark, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMGroupModifyMemberInfo(IntPtr json_group_modifymeminfo_param, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMGroupGetPendencyList(IntPtr json_group_getpendence_list_param, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMGroupReportPendencyReaded(long time_stamp, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMGroupHandlePendency(IntPtr json_group_handle_pendency_param, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMGroupGetOnlineMemberCount(IntPtr group_id, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMGroupSetGroupCounters(IntPtr group_id, IntPtr json_group_counter_array, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMGroupGetGroupCounters(IntPtr group_id, IntPtr json_group_counter_array, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMGroupIncreaseGroupCounter(IntPtr group_id, IntPtr group_counter_key, ulong group_counter_value, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMGroupDecreaseGroupCounter(IntPtr group_id, IntPtr group_counter_key, ulong group_counter_value, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMGroupSearchGroups(IntPtr json_group_search_groups_param, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMGroupSearchGroupMembers(IntPtr json_group_search_group_members_param, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMGroupInitGroupAttributes(IntPtr group_id, IntPtr json_group_atrributes, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMGroupSetGroupAttributes(IntPtr group_id, IntPtr json_group_atrributes, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMGroupDeleteGroupAttributes(IntPtr group_id, IntPtr json_keys, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMGroupGetGroupAttributes(IntPtr group_id, IntPtr json_keys, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMGroupGetJoinedCommunityList(CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMGroupCreateTopicInCommunity(IntPtr group_id, IntPtr json_topic_info, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMGroupDeleteTopicFromCommunity(IntPtr group_id, IntPtr json_topic_id_array, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMGroupSetTopicInfo(IntPtr json_topic_info, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMGroupGetTopicInfoList(IntPtr group_id, IntPtr json_topic_id_array, CommonValueCallback cb, IntPtr user_data);



    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMGetUserStatus(IntPtr json_identifier_array, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMSetSelfStatus(IntPtr json_current_user_status, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMSubscribeUserStatus(IntPtr json_identifier_array, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMUnsubscribeUserStatus(IntPtr json_identifier_array, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMProfileGetUserProfileList(IntPtr json_get_user_profile_list_param, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMProfileModifySelfUserProfile(IntPtr json_modify_self_user_profile_param, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMSubscribeUserInfo(IntPtr json_user_id_list, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMUnsubscribeUserInfo(IntPtr json_user_id_list, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMFriendshipGetFriendProfileList(CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMFriendshipAddFriend(IntPtr json_add_friend_param, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMFriendshipHandleFriendAddRequest(IntPtr json_handle_friend_add_param, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMFriendshipModifyFriendProfile(IntPtr json_modify_friend_info_param, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMFriendshipDeleteFriend(IntPtr json_delete_friend_param, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMFriendshipCheckFriendType(IntPtr json_check_friend_list_param, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMFriendshipCreateFriendGroup(IntPtr json_create_friend_group_param, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMFriendshipGetFriendGroupList(IntPtr json_get_friend_group_list_param, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMFriendshipModifyFriendGroup(IntPtr json_modify_friend_group_param, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMFriendshipDeleteFriendGroup(IntPtr json_delete_friend_group_param, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMFriendshipAddToBlackList(IntPtr json_add_to_blacklist_param, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMFriendshipGetBlackList(CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMFriendshipDeleteFromBlackList(IntPtr json_delete_from_blacklist_param, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMFriendshipGetPendencyList(IntPtr json_get_pendency_list_param, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMFriendshipDeletePendency(IntPtr json_delete_pendency_param, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMFriendshipReportPendencyReaded(ulong time_stamp, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMFriendshipSearchFriends(IntPtr json_search_friends_param, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMFriendshipGetFriendsInfo(IntPtr json_get_friends_info_param, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int callExperimentalAPI(IntPtr json_param, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMFollowUser(IntPtr json_user_id_list, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMUnfollowUser(IntPtr json_user_id_list, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMGetMyFollowingList(IntPtr next_cursor, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMGetMyFollowersList(IntPtr next_cursor, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMGetMutualFollowersList(IntPtr next_cursor, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMGetUserFollowInfo(IntPtr json_user_id_list, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMCheckFollowType(IntPtr json_user_id_list, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMSubscribeOfficialAccount(IntPtr official_account_id, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMUnsubscribeOfficialAccount(IntPtr official_account_id, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMGetOfficialAccountsInfo(IntPtr json_official_account_id_list, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMSignalingInvite(IntPtr invitee, IntPtr data, bool online_user_only, IntPtr json_offline_push_info, int timeout, StringBuilder invite_id_buffer, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMSignalingInviteInGroup(IntPtr group_id, IntPtr json_invitee_array, IntPtr data, bool online_user_only, int timeout, StringBuilder invite_id_buffer, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMSignalingCancel(IntPtr invite_id, IntPtr data, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMSignalingAccept(IntPtr invite_id, IntPtr data, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMSignalingReject(IntPtr invite_id, IntPtr data, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMGetSignalingInfo(IntPtr json_msg, CommonValueCallback json_signaling_info_cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMSignalingModifyInvitation(IntPtr invite_id, IntPtr data, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMCommunityCreate(IntPtr json_community_create_param, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMCommunityGetJoinedCommunityList(CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMCommunityCreateTopicInCommunity(IntPtr group_id, IntPtr json_topic_info, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMCommunityDeleteTopicFromCommunity(IntPtr group_id, IntPtr json_topic_id_array, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMCommunitySetTopicInfo(IntPtr json_topic_info, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMCommunityGetTopicInfoList(IntPtr group_id, IntPtr json_topic_id_array, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMCommunitySetTopicInheritMessageReceiveOptionFromCommunity(IntPtr topic_id, bool isInherit, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMCommunityCreatePermissionGroupInCommunity(IntPtr json_permission_group_info, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMCommunityDeletePermissionGroupFromCommunity(IntPtr group_id, IntPtr json_permission_group_id_array, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMCommunityModifyPermissionGroupInfoInCommunity(IntPtr json_permission_group_info, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMCommunityGetJoinedPermissionGroupListInCommunity(IntPtr group_id, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMCommunityGetPermissionGroupListInCommunity(IntPtr group_id, IntPtr json_permission_group_id_array, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMCommunityAddCommunityMembersToPermissionGroup(IntPtr group_id, IntPtr permission_group_id, IntPtr json_member_id_array, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMCommunityRemoveCommunityMembersFromPermissionGroup(IntPtr group_id, IntPtr permission_group_id, IntPtr json_member_id_array, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMCommunityGetCommunityMemberListInPermissionGroup(IntPtr group_id, IntPtr permission_group_id, IntPtr next_cursor, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMCommunityAddTopicPermissionToPermissionGroup(IntPtr group_id, IntPtr permission_group_id, IntPtr json_topic_permission_map, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMCommunityDeleteTopicPermissionFromPermissionGroup(IntPtr group_id, IntPtr permission_group_id, IntPtr json_topic_id_array, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMCommunityModifyTopicPermissionInPermissionGroup(IntPtr group_id, IntPtr permission_group_id, IntPtr json_topic_permission_map, CommonValueCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMCommunityGetTopicPermissionInPermissionGroup(IntPtr group_id, IntPtr permission_group_id, IntPtr json_topic_id_array, CommonValueCallback cb, IntPtr user_data);



    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMAddRecvNewMsgCallback(TIMRecvNewMsgCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMRemoveRecvNewMsgCallback(TIMRecvNewMsgCallback cb);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetMsgReactionsChangedCallback(TIMMsgReactionsChangedCallback cb,IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetMsgAllMessageReceiveOptionCallback(TIMMsgAllMessageReceiveOptionCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetMsgExtensionsChangedCallback(TIMMsgExtensionsChangedCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetMsgExtensionsDeletedCallback(TIMMsgExtensionsDeletedCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetMsgReadedReceiptCallback(TIMMsgReadedReceiptCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetMsgRevokeCallback(TIMMsgRevokeCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetMsgElemUploadProgressCallback(TIMMsgElemUploadProgressCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetGroupTipsEventCallback(TIMGroupTipsEventCallback cb, IntPtr user_data);
    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetGroupAttributeChangedCallback(TIMGroupAttributeChangedCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetGroupCounterChangedCallback(TIMGroupCounterChangedCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetConvEventCallback(TIMConvEventCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetConvConversationGroupCreatedCallback(TIMConvConversationGroupCreatedCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetConvConversationGroupDeletedCallback(TIMConvConversationGroupDeletedCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetConvConversationGroupNameChangedCallback(TIMConvConversationGroupNameChangedCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetConvConversationsAddedToGroupCallback(TIMConvConversationsAddedToGroupCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetConvConversationsDeletedFromGroupCallback(TIMConvConversationsDeletedFromGroupCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetConvTotalUnreadMessageCountChangedCallback(TIMConvTotalUnreadMessageCountChangedCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetConvUnreadMessageCountChangedByFilterCallback(TIMConvTotalUnreadMessageCountChangedByFilterCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetNetworkStatusListenerCallback(TIMNetworkStatusListenerCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetKickedOfflineCallback(TIMKickedOfflineCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetUserSigExpiredCallback(TIMUserSigExpiredCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetOnAddFriendCallback(TIMOnAddFriendCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetOnDeleteFriendCallback(TIMOnDeleteFriendCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetUpdateFriendProfileCallback(TIMUpdateFriendProfileCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetFriendAddRequestCallback(TIMFriendAddRequestCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetFriendApplicationListDeletedCallback(TIMFriendApplicationListDeletedCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetFriendApplicationListReadCallback(TIMFriendApplicationListReadCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetFriendBlackListAddedCallback(TIMFriendBlackListAddedCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetFriendBlackListDeletedCallback(TIMFriendBlackListDeletedCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetLogCallback(TIMLogCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetMsgUpdateCallback(TIMMsgUpdateCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetMsgGroupPinnedMessageChangedCallback(TIMMsgGroupPinnedMessageChangedCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMSetGroupTopicCreatedCallback(TIMGroupTopicCreatedCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMSetGroupTopicDeletedCallback(TIMGroupTopicDeletedCallback cb, IntPtr user_data);
    
    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMSetGroupTopicChangedCallback(TIMGroupTopicChangedCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMSetSelfInfoUpdatedCallback(TIMSelfInfoUpdatedCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMSetUserStatusChangedCallback(TIMUserStatusChangedCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMSetUserInfoChangedCallback(TIMUserInfoChangedCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetFriendGroupCreatedCallback(TIMFriendGroupCreatedCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetFriendGroupDeletedCallback(TIMFriendGroupDeletedCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetFriendGroupNameChangedCallback(TIMFriendGroupNameChangedCallback cb, IntPtr user_data);
  
    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetFriendsAddedToGroupCallback(TIMFriendsAddedToGroupCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetFriendsDeletedFromGroupCallback(TIMFriendsDeletedFromGroupCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetMyFollowingListChangedCallback(TIMMyFollowingListChangedCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetMyFollowersListChangedCallback(TIMMyFollowersListChangedCallback cb, IntPtr user_data);
  
    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetMutualFollowersListChangedCallback(TIMMutualFollowersListChangedCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetOfficialAccountSubscribedCallback(TIMOfficialAccountSubscribedCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetOfficialAccountUnsubscribedCallback(TIMOfficialAccountUnsubscribedCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetOfficialAccountDeletedCallback(TIMOfficialAccountDeletedCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetOfficialAccountInfoChangedCallback(TIMOfficialAccountInfoChangedCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetSignalingReceiveNewInvitationCallback(TIMSignalingReceiveNewInvitationCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetSignalingInviteeAcceptedCallback(TIMSignalingInviteeAcceptedCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetSignalingInviteeRejectedCallback(TIMSignalingInviteeRejectedCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetSignalingInvitationCancelledCallback(TIMSignalingInvitationCancelledCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetSignalingInvitationTimeoutCallback(TIMSignalingInvitationTimeoutCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetSignalingInvitationModifiedCallback(TIMSignalingInvitationModifiedCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetCommunityCreateTopicCallback(TIMCommunityCreateTopicCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetCommunityDeleteTopicCallback(TIMCommunityDeleteTopicCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetCommunityChangeTopicInfoCallback(TIMCommunityChangeTopicInfoCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetCommunityReceiveTopicRESTCustomDataCallback(TIMCommunityReceiveTopicRESTCustomDataCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetCommunityCreatePermissionGroupCallback(TIMCommunityCreatePermissionGroupCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetCommunityDeletePermissionGroupCallback(TIMCommunityDeletePermissionGroupCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetCommunityChangePermissionGroupInfoCallback(TIMCommunityChangePermissionGroupInfoCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetCommunityAddMembersToPermissionGroupCallback(TIMCommunityAddMembersToPermissionGroupCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetCommunityRemoveMembersFromPermissionGroupCallback(TIMCommunityRemoveMembersFromPermissionGroupCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetCommunityAddTopicPermissionCallback(TIMCommunityAddTopicPermissionCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetCommunityDeleteTopicPermissionCallback(TIMCommunityDeleteTopicPermissionCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetCommunityModifyTopicPermissionCallback(TIMCommunityModifyTopicPermissionCallback cb, IntPtr user_data);

    [DllImport(MyLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TIMSetExperimentalNotifyCallback(TIMExperimentalNotifyCallback cb, IntPtr user_data);



    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void CommonValueCallback(int code, IntPtr desc, IntPtr json_param, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMNetworkStatusListenerCallback(int status, int code, IntPtr desc, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMKickedOfflineCallback(IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMUserSigExpiredCallback(IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMRecvNewMsgCallback(IntPtr json_msg_array, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]

    public delegate void TIMMsgReactionsChangedCallback(IntPtr message_reaction_change_info_array,IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]

    public delegate void TIMMsgAllMessageReceiveOptionCallback(IntPtr json_receive_message_option_info, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMMsgReadedReceiptCallback(IntPtr json_msg_readed_receipt_array, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMMsgRevokeCallback(IntPtr json_msg_locator_array, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMMsgElemUploadProgressCallback(IntPtr json_msg, int index, int cur_size, int total_size, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMMsgUpdateCallback(IntPtr json_msg_array, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMMsgGroupMessageReadMemberListCallback(IntPtr json_group_member_array, ulong next_seq, bool is_finished, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMMsgExtensionsChangedCallback(IntPtr message_id, IntPtr message_extension_array, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMMsgExtensionsDeletedCallback(IntPtr message_id, IntPtr message_extension_key_array, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMMsgGroupPinnedMessageChangedCallback(IntPtr group_id, IntPtr json_msg, bool is_pinned, IntPtr op_user, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMGroupTipsEventCallback(IntPtr json_group_tip_array, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMGroupAttributeChangedCallback(IntPtr group_id, IntPtr json_group_attribute_array, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMGroupCounterChangedCallback(IntPtr group_id, IntPtr group_counter_key, ulong group_counter_new_value, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMConvEventCallback(int conv_event, IntPtr json_conv_array, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMConvConversationGroupCreatedCallback(IntPtr group_name, IntPtr conversation_array, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMConvConversationGroupDeletedCallback(IntPtr group_name, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMConvConversationGroupNameChangedCallback(IntPtr old_name, IntPtr new_name, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMConvConversationsAddedToGroupCallback(IntPtr group_name, IntPtr conversation_array, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMConvConversationsDeletedFromGroupCallback(IntPtr group_name, IntPtr conversation_array, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMConvTotalUnreadMessageCountChangedCallback(int total_unread_count, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMConvTotalUnreadMessageCountChangedByFilterCallback(IntPtr filter, int total_unread_count, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMOnAddFriendCallback(IntPtr json_identifier_array, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMOnDeleteFriendCallback(IntPtr json_identifier_array, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMUpdateFriendProfileCallback(IntPtr json_friend_profile_update_array, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMFriendAddRequestCallback(IntPtr json_friend_add_request_pendency_array, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMFriendApplicationListDeletedCallback(IntPtr json_identifier_array, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMFriendApplicationListReadCallback(IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMFriendBlackListAddedCallback(IntPtr json_friend_black_added_array, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMFriendBlackListDeletedCallback(IntPtr json_identifier_array, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMGroupTopicCreatedCallback(IntPtr group_id, IntPtr topic_id, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMGroupTopicDeletedCallback(IntPtr group_id, IntPtr topic_id_array, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMGroupTopicChangedCallback(IntPtr group_id, IntPtr topic_info, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMSelfInfoUpdatedCallback(IntPtr json_user_profile, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMUserStatusChangedCallback(IntPtr json_user_status_array, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMUserInfoChangedCallback(IntPtr json_user_info_array, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMFriendGroupCreatedCallback(IntPtr group_name, IntPtr json_friend_info_array, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMFriendGroupDeletedCallback(IntPtr json_group_name_array, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMFriendGroupNameChangedCallback(IntPtr old_group_name, IntPtr new_group_name, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMFriendsAddedToGroupCallback(IntPtr group_name, IntPtr json_friend_info_array, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMFriendsDeletedFromGroupCallback(IntPtr group_name, IntPtr json_friend_id_array, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMOfficialAccountSubscribedCallback(IntPtr json_official_account_info, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMOfficialAccountUnsubscribedCallback(IntPtr official_account_id, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMOfficialAccountDeletedCallback(IntPtr official_account_id, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMOfficialAccountInfoChangedCallback(IntPtr json_official_account_info, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMMyFollowingListChangedCallback(IntPtr json_user_info_list, bool is_add, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMMyFollowersListChangedCallback(IntPtr json_user_info_list, bool is_add, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMMutualFollowersListChangedCallback(IntPtr json_user_info_list, bool is_add, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMSignalingReceiveNewInvitationCallback(IntPtr invite_id, IntPtr inviter, IntPtr group_id, IntPtr json_invitee_list, IntPtr data, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMSignalingInvitationCancelledCallback(IntPtr invite_id, IntPtr inviter, IntPtr data, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMSignalingInviteeAcceptedCallback(IntPtr invite_id, IntPtr invitee, IntPtr data, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMSignalingInviteeRejectedCallback(IntPtr invite_id, IntPtr invitee, IntPtr data, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMSignalingInvitationTimeoutCallback(IntPtr invite_id, IntPtr json_invitee_list, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMSignalingInvitationModifiedCallback(IntPtr invite_id, IntPtr data, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMCommunityCreateTopicCallback(IntPtr group_id, IntPtr topic_id, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMCommunityDeleteTopicCallback(IntPtr group_id, IntPtr json_topic_id_array, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMCommunityChangeTopicInfoCallback(IntPtr group_id, IntPtr json_topic_info, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMCommunityReceiveTopicRESTCustomDataCallback(IntPtr topic_id, IntPtr custom_data, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMCommunityCreatePermissionGroupCallback(IntPtr group_id, IntPtr json_permission_group_info, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMCommunityDeletePermissionGroupCallback(IntPtr group_id, IntPtr json_permission_group_id_array, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMCommunityChangePermissionGroupInfoCallback(IntPtr group_id, IntPtr json_permission_group_info, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMCommunityAddMembersToPermissionGroupCallback(IntPtr group_id, IntPtr json_result, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMCommunityRemoveMembersFromPermissionGroupCallback(IntPtr group_id, IntPtr json_result, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMCommunityAddTopicPermissionCallback(IntPtr group_id, IntPtr json_result, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMCommunityDeleteTopicPermissionCallback(IntPtr group_id, IntPtr json_result, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMCommunityModifyTopicPermissionCallback(IntPtr group_id, IntPtr json_result, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMLogCallback(int level, IntPtr log, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void CommonCallback(int code, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMExperimentalNotifyCallback(IntPtr key, IntPtr data, IntPtr user_data);

  }
}