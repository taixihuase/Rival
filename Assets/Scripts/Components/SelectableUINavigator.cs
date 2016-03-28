//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：SelectableUINavigator.cs
//
// 文件功能描述：
//
// UI 控件导航，按 Tab 或 Shift + Tab 组合键控制焦点在 UI 间的切换
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

using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// ReSharper disable InconsistentNaming

namespace Assets.Scripts.Components
{
    /// <summary>
    /// 类型：类
    /// 名称：SelectableUINavigator
    /// 作者：taixihuase
    /// 作用：键盘切换 UI 焦点
    /// 编写日期：2016/3/28
    /// </summary>
    public class SelectableUINavigator : MonoBehaviour
    {
        public Selectable[] UI;

        private EventSystem _eventSystem;

        // Use this for initialization
        private void Start()
        {
            _eventSystem = EventSystem.current;
        }

        // Update is called once per frame
        private void Update()
        {
            // When TAB is pressed, we should select the next selectable UI element
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                Selectable next = null;
                Selectable current = null;

                // Figure out if we have a valid current selected gameobject
                if (_eventSystem.currentSelectedGameObject != null)
                {
                    // Unity doesn't seem to "deselect" an object that is made inactive
                    if (_eventSystem.currentSelectedGameObject.activeInHierarchy)
                    {
                        current = _eventSystem.currentSelectedGameObject.GetComponent<Selectable>();
                    }
                }

                if (current != null)
                {
                    int index = Array.IndexOf(UI, current);
                    if (index >= 0)
                    {
                        // When SHIFT is held along with tab, go backwards instead of forwards
                        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                        {
                            while (next == null)
                            {
                                index = index == 0 ? UI.Length - 1 : index - 1;
                                next = UI[index];
                            }
                        }
                        else
                        {
                            while (next == null)
                            {
                                index = index == UI.Length - 1 ? 0 : index + 1;
                                next = UI[index];
                            }
                        }
                    }
                }

                if (next != null)
                {
                    next.Select();
                }
            }
        }
    }
}
