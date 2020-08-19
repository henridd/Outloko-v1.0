using Infragistics.Windows.OutlookBar;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outloko.Core.Regions
{
    public class XamOutlookBarRegionAdapter : RegionAdapterBase<XamOutlookBar>
    {
        public XamOutlookBarRegionAdapter(IRegionBehaviorFactory factory) : base(factory)
        {

        }

        protected override void Adapt(IRegion region, XamOutlookBar regionTarget)
        {
            region.Views.CollectionChanged += ((x, y) =>
            {
                switch (y.Action)
                {
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                        foreach(OutlookBarGroup group in y.NewItems)
                        {
                            regionTarget.Groups.Add(group);
                        }
                        break;
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                        foreach (OutlookBarGroup group in y.OldItems)
                        {
                            regionTarget.Groups.Remove(group);
                        }
                        break;                    
                }
            });
        }

        protected override IRegion CreateRegion()
        {
            return new SingleActiveRegion();
        }
    }
}
