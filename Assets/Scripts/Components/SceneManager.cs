//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：SceneManager.cs
//
// 文件功能描述：
//
// 负责场景管理、切换和加载
//
// 创建标识：taixihuase 20160328
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
using System.ComponentModel;
using Protocol;

namespace Assets.Scripts.Components
{
    /// <summary>
    /// 类型：类
    /// 名称：SceneManager
    /// 作者：taixihuase
    /// 作用：场景管理单例类
    /// 编写日期：2016/3/28
    /// </summary>
    public sealed class SceneManager
    {
        public static readonly  SceneManager Instance = new SceneManager();

        static SceneManager()
        {

        }

        /// <summary>
        /// 类型：枚举
        /// 名称：SceneCode
        /// 作者：taixihuase
        /// 作用：各场景枚举
        /// 编写日期：2016/3/28
        /// </summary>
        public enum SceneCode
        {
            [Description("Setup")] Setup,
            [Description("Loading")] Loading,
            [Description("Login")] Login,
            [Description("Regist")] Regist,
        }

        private readonly Stack<SceneCode> _scenesStack = new Stack<SceneCode>();

        /// <summary>
        /// 类型：方法
        /// 名称：Push
        /// 作者：taixihuase
        /// 作用：压入场景枚举
        /// 编写日期：2016/3/28
        /// </summary>
        /// <param name="scene"></param>
        public void Push(SceneCode scene)
        {
            _scenesStack.Push(scene);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：Push
        /// 作者：taixihuase
        /// 作用：通过字符串压入场景枚举
        /// 编写日期：2016/3/28
        /// </summary>
        /// <param name="scene"></param>
        public void Push(string scene)
        {
            Push(EnumTool.GetEnum<SceneCode>(scene));
        }

        /// <summary>
        /// 类型：方法
        /// 名称：Pop
        /// 作者：taixihuase
        /// 作用：弹出场景枚举
        /// 编写日期：2016/3/28
        /// </summary>
        /// <returns></returns>
        public SceneCode? Pop()
        {
            if(_scenesStack.Count > 0)
                return _scenesStack.Pop();
            return null;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：PopName
        /// 作者：taixihuase
        /// 作用：弹出场景枚举名
        /// 编写日期：2016/3/28
        /// </summary>
        /// <returns></returns>
        public string PopName()
        {
            return Pop().GetDescription();
        }

        /// <summary>
        /// 类型：方法
        /// 名称：Peek
        /// 作者：taixihuase
        /// 作用：查看场景枚举
        /// 编写日期：2016/3/28
        /// </summary>
        /// <returns></returns>
        public SceneCode? Peek()
        {
            if (_scenesStack.Count > 0)
                return _scenesStack.Peek();
            return null;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：PeekName
        /// 作者：taixihuase
        /// 作用：查看场景枚举名
        /// 编写日期：2016/3/28
        /// </summary>
        /// <returns></returns>
        public string PeekName()
        {
            return Peek().GetDescription();
        }

        /// <summary>
        /// 类型：方法
        /// 名称：LoadScene
        /// 作者：taixihuase
        /// 作用：直接加载场景
        /// 编写日期：2016/3/28
        /// </summary>
        /// <param name="scene"></param>
        public void LoadScene(SceneCode scene)
        {
            Push(scene);
            UnityEngine.SceneManagement.SceneManager.LoadScene(scene.GetDescription());
        }

        /// <summary>
        /// 类型：方法
        /// 名称：LoadScene
        /// 作者：taixihuase
        /// 作用：通过场景名直接加载场景
        /// 编写日期：2016/3/28
        /// </summary>
        /// <param name="scene"></param>
        public void LoadScene(string scene)
        {
            LoadScene(EnumTool.GetEnum<SceneCode>(scene));
        }

        /// <summary>
        /// 类型：方法
        /// 名称：LoadSceneAsync
        /// 作者：taixihuase
        /// 作用：异步加载场景
        /// 编写日期：2016/3/28
        /// </summary>
        /// <param name="scene"></param>
        public void LoadSceneAsync(SceneCode scene)
        {
            Push(scene);
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneCode.Loading.GetDescription());
        }

        /// <summary>
        /// 类型：方法
        /// 名称：LoadSceneAsync
        /// 作者：taixihuase
        /// 作用：通过场景名异步加载场景
        /// 编写日期：2016/3/28
        /// </summary>
        /// <param name="scene"></param>
        public void LoadSceneAsync(string scene)
        {
            LoadSceneAsync(EnumTool.GetEnum<SceneCode>(scene));
        }

        public void Test()
        {

        }
    }
}
