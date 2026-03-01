/*
 * FancyScrollView (https://github.com/setchi/FancyScrollView)
 * Copyright (c) 2020 setchi
 * Licensed under MIT (https://github.com/setchi/FancyScrollView/blob/master/LICENSE)
 */

#if FANCY_SCROLL_VIEW_R3_SUPPORT
using R3;

namespace FancyScrollView
{
    public static class ScrollerR3Extensions
    {
        /// <summary>
        /// スクロール状態の変化を <see cref="Observable{T}"/> として購読します.
        /// </summary>
        public static Observable<ScrollState> OnScrollStateChangedAsObservable(this Scroller scroller)
        {
            return Observable.FromEvent<ScrollState>(
                h => scroller.ScrollStateChanged += h,
                h => scroller.ScrollStateChanged -= h)
                .TakeUntilDestroy(scroller);
        }
    }
}
#endif
