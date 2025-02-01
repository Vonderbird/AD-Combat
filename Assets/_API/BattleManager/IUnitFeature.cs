using System;

namespace ADC
{
    public interface IUnitFeature<T1, T2>
    {
        public T1 Value { get; set; }
        event EventHandler<T2> Changed;
    }
}