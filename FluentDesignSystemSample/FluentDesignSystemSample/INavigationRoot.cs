using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentDesignSystemSample
{
    interface INavigationRoot
    {
        event EventHandler IsPaneOpenChanged;

        bool IsPaneOpen { get; }

        double CompactPaneLength { get; }
    }
}
