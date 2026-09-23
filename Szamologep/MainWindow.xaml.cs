using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Szamologep
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GombokElhelyezese();
        }


        private void GombokElhelyezese()
        {
            for (int i = 0; i < 4; i++)
            {
                btn_grid.RowDefinitions.Add(new RowDefinition());
                btn_grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            string[,] feliratok =
            {
                {"7", "8", "9", "/"},
                {"4", "5", "6", "*"},
                {"1", "2", "3", "-"},
                {"C", "0", "=", "+"}
            };

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    string label = feliratok[i, j];
                    Button btn = new Button
                    {
                        Content = label,
                        FontSize = 20,
                        FontWeight = FontWeights.Bold,
                        Margin = new Thickness(3)


                    };
                    if (char.IsDigit(label[0]))
                    {
                        btn.Background = Brushes.WhiteSmoke;
                    }
                    else if (label == "C")
                    {
                        btn.Background = Brushes.IndianRed;
                        btn.Foreground = Brushes.White;
                    }
                    else
                    {
                        btn.Background = Brushes.DodgerBlue;
                        btn.Foreground = Brushes.White;
                    }

                    btn.Click += Button_Click;
                    Grid.SetRow(btn, i);
                    Grid.SetColumn(btn, j);

                    btn_grid.Children.Add(btn);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            string felirat = btn.Content.ToString();
            if (char.IsDigit(felirat[0]))
            {
                if (txbl_kijelzo.Text == "0")
                {
                    txbl_kijelzo.Text = "";
                }
                txbl_kijelzo.Text += felirat;
            }
            else if (felirat == "C")
            {
                txbl_kijelzo.Text = "0";
            }
            else if (felirat == "=")
            {
                if ("+-*/".Contains(txbl_kijelzo.Text.Last()))
                {

                }
                else
                {


                    double osszeg = 0;


                    if (txbl_kijelzo.Text.Contains('*') || txbl_kijelzo.Text.Contains('/'))
                    {
                        string firstnum = "";
                        string secondnum = "";
                        bool elso = true;
                        char op = '\0';
                        double result = 0;
                        bool done;
                        bool secondnumdone = false;
                        while (txbl_kijelzo.Text.Contains('*') || txbl_kijelzo.Text.Contains('/'))
                        {
                            done = false;
                            foreach (char c in txbl_kijelzo.Text)
                            {
                                if (!done)
                                {

                                    if (char.IsDigit(c) || c == '.')
                                    {
                                        if (elso)
                                        {
                                            firstnum += c;
                                        }
                                        else if (!secondnumdone)
                                        {
                                            secondnum += c;
                                        }
                                    }
                                    else if ("+-".Contains(c))
                                    {
                                        if (!elso)
                                        { 
                                            secondnumdone = true;
                                        }
                                        else
                                        {
                                            firstnum = "";
                                        }
                                        

                                    }
                                    else if ("*/".Contains(c))
                                    {
                                        if (elso)
                                        {
                                            op = c;
                                            elso = false;
                                        }
                                        else
                                        {

                                            switch (op)
                                            {
                                                case '*':
                                                    result = Convert.ToDouble(firstnum) * Convert.ToDouble(secondnum);
                                                    break;
                                                case '/':
                                                    result = Convert.ToDouble(firstnum) / Convert.ToDouble(secondnum);
                                                    break;
                                            }
                                            txbl_kijelzo.Text = txbl_kijelzo.Text.Replace($"{firstnum}{op}{secondnum}", result.ToString());
                                            firstnum = "";
                                            secondnum = "";
                                            secondnumdone = false;
                                            elso = true;
                                            done = true;
                                        }
                                    }
                                }
                            }
                    

                            if (!(firstnum == "" || secondnum == ""))
                            {

                                switch (op)
                                {
                                    case '*':
                                        result = Convert.ToDouble(firstnum) * Convert.ToDouble(secondnum);
                                        break;
                                    case '/':
                                        result = Convert.ToDouble(firstnum) / Convert.ToDouble(secondnum);
                                        break;
                                }

                                txbl_kijelzo.Text = txbl_kijelzo.Text.Replace($"{firstnum}{op}{secondnum}", result.ToString());
                                elso = true;
                                firstnum = "";
                                secondnum = "";
                            }
                        }



                    }


                    int pos = 0;
                    int prevpos = 0;
                    foreach (char c in txbl_kijelzo.Text)
                    {
                        if ("+-".Contains(c) && pos != 0)
                        {
                            string subszoveg = txbl_kijelzo.Text.Substring(prevpos, pos - prevpos);
                            osszeg += Convert.ToDouble(subszoveg);
                            prevpos = pos;
                        }
                        pos++;
                    }
                    osszeg += Convert.ToDouble(txbl_kijelzo.Text.Substring(prevpos));
                    txbl_kijelzo.Text = Math.Round(osszeg, 10).ToString();

                }

            }
            else
            {
                if (char.IsDigit(txbl_kijelzo.Text[txbl_kijelzo.Text.Length - 1]) && txbl_kijelzo.Text != "0")
                {
                    txbl_kijelzo.Text += felirat;
                }
            }
        }
    }
}