using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Windows.Controls.Primitives;
using System.Linq;

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

        private void AlphanumericButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                this.enterTextBlock.Text += button.Content;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            Key keyPressed = e.Key;

            switch (keyPressed)
            {
                case Key.Back:
                    if (!String.IsNullOrEmpty(this.enterTextBlock.Text))
                    {
                        this.enterTextBlock.Text = this.enterTextBlock.Text.Remove(this.enterTextBlock.Text.Length - 1, 1);
                    }
                    break;

                case Key.CapsLock: 
                    this.ChangeLetter();
                    break;

                case Key.LeftShift:
                    this.currentLetter = false;
                    this.ChangeLetter();
                    break;

                case Key.Space:
                    this.enterTextBlock.Text += " ";
                    break;
            }

            this.UpdateKeyboard(keyPressed);
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            Key keyPressed = e.Key;

            switch (keyPressed)
            {
                case Key.LeftAlt:
                    if (e.KeyboardDevice.IsKeyUp(Key.LeftShift) && e.KeyboardDevice.IsKeyUp(Key.LeftAlt))
                    {
                        this.ChangeLayout();
                    }
                    break;

                case Key.LeftShift:
                    if (e.KeyboardDevice.IsKeyUp(Key.LeftShift))
                    {
                        this.currentLetter = true;
                        this.ChangeLetter();
                    }
                    break;
            }
        }

        private async void UpdateKeyboard(Key keyPressed)
        {
            List<UIElementCollection> allButtonRows = new List<UIElementCollection>
            {
                this.firstButtonRow.Children,
                this.secondButtonRow.Children,
                this.thirdButtonRow.Children,
                this.fourthButtonRow.Children,
                this.fifthButtonRow.Children
            };

            foreach (UIElementCollection buttonRow in allButtonRows)
            {
                foreach (var i in buttonRow)
                {
                    if (i is UniformGrid uniformGrid)
                    {
                        foreach (var j in uniformGrid.Children)
                        {
                            if (j is Button button)
                            {
                                //MessageBox.Show($"{button.Name.ToString()} == {keyPressed.ToString()} : {button.Name.ToString() == keyPressed.ToString()}");
                                if (button.Name.ToString() == keyPressed.ToString())
                                {
                                    button.Background = Brushes.Blue;
                                    button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

                                    await Task.Delay(50);
                                    button.Background = new SolidColorBrush(Color.FromRgb(59, 59, 59));

                                    return;
                                }
                            }
                        }
                    }
                    else if (i is Button button1)
                    {
                        //MessageBox.Show($"{button.Name.ToString()} == {keyPressed.ToString()} : {button.Name.ToString() == keyPressed.ToString()}");
                        if (button1.Name.ToString() == keyPressed.ToString())
                        {
                            button1.Background = Brushes.Blue;
                            //button1.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                            await Task.Delay(50);
                            button1.Background = new SolidColorBrush(Color.FromRgb(59, 59, 59));

                            return;
                        }
                    }
                }
            }
        }


        public void ChangeLayout()
        {
            // ВЫБОР НЕОБХОДИМОЙ РАСКЛАДКИ
            char[] keyboard;

            if (this.currentLayout)
            {
                keyboard = this.currentLetter ? this.ruUpperCase : this.ruLowerCase;
            }
            else
            {
                keyboard = this.currentLetter ? this.enUpperCase : this.enLowerCase;
            }

            // ЗАПОЛНЕНИЕ КНОПОК
            this.FillButtons(keyboard);

            //
            this.currentLayout = !this.currentLayout;
        }

        public void ChangeLetter()
        {
            // ВЫБОР НЕОБХОДИМОЙ РАСКЛАДКИ
            char[] keyboard;

            if (this.currentLetter)
            {
                keyboard = this.currentLayout ? this.enLowerCase : this.ruLowerCase;
            }
            else
            {
                keyboard = this.currentLayout ? this.enUpperCase : this.ruUpperCase;
            }

            // ЗАПОЛНЕНИЕ КНОПОК
            this.FillButtons(keyboard);

            //
            this.currentLetter = !this.currentLetter;
        }


        private void FillButtons(char[] keyboard)
        {
            // КОЛЛЕКЦИЯ С КНОПКАМИ
            List<UIElementCollection> allAlphaNumericButtonRows = new List<UIElementCollection>
            {
                this.secondAlphaNumericButtonRow.Children,
                this.thirdAlphaNumericButtonRow.Children,
                this.fourthAlphaNumericButtonRow.Children
            };

            // ЗАПОЛНЕНИЕ КНОПОК
            this.Oem3.Content = keyboard[0];

            int i = 1;
            foreach (var button in allAlphaNumericButtonRows.SelectMany(row => row.OfType<Button>())) // ChatGPT про LINQ подсказал, а то забыл уже про него
            {
                button.Content = keyboard[i++];
            }
        }
    }
}
