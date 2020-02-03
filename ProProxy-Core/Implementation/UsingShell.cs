using System;

namespace ProProxy
{
    public class UsingShell<T> : IUsingShell<T>
    {
        public Type CorrespondingProxy { get; set; }
        public Func<T> UsingFunc { get; set; }

        public UsingShell(Func<T> usingFunc)
        {
            UsingFunc = usingFunc;
        }
    }
}