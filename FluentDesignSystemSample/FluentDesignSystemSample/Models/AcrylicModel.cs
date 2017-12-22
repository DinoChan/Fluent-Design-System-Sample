using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace FluentDesignSystemSample.Models
{
  public  class AcrylicModel: DependencyObject
    {
        public string Name { get; set; }


        /// <summary>
        /// 获取或设置Brush的值
        /// </summary>  
        public AcrylicBrush Brush
        {
            get { return (AcrylicBrush)GetValue(BrushProperty); }
            set { SetValue(BrushProperty, value); }
        }

        /// <summary>
        /// 标识 Brush 依赖属性。
        /// </summary>
        public static readonly DependencyProperty BrushProperty =
            DependencyProperty.Register("Brush", typeof(AcrylicBrush), typeof(AcrylicModel), new PropertyMetadata(null));
    }
}
