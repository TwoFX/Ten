using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Reflection;
using System.Threading;

namespace Ten
{
	/// <summary>
	/// Interaction logic for Selector.xaml
	/// </summary>
	public partial class Selector : Window
	{
		private IEnumerable<SelectorType> relevantTypes<T>()
		{
			Type inter = typeof(T);
			return AppDomain
				.CurrentDomain
				.GetAssemblies()
				.SelectMany(s => s.GetTypes())
				.Where(type => type.IsClass && type.GetInterfaces().Contains(inter) && type.GetCustomAttribute<HiddenFromSelectorAttribute>() == null)
				.Select(x => new SelectorType(x));
		}

		private List<SelectorType> sources;
		private List<SelectorType> sinks;

		public Selector()
		{
			InitializeComponent();

			sink.SelectionMode = SelectionMode.Multiple;

			sources = relevantTypes<IMoveProvider>().ToList();
			sinks = relevantTypes<IGameStateObserver>().ToList();

			source.ItemsSource = sources;
			sink.ItemsSource = sinks;

			source.SelectedIndex = 0;
		}

		private object construct(Type type, GameParameters pars)
		{
			var withPars = type.GetConstructor(new[] { typeof(GameParameters) });
			if (withPars != null)
				return withPars.Invoke(new object[] { pars });
			return type.GetConstructor(new Type[] { }).Invoke(new object[] { });
		}

		private void button_Click(object sender, RoutedEventArgs e)
		{
			var selectedSource = (SelectorType)source.SelectedItem;
			var selectedSinks = sink.SelectedItems.OfType<SelectorType>();

			GameParameters pars = new GameParameters(sizeX.Value.Value, sizeY.Value.Value, num.Value.Value);

			List<IGameStateObserver> finalSinks = selectedSinks.Select(x => construct(x.Type, pars)).Cast<IGameStateObserver>().ToList();
			IMoveProvider potentialSource = null;

			if (selectedSource.Type.GetCustomAttribute<SingleInstanceAttribute>() != null)
				potentialSource = (IMoveProvider)finalSinks.Where(x => x.GetType().Equals(selectedSource.Type)).FirstOrDefault();

			IMoveProvider finalSource = potentialSource ?? (IMoveProvider)construct(selectedSource.Type, pars);

			Game theGame = new Game(pars, finalSource);
			foreach (var s in finalSinks)
			{
				theGame.AddObserver(s);
			}

			Thread gameThread = new Thread(RunGame);
			gameThread.Start(theGame);

			Close();
		}

		static void RunGame(object game)
		{
			((Game)game).Run(100);
		}
	}
}
