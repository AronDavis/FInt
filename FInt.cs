using System;﻿
using UnityEngine;

public struct FInt
{
	private const int _PRICISION = 6;
	private const long _SCALE = 1000000;
	private long _value;

	#region Properties
	public static FInt MaxValue
	{
		get 
		{ 
			return new FInt(long.MaxValue, false); 
		}
	}

	public static FInt MinValue
	{
		get 
		{ 
			return new FInt(long.MinValue, false); 
		}
	}
	#endregion

	#region Constructors
	public FInt(int i)
	{
		_value = i * _SCALE;

		/* //scale should be a bool parameter that is optional "bool scale = true"
		if (scale) value = i * SCALE;
		else value = i;
		*/
	}

	public FInt(int i, bool setFalse) //bool is to get around a bug
	{
		_value = i;
	}

	public FInt(long lng)
	{
		if (lng > long.MaxValue / _SCALE || lng < long.MinValue / _SCALE)
		{
			//TODO: throw argument exception instead
			Debug.LogError("FInt Overflow on creation with scaling");
		}

		_value = lng * _SCALE;

		/* //scale should be a bool parameter that is optional "bool scale = true"
		if (scale)
		{
			if(lng > long.MaxValue / SCALE || lng < long.MinValue / SCALE) 
				Debug.LogError("FInt Overflow on creation with scaling");

			value = lng * SCALE;
		}
		else value = lng;*/
	}

	public FInt(long lng, bool setFalse) //bool is to get around a bug
	{
		_value = lng;
	}

	#endregion

	#region Operator Overloads

	public static FInt operator % (FInt fi1, FInt fi2)
	{
		return new FInt(fi1._value % fi2._value, false);
	}

	#region Relational Operators
	public static bool operator == (FInt fi1, FInt fi2)
	{
		return fi1._value == fi2._value;
	}

	public static bool operator != (FInt fi1, FInt fi2)
	{
		return fi1._value != fi2._value;
	}

	public static bool operator >= (FInt fi1, FInt fi2)
	{
		return fi1._value >= fi2._value;
	}

	public static bool operator > (FInt fi1, FInt fi2)
	{
		return fi1._value > fi2._value;
	}

	public static bool operator <= (FInt fi1, FInt fi2)
	{
		return fi1._value <= fi2._value;
	}

	public static bool operator < (FInt fi1, FInt fi2)
	{
		return fi1._value < fi2._value;
	}
	#endregion

	#region Addition
	public static FInt operator + (FInt fi1, FInt fi2)
	{
		return new FInt(fi1._value + fi2._value, false);
	}

	public static FInt operator + (FInt fi, int i)
	{
		return new FInt(fi._value + i * _SCALE, false);
	}
	
	public static FInt operator + (int i, FInt fi) 
	{ 
		return fi + i; 
	}

	public static FInt operator + (FInt fi, long lng)
	{
		return new FInt(fi._value + lng * _SCALE, false);
	}

	public static FInt operator + (long lng, FInt fi) 
	{ 
		return fi + lng; 
	}

	#endregion

	//++
	public static FInt operator ++ (FInt fi)
	{
		fi += 1;
		return fi;
	}

	#region Subtraction
	public static FInt operator - (FInt fi1, FInt fi2)
	{
		return new FInt(fi1._value - fi2._value, false);
	}

	public static FInt operator - (FInt fi, int i)
	{
		return new FInt(fi._value - i * _SCALE, false);
	}
	
	public static FInt operator - (int i, FInt fi) 
	{ 
		return fi - i; 
	}

	public static FInt operator - (FInt fi, long lng)
	{
		return new FInt(fi._value - lng * _SCALE, false);
	}
	
	public static FInt operator - (long lng, FInt fi) 
	{ 
		return fi - lng; 
	}
	#endregion

	//--
	public static FInt operator -- (FInt fi)
	{
		fi -= 1;
		return fi;
	}

	//Negative
	public static FInt operator - (FInt fi)
	{
		if (fi._value == long.MinValue)
		{
			Debug.LogError("FInt Overflow on negative operator");
		}
		return new FInt(-fi._value, false);
	}

	#region Multiplication
	public static FInt operator * (FInt fi1, FInt fi2)
	{
		//foil so we can have higher multiplication without overflow

		long fi1Whole = fi1._value / _SCALE;
		long fi1Decimal = fi1._value % _SCALE;

		long fi2Whole = fi2._value / _SCALE;
		long fi2Decimal = fi2._value % _SCALE;

		/*
		FInt m1 = new FInt(
		(fi1Whole * fi2Whole * SCALE)
		+ (fi1Whole * fi2Decimal) 
		+ (fi1Decimal * fi2Whole)
		+ ((fi1Decimal * fi2Decimal) / SCALE)
		, false);

		old method
		FInt m2 = new FInt((fi1.value * fi2.value)/(SCALE), false);

		Debug.Log(m1 + " vs " + m2 + "vs double: " + ((double)fi1.value/SCALE*(double)fi2.value/SCALE));
		*/

		//TODO: CHECK FOR OVERFLOW
		return new FInt(
			(fi1Whole * fi2Whole * _SCALE)
			+ (fi1Whole * fi2Decimal)
			+ (fi1Decimal * fi2Whole)
			+ ((fi1Decimal * fi2Decimal) / _SCALE)
			, false);
	}

	public static FInt operator * (FInt fi1, int i)
	{
		return new FInt(fi1._value * i, false);
	}
	//Other way around
	public static FInt operator * (int i, FInt fi1) { return fi1 * i; }

	public static FInt operator * (FInt fi1, long lng)
	{
		return new FInt(fi1._value * lng, false);
	}
	//Other way around
	public static FInt operator * (long lng, FInt fi1) { return fi1 * lng; }
	#endregion


	#region Division Helpers
	/*
	private static int CountDigits(long lng)
	{
		//9,223,372,036,854,775,807
		//19 possible digits

		if(lng < 0) lng = -lng;

		if (lng < 10)
			return 1;
		else if (lng < 100)
			return 2;
		else if (lng < 1000)
			return 3;
		else if (lng < 10000)
			return 4;
		else if (lng < 100000)
			return 5;
		else if (lng < 1000000)
			return 6;
		else if (lng < 10000000)
			return 7;
		else if (lng < 100000000)
			return 8;
		else if (lng < 1000000000)
			return 9;
		else if (lng < 10000000000)
			return 10;
		else if (lng < 100000000000)
			return 11;
		else if (lng < 1000000000000)
			return 12;
		else if (lng < 10000000000000)
			return 13;
		else if (lng < 100000000000000)
			return 14;
		else if (lng < 1000000000000000)
			return 15;
		else if (lng < 10000000000000000)
			return 16;
		else if (lng < 100000000000000000)
			return 17;
		else if (lng < 1000000000000000000)
			return 18;
		else return 19;
	}

	//Division Helper
	private static long min(long lng1, long lng2)
	{
		if(lng1 < lng2) return lng1;
		else return lng2;
	}

	//Division Helper
	private static int min(int i1, int i2)
	{
		if(i1 < i2) return i1;
		else return i2;
	}

	//Division Helper
	private static int SiginificantDigits(long lngDec)
	{
		if(lngDec < 0) lngDec = -lngDec;

		if(lngDec == 0) return 0;
		else if(lngDec < 10) return 6;
		else if(lngDec < 100) return 5;
		else if(lngDec < 1000) return 4;
		else if(lngDec < 10000) return 3;
		else if(lngDec < 100000) return 2;
		else return 1;
	}
	*/
	#endregion

	#region Division
	public static FInt operator / (FInt fi1, FInt fi2)
	{
		//
		/*
		long fi1Whole = fi1.value / SCALE;
		long fi1Dec = fi1.value % SCALE;
		
		long fi2Whole = fi2.value / SCALE;
		long fi2Dec = fi2.value % SCALE;
		
		int allowedShift = min(19-CountDigits(fi1Whole), 19-CountDigits(fi2Whole));
		int neededShift = SiginificantDigits(fi2Dec);
		
		int shift = min(allowedShift,neededShift);
		*/

		long whole = fi1._value / fi2._value;
		long remainder = _SCALE * (fi1._value - whole * fi2._value);
		remainder /= fi2._value;

		return new FInt(whole * _SCALE + remainder, false);

		//old method
		//return new FInt(fi1.value * SCALE / fi2.value, false); 
	}

	public static FInt operator / (FInt fi1, int i)
	{
		return new FInt(fi1._value / i, false);
	}

	public static FInt operator / (FInt fi1, long lng)
	{
		return new FInt(fi1._value / lng, false);
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
		//if negative value and it only has decimal digits
		if (_value < 0 && _value > -_SCALE)
		{
			return "-" + (_value / _SCALE) + "." + (Abs(this)._value % _SCALE).ToString().PadLeft(_PRICISION, '0');
		}
		else
		{
			return (_value / _SCALE) + "." + (Abs(this)._value % _SCALE).ToString().PadLeft(_PRICISION, '0');
		}
	}

	public static FInt Parse(string str)
	{
		//TODO: MAKE THIS FUNCTION
		throw new NotImplementedException();
	}

	#region Conversions

	//explicit FInt to int
	public static explicit operator int(FInt fi)
	{
		long lng = fi._value / _SCALE;
		if (lng > int.MaxValue || lng < int.MinValue)
		{
			Debug.LogError("FInt overflow on conversion to int");
		}
		return (int)lng;
	}

	//explicit FInt to long conversion operator
	public static explicit operator long(FInt fi)
	{
		return fi._value / _SCALE;
	}

	//explicit FInt to float conversion operator
	public static explicit operator float(FInt fi)
	{
		return fi._value / (float)_SCALE;
	}

	//explicit FInt to double conversion operator
	public static explicit operator double(FInt fi)
	{
		return fi._value / (double)_SCALE;
	}

	//implicit float to FInt
	public static implicit operator FInt(float f)
	{
		return new FInt((long)(f * _SCALE), false);
	}

	//implicit int to FInt
	public static implicit operator FInt(int i)
	{
		return new FInt(i);
	}

	//implicit long to FInt
	public static implicit operator FInt(long lng)
	{
		return new FInt(lng);
	}

	#endregion

	#region Utility Functions

	public static FInt MakeFraction(int numerator, int denominator)
	{
		return numerator.FI() / denominator.FI();
	}

	public static FInt Min(FInt f1, FInt f2)
	{
		if (f1 < f2)
		{
			return f1;
		}
		else
		{
			return f2;
		}
	}

	public static FInt Max(FInt f1, FInt f2)
	{
		if (f1 > f2)
		{
			return f1;
		}
		else
		{
			return f2;
		}
	}

	public static FInt Abs(FInt fi)
	{
		return new FInt(((fi._value < 0) ? -fi._value : fi._value), false);
	}

	public FInt Decimal
	{
		get
		{
			return new FInt(_value % _SCALE, false);
		}
	}

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

	public static FInt Round(FInt fi)
	{
		FInt fiDecimal = fi.Decimal;

		if (fiDecimal >= MakeFraction(1, 2))
		{
			return (fi - fiDecimal) + 1;
		}
		else
		{
			return fi - fiDecimal;
		}
	}

	public static FInt Sqrt(FInt fi)
	{
		if (fi._value == 0)
		{
			return new FInt(0);
		}

		FInt prev = new FInt(1);

		for (int i = 0; i < 20; i++) //could probably go as low as 5 iterations
		{
			prev = prev - ((prev * prev - fi) / (2 * prev));
		}
		//Debug.Log("SQRT " + fi + ": " + prev + " vs " + System.Math.Sqrt(fi.value/(double)SCALE));

		//Round off at the end
		return prev;


		//---NEWTON'S EQUATION---------- //http://en.wikipedia.org/wiki/Newton%27s_method#Square%5Froot%5Fof%5Fa%5Fnumber
		//f(x) = x^2 - NUMBER_TO_Sqrt
		//(derivative) f'(x) = 2x

		//loop through this multiple times to get more accurate results
		//prevX - f(prevX)/f'(prevX)
	}

	public static FInt Sin(FInt fi)
	{
		return new FInt(_sinLookUp((int)fi), false);
	}

	public static FInt Cos(FInt fi)
	{
		return new FInt(_cosLookUp((int)fi), false);
	}

	public static FInt Tan(FInt fi)
	{
		return Sin(fi) / Cos(fi);
	}

	#region Sin/Cos Helper Functions

	/// <summary>
	/// Look up the sine of the angle.
	/// </summary>
	/// <returns>Sine of the angle.</returns>
	/// <param name="deg">The angle in degrees.</param>
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
	/// <returns>Cosine of the angle.</returns>
	/// <param name="deg">The angle in degrees.</param>
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

	//index is the angle in degrees
	//values are multiplied by 100000 to keep 5 digit decimal precision
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

	//index is the angle in degrees
	//values are multiplied by 100000 to keep 5 digit decimal precision
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
