using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace FluentDesignSystemSample
{
    [TemplateVisualState(Name = PopupClosedName, GroupName = PopupStatesName)]
    [TemplateVisualState(Name = PopupOpenedName, GroupName = PopupStatesName)]
    [TemplatePart(Name = AcceptButtonName, Type = typeof(Button))]
    [TemplatePart(Name = DismissButtonName, Type = typeof(Button))]
    [TemplatePart(Name = FlyoutName, Type = typeof(FlyoutBase))]
    public abstract class Picker : HeaderedContentControl
    {
        private const string PopupClosedName = "PopupClosed";
        private const string PopupOpenedName = "PopupOpened";
        private const string PopupStatesName = "PopupStates";


        private const string FlyoutName = "Flyout";
        private const string AcceptButtonName = "AcceptButton";
        private const string DismissButtonName = "DismissButton";

        /// <summary>
        ///     标识 IsDropDownOpen 依赖属性。
        /// </summary>
        public static readonly DependencyProperty IsDropDownOpenProperty =
            DependencyProperty.Register("IsDropDownOpen", typeof(bool), typeof(Picker), new PropertyMetadata(false, OnIsDropDownOpenChanged));

        private Button _acceptButton;
        private Button _dismissButton;
        private FlyoutBase _flyout;

        /// <summary>
        ///     获取或设置IsDropDownOpen的值
        /// </summary>
        public bool IsDropDownOpen
        {
            get => (bool)GetValue(IsDropDownOpenProperty);
            set => SetValue(IsDropDownOpenProperty, value);
        }

        private static void OnIsDropDownOpenChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var target = obj as Picker;
            var oldValue = (bool)args.OldValue;
            var newValue = (bool)args.NewValue;
            if (oldValue != newValue)
                target.OnIsDropDownOpenChanged(oldValue, newValue);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _flyout = GetTemplateChild(FlyoutName) as FlyoutBase;
            if (_flyout != null)
                _flyout.Closed += OnFlyoutClosed;

            _acceptButton = GetTemplateChild(AcceptButtonName) as Button;
            if (_acceptButton != null)
                _acceptButton.Click += OnAccept;

            _dismissButton = GetTemplateChild(DismissButtonName) as Button;
            if (_dismissButton != null)
                _dismissButton.Click += OnDismiss;

            UpdateVisualState(false);
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

        protected override void UpdateVisualState(bool useTransitions)
        {
            base.UpdateVisualState(useTransitions);
            VisualStateManager.GoToState(this, IsDropDownOpen ? PopupOpenedName : PopupClosedName, useTransitions);
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


    }
}
