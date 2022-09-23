﻿using UnityEngine;
using System;

namespace TOHDragonFight3D
{
	public class StoryModeScreen : UFEScreen
	{
		#region public instance properties
		public Action nextScreenAction { get; set; }
		#endregion


		#region public instance methods
		public virtual void GoToNextScreen()
		{
			if (this.nextScreenAction != null)
			{
				this.nextScreenAction();
			}
		}
		#endregion
	}
}