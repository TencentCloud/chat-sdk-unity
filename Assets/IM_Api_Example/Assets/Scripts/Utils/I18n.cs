using com.tencent.im.unity.demo.types;
using System.Collections.Generic;
public static class I18n
{
  public static readonly Dictionary<string, I18nData> dict = new Dictionary<string, I18nData>{
    {
      "Base Module", new I18nData{
        cn = "基础模块"
      }
    },
    {
    "Conversation Module", new I18nData{
      cn = "会话模块"
      }
    },
    {
      "Message Module", new I18nData{
      cn = "消息模块"
      }
    },
    {
      "FriendShip Module", new I18nData{
      cn = "好友关系链模块"
      }
    },
    {
      "Group Module", new I18nData{
      cn = "群组模块"
      }
    },
    {
      "Signaling Module", new I18nData{
      cn = "信令模块"
      }
    },
    {
      "Community Module", new I18nData{
      cn = "社群模块"
      }
    },
    {
      "Config", new I18nData{
        cn = "配置信息"
      }
    },
    {
      "Modify Successfully", new I18nData{
        cn = "修改成功"
      }
    },
    {
      "Copied", new I18nData{
        cn = "已复制"
      }
    },
    {
      "Confirm", new I18nData{
        cn = "确认设置"
      }
    },
    {
      "Reset", new I18nData{
        cn = "清除所有配置"
      }
    },
    {
      "MessageLabel", new I18nData{
        cn = "发送文本",
        en = "Text Message Content"
      }
    },
    {
      "DataLabel", new I18nData{
        cn = "自定义数据Data",
        en = "Custom Message Data"
      }
    },
    {
      "DescLabel", new I18nData{
        cn = "自定义数据Desc",
        en = "Custom Message Desc"
      }
    },
    {
      "ExtLabel", new I18nData{
        cn = "自定义数据Extension",
        en = "Custom Message Extension"
      }
    },
    {
      "SelectElemTypeLabel", new I18nData{
        cn = "选择元素类型",
        en = "Select Elem Type"
      }
    },
    {
      "SelectFriendLabel", new I18nData{
        cn = "选择好友",
        en = "Select Friend"
      }
    },
    {
      "SelectFriendGroupNameLabel", new I18nData{
        cn = "选择好友分组",
        en = "Select Friend Group Name"
      }
    },
    {
      "SelectRoleLabel", new I18nData{
        cn = "选择角色",
        en = "Select Role"
      }
    },
    {
      "SelectBlackLabel", new I18nData{
        cn = "选择黑名单用户",
        en = "Select Black List User"
      }
    },
    {
      "SelectGroupLabel", new I18nData{
        cn = "选择群组",
        en = "Select Group"
      }
    },
    {
      "SelectGroupMemberLabel", new I18nData{
        cn = "选择群成员",
        en = "Select Group Member"
      }
    },
    {
      "SelectMessageFlagLabel", new I18nData{
        cn = "选择消息接受选项",
        en = "Select Message Flag"
      }
    },
    {
      "SelectGroupSearchFieldLabel", new I18nData{
        cn = "选择搜索域",
        en = "Select Search Field"
      }
    },
    {
      "SelectConvLabel", new I18nData{
        cn = "选择会话",
        en = "Select Conversation"
      }
    },
    {
      "SelectPriorityLabel", new I18nData{
        cn = "选择优先级",
        en = "Select Message Priority"
      }
    },
    {
      "SelectGenderLabel", new I18nData{
        cn = "选择性别",
        en = "Select Gender"
      }
    },
    {
      "SelectAddPermissionLabel", new I18nData{
        cn = "选择加好友选项",
        en = "Select friend permisison type"
      }
    },
    {
      "IsOnlineLabel", new I18nData{
        cn = "是否仅在线用户接受到信息",
        en = "Only online users can receive messages"
      }
    },
    {
      "IsUnreadLabel", new I18nData{
        cn = "发送消息是否不计入未读数",
        en = "Don't count this message in Unread Receipt"
      }
    },
    {
      "needReceiptLabel", new I18nData{
        cn = "是否需要回执",
        en = "Need read receipt"
      }
    },
    {
      "supportMsgExtLabel", new I18nData{
        cn = "是否支持消息扩展",
        en = "Need message extension"
      }
    },
    {
      "getGroupListFailed", new I18nData{
        cn = "获取群组失败，请登陆",
        en = "Get group list failed, please login"
      }
    },
    {
      "getGroupMemberListFailed", new I18nData{
        cn = "获取群组成员列表失败，请登陆",
        en = "Get group member list failed, please login"
      }
    },
    {
      "getTopicInfoListFailed", new I18nData{
        cn = "获取话题列表失败，请登陆",
        en = "Get topic info list failed, please login"
      }
    },
    {
      "getBlackListFailed", new I18nData{
        cn = "获取黑名单失败，请登陆",
        en = "Get black list failed, please login"
      }
    },
    {
      "getMsgListFailed", new I18nData{
        cn = "获取消息列表失败，请登陆",
        en = "Get message list failed, please login"
      }
    },
    {
      "getConvListFailed", new I18nData{
        cn = "获取会话失败，请登陆",
        en = "Get conv list failed, please login"
      }
    },
    {
      "getFriendListFailed", new I18nData{
        cn = "获取好友失败，请登陆",
        en = "Get friend list failed, please login"
      }
    },
    {
      "getFriendGroupListFailed", new I18nData{
        cn = "获取好友分组失败，请登陆",
        en = "Get friend group list failed, please login"
      }
    },
    {
      "getPendencyListFailed", new I18nData{
        cn = "获取好友请求失败，请登陆",
        en = "Get friend pendency list failed, please login"
      }
    },
    {
      "getGroupPendencyListFailed", new I18nData{
        cn = "获取群组请求失败，请登陆",
        en = "Get group pendency list failed, please login"
      }
    },
    {
      "Copy", new I18nData{
        cn ="复制结果",
        en = "Copy Result"
      }
    },
    {
      "SelectUserLabel", new I18nData{
        cn = "选择用户",
        en = "Select User"
      }
    },
    {
      "GroupIDLabel", new I18nData{
        cn = "群ID",
        en = "GroupID"
      }
    },
    {
      "GreetingLabel", new I18nData{
        cn = "进群打招呼 Message",
        en = "Greeting Message"
      }
    },
    {
      "GroupNameLabel", new I18nData{
        cn = "群名称",
        en = "Group Name"
      }
    },
    {
      "GroupNotificationLabel", new I18nData{
        cn = "群公告",
        en = "Group Notification"
      }
    },
    {
      "GroupIntroductionLabel", new I18nData{
        cn = "群简介",
        en = "Group Introduction"
      }
    },
    {
      "GroupFaceURLLabel", new I18nData{
        cn = "群头像URL",
        en = "Group Avatar URL"
      }
    },
    {
      "GroupMaxMemberLabel", new I18nData{
        cn = "最大群人数",
        en = "Maximum Group Member"
      }
    },
    {
      "GroupIsMutedAllLabel", new I18nData{
        cn = "是否全体禁言",
        en = "Is All Member Muted"
      }
    },
    {
      "ShutupTimeLabel", new I18nData{
        cn = "禁言时间",
        en = "Muted Time"
      }
    },
    {
      "GroupNameCardLabel", new I18nData{
        cn = "群名片",
        en = "Group Name Card"
      }
    },
    {
      "GroupOwnerLabel", new I18nData{
        cn = "群主ID",
        en = "Group Owner UserID"
      }
    },
    {
      "SelectGroupTypeLabel", new I18nData{
        cn = "选择群类型",
        en = "Select Group Type"
      }
    },
    {
      "SelectGroupAddOptionLabel", new I18nData{
        cn = "选择加群方式",
        en = "Select Group Admission Type"
      }
    },
    {
      "EventListener", new I18nData {
        cn = "事件回调",
        en ="Event Callback"
      }
    },
    {
      "register", new I18nData {
        cn = "注册",
        en = "Register"
      }
    },
    {
      "remove", new I18nData {
        cn = "注销",
        en = "Remove"
      }
    },
    {
      "AddRecvNewMsgCallback", new I18nData{
        cn = "收到新消息回调"
      }
    },
    {
      "SetMsgReactionsChangedCallback", new I18nData{
        cn = "设置消息回应信息更新的回调"
      }
    },
    {
      "SetMsgAllMessageReceiveOptionCallback", new I18nData{
        cn = "设置全局消息接收选项的回调"
      }
    },
    {
      "SetMsgReadedReceiptCallback", new I18nData{
        cn = "消息已读回执回调"
      }
    },
    {
      "SetMsgRevokeCallback", new I18nData{
        cn = "接收的消息被撤回回调"
      }
    },
    {
      "SetGroupTipsEventCallback", new I18nData{
        cn = "群组系统消息回调"
      }
    },
    {
      "SetMsgElemUploadProgressCallback", new I18nData{
        cn = "消息内元素相关文件上传进度回调"
      }
    },
    {
      "SetGroupAttributeChangedCallback", new I18nData{
        cn = "群组属性变更回调"
      }
    },
    {
      "SetConvEventCallback", new I18nData{
        cn = "会话事件回调"
      }
    },
    {
      "SetConvTotalUnreadMessageCountChangedCallback", new I18nData{
        cn = "会话未读消息总数变更回调"
      }
    },
    {
      "SetNetworkStatusListenerCallback", new I18nData{
        cn = "网络连接状态监听回调"
      }
    },
    {
      "SetKickedOfflineCallback", new I18nData{
        cn = "被踢下线通知回调"
      }
    },
    {
      "SetUserSigExpiredCallback", new I18nData{
        cn = "票据过期回调"
      }
    },
    {
      "SetOnAddFriendCallback", new I18nData{
        cn = "添加好友回调"
      }
    },
    {
      "SetOnDeleteFriendCallback", new I18nData{
        cn = "删除好友回调"
      }
    },
    {
      "SetUpdateFriendProfileCallback", new I18nData{
        cn = "更新好友资料回调"
      }
    },
    {
      "SetFriendAddRequestCallback", new I18nData{
        cn = "好友添加请求回调"
      }
    },
    {
      "SetFriendApplicationListDeletedCallback", new I18nData{
        cn = "好友申请被删除回调"
      }
    },
    {
      "SetFriendApplicationListReadCallback", new I18nData{
        cn = "好友申请已读回调"
      }
    },
    {
      "SetFriendBlackListAddedCallback", new I18nData{
        cn = "黑名单新增回调"
      }
    },
    {
      "SetFriendBlackListDeletedCallback", new I18nData{
        cn = "黑名单删除回调"
      }
    },
    {
      "SetLogCallback", new I18nData{
        cn = "日志回调"
      }
    },
    {
      "SetMsgUpdateCallback", new I18nData{
        cn = "消息在云端被修改后回传回来的消息更新通知回调"
      }
    },
    {
      "SetGroupTopicCreatedCallback", new I18nData{
        cn = "话题创建回调"
      }
    },
    {
      "SetGroupTopicDeletedCallback", new I18nData{
        cn = "话题被删除回调"
      }
    },
    {
      "SetGroupTopicChangedCallback", new I18nData{
        cn = "话题更新回调"
      }
    },
    {
      "SetSelfInfoUpdatedCallback", new I18nData{
        cn = "当前用户的资料发生更新时的回调"
      }
    },
    {
      "SetUserStatusChangedCallback", new I18nData{
        cn = "用户状态变更通知回调"
      }
    },
    {
      "SetUserInfoChangedCallback", new I18nData{
        cn = "设置用户资料变更回调"
      }
    },
    {
      "SetMsgExtensionsChangedCallback", new I18nData{
        cn = "消息扩展信息更新的回调"
      }
    },
    {
      "SetMsgExtensionsDeletedCallback", new I18nData{
        cn = "消息扩展信息删除的回调"
      }
    },
    {
      "SetMsgGroupPinnedMessageChangedCallback", new I18nData{
        cn = "群置顶消息变更的回调"
      }
    },
    {
      "SetFriendGroupCreatedCallback", new I18nData{
        cn = "好友分组创建通知回调"
      }
    },
    {
      "SetFriendGroupDeletedCallback", new I18nData{
        cn = "好友分组删除通知回调"
      }
    },
    {
      "SetFriendGroupNameChangedCallback", new I18nData{
        cn = "好友分组名称变更通知回调"
      }
    },
    {
      "SetFriendsAddedToGroupCallback", new I18nData{
        cn = "好友添加到分组通知回调"
      }
    },
    {
      "SetFriendsDeletedFromGroupCallback", new I18nData{
        cn = "好友从分组删除通知回调"
      }
    },
    {
      "SetOfficialAccountSubscribedCallback", new I18nData{
        cn = "订阅公众号通知回调"
      }
    },
    {
      "SetOfficialAccountUnsubscribedCallback", new I18nData{
        cn = "取消订阅公众号通知回调"
      }
    },
    {
      "SetOfficialAccountDeletedCallback", new I18nData{
        cn = "删除公众号通知回调"
      }
    },
    {
      "SetOfficialAccountInfoChangedCallback", new I18nData{
        cn = "公众号信息变更通知回调"
      }
    },
    {
      "SetMyFollowingListChangedCallback", new I18nData{
        cn = "我的关注列表变更通知回调"
      }
    },
    {
      "SetMyFollowersListChangedCallback", new I18nData{
        cn = "我的粉丝列表变更通知回调"
      }
    },
    {
      "SetMutualFollowersListChangedCallback", new I18nData{
        cn = "互相关注列表变更通知回调"
      }
    },
    {
      "SetSignalingReceiveNewInvitationCallback", new I18nData{
        cn = "收到信令邀请通知回调"
      }
    },
    {
      "SetSignalingInvitationCancelledCallback", new I18nData{
        cn = "信令邀请被取消通知回调"
      }
    },
    {
      "SetSignalingInviteeAcceptedCallback", new I18nData{
        cn = "信令邀请被接受通知回调"
      }
    },
    {
      "SetSignalingInviteeRejectedCallback", new I18nData{
        cn = "信令邀请被拒绝通知回调"
      }
    },
    {
      "SetSignalingInvitationTimeoutCallback", new I18nData{
        cn = "信令邀请超时通知回调"
      }
    },
    {
      "SetSignalingInvitationModifiedCallback", new I18nData{
        cn = "信令邀请被修改通知回调"
      }
    },
    {
      "SetCommunityCreateTopicCallback", new I18nData{
        cn = "话题创建通知回调"
      }
    },
    {
      "SetCommunityDeleteTopicCallback", new I18nData{
        cn = "话题删除通知回调"
      }
    },
    {
      "SetCommunityChangeTopicInfoCallback", new I18nData{
        cn = "话题变更通知回调"
      }
    },
    {
      "SetCommunityReceiveTopicRESTCustomDataCallback", new I18nData{
        cn = "话题自定义数据接收通知回调"
      }
    },
    {
      "SetCommunityCreatePermissionGroupCallback", new I18nData{
        cn = "权限组创建通知回调"
      }
    },
    {
      "SetCommunityDeletePermissionGroupCallback", new I18nData{
        cn = "权限组删除通知回调"
      }
    },
    {
      "SetCommunityChangePermissionGroupInfoCallback", new I18nData{
        cn = "权限组变更通知回调"
      }
    },
    {
      "SetCommunityAddMembersToPermissionGroupCallback", new I18nData{
        cn = "向权限组添加成员通知回调"
      }
    },
    {
      "SetCommunityRemoveMembersFromPermissionGroupCallback", new I18nData{
        cn = "从权限组删除成员通知回调"
      }
    },
    {
      "SetCommunityAddTopicPermissionCallback", new I18nData{
        cn = "添加话题权限的通知回调"
      }
    },
    {
      "SetCommunityDeleteTopicPermissionCallback", new I18nData{
        cn = "删除话题权限的通知回调"
      }
    },
    {
      "SetCommunityModifyTopicPermissionCallback", new I18nData{
        cn = "修改话题权限的通知回调"
      }
    },
    {
      "SetExperimentalNotifyCallback", new I18nData{
        cn = "实验性通知的回调"
      }
    },
    {
      "FriendIDLabel", new I18nData{
        cn = "好友UserID",
        en = "Friend's UserID"
      }
    },
    {
      "FriendRemarkLabel", new I18nData{
        cn = "好友备注",
        en = "Friend's Remark"
      }
    },
    {
      "FriendGroupLabel", new I18nData{
        cn = "好友分组，分组需提前创建",
        en = "Friend's group name, which is predefined"
      }
    },
    {
      "FriendAddWordLabel", new I18nData{
        cn = "好友附言",
        en = "Reason for adding friend"
      }
    },
    {
      "FriendResponseActionLabel", new I18nData{
        cn = "响应好友添加",
        en = "Response to adding friend request"
      }
    },
    {
      "SelectFriendTypeLabel", new I18nData{
        cn = "好友类型",
        en = "Friend type"
      }
    },
    {
      "ImageMessageLabel", new I18nData{
        cn = "选择图片",
        en = "Select image"
      }
    },
    {
      "VideoMessageLabel", new I18nData{
        cn = "选择视频",
        en = "Select video"
      }
    },
    {
      "ScreenshotMessageLabel", new I18nData{
        cn = "选择视频截图",
        en = "Select video thumbnail"
      }
    },
    {
      "FileMessageLabel", new I18nData{
        cn = "选择文件",
        en = "Select file"
      }
    },
    {
      "SoundMessageLabel", new I18nData{
        cn = "开始录音",
        en = "Start recording"
      }
    },
    {
      "SoundFinMessageLabel", new I18nData{
        cn = "结束录音",
        en = "End recording"
      }
    },
    {
      "DraftLabel", new I18nData{
        cn = "草稿内容",
        en = "Draft content"
      }
    },
    {
      "PinLabel", new I18nData{
        cn = "会话置顶",
        en = "Pin conversation"
      }
    },
    {
      "SelectMsgLabel", new I18nData{
        cn = "选择消息",
        en = "Select message"
      }
    },
    {
      "SelectMemberFilterLabel", new I18nData{
        cn = "选择群消息成员列表",
        en = "Select group read message member list"
      }
    },
    {
      "SelectFriendPendencyTypeLabel", new I18nData{
        cn = "选择好友添加请求未决类型",
        en = "Select the pendency type of friend request"
      }
    },
    {
      "SelectGroupPendencyLabel", new I18nData{
        cn = "选择加群请求",
        en = "Select the pendency of joining group request"
      }
    },
    {
      "SelectUserStatusLabel", new I18nData{
        cn = "选择用户状态",
        en = "Select user status"
      }
    },
    {
      "CustomStatusLabel", new I18nData{
        cn = "用户自定义状态",
        en = "User custom status"
      }
    },
    {
      "GroupHandlePendencyMsgLabel", new I18nData{
        cn = "加群请求处理附言",
        en = "Comments of handling group request"
      }
    },
    {
      "GroupHandlePendencyLabel", new I18nData{
        cn = "同意加群",
        en = "Accept joining group request"
      }
    },
    {
      "FaceIndexLabel", new I18nData{
        cn = "表情索引",
        en = "Index of face message"
      }
    },
    {
      "FaceBufLabel", new I18nData{
        cn = "表情额外信息",
        en = "Extra info of face message"
      }
    },
    {
      "LongtitudeLabel", new I18nData{
        cn = "经度",
        en = "Longtitude"
      }
    },
    {
      "LatitudeLabel", new I18nData{
        cn = "纬度",
        en = "Latitude"
      }
    },
    {
      "LocationDescLabel", new I18nData{
        cn = "位置描述",
        en = "Description of location message"
      }
    },
    {
      "FriendGroupNameLabel", new I18nData{
        cn = "好友分组名称",
        en = "Friend group name"
      }
    },
    {
      "KeywordLabel", new I18nData{
        cn = "关键字",
        en = "Keyword"
      }
    },
    {
      "FriendSearchFieldLabel", new I18nData{
        cn = "好友搜索类型",
        en = "Friendship search field"
      }
    },
    {
      "FriendCustomKeyLabel", new I18nData{
        cn = "自定义好友字段名称",
        en = "Friend's profile custom key"
      }
    },
    {
      "FriendCustomValueLabel", new I18nData{
        cn = "自定义好友字段值",
        en = "Friend's profile custom value"
      }
    },
    {
      "FriendCustomKeyPlaceHolder", new I18nData{
        cn = "请在控制台查看，按,分隔多个key",
        en = "Check key on the Console, keys are separated by ','"
      }
    },
    {
      "MemberCustomKeyLabel", new I18nData{
        cn = "群成员自定义字段名称",
        en = "Group member's profile custom key"
      }
    },
    {
      "MemberCustomValueLabel", new I18nData{
        cn = "群成员自定义字段值",
        en = "Group member's profile custom value"
      }
    },
    {
      "ProfileCustomKeyLabel", new I18nData{
        cn = "用户自定义字段名称",
        en = "User's profile custom key"
      }
    },
    {
      "ProfileCustomValueLabel", new I18nData{
        cn = "用户自定义字段值",
        en = "User's profile custom value"
      }
    },
    {
      "GroupCustomKeyLabel", new I18nData{
        cn = "群自定义字段名称",
        en = "Group custom key"
      }
    },
    {
      "GroupCustomValueLabel", new I18nData{
        cn = "群自定义字段值",
        en = "Group custom value"
      }
    },
    {
      "CustomKeyPlaceHolder", new I18nData{
        cn = "按,分隔多个key",
        en = "Keys are separated by ','"
      }
    },
    {
      "CustomValuePlaceHolder", new I18nData{
        cn = "按,分隔多个value",
        en = "Values are separated by ','"
      }
    },
    {
      "NicknameLabel", new I18nData{
        cn = "昵称",
        en = "Nickname"
      }
    },
    {
      "FaceURLLabel", new I18nData{
        cn = "头像URL",
        en = "Avatar URL"
      }
    },
    {
      "SignatureLabel", new I18nData{
        cn = "签名",
        en = "Signature"
      }
    },
    {
      "KeywordPlaceHolder", new I18nData{
        cn = "按,分隔多个关键字，最多支持5个",
        en = "Keywords are separated by ',', maximum 5"
      }
    },
    {
      "IsSearchUserIdLabel", new I18nData{
        cn = "设置是否搜索群成员 userID",
        en = "Search keywords as userID"
      }
    },
    {
      "IsSearchNickNameLabel", new I18nData{
        cn = "设置是否搜索群成员昵称",
        en = "Search keywords as nickname"
      }
    },
    {
      "IsSearchRemarkLabel", new I18nData{
        cn = "设置是否搜索群成员备注",
        en = "Search keywords as remark"
      }
    },
    {
      "IsSearchNameCardLabel", new I18nData{
        cn = "设置是否搜索群成员名片",
        en = "Search keywords as name card"
      }
    },
    {
      "TopicIDLabel", new I18nData{
        cn = "话题ID",
        en = "Topic ID"
      }
    },
    {
      "TopicNameLabel", new I18nData{
        cn = "话题名称",
        en = "Topic Name"
      }
    },
    {
      "TopicIntroLabel", new I18nData{
        cn = "话题介绍",
        en = "Topic Introduction"
      }
    },
    {
      "TopicNotificationLabel", new I18nData{
        cn = "话题公告",
        en = "Topic Notification"
      }
    },
    {
      "TopicFaceUrlLabel", new I18nData{
        cn = "话题头像",
        en = "Topic Avatar URL"
      }
    },
    {
      "IsTopicAllMutedLabel", new I18nData{
        cn = "是否话题全员禁言",
        en = "Is All Muted Under The Topic"
      }
    },
    {
      "TopicSelfMuteTimeLabel", new I18nData{
        cn = "当前用户在话题中禁言时间",
        en = "Self Mute Time Under The Topic"
      }
    },
    {
      "TopicCustomStrLabel", new I18nData{
        cn = "话题自定义字段",
        en = "Topic Custom String"
      }
    },
    {
      "GroupCounterKeyLabel", new I18nData{
        cn = "群计数器的 key 值",
        en = "Group Counter keys"
      }
    },
    {
      "GroupCounterValueLabel", new I18nData{
        cn = "群计数器的 value 值",
        en = "Group Counter values (long)"
      }
    },
    {
      "TopicRecvOptLabel", new I18nData{
        cn = "话题消息接收选项",
        en = "Topic Message Receiving Option"
      }
    },
    {
      "TopicDraftLabel", new I18nData{
        cn = "话题草稿",
        en = "Topic Draft Text"
      }
    },
    {
      "MsgRecvOptLabel", new I18nData{
        cn = "消息接收选项",
        en = "Message Receiving Option"
      }
    },
    {
      "UserIDsInputTips", new I18nData{
        cn = "多个用户 ID 请用空格分隔",
        en = "Please separate user IDs with spaces"
      }
    },
    {
      "OfficialAccountIDsInputTips", new I18nData{
        cn = "多个公众号 ID 请用空格分隔",
        en = "Please separate official account IDs with spaces"
      }
    },
    {
      "TopicIDsInputTips", new I18nData{
        cn = "多个话题 ID 请用空格分隔",
        en = "Please separate topic IDs with spaces"
      }
    },
    {
      "PermissionGroupIDsInputTips", new I18nData{
        cn = "多个权限组 ID 请用空格分隔",
        en = "Please separate permission group IDs with spaces"
      }
    }
  };
};