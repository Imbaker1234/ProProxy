using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using ImpromptuInterface;
using TypeCache;

namespace ProProxy
{
    public abstract class Proxy<T> : DynamicObject, IProxy<T> where T : class
    {
        internal T InnerSubject { get; set; }
        protected string InnerSubjectName { get; set; }
        protected Dictionary<string, dynamic> DelegateCache { get; }

        protected dynamic PreDelegate { get; set; }
        protected dynamic PostDelegate { get; set; }
        protected dynamic CatchDelegate { get; set; }

        protected Proxy(T innerSubject)
        {
            InnerSubject = innerSubject;
            InnerSubjectName = typeof(T).ToString().Split('+').Last();
            DelegateCache = DelegateCacheFactory.Create(InnerSubject);
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            result = DelegateCache[binder.Name];
            return true;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = DelegateCache["get_" + binder.Name].FastDynamicInvoke();
            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            DelegateCache["set_" + binder.Name].FastDynamicInvoke(value);
            return true;
        }

        public override string ToString()
        {
            return InnerSubject.ToString();
        }

        public override bool Equals(object obj)
        {
            return InnerSubject.Equals(obj);
        }

        public override int GetHashCode()
        {
            return InnerSubject.GetHashCode();
        }

        public static void ValidateInterface(Type innerSubject, Type outerInterface)
        {
            if (!outerInterface.IsInterface) throw new ArgumentException("As<I>: 'I' must be an Interface!", "Type<I>");
            if (!innerSubject.IsAssignableFrom(outerInterface))
                throw new ArgumentException(
                    "As<I>: InnerSubject must implement 'I' interface!", nameof(innerSubject));
        }
    }
}