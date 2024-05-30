using Flee.InternalTypes;
using System.Globalization;

namespace Flee.PublicTypes
{
    public class ExpressionParserOptions
    {
#pragma warning disable CS0618 // Type or member is obsolete
        private PropertyDictionary _myProperties;
#pragma warning restore CS0618 // Type or member is obsolete
        private readonly ExpressionContext _myOwner;
        private readonly CultureInfo _myParseCulture;

        private NumberStyles NumberStyles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent | NumberStyles.None;
        internal ExpressionParserOptions(ExpressionContext owner)
        {
            _myOwner = owner;
#pragma warning disable CS0618 // Type or member is obsolete
            _myProperties = new PropertyDictionary();
#pragma warning restore CS0618 // Type or member is obsolete
            _myParseCulture = (CultureInfo)CultureInfo.InvariantCulture.Clone();
            this.InitializeProperties();
        }

        #region "Methods - Public"

        public void RecreateParser()
        {
            _myOwner.RecreateParser();
        }

        #endregion

        #region "Methods - Internal"

        internal ExpressionParserOptions Clone()
        {
            ExpressionParserOptions copy = (ExpressionParserOptions)this.MemberwiseClone();
            copy._myProperties = _myProperties.Clone();
            return copy;
        }

        internal double ParseDouble(string image)
        {
            return double.Parse(image, NumberStyles, _myParseCulture);
        }

        internal float ParseSingle(string image)
        {
            return float.Parse(image, NumberStyles, _myParseCulture);
        }

        internal decimal ParseDecimal(string image)
        {
            return decimal.Parse(image, NumberStyles, _myParseCulture);
        }
        #endregion

        #region "Methods - Private"

        private void InitializeProperties()
        {
            this.DateTimeFormat = "dd/MM/yyyy";
            this.RequireDigitsBeforeDecimalPoint = false;
            this.DecimalSeparator = '.';
            this.FunctionArgumentSeparator = ',';
        }

        #endregion

        #region "Properties - Public"

        public string DateTimeFormat
        {
            get { return _myProperties.GetValue<string>("DateTimeFormat"); }
            set { _myProperties.SetValue("DateTimeFormat", value); }
        }

        public bool RequireDigitsBeforeDecimalPoint
        {
            get { return _myProperties.GetValue<bool>("RequireDigitsBeforeDecimalPoint"); }
            set { _myProperties.SetValue("RequireDigitsBeforeDecimalPoint", value); }
        }

        public char DecimalSeparator
        {
            get { return _myProperties.GetValue<char>("DecimalSeparator"); }
            set
            {
                _myProperties.SetValue("DecimalSeparator", value);
                _myParseCulture.NumberFormat.NumberDecimalSeparator = value.ToString();
            }
        }

        public char FunctionArgumentSeparator
        {
            get { return _myProperties.GetValue<char>("FunctionArgumentSeparator"); }
            set { _myProperties.SetValue("FunctionArgumentSeparator", value); }
        }

        #endregion
    }
}
