using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DoB.Xaml
{
	public class ColorConstant : Constant<Color>
	{
		//TODO: Can we use a TypeConverter instead of having a separate property? Simply setting Value in XAML worked in the XNA version :)
		public string StringValue
		{
			get => $"{Value.R},{Value.G},{Value.B},{Value.A}";
			set
			{
				var cc = value.Split(',').Select(i => byte.Parse(i)).ToArray();
				Value = new Color(cc[0], cc[1], cc[2], cc[3]);
			}
		}
	}
}
