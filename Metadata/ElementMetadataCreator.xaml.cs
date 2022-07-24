using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TrackLayeringNFT
{
    /// <summary>
    /// Interaction logic for ElementMetadataCreator.xaml
    /// </summary>
    public partial class ElementMetadataCreator : Window
    {
        string m_CategoryName = null;
        int m_Probability = 50;
        Guid m_Guid;
        public ElementMetadataCreator(string categoryName, string filePath, Guid guid)
        {
            InitializeComponent();

            m_CategoryName = categoryName;
            CategoryName.Content = m_CategoryName;
            ProbabilityTextBox.PreviewTextInput += IsTextAllowed;

            PreviewImage.Source = new BitmapImage(new Uri(filePath));

            m_Guid = guid;
        }

        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
        private void IsTextAllowed(object sender, TextCompositionEventArgs e)
        {
            TextBox textInput = (TextBox)sender;
            if (IsTextAllowed(textInput.Text))
            {
                int number = int.Parse(textInput.Text);

                if (NumberIsValidPercent(number))
                {
                    m_Probability = number;
                    textInput.Text = number.ToString(); // Make sure we didn't get a decimal number.
                    e.Handled = true;
                }

            }

            e.Handled = false;
        }
        private bool NumberIsValidPercent(int num)
        {
            return num >= 0 && num <= 100;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //MetadataGenerator.m_MetadataDescriptors.Add(new MetadataDescriptor(m_CategoryName, m_Probability));
            MetadataDescriptor desc;
            MetadataGenerator.GetMetadataForGuid(m_Guid, out desc);
            desc.probability = m_Probability;
            Close();
        }
    }
}
