/*
 * FancyScrollView (https://github.com/setchi/FancyScrollView)
 * Copyright (c) 2020 setchi
 * Licensed under MIT (https://github.com/setchi/FancyScrollView/blob/master/LICENSE)
 */

namespace FancyScrollView
{
    /// <summary>
    /// スクローラーのスクロール状態.
    /// </summary>
    public enum ScrollState
    {
        /// <summary>静止中.</summary>
        Idle,

        /// <summary>ユーザーがドラッグ中.</summary>
        Dragging,

        /// <summary>慣性スクロール中.</summary>
        InertiaScrolling,

        /// <summary><see cref="Scroller.ScrollTo"/> またはエラスティック復元によるアニメーション中.</summary>
        AutoScrolling,
    }
}
