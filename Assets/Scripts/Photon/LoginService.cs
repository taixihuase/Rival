//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：LoginService.cs
//
// 文件功能描述：
//
// 登录进程，负责连接登录服务器、收发消息以及处理游戏登录逻辑
//
// 创建标识：taixihuase 20160326
//
// 修改标识：
// 修改描述：
// 
//
// 修改标识：
// 修改描述：
//
//----------------------------------------------------------------------------------------------------------

using System;
using C2SProtocol.Common;
using ExitGames.Client.Photon;
using UnityEngine;

namespace Assets.Scripts.Photon
{
    /// <summary>
    /// 类型：类
    /// 名称：LoginService
    /// 作者：taixihuase
    /// 作用：客户端逻辑进程
    /// 编写日期：2016/3/26
    /// </summary>
    public sealed class LoginService : PhotonService
    {
        public static readonly LoginService Instance = new LoginService();

        static LoginService()
        {
        }

        public override void OnOperationResponse(OperationResponse operationResponse)
        {
            
        }

        public override void OnEvent(EventData eventData)
        {
            switch ((C2SEventCode) Enum.Parse(typeof (C2SEventCode), eventData.Code.ToString()))
            {
                case C2SEventCode.SocketExist:
                    SocketExist(eventData);
                    break;
            }
        }

        #region OnEvent

        private void SocketExist(EventData eventData)
        {
            Debug.Log("套接字编号已注册，尝试重连");
            Reconnect();
        }

        #endregion
    }
}

	

