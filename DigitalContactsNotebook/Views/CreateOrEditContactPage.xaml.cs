using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Text.RegularExpressions;

using UniversalClassLibrary;
using UniversalControlLibrary;
using DigitalContactsNotebook.Models;
using DigitalContactsNotebook.Data;
using System.Data.Entity;
using System.Windows.Media;

namespace DigitalContactsNotebook.Views
{
    /// <summary>
    /// <see cref="Page"/> создания/редактирования контакта
    /// </summary>
    public partial class CreateOrEditContactPage : Page
    {
        private readonly Frame MainWindowFrame;
        private readonly bool IsNewContact;
        private const string PhoneNumberMask = "+7 (XXX) XXX-XX-XX";

        /// <summary>
        /// Конструктор страницы создания/редактирования контакта
        /// </summary>
        /// <param name="MainWindowFrame"><see cref="Frame"/> окна <see cref="MainWindow"/></param>
        /// <param name="IsNewContact">Параметр, показывающий, создаётся или редактируется контакт</param>
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

            using(ApplicationContext ApplicationContext = new())
            {
                List<string> PhoneTypes = ApplicationContext.PhoneTypes?.Select(PhoneType => PhoneType.PhoneTypeText).ToList() ?? [];
                PhoneTypeComboBox.ItemsSource = PhoneTypes;
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

            PhoneNumberTextBox.GotFocus += (sender, e) => PhoneNumberTextBox.BorderBrush = Brushes.DarkGray;

            ContactInfoStackPanel.ProcessAllChildVisualControls(Control =>
            {
                if (Control is PlaceholderTextBox PlaceholderTextBox && Control != PhoneNumberTextBox)
                {
                    PlaceholderTextBox.GotFocus += (sender, e) => PlaceholderTextBox.BorderBrush = Brushes.DarkGray;

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

                else if (Control is Border Border)
                {
                    if (Border.Child is ComboBox ComboBox)
                    {
                        ComboBox.GotFocus += (sender, e) => Border.BorderThickness = new(0);
                    }
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
            bool AllowOperation = true;

            // Проверка заполненности полей данными //

            ContactInfoStackPanel.ProcessAllChildVisualControls(Control =>
            {
                if (Control is PlaceholderTextBox PlaceholderTextBox && PlaceholderTextBox != PatronymicTextBox)
                {
                    if (string.IsNullOrEmpty(PlaceholderTextBox.Text) || PlaceholderTextBox.Text == PlaceholderTextBox.PlaceholderText && PlaceholderTextBox.Foreground == Brushes.Gray)
                    {
                        PlaceholderTextBox.BorderBrush = Brushes.Red;

                        AllowOperation = false;
                    }
                }

                else if (Control is Border Border)
                {
                    if (Border.Child is ComboBox ComboBox && ComboBox != PhoneTypeComboBox)
                    {
                        if (string.IsNullOrEmpty(ComboBox.Text))
                        {
                            Border.BorderThickness = new(1);
                            Border.BorderBrush = Brushes.Red;

                            AllowOperation = false;
                        }
                    }
                }
            });

            // Валидация полей //

            if (!IsTextAllowed(PhoneNumberTextBox.Text, PhoneNumberOnlyRegex()))
            {
                PhoneNumberTextBox.BorderBrush = Brushes.Red;

                AllowOperation = false;
            }

            ContactInfoStackPanel.ProcessAllChildVisualControls(Control =>
            {
                if (Control is PlaceholderTextBox PlaceholderTextBox && PlaceholderTextBox != PhoneNumberTextBox)
                {
                    if (!IsTextAllowed(PlaceholderTextBox.Text, FullNameOnlyRegex()))
                    {
                        PlaceholderTextBox.BorderBrush = Brushes.Red;

                        AllowOperation = false;
                    }
                }
            });

            if (AllowOperation)
            {
                if (IsNewContact)
                {
                    using ApplicationContext ApplicationContext = new();

                    int ContactsInfoMaxID = ApplicationContext.ContactsInfo.Count();
                    int ContactsMaxID = ApplicationContext.Contacts.Count();

                    string? Patronymic = null;

                    if (!(PatronymicTextBox.Text == PatronymicTextBox.PlaceholderText && PatronymicTextBox.Foreground == Brushes.Gray))
                    {
                        Patronymic = PatronymicTextBox.Text;
                    }

                    ContactInfo ContactInfo = new()
                    {
                        ID = ContactsInfoMaxID + 1,
                        Name = NameTextBox.Text,
                        Surname = SurnameTextBox.Text,
                        Patronymic = Patronymic,
                        Sex = SexComboBox.Text[0]
                    };

                    int? PhoneTypeID = null;

                    if (PhoneTypeComboBox.SelectedIndex != -1)
                    {
                        PhoneTypeID = PhoneTypeComboBox.SelectedIndex;
                    }

                    Contact Contact = new()
                    {
                        ID = ContactsMaxID + 1,
                        PhoneNumber = PhoneNumberTextBox.Text,
                        PhoneTypeID = PhoneTypeID,
                        ContactInfoID = ContactInfo.ID,
                    };

                    ApplicationContext.ContactsInfo.Add(ContactInfo);
                    ApplicationContext.Contacts.Add(Contact);

                    ApplicationContext.SaveChanges();

                    //string sql = "INSERT INTO Contacts (ID, PhoneNumber, PhoneTypeID, ContactInfoID) VALUES (@p0, @p1, @p2, @p3)";
                    //ApplicationContext.Database.ExecuteSqlCommand(sql, 2, "1234567890", 1, 2);
                }

                else
                {
                    using ApplicationContext ApplicationContext = new();

                    List<string> PhoneTypes = ApplicationContext.PhoneTypes.Select(PhoneType => PhoneType.PhoneTypeText).ToList();
                    PhoneTypeComboBox.ItemsSource = PhoneTypes;
                }
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
        [GeneratedRegex("^[А-ЯЁ][а-яё]+$")]
        private static partial Regex FullNameOnlyRegex();

        /// <summary>
        /// <see cref="Regex"/>, которому соответствует только формат кириллической заглавной или строчной буквы
        /// </summary>
        /// <returns></returns>
        [GeneratedRegex("^[А-ЯЁа-яё]$")]
        private static partial Regex CyrillicCharOnlyRegex();
    }
}