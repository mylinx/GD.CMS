using Castle.DynamicProxy;
using System;

namespace GD.CMS.Common
{
    public abstract class BaseFilterAttribute : Attribute, IFilter
    {
        public abstract void OnActionExecuted(IInvocation invocation);
        public abstract void OnActionExecuting(IInvocation invocation);
    }
}
