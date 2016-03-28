//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：UIScriptDemo.cs
//
// 文件功能描述：
//
// UI 控件脚本编写示例
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

using ExitGames.Concurrency.Fibers;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable InconsistentNaming

namespace Assets.Scripts.Demo
{
    /// <summary>
    /// 类型：类
    /// 名称：UIScriptDemo
    /// 作者：taixihuase
    /// 作用：UI 控件脚本编写示例
    /// 编写日期：2016/3/26
    /// </summary>
    public class UIScriptDemo : MonoBehaviour
    {
        // 尽量为每个脚本直接引用所需对象，即使多个 UI 脚本可能共用同一个对象
        public Text DemoText;

        // UI 可以使用纤程池控制，少用协程
        public ExtendedPoolFiber Fiber;

        // 是否倒计时中
        private bool _isCD;

        private void Start()
        {
            Fiber = new ExtendedPoolFiber();
            Fiber.Start();
            _isCD = false;
        }

        private void Update()
        {
            // 多用 ReSharper 简化代码
            DemoText.text = !_isCD ? "测试按钮" : "点击";
        }

        private void OnDestroy()
        {
            Fiber.Stop();
        }

        /// <summary>
        /// 类型：方法
        /// 名称：OnClick
        /// 作者：taixihuase
        /// 作用：每个按键 UI 如果不是自动获取点击委托事件，则需为其编写类名与 UI 相同的脚本，并为其绑定自身所带的脚本的 OnClick 等方法，切记不要将多个不同 UI 控件的方法放于同一个脚本之中，减少冲突
        /// 编写日期：2016/3/26
        /// </summary>
        public void OnClick()
        {
            // 暂停 Fiber 不会清空任务，只是阻塞住，必须停用才能清空任务，且需重新实例化纤程池才能立即启用
            Fiber.Stop();
            Fiber.Dispose();
            Fiber = new ExtendedPoolFiber();
            Fiber.Start();
            Fiber.Enqueue((() => Countdown(2000)));
        }

        /// <summary>
        /// 类型：方法
        /// 名称：Countdown
        /// 作者：taixihuase
        /// 作用：纤程池所使用的委托方法，避免了协程
        /// 编写日期：2016/3/26
        /// </summary>
        /// <param name="delay"></param>
        private void Countdown(long delay)
        {
            _isCD = true;            
            Fiber.Schedule(() =>
            {
                _isCD = false;
            }, delay);
        }
    }
}
