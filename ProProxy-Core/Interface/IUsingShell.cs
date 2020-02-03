using System;

namespace ProProxy
{
    public interface IUsingShell<T> : IShell
    {
        Func<T> UsingFunc { get; set; }
    }
}