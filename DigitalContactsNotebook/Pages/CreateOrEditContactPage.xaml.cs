using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Text.RegularExpressions;

using UniversalClassLibrary;
using UniversalControlLibrary;

namespace DigitalContactsNotebook.Pages
{
    /// <summary>
    /// <see cref="Page"/> создания/редактирования контакта
    /// </summary>
    public partial class CreateOrEditContactPage : Page
    {
        private readonly Frame MainWindowFrame;

        /// <summary>
        /// <see cref="bool"/> параметр, показывающий, создаётся или редактируется контакт
        /// </summary>
        private readonly bool IsNewContact;

        private const string PhoneNumberMask = "+7 (XXX) XXX-XX-XX";

        public CreateOrEditContactPage(Frame MainWindowFrame, bool IsNewContact = true)
        {
            InitializeComponent();

            this.MainWindowFrame = MainWindowFrame;
            this.IsNewContact = IsNewContact;

            if (IsNewContact)
            {
                SaveOrEditContactButton.Content = "Сохранить";
            }

            else
            {
                SaveOrEditContactButton.Content = "Редактировать";
            }

            PhoneNumberTextBox.PreviewTextInput += (sender, e) =>
            {
                if (sender is TextBox TextBox)
                {
                    if (!IsValidPhoneNumber(TextBox.Text + e.Text))
                    {
                        e.Handled = true;
                    }
                }
            };

            PhoneNumberTextBox.PreviewKeyDown += (sender, e) =>
            {
                if (e.Key == Key.Space)
                {
                    if (sender is TextBox TextBox)
                    {
                        if (!IsValidPhoneNumber(TextBox.Text + " "))
                        {
                            e.Handled = true;
                        }
                    }
                }
            };

            ContactInfoStackPanel.ProcessAllChildVisualControls(Control =>
            {
                if (Control is PlaceholderTextBox PlaceholderTextBox && Control != PhoneNumberTextBox)
                {
                    PlaceholderTextBox.PreviewTextInput += (sender, e) =>
                    {
                        if (sender is TextBox TextBox)
                        {
                            if (!IsValidFullName(TextBox.Text + e.Text))
                            {
                                e.Handled = true;
                            }
                        }
                    };

                    PlaceholderTextBox.PreviewKeyDown += (sender, e) =>
                    {
                        if (e.Key == Key.Space)
                        {
                            e.Handled = true;
                        }
                    };
                }
            });
        }

        /// <summary>
        /// Событие нажатия на кнопку "Назад"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindowFrame.GoBack();
        }

        /// <summary>
        /// Событие нажатия на кнопку "Редактировать" (или "Сохранить")
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveOrEditContactButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsNewContact)
            {

            }

            else
            {

            }
        }

        /// <summary>
        /// Событие, срабатывающее перед вставкой символа/текста в <see href="PhoneNumberTextBox"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PhoneNumberTextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string Text = string.Empty;

                if (e.DataObject.GetData(typeof(string)) is string String)
                {
                    Text = String;
                }

                if (sender is TextBox TextBox)
                {
                    if (!IsValidPhoneNumber(TextBox.Text + Text))
                    {
                        e.CancelCommand();
                    }
                }
            }

            else
            {
                e.CancelCommand();
            }
        }

        /// <summary>
        /// Событие, срабатывающее перед вставкой символа/текста в <see href="FullNameTextBox"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FullNameTextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string Text = string.Empty;

                if (e.DataObject.GetData(typeof(string)) is string String)
                {
                    Text = String;
                }

                if (sender is TextBox TextBox)
                {
                    if (!IsValidFullName(TextBox.Text + Text))
                    {
                        e.CancelCommand();
                    }
                }
            }

            else
            {
                e.CancelCommand();
            }
        }

        /// <summary>
        /// Метод проверки валидности телефонного номера
        /// </summary>
        /// <param name="InputText"></param>
        /// <returns></returns>
        public static bool IsValidPhoneNumber(string InputText)
        {
            if (InputText.Length > PhoneNumberMask.Length)
            {
                return false;
            }

            for (int i = 0; i < InputText.Length; i++)
            {
                if (PhoneNumberMask[i] == 'X')
                {
                    if (!char.IsDigit(InputText[i]))
                    {
                        return false;
                    }
                }

                else
                {
                    if (InputText[i] != PhoneNumberMask[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Метод проверки валидности ФИО
        /// </summary>
        /// <param name="InputText"></param>
        /// <returns></returns>
        public static bool IsValidFullName(string InputText)
        {
            if (string.IsNullOrEmpty(InputText))
            {
                return false;
            }

            // Проверка первой буквы
            if (!char.IsUpper(InputText[0]) || !IsTextAllowed(InputText[0], CyrillicCharOnlyRegex()))
            {
                return false;
            }

            for (int i = 1; i < InputText.Length; i++)
            {
                if (!char.IsLower(InputText[i]) || !IsTextAllowed(InputText[i], CyrillicCharOnlyRegex()))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Метод, проверяющий соответствие текста заданному <see cref="Regex"/>
        /// </summary>
        /// <param name="Text">Проверяемый текст</param>
        /// <param name="Regex">Проверяющий <see cref="Regex"/></param>
        /// <returns></returns>
        private static bool IsTextAllowed(string Text, Regex Regex)
        {
            return Regex.IsMatch(Text);
        }

        /// <summary>
        /// Метод, проверяющий соответствие символа заданному <see cref="Regex"/>
        /// </summary>
        /// <param name="Char">Проверяемый символ</param>
        /// <param name="Regex">Проверяющий <see cref="Regex"/></param>
        /// <returns></returns>
        private static bool IsTextAllowed(char Char, Regex Regex)
        {
            return Regex.IsMatch(Char.ToString());
        }

        /// <summary>
        /// <see cref="Regex"/>, которому соответствует только формат "+7 (XXX) XXX-XX-XX"
        /// </summary>
        /// <returns></returns>
        [GeneratedRegex(@"^\+7 \(\d{3}\) \d{3}-\d{2}-\d{2}$")]
        private static partial Regex PhoneNumberOnlyRegex();

        /// <summary>
        /// <see cref="Regex"/>, которому соответствует только формат кириллицы с первой заглавной буквой и остальными строчными буквами
        /// </summary>
        /// <returns></returns>
        [GeneratedRegex("^[А-ЯЁ][а-яё]$")]
        private static partial Regex FullNameOnlyRegex();

        /// <summary>
        /// <see cref="Regex"/>, которому соответствует только формат кириллической заглавной или строчной буквы
        /// </summary>
        /// <returns></returns>
        [GeneratedRegex("^[А-ЯЁа-яё]$")]
        private static partial Regex CyrillicCharOnlyRegex();
    }
}