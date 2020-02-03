using System;

namespace ProProxy
{
    public interface IUsingProxy<TUsing> : IProxy<TUsing> where TUsing : IDisposable
    {
        Action<Type, dynamic, dynamic, dynamic> UsingFunc { get; set; }
    }
}