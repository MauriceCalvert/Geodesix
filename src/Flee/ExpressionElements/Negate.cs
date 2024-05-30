using Flee.ExpressionElements.Base;
using Flee.InternalTypes;
using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Flee.ExpressionElements
{
    internal class NegateElement : UnaryElement
    {
        public NegateElement()
        {
        }

        protected override System.Type GetResultType(System.Type childType)
        {
            TypeCode tc = Type.GetTypeCode(childType);

#pragma warning disable CS0618 // Type or member is obsolete
            MethodInfo mi = Utility.GetSimpleOverloadedOperator("UnaryNegation", childType, null);
#pragma warning restore CS0618 // Type or member is obsolete
            if ((mi != null))
            {
                return mi.ReturnType;
            }

            switch (tc)
            {
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Int32:
                case TypeCode.Int64:
                    return childType;
                case TypeCode.UInt32:
                    return typeof(Int64);
                default:
                    return null;
            }
        }

        public override void Emit(FleeILGenerator ilg, IServiceProvider services)
        {
            Type resultType = this.ResultType;
            MyChild.Emit(ilg, services);
            ImplicitConverter.EmitImplicitConvert(MyChild.ResultType, resultType, ilg);

#pragma warning disable CS0618 // Type or member is obsolete
            MethodInfo mi = Utility.GetSimpleOverloadedOperator("UnaryNegation", resultType, null);
#pragma warning restore CS0618 // Type or member is obsolete

            if (mi == null)
            {
                ilg.Emit(OpCodes.Neg);
            }
            else
            {
                ilg.Emit(OpCodes.Call, mi);
            }
        }
    }
}
