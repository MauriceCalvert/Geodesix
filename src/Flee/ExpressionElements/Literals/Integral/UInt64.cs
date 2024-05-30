using Flee.ExpressionElements.Base.Literals;
using Flee.InternalTypes;
using System;


namespace Flee.ExpressionElements.Literals.Integral
{
    internal class UInt64LiteralElement : IntegralLiteralElement
    {
        private readonly UInt64 _myValue;
        public UInt64LiteralElement(string image, System.Globalization.NumberStyles ns)
        {
#pragma warning disable CS0168 // Variable is declared but never used
            try
            {
                _myValue = UInt64.Parse(image, ns);
            }
            catch (OverflowException ex)
            {
                base.OnParseOverflow(image);
            }
#pragma warning restore CS0168 // Variable is declared but never used
        }

        public UInt64LiteralElement(UInt64 value)
        {
            _myValue = value;
        }

        public override void Emit(FleeILGenerator ilg, IServiceProvider services)
        {
            EmitLoad(Convert.ToInt64(_myValue), ilg);
        }

        public override System.Type ResultType => typeof(UInt64);
    }
}
