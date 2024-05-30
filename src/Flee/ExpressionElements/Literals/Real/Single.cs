using Flee.ExpressionElements.Base.Literals;
using Flee.InternalTypes;
using Flee.PublicTypes;
using System;
using System.Reflection.Emit;

namespace Flee.ExpressionElements.Literals.Real
{
    internal class SingleLiteralElement : RealLiteralElement
    {
        private readonly float _myValue;

        private SingleLiteralElement()
        {
        }

        public SingleLiteralElement(float value)
        {
            _myValue = value;
        }

        public static SingleLiteralElement Parse(string image, IServiceProvider services)
        {
            ExpressionParserOptions options = (ExpressionParserOptions)services.GetService(typeof(ExpressionParserOptions));
            SingleLiteralElement element = new SingleLiteralElement();

#pragma warning disable CS0168 // Variable is declared but never used
            try
            {
                float value = options.ParseSingle(image);
                return new SingleLiteralElement(value);
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
            ilg.Emit(OpCodes.Ldc_R4, _myValue);
        }

        public override System.Type ResultType => typeof(float);
    }
}
