using UnityEngine;

public struct fInt
{
	private const int PRICISION = 6;
	private const long SCALE = 1000000;
	long value;

	#region Properties
	public static fInt MaxValue
	{
		get	{ return new fInt(long.MaxValue, false); }
	}

	public static fInt MinValue
	{
		get	{ return new fInt(long.MinValue, false); }
	}
	#endregion

	#region Constructors
	public fInt(int i)
	{
		value = i * SCALE;

		/* //scale should be a bool parameter that is optional "bool scale = true"
		if (scale) value = i * SCALE;
		else value = i;
		*/
	}

	public fInt(int i, bool setFalse) //bool is to get around a bug
	{
		value = i;
	}

	public fInt(long lng)
	{
		if(lng > long.MaxValue / SCALE || lng < long.MinValue / SCALE) 
			Debug.LogError("fInt Overflow on creation with scaling");
		
		value = lng * SCALE;

		/* //scale should be a bool parameter that is optional "bool scale = true"
		if (scale)
		{
			if(lng > long.MaxValue / SCALE || lng < long.MinValue / SCALE) 
				Debug.LogError("fInt Overflow on creation with scaling");

			value = lng * SCALE;
		}
		else value = lng;*/
	}

	public fInt(long lng, bool setFalse) //bool is to get around a bug
	{
		value = lng;
	}

	#endregion
	
	#region operator overload

	public static fInt operator % (fInt fi1, fInt fi2) 
	{
		return new fInt(fi1.value % fi2.value, false);
	}

	#region Relational Operators
	public static bool operator == (fInt fi1, fInt fi2) 
	{
		return fi1.value == fi2.value;
	}

	public static bool operator != (fInt fi1, fInt fi2) 
	{
		return fi1.value != fi2.value;
	}

	public static bool operator >= (fInt fi1, fInt fi2) 
	{
		return fi1.value >= fi2.value;
	}
	
	public static bool operator > (fInt fi1, fInt fi2) 
	{
		return fi1.value > fi2.value;
	}

	public static bool operator <= (fInt fi1, fInt fi2) 
	{
		return fi1.value <= fi2.value;
	}

	public static bool operator < (fInt fi1, fInt fi2) 
	{
		return fi1.value < fi2.value;
	}
	#endregion

	#region Addition
	public static fInt operator + (fInt fi1, fInt fi2) 
	{
		return new fInt(fi1.value + fi2.value, false);
	}

	public static fInt operator + (fInt fi, int i) 
	{
		return new fInt(fi.value + i * SCALE, false);
	}
	//Other way around
	public static fInt operator + (int i, fInt fi) {return fi + i;}

	public static fInt operator + (fInt fi, long lng) 
	{
		return new fInt(fi.value + lng * SCALE, false);
	}
	//Other way around
	public static fInt operator + (long lng, fInt fi) {return fi + lng;}
	#endregion

	//++
	public static fInt operator ++ (fInt fi)
	{
		fi += 1;
		return fi;
	}

	#region Subtraction
	public static fInt operator - (fInt fi1, fInt fi2) 
	{
		return new fInt(fi1.value - fi2.value, false);
	}

	public static fInt operator - (fInt fi, int i) 
	{
		return new fInt(fi.value - i * SCALE, false);
	}
	//Other way around
	public static fInt operator - (int i, fInt fi) {return fi - i;}
	
	public static fInt operator - (fInt fi, long lng) 
	{
		return new fInt(fi.value - lng * SCALE, false);
	}
	//Other way around
	public static fInt operator - (long lng, fInt fi) {return fi - lng;}
	#endregion

	//--
	public static fInt operator -- (fInt fi)
	{
		fi -= 1;
		return fi;
	}

	//Negative
	public static fInt operator -(fInt fi)
	{
		if(fi.value == long.MinValue) Debug.LogError("fInt Overflow on negative operator");
		return new fInt(-fi.value, false);
	}

	#region Multiplication
	public static fInt operator * (fInt fi1, fInt fi2) 
	{
		//foil so we can have higher multiplication without overflow

		long fi1Whole = fi1.value / SCALE;
		long fi1Decimal = fi1.value % SCALE;

		long fi2Whole = fi2.value / SCALE;
		long fi2Decimal = fi2.value % SCALE;

		/*
		fInt m1 = new fInt(
		(fi1Whole * fi2Whole * SCALE)
		+ (fi1Whole * fi2Decimal) 
		+ (fi1Decimal * fi2Whole)
		+ ((fi1Decimal * fi2Decimal) / SCALE)
		, false);

		old method
		fInt m2 = new fInt((fi1.value * fi2.value)/(SCALE), false);

		Debug.Log(m1 + " vs " + m2 + "vs double: " + ((double)fi1.value/SCALE*(double)fi2.value/SCALE));
		*/

		//TODO: CHECK FOR OVERFLOW
		return new fInt(
			(fi1Whole * fi2Whole * SCALE)
			+ (fi1Whole * fi2Decimal) 
			+ (fi1Decimal * fi2Whole)
			+ ((fi1Decimal * fi2Decimal) / SCALE)
			, false);
	}

	public static fInt operator * (fInt fi1, int i) 
	{
		return new fInt(fi1.value * i, false);
	}
	//Other way around
	public static fInt operator * (int i, fInt fi1) { return fi1 * i;}

	public static fInt operator * (fInt fi1, long lng) 
	{
		return new fInt(fi1.value * lng, false);
	}
	//Other way around
	public static fInt operator * (long lng, fInt fi1) { return fi1 * lng;}
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
	public static fInt operator / (fInt fi1, fInt fi2) 
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

		long whole = fi1.value / fi2.value;
		long remainder = SCALE * (fi1.value - whole * fi2.value);
		remainder /= fi2.value;

		return new fInt(whole * SCALE + remainder, false); 

		//old method
		//return new fInt(fi1.value * SCALE / fi2.value, false); 
	}

	public static fInt operator / (fInt fi1, int i) 
	{
		return new fInt(fi1.value / i, false);
	}

	public static fInt operator / (fInt fi1, long lng) 
	{
		return new fInt(fi1.value / lng, false);
	}
	#endregion

	#endregion

	public override string ToString()
	{
		//if negative value and it only has decimal digits
		if(value < 0 && value > -SCALE) return "-" + (value/SCALE).ToString() + "." + (Abs(this).value%SCALE).ToString().PadLeft(PRICISION,'0');
		else return(value/SCALE).ToString() + "." + (Abs(this).value%SCALE).ToString().PadLeft(PRICISION,'0');
	}

	public static fInt Parse(string str)
	{
		//TODO: MAKE THIS FUNCTION
		return new fInt(0);
	}

	#region conversions

	//explicit fInt to int
	public static explicit operator int(fInt fi)
	{
		long lng = fi.value / SCALE;
		if(lng > int.MaxValue || lng < int.MinValue) Debug.LogError("fInt overflow on conversion to int");
		return (int)lng;
	}

	//explicit fInt to long conversion operator
	public static explicit operator long(fInt fi)
	{
		return fi.value / SCALE;
	}

	//explicit fInt to float conversion operator
	public static explicit operator float(fInt fi)
	{
		return fi.value / (float)SCALE;
	}

	//explicit fInt to double conversion operator
	public static explicit operator double(fInt fi)
	{
		return fi.value / (double)SCALE;
	}

	//implicit float to fInt
	public static implicit operator fInt(float f)
	{
		return new fInt((long)(f * SCALE), false);
	}

	//implicit int to fInt
	public static implicit operator fInt(int i)
	{
		return new fInt(i);
	}

	//implicit long to fInt
	public static implicit operator fInt(long lng)
	{
		return new fInt(lng);
	}
	#endregion

	#region utility functions
	public static fInt MakeFraction(int numerator, int denominator)
	{
		return numerator.FI()/denominator.FI();
	}

	public static fInt Min(fInt f1, fInt f2)
	{
		if(f1 < f2) return f1;
		else return f2;
	}

	public static fInt Max(fInt f1, fInt f2)
	{
		if(f1 > f2) return f1;
		else return f2;
	}

	public static fInt Abs(fInt fi)
	{
		return new fInt(((fi.value < 0) ? -fi.value : fi.value), false);
	}

	public fInt Decimal
	{
		get
		{
			return new fInt(this.value % SCALE, false);
		}
	}

	public fInt Sign
	{
		get
		{
			if(this.value >= 0) return 1.FI();
			else return (-1).FI();
		}
	}

	public static fInt Round(fInt fi)
	{
		fInt fiDecimal = fi.Decimal;

		if(fiDecimal >= MakeFraction(1,2)) return (fi - fiDecimal) + 1;
		else return fi - fiDecimal;
	}

	public static fInt Sqrt(fInt fi)
	{
		if(fi.value == 0) return new fInt(0,false);

		fInt prev = new fInt(1);

		for(int i = 0; i < 20; i++) //could probably go as low as 5 iterations
		{
			prev = prev - ((prev * prev - fi)/(2 * prev));
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

	public static fInt Sin(fInt fi)
	{
		return new fInt(SinLookUp((int)fi), false);
	}

	public static fInt Cos(fInt fi)
	{
		return new fInt(CosLookUp((int)fi), false);
	}

	public static fInt Tan(fInt fi)
	{
		return Sin(fi)/Cos(fi);
	}

	#region Sin/Cos Helper Functions
	/// <summary>
	/// Look up the sine of the angle.
	/// </summary>
	/// <returns>Sine of the angle.</returns>
	/// <param name="deg">The angle in degrees.</param>
	private static int SinLookUp(int deg)
	{
		deg %= 360;
		int sin = 0;

		//get the quadrant (0 based)
		int quad = deg / 90;

		//break it down relative to quadrant
		int i = deg % 90;

		if(quad % 2 == 0) //quad 1 or 3
		{
			if(i <= 45) sin = sinTable[i]; //if it's 45 or less, use sine table
			else sin = cosTable[90-i]; //else use cosine table
		}
		else
		{
			if(i <= 45) sin = cosTable[i]; //if it's 45 or less, use cosine table
			else sin = sinTable[90-i]; //else use sine table
		}

		//sin is negative in third and fourth quadrant
		if(deg > 180) sin = -sin;

		return sin;
	}

	/// <summary>
	/// Look up the cosine of the angle.
	/// </summary>
	/// <returns>Cosine of the angle.</returns>
	/// <param name="deg">The angle in degrees.</param>
	private static int CosLookUp(int deg)
	{
		deg %= 360;
		int cos = 0;
		
		//get the quadrant (0 based)
		int quad = deg / 90;
		
		//break it down relative to quadrant
		int i = deg % 90;
		
		if(quad % 2 == 0) //quad 1 or 3
		{
			if(i <= 45) cos = cosTable[i]; //if it's 45 or less, use cosine table
			else cos = sinTable[90-i]; //else use sine table

		}
		else
		{
			if(i <= 45) cos = sinTable[i]; //if it's 45 or less, use sine table
			else cos = cosTable[90-i]; //else use cosine table
		}
		
		//cos is negative in second and third quadrant
		if(deg > 90 && deg <= 270) cos = -cos;
		
		return cos;
	}


	//TODO: ?could just do sin 0-90 and then use that for cos 0-90?

	//index is the angle in degrees
	//values are multiplied by 100000 to keep 5 digit decimal precision
	static int[] sinTable = new int[]
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
	static int[] cosTable = new int[]
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

public static class fIntQoLExtensions
{	
	public static fInt FI(this System.Int32 i)
	{
		return new fInt(i);
	}
}