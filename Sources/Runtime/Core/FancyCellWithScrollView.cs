/*
 * FancyScrollView (https://github.com/setchi/FancyScrollView)
 * Copyright (c) 2020 setchi
 * Licensed under MIT (https://github.com/setchi/FancyScrollView/blob/master/LICENSE)
 */

namespace FancyScrollView
{
    /// <summary>
    /// <see cref="FancyScrollView{TItemData, TContext}.ResizePool"/> 実行時に ScrollView の参照を受け取るための内部インターフェース.
    /// </summary>
    interface IFancyCellScrollViewReceiver<TItemData, TContext> where TContext : class, new()
    {
        void SetScrollView(FancyScrollView<TItemData, TContext> scrollView);
    }

    /// <summary>
    /// <see cref="FancyCell{TItemData, TContext}"/> のバリエーション.
    /// Context の代わりに, このセルを管理している親の <see cref="FancyScrollView{TItemData, TContext}"/>
    /// の継承コンポーネントへの参照を保持します.
    /// </summary>
    /// <remarks>
    /// セル内から <c>ScrollTo</c> / <c>JumpTo</c> など ScrollView 固有のメソッドを直接呼び出したい場合に使用します.
    /// Context も引き続き利用可能です.
    /// <para>
    /// <b>注意:</b> <see cref="FancyGridView{TItemData, TContext}"/> の子セル (<see cref="FancyGridViewCell{TItemData, TContext}"/>) には
    /// 型パラメータの不一致により自動注入されません.
    /// </para>
    /// </remarks>
    /// <typeparam name="TItemData">アイテムのデータ型.</typeparam>
    /// <typeparam name="TContext"><see cref="FancyCell{TItemData, TContext}.Context"/> の型.</typeparam>
    /// <typeparam name="TScrollView">親 <see cref="FancyScrollView{TItemData, TContext}"/> の継承型.</typeparam>
    public abstract class FancyCellWithScrollView<TItemData, TContext, TScrollView>
        : FancyCell<TItemData, TContext>,
          IFancyCellScrollViewReceiver<TItemData, TContext>
        where TContext : class, new()
        where TScrollView : FancyScrollView<TItemData, TContext>
    {
        /// <summary>
        /// このセルを管理している <typeparamref name="TScrollView"/> の参照.
        /// <see cref="FancyCell{TItemData, TContext}.Initialize"/> の時点で参照可能です.
        /// </summary>
        protected TScrollView ScrollView { get; private set; }

        void IFancyCellScrollViewReceiver<TItemData, TContext>.SetScrollView(
            FancyScrollView<TItemData, TContext> scrollView)
            => ScrollView = (TScrollView)scrollView;
    }

    /// <summary>
    /// Context を使用しない <see cref="FancyCellWithScrollView{TItemData, TContext, TScrollView}"/>.
    /// </summary>
    /// <typeparam name="TItemData">アイテムのデータ型.</typeparam>
    /// <typeparam name="TScrollView">親 <see cref="FancyScrollView{TItemData}"/> の継承型.</typeparam>
    public abstract class FancyCellWithScrollView<TItemData, TScrollView>
        : FancyCellWithScrollView<TItemData, NullContext, TScrollView>
        where TScrollView : FancyScrollView<TItemData, NullContext> { }
}
