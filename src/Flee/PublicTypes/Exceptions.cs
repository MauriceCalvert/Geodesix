﻿using Flee.InternalTypes;
using Flee.Parsing;
using Flee.Resources;
using System;

namespace Flee.PublicTypes
{
    public enum CompileExceptionReason
    {
        SyntaxError,
        ConstantOverflow,
        TypeMismatch,
        UndefinedName,
        FunctionHasNoReturnValue,
        InvalidExplicitCast,
        AmbiguousMatch,
        AccessDenied,
        InvalidFormat
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable()]
    public sealed class ExpressionCompileException : Exception
    {
        private readonly CompileExceptionReason _myReason;
        internal ExpressionCompileException(string message, CompileExceptionReason reason) : base(message)
        {
            _myReason = reason;
        }

        internal ExpressionCompileException(ParserLogException parseException) : base(string.Empty, parseException)
        {
            _myReason = CompileExceptionReason.SyntaxError;
        }

        private ExpressionCompileException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
            _myReason = (CompileExceptionReason)info.GetInt32("Reason");
        }

        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Reason", Convert.ToInt32(_myReason));
        }

        public override string Message
        {
            get
            {
                if (_myReason == CompileExceptionReason.SyntaxError)
                {
                    Exception innerEx = this.InnerException;
#pragma warning disable CS0618 // Type or member is obsolete
                    string msg = $"{Utility.GetCompileErrorMessage(CompileErrorResourceKeys.SyntaxError)}: {innerEx.Message}";
#pragma warning restore CS0618 // Type or member is obsolete
                    return msg;
                }
                else
                {
                    return base.Message;
                }
            }
        }

        public CompileExceptionReason Reason => _myReason;
    }
}
