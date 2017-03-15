using System;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using Gtk;
using System.Text;

namespace CNXReader
{
	public class CNXWriter
	{
		TreeView treeView;
		string path;

		public CNXWriter(TreeView treeView, string path)
		{
			this.treeView = treeView;
			this.path = path;
		}

		public void Write()
		{
			TreeIter iter;
			if (treeView.Model.GetIterFirst(out iter))
			{
				using (var sw = new StreamWriter(path, false))
				{
					using (var cw = new CsvWriter(sw))
					{
						cw.Configuration.Encoding = Encoding.ASCII;

						var headers = new List<string>();
						var values = new List<string>();

						//Traverse the tree
						do
						{
							values.Clear();
							foreach (TreeViewColumn column in treeView.Columns)
							{
								//Only output visible columns
								if (column.Visible)
								{
									//Loop through CellRenderers to make sure we have a CellRendererText
									string value = null;
									column.CellSetCellData(treeView.Model, iter, false, false);
									foreach (CellRenderer renderer in column.CellRenderers)
									{
										CellRendererText text = renderer as CellRendererText;
										if (text != null)
										{
											//Setting value indicates this column had a CellRendererText and should be included
											if (value == null)
											{
												value = String.Empty;
											}


											//Add the header if the first time through
											if (headers != null)
											{
												headers.Add(column.Title);
											}


											//Append to the value
											if (text.Text != null)
											{
												value += text.Text;
											}
										}
									}
									if (value != null)
									{
										values.Add(value);
									}
								}
							}


							//Output the header
							if (headers != null)
							{
								foreach (var header in headers)
								{
									cw.WriteField(header);
								}
								cw.NextRecord();
								headers = null;
							}


							foreach (var val in values)
							{
								cw.WriteField(val);
							}
							cw.NextRecord();
						} while (treeView.Model.IterNext(ref iter));
					}
					sw.Close();
				}
			}
		}
	}
}
