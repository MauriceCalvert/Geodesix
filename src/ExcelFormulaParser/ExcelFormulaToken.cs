namespace ExcelFormulaParser
{
    public class ExcelFormulaToken
    {

        private string value;
        private ExcelFormulaTokenType type;
        private ExcelFormulaTokenSubtype subtype;

        private ExcelFormulaToken() { }

        internal ExcelFormulaToken(string value, ExcelFormulaTokenType type) : this(value, type, ExcelFormulaTokenSubtype.Nothing) { }

        internal ExcelFormulaToken(string value, ExcelFormulaTokenType type, ExcelFormulaTokenSubtype subtype)
        {
            this.value = value;
            this.type = type;
            this.subtype = subtype;
        }

        public string Value
        {
            get { return value; }
            internal set { this.value = value; }
        }

        public ExcelFormulaTokenType Type
        {
            get { return type; }
            internal set { type = value; }
        }

        public ExcelFormulaTokenSubtype Subtype
        {
            get { return subtype; }
            internal set { subtype = value; }
        }

    }

    public enum ExcelFormulaTokenType
    {
        Noop,
        Operand,
        Function,
        Subexpression,
        Argument,
        OperatorPrefix,
        OperatorInfix,
        OperatorPostfix,
        Whitespace,
        Unknown
    };

    public enum ExcelFormulaTokenSubtype
    {
        Nothing,
        Start,
        Stop,
        Text,
        Number,
        Logical,
        Error,
        Range,
        Math,
        Concatenation,
        Intersection,
        Union
    };

}
