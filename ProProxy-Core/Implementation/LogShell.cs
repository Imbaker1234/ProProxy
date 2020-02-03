using System;
using System.Dynamic;

namespace ProProxy
{
    public class LogShell : IShell
    {
        public Action<string, InvokeMemberBinder, object[]> PreDelegate, PostDelegate;

        public Action<string, InvokeMemberBinder, object[], Exception> CatchDelegate;
        public Type CorrespondingProxy { get; set; } = typeof(LogProxy<>);

        public LogShell(Action<string, InvokeMemberBinder, object[]> preDelegate, Action<string, InvokeMemberBinder, object[]> postDelegate, Action<string, InvokeMemberBinder, object[], Exception> catchDelegate)
        {
            PreDelegate = preDelegate;
            PostDelegate = postDelegate;
            CatchDelegate = catchDelegate;
        }

    }
}