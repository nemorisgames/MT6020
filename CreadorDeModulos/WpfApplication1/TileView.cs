using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Specialized;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Collections;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;

namespace CreadorModulos
{
	public class TileView : ItemsControl , IComparer
	{
		#region Ctor

		static TileView()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(TileView), new FrameworkPropertyMetadata(typeof(TileView)));
		}

		public TileView()
		{
			Loaded += new RoutedEventHandler(OnTileViewLoaded);
		}

		#endregion

		#region Overrides

		protected override Size MeasureOverride(Size constraint)
		{
			var sz = base.MeasureOverride(constraint);

			double availableWidth = constraint.Width - 2 * PADDING;
			double availableHeight = constraint.Height - 3 * PADDING;

			double activeHeight = .7 * availableHeight;
			double inactiveHeight = availableHeight - activeHeight;
			double inactiveWidth = (availableWidth - ((Items.Count - 2) * PADDING)) / (Items.Count - 1);

			foreach (Tile tile in Items)
			{
				tile.Measure(
					new Size(
					tile == ActiveTile ? availableWidth : inactiveWidth,
					tile == ActiveTile ? activeHeight : inactiveHeight
					));
			}

			return sz;
		}

		protected override Size ArrangeOverride(Size arrangeBounds)
		{
			if (ActiveTile == null && Items.Count > 0)
				ActiveTile = Items[0] as Tile;

			double availableWidth = arrangeBounds.Width - 2 * PADDING;
			double availableHeight = arrangeBounds.Height - 3 * PADDING;

			double activeHeight = .7 * availableHeight - PADDING;
			double inactiveHeight = availableHeight - activeHeight;
			double inactiveWidth = (availableWidth - ((Items.Count - 2) * PADDING)) / (Items.Count - 1);

			double x = PADDING;
			double y = 2 * PADDING + activeHeight;

			foreach (Tile tile in OrderedItems)
			{
				var rect = new Rect(
					ActiveTile == tile ? PADDING : x,
					ActiveTile == tile ? PADDING : y,
					ActiveTile == tile ? availableWidth : inactiveWidth,
					ActiveTile == tile ? activeHeight : inactiveHeight
					);

				tile.Arrange(rect);
				
				if(ActiveTile != tile)
					x += inactiveWidth + PADDING;
			}
			
			return arrangeBounds;
		}

		protected override bool IsItemItsOwnContainerOverride(object item)
		{
			return item is Tile;
		}

		protected override DependencyObject GetContainerForItemOverride()
		{
			return new Tile();
		}

		protected override void OnItemsChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			base.OnItemsChanged(e);

			if (e.Action == NotifyCollectionChangedAction.Add ||
				e.Action == NotifyCollectionChangedAction.Remove)
			{

				foreach (object o in e.NewItems)
				{
					var tile = o as Tile;

					if (tile != null)
						tile.Activated += new RoutedEventHandler(OnTileActivated);
				}

				foreach (object o in e.OldItems)
				{
					var tile = o as Tile;

					if (tile != null)
						tile.Activated -= new RoutedEventHandler(OnTileActivated);
				}
			}
		}

		#endregion

		#region Properties

		internal IEnumerable<Tile> OrderedItems
		{
			get
			{
				return Items.OfType<Tile>().OrderBy(t => t.LayoutOrder);
			}
		}

		public Tile ActiveTile
		{
			get { return (Tile)GetValue(ActiveTileProperty); }
			set { SetValue(ActiveTileProperty, value); }
		}

		public static readonly DependencyProperty ActiveTileProperty =
			DependencyProperty.Register("ActiveTile", typeof(Tile), typeof(TileView), new UIPropertyMetadata(null));

		#endregion

		#region Implementations

		private void ActivateTile(Tile tile)
		{
			if (isInTransition) return;

			Canvas.SetZIndex(ActiveTile, 0);
			Canvas.SetZIndex(tile, 100);

			AnimateTransition(tile, ActiveTile);

			InvalidateVisual();

			_newActiveTile = tile;
		}

		private void AnimateTransition(Tile newActive, Tile oldActive)
		{
			double activeX = 0, activeY = 0, activeWidth = 0, activeHeight = 0;
			double inActiveX = 0, inActiveY = 0, inActiveWidth = 0, inActiveHeight = 0;

			if (oldActive != null && newActive != null)
			{
				var pt = newActive.TransformToAncestor(this).Transform(new Point(0, 0));
				
				inActiveX = pt.X; 
				inActiveY = pt.Y; 
				inActiveWidth = newActive.ActualWidth; 
				inActiveHeight = newActive.ActualHeight;

				activeX = PADDING;
				activeY = PADDING;
				activeWidth = oldActive.ActualWidth;
				activeHeight = oldActive.ActualHeight;

				var transformGroup = new TransformGroup();
				transformGroup.Children.Add(new TranslateTransform());
				transformGroup.Children.Add(new ScaleTransform { CenterX = PADDING - inActiveX, CenterY = -inActiveY });

				oldActive.RenderTransform = null;
				newActive.RenderTransform = transformGroup;

				DoubleAnimation translateX = new DoubleAnimation { From = 0, To = PADDING - inActiveX, Duration = TimeSpan.FromSeconds(1), };
				DoubleAnimation translateY = new DoubleAnimation { From = 0, To = PADDING - inActiveY, Duration = TimeSpan.FromSeconds(1) };
				DoubleAnimation scaleX = new DoubleAnimation { To = activeWidth / inActiveWidth, Duration = TimeSpan.FromSeconds(1) };
				DoubleAnimation scaleY = new DoubleAnimation { To = activeHeight / inActiveHeight, Duration = TimeSpan.FromSeconds(1) };

				scaleY.Completed += OnAnimationCompleted;

				Storyboard tileActivator = new Storyboard();
				
				tileActivator.Children.Add(translateX);
				tileActivator.Children.Add(translateY);
				tileActivator.Children.Add(scaleX);
				tileActivator.Children.Add(scaleY);

				Storyboard.SetTarget(translateX, newActive);
				Storyboard.SetTarget(translateY, newActive);
				Storyboard.SetTarget(scaleX, newActive);
				Storyboard.SetTarget(scaleY, newActive);

				Storyboard.SetTargetProperty(translateX, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(TranslateTransform.X)"));
				Storyboard.SetTargetProperty(translateY, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(TranslateTransform.Y)"));
				Storyboard.SetTargetProperty(scaleX, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[1].(ScaleTransform.ScaleX)"));
				Storyboard.SetTargetProperty(scaleY, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[1].(ScaleTransform.ScaleY)"));

				isInTransition = true;

				tileActivator.Completed += new EventHandler(OnActivationAnimation_Completed);

				tileActivator.Begin();
			}
		}

		private void AnimateDeactivation(Tile inActiveTile)
		{
			Storyboard fadeIn = new Storyboard();
			TimeSpan duration = new TimeSpan(0, 0, 1);
			DoubleAnimation animation = new DoubleAnimation
			{
				From = 0,
				To = 1,
				Duration = TimeSpan.FromSeconds(1)
			};

			Storyboard.SetTarget(animation, inActiveTile);
			Storyboard.SetTargetProperty(animation, new PropertyPath(Control.OpacityProperty));
			
			fadeIn.Children.Add(animation);

			fadeIn.Begin(this);
		}

		public int Compare(object x, object y)
		{
			var tile1 = x as Tile;
			var tile2 = y as Tile;

			return tile1.LayoutOrder.CompareTo(tile2.LayoutOrder);
		}

		#endregion

		#region Event Handlers

		void OnTileViewLoaded(object sender, RoutedEventArgs e)
		{
			Loaded += new RoutedEventHandler(OnTileViewLoaded);
			int order = 0;
			foreach (object o in Items)
			{
				var tile = o as Tile;
				if (tile != null)
				{
					tile.Activated += new RoutedEventHandler(OnTileActivated);
					tile.LayoutOrder = order++;
				}
			}
		}

		void OnTileActivated(object sender, RoutedEventArgs e)
		{
			ActivateTile(sender as Tile);
		}

		void OnActivationAnimation_Completed(object sender, EventArgs e)
		{
			isInTransition = false;
			var inActiveTile = ActiveTile;
			inActiveTile.Opacity = 0;
			int order = _newActiveTile.LayoutOrder;
			_newActiveTile.LayoutOrder = ActiveTile.LayoutOrder;
			ActiveTile.LayoutOrder = order;
			ActiveTile = _newActiveTile;
			InvalidateArrange();
			AnimateDeactivation(inActiveTile);
		}

		void OnAnimationCompleted(object o, EventArgs e)
		{
			(Storyboard.GetTarget((o as AnimationClock).Timeline) as UIElement).RenderTransform = null;
		}

		#endregion

		#region Fields

		private const double PADDING = 10;
		private bool isInTransition = false;
		private Tile _newActiveTile = null;
		
		#endregion
	}
}
