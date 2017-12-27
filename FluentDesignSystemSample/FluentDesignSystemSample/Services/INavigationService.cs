using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentDesignSystemSample.Services
{
    public interface INavigationService
    {
        event EventHandler<bool> IsNavigatingChanged;

        event EventHandler Navigated;

        bool CanGoBack { get; }

        bool IsNavigating { get; }

        Task NavigateToDepthAsync();

        Task NavigateToLightAsync();

        Task NavigateToMaterialAsync();

        Task NavigateToMotionAsync();

        Task NavigateToScaleAsync();

        Task NavigateToSettingsAsync();

        Task NavigateToPage<TPage>();

        Task NavigateToPage<TPage>(object parameter);

        Task GoBackAsync();
    }
}
