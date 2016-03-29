//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：PhotonSingleton.cs
//
// 文件功能描述：
//
// Photon 客户端单例，存放客户端进程实例及服务端信息
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

using Assets.Scripts.Components;
using C2SProtocol.Common;
using ExitGames.Client.Photon;
using ExitGames.Concurrency.Fibers;
using UnityEngine;
// ReSharper disable UnusedMember.Local

namespace Assets.Scripts.Photon
{
    /// <summary>
    /// 类型：类
    /// 名称：PhotonSingleton
    /// 作者：taixihuase
    /// 作用：Photon 单例类，Unity 通过实例化该单例使用 PhotonService 类型客户端处理进程
    /// 编写日期：2016/3/26
    /// </summary>
    public class PhotonSingleton : MonoBehaviour
    {
        // 全局静态单例
        private static PhotonSingleton _instance;

        // 单例属性
        public static PhotonSingleton Instance
        {
            get
            {
                // 若获取不到单例，则寻找该单例，并拒绝销毁单例所挂载的对象上
                if (_instance == null)
                {
                    _instance = FindObjectOfType<PhotonSingleton>();
                    DontDestroyOnLoad(_instance.gameObject);
                }

                return _instance;
            }
        }

        // 客户端正在使用的服务进程
        public static PhotonService Service;

        // 是否正在尝试连接
        private bool _isTrying;

        // 纤程池
        private ExtendedPoolFiber Fiber { get; set; }

        // 是否允许请求服务
        public bool IsCanService { get; set; }

        /// <summary>
        /// 类型：方法
        /// 名称：Awake
        /// 作者：taixihuase
        /// 作用：创建单例
        /// 编写日期：2016/3/26
        /// </summary>
        private void Awake()
        {
            // 若当前不存在单例，则创建单例并实例化客户端服务进程
            if (_instance == null)
            {
                _instance = this;
                Service = LoginService.Instance;
                IsCanService = true;
                Fiber = new ExtendedPoolFiber();
                Fiber.Start();
                DontDestroyOnLoad(this);
            }
            else
            {
                // 若已存在一个单例，则销毁该单例所挂载的对象
                if (this != _instance)
                {
                    Destroy(gameObject);
                }
            }
        }


        // Use this for initialization
        void Start () {
            Service.SetConnection(ServerSettings.Default.IpOfLoginServer, ServerSettings.Default.PortOfLoginServer, ConnectionProtocol.Tcp, ServerSettings.Default.NameOfLoginServer);
            Fiber.Enqueue(TryConnect);
        }
	
        // Update is called once per frame
        private void Update()
        {
            if (IsCanService)
            {
                Service.Service();

                Debug.Log(Service.ServerConnected);

                // 如果未能连接到服务器，且需要自动重连服务器
                if (!Service.ServerConnected && Service.AutoReconnect)
                {
                    // 如果未尝试连接服务器，则发起连接请求
                    if (!_isTrying)
                    {
                        Fiber.Enqueue(TryConnect);
                    }
                }
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：OnApplicationQuit
        /// 作者：taixihuase
        /// 作用：退出游戏进程
        /// 编写日期：2016/3/26
        /// </summary>
        private void OnApplicationQuit()
        {
            Service.Disconnect();
        }

        /// <summary>
        /// 类型：方法
        /// 名称：TryConnect
        /// 作者：taixihuase
        /// 作用：尝试连接服务端
        /// 编写日期：2016/3/26
        /// </summary>
        /// <returns></returns>
        private void TryConnect()
        {
            _isTrying = true;
            Service.Connect();
            Fiber.Schedule((() => _isTrying = false), 5000);
        }
    }
}
