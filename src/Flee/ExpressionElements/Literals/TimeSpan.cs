using Flee.ExpressionElements.Base.Literals;
using Flee.InternalTypes;
using Flee.PublicTypes;
using Flee.Resources;
using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Flee.ExpressionElements.Literals
{
    internal class TimeSpanLiteralElement : LiteralElement
    {
        private TimeSpan _myValue;
        public TimeSpanLiteralElement(string image)
        {
            if (TimeSpan.TryParse(image, out _myValue) == false)
            {
                base.ThrowCompileException(CompileErrorResourceKeys.CannotParseType, CompileExceptionReason.InvalidFormat, typeof(TimeSpan).Name);
            }
        }

        public override void Emit(FleeILGenerator ilg, System.IServiceProvider services)
        {
            int index = ilg.GetTempLocalIndex(typeof(TimeSpan));

#pragma warning disable CS0618 // Type or member is obsolete
            Utility.EmitLoadLocalAddress(ilg, index);
#pragma warning restore CS0618 // Type or member is obsolete

            LiteralElement.EmitLoad(_myValue.Ticks, ilg);

            ConstructorInfo ci = typeof(TimeSpan).GetConstructor(new Type[] { typeof(long) });

            ilg.Emit(OpCodes.Call, ci);

#pragma warning disable CS0618 // Type or member is obsolete
            Utility.EmitLoadLocal(ilg, index);
#pragma warning restore CS0618 // Type or member is obsolete
        }

        public override System.Type ResultType => typeof(TimeSpan);
    }
}
