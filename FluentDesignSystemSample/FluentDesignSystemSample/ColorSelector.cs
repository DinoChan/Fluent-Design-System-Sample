using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

namespace FluentDesignSystemSample
{
    [TemplatePart(Name = ColorPickerName, Type = typeof(ColorPicker))]
    public class ColorSelector : Picker
    {
        private const string ColorPickerName = "ColorPicker";

        /// <summary>
        /// 标识 Color 依赖属性。
        /// </summary>
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(Color), typeof(ColorSelector), new PropertyMetadata(Colors.White, OnColorChanged));

        private static void OnColorChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ColorSelector target = obj as ColorSelector;
            Color oldValue = (Color)args.OldValue;
            Color newValue = (Color)args.NewValue;
            if (oldValue != newValue)
                target.OnColorChanged(oldValue, newValue);
        }


        /// <summary>
        /// 标识 ColorPickerStyle 依赖属性。
        /// </summary>
        public static readonly DependencyProperty ColorPickerStyleProperty =
            DependencyProperty.Register("ColorPickerStyle", typeof(Style), typeof(ColorSelector), new PropertyMetadata(null, OnColorPickerStyleChanged));

        private static void OnColorPickerStyleChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ColorSelector target = obj as ColorSelector;
            Style oldValue = (Style)args.OldValue;
            Style newValue = (Style)args.NewValue;
            if (oldValue != newValue)
                target.OnColorPickerStyleChanged(oldValue, newValue);
        }




        public ColorSelector()
        {
            DefaultStyleKey = typeof(ColorSelector);
        }

        public event EventHandler ColorChanged;

        /// <summary>
        /// 获取或设置Color的值
        /// </summary>  
        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        /// <summary>
        /// 获取或设置ColorPickerStyle的值
        /// </summary>  
        public Style ColorPickerStyle
        {
            get { return (Style)GetValue(ColorPickerStyleProperty); }
            set { SetValue(ColorPickerStyleProperty, value); }
        }

        private ColorPicker _colorPicker;


        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _colorPicker = GetTemplateChild(ColorPickerName) as ColorPicker;
            SetSelectedColor();
            UpdateVisualState(false);
        }

        protected override void OnAccept(RoutedEventArgs e)
        {
            base.OnAccept(e);
            if (_colorPicker != null)
                Color = _colorPicker.Color;
        }


        protected virtual void OnColorChanged(Color oldValue, Color newValue)
        {
            SetSelectedColor();
            if (ColorChanged != null)
                ColorChanged(this, EventArgs.Empty);
        }

        protected virtual void OnColorPickerStyleChanged(Style oldValue, Style newValue)
        {
        }

        protected override void OnIsDropDownOpenChanged(bool oldValue, bool newValue)
        {
            base.OnIsDropDownOpenChanged(oldValue, newValue);

            if (newValue)
                SetSelectedColor();
        }

        private void SetSelectedColor()
        {
            if (_colorPicker != null)
            {
                _colorPicker.Color = Color;
            }
        }
    }
}
