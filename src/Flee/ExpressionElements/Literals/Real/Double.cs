using Flee.ExpressionElements.Base.Literals;
using Flee.InternalTypes;
using Flee.PublicTypes;
using System;
using System.Reflection.Emit;


namespace Flee.ExpressionElements.Literals.Real
{
    internal class DoubleLiteralElement : RealLiteralElement
    {
        private readonly double _myValue;

        private DoubleLiteralElement()
        {
        }

        public DoubleLiteralElement(double value)
        {
            _myValue = value;
        }

        public static DoubleLiteralElement Parse(string image, IServiceProvider services)
        {
            ExpressionParserOptions options = (ExpressionParserOptions)services.GetService(typeof(ExpressionParserOptions));
            DoubleLiteralElement element = new DoubleLiteralElement();

#pragma warning disable CS0168 // Variable is declared but never used
            try
            {
                double value = options.ParseDouble(image);
                return new DoubleLiteralElement(value);
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
            ilg.Emit(OpCodes.Ldc_R8, _myValue);
        }

        public override System.Type ResultType => typeof(double);
    }
}
