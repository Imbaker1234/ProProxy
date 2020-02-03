using System;
using System.Dynamic;
using Dynamitey;
using ImpromptuInterface;


namespace ProProxy
{
    public class UsingProxy<TInnerSubject, TUsing> : Proxy<TInnerSubject>, IUsingProxy<TUsing>
        where TInnerSubject : class where TUsing : IDisposable
    {
        public Action<Type, dynamic, dynamic, dynamic> UsingFunc { get; set; }
        
        //Create New is Private
        private UsingProxy(Action<Type, dynamic, dynamic, dynamic> usingFunc, TInnerSubject innerSubject) : base(innerSubject)
        {
            UsingFunc = usingFunc;
            InnerSubject = innerSubject;
        }

        //Shell Constructor
        // public static I As<I>(IUsingShell<TUsing> shell, TInnerSubject instance = null) where I : class
        // {
            // return As<I>(shell.UsingFunc, instance);
        // }

        //Individual Value Constructor
        // public static I As<I>(Action<Type> usingFunc, TInnerSubject instance = null) where I : class
        // {
            // ValidateInterface(typeof(TInnerSubject), typeof(I), instance);

            // var product = new UsingProxy<TInnerSubject, TUsing>(usingFunc, instance).ActLike<I>();

            // return product;
        // }

        //Proxy Existing
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            var disposable2 = Activator.CreateInstance<TUsing>();

            dynamic usingFunc;
                
                Action actor = () => Console.WriteLine("Did it");
                Action<string, int> actor2 = (a, b) => Console.WriteLine($"{a}, {b}");
                usingFunc = actor2;

                usingFunc.Invoke();


                    result = DelegateCache[binder.Name].FastDynamicInvoke(args);

            // disposable.Dispose();

            return true;
        }
    }
}