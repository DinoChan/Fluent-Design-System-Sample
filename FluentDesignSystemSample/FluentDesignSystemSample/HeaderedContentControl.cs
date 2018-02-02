using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace FluentDesignSystemSample
{
    [TemplateVisualState(Name = NormalName, GroupName = CommonStatesName)]
    [TemplateVisualState(Name = DisabledName, GroupName = CommonStatesName)]
    [TemplatePart(Name = HeaderContentPresenterName, Type = typeof(ContentPresenter))]
    public class HeaderedContentControl : ContentControl
    {
        private const string CommonStatesName = "CommonStates";
        private const string NormalName = "Normal";
        private const string DisabledName = "Disabled";
        private const string HeaderContentPresenterName = "HeaderContentPresenter";

        /// <summary>
        ///     标识 Header 依赖属性。
        /// </summary>
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(object), typeof(HeaderedContentControl), new PropertyMetadata(null, OnHeaderChanged));

        /// <summary>
        ///     标识 HeaderTemplate 依赖属性。
        /// </summary>
        public static readonly DependencyProperty HeaderTemplateProperty =
            DependencyProperty.Register("HeaderTemplate", typeof(DataTemplate), typeof(HeaderedContentControl), new PropertyMetadata(null, OnHeaderTemplateChanged));


        private ContentPresenter _headerContentPresenter;

        public HeaderedContentControl()
        {
            DefaultStyleKey = typeof(HeaderedContentControl);
            IsEnabledChanged += OnPickerIsEnabledChanged;
        }

        /// <summary>
        ///     获取或设置Header的值
        /// </summary>
        public object Header
        {
            get => GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        /// <summary>
        ///     获取或设置HeaderTemplate的值
        /// </summary>
        public DataTemplate HeaderTemplate
        {
            get => (DataTemplate)GetValue(HeaderTemplateProperty);
            set => SetValue(HeaderTemplateProperty, value);
        }


        private static void OnHeaderChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var target = obj as HeaderedContentControl;
            var oldValue = args.OldValue;
            var newValue = args.NewValue;
            if (oldValue != newValue)
                target.OnHeaderChanged(oldValue, newValue);
        }

        private static void OnHeaderTemplateChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var target = obj as HeaderedContentControl;
            var oldValue = (DataTemplate)args.OldValue;
            var newValue = (DataTemplate)args.NewValue;
            if (oldValue != newValue)
                target.OnHeaderTemplateChanged(oldValue, newValue);
        }


        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _headerContentPresenter = GetTemplateChild(HeaderContentPresenterName) as ContentPresenter;
            UpdateVisibility();
            UpdateVisualState(false);

            if (_headerContentPresenter != null)
            {
                _headerContentPresenter.PointerReleased += OnHeaderContentPresenterPointerReleased;
                _headerContentPresenter.PointerPressed += OnHeaderContentPresenterPointerPressed1;
            }
        }

        protected virtual void OnHeaderChanged(object oldValue, object newValue)
        {
            UpdateVisibility();
        }

        protected virtual void OnHeaderTemplateChanged(DataTemplate oldValue, DataTemplate newValue)
        {
        }


        protected virtual void UpdateVisualState(bool useTransitions)
        {
            VisualStateManager.GoToState(this, IsEnabled ? NormalName : DisabledName, useTransitions);
        }

        private void UpdateVisibility()
        {
            if (_headerContentPresenter != null)
                _headerContentPresenter.Visibility = _headerContentPresenter.Content == null ? Visibility.Collapsed : Visibility.Visible;
        }

        private void OnPickerIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpdateVisualState(true);
        }

        private void OnHeaderContentPresenterPointerPressed1(object sender, PointerRoutedEventArgs e)
        {
            if (Content is Control control)
                control.Focus(FocusState.Programmatic);
        }

        private void OnHeaderContentPresenterPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            e.Handled = true;
        }
    }
}
