﻿using System;

namespace OTA.Misc
{
	public class ExitException : Exception
	{
		public ExitException() { }

		public ExitException(string Info) : base(Info) { }
	}
}