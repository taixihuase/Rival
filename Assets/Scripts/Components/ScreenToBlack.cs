//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：ScreenToBlack.cs
//
// 文件功能描述：
//
// 异步加载场景时，进行黑屏过渡
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
    /// 名称：ScreenToBlack
    /// 作者：taixihuase
    /// 作用：过渡到黑屏效果，再切换至加载画面
    /// 编写日期：2016/3/29
    /// </summary>
    public class ScreenToBlack : MonoBehaviour
    {
        public Image ToBlack;

        // 加载画面根对象
        public Image Transition;

        public float Duration = 1.0f;

        // Use this for initialization
        void Start ()
        {
            ToBlack.gameObject.transform.DOLocalMove(Vector3.zero, 0);
            if (Transition != null)
                Transition.gameObject.SetActive(false);

            ToBlack.DOFade(1.0f, Duration).OnComplete((() =>
            {
                if (Transition != null)
                {
                    Transition.gameObject.SetActive(true);
                }
            }));

        }
	
        // Update is called once per frame
        void Update () {
	
        }
    }
}
