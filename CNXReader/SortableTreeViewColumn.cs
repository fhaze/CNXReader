using System;
using Gtk;

namespace CNXReader
{
	public class SortableTreeViewColumn : TreeViewColumn
	{
		public SortableTreeViewColumn(int id) : base ()
		{
			SortColumnId = id;
			Clickable = true;
		}
	}
}