﻿using Flee.ExpressionElements.Base;
using Flee.InternalTypes;
using System;


namespace Flee.ExpressionElements.MemberElements
{
    internal class ExpressionMemberElement : MemberElement
    {
        private readonly ExpressionElement _myElement;
        public ExpressionMemberElement(ExpressionElement element)
        {
            _myElement = element;
        }

        protected override void ResolveInternal()
        {
        }

        public override void Emit(FleeILGenerator ilg, IServiceProvider services)
        {
            base.Emit(ilg, services);
            _myElement.Emit(ilg, services);
            if (_myElement.ResultType.IsValueType == true)
            {
                EmitValueTypeLoadAddress(ilg, this.ResultType);
            }
        }

        protected override bool SupportsInstance => true;

        protected override bool IsPublic => true;

        public override bool IsStatic => false;
        public override bool IsExtensionMethod => false;

        public override System.Type ResultType => _myElement.ResultType;
    }
}
