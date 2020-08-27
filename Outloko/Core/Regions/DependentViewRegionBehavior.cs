using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Ioc;
using Prism.Regions;

namespace Outloko.Core.Regions
{
    public class DependentViewRegionBehavior : RegionBehavior
    {
        public const string BehaviorKey = "DependentViewRegionBehavior";
        private readonly IContainerExtension _container;

        Dictionary<object, List<DependentViewInfo>> _dependentViewCache = new Dictionary<object, List<DependentViewInfo>>();

        public DependentViewRegionBehavior(IContainerExtension container)
        {
            this._container = container;
        }

        protected override void OnAttach()
        {
            Region.ActiveViews.CollectionChanged += ActiveViews_CollectionChanged;
        }

        private void ActiveViews_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var view in e.NewItems)
                {
                    var dependentViews = new List<DependentViewInfo>();

                    if (_dependentViewCache.ContainsKey(view))
                    {
                        dependentViews = _dependentViewCache[view];
                    }
                    else
                    {
                        var atts = GetCustomAttributes<DependentViewAttribute>(view.GetType());
                        foreach (var att in atts)
                        {
                            var info = CreateDependentViewInfo(att);
                            dependentViews.Add(info);
                        }

                        dependentViews.ForEach(item => Region.RegionManager.Regions[item.Region].Add(item.View));
                        _dependentViewCache.Add(view, dependentViews);
                    }
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach(var oldView in e.OldItems)
                {
                    if (_dependentViewCache.ContainsKey(oldView))
                    {
                        _dependentViewCache[oldView].ForEach(item => Region.RegionManager.Regions[item.Region].Remove(item.View));
                        if (!ShouldKeepAlive(oldView))
                            _dependentViewCache.Remove(oldView);
                    }
                }
            }
        }

        private bool ShouldKeepAlive(object oldView)
        {
            var regionLifetime = GetViewOrDataContextLifetime(oldView);
            if (regionLifetime != null)
                return regionLifetime.KeepAlive;

            return true;
        }

        IRegionMemberLifetime GetViewOrDataContextLifetime(object view)
        {
            if(view is IRegionMemberLifetime regionMemberLifetime)
                return regionMemberLifetime;

            if(view is FrameworkElement frameworkElement)
                return frameworkElement as IRegionMemberLifetime;

            return null;
        }

        DependentViewInfo CreateDependentViewInfo(DependentViewAttribute att)
        {
            var info = new DependentViewInfo();
            info.Region = att.Region;
            info.View = _container.Resolve(att.Type);

            return info;
        }

        private static IEnumerable<T> GetCustomAttributes<T>(Type type)
        {
            return type.GetCustomAttributes(typeof(T), true).OfType<T>();
        }
    }
}
