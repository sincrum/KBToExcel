using System;
using System.Collections.Generic;
using System.Text;

namespace Sincrum.Packages.KBToExcel
{
	public interface ITextEditor
	{
		string Content
		{
			get;
			set;
		}

		bool IsDirty
		{
			get;
			set;
		}
	}
}
