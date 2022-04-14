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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AnimalGame
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<TextBlock> selectedItems = new List<TextBlock>();
        int Matches = 0;
        DispatcherTimer dt = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            
            SetUpGame(dt);
            dt.Interval = TimeSpan.FromSeconds(.1);
            dt.Tick += dtTicker;
        }
        private void SetUpGame(DispatcherTimer dt)
        {
            List<string> ListOfEmoji = new List<string>()
            {
                "🐶", "🐶",
                "🐱", "🐱",
                "🦁", "🦁",
                "🐯", "🐯",
                "🦝", "🦝",
                "🦊", "🦊",
                "🐴", "🐴",
                "🐷", "🐷",
            };

            Random random = new Random();
            foreach (TextBlock textblock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textblock.Name == "Timer")
                {
                    continue;
                }
                int index = random.Next(ListOfEmoji.Count);
                string emoji = ListOfEmoji[index];
                textblock.Text = emoji;
                ListOfEmoji.RemoveAt(index);
                textblock.Visibility = Visibility.Visible;
            }
            
            
        }
        private void ResetGame(DispatcherTimer dt)
        {
            if (Matches == 8)
            {
                dt.Stop();
                Matches = 0;
                incr = 0;
                SetUpGame(dt);
            }
        }
        private int incr = 0;
        private void dtTicker(object sender, EventArgs e)
        {
            incr++;

            Timer.Text = $"{(float)incr / 10}";
        }
        private void MouseHandler(object sender, RoutedEventArgs e)
        {
            TextBlock textBlock = (TextBlock)e.Source;
            dt.Start();
            if (textBlock.Name != "Timer")
            {
                selectedItems.Add(textBlock);
                textBlock.Visibility = Visibility.Collapsed;
                if (selectedItems.Count == 2)
                {
                    DeleteHandler(selectedItems);
                }
            }
        }
        private void DeleteHandler(in List<TextBlock> selectedItems)
        {
            if (selectedItems[0].Text == selectedItems[1].Text)
            {
                selectedItems[0].Text = "";
                selectedItems[1].Text = "";
            }
            else
            {
                selectedItems[0].Visibility = Visibility.Visible;
                selectedItems[1].Visibility = Visibility.Visible;
            }
            selectedItems.Clear();
            Matches++;
            ResetGame(dt);
        }
    }
}
