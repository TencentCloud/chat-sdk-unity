using Newtonsoft.Json.Utilities;
using UnityEngine;
using com.tencent.imsdk.unity.types;
using Newtonsoft.Json.Linq;

// 此Helper类用于解决 IL2CPP 平台下的 AOT compile error，请参考
// https://github.com/jilleJr/Newtonsoft.Json-for-Unity/wiki/What-even-is-AOT
// https://github.com/jilleJr/Newtonsoft.Json-for-Unity/wiki/Fix-AOT-compilation-errors

public class AotTypeEnforcer : MonoBehaviour
{
  public void Awake()
  {
    AotHelper.EnsureDictionary<string, JToken>();
    AotHelper.EnsureType<SdkConfig>();
    AotHelper.EnsureType<ConvParam>();
    AotHelper.EnsureType<UserProfileCustemStringInfo>();
    AotHelper.EnsureType<UserProfile>();
    AotHelper.EnsureType<Message>();
    AotHelper.EnsureType<OfflinePushConfig>();
    AotHelper.EnsureType<IOSOfflinePushConfig>();
    AotHelper.EnsureType<AndroidOfflinePushConfig>();
    AotHelper.EnsureType<Elem>();
    AotHelper.EnsureType<UserProfileItem>();
    AotHelper.EnsureType<GroupMemberInfo>();
    AotHelper.EnsureType<GroupTipGroupChangeInfo>();
    AotHelper.EnsureType<GroupMemberInfoCustemString>();
    AotHelper.EnsureType<GroupTipMemberChangeInfo>();
    AotHelper.EnsureType<DraftParam>();
    AotHelper.EnsureType<MsgLocator>();
    AotHelper.EnsureType<MsgGetMsgListParam>();
    AotHelper.EnsureType<MsgDeleteParam>();
    AotHelper.EnsureType<DownloadElemParam>();
    AotHelper.EnsureType<MsgBatchSendParam>();
    AotHelper.EnsureType<MessageSearchParam>();
    AotHelper.EnsureType<CreateGroupParam>();
    AotHelper.EnsureType<GroupInfoCustemString>();
    AotHelper.EnsureType<GroupInviteMemberParam>();
    AotHelper.EnsureType<GroupDeleteMemberParam>();
    AotHelper.EnsureType<GroupModifyInfoParam>();
    AotHelper.EnsureType<OfflinePushToken>();
    AotHelper.EnsureType<GroupGetMemberInfoListParam>();
    AotHelper.EnsureType<GroupMemberGetInfoOption>();
    AotHelper.EnsureType<GroupModifyMemberInfoParam>();
    AotHelper.EnsureType<GroupPendencyOption>();
    AotHelper.EnsureType<GroupHandlePendencyParam>();
    AotHelper.EnsureType<GroupPendency>();
    AotHelper.EnsureType<GroupSearchParam>();
    AotHelper.EnsureType<GroupMemberSearchParam>();
    AotHelper.EnsureType<GroupAttributes>();
    AotHelper.EnsureType<FriendShipGetProfileListParam>();
    AotHelper.EnsureType<FriendshipAddFriendParam>();
    AotHelper.EnsureType<FriendResponse>();
    AotHelper.EnsureType<FriendshipModifyFriendProfileParam>();
    AotHelper.EnsureType<FriendProfileItem>();
    AotHelper.EnsureType<FriendProfileCustemStringInfo>();
    AotHelper.EnsureType<FriendshipDeleteFriendParam>();
    AotHelper.EnsureType<FriendshipCheckFriendTypeParam>();
    AotHelper.EnsureType<FriendGroupInfo>();
    AotHelper.EnsureType<CreateFriendGroupInfo>();
    AotHelper.EnsureType<FriendshipModifyFriendGroupParam>();
    AotHelper.EnsureType<FriendshipGetPendencyListParam>();
    AotHelper.EnsureType<FriendshipDeletePendencyParam>();
    AotHelper.EnsureType<FriendSearchParam>();
    AotHelper.EnsureType<MessageReceipt>();
    AotHelper.EnsureType<GroupTipsElem>();
    AotHelper.EnsureType<ConvInfo>();
    AotHelper.EnsureType<Draft>();
    AotHelper.EnsureType<GroupAtInfo>();
    AotHelper.EnsureType<FriendAddPendency>();
    AotHelper.EnsureType<FriendProfile>();
    AotHelper.EnsureType<SetConfig>();
    AotHelper.EnsureType<HttpProxyInfo>();
    AotHelper.EnsureType<Socks5ProxyInfo>();
    AotHelper.EnsureType<UserConfig>();
    AotHelper.EnsureType<GroupGetInfoOption>();
    AotHelper.EnsureType<SSODataParam>();
    AotHelper.EnsureType<ServerAddress>();
    AotHelper.EnsureType<CustomServerInfo>();
    AotHelper.EnsureType<SM4GCMCallbackParam>();
    AotHelper.EnsureType<CosSaveRegionForConversationParam>();
    AotHelper.EnsureType<ExperimentalAPIReqeustParam>();
    AotHelper.EnsureType<GetTotalUnreadNumberResult>();
    AotHelper.EnsureType<GetC2CRecvMsgOptResult>();
    AotHelper.EnsureType<CreateGroupResult>();
    AotHelper.EnsureType<GroupInviteMemberResult>();
    AotHelper.EnsureType<GroupDeleteMemberResult>();
    AotHelper.EnsureType<GroupBaseInfo>();
    AotHelper.EnsureType<GroupSelfInfo>();
    AotHelper.EnsureType<GetGroupInfoResult>();
    AotHelper.EnsureType<GroupDetailInfo>();
    AotHelper.EnsureType<GroupPendencyResult>();
    AotHelper.EnsureType<GroupGetOnlineMemberCountResult>();
    AotHelper.EnsureType<FriendResult>();
    AotHelper.EnsureType<FriendshipCheckFriendTypeResult>();
    AotHelper.EnsureType<PendencyPage>();
    AotHelper.EnsureType<FriendAddPendencyInfo>();
    AotHelper.EnsureType<FriendInfoGetResult>();
    AotHelper.EnsureType<MessageSearchResult>();
    AotHelper.EnsureType<MessageSearchResultItem>();
    AotHelper.EnsureType<MsgBatchSendResult>();
    AotHelper.EnsureType<MsgDownloadElemResult>();
    AotHelper.EnsureType<ReponseInfo>();
    AotHelper.EnsureType<UserInfo>();
    AotHelper.EnsureType<SSODataRes>();
    AotHelper.EnsureType<GroupGetMemberInfoListResult>();
    AotHelper.EnsureType<GroupTopicInfo>();
    AotHelper.EnsureType<GroupInfo>();
    AotHelper.EnsureType<GroupTopicOperationResult>();
    AotHelper.EnsureType<UserStatus>();

    AotHelper.EnsureList<string>();
    AotHelper.EnsureList<ulong>();
    AotHelper.EnsureList<SdkConfig>();
    AotHelper.EnsureList<ConvParam>();
    AotHelper.EnsureList<UserProfileCustemStringInfo>();
    AotHelper.EnsureList<UserProfile>();
    AotHelper.EnsureList<Message>();
    AotHelper.EnsureList<OfflinePushConfig>();
    AotHelper.EnsureList<IOSOfflinePushConfig>();
    AotHelper.EnsureList<AndroidOfflinePushConfig>();
    AotHelper.EnsureList<Elem>();
    AotHelper.EnsureList<UserProfileItem>();
    AotHelper.EnsureList<GroupMemberInfo>();
    AotHelper.EnsureList<GroupTipGroupChangeInfo>();
    AotHelper.EnsureList<GroupMemberInfoCustemString>();
    AotHelper.EnsureList<GroupTipMemberChangeInfo>();
    AotHelper.EnsureList<DraftParam>();
    AotHelper.EnsureList<MsgLocator>();
    AotHelper.EnsureList<MsgGetMsgListParam>();
    AotHelper.EnsureList<MsgDeleteParam>();
    AotHelper.EnsureList<DownloadElemParam>();
    AotHelper.EnsureList<MsgBatchSendParam>();
    AotHelper.EnsureList<MessageSearchParam>();
    AotHelper.EnsureList<CreateGroupParam>();
    AotHelper.EnsureList<GroupInfoCustemString>();
    AotHelper.EnsureList<GroupInviteMemberParam>();
    AotHelper.EnsureList<GroupDeleteMemberParam>();
    AotHelper.EnsureList<GroupModifyInfoParam>();
    AotHelper.EnsureList<OfflinePushToken>();
    AotHelper.EnsureList<GroupGetMemberInfoListParam>();
    AotHelper.EnsureList<GroupMemberGetInfoOption>();
    AotHelper.EnsureList<GroupModifyMemberInfoParam>();
    AotHelper.EnsureList<GroupPendencyOption>();
    AotHelper.EnsureList<GroupHandlePendencyParam>();
    AotHelper.EnsureList<GroupPendency>();
    AotHelper.EnsureList<GroupSearchParam>();
    AotHelper.EnsureList<GroupMemberSearchParam>();
    AotHelper.EnsureList<GroupAttributes>();
    AotHelper.EnsureList<FriendShipGetProfileListParam>();
    AotHelper.EnsureList<FriendshipAddFriendParam>();
    AotHelper.EnsureList<FriendResponse>();
    AotHelper.EnsureList<FriendshipModifyFriendProfileParam>();
    AotHelper.EnsureList<FriendProfileItem>();
    AotHelper.EnsureList<FriendProfileCustemStringInfo>();
    AotHelper.EnsureList<FriendshipDeleteFriendParam>();
    AotHelper.EnsureList<FriendshipCheckFriendTypeParam>();
    AotHelper.EnsureList<FriendGroupInfo>();
    AotHelper.EnsureList<CreateFriendGroupInfo>();
    AotHelper.EnsureList<FriendshipModifyFriendGroupParam>();
    AotHelper.EnsureList<FriendshipGetPendencyListParam>();
    AotHelper.EnsureList<FriendshipDeletePendencyParam>();
    AotHelper.EnsureList<FriendSearchParam>();
    AotHelper.EnsureList<MessageReceipt>();
    AotHelper.EnsureList<GroupTipsElem>();
    AotHelper.EnsureList<ConvInfo>();
    AotHelper.EnsureList<Draft>();
    AotHelper.EnsureList<GroupAtInfo>();
    AotHelper.EnsureList<FriendAddPendency>();
    AotHelper.EnsureList<FriendProfile>();
    AotHelper.EnsureList<SetConfig>();
    AotHelper.EnsureList<HttpProxyInfo>();
    AotHelper.EnsureList<Socks5ProxyInfo>();
    AotHelper.EnsureList<UserConfig>();
    AotHelper.EnsureList<GroupGetInfoOption>();
    AotHelper.EnsureList<SSODataParam>();
    AotHelper.EnsureList<ServerAddress>();
    AotHelper.EnsureList<CustomServerInfo>();
    AotHelper.EnsureList<SM4GCMCallbackParam>();
    AotHelper.EnsureList<CosSaveRegionForConversationParam>();
    AotHelper.EnsureList<ExperimentalAPIReqeustParam>();
    AotHelper.EnsureList<GetTotalUnreadNumberResult>();
    AotHelper.EnsureList<GetC2CRecvMsgOptResult>();
    AotHelper.EnsureList<CreateGroupResult>();
    AotHelper.EnsureList<GroupInviteMemberResult>();
    AotHelper.EnsureList<GroupDeleteMemberResult>();
    AotHelper.EnsureList<GroupBaseInfo>();
    AotHelper.EnsureList<GroupSelfInfo>();
    AotHelper.EnsureList<GetGroupInfoResult>();
    AotHelper.EnsureList<GroupDetailInfo>();
    AotHelper.EnsureList<GroupPendencyResult>();
    AotHelper.EnsureList<GroupGetOnlineMemberCountResult>();
    AotHelper.EnsureList<FriendResult>();
    AotHelper.EnsureList<FriendshipCheckFriendTypeResult>();
    AotHelper.EnsureList<PendencyPage>();
    AotHelper.EnsureList<FriendAddPendencyInfo>();
    AotHelper.EnsureList<FriendInfoGetResult>();
    AotHelper.EnsureList<MessageSearchResult>();
    AotHelper.EnsureList<MessageSearchResultItem>();
    AotHelper.EnsureList<MsgBatchSendResult>();
    AotHelper.EnsureList<MsgDownloadElemResult>();
    AotHelper.EnsureList<ReponseInfo>();
    AotHelper.EnsureList<UserInfo>();
    AotHelper.EnsureList<SSODataRes>();
    AotHelper.EnsureList<GroupGetMemberInfoListResult>();
    AotHelper.EnsureList<GroupTopicInfo>();
    AotHelper.EnsureList<GroupInfo>();
    AotHelper.EnsureList<GroupTopicOperationResult>();
    AotHelper.EnsureList<UserStatus>();
  }
}