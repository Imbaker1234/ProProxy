using System;
using System.Dynamic;

namespace ProProxy
{
    public class LogProxy<T> : Proxy<T> where T : class
    {
        public LogProxy(Action<string, InvokeMemberBinder, object[]> logOnEntry,
            Action<string, InvokeMemberBinder, object[]> logOnExit,
            Action<string, InvokeMemberBinder, object[], Exception> logOnFailure,
            T innerSubject) : base(innerSubject)
        {
            PreDelegate = logOnEntry;
            PostDelegate = logOnExit;
            CatchDelegate = logOnFailure;
        }

        public LogProxy(LogShell shell, T innerSubject) : base(innerSubject)
        {
            PreDelegate = shell.PreDelegate;
            PostDelegate = shell.PostDelegate;
            CatchDelegate = shell.CatchDelegate;
        }


        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            try
            {
                PreDelegate?.Invoke(InnerSubjectName, binder, args);

                base.TryInvokeMember(binder, args, out result);

                PostDelegate?.Invoke(InnerSubjectName, binder, args);

                return true;
            }
            catch (Exception e)
            {
                CatchDelegate?.Invoke(InnerSubjectName, binder, args, e);
                throw;
            }
        }
    }
}