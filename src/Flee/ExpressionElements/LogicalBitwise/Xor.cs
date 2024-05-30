using Flee.ExpressionElements.Base;
using Flee.InternalTypes;
using System;
using System.Reflection.Emit;


namespace Flee.ExpressionElements.LogicalBitwise
{
#pragma warning disable CS0618 // Type or member is obsolete
    internal class XorElement : BinaryExpressionElement
#pragma warning restore CS0618 // Type or member is obsolete
    {
        protected override System.Type GetResultType(System.Type leftType, System.Type rightType)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            Type bitwiseType = Utility.GetBitwiseOpType(leftType, rightType);
#pragma warning restore CS0618 // Type or member is obsolete

            if ((bitwiseType != null))
            {
                return bitwiseType;
            }
            else if (this.AreBothChildrenOfType(typeof(bool)) == true)
            {
                return typeof(bool);
            }
            else
            {
                return null;
            }
        }

        public override void Emit(FleeILGenerator ilg, IServiceProvider services)
        {
            Type resultType = this.ResultType;

            MyLeftChild.Emit(ilg, services);
            ImplicitConverter.EmitImplicitConvert(MyLeftChild.ResultType, resultType, ilg);
            MyRightChild.Emit(ilg, services);
            ImplicitConverter.EmitImplicitConvert(MyRightChild.ResultType, resultType, ilg);
            ilg.Emit(OpCodes.Xor);
        }


        protected override void GetOperation(object operation)
        {
        }
    }
}
