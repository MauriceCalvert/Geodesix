using Flee.ExpressionElements.Base.Literals;
using Flee.InternalTypes;
using Flee.PublicTypes;
using System;
using System.Reflection;
using System.Reflection.Emit;


namespace Flee.ExpressionElements.Literals.Real
{
    internal class DecimalLiteralElement : RealLiteralElement
    {
        private static readonly ConstructorInfo OurConstructorInfo = GetConstructor();
        private readonly decimal _myValue;

        private DecimalLiteralElement()
        {
        }

        public DecimalLiteralElement(decimal value)
        {
            _myValue = value;
        }

        private static ConstructorInfo GetConstructor()
        {
            Type[] types = {
            typeof(Int32),
            typeof(Int32),
            typeof(Int32),
            typeof(bool),
            typeof(byte)
        };
            return typeof(decimal).GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, CallingConventions.Any, types, null);
        }

        public static DecimalLiteralElement Parse(string image, IServiceProvider services)
        {
            ExpressionParserOptions options = (ExpressionParserOptions)services.GetService(typeof(ExpressionParserOptions));
            DecimalLiteralElement element = new DecimalLiteralElement();

#pragma warning disable CS0168 // Variable is declared but never used
            try
            {
                decimal value = options.ParseDecimal(image);
                return new DecimalLiteralElement(value);
            }
            catch (OverflowException ex)
            {
                element.OnParseOverflow(image);
                return null;
            }
#pragma warning restore CS0168 // Variable is declared but never used
        }

        public override void Emit(FleeILGenerator ilg, IServiceProvider services)
        {
            int index = ilg.GetTempLocalIndex(typeof(decimal));
#pragma warning disable CS0618 // Type or member is obsolete
            Utility.EmitLoadLocalAddress(ilg, index);
#pragma warning restore CS0618 // Type or member is obsolete

            int[] bits = decimal.GetBits(_myValue);
            EmitLoad(bits[0], ilg);
            EmitLoad(bits[1], ilg);
            EmitLoad(bits[2], ilg);

            int flags = bits[3];

            EmitLoad((flags >> 31) == -1, ilg);

            EmitLoad(flags >> 16, ilg);

            ilg.Emit(OpCodes.Call, OurConstructorInfo);

#pragma warning disable CS0618 // Type or member is obsolete
            Utility.EmitLoadLocal(ilg, index);
#pragma warning restore CS0618 // Type or member is obsolete
        }

        public override System.Type ResultType => typeof(decimal);
    }
}
