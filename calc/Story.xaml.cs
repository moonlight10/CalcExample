using MahApps.Metro.Controls;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Animation;

namespace calc
{
    /// <summary>
    /// Логика взаимодействия для Story.xaml
    /// </summary>
    public partial class Story : MetroWindow
    {
        
        
        string line;
        private void FromStory()
        {
            StreamReader sr = new StreamReader(@"E:\calculations.txt");
            int count = System.IO.File.ReadAllLines(@"E:\calculations.txt").Length;
            string[] list = new string[count];
            while (!sr.EndOfStream)
            {
                for (int i = 0; i <= count - 1; i++)
                {
                    line = sr.ReadLine();
                    list[i] = line;
                }
            }
            Top_field.Text = list[0];
            for (int i = 1; i <= count - 1; i++)
                Bot_field.Text += list[i] + "\n";
            sr.Close();
            }

        public Story()
        {
            InitializeComponent();

            var ripple = new DoubleAnimation                        // анимация тени
            {
                To = 0,
                AutoReverse = true,
                Duration = TimeSpan.FromMilliseconds(2500),
                RepeatBehavior = RepeatBehavior.Forever,
                AccelerationRatio = 0.4,
                DecelerationRatio = 0.3
            };

            Storyboard.SetTarget(ripple, b_shadow);
            Storyboard.SetTargetProperty(ripple, new PropertyPath("Effect.Opacity"));

            var storyboard = new Storyboard { Children = new TimelineCollection { ripple } };
            storyboard.Begin();


            string[] strok = File.ReadAllLines(@"E:\calculations.txt");
            if (strok.Length != 0)
            {
                FromStory();
            }
            
            if (Bot_field.Text == "")
            {
                scrolling.Visibility = Visibility.Hidden;
            } else
            {
                scrolling.Visibility = Visibility.Visible;
            }
           
        }

        private void clr_story_Click(object sender, RoutedEventArgs e)
        {
            Top_field.Text = "";
            Bot_field.Text = "";
            File.WriteAllText(@"E:\calculations.txt", String.Empty);
        }
    }
}
