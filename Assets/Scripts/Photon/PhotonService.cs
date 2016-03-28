//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：PhotonService.cs
//
// 文件功能描述：
//
// Photon 客户端进程抽象基类
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

using System.Collections.Generic;
using C2SProtocol.Common;
using ExitGames.Client.Photon;
using ExitGames.Concurrency.Fibers;
using Protocol;

// ReSharper disable InconsistentNaming

namespace Assets.Scripts.Photon
{
    /// <summary>
    /// 类型：类
    /// 名称：PhotonService
    /// 作者：taixihuase
    /// 作用：Photon 客户端进程基类
    /// 编写日期：2016/3/26
    /// </summary>
    public abstract class PhotonService : IPhotonPeerListener, IResponseReceive, IEventReceive
    {
        // 连线用的 peer
        public PhotonPeer Peer { protected set; get; }

        // 连线状态
        public bool ServerConnected { protected set; get; }

        // 是否需要自动重连
        public bool AutoReconnect { protected set; get; }

        // 存放 Debug 信息
        public string DebugMessage { protected set; get; }

        // 服务器 IP
        public string ServerIP { protected set; get; }

        // 服务器端口
        public ushort ServerPort { protected set; get; }

        // 服务器连接协议
        public ConnectionProtocol ConnectionProtocol { protected set; get; }

        // 服务器名
        public string ServerName { protected set; get; }

        // 纤程池
        protected ExtendedPoolFiber Fiber { set; get; }

        /// <summary>
        /// 类型：方法
        /// 名称：PhotonService
        /// 作者：taixihuase
        /// 作用：程序运行后，构造客户端主进程实例
        /// 编写日期：2016/3/26
        /// </summary>
        protected PhotonService()
        {
            Peer = null;
            ServerConnected = false;
            AutoReconnect = true;
            Fiber = new ExtendedPoolFiber();
            Fiber.Start();
        }

        /// <summary>
        /// 类型：方法
        /// 名称：SetConnection
        /// 作者：taixihuase
        /// 作用：设置连接服务器参数
        /// 编写日期：2016/3/26
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="connectionProtocol"></param>
        /// <param name="name"></param>
        public void SetConnection(string ip, ushort port, ConnectionProtocol connectionProtocol, string name)
        {
            ServerIP = ip;
            ServerPort = port;
            ConnectionProtocol = connectionProtocol;
            ServerName = name;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：Connect
        /// 作者：taixihuase
        /// 作用：尝试与服务端连线
        /// 编写日期：2016/3/26
        /// </summary>
        public void Connect()
        {
            if (!ServerConnected && ServerIP != string.Empty)
            {
                string serverAddress = ServerIP + ":" + ServerPort;
                Peer = new PhotonPeer(this, ConnectionProtocol);
                Peer.Connect(serverAddress, ServerName);
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：Reconnect
        /// 作者：taixihuase
        /// 作用：尝试重连服务端连线
        /// 编写日期：2016/3/26
        /// </summary>
        public void Reconnect()
        {
            Disconnect();
            AutoReconnect = true;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：DebugReturn
        /// 作者：taixihuase
        /// 作用：获取返回的 Debug 消息
        /// 编写日期：2016/3/26
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        public void DebugReturn(DebugLevel level, string message)
        {
            DebugMessage = message;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：OnOperationResponse
        /// 作者：taixihuase
        /// 作用：客户端发送请求后，接收并处理相应的服务端响应内容
        /// 编写日期：2016/3/26
        /// </summary>
        /// <param name="operationResponse"></param>
        public virtual void OnOperationResponse(OperationResponse operationResponse)
        {
        }

        /// <summary>
        /// 类型：方法
        /// 名称：OnEvent
        /// 作者：taixihuase
        /// 作用：监听服务端发来的广播并回调触发事件
        /// 编写日期：2016/3/26
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnEvent(EventData eventData)
        {
        }

        /// <summary>
        /// 类型：方法
        /// 名称：OnStatusChanged
        /// 作者：taixihuase
        /// 作用：当连接状态发生改变时，回调触发
        /// 编写日期：2016/3/26
        /// </summary>
        /// <param name="statusCode"></param>
        public void OnStatusChanged(StatusCode statusCode)
        {
            switch (statusCode)
            {
                case StatusCode.Connect: // 连接
                    ServerConnected = true;
                    break;

                case StatusCode.Disconnect: // 断线
                    ServerConnected = false;
                    Peer = null;
                    break;
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：Service
        /// 作者：taixihuase
        /// 作用：请求服务
        /// 编写日期：2016/3/26
        /// </summary>
        public void Service()
        {
            if (Peer != null)
            {
                Peer.Service();
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：Disconnect
        /// 作者：taixihuase
        /// 作用：断开与服务端之间的连接
        /// 编写日期：2016/3/26
        /// </summary>
        public void Disconnect()
        {
            if (Peer != null)
            {
                Peer.Disconnect();
                Release();
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：Release
        /// 作者：taixihuase
        /// 作用：清除资源
        /// 编写日期：2016/3/26
        /// </summary>
        protected virtual void Release()
        {
        }

        /// <summary>
        /// 类型：方法
        /// 名称：SendRequest
        /// 作者：taixihuase
        /// 作用：通过 OperationRequest 实例向服务器发送请求
        /// 编写日期：2016/3/28
        /// </summary>
        /// <param name="operationRequest"></param>
        /// <param name="reliable"></param>
        public virtual void SendRequest(OperationRequest operationRequest, bool reliable = true)
        {
            if (Peer != null)
            {
                Peer.OpCustom(operationRequest.OperationCode, operationRequest.Parameters, reliable);
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：SendRequest
        /// 作者：taixihuase
        /// 作用：通过操作码、数据字典向服务器发送请求
        /// 编写日期：2016/3/28
        /// </summary>
        /// <param name="opCode"></param>
        /// <param name="obj"></param>
        /// <param name="reliable"></param>
        public virtual void SendRequest(C2SOpCode opCode, Dictionary<byte, object> obj = null,
            bool reliable = true)
        {
            var request = new OperationRequest
            {
                OperationCode = (byte) opCode,
                Parameters = obj
            };
            SendRequest(request, reliable);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：SendRequest
        /// 作者：taixihuase
        /// 作用：通过操作码、参数码、数据对象向服务器发送请求
        /// 编写日期：2016/3/28
        /// </summary>
        /// <param name="opCode"></param>
        /// <param name="paraCode"></param>
        /// <param name="obj"></param>
        /// <param name="reliable"></param>
        public virtual void SendRequest(C2SOpCode opCode, C2SParaCode ?paraCode, object obj = null,
            bool reliable = true)
        {
            Dictionary<byte, object> para = null;
            if (paraCode.HasValue && obj != null)
            {
                para = new Dictionary<byte, object>
                {
                    {(byte) paraCode, Serialization.IsNeed(obj) ? Serialization.Serialize(obj) : obj}
                };
            }
            SendRequest(opCode, para, reliable);
        }
    }
}
