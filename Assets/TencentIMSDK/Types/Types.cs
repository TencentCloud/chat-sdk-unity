using com.tencent.imsdk.unity.enums;
using com.tencent.imsdk.unity.utils;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace com.tencent.imsdk.unity.types
{
  [JsonObject(MemberSerialization.OptOut)]
  public class SdkConfig : ExtraData
  {
    /// <value>只写，SDK配置本地路径 (Write only, SDK config local path) 推荐设置为 $"{System.IO.Directory.GetCurrentDirectory()}/.TIM-Config"</value>
    public string sdk_config_config_file_path;
    /// <value>只写，SDK日志本地路径 (Write only, SDK log file path) 推荐设置为 $"{System.IO.Directory.GetCurrentDirectory()}/.TIM-Log"</value>
    public string sdk_config_log_file_path;
    /// <value> uint64, 只写(选填), 配置Android平台的Java虚拟机指针 (Write only (optional), config of Android Java VM pointer)</value>
    public ulong? sdk_config_java_vm;
  }


  [JsonObject(MemberSerialization.OptOut)]
  public class ConvParam : ExtraData
  {
    /// <value>只写, 会话类型 (Write only, conversation type)</value>
    public TIMConvType get_conversation_list_param_conv_type;
    /// <value>只写, 会话ID (Write only, conversation ID)</value>
    public string get_conversation_list_param_conv_id;
  }


  [JsonObject(MemberSerialization.OptOut)]
  public class UserProfileCustomStringInfo : ExtraData
  {
    /// <value>只写, 用户自定义资料字段的key值（包含前缀Tag_Profile_Custom_） (Write only, user profile custom string key)</value>
    public string user_profile_custom_string_info_key;
    /// <value>只写, 该字段对应的字符串值 (Write only, user profile custom string value)</value>
    public string user_profile_custom_string_info_value;
  }


  [JsonObject(MemberSerialization.OptOut)]
  public class UserProfile : ExtraData
  {
    /// <value>只读, 用户ID (Read only, user ID)</value>
    public string user_profile_identifier;
    /// <value>只读, 用户的昵称 (Read only, user nickname)</value>
    public string user_profile_nick_name;
    /// <value>只读, 性别 (Read only, user gender)</value>
    public TIMGenderType user_profile_gender;
    /// <value>只读, 用户头像URL (Read only, user avatar url)</value>
    public string user_profile_face_url;
    /// <value>只读, 用户个人签名 (Read only, user signature)</value>
    public string user_profile_self_signature;
    /// <value>只读, 用户加好友的选项 (Read only, user add permission type)</value>
    public TIMProfileAddPermission user_profile_add_permission;
    /// <value>只读, 用户位置信息 (Read only, user location info)</value>
    public string user_profile_location;
    /// <value>只读, 语言 (Read only, user preferred language)</value>
    public uint user_profile_language;
    /// <value>只读, 生日 (Read only, user's birthday)</value>
    public uint user_profile_birthday;
    /// <value>只读, 等级 (Read only, user's level)</value>
    public uint user_profile_level;
    /// <value>只读, 角色 (Read only, user's role)</value>
    public uint user_profile_role;
    /// <value>只读, 请参考[自定义资料字段](https://cloud.tencent.com/document/product/269/1500#.E8.87.AA.E5.AE.9A.E4.B9.89.E8.B5.84.E6.96.99.E5.AD.97.E6.AE.B5) (Read only, user profile's custom string key-value pair. Check [Custom Profile Fields](https://www.tencentcloud.com/document/product/1047/33520))</value>
    public List<UserProfileCustomStringInfo> user_profile_custom_string_array;
    /// <value>只读, 用户好友备注(请注意，本字段仅在消息的发送者资料中提供)</value>
    public string user_profile_friend_remark;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class Message
  {
    /// <value>读写(必填), 消息内元素列表 (Read & Write (Required), element list in the message)</value>
    public List<Elem> message_elem_array;
    /// <value>读写(选填), 消息所属会话ID (Read & Write (Optional), conversation ID of the message)</value>
    public string message_conv_id;
    /// <value>读写(选填), 消息所属会话类型 (Read & Write (Optional), conversation type of the message)</value>
    public TIMConvType? message_conv_type;
    /// <value>读写(选填), 消息的发送者 (Read & Write (Optional), message sender)</value>
    public string message_sender;
    /// <value>读写(选填), 消息优先级 (Read & Write (Optional), message priority)</value>
    public TIMMsgPriority? message_priority;
    /// <value>读写(选填), 客户端时间 (Read & Write (Optional), message client time)</value>
    public ulong? message_client_time;
    /// <value>读写(选填), 服务端时间 (Read & Write (Optional), message server time)</value>
    public ulong? message_server_time;
    /// <value>读写(选填), 消息是否来自自己 (Read & Write (Optional), message is from self)</value>
    public bool? message_is_from_self;
    /// <value>读写(选填), 发送消息的平台 (Read & Write (Optional), message platform)</value>
    public TIMPlatform? message_platform;
    /// <value>读写(选填), 消息是否已读 (Read & Write (Optional), message is read or not)</value>
    public bool? message_is_read;
    /// <value>读写(选填), 消息是否是在线消息，false表示普通消息,true表示阅后即焚消息，默认为false (Read & Write (Optional), is online message or not. False means normal message, ture means dissolve after read)</value>
    public bool? message_is_online_msg;
    /// <value>只读, 消息是否被会话对方已读 (Read only, message is read by peer or not)</value>
    public bool? message_is_peer_read;
    /// <value>bool, 只读, 对方是否已读（会话维度，已读的条件：msg_time <= 对端标记会话已读的时间），该字段为 true 的条件是消息 timestamp <= 对端标记会话已读的时间 (Read only, message receipt is read by peer or not. It's true only when msg_time <= the time peer marked read receipt)</value>
    public bool? message_receipt_peer_read;
    /// <value>读写(选填), 消息是否需要已读回执（6.1 以上版本有效，需要您购买旗舰版套餐），群消息在使用该功能之前，需要先到 IM 控制台设置已读回执支持的群类型 (Read & Write (Optional), message needs read receipt or not, (SDK ver. ^6.1, only for Flagship Package). Before activate it, please go to IM console and set the group for read receipt feature.)</value>
    public bool? message_need_read_receipt;
    /// <value>读写(选填), 是否支持消息扩展 (Read & Write (Optional), whether the message needs extension)</value>
    public bool? message_support_message_extension;
    /// <value>只读, 是否已经发送了已读回执（只有Group 消息有效）(Read only, has sent receipt or not)</value>
    public bool? message_has_sent_receipt;
    /// <value>读写(选填), 消息当前状态 (Read & Write (Optional), message status)</value>
    public TIMMsgStatus? message_status;
    /// <value>只读, 消息的唯一标识，推荐使用 kTIMMsgMsgId (Read only, message unique ID, please use kTIMMsgMsgId instead)</value>
    public ulong message_unique_id;
    /// <value>只读, 消息的唯一标识 (Read only, message ID)</value>
    public string message_msg_id;
    /// <value> 只读, 消息的随机码 (Read only, message random)</value>
    public ulong message_rand;
    /// <value>只读, 消息序列 (Read only, message sequence)</value>
    public ulong message_seq;
    /// <value>读写(选填), 自定义整数值字段（本地保存，不会发送到对端，程序卸载重装后失效） (Read & Write (Optional), message custom integer, (stored locally, won't be sent to peers, dismissed after unload the App))</value>
    public int? message_custom_int;
    /// <value>读写(选填), 自定义数据字段（本地保存，不会发送到对端，程序卸载重装后失效） (Read & Write (Optional), message custom string, (stored locally, won't be sent to peers, dismissed after unload the App))</value>
    public string message_custom_str;
    /// <value>读写(选填), 消息自定义数据（云端保存，会发送到对端，程序卸载重装后还能拉取到） (Read & Write (Optional), message custom string, (stored online, will be sent to peers, dismissed after unload the App))</value>
    public string message_cloud_custom_str;
    /// <value>读写(选填),消息是否不计入未读计数：默认为 NO，表明需要计入未读计数，设置为 YES，表明不需要计入未读计数 (Read & Write (Optional), message is excluded from unread count or not: Default False.)</value>
    public bool? message_is_excluded_from_unread_count;
    /// <value>读写(选填),是否是转发消息 (Read & Write (Optional), message is forward message or not)</value>
    public bool? message_is_forward_message;
    /// <value>读写(选填), 群消息中被 @ 的用户 UserID 列表（即该消息都 @ 了哪些人），如果需要 @ALL ，请传入 kImSDK_MesssageAtALL 字段 (Read & Write (Optional), group @ userID list, kImSDK_MesssageAtALL means @ALL)</value>
    public List<string> message_group_at_user_array;
    /// <value>读写(选填), 消息的发送者的用户资料 (Read & Write (Optional), message sender profile)</value>
    public UserProfile message_sender_profile;
    /// <value>读写(选填), 消息发送者在群里面的信息，只有在群会话有效。目前仅能获取字段 kTIMGroupMemberInfoIdentifier、kTIMGroupMemberInfoNameCard 其他的字段建议通过 TIMGroupGetMemberInfoList 接口获取 (Read & Write (Optional), group message sender info, only for group message, only kTIMGroupMemberInfoIdentifier, kTIMGroupMemberInfoNameCard are available here, others can be retrieved by TIMGroupGetMemberInfoList.)</value>
    public GroupMemberInfo message_sender_group_member_info;
    /// <value>只写(选填), 指定群消息接收成员（定向消息）；不支持群 @ 消息设置，不支持社群（Community）和直播群（AVChatRoom）消息设置；该字段设置后，消息会不计入会话未读数。 (Read & Write (Optional), message target group member userID list, not support for group @ message, not support for Community and AVChatRoom. Once set, this message won't be counted in conversation unread count)</value>
    public List<string> message_target_group_member_array;
    /// <value>读写(选填), 消息的离线推送设置 (Read & Write (Optional), message offline push config)</value>
    public OfflinePushConfig message_offline_push_config;
    /// <value>读写 是否作为会话的 lasgMessage，true - 不作为，false - 作为 (Read & Write (Optional), message is excluded from the lastMessage)</value>
    public bool? message_excluded_from_last_message;
    ///<value>读写 是否不过内容审核（包含【本地审核】和【云端审核】），只有在开通【本地审核】或【云端审核】功能后，该字段设置才有效，设置为 true，表明不过内容审核，设置为 false：表明过内容审核。【本地审核】开通流程请参考 [本地审核功能](https://cloud.tencent.com/document/product/269/83795#.E6.9C.AC.E5.9C.B0.E5.AE.A1.E6.A0.B8.E5.8A.9F.E8.83.BD)。 【云端审核】开通流程请参考 [云端审核功能](https://cloud.tencent.com/document/product/269/83795#.E4.BA.91.E7.AB.AF.E5.AE.A1.E6.A0.B8.E5.8A.9F.E8.83.BD)</value>
    public bool? message_excluded_from_content_moderation;
    ///<value>只读（选填），是否被标记为有安全风险的消息（暂时只支持语音和视频消息），只有在开通【云端审核】功能后才生效，如果您发送的语音或视频消息内容不合规，云端异步审核后会触发 SDK 的 TIMMsgMessageModifiedCallback 回调，回调里的 message 对象该字段值为 true，从 7.4 版本开始支持 (Read only(Optional), whether the message has risk content, supported in version 7.4 and later)</value>
    public bool? message_has_risk_content;
    /// <value>读写(选填), 是否禁用消息发送前云端回调，true: 禁用，false: 不禁用（从 8.1 版本开始支持）(Read & Write (Optional), whether to disable cloud message pre-hook, true: disable, false: enable, supported in version 8.1 and later)</value>
    public bool? message_disable_cloud_message_pre_hook;
    /// <value>读写(选填), 是否禁用消息发送后云端回调，true: 禁用，false: 不禁用（从 8.1 版本开始支持）(Read & Write (Optional), whether to disable cloud message post-hook, true: disable, false: enable, supported in version 8.1 and later)</value>
    public bool? message_disable_cloud_message_post_hook;
    ///<value>只读(选填), 消息撤回者的 user_id, 仅当消息为撤回状态时有效，从 7.4 版本开始支持</value>
    public string? message_revoker_user_id;
    ///<value>只读(选填), 消息撤回者的昵称, 仅当消息为撤回状态时有效，从 7.4 版本开始支持</value>
    public string? message_revoker_nick_name;
    ///<value>消息撤回者的头像地址, 仅当消息为撤回状态时有效，从 7.4 版本开始支持</value>
    public string? message_revoker_face_url;
    ///<value>只读(选填), 消息撤回的原因, 仅当消息为撤回状态时有效，从 7.4 版本开始支持</value>
    public string? message_revoke_reason;
    /// <value>只读(选填), 消息置顶者的 user_id, 只有通过 GetPinnedGroupMessageList 获取到的置顶消息才包含该字段，从 8.0 版本开始支持(Read only(Optional), the user_id of the user who pinned the message is only valid when using the GetPinnedGroupMessageList API, supported in version 8.0 and later)</value>
    public string? message_pinner_user_id;
    /// <value>只读(选填), 消息置顶者的昵称, 只有通过 GetPinnedGroupMessageList 获取到的置顶消息才包含该字段，从 8.0 版本开始支持(Read only(Optional), the nickname of the user who pinned the message is only valid when using the GetPinnedGroupMessageList API, supported in version 8.0 and later)</value>
    public string? message_pinner_nick_name;
    /// <value>只读(选填), 消息置顶者的好友备注, 只有通过 GetPinnedGroupMessageList 获取到的置顶消息才包含该字段，从 8.0 版本开始支持(Read only(Optional), the friend remark of the user who pinned the message is only valid when using the GetPinnedGroupMessageList API, supported in version 8.0 and later)</value>
    public string? message_pinner_friend_remark;
    /// <value>只读(选填), 消息置顶的群成员名片, 只有通过 GetPinnedGroupMessageList 获取到的置顶消息才包含该字段，从 8.0 版本开始支持(Read only(Optional), the group member card of the user who pinned the message is only valid when using the GetPinnedGroupMessageList API, supported in version 8.0 and later)</value>
    public string? message_pinner_name_card;
    /// <value>只读(选填), 消息置顶者的头像, 只有通过 GetPinnedGroupMessageList 获取到的置顶消息才包含该字段，从 8.0 版本开始支持(Read only(Optional), the face_url of the user who pinned the message is only valid when using the GetPinnedGroupMessageList API, supported in version 8.0 and later)</value>
    public string? message_pinner_face_url;
    /// <value>只读，注意：这个字段是内部字段，不推荐使用 (Read only. Caveat: this is SDK internal field, don't use it.)</value>
    public ulong? message_sender_tiny_id;
    /// <value>只读，注意：这个字段是内部字段，不推荐使用 (Read only. Caveat: this is SDK internal field, don't use it.)</value>
    public ulong? message_receiver_tiny_id;
    /// <value>只读, 注意：这个字段是内部字段，不推荐使用，推荐调用 TIMMsgGetMessageReadReceipts 获取群消息已读回执 (Read only. Caveat: this is SDK internal field, please call TIMMsgGetMessageReadReceipts to get group message read receipts.)</value>
    public int? message_group_receipt_read_count;
    /// <value>只读, 注意：这个字段是内部字段，不推荐使用，推荐调用 TIMMsgGetMessageReadReceipts 获取群消息已读回执 (Read only. Caveat: this is SDK internal field, please call TIMMsgGetMessageReadReceipts to get group message read receipts.)</value>
    public int? message_group_receipt_unread_count;
    /// <value>只读，注意：这个字段是内部字段，不推荐使用 (Read only. Caveat: this is SDK internal field, don't use it.)</value>
    public ulong? message_version;
    /// <value>只读, 注意：这个字段是内部字段，不推荐使用 (Read only. Caveat: this is SDK internal field, don't use it.)</value>
    public int? message_risk_type_identified;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class MessageExtension : ExtraData
  {
    /// <value>string, 读写(选填), 扩展字段的 key (Read & Write (Optional), message extension key)</value>
    public string message_extension_key;
    /// <value>string, 读写(选填), 扩展字段的 value (Read & Write (Optional), message extension value)</value>
    public string message_extension_value;
  }
  
  [JsonObject(MemberSerialization.OptOut)]
  public class MessageExtensionResult : ExtraData
  {
    /// <value>object, 只读, 消息扩展操作返回结果所对应的扩展字段 (Read only, message extension item)</value>
    public MessageExtension message_extension_item;
    /// <value>uint32, 只读, 消息扩展操作的返回码 (Read only, result code)</value>
    public int message_extension_result_code;
    /// <value>string, 只读, 消息扩展操作的返回信息提示 (Read only, result infomation)</value>
    public string message_extension_result_info;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class MessageReaction : ExtraData
  {
    ///<value>只读, 消息回应 ID</value>
    public string message_reaction_id;
    ///<value>只读，消息回应总用户个数</value>
    public int message_reaction_total_user_count;
    ///<value>只读, 使用同一个 reaction_id 回应消息的部分用户列表（用户列表数量取决于调用 TIMMsgGetMessageReactions 接口时设置的 max_user_count_per_reaction 值）</value>
    public List<UserInfo> message_reaction_partial_user_info_array;
    ///<value>只读，自己是否使用了该 reaction</value>
    public bool message_reaction_reacted_by_myself;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class MessageReactionResult : ExtraData
  {
    ///<value>只读, 消息扩展操作的返回码</value>
    public int message_reaction_result_code;
    ///<value>只读, 消息扩展操作的返回信息提示</value>
    public string message_reaction_result_info;
    ///<value>只读, 消息 ID</value>
    public string message_reaction_result_msg_id;
    ///<value>只读, 消息回应列表</value>
    public List<MessageReaction> message_reaction_result_reaction_list;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class MessageReactionUserResult : ExtraData
  {
    /// <value>只读, 消息回应用户列表</value>
    public List<UserInfo> message_reaction_user_result_user_info_array;
    /// <value>只读, 消息回应用户列表下次拉取 seq</value>
    public ulong message_reaction_user_result_next_seq;
    /// <value>只读, true : 已拉取完所有消息回应用户列表，false: 未拉取完所有消息回应用户列表</value>
    public bool message_reaction_user_result_is_finished;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class MessageReactionChangeInfo : ExtraData
  {
    public string message_reaction_change_info_msg_id;
    public List<MessageReaction> message_reaction_change_info_reaction_list;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class OfflinePushConfig : ExtraData
  {
    /// <value>读写, 当前消息在对方收到离线推送时候展示标题 ((Read & Write, offline message notification text title))</value>
    public string offline_push_config_title;
    /// <value>读写, 当前消息在对方收到离线推送时候展示内容 (Read & Write, offline message notification text description)</value>
    public string offline_push_config_desc; // 文本消息
    /// <value>读写, 当前消息离线推送时的扩展字段 (Read & Write, offline message notification text extension info)</value>
    public string offline_push_config_ext; // 文本消息
    /// <value>读写, 当前消息是否允许推送，默认允许推送 kTIMOfflinePushFlag_Default (Read & Write, allow offline push or not: Default kTIMOfflinePushFlag_Default)</value>
    public TIMOfflinePushFlag? offline_push_config_flag; // 文本消息
    /// <value>读写, iOS离线推送配置 (Read & Write, iOS offline push config)</value>
    public IOSOfflinePushConfig offline_push_config_ios_config; // 文本消息
    /// <value>读写, Android离线推送配置 (Read & Write, Android offline push config)</value>
    public AndroidOfflinePushConfig offline_push_config_android_config; // 文本消息
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class IOSOfflinePushConfig : ExtraData
  {
    /// <value>读写, 通知标题 (Read & Write, offline push title)</value>
    public string ios_offline_push_config_title; // 文本消息
    /// <value>读写, 当前消息在iOS设备上的离线推送提示声音URL。当设置为push.no_sound时表示无提示音无振动 (Read & Write, offline push sound URL)</value>
    public string ios_offline_push_config_sound; // 文本消息
    /// <value>读写, 是否忽略badge计数。若为true，在iOS接收端，这条消息不会使App的应用图标未读计数增加 (Read & Write, should offline push ignore badge count.)</value>
    public bool? ios_offline_push_config_ignore_badge; // 文本消息
    /// <value>读写，iOS 离线推送的类型（仅对 iOS 生效），默认值是 TIMIOSOfflinePushType_APNS</value>
    public TIMIOSOfflinePushType? ios_offline_push_config_push_type;

  }

  [JsonObject(MemberSerialization.OptOut)]
  public class AndroidOfflinePushConfig : ExtraData
  {
    /// <value>读写, 通知标题 (Read & Write, offline push title)</value>
    public string android_offline_push_config_title; // 文本消息
    /// <value>读写, 当前消息在Android设备上的离线推送提示声音URL (Read & Write, offline push sound URL)</value>
    public string android_offline_push_config_sound; // 文本消息
    /// <value>读写, 当前消息的通知模式 (Read & Write, offline push notify mode)</value>
    public TIMAndroidOfflinePushNotifyMode? android_offline_push_config_notify_mode; // 文本消息
    /// <value>读写，离线推送设置 VIVO 手机 （仅对 Android 生效），VIVO 手机离线推送消息分类，0：运营消息，1：系统消息。默认取值为 1 。 (Read & Write, offline push config for VIVO classification: 0: operation msg, 1: system message. Default 1)</value>
    public int android_offline_push_config_vivo_classification = 1;
    /// <value>读写, OPPO的ChannelID (OPPO ChannelID)</value>
    public string android_offline_push_config_oppo_channel_id; // 文本消息
    ///<value>读写, 离线推送设置 VIVO 推送消息类别，详见：https://dev.vivo.com.cn/documentCenter/doc/359。(VIVO 推送服务于 2023 年 4 月 3 日优化消息分类规则，推荐使用 kTIMAndroidOfflinePushConfigVIVOCategory 设置消息类别，不需要再关注和设置 kTIMAndroidOfflinePushConfigVIVOClassification)</value>
    public string android_offline_push_config_vivo_category;
    ///<value>读写, 离线推送设置小米手机 8.0 系统及以上的渠道 ID（仅对 Android 生效）。</value>
    public string android_offline_push_config_xiaomi_channel_id;
    ///<value>读写, 离线推送设置 FCM 通道手机 8.0 系统及以上的渠道 ID（仅对 Android 生效）。</value>
    public string android_offline_push_config_fcm_channel_id;
    ///<value>读写, 离线推送设置华为推送消息分类，详见：https://developer.huawei.com/consumer/cn/doc/development/HMSCore-Guides/message-classification-0000001149358835</value>
    public string android_offline_push_config_huawei_category;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class Elem : ExtraData
  {
    /// <value>读写(必填), 元素类型 (Read & Write (Required), elem type)</value>
    public TIMElemType elem_type;
    /// <value>读写(必填), 文本内容 (Read & Write (Required), text elem content)</value>
    public string text_elem_content; // 文本消息


    /// <value>读写(必填), 表情索引 (Read & Write (Required), face elem index)</value>
    public int face_elem_index; // 表情消息
    /// <value>读写(选填), 其他额外数据,可由用户自定义填写。若要传输二进制，麻烦先转码成字符串。JSON只支持字符串 (Read & Write (Optional), face elem info custom string)</value>
    public string face_elem_buf; // 表情消息


    /// <value>读写(选填), 位置描述 (Read & Write (Optional), location elem desc)</value>
    public string location_elem_desc; // 位置消息
    /// <value>读写(必填), 经度 (Read & Write (Required), location elem longitude)</value>
    public double location_elem_longitude; // 位置消息
    /// <value>读写(必填), 纬度 (Read & Write (Required), location elem latitude)</value>
    public double location_elem_latitude; // 位置消息


    /// <value>读写(必填), 发送图片的路径 (Read & Write (Required), image elem orignal image path)</value>
    public string image_elem_orig_path; // 图片消息
    /// <value>读写(必填), 发送图片的质量级别 (Read & Write (Required), image elem level)</value>
    public TIMImageLevel image_elem_level; // 图片消息
    /// <value>读写, 发送图片格式:，0xff:未知格式, 1：JPG, 2:GIF, 3:PNG, 4:BMP (Read & Write, image elem format.0xff:unknown, 1：JPG, 2:GIF, 3:PNG, 4:BMP)</value>
    public int image_elem_format; // 图片消息
    /// <value>只读, 原图 ID (Read only, image elem orignal image ID)</value>
    public string image_elem_orig_id; // 图片消息
    /// <value>只读, 原图的图片高度 (Read only, image elem orignal image height)</value>
    public int image_elem_orig_pic_height; // 图片消息
    /// <value>只读, 原图的图片宽度 (Read only, image elem orignal image width)</value>
    public int image_elem_orig_pic_width; // 图片消息
    /// <value>只读, 原图的图片大小 (Read only, image elem orignal image size)</value>
    public int image_elem_orig_pic_size; // 图片消息
    /// <value>只读, 缩略图 ID (Read only, image elem thumbnail ID)</value>
    public string image_elem_thumb_id; // 图片消息
    /// <value>只读, 缩略图的图片高度 (Read only, image elem thumbnail height)</value>
    public int image_elem_thumb_pic_height; // 图片消息
    /// <value>只读, 缩略图的图片宽度 (Read only, image elem thumbnail width)</value>
    public int image_elem_thumb_pic_width; // 图片消息
    /// <value>只读, 缩略图的图片大小 (Read only, image elem thumbnail size)</value>
    public int image_elem_thumb_pic_size; // 图片消息
    /// <value>只读, 大图片uuid (Read only, image elem large image ID)</value>
    public string image_elem_large_id; // 图片消息
    /// <value>只读, 大图片的图片高度 (Read only, image elem large image height)</value>
    public int image_elem_large_pic_height; // 图片消息
    /// <value>只读, 大图片的图片宽度 (Read only, image elem large image width)</value>
    public int image_elem_large_pic_width; // 图片消息
    /// <value>只读, 大图片的图片大小 (Read only, image elem large image size)</value>
    public int image_elem_large_pic_size; // 图片消息
    /// <value>只读, 原图URL (Read only, image elem orignal image URL)</value>
    public string image_elem_orig_url; // 图片消息
    /// <value>只读, 缩略图URL (Read only, image elem thumbnail image URL)</value>
    public string image_elem_thumb_url; // 图片消息
    /// <value>只读, 大图片URL (Read only,  image elem large image URL)</value>
    public string image_elem_large_url; // 图片消息
    /// <value>只读, 任务ID，废弃 (Read only, image elem task ID (deprecated))</value>
    public int image_elem_task_id; // 图片消息


    /// <value>读写(必填), 语音文件路径,需要开发者自己先保存语言然后指定路径 (Read & Write (Required), sound elem file path)</value>
    public string sound_elem_file_path; // 声音消息
    /// <value>读写(必填), 语音数据文件大小 (Read & Write (Required), sound elem file size)</value>
    public int sound_elem_file_size; // 声音消息
    /// <value>读写(必填), 语音时长 (Read & Write (Required), sound elem file time in seconds)</value>
    public int sound_elem_file_time; // 声音消息
    /// <value>只读, 语音 ID (Read only, sonud elem file ID)</value>
    public string sound_elem_file_id; // 声音消息
    /// <value>只读, 下载时用到的businessID (Read only, sound elem businessID for downloading)</value>
    public int sound_elem_business_id; // 声音消息
    /// <value>只读, 是否需要申请下载地址(0:需要申请，1:到cos申请，2:不需要申请,直接拿url下载) (Read only, sound elem download permission flag: 0: need permission, 1: get permission from Tencent COS, 2: no need for permission)</value>
    public int sound_elem_download_flag; // 声音消息
    /// <value>只读,下载的URL (Read only, sound elem URL)</value>
    public string sound_elem_url; // 声音消息
    /// <value>只读,任务ID，废弃 (Read only, sound elem task ID (deprecated))</value>
    public int sound_elem_task_id; // 声音消息


    /// <value>读写, 数据,支持二进制数据 (Read & Write, custom elem data)</value>
    public string custom_elem_data; // 自定义消息
    /// <value>读写, 自定义描述 (Read & Write, custom elem desc)</value>
    public string custom_elem_desc; // 自定义消息
    /// <value>读写, 后台推送对应的ext字段 (Read & Write, custom elem offline push extension)</value>
    public string custom_elem_ext; // 自定义消息
    /// <value>读写, 自定义声音 (Read & Write, custom elem sound)</value>
    public string custom_elem_sound; // 自定义消息


    /// <value>读写(必填), 文件所在路径（包含文件名） (Read & Write (Required), file elem file path)</value>
    public string file_elem_file_path; // 文件消息
    /// <value>读写(必填), 文件名，显示的名称。不设置该参数时，kTIMFileElemFileName默认为kTIMFileElemFilePath指定的文件路径中的文件名 (Read & Write (Required), file elem file name. Default: file name from the file path)</value>
    public string file_elem_file_name; // 文件消息
    /// <value>读写(必填), 文件大小 (Read & Write (Required), file elem file size)</value>
    public int file_elem_file_size; // 文件消息
    /// <value>只读, 文件 ID (Read & Write (Required), file elem file ID)</value>
    public string file_elem_file_id; // 文件消息
    /// <value>只读, 下载时用到的businessID (Read only, file elem businessID for downloading)</value>
    public int file_elem_business_id; // 文件消息
    /// <value>只读, 文件下载flag (Read only, file elem download flag)</value>
    public int file_elem_download_flag; // 文件消息
    /// <value>只读, 文件下载的URL (Read only, file elem url)</value>
    public string file_elem_url; // 文件消息
    /// <value>只读, 任务ID，废弃 (Read only, file elem task ID (deprecated))</value>
    public int file_elem_task_id; // 文件消息


    /// <value>读写(必填), 视频文件类型，发送消息时进行设置 (Read & Write (Required), video elem video type)</value>
    public string video_elem_video_type; // 视频消息
    /// <value>读写(必填), 视频文件大小 (Read & Write (Required), video elem video size)</value>
    public uint video_elem_video_size; // 视频消息
    /// <value>读写(必填), 视频时长，发送消息时进行设置 (Read & Write (Required), video elem video duration)</value>
    public uint video_elem_video_duration; // 视频消息
    /// <value>读写(必填), 适配文件路径 (Read & Write (Required), video elem video path)</value>
    public string video_elem_video_path; // 视频消息
    /// <value>只读, 视频 ID (Read only, video elem video ID)</value>
    public string video_elem_video_id; // 视频消息
    /// <value>只读, 下载时用到的businessID (Read only, video elem businessID for downloading)</value>
    public int video_elem_business_id; // 视频消息
    /// <value>只读, 视频文件下载flag (Read only, video elem video download flag)</value>
    public int video_elem_video_download_flag; // 视频消息
    /// <value>只读, 视频文件下载的URL(Read only, video elem video url)</value>
    public string video_elem_video_url; // 视频消息
    /// <value>读写(必填), 截图文件类型，发送消息时进行设置 (Read & Write (Required), video elem thumbnail type)</value>
    public string video_elem_image_type; // 视频消息
    /// <value>读写(必填), 截图文件大小 (Read & Write (Required), video elem thumbnail size)</value>
    public uint video_elem_image_size; // 视频消息
    /// <value>读写(必填), 截图高度，发送消息时进行设置 (Read & Write (Required), video elem thumbnail width)</value>
    public uint video_elem_image_width; // 视频消息
    /// <value>读写(必填), 截图宽度，发送消息时进行设置 (Read & Write (Required), video elem thumbnail height)</value>
    public uint video_elem_image_height; // 视频消息
    /// <value>读写(必填), 保存截图的路径 (Read & Write (Required), video elem thubmnail path)</value>
    public string video_elem_image_path; // 视频消息
    /// <value>只读, 截图 ID (Read & Write (Required), video elem thumbnail ID)</value>
    public string video_elem_image_id; // 视频消息
    /// <value>只读, 截图文件下载flag(Read & Write (Required), video elem thubmnail download flag)</value>
    public int video_elem_image_download_flag; // 视频消息
    /// <value>只读, 截图文件下载的URL(Read only, video elem thumbnail URL)</value>
    public string video_elem_image_url; // 视频消息
    /// <value>只读, 任务ID，废弃 (Read only, video elem task ID (deprecated))</value>
    public uint video_elem_task_id; // 视频消息


    /// <value>读写(必填), 合并消息 title (Read & Write (Required), merge elem title)</value>
    public string merge_elem_title; // 合并消息
    /// <value>读写(必填), 合并消息摘要列表 (Read & Write (Required), merge elem abstract info list)</value>
    public List<string> merge_elem_abstract_array; // 合并消息
    /// <value>读写(必填), 消息列表（最大支持 300 条，消息对象必须是 kTIMMsg_SendSucc 状态，消息类型不能为 GroupTipsElem 或 GroupReportElem） (Read & Write (Required), merge elem message list, maximum 300, must be in kTIMMsg_SendSucc status and cannot be GroupTipsElem or GroupReportElem)</value>
    public List<Message> merge_elem_message_array; // 合并消息
    /// <value>读写(必填), 合并消息兼容文本，低版本 SDK 如果不支持合并消息，默认会收到一条文本消息，文本消息的内容为 compatibleText，该参数不能为空。 (Read & Write (Required), merge elem compatibleText, fallback text for lower ver. SDK)</value>
    public string merge_elem_compatible_text; // 合并消息
    /// <value>只读, 合并消息里面又包含合并消息我们称之为合并嵌套，合并嵌套层数不能超过 100 层，如果超过限制，layersOverLimit 会返回 YES，kTIMMergerElemTitle 和 kTIMMergerElemAbstractArray 为空，DownloadMergerMessage 会返回 ERR_MERGER_MSG_LAYERS_OVER_LIMIT 错误码。 (Read only, merge elem is layer over limit. If merged message layer is over 100, this will be True and YES，kTIMMergerElemTitle and kTIMMergerElemAbstractArray will be empty. 为空，DownloadMergerMessage will return ERR_MERGER_MSG_LAYERS_OVER_LIMIT error)</value>
    public bool merge_elem_layer_over_limit; // 合并消息
    /// <value>只读, native 端消息列表下载 key (Read only, merge elem relay pb key for native end)</value>
    public string merge_elem_relay_pb_key; // 合并消息
    /// <value>只读, web 端消息列表下载 key (Read only, merge elem relay json key for web end)</value>
    public string merge_elem_relay_json_key; // 合并消息
    /// <value>只读, 转发消息的 buffer (Read only, merge elem relay buffer)</value>
    public string merge_elem_relay_buffer; // 合并消息


    /// <value>只读, 群消息类型 (Read only, group tips elem tip type)</value>
    public TIMGroupTipType group_tips_elem_tip_type; // 群组系统消息元素
    /// <value>只读, 操作者ID (Read only, group tips elem operator userID)</value>
    public string group_tips_elem_op_user; // 群组系统消息元素
    /// <value>只读, 群组名称 (Read only, group tips elem group name)</value>
    public string group_tips_elem_group_name; // 群组系统消息元素
    /// <value>只读, 群组ID (Read only, group tips elem group ID)</value>
    public string group_tips_elem_group_id; // 群组系统消息元素
    /// <value>只读, 群消息时间，废弃 (Read only, group tips elem timestamp (deprecated))</value>
    public uint group_tips_elem_time; // 群组系统消息元素
    /// <value>只读, 被操作的帐号列表 (Read only, group tips elem operant user ID list)</value>
    public List<string> group_tips_elem_user_array; // 群组系统消息元素
    /// <value>只读, 群资料变更信息列表,仅当 tips_type 值为 kTIMGroupTip_GroupInfoChange 时有效 (Read only, group tips elem changed group info list, works only when tips_type is kTIMGroupTip_GroupInfoChange)</value>
    public List<GroupTipGroupChangeInfo> group_tips_elem_group_change_info_array; // 群组系统消息元素
    /// <value>只读, 群成员变更信息列表,仅当 tips_type 值为 kTIMGroupTip_MemberInfoChange 时有效 (Read only, group tips elem changed member info list, works only when tips_type is kTIMGroupTip_MemberInfoChange)</value>
    public List<GroupTipMemberChangeInfo> group_tips_elem_member_change_info_array; // 群组系统消息元素
    /// <value>只读, 操作者个人资料 (Read only, group tips elem operant user info)</value>
    public UserProfile group_tips_elem_op_user_info; // 群组系统消息元素
    /// <value>只读, 群成员信息 (Read only, group tips elem operant group member info)</value>
    public GroupMemberInfo group_tips_elem_op_group_memberinfo; // 群组系统消息元素
    /// <value>只读, 被操作者列表资料 (Read only, group tips elem operant user info list)</value>
    public List<UserProfile> group_tips_elem_changed_user_info_array; // 群组系统消息元素
    /// <value>只读, 群成员信息列表 (Read only, group tips elem changed group member info list)</value>
    public List<GroupMemberInfo> group_tips_elem_changed_group_memberinfo_array; // 群组系统消息元素
    /// <value>只读, 当前群成员数,只有当事件消息类型为 kTIMGroupTip_Invite 、 kTIMGroupTip_Quit 、 kTIMGroupTip_Kick 时有效 (Read only, group tips elem current member count, works only when tips_type is kTIMGroupTip_Invite, kTIMGroupTip_Quit or kTIMGroupTip_Kick)</value>
    public uint group_tips_elem_member_num; // 群组系统消息元素
    /// <value>只读, 操作方平台信息 (Read only, group tips elem platform)</value>
    public string group_tips_elem_platform; // 群组系统消息元素


    /// <value>只读, 类型 (Read only, group report elem report type )</value>
    public TIMGroupReportType group_report_elem_report_type; // 群组系统通知元素(针对个人)
    /// <value>只读, 群组ID (Read only, group report elem group ID)</value>
    public string group_report_elem_group_id; // 群组系统通知元素(针对个人)
    /// <value>只读, 群组名称 (Read only, group report elem group name)</value>
    public string group_report_elem_group_name; // 群组系统通知元素(针对个人)
    /// <value>只读, 操作者ID (Read only, group report elem operant user ID)</value>
    public string group_report_elem_op_user; // 群组系统通知元素(针对个人)
    /// <value>只读, 操作理由 (Read only, group report elem reason message)</value>
    public string group_report_elem_msg; // 群组系统通知元素(针对个人)
    /// <value>只读, 操作者填的自定义数据 (Read only, group report elem user data)</value>
    public string group_report_elem_user_data; // 群组系统通知元素(针对个人)
    /// <value>只读, 操作者个人资料 (Read only, group report elem operator user info)</value>
    public UserProfile group_report_elem_op_user_info; // 群组系统通知元素(针对个人)
    /// <value>只读,操作者群内资料 (Read only, group report elem operator group member info)</value>
    public GroupMemberInfo group_report_elem_op_group_memberinfo; // 群组系统通知元素(针对个人)
    /// <value>只读, 操作方平台信息 (Read only, group report elem platform)</value>
    public string group_report_elem_platform;


    /// <value>只读, 资料变更类型 (Read only, profile change elem changed type)</value>
    public TIMProfileChangeType profile_change_elem_change_type; // GroupMemberInfo
    /// <value>只读, 资料变更用户的UserID (Read only, profile change elem changed user ID)</value>
    public string profile_change_elem_from_identifier; // GroupMemberInfo
    /// <value>只读, 具体的变更信息，只有当 change_type 为 kTIMProfileChange_Profile 时有效 (Read only, profile change elem user profile item, works only when change_type is kTIMProfileChange_Profile)</value>
    public UserProfileItem profile_change_elem_user_profile_item; // GroupMemberInfo
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class UserProfileItem : ExtraData
  {
    /// <value>只写, 修改用户昵称 (Write only, nick name)</value>
    public string user_profile_item_nick_name; // UserProfileItem
    /// <value>只写, 修改用户性别 (Write only, gender)</value>
    public TIMGenderType? user_profile_item_gender; // UserProfileItem
    /// <value>只写, 修改用户头像 (Write only, avatar URL)</value>
    public string user_profile_item_face_url; // UserProfileItem
    /// <value>只写, 修改用户签名 (Write only, self signature)</value>
    public string user_profile_item_self_signature; // UserProfileItem
    /// <value>只写, 修改用户加好友的选项 (Write only, add permission type)</value>
    public TIMProfileAddPermission? user_profile_item_add_permission; // UserProfileItem
    /// <value>只写, 修改位置 (Write only, location)</value>
    public string user_profile_item_location; // UserProfileItem
    /// <value>只写, 修改语言 (Write only, preferred language)</value>
    public uint? user_profile_item_language; // UserProfileItem
    /// <value>只写, 修改生日 (Write only, birthday)</value>
    public uint? user_profile_item_birthday; // UserProfileItem
    /// <value>只写, 修改等级 (Write only, level)</value>
    public uint? user_profile_item_level; // UserProfileItem
    /// <value>只写, 修改角色 (Write only, role)</value>
    public uint? user_profile_item_role; // UserProfileItem
    /// <value>只写, 修改[自定义资料字段](https://cloud.tencent.com/document/product/269/1500#.E8.87.AA.E5.AE.9A.E4.B9.89.E8.B5.84.E6.96.99.E5.AD.97.E6.AE.B5) (Write only, custom key-value pair. Check [Custom Profile Fields](https://www.tencentcloud.com/document/product/1047/33520))</value>
    public List<UserProfileCustomStringInfo> user_profile_item_custom_string_array; // UserProfileItem
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class GroupMemberInfo : ExtraData
  {
    /// <value>读写(必填), 群组成员ID (Read & Write (Required), group member user ID)</value>
    public string group_member_info_identifier; // GroupMemberInfo
    /// <value>只读, 群组 ID (Read only, group ID)</value>
    public string group_member_info_group_id; // GroupMemberInfo
    /// <value>只读, 群组成员加入时间 (Read only, member joined time)</value>
    public uint group_member_info_join_time; // GroupMemberInfo
    /// <value>读写(选填), 群组成员角色 (Read & Write (Optional), member role)</value>
    public TIMGroupMemberRole group_member_info_member_role; // GroupMemberInfo
    /// <value>只读, 成员接收消息的选项 (Read only, message receiving option)</value>
    public TIMReceiveMessageOpt group_member_info_msg_flag; // GroupMemberInfo
    /// <value>只读, 消息序列号 (Read only, message sequence)</value>
    public uint group_member_info_msg_seq; // GroupMemberInfo
    /// <value>只读, 成员禁言时间 (Read only, muted time)</value>
    public uint group_member_info_shutup_time; // GroupMemberInfo
    /// <value>只读, 成员群名片 (Read only, member's name card)</value>
    public string group_member_info_name_card; // GroupMemberInfo
    /// <value>只读, 好友昵称 (Read only, member's nickname)</value>
    public string group_member_info_nick_name; // GroupMemberInfo
    /// <value>只读，好友备注 (Read only, member's remark)</value>
    public string group_member_info_remark;// string, 只读, 好友备注
    /// <value>只读, 好友头像 (Read only, member's avatar URL)</value>
    public string group_member_info_face_url; // GroupMemberInfo
    /// <value>只读, 请参考[自定义字段](https://cloud.tencent.com/document/product/269/1502#.E8.87.AA.E5.AE.9A.E4.B9.89.E5.AD.97.E6.AE.B5) (Read only, custom key-value pair. Check [Custom Group Fields](https://www.tencentcloud.com/document/product/1047/33529))</value>
    public List<GroupMemberInfoCustomString> group_member_info_custom_info; // GroupMemberInfo
    public List<string> group_member_info_online_devices;
    public string group_member_info_is_online;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class GroupTipGroupChangeInfo : ExtraData
  {
    /// <value>只读, 群消息修改群信息标志 (Read only, group tips change info flag)</value>
    public TIMGroupTipGroupChangeFlag group_tips_group_change_info_flag;
    /// <value>只读, 自定义信息对应的 key 值，只有 info_flag 为 kTIMGroupTipChangeFlag_Custom 时有效 (Read only, change info key, works only when info_flag is kTIMGroupTipChangeFlag_Custom)</value>
    public string group_tips_group_change_info_key;
    /// <value>只读, 修改的后值,不同的 info_flag 字段,具有不同的含义 (Read only, changed info value)</value>
    public string group_tips_group_change_info_value;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class GroupMemberInfoCustomString : ExtraData
  {
    /// <value>只写, 自定义字段的key (Write only, custom string info key)</value>
    public string group_member_info_custom_string_info_key;
    /// <value>只写, 自定义字段的value (Write only, custom string info value)</value>
    public string group_member_info_custom_string_info_value;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class GroupTipMemberChangeInfo : ExtraData
  {
    /// <value>只读, 群组成员ID (Read only, group member user ID)</value>
    public string group_tips_member_change_info_identifier;
    /// <value>只读, 禁言时间 (Read only, mute time)</value>
    public uint group_tips_member_change_info_shutupTime;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class DraftParam : ExtraData
  {
    /// <value>只读, 草稿最新编辑时间 (Read only, draft edit time)</value>
    public ulong draft_edit_time;
    /// <value>只读, 用户自定义数据 (Read only, draft custom data)</value>
    public string draft_user_define;
    /// <value>只读, 草稿内的消息 (Read only, draft message)</value>
    public Message draft_msg;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class MsgLocator : ExtraData
  {
    /// <value>读写,要查找的消息所属的会话ID (Read & Write, conversation ID of the searched message)</value>
    public string message_locator_conv_id;
    /// <value>读写,要查找的消息所属的会话类型 (Read & Write, conversation type of the searched message)</value>
    public TIMConvType message_locator_conv_type;
    /// <value>读写(必填), 要查找的消息是否是被撤回。true表示被撤回的，false表示未撤回的。默认为false (Read & Write (Required), is message revoked, default: false)</value>
    public bool message_locator_is_revoked;
    /// <value>读写(必填), 要查找的消息的时间戳 (Read & Write (Required), message timestamp)</value>
    public ulong message_locator_time;
    /// <value>读写(必填), 要查找的消息的序列号 (Read & Write (Required), message sequence number)</value>
    public ulong message_locator_seq;
    /// <value>读写(必填), 要查找的消息的发送者是否是自己。true表示发送者是自己，false表示发送者不是自己。默认为false (Read & Write (Required), is message from self, default: false)</value>
    public bool message_locator_is_self;
    /// <value>读写(必填), 要查找的消息随机码 (Read & Write (Required), message random code)</value>
    public ulong message_locator_rand;
    /// <value>读写(必填), 要查找的消息的唯一标识 (Read & Write (Required), message unique ID)</value>
    public ulong message_locator_unique_id;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class MsgGetMsgListParam : ExtraData
  {
    /// <value>只写(选填), 指定的消息，不允许为null (Write (Optional), specified message, can't be null)</value>
    public Message msg_getmsglist_param_last_msg;
    /// <value>只写(选填), 从指定消息往后的消息数 (Write (Optional), message count)</value>
    public uint? msg_getmsglist_param_count;
    /// <value>只写(选填), 是否漫游消息 (Write (Optional), is ramble)</value>
    public bool? msg_getmsglist_param_is_ramble;
    /// <value>只写(选填), 是否向前排序 (Write (Optional), is forwarding messages)</value>
    public bool? msg_getmsglist_param_is_forward;
    /// <value>只写(选填), 指定的消息的 seq (Write (Optional), specified message sequence)</value>
    public ulong? msg_getmsglist_param_last_msg_seq;
    /// <value>只写(选填), 开始时间；UTC 时间戳， 单位：秒 (Write (Optional), begin time, UTC timestamp, unit: second)</value>
    public ulong? msg_getmsglist_param_time_begin;
    /// <value>只写(选填), 持续时间；单位：秒 (Write (Optional), duration, timestamp, unit: second)</value>
    public ulong? msg_getmsglist_param_time_period;

    /// <value>只写(选填)，拉取的消息 seq 集合，仅针对 group 有效</value>
    public List<ulong>? msg_getmsglist_param_message_seq_array;
    /// <value>只写(选填)，last message id</value>
    public string? msg_last_msg_id;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class MsgDeleteParam : ExtraData
  {
    /// <value>只写(选填), 要删除的消息 (Write (Optional), deleted message)</value>
    public Message msg_delete_param_msg;
    /// <value>只写(选填), 是否删除本地/漫游所有消息。true删除漫游消息，false删除本地消息，默认值false (Write (Optional), is deleting ramble message, default: false)</value>
    public bool? msg_delete_param_is_ramble;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class DownloadElemParam : ExtraData
  {
    /// <value>只写, 从消息元素里面取出来,元素的下载类型 (Write only, download elem flag)</value>
    public uint msg_download_elem_param_flag;
    /// <value>只写, 从消息元素里面取出来,元素的类型 (Write only, download elem type)</value>
    public TIMDownloadType msg_download_elem_param_type;
    /// <value>只写, 从消息元素里面取出来,元素的ID (Write only, download elem ID)</value>
    public string msg_download_elem_param_id;
    /// <value>只写, 从消息元素里面取出来,元素的BusinessID (Write only, download elem businessID)</value>
    public uint msg_download_elem_param_business_id;
    /// <value>只写, 从消息元素里面取出来,元素URL (Write only, download elem URL)</value>
    public string msg_download_elem_param_url;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class MsgBatchSendParam : ExtraData
  {
    /// <value>只写(必填), 接收群发消息的用户 ID 列表 (Write (Required), batch receiver user ID list)</value>
    public List<string> msg_batch_send_param_identifier_array;
    /// <value>只写(必填), 群发的消息 (Write (Required), batch messages)</value>
    public Message msg_batch_send_param_msg;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class MessageSearchParam : ExtraData
  {
    /// <value>只写(必填)，搜索关键字列表，最多支持5个。 (Write (Required), searching keyword, maximum 5)</value>
    public List<string> msg_search_param_keyword_array;
    /// <value>只写(选填), 指定搜索的消息类型集合，传入空数组，表示搜索支持的全部类型消息（FaceElem 和 GroupTipsElem 暂不支持）取值详见 TIMElemType。 (Write (Optional), searched message type list, can't search FaceElem and GroupTipsElem)</value>
    public List<TIMElemType> msg_search_param_message_type_array;
    /// <value>只写(选填)，会话 ID (Write (Optional), conversation ID)</value>
    public string msg_search_param_conv_id;
    /// <value>只写(选填), 会话类型，如果设置 kTIMConv_Invalid，代表搜索全部会话。否则，代表搜索指定会话。 (Write (Optional), conversation type, set kTIMConv_Invalid to search all the conversation)</value>
    public TIMConvType? msg_search_param_conv_type;
    /// <value>只写(选填), 搜索的起始时间点。默认为0即代表从现在开始搜索。UTC 时间戳，单位：秒 (Write (Optional), start time in second, UTC timestamp, default: 0 means search from now)</value>
    public ulong? msg_search_param_search_time_position;
    /// <value>只写(选填), 从起始时间点开始的过去时间范围，单位秒。默认为0即代表不限制时间范围，传24x60x60代表过去一天。 (Write (Optional), time period, unit: second, default: 0 means no restriction, 24 * 60 * 60 means search for 1 day)</value>
    public ulong? msg_search_param_search_time_period;
    /// <value>只写(选填), 分页的页号：用于分页展示查找结果，从零开始起步。首次调用：通过参数 pageSize = 10, pageIndex = 0 调用 searchLocalMessage，从结果回调中的 totalCount 可以获知总共有多少条结果。(Write (Optional), page index, start from zero. First search set pageIndex as 0 and call searchLocalMessage to get totalCount of the message)</value>
    public uint? msg_search_param_page_index;
    /// <value>只写(选填), 每页结果数量：用于分页展示查找结果，如不希望分页可将其设置成 0，但如果结果太多，可能会带来性能问题。 (Write (Optional), search page size)</value>
    public uint? msg_search_param_page_size;
    /// <value>关键字进行 Or 或者 And 进行搜索 (AND keywords or OR keywords)</value>
    public TIMKeywordListMatchType msg_search_param_keyword_list_match_type;
    /// <value>按照发送者的 userid 进行搜索 (Search by sender user ID list)</value>
    public List<string> msg_search_param_send_identifier_array;
    /// <value>只写(选填), 服务武器搜索结果数量。</value>
    public uint? msg_search_param_search_count;
    /// <value>只写(选填)，服务武器搜索游标。第一次填空字符串，续拉时填写 [MessageSearchResult]() 中的返回值。</value>
    public string? msg_search_param_search_cursor;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class CreateGroupParam : ExtraData
  {
    /// <value>只写(必填), 群组名称 (Write (Required), group name)</value>
    public string create_group_param_group_name;
    /// <value>只写(选填), 群组ID,不填时创建成功回调会返回一个后台分配的群ID，如果创建社群（Community）需要自定义群组 ID ，那必须以 "@TGS#_" 作为前缀。 (Write (Optional), group ID, if null, the IM will designate an ID for the group. Community ID must start by "@TGS#_")</value>
    public string create_group_param_group_id;
    /// <value>只写(选填), 群组类型,默认为Public (Write (Optional), group type, default: Public)</value>
    public TIMGroupType? create_group_param_group_type;
    /// <value>只写(选填), 社群是否支持创建话题, 只在群类型为 Community 时有效 (Write (Optional), support topic or not, only valid when group type is Community)</value>
    public bool? create_group_param_is_support_topic;
    /// <value>只写(选填), 群组初始成员数组 (Write (Optional), init group members)</value>
    public List<GroupMemberInfo> create_group_param_group_member_array;
    /// <value>只写(选填), 群组公告 (Write (Optional), group notification)</value>
    public string create_group_param_notification;
    /// <value>只写(选填), 群组简介 (Write (Optional), group introduction)</value>
    public string create_group_param_introduction;
    /// <value>只写(选填), 群组头像URL (Write (Optional), group avatar URL)</value>
    public string create_group_param_face_url;
    /// <value>只写(选填), 加群选项，默认为Any (Write (Optional), join group option, default: Any)</value>
    public TIMGroupAddOption? create_group_param_add_option;
    /// <value>只写(选填), 邀请进群审批选项 (Write (Optional), invite group approval option)</value>
    public TIMGroupAddOption? create_group_param_approve_option;

    /// <value>只写(选填), 群组最大成员数 (Write (Optional), group max member number)</value>
    public uint? create_group_param_max_member_num;
    /// <value>只读(选填), 请参考[自定义字段](https://cloud.tencent.com/document/product/269/1502#.E8.87.AA.E5.AE.9A.E4.B9.89.E5.AD.97.E6.AE.B5) (Write (Optional), Check [Custom Group Fields](https://www.tencentcloud.com/document/product/1047/33529))</value>
    public List<GroupInfoCustomString> create_group_param_custom_info;
    /// <value>只写(选填), 开启权限组功能, 仅支持社群, 7.8 版本开始支持。开启后，管理员角色的权限失效，用群权限、话题权限和权限组能力来对社群、话题进行管理。 (Write (Optional), enable permission group, it only supports the community and has been supported since version 7.8.)</value>
    public bool? create_group_param_enable_permission_group;
    /// <value>只写(选填), 群默认权限, 仅支持社群, 7.8 版本开始支持。群成员在没有加入任何权限组时的默认权限，仅在 kTIMCreateGroupParamEnablePermissionGroup 为 true 时生效 (Write (Optional), group default permissions, it only supports the community and has been supported since version 7.8.)</value>
    public ulong? create_group_param_default_permissions;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class GroupInfoCustomString : ExtraData
  {
    /// <value>只写, 自定义字段的key (Write only, group custom string key)</value>
    public string group_info_custom_string_info_key;
    /// <value>只写, 自定义字段的value (Write only, group custom string value)</value>
    public string group_info_custom_string_info_value;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class GroupInviteMemberParam : ExtraData
  {
    /// <value>只写(必填), 群组ID (Write (Required), group ID)</value>
    public string group_invite_member_param_group_id;
    /// <value>只写(必填), 被邀请加入群组用户ID数组 (Write (Required), invited member user ID list)</value>
    public List<string> group_invite_member_param_identifier_array;
    /// <value>只写(选填), 用于自定义数据 (Write (Optional), group invite member custom data)</value>
    public string group_invite_member_param_user_data;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class GroupDeleteMemberParam : ExtraData
  {
    /// <value>只写(必填), 群组ID (Write (Required), group ID)</value>
    public string group_delete_member_param_group_id;
    /// <value>只写(必填), 被删除群组成员数组 (Write (Required), deleted member user ID list)</value>
    public List<string> group_delete_member_param_identifier_array;
    /// <value>只写(选填), 用于自定义数据 (Write (Optional), group delete member custom data)</value>
    public string group_delete_member_param_user_data;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class GroupModifyInfoParam : ExtraData
  {
    /// <value>只写(必填), 群组ID (Write (Required), group ID)</value>
    public string group_modify_info_param_group_id;
    /// <value>只写(必填), 修改标识,可设置多个值按位或 (Write (Required), modify flag, can set multiple values by bit union)</value>
    public TIMGroupModifyInfoFlag group_modify_info_param_modify_flag;
    /// <value>只写(选填), 修改群组名称,当 modify_flag 包含 kTIMGroupModifyInfoFlag_Name 时必填,其他情况不用填 (Write (Optional), modified group name, required when modify_flag contains kTIMGroupModifyInfoFlag_Name)</value>
    public string group_modify_info_param_group_name;
    /// <value>只写(选填), 修改群公告,当 modify_flag 包含 kTIMGroupModifyInfoFlag_Notification 时必填,其他情况不用填 (Write (Optional), modified group notification, required when modify_flag contains kTIMGroupModifyInfoFlag_Notification)</value>
    public string group_modify_info_param_notification;
    /// <value>只写(选填), 修改群简介,当 modify_flag 包含 kTIMGroupModifyInfoFlag_Introduction 时必填,其他情况不用填 (Write (Optional), modified group introduction, required when modify_flag contains kTIMGroupModifyInfoFlag_Introduction)</value>
    public string group_modify_info_param_introduction;
    /// <value>只写(选填), 修改群头像URL, 当 modify_flag 包含 kTIMGroupModifyInfoFlag_FaceUrl 时必填,其他情况不用填 (Write (Optional), modified group avatar URL, required when modify_flag contains kTIMGroupModifyInfoFlag_FaceUrl)</value>
    public string group_modify_info_param_face_url;
    /// <value>只写(选填), 修改加群方式, 当 modify_flag 包含 kTIMGroupModifyInfoFlag_AddOption 时必填,其他情况不用填 (Write (Optional), modified join group option, required when modify_flag contains kTIMGroupModifyInfoFlag_AddOption)</value>
    public TIMGroupAddOption? group_modify_info_param_add_option;
    /// <value>只写(选填), 修改群最大成员数,当 modify_flag 包含 kTIMGroupModifyInfoFlag_MaxMemberNum 时必填,其他情况不用填 (Write (Optional), modified group maxmium member number, required when modify_flag contains kTIMGroupModifyInfoFlag_MaxMemberNum)</value>
    public uint? group_modify_info_param_max_member_num;
    /// <value>只写(选填), 修改群是否可见,当 modify_flag 包含 kTIMGroupModifyInfoFlag_Visible 时必填,其他情况不用填 (Write (Optional), modified group is visible, required when modify_flag contains kTIMGroupModifyInfoFlag_Visible)</value>
    public uint? group_modify_info_param_visible;
    /// <value>只写(选填), 修改群是否允许被搜索, 当 modify_flag 包含 kTIMGroupModifyInfoFlag_Searchable 时必填,其他情况不用填 (Write (Optional), modified group is searchable, required when modify_flag contains kTIMGroupModifyInfoFlag_Searchable)</value>
    public uint? group_modify_info_param_searchable;
    /// <value>只写(选填), 修改群是否全体禁言,当 modify_flag 包含 kTIMGroupModifyInfoFlag_ShutupAll 时必填,其他情况不用填 (Write (Optional), modified group members are all muted, required when modify_flag contains kTIMGroupModifyInfoFlag_ShutupAll)</value>
    public bool? group_modify_info_param_is_shutup_all;
    /// <value>只写(选填), 修改群主所有者, 当 modify_flag 包含 kTIMGroupModifyInfoFlag_Owner 时必填,其他情况不用填。此时 modify_flag 不能包含其他值，当修改群主时，同时修改其他信息已无意义 (Write (Optional), modified group owner, required when modify_flag contains kTIMGroupModifyInfoFlag_Owner, and modify_flag can't contain other values)</value>
    public string group_modify_info_param_owner;
    /// <value>只写(选填), 请参考[自定义字段](https://cloud.tencent.com/document/product/269/1502#.E8.87.AA.E5.AE.9A.E4.B9.89.E5.AD.97.E6.AE.B5) (Write (Optional), Check [Custom Group Fields](https://www.tencentcloud.com/document/product/1047/33529))</value>
    public List<GroupInfoCustomString> group_modify_info_param_custom_info;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class OfflinePushToken : ExtraData
  {
    /// <value>只写（选填）, 注册应用到厂商平台或者 TPNS 时获取的 token。使用注意事项：当接入推送 TPNS 通道，需要设置 isTPNSToken 为 true，上报注册 TPNS 获取的 token；当接入推送为厂商通道，需要设置 isTPNSToken 为 false，上报注册厂商获取的 token。 (Write (Optional), offline push token. Caveat: when using TPNS channel, isTPNSToken needs to be set to true, and report TPNS token; when using vendor channels, isTPNSToken needs to be set to false, and report vendor token)</value>
    public string offline_push_token_token;
    /// <value>只写（选填），IM 控制台证书 ID，接入 TPNS 时不需要填写 (Write (Optional), IM console certificate ID, no need when using TPNS)</value>
    public int? offline_push_token_business_id;
    /// <value>只写（选填），是否接入配置 TPNS, token 是否是从 TPNS 获取 (Write (Optional), is token from TPNS token)</value>
    public bool? offline_push_token_is_tpns_token;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class GroupGetMemberInfoListParam : ExtraData
  {
    /// <value>只写(必填), 群组ID (Write (Required), group ID)</value>
    public string group_get_members_info_list_param_group_id;
    /// <value>只写(选填), 群成员ID列表 (Write (Optional), group member user ID list)</value>
    public List<string> group_get_members_info_list_param_identifier_array;
    /// <value>只写(选填), 获取群成员信息的选项 (Write (Optional), get member info option)</value>
    public GroupMemberGetInfoOption group_get_members_info_list_param_option;
    /// <value>只写(选填), 分页拉取标志,第一次拉取填0,回调成功如果不为零,需要分页,调用接口传入再次拉取,直至为0 (Write (Optional), page index, default: 0, and if return seq is not zero, you have more pages to be retrieved, until it's zero)</value>
    public ulong? group_get_members_info_list_param_next_seq;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class GroupMemberGetInfoOption : ExtraData
  {
    /// <value>读写(选填), 根据想要获取的信息过滤，默认值为0xffffffff(获取全部信息) (Write (Optional), get group member info flag, default: 0xffffffff)</value>
    public TIMGroupMemberInfoFlag? group_member_get_info_option_info_flag;
    /// <value>读写(选填), 根据成员角色过滤，默认值为kTIMGroupMemberRoleFlag_All，获取所有角色 (Write (Optional), filter by role flag, default: kTIMGroupMemberRoleFlag_All)</value>
    public TIMGroupMemberRoleFlag? group_member_get_info_option_role_flag;
    /// <value>只写(选填), 请参考[自定义字段](https://cloud.tencent.com/document/product/269/1502#.E8.87.AA.E5.AE.9A.E4.B9.89.E5.AD.97.E6.AE.B5) (Write (Optional), Check [Custom Profile Fields](https://www.tencentcloud.com/document/product/1047/33520))</value>
    public List<string> group_member_get_info_option_custom_array;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class GroupModifyMemberInfoParam : ExtraData
  {
    /// <value>只写(必填), 群组ID (Write (Required), group ID)</value>
    public string group_modify_member_info_group_id;
    /// <value>只写(必填), 被设置信息的成员ID (Write (Required), modified group member user ID)</value>
    public string group_modify_member_info_identifier;
    /// <value>只写(必填), 修改类型,可设置多个值按位或 (Write (Required), modify type, bit union)</value>
    public TIMGroupMemberModifyInfoFlag group_modify_member_info_modify_flag;
    /// <value>只写(选填), 修改消息接收选项,当 modify_flag 包含 kTIMGroupMemberModifyFlag_MsgFlag 时必填,其他情况不用填 (Write (Optional), modified message receiving option, required when modify_flag contains kTIMGroupMemberModifyFlag_MsgFlag)</value>
    public TIMReceiveMessageOpt? group_modify_member_info_msg_flag;
    /// <value>只写(选填), 修改成员角色, 当 modify_flag 包含 kTIMGroupMemberModifyFlag_MemberRole 时必填,其他情况不用填 (Write (Optional), modified group member role, required when modify_flag contains kTIMGroupMemberModifyFlag_MemberRole)</value>
    public TIMGroupMemberRole? group_modify_member_info_member_role;
    /// <value>只写(选填), 修改禁言时间,当 modify_flag 包含 kTIMGroupMemberModifyFlag_ShutupTime 时必填,其他情况不用填 (Write (Optional), modified mute time, required when modify_flag contains kTIMGroupMemberModifyFlag_ShutupTime)</value>
    public uint? group_modify_member_info_shutup_time;
    /// <value>只写(选填), 修改群名片,当 modify_flag 包含 kTIMGroupMemberModifyFlag_NameCard 时必填,其他情况不用填 (Write (Optional), modified member name card, required when modify_flag contains kTIMGroupMemberModifyFlag_NameCard)</value>
    public string group_modify_member_info_name_card;
    /// <value>只写(选填), 请参考[自定义字段](https://cloud.tencent.com/document/product/269/1502#.E8.87.AA.E5.AE.9A.E4.B9.89.E5.AD.97.E6.AE.B5) (Write (Optional), Check [Custom Group Fields](https://www.tencentcloud.com/document/product/1047/33529))</value>
    public List<GroupMemberInfoCustomString> group_modify_member_info_custom_info;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class GroupPendencyOption : ExtraData
  {
    /// <value>只写(必填), 设置拉取时间戳,第一次请求填0,后边根据server返回的[GroupPendencyResult]()键kTIMGroupPendencyResultNextStartTime指定的时间戳进行填写 (Write (Required), group pendency start time, start by 0 and use returned kTIMGroupPendencyResultNextStartTime from GroupPendencyResult to set mext start time)</value>
    public ulong group_pendency_option_start_time;
    /// <value>只写(选填), 拉取的建议数量,server可根据需要返回或多或少,不能作为完成与否的标志 (Write (Optional), group pendency maximum limit)</value>
    public uint? group_pendency_option_max_limited;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class GroupHandlePendencyParam : ExtraData
  {
    /// <value>只写(选填), true表示接受，false表示拒绝。默认为false ()</value>
    public bool? group_handle_pendency_param_is_accept;
    /// <value>只写(选填), 同意或拒绝信息,默认为空字符串 ()</value>
    public string group_handle_pendency_param_handle_msg;
    /// <value>只写(必填), 未决信息详情 ()</value>
    public GroupPendency group_handle_pendency_param_pendency;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class GroupPendency : ExtraData
  {
    /// <value>读写, 群组ID (Read & Write, group ID)</value>
    public string group_pendency_group_id;
    /// <value>读写, 请求者的ID,例如：请求加群:请求者,邀请加群:邀请人 (Read & Write, applicant's user ID)</value>
    public string group_pendency_form_identifier;
    /// <value>读写, 判决者的 ID，处理此条“加群的未决请求”的管理员ID (Read & Write, operator's user ID, handler of the request)</value>
    public string group_pendency_to_identifier;
    /// <value>只读, 未决信息添加时间 (Read & Write, pendency requested time)</value>
    public ulong group_pendency_add_time;
    /// <value>只读, 未决请求类型 (Read only, pendency type)</value>
    public TIMGroupPendencyType group_pendency_pendency_type;
    /// <value>只读, 群未决处理状态 (Read only, pendency handle)</value>
    public TIMGroupPendencyHandle group_pendency_handled;
    /// <value>只读, 群未决处理操作类型 (Read only, pendency handle result)</value>
    public TIMGroupPendencyHandleResult group_pendency_handle_result;
    /// <value>只读, 申请或邀请附加信息 (Read only, pendency apply or invitation additional message)</value>
    public string group_pendency_apply_invite_msg;
    /// <value>只读, 申请或邀请者自定义字段 (Read only, operant's custom data)</value>
    public string group_pendency_form_user_defined_data;
    /// <value>只读, 审批信息：同意或拒绝信息 (Read only, approval message, granted or rejected message)</value>
    public string group_pendency_approval_msg;
    /// <value>只读, 审批者自定义字段 (Read only, operator's custom data)</value>
    public string group_pendency_to_user_defined_data;
    /// <value>只读, 签名信息，客户不用关心 (Read only, pendency key)</value>
    public string group_pendency_key;
    /// <value>只读, 签名信息，客户不用关心 (Read only, pendency authentication)</value>
    public string group_pendency_authentication;
    /// <value>只读, 自己的ID (Read only, self user ID)</value>
    public string group_pendency_self_identifier;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class GroupSearchParam : ExtraData
  {
    /// <value>搜索关键字列表，最多支持5个，如果是本地搜索，您需主动设置 keyword 是否匹配群 ID、群名称；如果是云端搜索，keyword 会自动匹配群 ID、群名称(searching keyword list, maximum 5, if local search, you need to set keyword whether to match group ID, group name; if cloud search, keyword will automatically match group ID, group name)</value>
    public List<string> group_search_params_keyword_list;
    /// <value>搜索域列表(仅本地搜索有效)(Search field list (only valid for local search))</value>
    public List<TIMGroupSearchFieldKey>? group_search_params_field_list;
    /// <value>指定关键字列表匹配类型，可设置为“或”关系搜索或者“与”关系搜索（仅云端搜索有效）(Specify keyword list matching type, can be set to "or" relationship search or "and" relationship search (only valid for cloud search))</value>
    public TIMKeywordListMatchType? group_search_params_keyword_list_match_type;
    /// <value>每次云端搜索返回结果的条数（必须大于 0，最大支持 100，仅云端搜索有效）(Number of results returned by each cloud search, must be greater than 0, maximum 100, only valid for cloud search)</value>
    public uint group_search_params_search_count;
    /// <value>每次云端搜索的起始位置。第一次填空字符串，续拉时填写 GroupSearchResult 中的返回值（仅云端搜索有效）(Start position of each cloud search. Fill in an empty string the first time, and fill in the return value in GroupSearchResult when pulling again (only valid for cloud search))</value>
    public string? group_search_params_search_cursor;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class GroupSearchResult : ExtraData
  {
    /// <value>满足搜索条件的群列表是否已经全部返回(Is finished)</value>
    public bool group_search_result_is_finished;
    /// <value>满足搜索条件的群总数量(Total count of groups that meet the search conditions)</value>
    public uint group_search_result_total_count;
    /// <value>下一次云端搜索的起始位置(Next cursor for next search)</value>
    public string group_search_result_next_cursor;
    /// <value>满足搜索条件的群列表(List of groups that meet the search conditions)</value>
    public List<GroupDetailInfo> group_search_result_group_list;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class GroupMemberSearchParam : ExtraData
  {
    /// <value>指定群 ID 列表，若为不填则搜索全部群中的群成员 (Search group member's user ID list, null means all the member)</value>
    public List<string> group_search_member_params_groupid_list;
    /// <value>搜索关键字列表，最多支持5个，如果是本地搜索，您需主动设置 keyword 是否匹配群成员 ID、昵称、备注、群名片。如果是云端搜索，keyword 会自动匹配群成员 ID、昵称、群名片(searching keyword list, maximum 5, if local search, you need to set keyword whether to match group member ID, nickname, remark, group card; if cloud search, keyword will automatically match group member ID, nickname, group card)</value>
    public List<string> group_search_member_params_keyword_list;
    /// <value>搜索域列表(仅本地搜索有效)(Search field list (only valid for local search))</value>
    public List<TIMGroupMemberSearchFieldKey>? group_search_member_params_field_list;
    /// <value>指定关键字列表匹配类型，可设置为“或”关系搜索或者“与”关系搜索（仅云端搜索有效）(Specify keyword list matching type, can be set to "or" relationship search or "and" relationship search (only valid for cloud search))</value>
    public TIMKeywordListMatchType? group_member_search_params_keyword_list_match_type;
    /// <value>每次云端搜索返回结果的条数（必须大于 0，最大支持 100，仅云端搜索有效）(Number of results returned by each cloud search, must be greater than 0, maximum 100, only valid for cloud search)</value>
    public uint group_member_search_params_search_count;
    /// <value>每次云端搜索的起始位置。第一次填空字符串，续拉时填写 GroupSearchResult 中的返回值（仅云端搜索有效）(Start position of each cloud search. Fill in an empty string the first time, and fill in the return value in GroupSearchResult when pulling again (only valid for cloud search))</value>
    public string group_member_search_params_search_cursor;
  }

  [JsonObject(MemberSerialization.OptOut)]
  /// 本地群成员搜索结果
  public class GroupSearchGroupMembersResult : ExtraData
  {
    /// <value>只读, 群 id (Read only, group id)</value>
    public string group_search_member_result_groupid;
    /// <value>只读, 群成员的列表 (Read only, group member list)</value>
    public List<GroupMemberInfo> group_search_member_result_member_info_list;
  }

  [JsonObject(MemberSerialization.OptOut)]
  // 云端群成员搜索结果
  public class GroupMemberSearchResult : ExtraData
  {
    /// <value>只读, 满足搜索条件的群成员列表是否已经全部返回(Is finished)</value>
    public bool group_member_search_result_is_finished;
    /// <value>只读, 满足搜索条件的群成员总数量(Total count of group m  embers that meet the search conditions)</value>
    public uint group_member_search_result_total_count;
    /// <value>只读, 下一次云端搜索的起始位置(Next cursor for next search)</value>
    public string group_member_search_result_next_cursor;
    /// <value>只读, 满足搜索条件的群成员列表(List of group members that meet the search conditions)</value>
    [JsonConverter(typeof(StringToList<GroupSearchGroupMembersResult>))]
    public List<GroupSearchGroupMembersResult> group_member_search_result_member_list;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class GroupAttributes : ExtraData
  {
    /// <value>群属性 map 的 key (Group attribute key)</value>
    public string group_attribute_key;
    /// <value>群属性 map 的 value (Group attribute value)</value>
    public string group_attribute_value;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class FriendShipGetProfileListParam : ExtraData
  {
    /// <value>只写, 想要获取目标用户资料的UserID列表 (Write only, target user ID list)</value>
    public List<string> friendship_getprofilelist_param_identifier_array;
    /// <value>只写, 是否强制更新。false表示优先从本地缓存获取，获取不到则去网络上拉取。true表示直接去网络上拉取资料。默认为false (Write only, is forcing update, default: false. False means search from local cache, true means retrieve online)</value>
    public bool friendship_getprofilelist_param_force_update;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class FriendshipAddFriendParam : ExtraData
  {
    /// <value>只写, 请求加好友对应的UserID (Write only, target friend's user ID)</value>
    public string friendship_add_friend_param_identifier;
    /// <value>只写, 请求添加好友的好友类型 (Write only, add friend type)</value>
    public TIMFriendType friendship_add_friend_param_friend_type;
    /// <value>只写, 预备注 (Write only, friend's remark)</value>
    public string friendship_add_friend_param_remark;
    /// <value>只写, 预分组名 (Write only, add friend to the group)</value>
    public string friendship_add_friend_param_group_name;
    /// <value>只写, 加好友来源描述 (Write only, friend's source )</value>
    public string friendship_add_friend_param_add_source;
    /// <value>只写, 加好友附言 (Write only, friend's adding word)</value>
    public string friendship_add_friend_param_add_wording;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class FriendResponse : ExtraData
  {
    /// <value>只写(必填), 响应好友添加的UserID (Write (Required), respondent's user ID)</value>
    public string friend_response_identifier;
    /// <value>只写(必填), 响应好友添加的动作 (Write (Required), response action)</value>
    public TIMFriendResponseAction friend_response_action;
    /// <value>只写(选填), 好友备注 (Write (Optional), friend's remark)</value>
    public string friend_response_remark;
    /// <value>只写(选填), 好友分组列表 (Write (Optional), friend's group name)</value>
    public string friend_response_group_name;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class FriendshipModifyFriendProfileParam : ExtraData
  {
    /// <value>只写, 被修改的好友的UserID (Write only, operant's user ID)</value>
    public string friendship_modify_friend_profile_param_identifier;
    /// <value>只写, 修改的好友资料各个选项 (Write only, modified params)</value>
    public FriendProfileItem friendship_modify_friend_profile_param_item;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class FriendProfileItem : UserProfileItem
  {
    /// <value>只写, 修改好友备注 (Write only, modified friend's remark)</value>
    public string friend_profile_item_remark;
    /// <value>只写, 修改好友分组名称列表 (Write only, modified group name list)</value>
    public List<string> friend_profile_item_group_name_array;
    /// <value>只写, 修改[自定义好友字段](https://cloud.tencent.com/document/product/269/1501#.E8.87.AA.E5.AE.9A.E4.B9.89.E5.A5.BD.E5.8F.8B.E5.AD.97.E6.AE.B5) (Write only, modified [Custom Friend Fields](https://www.tencentcloud.com/document/product/1047/33521))</value>
    public List<FriendProfileCustomStringInfo> friend_profile_item_custom_string_array;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class FriendProfileUpdate : ExtraData
  {
    /// <value>只写, 资料更新的好友的UserID (Write only, profile updated friend's userID)</value>
    public string friend_profile_update_identifier;
    /// <value>只写, FriendProfileItem, 资料更新的Item (Write only, updated friend's profile item)</value>
    public FriendProfileItem friend_profile_update_item;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class FriendProfileCustomStringInfo : ExtraData
  {
    /// <value>只写, 好友自定义资料字段key (Write only, custom friend field key)</value>
    public string friend_profile_custom_string_info_key;
    /// <value>只写, 好友自定义资料字段value (Write only, custom friend field value)</value>
    public string friend_profile_custom_string_info_value;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class FriendshipDeleteFriendParam : ExtraData
  {
    /// <value>只写, 删除好友，指定删除的好友类型 (Write only, deleted friend type)</value>
    public TIMFriendType friendship_delete_friend_param_friend_type;
    /// <value>只写(选填), 删除好友UserID列表 (Write (Optional), deleted friend's user ID list)</value>
    public List<string> friendship_delete_friend_param_identifier_array;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class FriendshipCheckFriendTypeParam : ExtraData
  {
    /// <value>只写, 要检测的好友类型 (Write only, checked friend type)</value>
    public TIMFriendType friendship_check_friendtype_param_check_type;
    /// <value>只写, 要检测的好友UserID列表 (Write only, checked friend's user ID list)</value>
    public List<string> friendship_check_friendtype_param_identifier_array;
  }
  [JsonObject(MemberSerialization.OptOut)]
  public class CreateFriendGroupInfo : ExtraData
  {
    /// <value>只写, 创建分组的名称列表 (Write only, friend's group name list)</value>
    public List<string> friendship_create_friend_group_param_name_array;
    /// <value>只写, 要放到创建的分组的好友UserID列表 (Write only, friend's group member user ID list)</value>
    public List<string> friendship_create_friend_group_param_identifier_array;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class FriendGroupInfo : ExtraData
  {
    /// <value>只读, 分组名称 (Read only, friend group name)</value>
    public string friend_group_info_name;
    /// <value>只读, 当前分组的好友个数 (Read only, current friend group member count)</value>
    public uint friend_group_info_count;
    /// <value>只读, 当前分组内好友UserID列表 (Read only, current friend group user ID list)</value>
    public List<string> friend_group_info_identifier_array;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class FriendshipModifyFriendGroupParam : ExtraData
  {
    /// <value>只写, 要修改的分组名称 (Write only, friend group's old name)</value>
    public string friendship_modify_friend_group_param_name;
    /// <value>只写(选填), 修改后的分组名称 (Write (Optional), friend group's new name)</value>
    public string friendship_modify_friend_group_param_new_name;
    /// <value>只写(选填), 要从当前分组删除的好友UserID列表 (Write (Optional), deleted friends' user ID list from the friend group)</value>
    public List<string> friendship_modify_friend_group_param_delete_identifier_array;
    /// <value>只写(选填), 当前分组要新增的好友UserID列表 (Write (Optional), added friends' user ID list to the friend group)</value>
    public List<string> friendship_modify_friend_group_param_add_identifier_array;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class FriendshipGetPendencyListParam : ExtraData
  {
    /// <value>只写, 添加好友的未决请求类型 (Write only, friend pendency list type)</value>
    public TIMFriendPendencyType friendship_get_pendency_list_param_type;
    /// <value>只写, 分页获取未决请求的起始 seq，返回的结果包含最大 seq，作为获取下一页的起始 seq (Write only, friend pendency list start sequence)</value>
    public ulong friendship_get_pendency_list_param_start_seq;
    /// <value>只写, 获取未决信息的开始时间戳 (Write only, friend pendency list start time)</value>
    public ulong friendship_get_pendency_list_param_start_time;
    /// <value>只写, 获取未决信息列表，每页的数量 (Write only, friend pendency list's page size for each searching)</value>
    public int friendship_get_pendency_list_param_limited_size;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class FriendshipDeletePendencyParam : ExtraData
  {
    /// <value>只读, 添加好友的未决请求类型 (Read only, deleted friend pendency type)</value>
    public TIMFriendPendencyType friendship_delete_pendency_param_type;
    /// <value>只读, 删除好友未决请求的UserID列表 (Read only, deleted friend pendency list's friends' user ID list)</value>
    public List<string> friendship_delete_pendency_param_identifier_array;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class FriendSearchParam : ExtraData
  {
    /// <value>只写, 搜索的关键字列表，关键字列表最多支持 5 个 (Write only, searching keyword list, maximum 5)</value>
    public List<string> friendship_search_param_keyword_list;
    /// <value>只写 [TIMFriendshipSearchFieldKey]() 好友搜索类型 (Write only, friend searching type)</value>
    public List<TIMFriendshipSearchFieldKey> friendship_search_param_search_field_list;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class MessageReceipt : ExtraData
  {
    /// <value>只读, 会话ID (Read only, conversation ID)</value>
    public string msg_receipt_conv_id;
    /// <value>只读, 会话类型 (Read only, conversation type)</value>
    public TIMConvType msg_receipt_conv_type;
    /// <value>只读, 时间戳 (Read only, receipt timestamp)</value>
    public ulong msg_receipt_time_stamp;
    /// <value>string, 只读, 群消息 ID (Read only, receipt message ID)</value>
    public string msg_receipt_msg_id;
    /// <value>bool, C2C 对端消息是否已读 (is C2C peer end read)</value>
    public bool msg_receipt_is_peer_read;
    /// <value>uint64, 只读, 群消息已读人数 (Read only, message receipt read count)</value>
    public int msg_receipt_read_count;
    /// <value>uint64, 只读, 群消息未读人数 (Read only, message receipt unread count)</value>
    public int msg_receipt_unread_count;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class GroupTipsElem : ExtraData
  {
    /// <value>只读, 群消息类型 (Read only, group tip type)</value>
    public TIMGroupTipType group_tips_elem_tip_type; // 群组系统消息元素
    /// <value>只读, 操作者ID (Read only, operant user ID)</value>
    public string group_tips_elem_op_user; // 群组系统消息元素
    /// <value>只读, 群组名称 (Read only, group name)</value>
    public string group_tips_elem_group_name; // 群组系统消息元素
    /// <value>只读, 群组ID (Read only, group ID)</value>
    public string group_tips_elem_group_id; // 群组系统消息元素
    /// <value>只读, 群消息时间，废弃 (Read only, elem time (deprecated))</value>
    public uint group_tips_elem_time; // 群组系统消息元素
    /// <value>只读, 被操作的帐号列表 (Read only, operant user ID list)</value>
    public List<string> group_tips_elem_user_array; // 群组系统消息元素
    /// <value>只读, 群资料变更信息列表,仅当 tips_type 值为 kTIMGroupTip_GroupInfoChange 时有效 (Read only, changed group info list, works only when tips_type is kTIMGroupTip_GroupInfoChange)</value>
    public List<GroupTipGroupChangeInfo> group_tips_elem_group_change_info_array; // 群组系统消息元素
    /// <value>只读, 群成员变更信息列表,仅当 tips_type 值为 kTIMGroupTip_MemberInfoChange 时有效 (Read only, changed member info list, works only when tips_type is kTIMGroupTip_MemberInfoChange)</value>
    public List<GroupTipMemberChangeInfo> group_tips_elem_member_change_info_array; // 群组系统消息元素
    /// <value>只读, 操作者个人资料 (Read only, operator's user info)</value>
    public UserProfile group_tips_elem_op_user_info; // 群组系统消息元素
    /// <value>只读, 群成员信息 (Read only, group member info)</value>
    public GroupMemberInfo group_tips_elem_op_group_memberinfo; // 群组系统消息元素
    /// <value>只读, 被操作者列表资料 (Read only, operant's user info list)</value>
    public List<UserProfile> group_tips_elem_changed_user_info_array; // 群组系统消息元素
    /// <value>只读, 群成员信息列表 (Read only, group member info list)</value>
    public List<GroupMemberInfo> group_tips_elem_changed_group_memberinfo_array; // 群组系统消息元素
    /// <value>只读, 当前群成员数,只有当事件消息类型为 kTIMGroupTip_Invite 、 kTIMGroupTip_Quit 、 kTIMGroupTip_Kick 时有效 (Read only, current group member count, works when tips_type is kTIMGroupTip_Invite, kTIMGroupTip_Quit or kTIMGroupTip_Kick)</value>
    public uint group_tips_elem_member_num; // 群组系统消息元素
    /// <value>只读, 操作方平台信息 (Read only, operator's platform)</value>
    public string group_tips_elem_platform; // 群组系统消息元素
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class ConvInfo : ExtraData
  {
    /// <value>只读, 会话ID (Read only, conversation ID)</value>
    public string conv_id; // 群组系统消息元素
    /// <value>只读, 会话类型 (Read only, conversation type)</value>
    public TIMConvType conv_type; // 群组系统消息元素
    /// <value>只读, 会话所有者，废弃 (Read only, conversation owner (deprecated))</value>
    public string conv_owner; // 群组系统消息元素
    /// <value>只读, 会话未读计数 (Read only, conversation unread number)</value>
    public ulong conv_unread_num; // 群组系统消息元素
    /// <value>只读, 会话的激活时间 (Read only, conversation active time)</value>
    public ulong? conv_active_time; // 群组系统消息元素
    /// <value>只读, 会话是否有最后一条消息 (Read only, is conversation has last message)</value>
    public bool conv_is_has_lastmsg; // 群组系统消息元素
    /// <value>只读, 会话最后一条消息 (Read only, conversation's last message)</value>
    public Message conv_last_msg; // 群组系统消息元素
    /// <value>只读, 会话是否有草稿 (Read only, is conversation has draft)</value>
    public bool conv_is_has_draft; // 群组系统消息元素
    /// <value>只读(选填), 会话草稿 (Read only, conversation draft)</value>
    public Draft conv_draft; // 群组系统消息元素
    /// <value>只读(选填), 消息接收选项 (Read only, conversation receiving option)</value>
    public TIMReceiveMessageOpt conv_recv_opt; // 群组系统消息元素
    /// <value>只读(选填),群会话 @ 信息列表，用于展示 “有人@我” 或 “@所有人” 这两种提醒状态 (Read only, group conversation's at message info list, for displaying "someone @ME" or "@ALL")</value>
    public List<GroupAtInfo> conv_group_at_info_array; // 群组系统消息元素
    /// <value>只读,是否置顶 (Read only, is conversation pinned)</value>
    public bool conv_is_pinned; // 群组系统消息元素
    /// <value>只读, 获取会话展示名称，其展示优先级如下：1、群组，群名称 -> 群 ID;C2C; 2、对方好友备注 -> 对方昵称 -> 对方的 userID (Read only, conversation show name, the display priority: 1. group conversation: group name -> group ID. 2. C2C conversation: peer's remark -> peer's nickname -> peer's user ID)</value>
    public string conv_show_name; // 群组系统消息元素
    /// <value>只读, 会话标记列表，取值详见 @TIMConversationMarkType（从 6.5 版本开始支持）(Read only, conversation's mark type list)</value>
    public List<ulong> conv_mark_array;
    /// <value>只读, 会话自定义数据（从 6.5 版本开始支持）</value>
    public string conv_custom_data;
    /// <value>只读, 会话所属分组列表（从 6.5 版本开始支持）</value>
    public List<string> conv_conversation_group_array;
    /// <value>只读, 最新已读消息的 UTC 时间戳，仅对单聊会话生效（从 7.1 版本开始支持）</value>
    public ulong conv_c2c_read_timestamp;
    /// <value>只读, 群消息已读 Sequence（从 7.1 版本开始支持）</value>
    public ulong conv_group_read_sequence;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class Draft : ExtraData
  {
    /// <value>只读, 草稿内的消息 (Read only, draft message)</value>
    public Message draft_msg; // 群组系统消息元素
    /// <value>只读, 用户自定义数据 (Read only, user defined draft data)</value>
    public string draft_user_define; // 群组系统消息元素
    /// <value>只读, 草稿最新编辑时间 (Read only, draft edit time)</value>
    public uint draft_edit_time; // 群组系统消息元素
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class GroupAtInfo : ExtraData
  {
    /// <value>只读, @ 消息序列号，即带有 “@我” 或者 “@所有人” 标记的消息的序列号 (Read only, @ message sequence number)</value>
    public ulong conv_group_at_info_seq; // 群组系统消息元素
    /// <value>只读, @ 提醒类型，分成 “@我” 、“@所有人” 以及 “@我并@所有人” 三类 (Read only, @ message type: "@ME", "@ALL" and "@ME AND @ALL")</value>
    public TIMGroupAtType conv_group_at_info_at_type; // 群组系统消息元素
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class FriendAddPendency : ExtraData
  {
    /// <value>只读, 添加好友请求方的UserID (Read only, friend requester's user ID)</value>
    public string friend_add_pendency_identifier;
    /// <value>只读, 添加好友请求方的昵称 (Read only, friend requester's nick name)</value>
    public string friend_add_pendency_nick_name;
    /// <value>只读, 添加好友请求方的来源 (Read only, friend requester's source)</value>
    public string friend_add_pendency_add_source;
    /// <value>只读, 添加好友请求方的附言 (Read only, friend requester's appending words)</value>
    public string friend_add_pendency_add_wording;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class FriendProfile : ExtraData
  {
    /// <value>只读, 好友UserID (Read only, friend's user ID)</value>
    public string friend_profile_identifier;
    /// <value>只读, 好友分组名称列表 (Read only, friend group's name list)</value>
    public List<string> friend_profile_group_name_array;
    /// <value>只读, 好友备注，最大96字节，获取自己资料时，该字段为空 (Read only, friend's remark, maximum 96 Bytes, self profile's remark is null)</value>
    public string friend_profile_remark;
    /// <value>只读, 好友申请时的添加理由 (Read only, friend request's reason)</value>
    public string friend_profile_add_wording;
    /// <value>只读, 好友申请时的添加来源 (Read only, friend request's source)</value>
    public string friend_profile_add_source;
    /// <value>只读, 好友添加时间 (Read only, friend request's time)</value>
    public long friend_profile_add_time;
    /// <value>只读, 好友的个人资料 (Read only, friend's user profile)</value>
    public UserProfile friend_profile_user_profile;
    /// <value>只读, [自定义好友字段](https://cloud.tencent.com/document/product/269/1501#.E8.87.AA.E5.AE.9A.E4.B9.89.E5.A5.BD.E5.8F.8B.E5.AD.97.E6.AE.B5) (Read only, [Custom Friend Fields](https://www.tencentcloud.com/document/product/1047/33521))</value>
    public List<FriendProfileCustomStringInfo> friend_profile_custom_string_array;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class SetConfig : ExtraData
  {
    /// <value>只写(选填), 输出到日志文件的日志级别 (Write only (Optional), set config log level)</value>
    public TIMLogLevel? set_config_log_level;
    /// <value>只写(选填), 日志回调的日志级 (Write only (Optional), set config callback log level)</value>
    public TIMLogLevel? set_config_callback_log_level;
    /// <value>只写(选填), 是否输出到控制台，默认为 true (Write only (Optional), is log output to console)</value>
    public bool? set_config_is_log_output_console;
    /// <value>只写(选填), 用户配置 (Write only (Optional), user config)</value>
    public UserConfig set_config_user_config;
    /// <value>只写(选填), 自定义数据，如果需要，初始化前设置 (Write only (Optional), user defined data, if needed, preset before init the SDK)</value>
    public string set_config_user_define_data;
    /// <value>只写(选填), 设置HTTP代理，如果需要，在发送图片、文件、语音、视频前设置 (Write only (Optional), set http proxy info, if needed, preset before sending image, file, audio, video messages)</value>
    public HttpProxyInfo set_config_http_proxy_info;
    /// <value>只写(选填), 设置SOCKS5代理，如果需要，初始化前设置 (Write only (Optional), set socks5 proxy info, if needed, preset before init the SDK)</value>
    public Socks5ProxyInfo set_config_socks5_proxy_info;
    /// <value>只写(选填), 如果为true，SDK内部会在选择最优IP时只使用LocalDNS (Write only (Optional), is only using local DNS source for the preferred IP address)</value>
    public bool? set_config_is_only_local_dns_source;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class HttpProxyInfo : ExtraData
  {
    /// <value>只写(必填), 代理的IP (Write only (Required), http proxy ip)</value>
    public string http_proxy_info_ip;
    /// <value>只写(必填), 代理的端口 (Write only (Required), http proxy port)</value>
    public int http_proxy_info_port;
    /// <value>只写(选填), 认证的用户名 (Write only (Optional), http proxy username)</value>
    public string http_proxy_info_username;
    /// <value>只写(选填), 认证的密码 (Write only (Optional), http proxy password)</value>
    public string http_proxy_info_password;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class Socks5ProxyInfo : ExtraData
  {
    /// <value>只写(必填), socks5代理的IP (Write only (Required), socks5 proxy ip)</value>
    public string socks5_proxy_info_ip;
    /// <value>只写(必填), socks5代理的端口 (Write only (Required), socks5 proxy port)</value>
    public int socks5_proxy_info_port;
    /// <value>只写(选填), 认证的用户名 (Write only (Optional), socks5 proxy username)</value>
    public string socks5_proxy_info_username;
    /// <value>只写(选填), 认证的密码 (Write only (Optional), socks5 proxy password)</value>
    public string socks5_proxy_info_password;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class UserConfig : ExtraData
  {
    /// <value>只写(选填), true表示要收已读回执事件 (Write only (Optional), is read receipt acceptable)</value>
    public bool? user_config_is_read_receipt;
    /// <value>只写(选填), true表示服务端要删掉已读状态 (Write only (Optional), is sync report (should server end delete the read status))</value>
    public bool? user_config_is_sync_report;
    /// <value>只写(选填), true表示群tips不计入群消息已读计数 (Write only (Optional), is ingoring group tips messages for unread count)</value>
    public bool? user_config_is_ignore_grouptips_unread;
    /// <value>只写(选填), 是否禁用本地数据库，true表示禁用，false表示不禁用。默认是false (Write only (Optional), is disabling local storage, default: false)</value>
    public bool? user_config_is_is_disable_storage;
    /// <value>只写(选填),获取群组信息默认选项 (Write only (Optional), get group info option)</value>
    public GroupGetInfoOption user_config_group_getinfo_option;
    /// <value>只写(选填),获取群组成员信息默认选项 (Write only (Optional), get group member info option)</value>
    public GroupMemberGetInfoOption user_config_group_member_getinfo_option;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class GroupGetInfoOption : ExtraData
  {
    /// <value>读写(选填), 根据想要获取的信息过滤，默认值为0xffffffff(获取全部信息) (Read & Write (Optional), filter flag, default: 0xffffffff (accuring all info))</value>
    public TIMGroupGetInfoFlag? group_get_info_option_info_flag;
    /// <value>只写(选填), 请参考[自定义字段](https://cloud.tencent.com/document/product/269/1502#.E8.87.AA.E5.AE.9A.E4.B9.89.E5.AD.97.E6.AE.B5) (Write only (Optional), check [Custom Group Fields](https://www.tencentcloud.com/document/product/1047/33529))</value>
    public List<string> group_get_info_option_custom_array;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class SSODataParam : ExtraData
  {
    /// <value>sso请求的命令字 (SSO cmd data)</value>
    public string sso_data_param_cmd;
    /// <value>sso请求的内容，内容是二进制，需要外部传入base64编码后的字符串，sdk内部回解码成原二进制 (SSO data body in binary. Input external base64 format string, and the SDK will decode it to the binary)</value>
    public string sso_data_param_body;
    /// <value>sso请求的超时时间，默认是15秒 (SSO timeout time, default: 15 seconds)</value>
    public ulong sso_data_param_timeout;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class ServerAddress : ExtraData
  {
    /// <value>只写(必填), 服务器 IP (Write only (Required), server address ip)</value>
    public string server_address_ip;
    /// <value>只写(必填), 服务器端口 (Write only (Required), server address port)</value>
    public int server_address_port;
    /// <value>只写(选填), 是否 IPv6 地址，默认为 false (Write only (Optional), is ipv6)</value>
    public bool server_address_is_ipv6;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class CustomServerInfo : ExtraData
  {
    /// <value>只写(必填), 长连接服务器地址列表 (Write only (Required), long connection address list)</value>
    public List<ServerAddress> longconnection_address_array;
    /// <value>只写(选填), 短连接服务器地址列表 (Write only (Optional), short connection address list)</value>
    public List<ServerAddress> shortconnection_address_array;
    /// <value>只写(必填), 服务器公钥 (Write only (Required), server public key)</value>
    public string server_public_key;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class SM4GCMCallbackParam : ExtraData
  {
    /// <value>只写(必填), SM4 GCM 加密回调函数地址 (Write only (Required), SM4 GCM encrypted callback)</value>
    public ulong sm4_gcm_callback_param_encrypt;
    /// <value>只写(必填), SM4 GCM 解密回调函数地址 (Write only (Required), SM4 GCM decrypted callback)</value>
    public ulong sm4_gcm_callback_param_decrypt;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class CosSaveRegionForConversationParam : ExtraData
  {
    /// <value>只写(必填), 会话 ID (Write only (Required), conversation ID)</value>
    public string cos_save_region_for_conversation_param_conversation_id;
    /// <value>只写(必填), 存储区域 (Write only (Required), COS save region)</value>
    public string cos_save_region_for_conversation_param_cos_save_region;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class ExperimentalAPIRequestParam : ExtraData
  {
    /// <value>只写(必填), 内部接口的操作类型 (Write only (Required), internal operation)</value>
    public string request_internal_operation; // TIMInternalOperation
    /// <value>只写(选填), sso发包请求, 当 kTIMRequestInternalOperation 为 kTIMInternalOperationSSOData 时需要设置 (Write only (Optional), SSO data package, required when kTIMRequestInternalOperation sets to kTIMInternalOperationSSOData)</value>
    public SSODataParam request_sso_data_param;
    /// <value>只写(选填), 请求需要转换成tinyid的userid列表, 当 kTIMRequestInternalOperation 为 kTIMInternalOperationUserId2TinyId 时需要设置 (Write only (Optional), request user ID list for converting to tiny IDs, required when kTIMRequestInternalOperation sets to kTIMInternalOperationUserId2TinyId)</value>
    public List<string> request_userid_tinyid_param;
    /// <value>只写(选填), 请求需要转换成userid的tinyid列表, 当 kTIMRequestInternalOperation 为 kTIMInternalOperationTinyId2UserId 时需要设置 (Write only (Optional), request tiny ID list for converting to user IDs, required when kTIMRequestInternalOperation sets to kTIMInternalOperationTinyId2UserId)</value>
    public List<ulong> request_tinyid_userid_param;
    /// <value>只写(选填), true 表示设置当前环境为测试环境，false表示设置当前环境是正式环境，默认是正式环境, 当 kTIMRequestInternalOperation 为 kTIMInternalOperationSetEnv 时需要设置 (Write only (Optional), is test environment, default: false, required when kTIMRequestInternalOperation sets to kTIMInternalOperationSetEnv)</value>
    public bool? request_set_env_param;
    /// <value>只写(选填), 设置登录、发消息的重试次数, 当 kTIMRequestInternalOperation 为 kTIMInternalOperationSetMaxRetryCount 时需要设置 (Write only (Optional), maximum login/send message times, required when kTIMRequestInternalOperation sets to kTIMInternalOperationSetMaxRetryCount)</value>
    public int? request_set_max_retry_count_param;
    /// <value>只写(选填), 自定义服务器信息, 当 kTIMRequestInternalOperation 为 kTIMInternalOperationSetCustomServerInfo 时需要设置 (Write only (Optional), custom server info, required when kTIMRequestInternalOperation sets to kTIMInternalOperationSetCustomServerInfo)</value>
    public CustomServerInfo request_set_custom_server_info_param;
    /// <value>只写(选填), 国密 SM4 GCM 回调函数地址的参数, 当 kTIMRequestInternalOperation 为 kTIMInternalOperationSetSM4GCMCallback 时需要设置 (Write only (Optional), SM4 GCM callback param, required when kTIMRequestInternalOperation sets to kTIMInternalOperationSetSM4GCMCallback)</value>
    public SM4GCMCallbackParam request_set_sm4_gcm_callback_param;
    /// <value>只写(选填), 初始化 Database 的用户 ID, 当 kTIMRequestInternalOperation 为 kTIMInternalOperationInitLocalStorage 时需要设置 (Write only (Optional), init local storage's user ID, required when kTIMRequestInternalOperation sets to kTIMInternalOperationInitLocalStorage)</value>
    public string request_init_local_storage_user_id_param;
    /// <value>只写(选填), 设置 cos 存储区域的参数, 当 kTIMRequestInternalOperation 为 kTIMInternalOperationSetCosSaveRegionForConversation 时需要设置 (Write only (Optional), set COS save region param, required when kTIMRequestInternalOperation sets to kTIMInternalOperationSetCosSaveRegionForConversation)</value>
    public CosSaveRegionForConversationParam request_set_cos_save_region_for_conversation_param;
    /// <value>只写(选填), 设置 UI 平台，当 kTIMRequestInternalOperation 为 kTIMInternalOperationSetUIPlatform 时需要设置 (Write only (Optional), set ui platform, required when kTIMRequestInternalOperation sets to kTIMInternalOperationSetUIPlatform)</value>
    public int? request_set_ui_platform_param;
    /// <value>int, 只写(选填), 打印日志的级别, 当 kTIMRequestInternalOperation 为 kTIMInternalOperationWriteLog 时需要设置 (Write only (Optional), set log level, used when kTIMRequestInternalOperation sets to kTIMInternalOperationWriteLog)</value>
    public int? request_write_log_log_level_param;
    /// <value>string, 只写(选填), 打印日志的内容, 当 kTIMRequestInternalOperation 为 kTIMInternalOperationWriteLog 时需要设置 (Write only (Optional), set log content, used when kTIMRequestInternalOperation sets to kTIMInternalOperationWriteLog)</value>
    public string request_write_log_log_content_param;
    /// <value>string, 只写(选填), 打印日志的文件名, 当 kTIMRequestInternalOperation 为 kTIMInternalOperationWriteLog 时需要设置 (Write only (Optional), set log file name, used when kTIMRequestInternalOperation sets to kTIMInternalOperationWriteLog)</value>
    public string request_write_log_file_name_param;
    /// <value>string, 只写(选填), 打印日志的函数名, 当 kTIMRequestInternalOperation 为 kTIMInternalOperationWriteLog 时需要设置 (Write only (Optional), set log function name, used when kTIMRequestInternalOperation sets to kTIMInternalOperationWriteLog)</value>
    public string request_write_log_func_name_param;
    /// <value>int, 只写(选填), 打印日志的所在行数, 当 kTIMRequestInternalOperation 为 kTIMInternalOperationWriteLog 时需要设置 (Write only (Optional), set log line number, used when kTIMRequestInternalOperation sets to kTIMInternalOperationWriteLog)</value>
    public int? request_write_log_line_number_param;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class GetTotalUnreadNumberResult : ExtraData
  {
    /// <value>int, 只读，会话未读数 (Read only, conversation unread count)</value>
    public int conv_get_total_unread_message_count_result_unread_count;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class GetC2CRecvMsgOptResult : ExtraData
  {
    /// <value>string, 只读，用户ID (Read only, user ID)</value>
    public string msg_recv_msg_opt_result_identifier;
    /// <value>uint [TIMReceiveMessageOpt](), 只读，消息接收选项 (Read only, message receiving option)</value>
    public TIMReceiveMessageOpt msg_recv_msg_opt_result_opt;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class ReceiveMessageOptInfo : ExtraData
  {
    ///<value>只读，获取消息免打扰开始时间：小时 (read-only, get message do not disturb start time: hours)</value>
    public int msg_all_recv_msg_opt_start_hour;
    ///<value>只读，获取消息免打扰开始时间：分钟 (Read only, get messages without disturbing start time: minutes)</value>
    public int msg_all_recv_msg_opt_start_minute;
    ///<value>只读，获取消息免打扰开始时间：秒 (Read only, get message do not disturb start time: seconds)</value>
    public int msg_all_recv_msg_opt_start_second;
    ///<value>只读，获取消息免打扰开始的 UTC 时间戳，如果返回的 startTimeStamp 大于 0，您可以直接使用；如果返回的 startTimeStamp 等于 0，您需要调用 getStartHour()、getStartMinute()、getStartSecond() 来获取免打扰的相对开始时间 (Read-only, get the UTC timestamp of message DND start. If the returned startTimeStamp is greater than 0, you can use it directly; if the returned startTimeStamp is equal to 0, you need to call getStartHour(), getStartMinute(), getStartSecond() to get DND relative start time of)</value>
    public int msg_all_recv_msg_opt_start_time_stamp;
    ///<value>只读，获取免打扰持续时长，单位：秒 (Read only, get the duration of DND, unit: seconds)</value>
    public int msg_all_recv_msg_duration;
    ///<value>[TIMReceiveMessageOpt](), 只读，消息接收选项 (Read only, message receiving option)</value>
    public TIMReceiveMessageOpt msg_all_recv_msg_opt_level;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class CreateGroupResult : ExtraData
  {
    /// <value>string, 只读, 创建的群ID (Read only, created group ID)</value>
    public string create_group_result_groupid;
  }
  [JsonObject(MemberSerialization.OptOut)]
  public class GroupInviteMemberResult : ExtraData
  {
    /// <value>string, 只读, 被邀请加入群组的用户ID (Read only, invited member user ID)</value>
    public string group_invite_member_result_identifier;
    /// <value>uint [HandleGroupMemberResult](), 只读, 邀请结果 (Read only, invitation result)</value>
    public HandleGroupMemberResult group_invite_member_result_result;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class GroupDeleteMemberResult : ExtraData
  {
    /// <value>string, 只读, 删除的成员ID (Read only, deleted user ID)</value>
    public string group_delete_member_result_identifier;
    /// <value>uint [HandleGroupMemberResult](), 只读, 删除结果 (Read only, deletion result)</value>
    public HandleGroupMemberResult group_delete_member_result_result;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class GroupBaseInfo : ExtraData
  {
    /// <value>string, 只读, 群组ID (Read only, group ID)</value>
    public string group_base_info_group_id;
    /// <value>string, 只读, 群组名称 (Read only, group name)</value>
    public string group_base_info_group_name;
    /// <value>uint [TIMGroupType](), 只读, 群组类型 (Read only, group type)</value>
    public TIMGroupType group_base_info_group_type;
    /// <value>string, 只读, 群组头像URL (Read only, group avatar URL)</value>
    public string group_base_info_face_url;
    /// <value>uint, 只读, 群资料的Seq，群资料的每次变更都会增加这个字段的值 (Read only, group info sequence, every modification of the group will change this seq)</value>
    public uint group_base_info_info_seq;
    /// <value>uint, 只读, 群最新消息的Seq。群组内每一条消息都有一条唯一的消息Seq，且该Seq是按照发消息顺序而连续的。从1开始，群内每增加一条消息，LastestSeq就会增加1 (Read only, group message's latest seq. Every group maintains its own sequencial message seq number. Start from 1, every message occurs an augmentation of the seq number)</value>
    public uint group_base_info_latest_seq;
    /// <value>uint, 只读, 用户所在群已读的消息Seq (Read only, group message read seq)</value>
    public uint group_base_info_readed_seq;
    /// <value>uint, 只读, 消息接收选项 (Read only, group message receiving flag)</value>
    public uint group_base_info_msg_flag;
    /// <value>bool, 只读, 当前群组是否设置了全员禁言 (Read only, is current group set mute all)</value>
    public bool group_base_info_is_shutup_all;
    /// <value>object [GroupSelfInfo](), 只读, 用户所在群的个人信息 (Read only, user self info in that group)</value>
    public GroupSelfInfo group_base_info_self_info;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class GroupSelfInfo : ExtraData
  {
    /// <value>uint, 只读, 加入群组时间 (Read only, group join time)</value>
    public uint group_self_info_join_time;
    /// <value>uint, 只读, 用户在群组中的角色 (Read only, self role in the group)</value>
    public uint group_self_info_role;
    /// <value>uint, 只读, 消息未读计数 (Read only, message unread count)</value>
    public uint group_self_info_unread_num;
    /// <value>uint [TIMReceiveMessageOpt](), 只读, 消息接收选项 (Read only, message receiving option)</value>
    public TIMReceiveMessageOpt group_self_info_msg_flag;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class GetGroupInfoResult : ExtraData
  {
    /// <value>int [错误码](https:///cloud.tencent.com/document/product/269/1671), 只读, 获取群组详细信息的结果 (Read only, group info result code [Error Codes](https://www.tencentcloud.com/document/product/1047/34348))</value>
    public int get_groups_info_result_code;
    /// <value>string, 只读, 获取群组详细失败的描述信息 (Read only, group info result description)</value>
    public string get_groups_info_result_desc;
    /// <value>object [GroupDetailInfo](), 只读, 群组详细信息 (Read only, group info details)</value>
    public GroupDetailInfo get_groups_info_result_info;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class GroupDetailInfo : ExtraData
  {
    /// <value>string, 只读, 群组ID (Read only, group ID)</value>
    public string group_detail_info_group_id;
    /// <value>uint [TIMGroupType](), 只读, 群组类型 (Read only, group type)</value>
    public TIMGroupType group_detail_info_group_type;
    /// <value>bool, 只读, 社群是否支持创建话题，只在群类型为 Community 时有效 (Read only, does support topic, works only when group_type is Community)</value>
    public bool? group_detail_info_is_support_topic;
    /// <value>string, 只读, 群组名称 (Read only, group name)</value>
    public string group_detail_info_group_name;
    /// <value>string, 只读, 群组公告 (Read only, group notification)</value>
    public string group_detail_info_notification;
    /// <value>string, 只读, 群组简介 (Read only, group introduction)</value>
    public string group_detail_info_introduction;
    /// <value>string, 只读, 群组头像URL (Read only, group avatar URL)</value>
    public string group_detail_info_face_url;
    /// <value>uint, 只读, 群组创建时间 (Read only, group created time)</value>
    public uint group_detail_info_create_time;
    /// <value>uint, 只读, 群资料的Seq，群资料的每次变更都会增加这个字段的值 (Read only, group info sequence number, every modification will augment the sequence number)</value>
    public uint group_detail_info_info_seq;
    /// <value>uint, 只读, 群组信息最后修改时间 (Read only, last modification time)</value>
    public uint group_detail_info_last_info_time;
    /// <value>uint, 只读, 群最新消息的Seq (Read only, latest group message sequence)</value>
    public uint group_detail_info_next_msg_seq;
    /// <value>uint, 只读, 最新群组消息时间 (Read only, latest message time)</value>
    public uint group_detail_info_last_msg_time;
    /// <value>uint, 只读, 群组当前成员数量 (Read only, group member count)</value>
    public uint group_detail_info_member_num;
    /// <value>uint, 只读, 群组最大成员数量 (Read only, maximum group member count)</value>
    public uint group_detail_info_max_member_num;
    /// <value>uint [TIMGroupAddOption](), 只读, 群组加群选项 (Read only, joining group option)</value>
    public TIMGroupAddOption group_detail_info_add_option;
    /// <value>uint, 只读, 群组在线成员数量 (Read only, online member count)</value>
    public uint group_detail_info_online_member_num;
    /// <value>uint, 只读, 群组成员是否对外可见 (Read only, is visible to outsiders)</value>
    public uint group_detail_info_visible;
    /// <value>uint, 只读, 群组是否能被搜索 (Read only, is searchable)</value>
    public uint group_detail_info_searchable;
    /// <value>bool, 只读, 群组是否被设置了全员禁言 (Read only, is set mute all)</value>
    public bool group_detail_info_is_shutup_all;
    /// <value>string, 只读, 群组所有者ID (Read only, group owner user ID)</value>
    public string group_detail_info_owner_identifier;
    /// <value>array [GroupInfoCustomString](), 只读, 请参考[自定义字段](https:///cloud.tencent.com/document/product/269/1502#.E8.87.AA.E5.AE.9A.E4.B9.89.E5.AD.97.E6.AE.B5) (Read only, [Custom Group Fields](https://www.tencentcloud.com/document/product/1047/33529))</value>
    public List<GroupInfoCustomString> group_detail_info_custom_info;
    /// <value>bool, 只读, 开启权限组功能，仅在社群生效，7.8 版本开始支持 (Read only, enable permission group, only works in community, 7.8 version and later)</value>
    public bool group_detail_info_enable_permission_group;
    /// <value>uint, 只读, 群组权限，仅在社群生效，7.8 版本开始支持 (Read only, group permissions, only works in community, 7.8 version and later)</value>
    public uint group_detail_info_default_permissions;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class GroupInfo : ExtraData
  {
    /// <value>string, 只读, 群组ID (Read only, group ID)</value>
    public string group_base_info_group_id;
    /// <value>string, 只读, 群组名称 (Read only, group name)</value>
    public string group_base_info_group_name;
    /// <value>uint [TIMGroupType](), 只读, 群组类型 (Read only, group type)</value>
    public TIMGroupType group_base_info_group_type;
    /// <value>string, 只读, 群组头像URL (Read only, group avatar URL)</value>
    public string group_base_info_face_url;
    /// <value>uint, 只读, 群资料的Seq，群资料的每次变更都会增加这个字段的值 (Read only,  group info sequence, every modification of the group will change this seq)</value>
    public uint group_base_info_info_seq;
    /// <value>uint, 只读, 群最新消息的Seq。群组内每一条消息都有一条唯一的消息Seq，且该Seq是按照发消息顺序而连续的。从1开始，群内每增加一条消息，LastestSeq就会增加1 (Read only, group message's latest seq. Every group maintains its own sequencial message seq number. Start from 1, every message occurs an augmentation of the seq number)</value>
    public uint group_base_info_latest_seq;
    /// <value>uint, 只读, 用户所在群已读的消息Seq (Read only, group message read seq)</value>
    public uint group_base_info_readed_seq;
    /// <value>uint, 只读, 消息接收选项 (Read only, group message receiving flag)</value>
    public uint group_base_info_msg_flag;
    /// <value>bool, 只读, 当前群组是否设置了全员禁言 (Read only, is current group set mute all)</value>
    public bool group_base_info_is_shutup_all;
    /// <value>object [GroupSelfInfo](), 只读, 用户所在群的个人信息 (Read only, user self info in that group)</value>
    public GroupSelfInfo group_base_info_self_info;
    /// <value>string, 只读, 群组ID (Read only, group ID)</value>
    public string group_detail_info_group_id;
    /// <value>uint [TIMGroupType](), 只读, 群组类型 (Read only, group type)</value>
    public TIMGroupType group_detail_info_group_type;
    /// <value>bool, 只读, 社群是否支持创建话题，只在群类型为 Community 时有效 (Read only, does support topic, works only when group_type is Community)</value>
    public bool? group_detail_info_is_support_topic;
    /// <value>string, 只读, 群组名称 (Read only, group name)</value>
    public string group_detail_info_group_name;
    /// <value>string, 只读, 群组公告 (Read only, group notification)</value>
    public string group_detail_info_notification;
    /// <value>string, 只读, 群组简介 (Read only, group introduction)</value>
    public string group_detail_info_introduction;
    /// <value>string, 只读, 群组头像URL (Read only, group avatar URL)</value>
    public string group_detail_info_face_url;
    /// <value>uint, 只读, 群组创建时间 (Read only, group created time)</value>
    public uint group_detail_info_create_time;
    /// <value>uint, 只读, 群资料的Seq，群资料的每次变更都会增加这个字段的值 (Read only, group info sequence number, every modification will augment the sequence number)</value>
    public uint group_detail_info_info_seq;
    /// <value>uint, 只读, 群组信息最后修改时间 (Read only, last modification time)</value>
    public uint group_detail_info_last_info_time;
    /// <value>uint, 只读, 群最新消息的Seq (Read only, latest group message sequence)</value>
    public uint group_detail_info_next_msg_seq;
    /// <value>uint, 只读, 最新群组消息时间 (Read only, group member count)</value>
    public uint group_detail_info_last_msg_time;
    /// <value>uint, 只读, 群组当前成员数量 (Read only, group member count)</value>
    public uint group_detail_info_member_num;
    /// <value>uint, 只读, 群组最大成员数量 (Read only, maximum group member count)</value>
    public uint group_detail_info_max_member_num;
    /// <value>uint [TIMGroupAddOption](), 只读, 群组加群选项 (Read only, joining group option)</value>
    public TIMGroupAddOption group_detail_info_add_option;
    /// <value>uint, 只读, 群组在线成员数量 (Read only, online member count)</value>
    public uint group_detail_info_online_member_num;
    /// <value>uint, 只读, 群组成员是否对外可见 (Read only, is visible to outsiders)</value>
    public uint group_detail_info_visible;
    /// <value>uint, 只读, 群组是否能被搜索 (Read only, is searchable)</value>
    public uint group_detail_info_searchable;
    /// <value>bool, 只读, 群组是否被设置了全员禁言 (Read only, is set mute all)</value>
    public bool group_detail_info_is_shutup_all;
    /// <value>string, 只读, 群组所有者ID (Read only, group owner user ID)</value>
    public string group_detail_info_owner_identifier;
    /// <value>array [GroupInfoCustomString](), 只读, 请参考[自定义字段](https:///cloud.tencent.com/document/product/269/1502#.E8.87.AA.E5.AE.9A.E4.B9.89.E5.AD.97.E6.AE.B5) (Read only, [Custom Group Fields](https://www.tencentcloud.com/document/product/1047/33529))</value>
    public List<GroupInfoCustomString> group_detail_info_custom_info;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class GroupPendencyResult : ExtraData
  {
    /// <value>uint64, 只读, 下一次拉取的起始时戳,server返回0表示没有更多的数据,否则在下次获取数据时以这个时间戳作为开始时间戳 (Read only, next group pendency start time, 0 means no more data, otherwise use this timestamp to search)</value>
    public ulong group_pendency_result_next_start_time;
    /// <value>uint64, 只读, 已读上报的时间戳 (Read only, reported read time seq)</value>
    public ulong group_pendency_result_read_time_seq;
    /// <value>uint, 只读, 未决请求的未读数 (Read only, pendency unread number)</value>
    public uint group_pendency_result_unread_num;
    /// <value>array [GroupPendency](), 只读, 群未决信息列表 (Read only, pendency list)</value>
    public List<GroupPendency> group_pendency_result_pendency_array;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class GroupGetOnlineMemberCountResult : ExtraData
  {
    /// <value>int, 只读, 指定群的在线人数 (Read only, group online member count)</value>
    public int group_get_online_member_count_result;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class FriendResult : ExtraData
  {
    /// <value>string, 只读, 关系链操作的用户ID (Read only, friend operator's user ID)</value>
    public string friend_result_identifier;
    /// <value>int [错误码](https:///cloud.tencent.com/document/product/269/1671), 只读, 关系链操作的结果 (Read only, operation code, [Error Codes](https://www.tencentcloud.com/document/product/1047/34348))</value>
    public int friend_result_code;
    /// <value>string, 只读, 关系链操作失败的详细描述 (Read only, friend result description)</value>
    public string friend_result_desc;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class FriendshipCheckFriendTypeResult : ExtraData
  {
    /// <value>string, 只读, 被检测的好友UserID (Read only, checked friend's user ID)</value>
    public string friendship_check_friendtype_result_identifier;
    /// <value>uint [TIMFriendCheckRelation](), 只读, 检测成功时返回的二者之间的关系 (Read only, relation result)</value>
    public TIMFriendCheckRelation friendship_check_friendtype_result_relation;
    /// <value>int [错误码](https:///cloud.tencent.com/document/product/269/1671), 只读, 检测的结果 (Read only, result code, [Error Codes](https://www.tencentcloud.com/document/product/1047/34348))</value>
    public int friendship_check_friendtype_result_code;
    /// <value>string, 只读, 检测好友失败的描述信息 (Read only, failed result description)</value>
    public string friendship_check_friendtype_result_desc;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class PendencyPage : ExtraData
  {
    /// <value>uint64, 只读, 未决请求信息页的起始时间 (Read only, pendency page start time)</value>
    public ulong pendency_page_start_time;
    /// <value>uint64, 只读, 未决请求信息页的未读数量 (Read only, pendency page unread number)</value>
    public int pendency_page_unread_num;
    /// <value>uint64, 只读, 未决请求信息页的当前Seq (Read only, pendency page current seq)</value>
    public ulong pendency_page_current_seq;
    /// <value>array [FriendAddPendencyInfo](), 只读, 未决请求信息页的未决信息列表 (Read only, pendency info list)</value>
    public List<FriendAddPendencyInfo> pendency_page_pendency_info_array;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class FriendAddPendencyInfo : ExtraData
  {
    /// <value>uint [TIMFriendPendencyType](), 只读, 好友添加请求未决类型 (Read only, friend addition pendency type)</value>
    public TIMFriendPendencyType friend_add_pendency_info_type;
    /// <value>string, 只读, 好友添加请求未决的UserID (Read only, peer's user ID)</value>
    public string friend_add_pendency_info_identifier;
    /// <value>string, 只读, 好友添加请求未决的昵称 (Read only, peer's nickname)</value>
    public string friend_add_pendency_info_nick_name;
    /// <value>uint64, 只读, 发起好友申请的时间 (Read only, request time)</value>
    public ulong friend_add_pendency_info_add_time;
    /// <value>string, 只读, 好友添加请求未决的添加来源 (Read only, request source)</value>
    public string friend_add_pendency_info_add_source;
    /// <value>string, 只读, 好友添加请求未决的添加附言 (Read only, request appending words)</value>
    public string friend_add_pendency_info_add_wording;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class FriendInfoGetResult : ExtraData
  {
    /// <value>string, 只读, 好友 user_id (Read only, friend's user ID)</value>
    public string friendship_friend_info_get_result_identifier;
    /// <value>uint [TIMFriendshipRelationType], 只读， 好友关系 (Read only, relationship)</value>
    public TIMFriendshipRelationType friendship_friend_info_get_result_relation_type;
    /// <value>uint， 只读，错误码 (Read only, error code)</value>
    public uint friendship_friend_info_get_result_error_code;
    /// <value>string, 只读， 错误描述 (Read only, error description)</value>
    public string friendship_friend_info_get_result_error_message;
    /// <value>[FriendProfile], 只读, 好友资料 (Read only, friend's profile)</value>
    public FriendProfile friendship_friend_info_get_result_field_info;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class MessageSearchResult : ExtraData
  {
    /// <value>uint, 只读, 如果您本次搜索【指定会话】，那么返回满足搜索条件的消息总数量；如果您本次搜索【全部会话】，那么返回满足搜索条件的消息所在的所有会话总数量。 (Read only, if you searched for [specified conversation], it will return the number of messages in the specified conversation; if you searched for [all conversation], it will return the number of messages in all conversations)</value>
    public uint msg_search_result_total_count;
    /// <value>array [TIMMessageSearchResultItem](), 只读, 如果您本次搜索【指定会话】，那么返回结果列表只包含该会话结果；如果您本次搜索【全部会话】，那么对满足搜索条件的消息根据会话 ID 分组，分页返回分组结果； (Read only, if you searched for [specified conversation], it will return the search result in the specified conversation; if you searched for [all conversation], it will return message results in all conversations by pages)</value>
    public List<MessageSearchResultItem> msg_search_result_item_array;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class MessageSearchResultItem : ExtraData
  {
    /// <value>string, 只读，会话 ID (Read only, conversation ID)</value>
    public string msg_search_result_item_conv_id;
    /// <value>uint [TIMConvType](), 只读, 会话类型 (Read only, conversation type)</value>
    public TIMConvType msg_search_result_item_conv_type;
    /// <value>uint, 只读, 当前会话一共搜索到了多少条符合要求的消息 (Read only, number of total message count)</value>
    public uint msg_search_result_item_total_message_count;
    /// <value>array [Message](), 只读, 满足搜索条件的消息列表 (Read only, message list)</value>
    public List<Message> msg_search_result_item_message_array;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class MsgBatchSendResult : ExtraData
  {
    /// <value>string, 只读, 接收群发消息的用户 ID (Read only, receiver's user ID)</value>
    public string msg_batch_send_result_identifier;
    /// <value>int [错误码](https:///cloud.tencent.com/document/product/269/1671), 只读, 消息发送结果 (Read only, result code [Error Codes](https://www.tencentcloud.com/document/product/1047/34348))</value>
    public int msg_batch_send_result_code;
    /// <value>string, 只读, 消息发送的描述 (Read only, result description)</value>
    public string msg_batch_send_result_desc;
    /// <value>object [Message](), 只读, 发送的消息 (Read only, message)</value>
    public Message msg_batch_send_result_msg;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class MsgDownloadElemResult : ExtraData
  {
    /// <value>uint, 只读, 当前已下载的大小 (Read only, current downloading size)</value>
    public uint msg_download_elem_result_current_size;
    /// <value>uint, 只读, 需要下载的文件总大小 (Read only, total downloading size)</value>
    public uint msg_download_elem_result_total_size;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class ResponseInfo : ExtraData
  {
    /// <value>string [TIMInternalOperation](), 只读(必填), 响应的内部操作 (Read only, internal operation response)</value>
    public string response_internal_operation;
    /// <value>object [SSODataRes](), 只读(选填), sso发包请求的响应, 当 kTIMResponseInternalOperation 为 kTIMInternalOperationSSOData 时有值 (Read only, SSO data package, required when kTIMResponseInternalOperation is kTIMInternalOperationSSOData)</value>
    public SSODataRes response_sso_data_res;
    /// <value>array [UserInfo](), 只读(选填), 响应的tinyid列表, 当 kTIMResponseInternalOperation 为 kTIMInternalOperationUserId2TinyId 时有值 (Read only, tiny ID list response, required when kTIMRequestInternalOperation is kTIMInternalOperationUserId2TinyId)</value>
    public List<UserInfo> response_userid_tinyid_res;
    /// <value>array [UserInfo](), 只读(选填), 响应的userid列表, 当 kTIMResponseInternalOperation 为 kTIMInternalOperationTinyId2UserId 时有值 (Read only, user ID list response, required when kTIMResponseInternalOperation is kTIMInternalOperationTinyId2UserId)</value>
    public List<UserInfo> response_tinyid_userid_res;
    /// <value>bool, 只读(选填), true 表示当前环境为测试环境，false表示当前环境是正式环境, 当 kTIMResponseInternalOperation 为 kTIMInternalOperationSetEnv 时有值 (Read only, is test environment, default: false, required when kTIMResponseInternalOperation is kTIMInternalOperationSetEnv)</value>
    public bool response_set_env_res;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class UserInfo : ExtraData
  {
    /// <value>string, userid</value>
    public string user_info_userid;
    /// <value>uint, tinyid</value>
    public uint user_info_tinyid;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class SSODataRes : ExtraData
  {
    /// <value>string, 只读(必填), sso返回数据对应请求的命令字 (Read only, SSO cmd data response)</value>
    public string sso_data_res_cmd;
    /// <value>string, 只读(必填), sso返回的内容，内容是二进制，sdk内部使用base64编码了，外部使用前需要base64解码 (Read only, SSO returned data in base64 format, use after decoding base64)</value>
    public string sso_data_res_body;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class GroupGetMemberInfoListResult : ExtraData
  {
    /// <value>uint64, 只读, 下一次拉取的标志,server返回0表示没有更多的数据,否则在下次获取数据时填入这个标志 (Read only, next group member seq, 0 means no more data, otherwise use this seq to search)</value>
    public ulong group_get_member_info_list_result_next_seq;
    /// <value>array [GroupMemberInfo](), 只读, 成员信息列表 (Read only, group member list)</value>
    public List<GroupMemberInfo> group_get_member_info_list_result_info_array;
  }
  [JsonObject(MemberSerialization.OptOut)]
  public class GroupTopicInfoResult : ExtraData
  {
    /// <value>int, 只读, 结果 0：成功；非0：失败 (Read only, 0: success, !0: error)</value>
    public int group_topic_info_result_error_code;
    /// <value>string, 只读, 如果删除失败，会返回错误信息 (Read only, if failed, it will return error message)</value>
    public string group_topic_info_result_error_message;
    /// <value>object [TIMGroupTopicInfo](), 只读, 如果获取成功，会返回对应的 info (Read only, it will return GroupTopicInfo if successful)</value>
    public GroupTopicInfo group_topic_info_result_topic_info;
  }
  [JsonObject(MemberSerialization.OptOut)]
  public class GroupTopicInfo : ExtraData
  {
    /// <value>string, 读写, 话题 ID (Read & Write, topic ID)</value>
    public string group_topic_info_topic_id;
    /// <value>string, 读写, 话题名称 (Read & Write, topic name)</value>
    public string group_topic_info_topic_name;
    /// <value>string, 读写, 话题介绍 (Read & Write, topic introduction)</value>
    public string group_topic_info_introduction;
    /// <value>string, 读写, 话题公告 (Read & Write, topic notification)</value>
    public string group_topic_info_notification;
    /// <value>string, 读写, 话题头像 (Read & Write, topic avatar URL)</value>
    public string group_topic_info_topic_face_url;
    /// <value>bool, 读写, 话题全员禁言 (Read & Write, is topic all muted)</value>
    public bool? group_topic_info_is_all_muted;
    /// <value>uint32, 读写, 当前用户在话题中的禁言时间 (Read & Write, self muted time)</value>
    public uint? group_topic_info_self_mute_time;
    /// <value>string, 读写, 话题自定义字段 (Read & Write, custom topic string)</value>
    public string group_topic_info_custom_string;
    /// <value>uint [TIMReceiveMessageOpt]()只读，话题消息接收选项，修改话题消息接收选项请调用 setGroupReceiveMessageOpt 接口 (Read only, topic message receiving option, call setGroupReceiveMessageOpt to modify this field)</value>
    public TIMReceiveMessageOpt? group_topic_info_recv_opt;
    /// <value>string, 读写, 话题草稿 (Read & Write, topic draft text)</value>
    public string group_topic_info_draft_text;
    /// <value>uint64, 只读, 话题消息未读数量 (Read only, topic unread count)</value>
    public uint? group_topic_info_unread_count;
    /// <value>object [Message](),只读, 话题 lastMessage (Read only, topic lastMessage)</value>
    public Message group_topic_info_last_message;
    /// <value>array [GroupAtInfo](), 只读, 话题 at 信息列表 (Read only, topic group @ info list)</value>
    public List<GroupAtInfo> group_topic_info_group_at_info_array;
    /// <value>uint [TIMGroupModifyInfoFlag](),只写(必填), 修改标识,可设置多个值按位或 (Write only (Required), modification flag, bit union)</value>
    public TIMGroupModifyInfoFlag? group_modify_info_param_modify_flag;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class UserStatus : ExtraData
  {
    /// <value>string, 只读, 用户 ID (Read only, user ID)</value>
    public string user_status_identifier;
    /// <value>uint [TIMUserStatusType](), 只读, 用户的状态 (Read only, user status type)</value>
    public TIMUserStatusType? user_status_status_type;
    /// <value>string, 读写, 用户的自定义状态 (Read & Write, user's custom status)</value>
    public string user_status_custom_status;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class GroupTopicOperationResult : ExtraData
  {
    /// <value>只读, 结果 0：成功；非0：失败 (Read only, result code, 0: success; not 0: fail)</value>
    public int group_topic_operation_result_error_code;
    /// <value>只读, 如果删除失败，会返回错误信息 (Read only, error message)</value>
    public string group_topic_operation_result_error_message;
    /// <value>只读, 如果删除成功，会返回对应的 topicID (Read only, it will return topic ID if successful)</value>
    public string group_topic_operation_result_topic_id;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class TopicInfo : ExtraData
  {
    /// <value>读写, 话题 ID, 只能在创建话题或者修改话题信息的时候设置。组成方式为：社群 ID + @TOPIC#_xxx，例如社群 ID 为 @TGS#_123，则话题 ID 为 @TGS#_123@TOPIC#_xxx(Read & Write, topic id)</value>
    public string group_topic_info_topic_id;
    /// <value>读写, 话题名称(Read & Write, topic name)</value>
    public string group_topic_info_topic_name;
    /// <value>读写, 话题介绍(Read & Write, topic introduction)</value>
    public string group_topic_info_introduction;
    /// <value>读写, 话题公告(Read & Write, topic notification)</value>
    public string group_topic_info_notification;
    /// <value>读写, 话题头像(Read & Write, topic face url)</value>
    public string group_topic_info_topic_face_url;
    /// <value>读写, 话题全员禁言(Read & Write, topic is all muted)</value>
    public bool? group_topic_info_is_all_muted;
    /// <value>只读, 当前用户在话题中的禁言时间(Read only, self mute time)</value>
    public uint? group_topic_info_self_mute_time;
    /// <value>读写, 话题自定义字段(Read & Write, topic custom string)</value>
    public string group_topic_info_custom_string;
    /// <value>TIMReceiveMessageOpt, 只读, 话题消息接收选项，修改话题消息接收选项请调用 @ref TIMMsgSetGroupReceiveMessageOpt 接口(Read & Write, receive opt)</value>
    public TIMReceiveMessageOpt? group_topic_info_recv_opt;
    /// <value>读写, 话题草稿(Read & Write, topic draft text)</value>
    public string group_topic_info_draft_text;
    /// <value>只读, 话题已读消息的 sequence，从 7.8 版本开始支持(Read only, topic unread count)</value>
    public ulong group_topic_info_unread_count;
    /// <value>只读, 话题 lastMessage(Read only, topic last message)</value>
    public Message group_topic_info_last_message;
    /// <value>只读, 话题已读消息的 sequence，从 7.8 版本开始支持(Read only, topic read sequence)</value>
    public ulong group_topic_info_read_sequence;
    /// <value>只读, 话题 at 信息列表(Read only, topic at info array)</value>
    public List<GroupAtInfo> group_topic_info_group_at_info_array;
    /// <value>ulong, 只写(必填), 修改标识, TIMCommunityTopicModifyInfoFlag 可设置多个值按位或(Write only, modify flag)</value>
    public ulong group_modify_info_param_modify_flag;
    /// <value>只读, 话题创建时间(Read only, topic create time)</value>
    public uint create_time;
    /// <value>读写, 话题默认权限, 7.8 版本开始支持。群成员在没有加入任何权限组时的默认权限，仅在社群资料 GroupDetailInfo 中的 group_detail_info_enable_permission_group 为 true 时生效(Read & Write, default permissions)</value>
    public ulong default_permissions;
    /// <value>只读, 登录用户在话题中的消息接收选项是否继承社群。该功能仅增强版 SDK 8.1 及以上版本支持。(Read only, whether the message receive option is inherited from community)</value>
    public bool group_topic_info_is_inherit_message_receive_option_from_community;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class PermissionGroupInfo : ExtraData
  {
    /// <value>读写, 社群 ID(Read & Write, community group id)</value>
    public string community_group_id;
    /// <value>读写, 权限组 ID, 只能在创建话题或者修改话题信息的时候设置(Read & Write, permission group id)</value>
    public string permission_group_id;
    /// <value>读写, 权限组名称(Read & Write, permission group name)</value>
    public string permission_group_name;
    /// <value>读写, 权限组自定义数据(Read & Write, permission group custom data)</value>
    public string permission_custom_data;
    /// <value>读写, 社群权限(Read & Write, community permission)</value>
    public ulong group_permission;
    /// <value>只读, 权限组人数(Read only, permission group member count)</value>
    public ulong permission_group_member_count;
    /// <value>只写(必填), 修改标识, TIMPermissionGroupModifyInfoFlag 可设置多个值按位或(Write only, modify flag)</value>
    public uint permission_group_modify_info_flag;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class TopicPermission : ExtraData
  {
    /// <value>读写, 话题权限自定义的 key(Read & Write, the key of custom topic permission)</value>
    public string topic_permission_key;
    /// <value>读写, 话题权限自定义的 value(Read & Write, the value of custom topic permission)</value>
    public ulong topic_permission_value;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class PermissionGroupInfoResult : ExtraData
  {
    /// <value>只读, 结果 0：成功；非 0：失败(Read only, result code, 0: success; not 0: fail)</value>
    public int permission_group_info_result_error_code;
    /// <value>只读, 如果删除失败，会返回错误信息(Read only, error message)</value>
    public string permission_group_info_result_error_message;
    /// <value>只读, 如果获取成功，会返回对应的 PermissionGroupInfo 信息(Read only, it will return PermissionGroupInfo if successful)</value>
    public PermissionGroupInfo permission_group_info_result_permission_group_info;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class PermissionGroupOperationResult : ExtraData
  {
    /// <value>只读, 结果 0：成功；非 0：失败(Read only, result code, 0: success; not 0: fail)</value>
    public int permission_group_operation_result_error_code;
    /// <value>只读, 如果删除失败，会返回错误信息(Read only, error message)</value>
    public string permission_group_operation_result_error_message;
    /// <value>只读, 如果删除成功，会返回对应的 permissionGroupID(Read only, it will return permissionGroupID if successful)</value>
    public string permission_group_operation_result_id;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class PermissionGroupMemberOperationResult : ExtraData
  {
    /// <value>只读, 被邀请加入权限组的用户 ID(Read only, the identifier of the user invited to the permission group)</value>
    public string permission_group_member_operation_result_identifier;
    /// <value>只读, 处理结果(Read only, result code)</value>
    public uint permission_group_member_operation_result_error_code;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class PermissionGroupMemberInfoResult : ExtraData
  {
    /// <value>只读, 下一次拉取的标志, server 返回空表示没有更多的数据,否则在下次获取数据时可以填入这个标志继续拉取(Read only, the next cursor to get the next page of data)</value>
    public string permission_group_member_info_result_next_cursor;
    /// <value>只读, 成员信息列表(Read only, group member info list)</value>
    public List<GroupMemberInfo> permission_group_member_info_result_array;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class TopicPermissionResult : ExtraData
  {
    /// <value>只读, 结果 0：成功；非 0：失败(Read only, result code, 0: success; not 0: fail)</value>
    public int topic_permission_result_error_code;
    /// <value>只读, 如果获取失败，会返回错误信息(Read only, error message)</value>
    public string topic_permission_result_error_message;
    /// <value>只读, 话题 ID。(Read only, topic ID)</value>
    public string topic_permission_result_topic_id;
    /// <value>只读, 话题 权限。(Read only, topic permission)</value>
    public uint topic_permission_result_topic_permission;
    /// <value>只读, 社群 ID。(Read only, group ID)</value>
    public string topic_permission_result_group_id;
    /// <value>只读, 权限组 ID。(Read only, permission group ID)</value>
    public string topic_permission_result_permission_group_id;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class PermissionGroupCallback : ExtraData
  {
    /// <value>读写, 权限组 ID(Read & Write, permission group id)</value>
    public string permission_group_info_permission_group_id;
    /// <value>只读, 话题权限 map (Read only, topic permission map)</value>
    public List<TopicPermission> permission_group_callback_topic_permission_map;
    /// <value>只读, 删除话题权限的话题 id 列表 (Read only, topic id list)</value>
    public List<string> permission_group_callback_topic_id_list;
    /// <value>只读, 权限组中添加或者删除成员的 id 列表 (Read only, member id list)</value>
    public List<string> permission_group_callback_member_id_list;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class ConversationOperationResult : ExtraData
  {
    /// <value>只读, 会话 ID (Read only, conversation ID)</value>
    public string conversation_operation_result_conversation_id;
    /// <value>只读, 返回码 (Read only, result code)</value>
    public int conversation_operation_result_result_code;
    /// <value>只读, 返回信息 (Read only, result info)</value>
    public string conversation_operation_result_result_info;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class ConversationListFilter : ExtraData
  {
    /// <value>[TIMConvType](), 只写, 会话类型 (Write only, conversation type)</value>
    public TIMConvType? conversation_list_filter_conv_type;
    /// <value>[TIMConversationMarkType](), 只写, 标记类型 (Write only, mark type)</value>
    public TIMConversationMarkType? conversation_list_filter_mark_type;
    /// <value>只写, 会话分组名称，注意：不是群组名称 (Write only, conversation group name)</value>
    public string conversation_list_filter_conversation_group;
    /// <value>只写, true:返回包含未读数的会话；false:返回所有会话 (default:false)</value>
    public bool? conversation_list_filter_has_unread_count;
    /// <value>true:返回包含群 @ 消息的会话；false:返回所有会话 (default:false)</value>
    public bool? conversation_list_filter_has_group_at_info;

  }

  [JsonObject(MemberSerialization.OptOut)]
  public class ConversationListResult : ExtraData
  {
    /// <value>array [ConvInfo](), 只读, 会话列表 (Read only, conversation info array)</value>
    public List<ConvInfo> conversation_list_result_conv_list;
    /// <value>uint64, 只读, 分页拉取的游标 (Read only, next sequence)</value>
    public ulong? conversation_list_result_next_seq;
    /// <value>bool, 只读, 分页拉取是否结束 (Read only, is result finished)</value>
    public bool? conversation_list_result_is_finished;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class GroupCounter : ExtraData
  {
    /// <value>string, 读写, 群计数器的 key 值 (Read & write, group counter key)</value>
    public string group_counter_key;
    /// <value>int64, 读写, 群计数器的 value 值 (Read & write, group counter value)</value>
    public long? group_counter_value;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class MessageTranslateTextResult : ExtraData
  {
    /// <value>string, 只读, 待翻译的文本 (Read only, source text)</value>
    public string msg_translate_text_source_text;
    /// <value>string, 只读, 翻译后的文本 (Read only, translated text)</value>
    public string msg_translate_text_target_text;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class OfficialAccountInfo : ExtraData
  {
    /// <value>只读, 公众号 ID (Read only, official account ID)</value>
    public string official_account_id;
    /// <value>只读, 公众号名称 (Read only, official account name)</value>
    public string official_account_name;
    /// <value>只读, 公众号头像 URL (Read only, official account face url)</value>
    public string official_account_face_url;
    /// <value>只读, 公众号所有者 (Read only, official account owner)</value>
    public string official_account_owner_user_id;
    /// <value>只读, 公众号介绍 (Read only, official account introduction)</value>
    public string official_account_introduction;
    /// <value>只读, 公众号组织 (Read only, official account organization)</value>
    public string official_account_organization;
    /// <value>只读, 公众号创建时间，单位：秒 (Read only, official account create time)</value>
    public ulong official_account_create_time;
    /// <value>只读, 公众号订阅者数量 (Read only, official account subscriber count)</value>
    public ulong official_account_subscriber_count;
    /// <value>只读, 公众号订阅时间，单位：秒 (Read only, official account subscribe time)</value>
    public ulong official_account_subscribe_time;
    /// <value>只读, 公众号自定义字段 (Read only, official account custom data)</value>
    public string official_account_custom_data;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class GetOfficialAccountInfoResult : ExtraData
  {
    /// <value>只读, 获取公众号详细资料的结果 (Read only, result code)</value>
    public int get_official_account_info_result_code;
    /// <value>只读, 获取公众号详细资料失败的描述信息 (Read only, result desc)</value>
    public string get_official_account_info_result_desc;
    /// <value>只读, 公众号详细信息 (Read only, official account info)</value>
    public OfficialAccountInfo get_official_account_info;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class FollowOperationResult : ExtraData
  {
    /// <value>只读, 关注/取关操作的用户 ID (Read only, user ID)</value>
    public string follow_operation_result_user_id;
    /// <value>只读, 关注/取关的操作返回码 (Read only, result code)</value>
    public int follow_operation_result_code;
    /// <value>只读, 关注/取关操作的返回结果描述 (Read only, result desc)</value>
    public string follow_operation_result_info;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class FollowListResult : ExtraData
  {
    /// <value>只读, 下一次拉要取列表的起始位置 (Read only, next cursor)</value>
    public string follow_list_result_next_cursor;
    /// <value>只读, 用户资料列表 (Read only, user info list)</value>
    public List<UserProfile> follow_list_result_user_info_list;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class FollowInfo : ExtraData
  {
    /// <value>只读, 用户 ID (Read only, user ID)</value>
    public string follow_info_user_id;
    /// <value>只读, 获取用户关注数量信息的返回码 (Read only, result code)</value>
    public int follow_info_result_code;
    /// <value>只读, 获取用户关注数量信息的返回结果描述 (Read only, result desc)</value>
    public string follow_info_result_info;
    /// <value>只读, 用户的关注数量 (Read only, following count)</value>
    public ulong? follow_info_following_count;
    /// <value>只读, 用户的粉丝数量 (Read only, followers count)</value>
    public ulong? follow_info_followers_count;
    /// <value>只读, 用户的互关数量 (Read only, mutual followers count)</value>
    public ulong? follow_info_mutual_followers_count;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class FollowTypeCheckResult : ExtraData
  {
    /// <value>只读, 用户 ID (Read only, user ID)</value>
    public string follow_type_check_result_user_id;
    /// <value>只读, 关注类型检查的返回码 (Read only, result code)</value>
    public int follow_type_check_result_code;
    /// <value>只读, 关注类型检查的返回结果描述 (Read only, result desc)</value>
    public string follow_type_check_result_info;
    /// <value>只读, 关注类型 (Read only, follow type)</value>
    public TIMFollowType follow_type_check_result_follow_type;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class UserSearchParam : ExtraData
  {
    /// <value>只写, 搜索的关键字列表，关键字列表最多支持 5 个，keyword 会自动匹配用户 ID、昵称。(Write only, keyword list, max: 5)</value>
    public List<string> user_search_param_keyword_list;
    /// <value>只写(选填), 指定关键字列表匹配类型，可设置为“或”关系搜索或者“与”关系搜索.(Write only, keyword list match type, default: TIMKeywordListMatchType_Or)</value>
    public TIMKeywordListMatchType? user_search_param_keyword_list_match_type;
    /// <value>只写(选填), 用户性别（如果不设置，默认男性和女性都会返回）(Write only, user gender, default: kTIMGenderType_Unknown)</value>
    public TIMGenderType? user_search_param_gender;
    /// <value>只写(选填), 用户最小生日（如果不设置，默认值为 0）(Write only, user min birthday, default: 0)</value>
    public uint? user_search_param_min_birthday;
    /// <value>只写(选填), 用户最大生日（如果不设置，默认 birthday >= minBirthday 的用户都会返回）(Write only, user max birthday, default: birthday >= minBirthday)</value>
    public uint? user_search_param_max_birthday;
    /// <value>只写, 每次云端搜索返回结果的条数（必须大于 0，最大支持 100，仅云端搜索有效）(Write only, count of results returned by each cloud search, must be greater than 0, maximum 100, only valid for cloud search)</value>
    public uint user_search_param_search_count;
    /// <value>只写(选填), 每次云端搜索的起始位置。第一次填空字符串，续拉时填写 UserSearchResult 中的返回值。(Write only, search cursor, default: "")</value>
    public string user_search_param_search_cursor;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class UserSearchResult : ExtraData
  {
    /// <value>只读, 是否已经返回全部满足搜索条件的用户列表(Read only, is finished)</value>
    public bool? user_search_result_is_finished;
    /// <value>只读, 满足搜索条件的用户总数量(Read only, total count of users that meet the search conditions)</value>
    public uint? user_search_result_total_count;
    /// <value>只读, 下一次云端搜索的起始位置(Read only, next cursor for next search)</value>
    public string user_search_result_next_cursor;
    /// <value>只读, 当前一次云端搜索返回的用户列表(Read only, user list of current search)</value>
    [JsonConverter(typeof(StringToList<UserProfile>))]
    public List<UserProfile> user_search_result_user_list;
  }

  [JsonObject(MemberSerialization.OptOut)]
  public class SignalingInfo : ExtraData
  {
    /// <value>只读, 邀请 ID (Read only, invite ID)</value>
    public string signaling_info_invite_id;
    /// <value>只读, 群组 ID (Read only, group ID)</value>
    public string signaling_info_group_id;
    /// <value>只读, 邀请方的 ID (Read only, inviter ID)</value>
    public string signaling_info_inviter;
    /// <value>只读, 被邀请方列表 (Read only, invitee list)</value>
    public List<string> signaling_info_invitee_list;
    /// <value>只读, 信令自定义内容 (Read only, data)</value>
    public string signaling_info_data;
    /// <value>只读, 信令响应类型 (Read only, action type)</value>
    public TIMSignalingActionType signaling_info_action_type;
    /// <value>只读, 超时时间 (Read only, timeout)</value>
    public uint signaling_info_timeout;
  }


  // 用于处理Deserialize时多余的参数
  // Handle Unknown Fields when Deserialize
  public class ExtraData
  {
    // public IDictionary<string, JToken> extra_data;

    [JsonExtensionData]
    private IDictionary<string, JToken> _additionalData;

    // [OnDeserialized]
    // private void OnDeserialized(StreamingContext context)
    // {
    // extra_data = _additionalData;
    // }
  }
}