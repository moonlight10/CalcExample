using System;
using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls;
using System.Diagnostics;
using System.Windows.Media.Animation;
using System.IO;

namespace calc
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {

        double numb1 = 0; 
	    double numb2 = 0;
        string operation = "";
        public string story_str = "";                                // запись в файл


        private void ToStory(string a)                              
        {
            string filecontent = String.Empty;
            filecontent = File.ReadAllText(@"E:\calculations.txt");
            File.WriteAllText(@"E:\calculations.txt", a + filecontent);
        }

        private void _in(string a)                                  // метод для кнопок 
        {
            if (field.Text.Length <= 27)
            {
                if (field.Text == "Деление на ноль невозможно")
                    field.Text = "0";

                if (!(field.Text[0] == '0') && ((field.Text.Substring(field.Text.IndexOf(",")+ 1) == "") || (field.Text.Substring(field.Text.IndexOf(",") + 1) == " ")))
                    field.Text = field.Text.Substring(0, field.Text.Length - 1);

                if (field.Text == "0")
                    if (a == ",")
                        field.Text += a;
                    else
                        field.Text = a;
                else
                {
                    if (field.Text.Contains(","))
                    {
                        field.Text += a;
                    } else
                    {
                        field.Text += a;
                        if ((field.Text.Length + 1) % 4 == 0)
                            field.Text += " ";
                    }
                }    
            }
            else field.Text.TrimEnd();
        }

        public MainWindow()
        {
            int prC = 0;                                            // предотвращаем запуск нескольких копий приложения
            foreach (Process pr in Process.GetProcesses())
                if (pr.ProcessName == "calc") prC++;
            if (prC > 1) {
                Process.GetCurrentProcess().Kill();
            }
            
            InitializeComponent();

            if (!File.Exists(@"E:\calculations.txt"))
                File.Create(@"E:\calculations.txt");


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


            DoubleAnimation WinAnimation = new DoubleAnimation();   // анимация появления окна

            this.Opacity = 0;
            WinAnimation.From = this.Opacity;
            WinAnimation.To = 1;
            WinAnimation.Duration = TimeSpan.FromMilliseconds(1100);
            this.BeginAnimation(Window.OpacityProperty, WinAnimation);
        }


#region numb_s

        private void zero_Click(object sender, RoutedEventArgs e)
        {
            _in("0");
        }

        private void one_Click(object sender, RoutedEventArgs e)
        {
            _in("1");
        }

        private void two_Click(object sender, RoutedEventArgs e)
        {
            _in("2");
        }

        private void three_Click(object sender, RoutedEventArgs e)
        {
            _in("3");
        }

        private void four_Click(object sender, RoutedEventArgs e)
        {
            _in("4");
        }

        private void five_Click(object sender, RoutedEventArgs e)
        {
            _in("5");
        }

        private void six_Click(object sender, RoutedEventArgs e)
        {
            _in("6");
        }

        private void seven_Click(object sender, RoutedEventArgs e)
        {
            _in("7");
        }

        private void eight_Click(object sender, RoutedEventArgs e)
        {
            _in("8");
        }

        private void nine_Click(object sender, RoutedEventArgs e)
        {
            _in("9");
        }
        #endregion

#region basic_ar_operations

        private void div_Click(object sender, RoutedEventArgs e)
        {
            if (operation.Contains("÷"))
                operation.TrimEnd();
            else
            {
                numb1 = double.Parse(field.Text = field.Text.Replace(" ", ""));         //удаление пробелов 
                operation = "÷";
                field.Text = "0";
            }
        }

        private void mul_Click(object sender, RoutedEventArgs e)
        {
            if (operation.Contains("*"))
                operation.TrimEnd();
            else
            {
                numb1 = double.Parse(field.Text = field.Text.Replace(" ", ""));
                operation = "*";
                field.Text = "0";
            }
        }

        private void minus_Click(object sender, RoutedEventArgs e)
        {
            if (operation.Contains("-"))
                operation.TrimEnd();
            else
            {
                numb1 = double.Parse(field.Text = field.Text.Replace(" ", ""));
                operation = "-";
                field.Text = "0";
            }
        }

        private void plus_Click(object sender, RoutedEventArgs e)
        {
            if (operation.Contains("+"))
                operation.TrimEnd();
            else
            {
                numb1 = double.Parse(field.Text = field.Text.Replace(" ", ""));
                operation = "+";
                field.Text = "0";
            }
        }

        private void res_Click(object sender, RoutedEventArgs e)
        {
            if ((field.Text.Substring(field.Text.IndexOf(",") + 1) == "") || (field.Text.Substring(field.Text.IndexOf(",") + 1) == " "))  //удаление пробелов после запятой
                field.Text = field.Text.Substring(0, field.Text.Length - 1);
            numb2 = double.Parse(field.Text = field.Text.Replace(" ", ""));
            string res = "";
            switch (operation)
             {
                 case "+":
                     res = (numb1 + numb2).ToString();
                     field.Text = res;
                     break;
                 case "-":
                     res = (numb1 - numb2).ToString();
                     field.Text = res;
                     break;
                 case "÷":
                     if (numb2 == 0)
                     {
                        res = "Деление на ноль невозможно";
                        field.Text = res;
                     } else
                     {
                        res = (numb1 / numb2).ToString();
                        field.Text = res;
                     } 
                     break;
                 case "*":
                     res = (numb1 * numb2).ToString();
                     field.Text = res;
                     break;
             }
            DateTime now = DateTime.Now;
            story_str = now.ToString("MM/dd/yyyy, H:mm\t") + numb1.ToString() + " " + operation + " " + numb2.ToString() + " = " + res + Environment.NewLine;
            ToStory(story_str);
            numb1 = 0; numb2 = 0; operation = ""; res = ""; story_str = "";
        }

        private void root_Click(object sender, RoutedEventArgs e)
        {
            DateTime now = DateTime.Now;
            if (operation == "")
            {
                story_str = now.ToString("MM/dd/yyyy, H:mm\t") + "√ " + field.Text + " = " + (Math.Sqrt(double.Parse(field.Text))).ToString() + Environment.NewLine; ;
                ToStory(story_str);
                field.Text = (Math.Sqrt(double.Parse(field.Text))).ToString();
                
                numb1 = 0; numb2 = 0; operation = ""; story_str = "";
            }
                
            else if (numb1 != 0 && operation != "" && numb2 == 0)
            {
                field.Text = (Math.Sqrt(double.Parse(numb1.ToString()))).ToString();
                story_str = now.ToString("MM/dd/yyyy, H:mm\t") + "√ " + double.Parse(numb1.ToString()) + " = " + field.Text + Environment.NewLine; ;
                ToStory(story_str);
                numb1 = 0; numb2 = 0; operation = ""; story_str = "";
            }
                
            else
            {
                numb2 = double.Parse(field.Text);
                switch (operation)
                {
                    case "+":
                        field.Text = (numb1 + numb2).ToString();
                        break;
                    case "-":
                        field.Text = (numb1 - numb2).ToString();
                        break;
                    case "*":
                        field.Text = (numb1 * numb2).ToString();
                        break;
                    case "÷":
                        field.Text = (numb1 / numb2).ToString();
                        break;
                }
                field.Text = (Math.Sqrt(double.Parse(field.Text))).ToString();
                story_str = now.ToString("MM/dd/yyyy, H:mm\t") + "√ " + double.Parse(numb1.ToString()) + " = " + field.Text + Environment.NewLine; ;
                ToStory(story_str);
                numb1 = 0; numb2 = 0; operation = ""; story_str = "";
            }
        }

        #endregion

#region other_btns
        private void story_btn_Click(object sender, RoutedEventArgs e)
        {
            Story win = new Story();
            win.ShowDialog();
        }

        private void dot_Click(object sender, RoutedEventArgs e)
        {
            if (field.Text != "")
            {
                if (!field.Text.Contains(","))
                    _in(",");
            }
        }

        private void del_Click(object sender, RoutedEventArgs e)
        {
            if (field.Text != "0")
            {
                if (field.Text.Length == 1)
                {
                    field.Text = "0";
                }
                else if (field.Text.Length > 0)
                {
                    field.Text = field.Text.Substring(0, field.Text.Length - 1);
                }
            }
        }



        private void MetroWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.D0:
                case Key.NumPad0: _in("0"); break;

                case Key.D1:
                case Key.NumPad1: _in("1"); break;

                case Key.D2:
                case Key.NumPad2: _in("2"); break;

                case Key.D3:
                case Key.NumPad3: _in("3"); break;

                case Key.D4:
                case Key.NumPad4: _in("4"); break;

                case Key.D5:
                case Key.NumPad5: _in("5"); break;

                case Key.D6:
                case Key.NumPad6: _in("6"); break;

                case Key.D7:
                case Key.NumPad7: _in("7"); break;

                case Key.D8:
                case Key.NumPad8: _in("8"); break;

                case Key.D9:
                case Key.NumPad9: _in("9"); break;

                case Key.Space:
                    this.clr_Click(this, null);
                    break;

                case Key.Back:
                    this.del_Click(this, null);
                    break;

                case Key.OemPlus:
                case Key.Add:
                    this.plus_Click(this, null);
                    break;

                case Key.Divide:
                    this.div_Click(this, null);
                    break;

                case Key.Multiply:
                    this.mul_Click(this, null);
                    break;

                case Key.OemMinus:
                case Key.Subtract:
                    this.minus_Click(this, null);
                    break;

                case Key.Decimal:
                    this.dot_Click(this, null);
                    break;

                case Key.Enter:
                    this.res_Click(this, null);
                    break;

                case Key.OemComma:
                    this.dot_Click(this, null);
                    break;

            }
        }

        private void clr_Click(object sender, RoutedEventArgs e)
        {
            numb1 = 0;
            numb2 = 0;
            operation = "";
            field.Text = "0";
        }


        private void git_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/moonlight10/CalcExample");
        }

        #endregion
    }
}
