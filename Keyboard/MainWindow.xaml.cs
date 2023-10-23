using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Threading.Tasks;

namespace Keyboard
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private char[] ruLowerCase = { 'ё', 'й', 'ц', 'у', 'к', 'е', 'н', 'г', 'ш', 'щ', 'з', 'х', 'ъ', 'ф', 'ы', 'в', 'а', 'п', 'р', 'о', 'л', 'д', 'ж', 'э', 'я', 'ч', 'с', 'м', 'и', 'т', 'ь', 'б', 'ю', '.' };
        private char[] enLowerCase = { '`', 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', '[', ']', 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', ';', '\'', 'z', 'x', 'c', 'v', 'b', 'n', 'm', ',', '.', '/' };

        private char[] ruUpperCase = { 'Ё', 'Й', 'Ц', 'У', 'К', 'Е', 'Н', 'Г', 'Ш', 'Щ', 'З', 'Х', 'Ъ', 'Ф', 'Ы', 'В', 'А', 'П', 'Р', 'О', 'Л', 'Д', 'Ж', 'Э', 'Я', 'Ч', 'С', 'М', 'И', 'Т', 'Ь', 'Б', 'Ю', '.' };
        private char[] enUpperCase = { '`', 'Q', 'W', 'E', 'R', 'T', 'Y', 'U', 'I', 'O', 'P', '[', ']', 'A', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L', ';', '\'', 'Z', 'X', 'C', 'V', 'B', 'N', 'M', ',', '.', '/' };

        bool currentLayout; // раскладка 0 - ru, 1 - en
        bool currentLetter; // регистр 0 - l, 1 - U

        public MainWindow()
        {
            InitializeComponent();

            this.currentLayout = false;
            this.currentLetter = false;
        }

        private async void AlphanumericButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                this.enterTextBlock.Text += button.Content;

                await Task.Delay(50);
                button.Background = new SolidColorBrush(Color.FromRgb(59, 59, 59));
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            Key keyPressed = e.Key;

            if (keyPressed == Key.Escape)
            {
                return;
            }

            else if (keyPressed == Key.Back)
            {
                if (!String.IsNullOrEmpty(this.enterTextBlock.Text))
                {
                    this.enterTextBlock.Text = this.enterTextBlock.Text.Remove(this.enterTextBlock.Text.Length - 1, 1);
                }
            }

            foreach (var i in this.firstButtonRow.Children)
            {
                if (i is Button button)
                {
                    //MessageBox.Show($"{button.Name.ToString()} == {keyPressed.ToString()} : {button.Name.ToString() == keyPressed.ToString()}");
                    if (button.Name.ToString() == keyPressed.ToString())
                    {
                        button.Background = Brushes.Blue;
                        button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

                        return;
                    }
                }
            }

            foreach (var i in this.secondButtonRow.Children)
            {
                if (i is Button button)
                {
                    //MessageBox.Show($"{button.Name.ToString()} == {keyPressed.ToString()} : {button.Name.ToString() == keyPressed.ToString()}");
                    if (button.Name.ToString() == keyPressed.ToString())
                    {
                        button.Background = Brushes.Blue;
                        button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

                        return;
                    }
                }
            }

            foreach (var i in this.thirdButtonRow.Children)
            {
                if (i is Button button)
                {
                    //MessageBox.Show($"{button.Name.ToString()} == {keyPressed.ToString()} : {button.Name.ToString() == keyPressed.ToString()}");
                    if (button.Name.ToString() == keyPressed.ToString())
                    {
                        button.Background = Brushes.Blue;
                        button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

                        return;
                    }
                }
            }

            foreach (var i in this.fourthButtonRow.Children)
            {
                if (i is Button button)
                {
                    //MessageBox.Show($"{button.Name.ToString()} == {keyPressed.ToString()} : {button.Name.ToString() == keyPressed.ToString()}");
                    if (button.Name.ToString() == keyPressed.ToString())
                    {
                        button.Background = Brushes.Blue;
                        button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

                        return;
                    }
                }
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            Key keyPressed = e.Key;

            if (e.Key == Key.LeftShift)
            {
                this.ChangeLetter();
            }

            if (keyPressed == Key.Escape)
            {
                return;
            }

            else if (keyPressed == Key.Space)
            {
                this.enterTextBlock.Text += " ";
            }

            else if (keyPressed == Key.CapsLock)
            {
                this.ChangeLetter();
            }

            else if (keyPressed == Key.LeftCtrl)
            {
                this.ChangeLayout();
            }
        }


        public void ChangeLayout()
        {
            char[] keyboard;

            if (this.currentLayout)
            {
                if (this.currentLetter)
                {
                    keyboard = this.ruUpperCase;
                }
                else
                {
                    keyboard = this.ruLowerCase;
                }
            }
            else
            {
                if (this.currentLetter)
                {
                    keyboard = this.enUpperCase;
                }
                else
                {
                    keyboard = this.enLowerCase;
                }
            }

            this.Oem3.Content = keyboard[0];
            
            for (int i = 1; i < 34;)
            {
                foreach (var item in this.secondButtonRow.Children)
                {
                    if (item is Button button)
                    {
                        button.Content = keyboard[i];
                        i++;
                    }
                }

                foreach (var item in this.thirdButtonRow.Children)
                {
                    if (item is Button button)
                    {
                        button.Content = keyboard[i];
                        i++;
                    }
                }

                foreach (var item in this.fourthButtonRow.Children)
                {
                    if (item is Button button)
                    {
                        button.Content = keyboard[i];
                        i++;
                    }
                }
            }

            this.currentLayout = !this.currentLayout;
        }


        public void ChangeLetter()
        {
            char[] keyboard;

            if (this.currentLetter)
            {
                if (this.currentLayout)
                {
                    keyboard = this.enLowerCase;
                }
                else
                {
                    keyboard = this.ruLowerCase;
                }
            }
            else
            {
                if (this.currentLayout)
                {
                    keyboard = this.enUpperCase;
                }
                else
                {
                    keyboard = this.ruUpperCase;

                }
            }

            this.Oem3.Content = keyboard[0];

            for (int i = 1; i < 34;)
            {
                foreach (var item in this.secondButtonRow.Children)
                {
                    if (item is Button button)
                    {
                        button.Content = keyboard[i];
                        i++;
                    }
                }

                foreach (var item in this.thirdButtonRow.Children)
                {
                    if (item is Button button)
                    {
                        button.Content = keyboard[i];
                        i++;
                    }
                }

                foreach (var item in this.fourthButtonRow.Children)
                {
                    if (item is Button button)
                    {
                        button.Content = keyboard[i];
                        i++;
                    }
                }
            }

            this.currentLetter = !this.currentLetter;
        }

    }
}
