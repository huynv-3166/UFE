using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TOHDragonFight3D
{
	[Flags]
	public enum NetworkButtonPress
	{
		None = 0,
		Forward = 1 << 0,
		Back = 1 << 1,
		Up = 1 << 2,
		Down = 1 << 3,
		Button1 = 1 << 4,
		//Button2 = 1 << 5,
		//Button3 = 1 << 6,
		//Button4 = 1 << 7,

		// 16bits network packages required
		//Button5 = 1 << 8,
		LeftSwipeL = 1 << 8,
		//Button6 = 1 << 9,
		LeftSwipeR = 1 << 9,
		//Button7 = 1 << 10,
		RightSwipeL = 1 << 10,
		//Button8 = 1 << 11,
		RightSwipeR = 1 << 11,
		//Button9 = 1 << 12,
		RightTap = 1 << 12,
		//Button10 = 1 << 13,
		LeftPress = 1 << 13,
		//Button11 = 1 << 14,
		RightLongPress = 1 << 14,
		//Button12 = 1 << 15,
		LeftTap = 1 << 15,

		// 32bits network packages required
		Start = 1 << 16,
		Skill1 = 1 << 17,
		Skill2 = 1 << 18,
		Skill3 = 1 << 19,
		Skill4 = 1 << 20,
	}

	public static class NetworkButtonPressExtensions
	{
		public static NetworkButtonPress ToNetworkButtonPress(this ButtonPress button)
		{
			switch (button)
			{
				//case ButtonPress.Up: return NetworkButtonPress.Up;
				case ButtonPress.Down: return NetworkButtonPress.Down;
				case ButtonPress.Back: return NetworkButtonPress.Back;
				case ButtonPress.Forward: return NetworkButtonPress.Forward;
				case ButtonPress.Button1: return NetworkButtonPress.Button1;
				case ButtonPress.Skill1: return NetworkButtonPress.Skill1;
				case ButtonPress.Skill2: return NetworkButtonPress.Skill2;
				//case ButtonPress.Button2: return NetworkButtonPress.Button2;
				//case ButtonPress.Button3: return NetworkButtonPress.Button3;
				case ButtonPress.Skill3: return NetworkButtonPress.Skill3;
				//case ButtonPress.Button4: return NetworkButtonPress.Button4;
				case ButtonPress.Skill4: return NetworkButtonPress.Skill4;
				//case ButtonPress.Button5: return NetworkButtonPress.Button5;
				//case ButtonPress.Button6: return NetworkButtonPress.Button6;
				//case ButtonPress.Button7: return NetworkButtonPress.Button7;
				//case ButtonPress.Button8: return NetworkButtonPress.Button8;
				//case ButtonPress.Button9: return NetworkButtonPress.Button9;
				//case ButtonPress.Button10: return NetworkButtonPress.Button10;
				//case ButtonPress.Button11: return NetworkButtonPress.Button11;
				//case ButtonPress.Button12: return NetworkButtonPress.Button12;
				case ButtonPress.Start: return NetworkButtonPress.Start;
				case ButtonPress.LeftSwipeL: return NetworkButtonPress.LeftSwipeL;
				case ButtonPress.LeftSwipeR: return NetworkButtonPress.LeftSwipeR;
				case ButtonPress.RightSwipeL: return NetworkButtonPress.RightSwipeL;
				case ButtonPress.RightSwipeR: return NetworkButtonPress.RightSwipeR;
				case ButtonPress.RightTap: return NetworkButtonPress.RightTap;
				case ButtonPress.LeftPress: return NetworkButtonPress.LeftPress;
				case ButtonPress.RightLongPress: return NetworkButtonPress.RightLongPress;
				case ButtonPress.LeftTap: return NetworkButtonPress.LeftTap;
				default: return NetworkButtonPress.None;
			}
		}

		public static NetworkButtonPress ToNetworkButtonPress(this IEnumerable<ButtonPress> buttons)
		{
			NetworkButtonPress n = NetworkButtonPress.None;

			if (buttons != null)
			{
				foreach (ButtonPress button in buttons)
				{
					n |= button.ToNetworkButtonPress();
				}
			}

			return n;
		}

		public static ReadOnlyCollection<ButtonPress> ToButtonPresses(this NetworkButtonPress buttonPresses)
		{
			List<ButtonPress> list = new List<ButtonPress>();

			if (buttonPresses != NetworkButtonPress.None)
			{
				//if ((buttonPresses & NetworkButtonPress.Up) != 0) list.Add(ButtonPress.Up);
				if ((buttonPresses & NetworkButtonPress.Down) != 0) list.Add(ButtonPress.Down);
				if ((buttonPresses & NetworkButtonPress.Back) != 0) list.Add(ButtonPress.Back);
				if ((buttonPresses & NetworkButtonPress.Skill1) != 0) list.Add(ButtonPress.Skill1);
				if ((buttonPresses & NetworkButtonPress.Skill2) != 0) list.Add(ButtonPress.Skill2);
				if ((buttonPresses & NetworkButtonPress.Skill3) != 0) list.Add(ButtonPress.Skill3);
				if ((buttonPresses & NetworkButtonPress.Skill4) != 0) list.Add(ButtonPress.Skill4);
				if ((buttonPresses & NetworkButtonPress.Forward) != 0) list.Add(ButtonPress.Forward);
				if ((buttonPresses & NetworkButtonPress.Button1) != 0) list.Add(ButtonPress.Button1);
				//if ((buttonPresses & NetworkButtonPress.Button2) != 0) list.Add(ButtonPress.Button2);
				//if ((buttonPresses & NetworkButtonPress.Button3) != 0) list.Add(ButtonPress.Button3);
				//if ((buttonPresses & NetworkButtonPress.Button4) != 0) list.Add(ButtonPress.Button4);
				//if ((buttonPresses & NetworkButtonPress.Button5) != 0) list.Add(ButtonPress.Button5);
				//if ((buttonPresses & NetworkButtonPress.Button6) != 0) list.Add(ButtonPress.Button6);
				//if ((buttonPresses & NetworkButtonPress.Button7) != 0) list.Add(ButtonPress.Button7);
				//if ((buttonPresses & NetworkButtonPress.Button8) != 0) list.Add(ButtonPress.Button8);
				//if ((buttonPresses & NetworkButtonPress.Button9) != 0) list.Add(ButtonPress.Button9);
				//if ((buttonPresses & NetworkButtonPress.Button10) != 0) list.Add(ButtonPress.Button10);
				//if ((buttonPresses & NetworkButtonPress.Button11) != 0) list.Add(ButtonPress.Button11);
				//if ((buttonPresses & NetworkButtonPress.Button12) != 0) list.Add(ButtonPress.Button12);
				if ((buttonPresses & NetworkButtonPress.Start) != 0) list.Add(ButtonPress.Start);
				if ((buttonPresses & NetworkButtonPress.LeftSwipeL) != 0) list.Add(ButtonPress.LeftSwipeL);
				if ((buttonPresses & NetworkButtonPress.LeftSwipeR) != 0) list.Add(ButtonPress.LeftSwipeR);
				if ((buttonPresses & NetworkButtonPress.RightSwipeL) != 0) list.Add(ButtonPress.RightSwipeL);
				if ((buttonPresses & NetworkButtonPress.RightSwipeR) != 0) list.Add(ButtonPress.RightSwipeR);
				if ((buttonPresses & NetworkButtonPress.RightTap) != 0) list.Add(ButtonPress.RightTap);
				if ((buttonPresses & NetworkButtonPress.LeftPress) != 0) list.Add(ButtonPress.LeftPress);
				if ((buttonPresses & NetworkButtonPress.RightLongPress) != 0) list.Add(ButtonPress.RightLongPress);
				if ((buttonPresses & NetworkButtonPress.LeftTap) != 0) list.Add(ButtonPress.LeftTap);
			}

			return new ReadOnlyCollection<ButtonPress>(list);
		}
	}
}