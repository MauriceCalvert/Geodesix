﻿using Flee.ExpressionElements.Base.Literals;
using Flee.InternalTypes;
using System;
using System.Reflection.Emit;

namespace Flee.ExpressionElements.Literals
{
    internal class NullLiteralElement : LiteralElement
    {
        public override void Emit(FleeILGenerator ilg, IServiceProvider services)
        {
            ilg.Emit(OpCodes.Ldnull);
        }

        public override System.Type ResultType => typeof(Null);
    }
}
