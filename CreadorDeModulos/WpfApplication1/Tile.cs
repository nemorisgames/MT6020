using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace CreadorModulos
{
	public class Tile : HeaderedContentControl
	{
		#region Ctor

		static Tile()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(Tile), new FrameworkPropertyMetadata(typeof(Tile)));
		}

		#endregion

		#region Properties

		internal int LayoutOrder
		{
			get;
			set;
		}

		#endregion

		#region Overrides

		protected override void OnMouseDown(System.Windows.Input.MouseButtonEventArgs e)
		{
			base.OnMouseDown(e);

			if (Activated != null)
				Activated(this, new RoutedEventArgs());
		}

		#endregion

		#region Events

		public event RoutedEventHandler Activated;

		#endregion
	}
}
