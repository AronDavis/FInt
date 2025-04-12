public struct FInt
{
    #region Constants

    public const int PRICISION = 6;
	private const long _SCALE = 1_000_000;

    #endregion

    #region Properties

    private long _value;

    /// <summary>
    /// Represents the largest possible value of an <see cref="FInt"/>.  This field is constant.
    /// </summary>
    public static FInt MaxValue
	{
		get 
		{
			return new FInt(long.MaxValue, UseScale.None);
		}
	}

    /// <summary>
    /// Represents the smallest possible value of an <see cref="FInt"/>.  This field is constant.
    /// </summary>
    public static FInt MinValue
	{
		get 
		{
			return new FInt(long.MinValue, UseScale.None);
		}
	}

	/// <summary>
	/// Gets the decimal value of the number.
	/// </summary>
    public FInt Decimal
    {
        get
        {
			long scaledValue = _value % _SCALE;

			if(scaledValue < 0)
			{
                scaledValue = -scaledValue;
            }

            return new FInt(scaledValue % _SCALE, UseScale.None);
        }
    }

	/// <summary>
	/// Gets the sign of the number.
	/// </summary>
    public FInt Sign
    {
        get
        {
            if (_value >= 0)
            {
                return 1.FI();
            }
            else
            {
                return (-1).FI();
            }
        }
    }

	#endregion

	#region Private Enum

	/// <summary>
	/// This is used for cleanly distinguishing constructor signatures.
	/// </summary>
	private enum UseScale : byte
	{
		None
	}

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of <see cref="FInt"/> from a scaled <see cref="int"/> value.
    /// </summary>
    /// <param name="value">The value to convert to <see cref="FInt"/>.</param>
    public FInt(int value)
	{
		_value = value * _SCALE;
	}

    /// <summary>
    /// Initializes a new instance of <see cref="FInt"/> from a <see cref="long"/> value.
    /// </summary>
    /// <param name="value">The value to convert to <see cref="FInt"/>.</param>
	/// <exception cref="OverflowException">Will throw if <paramref name="value"/> overflows when scaled internally.</exception>
    public FInt(long value)
	{
        if (value > long.MaxValue / _SCALE || value < long.MinValue / _SCALE)
        {
            throw new OverflowException("FInt overflow on creation with scaling.");
        }

        _value = value * _SCALE;
    }

    /// <summary>
    /// Initializes a new instance of <see cref="FInt"/> from a raw internal value without applying scaling.
    /// Intended for internal use only.
    /// </summary>
    /// <param name="value">The value to convert to <see cref="FInt"/>.</param>
    /// <param name="_">This parameter only exists to distinguish from the public constructor <see cref="FInt(long)"/>.</param>
    private FInt(long value, UseScale _)
    {
        _value = value;
    }

    #endregion

    #region Operator Overloads

    public static FInt operator % (FInt left, FInt right)
	{
		return new FInt(left._value % right._value, UseScale.None);
	}

	#region Relational Operators

	public static bool operator == (FInt left, FInt right)
	{
		return left._value == right._value;
	}

	public static bool operator != (FInt left, FInt right)
	{
		return left._value != right._value;
	}

	public static bool operator >= (FInt left, FInt right)
	{
		return left._value >= right._value;
	}

	public static bool operator > (FInt left, FInt right)
	{
		return left._value > right._value;
	}

	public static bool operator <= (FInt left, FInt right)
	{
		return left._value <= right._value;
	}

	public static bool operator < (FInt left, FInt right)
	{
		return left._value < right._value;
	}

	#endregion

	#region Addition

	public static FInt operator + (FInt left, FInt right)
	{
		return new FInt(left._value + right._value, UseScale.None);
	}

	public static FInt operator + (FInt fi, int i)
	{
		return new FInt(fi._value + i * _SCALE, UseScale.None);
	}
	
	public static FInt operator + (int i, FInt fi) 
	{
		return fi + i; 
	}

	public static FInt operator + (FInt fi, long lng)
	{
		return new FInt(fi._value + lng * _SCALE, UseScale.None);
	}

	public static FInt operator + (long lng, FInt fi) 
	{ 
		return fi + lng; 
	}

    public static FInt operator ++(FInt value)
    {
        value += 1;
        return value;
    }

    #endregion

    #region Subtraction

    public static FInt operator - (FInt left, FInt right)
	{
		return new FInt(left._value - right._value, UseScale.None);
	}

	public static FInt operator - (FInt fi, int i)
	{
		return new FInt(fi._value - i * _SCALE, UseScale.None);
	}
	
	public static FInt operator - (int i, FInt fi) 
	{ 
		return fi - i; 
	}

	public static FInt operator - (FInt fi, long lng)
	{
		return new FInt(fi._value - lng * _SCALE, UseScale.None);
	}
	
	public static FInt operator - (long lng, FInt fi) 
	{ 
		return fi - lng; 
	}

    public static FInt operator --(FInt value)
    {
        value -= 1;
        return value;
    }

    #endregion

    public static FInt operator - (FInt value)
	{
		if (value._value == long.MinValue)
		{
			throw new OverflowException("FInt overflow on negative operator.");
		}

		return new FInt(-value._value, UseScale.None);
	}

	#region Multiplication

	public static FInt operator * (FInt left, FInt right)
	{
		//foil so we can have higher multiplication without overflow

		long leftWhole = left._value / _SCALE;
		long leftDecimal = left._value % _SCALE;

		long rightWhole = right._value / _SCALE;
		long rightDecimal = right._value % _SCALE;

		/*
		old method (so we can compare in tests)
		FInt m2 = new FInt((fi1.value * fi2.value)/(SCALE), false);

		Debug.Log(m1 + " vs " + m2 + "vs double: " + ((double)fi1.value/SCALE*(double)fi2.value/SCALE));
		*/

		//TODO: CHECK FOR OVERFLOW
		return new FInt(
			(leftWhole * rightWhole * _SCALE)
			+ (leftWhole * rightDecimal)
			+ (leftDecimal * rightWhole)
			+ ((leftDecimal * rightDecimal) / _SCALE)
			, UseScale.None
            );
	}

	public static FInt operator * (FInt fi1, int i)
	{
		return new FInt(fi1._value * i, UseScale.None);
	}

	public static FInt operator * (int i, FInt fi1) 
	{ 
		return fi1 * i; 
	}

	public static FInt operator * (FInt fi1, long lng)
	{
		return new FInt(fi1._value * lng, UseScale.None);
	}

	public static FInt operator * (long lng, FInt fi1) 
	{ 
		return fi1 * lng; 
	}

	#endregion

	#region Division

	public static FInt operator / (FInt left, FInt right)
	{
		if(right._value == 0)
		{
			throw new DivideByZeroException();
		}

		//TODO: overflow check +/- values that approach min/max values (like +right < 1)?

		long whole = left._value / right._value;
		long remainder = _SCALE * (left._value - (whole * right._value));  //TODO: if we wanted to round the last digit, we could * 10, round based on the last digit, then truncate the last digit (e.g. 2/3 -> 6666666 -> 6666670 -> 666667)
		remainder /= right._value;

		return new FInt(whole * _SCALE + remainder, UseScale.None);
	}

	public static FInt operator / (FInt left, int right)
	{
		return new FInt(left._value / right, UseScale.None);
	}

	public static FInt operator / (FInt left, long right)
	{
		return new FInt(left._value / right, UseScale.None);
	}

	#endregion

	#endregion

	public override int GetHashCode()
    {
		return _value.GetHashCode();
    }

	public override bool Equals(object obj)
    {
		if(obj is FInt fint)
        {
			return this == fint;
        }

		return false;
	}

	public override string ToString()
	{
		//if value is negative and it only has decimal digits
		if (_value < 0 && _value > -_SCALE)
		{
			return $"-{_value / _SCALE}." + (Abs(this)._value % _SCALE).ToString().PadLeft(PRICISION, '0');
		}
		else
		{
			return $"{_value / _SCALE}." + (Abs(this)._value % _SCALE).ToString().PadLeft(PRICISION, '0');
		}
	}

	#region Conversions

	/// <summary>
	/// Explicit <see cref="FInt"/> to <see cref="int"/> operator.
	/// </summary>
	/// <param name="value"></param>
	public static explicit operator int(FInt value)
	{
		long longValue = value._value / _SCALE;

		if (longValue > int.MaxValue || longValue < int.MinValue)
		{
            throw new OverflowException("Overflow when converting FInt to int.");
		}

		return (int)longValue;
	}

    /// <summary>
    /// Explicit <see cref="FInt"/> to <see cref="long"/> operator.
    /// </summary>
    /// <param name="value"></param>
    public static explicit operator long(FInt value)
	{
		return value._value / _SCALE;
	}

    /// <summary>
    /// Explicit <see cref="FInt"/> to <see cref="float"/> operator.
    /// </summary>
    /// <param name="value"></param>
    public static explicit operator float(FInt value)
	{
		return value._value / (float)_SCALE;
	}

    /// <summary>
    /// Explicit <see cref="FInt"/> to <see cref="double"/> operator.
    /// </summary>
    /// <param name="value"></param>
    public static explicit operator double(FInt value)
	{
		return value._value / (double)_SCALE;
	}

    
    /// <summary>
    /// Implicit <see cref="float"/> to <see cref="FInt"/> operator.
    /// </summary>
    /// <param name="value"></param>
    public static implicit operator FInt(float value)
	{
		return new FInt((long)(value * _SCALE), UseScale.None); //TODO: do this differently
	}

    /// <summary>
    /// Implicit <see cref="int"/> to <see cref="FInt"/> operator.
    /// </summary>
    /// <param name="value"></param>
    public static implicit operator FInt(int value)
	{
		return new FInt(value);
	}

    /// <summary>
    /// Implicit <see cref="long"/> to <see cref="FInt"/> operator.
    /// </summary>
    /// <param name="value"></param>
    public static implicit operator FInt(long value)
	{
		return new FInt(value);
	}

    #endregion

    #region Utility Functions

	/// <summary>
	/// Converts the string representation of a number to its <see cref="FInt"/> equivalent.
	/// </summary>
	/// <param name="s">A string containing a number to convert.</param>
	/// <returns></returns>
    public static FInt Parse(string s)
    {
        return new FInt(long.Parse(s));
    }

    /// <summary>
    /// Used to make fractional values.
    /// </summary>
    /// <param name="numerator"></param>
    /// <param name="denominator"></param>
    /// <returns></returns>
    public static FInt MakeFraction(int numerator, int denominator) //TODO: bake this into a constructor?
    {
        return numerator.FI() / denominator.FI();
    }

    /// <summary>
    /// Returns the smaller of <paramref name="val1"/> and <paramref name="val2"/>.
    /// </summary>
    /// <param name="val1"></param>
    /// <param name="val2"></param>
    /// <returns></returns>
    public static FInt Min(FInt val1, FInt val2)
	{
		if (val1 < val2)
		{
			return val1;
		}
		else
		{
			return val2;
		}
	}

    /// <summary>
    /// Returns the larger of <paramref name="val1"/> and <paramref name="val2"/>.
    /// </summary>
    /// <param name="val1"></param>
    /// <param name="val2"></param>
    /// <returns></returns>
    public static FInt a(FInt val1, FInt val2)
	{
		if (val1 > val2)
		{
			return val1;
		}
		else
		{
			return val2;
		}
	}

	/// <summary>
	/// Returns the absolute value of <paramref name="value"/>.
	/// </summary>
	/// <param name="value"></param>
	/// <returns></returns>
	public static FInt Abs(FInt value)
	{
		if (value._value < 0)
		{
			if (value._value == long.MinValue)
			{
				throw new OverflowException("Cannot get absolute value of minimum value.");
			}

			return new FInt(-value._value, UseScale.None);
		}

		return value;
	}

	/// <summary>
	/// Rounds <paramref name="value"/> to the nearest integral value.
	/// </summary>
	/// <param name="value"></param>
	/// <returns></returns>
	public static FInt Round(FInt value) //TODO: THIS WON'T WORK WITH NEGATIVE NUMBERS
	{
		FInt fiDecimal = value.Decimal;

		if (fiDecimal >= MakeFraction(1, 2))
		{
			return (value - fiDecimal) + 1;
		}

		return value - fiDecimal;
	}

	/// <summary>
	/// Returns the square root of <paramref name="value"/>.
	/// </summary>
	/// <param name="value"></param>
	/// <returns></returns>
	public static FInt Sqrt(FInt value)
	{
		if (value._value == 0)
		{
			return new FInt(0);
		}

        //---NEWTON'S EQUATION---------- //http://en.wikipedia.org/wiki/Newton%27s_method#Square%5Froot%5Fof%5Fa%5Fnumber
        //f(x) = x^2 - NUMBER_TO_Sqrt
        //(derivative) f'(x) = 2x

        //loop through this multiple times to get more accurate results
        //prevX - f(prevX)/f'(prevX)

        FInt prev = new FInt(1);

		for (int i = 0; i < 20; i++) //could probably go as low as 5 iterations
		{
			prev -= ((prev * prev - value) / (2 * prev));
		}

		//Round off at the end
		return prev;
	}

	/// <summary>
	/// Returns the sine of the specified angle <paramref name="a"/>.
	/// </summary>
	/// <param name="a"></param>
	/// <returns></returns>
	public static FInt Sin(FInt a)
	{
		return new FInt(_sinLookUp((int)a), UseScale.None);
	}

    /// <summary>
    /// Returns the cosine of the specified angle <paramref name="a"/>.
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public static FInt Cos(FInt a)
	{
		return new FInt(_cosLookUp((int)a), UseScale.None);
	}

    /// <summary>
    /// Returns the tangent of the specified angle <paramref name="a"/>.
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public static FInt Tan(FInt a)
	{
		return Sin(a) / Cos(a);
	}

    #region Sin/Cos Helper Functions

    /// <summary>
    /// Look up the sine of the angle.
    /// </summary>
    /// <param name="deg">The angle in degrees.</param>
    /// <returns>Sine of the angle.</returns>
    private static int _sinLookUp(int deg)
	{
		deg %= 360;
		int sin = 0;

		//get the quadrant (0 based)
		int quad = deg / 90;

		//break it down relative to quadrant
		int i = deg % 90;

		if (quad % 2 == 0) //quad 1 or 3
		{
			if (i <= 45)
			{
				sin = _sinTable[i]; //if it's 45 or less, use sine table
			}
			else
			{
				sin = _cosTable[90 - i]; //else use cosine table
			}
		}
		else
		{
			if (i <= 45)
			{
				sin = _cosTable[i]; //if it's 45 or less, use cosine table
			}
			else
			{
				sin = _sinTable[90 - i]; //else use sine table
			}
		}

		//sin is negative in third and fourth quadrant
		if (deg > 180)
		{
			sin = -sin;
		}

		return sin;
	}

    /// <summary>
    /// Look up the cosine of the angle.
    /// </summary>
    /// <param name="deg">The angle in degrees.</param>
    /// <returns>Cosine of the angle.</returns>
    private static int _cosLookUp(int deg)
	{
		deg %= 360;
		int cos = 0;

		//get the quadrant (0 based)
		int quad = deg / 90;

		//break it down relative to quadrant
		int i = deg % 90;

		if (quad % 2 == 0) //quad 1 or 3
		{
			if (i <= 45)
			{
				cos = _cosTable[i]; //if it's 45 or less, use cosine table
			}
			else
			{
				cos = _sinTable[90 - i]; //else use sine table
			}
		}
		else
		{
			if (i <= 45)
			{
				cos = _sinTable[i]; //if it's 45 or less, use sine table
			}
			else
			{
				cos = _cosTable[90 - i]; //else use cosine table
			}
		}

		//cos is negative in second and third quadrant
		if (deg > 90 && deg <= 270)
		{
			cos = -cos;
		}

		return cos;
	}


    //TODO: ?could just do sin 0-90 and then use that for cos 0-90?

    /// <summary>
    /// Sine value are stored by index where the index is the angle in degrees.  Values are multiplied by 100000 to keep 5 digit decimal precision;
    /// </summary>
    private static int[] _sinTable = new int[]
	{
		0,17452,34899,52336,69756,87156,	//00-05
		104528,121869,139173,156434,173648,	//06-10
		190809,207912,224951,241922,258819,	//11-15
		275637,292372,309017,325568,342020,	//16-20
		358368,374607,390731,406737,422618,	//21-25
		438371,453990,469472,484810,500000,	//26-30
		515038,529919,544639,559193,573576,	//31-35
		587785,601815,615661,629320,642788,	//36-40
		656059,669131,681998,694658,707107	//41-45
	};

    /// <summary>
    /// Cosine value are stored by index where the index is the angle in degrees.  Values are multiplied by 100000 to keep 5 digit decimal precision;
    /// </summary>
    private static int[] _cosTable = new int[]
	{
		1000000,999848,999391,998630,997564,996195,	//00-05
		994522,992546,990268,987688,984808,			//06-10
		981627,978148,974370,970296,965926,			//11-15
		961262,956305,951057,945519,939693,			//16-20
		933580,927184,920505,913545,906308,			//21-25
		898794,891007,882948,874620,866025,			//26-30
		857167,848048,838671,829038,819152,			//31-35
		809017,798636,788011,777146,766044,			//36-40
		754710,743145,731354,719340,707107			//41-45
	};

	#endregion

	#endregion
}

public static class FIntQoLExtensions
{
	public static FInt FI(this int i)
	{
		return new FInt(i);
	}
}
