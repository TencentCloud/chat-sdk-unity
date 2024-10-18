namespace com.tencent.im.unity.demo.config.EventListenerList
{
  public static class EventListenerList
  {
    public static string EventListenerListStr = @"[
      {
        ""eventName"": ""AddRecvNewMsgCallback"",
        ""eventText"": ""收到新消息回调"",
        ""eventDesc"": ""注册收到新消息回调""
      },
      {
        ""eventName"": ""SetMsgReactionsChangedCallback"",
        ""eventText"": ""设置消息回应信息更新的回调"",
        ""eventDesc"": ""设置消息回应信息更新的回调""
      },
      {
        ""eventName"": ""SetMsgAllMessageReceiveOptionCallback"",
        ""eventText"": ""设置全局消息接收选项的回调"",
        ""eventDesc"": ""设置全局消息接收选项的回调""
      },
      {
        ""eventName"": ""SetMsgReadedReceiptCallback"",
        ""eventText"": ""消息已读回执回调"",
        ""eventDesc"": ""设置消息已读回执回调""
      },
      {
        ""eventName"": ""SetMsgRevokeCallback"",
        ""eventText"": ""接收的消息被撤回回调"",
        ""eventDesc"": ""设置接收的消息被撤回回调""
      },
      {
        ""eventName"": ""SetGroupTipsEventCallback"",
        ""eventText"": ""群组系统消息回调"",
        ""eventDesc"": ""设置群组系统消息回调""
      },
      {
        ""eventName"": ""SetMsgElemUploadProgressCallback"",
        ""eventText"": ""消息内元素相关文件上传进度回调"",
        ""eventDesc"": ""设置消息内元素相关文件上传进度回调""
      },
      {
        ""eventName"": ""SetGroupAttributeChangedCallback"",
        ""eventText"": ""群组属性变更回调"",
        ""eventDesc"": ""设置群组属性变更回调""
      },
      {
        ""eventName"": ""SetConvEventCallback"",
        ""eventText"": ""会话事件回调"",
        ""eventDesc"": ""设置会话事件回调""
      },
      {
        ""eventName"": ""SetConvTotalUnreadMessageCountChangedCallback"",
        ""eventText"": ""会话未读消息总数变更的回调"",
        ""eventDesc"": ""设置会话未读消息总数变更的回调""
      },
      {
        ""eventName"": ""SetNetworkStatusListenerCallback"",
        ""eventText"": ""网络连接状态监听回调"",
        ""eventDesc"": ""设置网络连接状态监听回调""
      },
      {
        ""eventName"": ""SetKickedOfflineCallback"",
        ""eventText"": ""被踢下线通知回调"",
        ""eventDesc"": ""设置被踢下线通知回调""
      },
      {
        ""eventName"": ""SetUserSigExpiredCallback"",
        ""eventText"": ""票据过期回调"",
        ""eventDesc"": ""设置票据过期回调""
      },
      {
        ""eventName"": ""SetOnAddFriendCallback"",
        ""eventText"": ""添加好友的回调"",
        ""eventDesc"": ""设置添加好友的回调""
      },
      {
        ""eventName"": ""SetOnDeleteFriendCallback"",
        ""eventText"": ""删除好友的回调"",
        ""eventDesc"": ""设置删除好友的回调""
      },
      {
        ""eventName"": ""SetUpdateFriendProfileCallback"",
        ""eventText"": ""更新好友资料的回调"",
        ""eventDesc"": ""设置更新好友资料的回调""
      },
      {
        ""eventName"": ""SetFriendAddRequestCallback"",
        ""eventText"": ""好友添加请求的回调"",
        ""eventDesc"": ""设置好友添加请求的回调""
      },
      {
        ""eventName"": ""SetFriendApplicationListDeletedCallback"",
        ""eventText"": ""好友申请被删除的回调"",
        ""eventDesc"": ""设置好友申请被删除的回调""
      },
      {
        ""eventName"": ""SetFriendApplicationListReadCallback"",
        ""eventText"": ""好友申请已读的回调"",
        ""eventDesc"": ""设置好友申请已读的回调""
      },
      {
        ""eventName"": ""SetFriendBlackListAddedCallback"",
        ""eventText"": ""黑名单新增的回调"",
        ""eventDesc"": ""设置黑名单新增的回调""
      },
      {
        ""eventName"": ""SetFriendBlackListDeletedCallback"",
        ""eventText"": ""黑名单删除的回调"",
        ""eventDesc"": ""设置黑名单删除的回调""
      },
      {
        ""eventName"": ""SetLogCallback"",
        ""eventText"": ""日志回调"",
        ""eventDesc"": ""设置日志回调""
      },
      {
        ""eventName"": ""SetMsgUpdateCallback"",
        ""eventText"": ""消息在云端被修改后回传回来的消息更新通知回调"",
        ""eventDesc"": ""设置消息在云端被修改后回传回来的消息更新通知回调""
      },
      {
        ""eventName"": ""SetGroupTopicCreatedCallback"",
        ""eventText"": ""话题创建回调"",
        ""eventDesc"": ""设置话题创建回调""
      },
      {
        ""eventName"": ""SetGroupTopicDeletedCallback"",
        ""eventText"": ""话题被删除回调"",
        ""eventDesc"": ""设置话题被删除回调""
      },
      {
        ""eventName"": ""SetGroupTopicChangedCallback"",
        ""eventText"": ""话题更新回调"",
        ""eventDesc"": ""设置话题更新回调""
      },
      {
        ""eventName"": ""SetSelfInfoUpdatedCallback"",
        ""eventText"": ""当前用户的资料发生更新时的回调"",
        ""eventDesc"": ""设置当前用户的资料发生更新时的回调""
      },
      {
        ""eventName"": ""SetUserStatusChangedCallback"",
        ""eventText"": ""用户状态变更通知回调"",
        ""eventDesc"": ""设置用户状态变更通知回调""
      },
      {
        ""eventName"": ""SetUserInfoChangedCallback"",
        ""eventText"": ""用户资料变更回调"",
        ""eventDesc"": ""设置用户资料变更回调""
      },
      {
        ""eventName"": ""SetMsgExtensionsChangedCallback"",
        ""eventText"": ""消息扩展信息更新的回调"",
        ""eventDesc"": ""设置消息扩展信息更新的回调""
      },
      {
        ""eventName"": ""SetMsgExtensionsDeletedCallback"",
        ""eventText"": ""消息扩展信息删除的回调"",
        ""eventDesc"": ""设置消息扩展信息删除的回调""
      },
      {
        ""eventName"": ""SetMsgGroupPinnedMessageChangedCallback"",
        ""eventText"": ""群置顶消息变更的回调"",
        ""eventDesc"": ""设置群置顶消息变更的回调""
      },
      {
        ""eventName"": ""SetFriendGroupCreatedCallback"",
        ""eventText"": ""好友分组创建通知回调"",
        ""eventDesc"": ""设置好友分组创建通知回调""
      },
      {
        ""eventName"": ""SetFriendGroupDeletedCallback"",
        ""eventText"": ""好友分组删除通知回调"",
        ""eventDesc"": ""设置好友分组删除通知回调""
      },
      {
        ""eventName"": ""SetFriendGroupNameChangedCallback"",
        ""eventText"": ""好友分组名称变更通知回调"",
        ""eventDesc"": ""设置好友分组名称变更通知回调""
      },
      {
        ""eventName"": ""SetFriendsAddedToGroupCallback"",
        ""eventText"": ""好友添加到分组通知回调"",
        ""eventDesc"": ""设置好友添加到分组通知回调""
      },
      {
        ""eventName"": ""SetFriendsDeletedFromGroupCallback"",
        ""eventText"": ""好友从分组删除通知回调"",
        ""eventDesc"": ""设置好友从分组删除通知回调""
      },
      {
        ""eventName"": ""SetOfficialAccountSubscribedCallback"",
        ""eventText"": ""订阅公众号通知回调"",
        ""eventDesc"": ""设置订阅公众号通知回调""
      },
      {
        ""eventName"": ""SetOfficialAccountUnsubscribedCallback"",
        ""eventText"": ""取消订阅公众号通知回调"",
        ""eventDesc"": ""设置取消订阅公众号通知回调""
      },
      {
        ""eventName"": ""SetOfficialAccountDeletedCallback"",
        ""eventText"": ""删除公众号通知回调"",
        ""eventDesc"": ""设置删除公众号通知回调""
      },
      {
        ""eventName"": ""SetOfficialAccountInfoChangedCallback"",
        ""eventText"": ""公众号信息变更通知回调"",
        ""eventDesc"": ""设置公众号信息变更通知回调""
      },
      {
        ""eventName"": ""SetMyFollowingListChangedCallback"",
        ""eventText"": ""我的关注列表变更通知回调"",
        ""eventDesc"": ""设置我的关注列表变更通知回调""
      },
      {
        ""eventName"": ""SetMyFollowersListChangedCallback"",
        ""eventText"": ""我的粉丝列表变更通知回调"",
        ""eventDesc"": ""设置我的粉丝列表变更通知回调""
      },
      {
        ""eventName"": ""SetMutualFollowersListChangedCallback"",
        ""eventText"": ""我的互关列表变更通知回调"",
        ""eventDesc"": ""设置我的互关列表变更通知回调""
      },
      {
        ""eventName"": ""SetSignalingReceiveNewInvitationCallback"",
        ""eventText"": ""收到信令邀请通知回调"",
        ""eventDesc"": ""设置收到信令邀请通知回调""
      },
      {
        ""eventName"": ""SetSignalingInvitationCancelledCallback"",
        ""eventText"": ""信令邀请被取消通知回调"",
        ""eventDesc"": ""设置信令邀请被取消通知回调""
      },
      {
        ""eventName"": ""SetSignalingInviteeAcceptedCallback"",
        ""eventText"": ""信令邀请被接受通知回调"",
        ""eventDesc"": ""设置信令邀请被接受通知回调""
      },
      {
        ""eventName"": ""SetSignalingInviteeRejectedCallback"",
        ""eventText"": ""信令邀请被拒绝通知回调"",
        ""eventDesc"": ""设置信令邀请被拒绝通知回调""
      },
      {
        ""eventName"": ""SetSignalingInvitationTimeoutCallback"",
        ""eventText"": ""信令邀请超时通知回调"",
        ""eventDesc"": ""设置信令邀请超时通知回调""
      },
      {
        ""eventName"": ""SetSignalingInvitationModifiedCallback"",
        ""eventText"": ""信令邀请被修改通知回调"",
        ""eventDesc"": ""设置信令邀请被修改通知回调""
      },
      {
        ""eventName"": ""SetCommunityCreateTopicCallback"",
        ""eventText"": ""话题创建通知回调"",
        ""eventDesc"": ""设置话题创建通知回调""
      },
      {
        ""eventName"": ""SetCommunityDeleteTopicCallback"",
        ""eventText"": ""话题删除通知回调"",
        ""eventDesc"": ""设置话题删除通知回调""
      },
      {
        ""eventName"": ""SetCommunityChangeTopicInfoCallback"",
        ""eventText"": ""话题变更通知回调"",
        ""eventDesc"": ""设置话题变更通知回调""
      },
      {
        ""eventName"": ""SetCommunityReceiveTopicRESTCustomDataCallback"",
        ""eventText"": ""话题自定义数据通知回调"",
        ""eventDesc"": ""设置话题自定义数据通知回调""
      },
      {
        ""eventName"": ""SetCommunityCreatePermissionGroupCallback"",
        ""eventText"": ""权限组创建通知回调"",
        ""eventDesc"": ""设置权限组创建通知回调""
      },
      {
        ""eventName"": ""SetCommunityDeletePermissionGroupCallback"",
        ""eventText"": ""权限组删除通知回调"",
        ""eventDesc"": ""设置权限组删除通知回调""
      },
      {
        ""eventName"": ""SetCommunityChangePermissionGroupInfoCallback"",
        ""eventText"": ""权限组变更通知回调"",
        ""eventDesc"": ""设置权限组变更通知回调""
      },
      {
        ""eventName"": ""SetCommunityAddMembersToPermissionGroupCallback"",
        ""eventText"": ""向权限组添加成员通知回调"",
        ""eventDesc"": ""设置向权限组添加成员通知回调""
      },
      {
        ""eventName"": ""SetCommunityRemoveMembersFromPermissionGroupCallback"",
        ""eventText"": ""从权限组删除成员通知回调"",
        ""eventDesc"": ""设置从权限组删除成员通知回调""
      },
      {
        ""eventName"": ""SetCommunityAddTopicPermissionCallback"",
        ""eventText"": ""添加话题权限的通知回调"",
        ""eventDesc"": ""设置添加话题权限的通知回调""
      },
      {
        ""eventName"": ""SetCommunityDeleteTopicPermissionCallback"",
        ""eventText"": ""删除话题权限的通知回调"",
        ""eventDesc"": ""设置删除话题权限的通知回调""
      },
      {
        ""eventName"": ""SetCommunityModifyTopicPermissionCallback"",
        ""eventText"": ""修改话题权限的通知回调"",
        ""eventDesc"": ""设置修改话题权限的通知回调""
      },
      {
        ""eventName"": ""SetExperimentalNotifyCallback"",
        ""eventText"": ""实验性通知的回调"",
        ""eventDesc"": ""设置实验性通知的回调""
      },
    ]";
  }
}