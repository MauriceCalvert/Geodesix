using Flee.ExpressionElements.Base;
using Flee.InternalTypes;
using System;
using System.Reflection.Emit;


namespace Flee.ExpressionElements.LogicalBitwise
{
    internal class NotElement : UnaryElement
    {
        public override void Emit(FleeILGenerator ilg, IServiceProvider services)
        {
            if (object.ReferenceEquals(MyChild.ResultType, typeof(bool)))
            {
                this.EmitLogical(ilg, services);
            }
            else
            {
                MyChild.Emit(ilg, services);
                ilg.Emit(OpCodes.Not);
            }
        }

        private void EmitLogical(FleeILGenerator ilg, IServiceProvider services)
        {
            MyChild.Emit(ilg, services);
            ilg.Emit(OpCodes.Ldc_I4_0);
            ilg.Emit(OpCodes.Ceq);
        }

        protected override System.Type GetResultType(System.Type childType)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            if (object.ReferenceEquals(childType, typeof(bool)))
            {
                return typeof(bool);
            }
            else if (Utility.IsIntegralType(childType) == true)
            {
                return childType;
            }
            else
            {
                return null;
            }
#pragma warning restore CS0618 // Type or member is obsolete
        }
    }
}
