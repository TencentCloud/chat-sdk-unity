namespace com.tencent.im.unity.demo.config.ApiDataList
{
  public static class ApiDataList
  {
    public static string ApiDataListStr = @"[
  {
    ""apiManager"": ""V2TimManager"",
    ""managerName"": ""Base Module"",
    ""apis"": [
      {
        ""apiName"": ""InitSDK"",
        ""apiText"": ""初始化SDK"",
        ""apiDesc"": ""sdk 初始化"",
        ""scene"": ""InitSDK""
      },
      {
        ""apiName"": ""AddEventListener"",
        ""apiText"": ""添加事件监听"",
        ""apiDesc"": ""事件监听应先于登录方法前添加，以防漏消息"",
        ""scene"": ""AddEventListener""
      },
      {
        ""apiName"": ""GetServerTime"",
        ""apiText"": ""获取服务端时间"",
        ""apiDesc"": ""sdk 获取服务端时间"",
        ""scene"": ""GetServerTime""
      },
      {
        ""apiName"": ""Login"",
        ""apiText"": ""登录"",
        ""apiDesc"": ""sdk 登录接口，先初始化"",
        ""scene"": ""Login""
      },
      {
        ""apiName"": ""Logout"",
        ""apiText"": ""登出"",
        ""apiDesc"": ""sdk 登录接口，先初始化"",
        ""scene"": ""Logout""
      },
      {
        ""apiName"": ""GetSDKVersion"",
        ""apiText"": ""获取 SDK 版本"",
        ""apiDesc"": ""获取 SDK 版本"",
        ""scene"": ""GetSDKVersion""
      },
      {
        ""apiName"": ""GetLoginUser"",
        ""apiText"": ""获取当前登录用户"",
        ""apiDesc"": ""获取当前登录用户"",
        ""scene"": ""GetLoginUser""
      },
      {
        ""apiName"": ""GetLoginStatus"",
        ""apiText"": ""获取当前登录状态"",
        ""apiDesc"": ""获取当前登录状态"",
        ""scene"": ""GetLoginStatus""
      },
      {
        ""apiName"": ""ProfileGetUserProfileList"",
        ""apiText"": ""获取用户信息列表"",
        ""apiDesc"": ""获取用户信息列表"",
        ""scene"": ""ProfileGetUserProfileList""
      },
      {
        ""apiName"": ""ProfileModifySelfUserProfile"",
        ""apiText"": ""修改自己的信息"",
        ""apiDesc"": ""修改自己的信息"",
        ""scene"": ""ProfileModifySelfUserProfile""
      },
      {
        ""apiName"": ""SubscribeUserInfo"",
        ""apiText"": ""订阅用户资料"",
        ""apiDesc"": ""订阅用户资料"",
        ""scene"": ""SubscribeUserInfo""
      },
      {
        ""apiName"": ""UnsubscribeUserInfo"",
        ""apiText"": ""取消订阅用户资料"",
        ""apiDesc"": ""取消订阅用户资料"",
        ""scene"": ""UnsubscribeUserInfo""
      },
      {
        ""apiName"": ""GetUserStatus"",
        ""apiText"": ""查询用户状态"",
        ""apiDesc"": ""查询用户状态"",
        ""scene"": ""GetUserStatus""
      },
      {
        ""apiName"": ""SetSelfStatus"",
        ""apiText"": ""设置自己的状态"",
        ""apiDesc"": ""设置自己的状态"",
        ""scene"": ""SetSelfStatus""
      },
      {
        ""apiName"": ""SubscribeUserStatus"",
        ""apiText"": ""订阅用户状态"",
        ""apiDesc"": ""订阅用户状态"",
        ""scene"": ""SubscribeUserStatus""
      },
      {
        ""apiName"": ""UnsubscribeUserStatus"",
        ""apiText"": ""取消订阅用户状态"",
        ""apiDesc"": ""取消订阅用户状态"",
        ""scene"": ""UnsubscribeUserStatus""
      }
    ]
  },
  {
    ""apiManager"": ""V2TimMessageManager"",
    ""managerName"": ""Conversation Module"",
    ""apis"": [
      {
        ""apiName"": ""ConvGetConvList"",
        ""apiText"": ""获取会话列表"",
        ""apiDesc"": ""获取会话列表"",
        ""scene"": ""ConvGetConvList""
      },
      {
        ""apiName"": ""ConvGetConvInfo"",
        ""apiText"": ""获取会话详情"",
        ""apiDesc"": ""获取会话详情"",
        ""scene"": ""ConvGetConvInfo""
      },
      {
        ""apiName"": ""ConvGetTotalUnreadMessageCount"",
        ""apiText"": ""获取会话未读总数"",
        ""apiDesc"": ""获取会话未读总数"",
        ""scene"": ""ConvGetTotalUnreadMessageCount""
      },
      {
        ""apiName"": ""ConvDelete"",
        ""apiText"": ""删除会话"",
        ""apiDesc"": ""删除会话"",
        ""scene"": ""ConvDelete""
      },
      {
        ""apiName"": ""ConvDeleteConversationList"",
        ""apiText"": ""删除会话列表"",
        ""apiDesc"": ""删除会话列表"",
        ""scene"": ""ConvDeleteConversationList""
      },
      {
        ""apiName"": ""ConvCleanConversationUnreadMessageCount"",
        ""apiText"": ""清理会话的未读消息计数"",
        ""apiDesc"": ""清理会话的未读消息计数"",
        ""scene"": ""ConvCleanConversationUnreadMessageCount""
      },
      {
        ""apiName"": ""ConvSetDraft"",
        ""apiText"": ""设置会话为草稿"",
        ""apiDesc"": ""设置会话为草稿"",
        ""scene"": ""ConvSetDraft""
      },
      {
        ""apiName"": ""ConvCancelDraft"",
        ""apiText"": ""取消会话为草稿"",
        ""apiDesc"": ""取消会话为草稿"",
        ""scene"": ""ConvCancelDraft""
      },
      {
        ""apiName"": ""ConvPinConversation"",
        ""apiText"": ""会话置顶"",
        ""apiDesc"": ""会话置顶"",
        ""scene"": ""ConvPinConversation""
      },
      {
        ""apiName"": ""ConvMarkConversation"",
        ""apiText"": ""标记会话"",
        ""apiDesc"": ""标记会话"",
        ""scene"": ""ConvMarkConversation""
      },
      {
        ""apiName"": ""ConvCreateConversationGroup"",
        ""apiText"": ""创建会话分组"",
        ""apiDesc"": ""创建会话分组"",
        ""scene"": ""ConvCreateConversationGroup""
      },
      {
        ""apiName"": ""ConvAddConversationsToGroup"",
        ""apiText"": ""添加会话到一个会话分组"",
        ""apiDesc"": ""添加会话到一个会话分组"",
        ""scene"": ""ConvAddConversationsToGroup""
      },
      {
        ""apiName"": ""ConvGetConversationGroupList"",
        ""apiText"": ""获取会话分组列表"",
        ""apiDesc"": ""获取会话分组列表"",
        ""scene"": ""ConvGetConversationGroupList""
      },
      {
        ""apiName"": ""ConvRenameConversationGroup"",
        ""apiText"": ""重命名会话分组"",
        ""apiDesc"": ""重命名会话分组"",
        ""scene"": ""ConvRenameConversationGroup""
      },
      {
        ""apiName"": ""ConvDeleteConversationsFromGroup"",
        ""apiText"": ""从会话分组中删除多个会话"",
        ""apiDesc"": ""从会话分组中删除多个会话"",
        ""scene"": ""ConvDeleteConversationsFromGroup""
      },
      {
        ""apiName"": ""ConvDeleteConversationGroup"",
        ""apiText"": ""删除会话分组"",
        ""apiDesc"": ""删除会话分组"",
        ""scene"": ""ConvDeleteConversationGroup""
      },
      {
        ""apiName"": ""ConvGetConversationListByFilter"",
        ""apiText"": ""获取会话列表高级接口"",
        ""apiDesc"": ""获取会话列表高级接口"",
        ""scene"": ""ConvGetConversationListByFilter""
      },
      {
        ""apiName"": ""ConvGetUnreadMessageCountByFilter"",
        ""apiText"": ""根据 filter 获取未读总数"",
        ""apiDesc"": ""根据 filter 获取未读总数"",
        ""scene"": ""ConvGetUnreadMessageCountByFilter""
      },
      {
        ""apiName"": ""ConvSubscribeUnreadMessageCountByFilter"",
        ""apiText"": ""注册监听指定 filter 的会话未读总数变化"",
        ""apiDesc"": ""注册监听指定 filter 的会话未读总数变化"",
        ""scene"": ""ConvSubscribeUnreadMessageCountByFilter""
      },
      {
        ""apiName"": ""ConvUnsubscribeUnreadMessageCountByFilter"",
        ""apiText"": ""取消监听指定 filter 的会话未读总数变化"",
        ""apiDesc"": ""取消监听指定 filter 的会话未读总数变化"",
        ""scene"": ""ConvUnsubscribeUnreadMessageCountByFilter""
      },
      {
        ""apiName"": ""ConvSetConversationCustomData"",
        ""apiText"": ""设置会话自定义数据"",
        ""apiDesc"": ""设置会话自定义数据"",
        ""scene"": ""ConvSetConversationCustomData""
      }
    ]
  },
  {
    ""apiManager"": ""V2TimMessageManager"",
    ""managerName"": ""Message Module"",
    ""apis"": [
      {
        ""apiName"": ""MsgGetMsgList"",
        ""apiText"": ""获取历史消息列表"",
        ""apiDesc"": ""获取历史消息列表"",
        ""scene"": ""MsgGetMsgList""
      },
      {
        ""apiName"": ""SendTextMessage"",
        ""apiText"": ""发送文本消息"",
        ""apiDesc"": ""发送文本消息"",
        ""scene"": ""SendTextMessage""
      },
      {
        ""apiName"": ""SendCustomMessage"",
        ""apiText"": ""发送自定义消息"",
        ""apiDesc"": ""发送自定义消息"",
        ""scene"": ""SendCustomMessage""
      },
      {
        ""apiName"": ""SendImageMessage"",
        ""apiText"": ""发送图片消息"",
        ""apiDesc"": ""发送图片消息"",
        ""scene"": ""SendImageMessage""
      },
      {
        ""apiName"": ""SendVideoMessage"",
        ""apiText"": ""发送视频消息"",
        ""apiDesc"": ""发送视频消息"",
        ""scene"": ""SendVideoMessage""
      },
      {
        ""apiName"": ""SendFileMessage"",
        ""apiText"": ""发送文件消息"",
        ""apiDesc"": ""发送文件消息"",
        ""scene"": ""SendFileMessage""
      },
      {
        ""apiName"": ""SendSoundMessage"",
        ""apiText"": ""发送录音消息"",
        ""apiDesc"": ""发送录音消息"",
        ""scene"": ""SendSoundMessage""
      },
      {
        ""apiName"": ""SendTextAtMessage"",
        ""apiText"": ""发送文本At消息"",
        ""apiDesc"": ""发送文本At消息"",
        ""scene"": ""SendTextAtMessage""
      },
      {
        ""apiName"": ""SendLocationMessage"",
        ""apiText"": ""发送地理位置消息"",
        ""apiDesc"": ""发送地理位置消息"",
        ""scene"": ""SendLocationMessage""
      },
      {
        ""apiName"": ""SendFaceMessage"",
        ""apiText"": ""发送表情消息"",
        ""apiDesc"": ""发送表情消息"",
        ""scene"": ""SendFaceMessage""
      },
      {
        ""apiName"": ""SendMergerMessage"",
        ""apiText"": ""发送合并消息"",
        ""apiDesc"": ""发送合并消息"",
        ""scene"": ""SendMergerMessage""
      },
      {
        ""apiName"": ""SendForwardMessage"",
        ""apiText"": ""发送逐条转发消息"",
        ""apiDesc"": ""发送逐条转发消息"",
        ""scene"": ""SendForwardMessage""
      },
      {
        ""apiName"": ""MsgDelete"",
        ""apiText"": ""删除消息"",
        ""apiDesc"": ""删除消息"",
        ""scene"": ""MsgDelete""
      },
      {
        ""apiName"": ""MsgListDelete"",
        ""apiText"": ""消息列表删除"",
        ""apiDesc"": ""消息列表删除"",
        ""scene"": ""MsgListDelete""
      },
      {
        ""apiName"": ""MsgCancelSend"",
        ""apiText"": ""取消消息发送"",
        ""apiDesc"": ""取消消息发送"",
        ""scene"": ""MsgCancelSend""
      },
      {
        ""apiName"": ""MsgFindMessages"",
        ""apiText"": ""从本地查找消息"",
        ""apiDesc"": ""从本地查找消息"",
        ""scene"": ""MsgFindMessages""
      },
      {
        ""apiName"": ""MsgRevoke"",
        ""apiText"": ""撤回消息"",
        ""apiDesc"": ""撤回消息"",
        ""scene"": ""MsgRevoke""
      },
      {
        ""apiName"": ""MsgModifyMessage"",
        ""apiText"": ""消息变更"",
        ""apiDesc"": ""消息变更"",
        ""scene"": ""MsgModifyMessage""
      },
      {
        ""apiName"": ""MsgFindByMsgLocatorList"",
        ""apiText"": ""通过消息定位符查找消息"",
        ""apiDesc"": ""通过消息定位符查找消息"",
        ""scene"": ""MsgFindByMsgLocatorList""
      },
      {
        ""apiName"": ""MsgImportMsgList"",
        ""apiText"": ""导入消息"",
        ""apiDesc"": ""导入消息"",
        ""scene"": ""MsgImportMsgList""
      },
      {
        ""apiName"": ""MsgSaveMsg"",
        ""apiText"": ""保存消息"",
        ""apiDesc"": ""保存消息"",
        ""scene"": ""MsgSaveMsg""
      },
      {
        ""apiName"": ""MsgGetC2CReceiveMessageOpt"",
        ""apiText"": ""获取C2C收消息选项"",
        ""apiDesc"": ""获取C2C收消息选项"",
        ""scene"": ""MsgGetC2CReceiveMessageOpt""
      },
      {
        ""apiName"": ""MsgSetC2CReceiveMessageOpt"",
        ""apiText"": ""设置C2C收消息选项"",
        ""apiDesc"": ""设置C2C收消息选项"",
        ""scene"": ""MsgSetC2CReceiveMessageOpt""
      },
      {
        ""apiName"": ""MsgSetGroupReceiveMessageOpt"",
        ""apiText"": ""设置群收消息选项"",
        ""apiDesc"": ""设置群收消息选项"",
        ""scene"": ""MsgSetGroupReceiveMessageOpt""
      },
      {
        ""apiName"": ""MsgSetAllReceiveMessageOpt"",
        ""apiText"": ""设置all收消息选项"",
        ""apiDesc"": ""设置all收消息选项"",
        ""scene"": ""MsgSetAllReceiveMessageOpt""
      },
      {
        ""apiName"": ""MsgGetAllReceiveMessageOpt"",
        ""apiText"": ""批量拉取消息回应列表"",
        ""apiDesc"": ""批量拉取消息回应列表"",
        ""scene"": ""MsgGetAllReceiveMessageOpt""
      },
      {
        ""apiName"": ""MsgGetMessageReactions"",
        ""apiText"": ""设置all收消息选项"",
        ""apiDesc"": ""设置all收消息选项"",
        ""scene"": ""MsgGetMessageReactions""
      },
      {
        ""apiName"": ""MsgAddMessageReaction"",
        ""apiText"": ""添加消息回应"",
        ""apiDesc"": ""添加消息回应"",
        ""scene"": ""MsgAddMessageReaction""
      },
      {
        ""apiName"": ""MsgDownloadElemToPath"",
        ""apiText"": ""下载多媒体消息"",
        ""apiDesc"": ""下载多媒体消息"",
        ""scene"": ""MsgDownloadElemToPath""
      },
      {
        ""apiName"": ""MsgDownloadMergerMessage"",
        ""apiText"": ""下载合并消息"",
        ""apiDesc"": ""下载合并消息"",
        ""scene"": ""MsgDownloadMergerMessage""
      },
      {
        ""apiName"": ""MsgBatchSend"",
        ""apiText"": ""批量发送消息"",
        ""apiDesc"": ""批量发送消息"",
        ""scene"": ""MsgBatchSend""
      },
      {
        ""apiName"": ""MsgSearchLocalMessages"",
        ""apiText"": ""搜索本地消息"",
        ""apiDesc"": ""搜索本地消息"",
        ""scene"": ""MsgSearchLocalMessages""
      },
      {
        ""apiName"": ""MsgSetLocalCustomData"",
        ""apiText"": ""设置消息本地数据"",
        ""apiDesc"": ""设置消息本地数据"",
        ""scene"": ""MsgSetLocalCustomData""
      },
      {
        ""apiName"": ""MsgReportReaded (C2C)"",
        ""apiText"": ""标记C2C会话已读"",
        ""apiDesc"": ""标记C2C会话已读"",
        ""scene"": ""MsgReportReaded""
      },
      {
        ""apiName"": ""MsgSendMessageReadReceipts (Group)"",
        ""apiText"": ""标记Group会话已读"",
        ""apiDesc"": ""标记Group会话已读"",
        ""scene"": ""MsgSendMessageReadReceipts""
      },
      {
        ""apiName"": ""MsgMarkAllMessageAsRead"",
        ""apiText"": ""标记所有消息为已读"",
        ""apiDesc"": ""标记所有消息为已读"",
        ""scene"": ""MsgMarkAllMessageAsRead""
      },
      {
        ""apiName"": ""MsgGetMessageReadReceipts"",
        ""apiText"": ""获取群已读回执"",
        ""apiDesc"": ""获取群已读回执"",
        ""scene"": ""MsgGetMessageReadReceipts""
      },
      {
        ""apiName"": ""GetMsgGroupMessageReadMemberList"",
        ""apiText"": ""获取群已读（未读）成员列表"",
        ""apiDesc"": ""获取群已读（未读）成员列表"",
        ""scene"": ""GetMsgGroupMessageReadMemberList""
      },
      {
        ""apiName"": ""MsgGetMessageExtensions"",
        ""apiText"": ""获取消息扩展"",
        ""apiDesc"": ""获取消息扩展"",
        ""scene"": ""MsgGetMessageExtensions""
      },
      {
        ""apiName"": ""MsgSetMessageExtensions"",
        ""apiText"": ""设置消息扩展"",
        ""apiDesc"": ""设置消息扩展"",
        ""scene"": ""MsgSetMessageExtensions""
      },
      {
        ""apiName"": ""MsgDeleteMessageExtensions"",
        ""apiText"": ""删除消息扩展"",
        ""apiDesc"": ""删除消息扩展"",
        ""scene"": ""MsgDeleteMessageExtensions""
      },
      {
        ""apiName"": ""MsgTranslateText"",
        ""apiText"": ""翻译文本消息"",
        ""apiDesc"": ""翻译文本消息"",
        ""scene"": ""MsgTranslateText""
      }
    ]
  },
  {
    ""apiManager"": ""V2TimMessageManager"",
    ""managerName"": ""FriendShip Module"",
    ""apis"": [
      {
        ""apiName"": ""FriendshipGetFriendProfileList"",
        ""apiText"": ""获取好友列表"",
        ""apiDesc"": ""获取好友列表"",
        ""scene"": ""FriendshipGetFriendProfileList""
      },
      {
        ""apiName"": ""FriendshipGetFriendsInfo"",
        ""apiText"": ""获取好友信息"",
        ""apiDesc"": ""获取好友信息"",
        ""scene"": ""FriendshipGetFriendsInfo""
      },
      {
        ""apiName"": ""FriendshipAddFriend"",
        ""apiText"": ""添加好友"",
        ""apiDesc"": ""添加好友"",
        ""scene"": ""FriendshipAddFriend""
      },
      {
        ""apiName"": ""FriendshipDeleteFriend"",
        ""apiText"": ""删除好友"",
        ""apiDesc"": ""删除好友"",
        ""scene"": ""FriendshipDeleteFriend""
      },
      {
        ""apiName"": ""FriendshipCheckFriendType"",
        ""apiText"": ""检测好友"",
        ""apiDesc"": ""检测好友"",
        ""scene"": ""FriendshipCheckFriendType""
      },
      {
        ""apiName"": ""FriendshipGetPendencyList"",
        ""apiText"": ""获取好友申请未决"",
        ""apiDesc"": ""获取好友申请未决"",
        ""scene"": ""FriendshipGetPendencyList""
      },
      {
        ""apiName"": ""FriendshipHandleFriendAddRequest"",
        ""apiText"": ""处理好友请求"",
        ""apiDesc"": ""处理好友请求"",
        ""scene"": ""FriendshipHandleFriendAddRequest""
      },
      {
        ""apiName"": ""FriendshipReportPendencyReaded"",
        ""apiText"": ""上报好友申请未决已读"",
        ""apiDesc"": ""上报好友申请未决已读"",
        ""scene"": ""FriendshipReportPendencyReaded""
      },
      {
        ""apiName"": ""FriendshipDeletePendency"",
        ""apiText"": ""删除好友申请未决"",
        ""apiDesc"": ""删除好友申请未决"",
        ""scene"": ""FriendshipDeletePendency""
      },
      {
        ""apiName"": ""FriendshipModifyFriendProfile"",
        ""apiText"": ""修改好友信息"",
        ""apiDesc"": ""修改好友信息"",
        ""scene"": ""FriendshipModifyFriendProfile""
      },
      {
        ""apiName"": ""FriendshipGetBlackList"",
        ""apiText"": ""获取黑名单列表"",
        ""apiDesc"": ""获取黑名单列表"",
        ""scene"": ""FriendshipGetBlackList""
      },
      {
        ""apiName"": ""FriendshipAddToBlackList"",
        ""apiText"": ""添加黑名单"",
        ""apiDesc"": ""添加黑名单"",
        ""scene"": ""FriendshipAddToBlackList""
      },
      {
        ""apiName"": ""FriendshipDeleteFromBlackList"",
        ""apiText"": ""从黑名单删除"",
        ""apiDesc"": ""从黑名单删除"",
        ""scene"": ""FriendshipDeleteFromBlackList""
      },
      {
        ""apiName"": ""FriendshipCreateFriendGroup"",
        ""apiText"": ""创建好友分组"",
        ""apiDesc"": ""创建好友分组"",
        ""scene"": ""FriendshipCreateFriendGroup""
      },
      {
        ""apiName"": ""FriendshipGetFriendGroupList"",
        ""apiText"": ""获取好友分组列表"",
        ""apiDesc"": ""获取好友分组列表"",
        ""scene"": ""FriendshipGetFriendGroupList""
      },
      {
        ""apiName"": ""FriendshipModifyFriendGroup"",
        ""apiText"": ""修改好友分组列表"",
        ""apiDesc"": ""修改好友分组列表"",
        ""scene"": ""FriendshipModifyFriendGroup""
      },
      {
        ""apiName"": ""FriendshipDeleteFriendGroup"",
        ""apiText"": ""删除好友分组列表"",
        ""apiDesc"": ""删除好友分组列表"",
        ""scene"": ""FriendshipDeleteFriendGroup""
      },
      {
        ""apiName"": ""FriendshipSearchFriends"",
        ""apiText"": ""搜索好友"",
        ""apiDesc"": ""搜索好友"",
        ""scene"": ""FriendshipSearchFriends""
      }
    ]
  },
  {
    ""apiManager"": ""V2TimMessageManager"",
    ""managerName"": ""Group Module"",
    ""apis"": [
      {
        ""apiName"": ""GroupGetJoinedGroupList"",
        ""apiText"": ""获取加群列表"",
        ""apiDesc"": ""获取加群列表"",
        ""scene"": ""GroupGetJoinedGroupList""
      },
      {
        ""apiName"": ""GroupGetGroupInfoList"",
        ""apiText"": ""获取群信息"",
        ""apiDesc"": ""获取群信息"",
        ""scene"": ""GroupGetGroupInfoList""
      },
      {
        ""apiName"": ""GroupGetOnlineMemberCount"",
        ""apiText"": ""获取群在线人数"",
        ""apiDesc"": ""获取群在线人数"",
        ""scene"": ""GroupGetOnlineMemberCount""
      },
      {
        ""apiName"": ""GroupCreate"",
        ""apiText"": ""创建群组"",
        ""apiDesc"": ""创建群组"",
        ""scene"": ""GroupCreate""
      },
      {
        ""apiName"": ""GroupJoin"",
        ""apiText"": ""加入群组"",
        ""apiDesc"": ""加入群组"",
        ""scene"": ""GroupJoin""
      },
      {
        ""apiName"": ""GroupDelete"",
        ""apiText"": ""退出（解散）群组"",
        ""apiDesc"": ""退出（解散）群组"",
        ""scene"": ""GroupDelete""
      },
      {
        ""apiName"": ""GroupInviteMember"",
        ""apiText"": ""邀请好友进群"",
        ""apiDesc"": ""邀请好友进群"",
        ""scene"": ""GroupInviteMember""
      },
      {
        ""apiName"": ""GroupDeleteMember"",
        ""apiText"": ""踢人出群"",
        ""apiDesc"": ""踢人出群"",
        ""scene"": ""GroupDeleteMember""
      },
      {
        ""apiName"": ""GroupQuit"",
        ""apiText"": ""退出群组"",
        ""apiDesc"": ""退出群组"",
        ""scene"": ""GroupQuit""
      },
      {
        ""apiName"": ""GroupModifyGroupInfo"",
        ""apiText"": ""修改群信息"",
        ""apiDesc"": ""修改群信息"",
        ""scene"": ""GroupModifyGroupInfo""
      },
      {
        ""apiName"": ""GroupGetMemberInfoList"",
        ""apiText"": ""获取群成员信息"",
        ""apiDesc"": ""获取群成员信息"",
        ""scene"": ""GroupGetMemberInfoList""
      },
      {
        ""apiName"": ""GroupModifyMemberInfo"",
        ""apiText"": ""修改群成员信息"",
        ""apiDesc"": ""修改群成员信息"",
        ""scene"": ""GroupModifyMemberInfo""
      },
      {
        ""apiName"": ""GroupGetPendencyList"",
        ""apiText"": ""获取群未决信息列表"",
        ""apiDesc"": ""获取群未决信息列表"",
        ""scene"": ""GroupGetPendencyList""
      },
      {
        ""apiName"": ""GroupHandlePendency"",
        ""apiText"": ""处理群未决信息"",
        ""apiDesc"": ""处理群未决信息"",
        ""scene"": ""GroupHandlePendency""
      },
      {
        ""apiName"": ""GroupReportPendencyReaded"",
        ""apiText"": ""上报群未决信息已读"",
        ""apiDesc"": ""上报群未决信息已读"",
        ""scene"": ""GroupReportPendencyReaded""
      },
      {
        ""apiName"": ""GroupSearchGroupMembers"",
        ""apiText"": ""搜索群成员"",
        ""apiDesc"": ""搜索群成员"",
        ""scene"": ""GroupSearchGroupMembers""
      },
      {
        ""apiName"": ""GroupSearchGroups (Flagship only)"",
        ""apiText"": ""搜索群资料 (需旗舰版套餐)"",
        ""apiDesc"": ""搜索群资料 (需旗舰版套餐)"",
        ""scene"": ""GroupSearchGroups""
      },
      {
        ""apiName"": ""GroupInitGroupAttributes"",
        ""apiText"": ""初始化群自定义属性"",
        ""apiDesc"": ""初始化群自定义属性"",
        ""scene"": ""GroupInitGroupAttributes""
      },
      {
        ""apiName"": ""GroupSetGroupAttributes"",
        ""apiText"": ""设置群属性"",
        ""apiDesc"": ""设置群属性"",
        ""scene"": ""GroupSetGroupAttributes""
      },
      {
        ""apiName"": ""GroupDeleteGroupAttributes"",
        ""apiText"": ""删除群自定义属性"",
        ""apiDesc"": ""删除群自定义属性"",
        ""scene"": ""GroupDeleteGroupAttributes""
      },
      {
        ""apiName"": ""GroupGetGroupAttributes"",
        ""apiText"": ""获取群指定属性"",
        ""apiDesc"": ""获取群指定属性"",
        ""scene"": ""GroupGetGroupAttributes""
      },
      {
        ""apiName"": ""GroupGetJoinedCommunityList"",
        ""apiText"": ""获取当前用户已经加入的支持话题的社群列表"",
        ""apiDesc"": ""获取当前用户已经加入的支持话题的社群列表"",
        ""scene"": ""GroupGetJoinedCommunityList""
      },
      {
        ""apiName"": ""GroupGetTopicInfoList"",
        ""apiText"": ""获取话题列表"",
        ""apiDesc"": ""获取话题列表"",
        ""scene"": ""GroupGetTopicInfoList""
      },
      {
        ""apiName"": ""GroupCreateTopicInCommunity"",
        ""apiText"": ""创建话题"",
        ""apiDesc"": ""创建话题"",
        ""scene"": ""GroupCreateTopicInCommunity""
      },
      {
        ""apiName"": ""GroupDeleteTopicFromCommunity"",
        ""apiText"": ""删除话题"",
        ""apiDesc"": ""删除话题"",
        ""scene"": ""GroupDeleteTopicFromCommunity""
      },
      {
        ""apiName"": ""GroupSetTopicInfo"",
        ""apiText"": ""修改话题信息"",
        ""apiDesc"": ""修改话题信息"",
        ""scene"": ""GroupSetTopicInfo""
      },
      {
        ""apiName"": ""GroupMarkGroupMemberList"",
        ""apiText"": ""标记群成员"",
        ""apiDesc"": ""标记群成员"",
        ""scene"": ""GroupMarkGroupMemberList""
      },
      {
        ""apiName"": ""GroupSetGroupCounters"",
        ""apiText"": ""设置群计数器"",
        ""apiDesc"": ""设置群计数器"",
        ""scene"": ""GroupSetGroupCounters""
      },
      {
        ""apiName"": ""GroupGetGroupCounters"",
        ""apiText"": ""获取群计数器"",
        ""apiDesc"": ""获取群计数器"",
        ""scene"": ""GroupGetGroupCounters""
      },
      {
        ""apiName"": ""GroupIncreaseGroupCounter"",
        ""apiText"": ""递增群计数器"",
        ""apiDesc"": ""递增群计数器"",
        ""scene"": ""GroupIncreaseGroupCounter""
      },
      {
        ""apiName"": ""GroupDecreaseGroupCounter"",
        ""apiText"": ""递减群计数器"",
        ""apiDesc"": ""递减群计数器"",
        ""scene"": ""GroupDecreaseGroupCounter""
      }
    ]
  },
]
";
  }
}