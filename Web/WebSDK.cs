using com.tencent.imsdk.unity.utils;
using com.tencent.imsdk.unity.types;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using com.tencent.imsdk.unity.enums;
using com.tencent.imsdk.unity.callback;
using Newtonsoft.Json;
using UnityEngine;
using AOT;
using System.Text;
using com.tencent.imsdk.unity.native;

namespace com.tencent.imsdk.unity.web
{
  public class IMWebSDK
  {
#if !UNITY_EDITOR && UNITY_WEBGL
    [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMGetLoginUserIDWeb(IMNativeSDK.CommonValueCallback cb, IntPtr user_id);
    [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
    public static extern int TIMMsgGetGroupMessageReadMemberListWeb(IntPtr json_msg, TIMGroupMessageReadMembersFilter filter, IntPtr next_seq, int count, TIMMsgGroupMessageReadMemberListCallback cb, IntPtr user_data);
#endif
  /// <summary>
  /// 获取群消息已读群成员列表回调 (Callback for group message read member list)
  /// </summary>
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void MsgGroupMessageReadMemberListCallback(string json_group_member_array, string next_seq, bool is_finished, string user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TIMMsgGroupMessageReadMemberListCallback(IntPtr json_group_member_array, IntPtr next_seq, IntPtr is_finished, IntPtr user_data);

    // [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    // public delegate void CommonStrPtrCallback(IntPtr str, IntPtr user_data);
  }
}