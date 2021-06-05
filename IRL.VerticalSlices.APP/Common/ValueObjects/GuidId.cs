using System;
using Tactical.DDD;

namespace IRL.VerticalSlices.APP.Common.ValueObjects
{
    public class GuidId : EntityId
    {
        private Guid guidId;

        public GuidId()
        {
            this.guidId = Guid.NewGuid();
        }

        public GuidId(string id)
        {
            guidId = Guid.Parse(id);
        }

        public override string ToString() => guidId.ToString();
    }
}