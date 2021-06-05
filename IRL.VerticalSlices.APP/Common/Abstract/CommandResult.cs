using System;

namespace IRL.VerticalSlices.APP.Common.Abstract
{
    public abstract class CommandResult<T>
    {
        public T Payload { get; set; }
        public string Error { get; set; }
        public bool IsSuccess => String.IsNullOrEmpty(Error);
    }
}