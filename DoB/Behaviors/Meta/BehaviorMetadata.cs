using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Markup;

namespace DoB.Behaviors
{
    [ContentProperty("Behavior"), Obsolete("Only used by BehaviorSequence which is Obsolete")]
    public class BehaviorMetadata
    {
        public Behavior Behavior { get; set; }
        public double StartTimeMs { get; set; }
        public double LengthMs { get; set; }

        public BehaviorMetadata Clone()
        {
            var c = (BehaviorMetadata)this.MemberwiseClone();
            if (Behavior != null)
                c.Behavior = (Behavior)Behavior.Clone();
            return c;
        }
    }
}
