using Flee.ExpressionElements.Base.Literals;
using Flee.InternalTypes;
using Flee.PublicTypes;
using Flee.Resources;
using System;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;

namespace Flee.ExpressionElements.Literals
{
    internal class DateTimeLiteralElement : LiteralElement
    {
        private DateTime _myValue;
        public DateTimeLiteralElement(string image, ExpressionContext context)
        {
            ExpressionParserOptions options = context.ParserOptions;

            if (DateTime.TryParseExact(image, options.DateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out _myValue) == false)
            {
                base.ThrowCompileException(CompileErrorResourceKeys.CannotParseType, CompileExceptionReason.InvalidFormat, typeof(DateTime).Name);
            }
        }

        public override void Emit(FleeILGenerator ilg, IServiceProvider services)
        {
            int index = ilg.GetTempLocalIndex(typeof(DateTime));

#pragma warning disable CS0618 // Type or member is obsolete
            Utility.EmitLoadLocalAddress(ilg, index);
#pragma warning restore CS0618 // Type or member is obsolete

            LiteralElement.EmitLoad(_myValue.Ticks, ilg);

            ConstructorInfo ci = typeof(DateTime).GetConstructor(new Type[] { typeof(long) });

            ilg.Emit(OpCodes.Call, ci);

#pragma warning disable CS0618 // Type or member is obsolete
            Utility.EmitLoadLocal(ilg, index);
#pragma warning restore CS0618 // Type or member is obsolete
        }

        public override System.Type ResultType => typeof(DateTime);
    }
}
