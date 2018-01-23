using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FluentDesignSystemSample
{
    [TemplateVisualState(Name = PopupClosedName, GroupName = PopupStatesName)]
    [TemplateVisualState(Name = PopupOpenedName, GroupName = PopupStatesName)]
    [TemplateVisualState(Name = NormalName, GroupName = CommonStatesName)]
    [TemplateVisualState(Name = DisabledName, GroupName = CommonStatesName)]
    [TemplatePart(Name = HeaderContentPresenterName, Type = typeof(ContentPresenter))]
    [TemplatePart(Name = AcceptButtonName, Type = typeof(Button))]
    [TemplatePart(Name = DismissButtonName, Type = typeof(Button))]
    [TemplatePart(Name = FlyoutName, Type = typeof(Flyout))]

    public abstract class Picker : Control
    {
        private const string PopupClosedName = "PopupClosed";
        private const string PopupOpenedName = "PopupOpened";
        private const string PopupStatesName = "PopupStates";

        private const string CommonStatesName = "CommonStates";
        private const string NormalName = "Normal";
        private const string DisabledName = "Disabled";

        private const string HeaderContentPresenterName = "HeaderContentPresenter";
        private const string FlyoutName = "Flyout";
        private const string AcceptButtonName = "AcceptButton";
        private const string DismissButtonName = "DismissButton";

        /// <summary>
        ///     标识 IsDropDownOpen 依赖属性。
        /// </summary>
        public static readonly DependencyProperty IsDropDownOpenProperty =
            DependencyProperty.Register("IsDropDownOpen", typeof(bool), typeof(Picker), new PropertyMetadata(false, OnIsDropDownOpenChanged));

        private static void OnIsDropDownOpenChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var target = obj as Picker;
            var oldValue = (bool)args.OldValue;
            var newValue = (bool)args.NewValue;
            if (oldValue != newValue)
                target.OnIsDropDownOpenChanged(oldValue, newValue);
        }

        /// <summary>
        ///     标识 Header 依赖属性。
        /// </summary>
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(object), typeof(Picker), new PropertyMetadata(null, OnHeaderChanged));

        private static void OnHeaderChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var target = obj as Picker;
            var oldValue = args.OldValue;
            var newValue = args.NewValue;
            if (oldValue != newValue)
                target.OnHeaderChanged(oldValue, newValue);
        }

        /// <summary>
        ///     标识 HeaderTemplate 依赖属性。
        /// </summary>
        public static readonly DependencyProperty HeaderTemplateProperty =
            DependencyProperty.Register("HeaderTemplate", typeof(DataTemplate), typeof(Picker), new PropertyMetadata(null, OnHeaderTemplateChanged));

        private static void OnHeaderTemplateChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var target = obj as Picker;
            var oldValue = (DataTemplate)args.OldValue;
            var newValue = (DataTemplate)args.NewValue;
            if (oldValue != newValue)
                target.OnHeaderTemplateChanged(oldValue, newValue);
        }

        protected Picker()
        {
            base.IsEnabledChanged += OnPickerIsEnabledChanged;
        }



        /// <summary>
        ///     获取或设置IsDropDownOpen的值
        /// </summary>
        public bool IsDropDownOpen
        {
            get => (bool)GetValue(IsDropDownOpenProperty);
            set => SetValue(IsDropDownOpenProperty, value);
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

        private ContentPresenter _headerContentPresenter;
        private Flyout _flyout;
        private Button _acceptButton;
        private Button _dismissButton;

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _headerContentPresenter = GetTemplateChild(HeaderContentPresenterName) as ContentPresenter;
            _flyout = GetTemplateChild(FlyoutName) as Flyout;
            if (_flyout != null)
                _flyout.Closed += OnFlyoutClosed;

            _acceptButton = GetTemplateChild(AcceptButtonName) as Button;
            if (_acceptButton != null)
                _acceptButton.Click += OnAccept;

            _dismissButton = GetTemplateChild(DismissButtonName) as Button;
            if (_dismissButton != null)
                _dismissButton.Click += OnDismiss;
            UpdateVisibility();
        }


        protected virtual void OnIsDropDownOpenChanged(bool oldValue, bool newValue)
        {
            if (_flyout == null)
                return;

            if (newValue)
                _flyout.ShowAt(this);
            else
                _flyout.Hide();
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
            VisualStateManager.GoToState(this, IsDropDownOpen ? PopupOpenedName : PopupClosedName, useTransitions);

            VisualStateManager.GoToState(this, IsEnabled ? NormalName : DisabledName, useTransitions);
        }

        protected virtual void OnFlyoutClosed(object e)
        {
            IsDropDownOpen = false;
        }

        protected virtual void OnAccept(RoutedEventArgs e)
        {
            IsDropDownOpen = false;
        }

        protected virtual void OnDismiss(RoutedEventArgs e)
        {
            IsDropDownOpen = false;
        }


        private void OnFlyoutClosed(object sender, object e)
        {
            OnFlyoutClosed(e);
        }

        private void OnAccept(object sender, RoutedEventArgs e)
        {
            OnAccept(e);
        }

        private void OnDismiss(object sender, RoutedEventArgs e)
        {
            OnDismiss(e);
        }

        private void OnPickerIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpdateVisualState(true);
        }

        private void UpdateVisibility()
        {
            if (_headerContentPresenter != null)
            {
                _headerContentPresenter.Visibility = _headerContentPresenter.Content == null ? Visibility.Collapsed : Visibility.Visible;
            }
        }
    }
}
