//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：LogicService.cs
//
// 文件功能描述：
//
// 逻辑进程，负责连接网工代理服务器、收发消息以及处理游戏主逻辑
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
    /// 名称：LogicService
    /// 作者：taixihuase
    /// 作用：客户端登录进程
    /// 编写日期：2016/3/26
    /// </summary>
    public sealed class LogicService : PhotonService
    {
        public static readonly LogicService Instance = new LogicService();

        static LogicService()
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

	

