//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：LoadSceneAsyncDemo.cs
//
// 文件功能描述：
//
// 调用异步加载场景方法示例
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

using Assets.Scripts.Components;
using UnityEngine;

namespace Assets.Scripts.Demo
{
    /// <summary>
    /// 类型：类
    /// 名称：LoadSceneAsyncDemo
    /// 作者：taixihuase
    /// 作用：异步加载场景调用示例
    /// 编写日期：2016/3/28
    /// </summary>
    public class LoadSceneAsyncDemo : MonoBehaviour
    {
        /// <summary>
        /// 类型：方法
        /// 名称：OnClick
        /// 作者：taixihuase
        /// 作用：异步加载场景，调用 SceneManager 中的方法进行加载会先切换到 Loading 界面再异步加载指定场景
        /// 编写日期：2016/3/28
        /// </summary>
        /// <param name="scene"></param>
        public void OnClick(string scene)
        {
            SceneManager.Instance.LoadSceneAsync(scene);
        }
    }
}
