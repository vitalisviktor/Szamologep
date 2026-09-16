using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
                    else if(label == "C")
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
            int pos = 0;
            string elsoFel;
            string masodikFel;
            int elozoOpPos;
            int koviOpPos;
            bool koviOpPosKereses;
            Button btn = (Button)sender;
            string felirat = btn.Content.ToString();
            if (char.IsDigit(felirat[0]))
            {
                if(txbl_kijelzo.Text == "0")
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
                pos = 0;
                elozoOpPos = 0;
                koviOpPos = 0;
                foreach(char c in txbl_kijelzo.Text)
                {
                    if ("*/".Contains(c))
                    {
                        koviOpPosKereses = true;
                        foreach (char d in felirat)
                        {
                            if(koviOpPos > pos && "+-*/".Contains(d))
                            {
                                koviOpPosKereses = false;
                            }
                            if (koviOpPosKereses)
                            {
                            koviOpPos++;
                            }

                        }


                        elsoFel = txbl_kijelzo.Text.Substring(elozoOpPos, pos + 1);
                        masodikFel = txbl_kijelzo.Text.Substring(pos + 1,txbl_kijelzo.Text.Length - pos - koviOpPos);

                        Console.WriteLine(elsoFel);
                        Console.WriteLine(masodikFel);



                    }



                    if ("+-*/".Contains(c))
                    {
                        elozoOpPos = pos;
                    }

                    pos++;
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