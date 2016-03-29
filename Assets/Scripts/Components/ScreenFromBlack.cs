//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：ScreenFromBlack.cs
//
// 文件功能描述：
//
// 进入新场景后，从黑屏平滑过渡到游戏场景
//
// 创建标识：taixihuase 20160329
//
// 修改标识：
// 修改描述：
// 
//
// 修改标识：
// 修改描述：
//
//----------------------------------------------------------------------------------------------------------

using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Components
{
    /// <summary>
    /// 类型：类
    /// 名称：ScreenFromBlack
    /// 作者：taixihuase
    /// 作用：从黑屏过渡效果
    /// 编写日期：2016/3/29
    /// </summary>
    public class ScreenFromBlack : MonoBehaviour
    {
        public Image FromBlack;

        public float Duration = 1.0f;

        // Use this for initialization
        void Start ()
        {
            FromBlack.gameObject.transform.DOLocalMove(Vector3.zero, 0);
            FromBlack.DOFade(0, Duration)
                .OnComplete(() => Destroy(gameObject));
        }
	
        // Update is called once per frame
        void Update () {
	
        }
    }
}
