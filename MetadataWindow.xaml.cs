using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using Path = System.IO.Path;
using TreeView = System.Windows.Controls.TreeView;

namespace TrackLayeringNFT
{
    /// <summary>
    /// Interaction logic for Metadata.xaml
    /// </summary>
    public partial class MetadataWindow : Window
    {
        private object dummyNode = null;
        public string SelectedImagePath { get; set; }
        private TreeViewItem activeItem = null;
        private MetadataDescriptor activeDescriptor;
        public MetadataWindow()
        {
            InitializeComponent();

            TreeViewProgress.Visibility = Visibility.Hidden;

            FolderView.SelectedItemChanged += FolderView_SelectedItemChanged;
            ProbabilityTextBox.TextChanged += Probability_TextChanged;
            
            ProbabilityTextBox.IsEnabled = false;
            ExclusionDropDown.IsEnabled = false;
            UniformProbability.IsEnabled = false;
            ExclusionButton.IsEnabled = false;
            ConfirmButton.IsEnabled = false;
        }

        private void Probability_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (activeItem == null)
                return;

            try
            {
                int value = int.Parse(((System.Windows.Controls.TextBox)sender).Text);
                activeDescriptor.probability = value;
            }
            catch (System.Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void FolderView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeViewItem newSelection = (TreeViewItem)e.NewValue;

            if(newSelection != null)
            {
                activeItem = newSelection;

                MetadataGenerator.GetMetadataForName(activeItem.Header.ToString(), out activeDescriptor);

                UpdateDisplayStyle(newSelection);
                UpdateExclusionList();
                UniformProbability.IsChecked = activeDescriptor.isUniformProbability;
            }
        }

        private void UpdateExclusionList()
        {
            MetadataDescriptor desc;
            MetadataGenerator.GetMetadataForGuid(activeDescriptor.parent, out desc);

            List<string> childNames = desc.GetChildNames();
            
            if(childNames.Count > 1)
                childNames.Remove(activeDescriptor.name);

            ExclusionDropDown.ItemsSource = childNames;
            ExclusionDropDown.SelectedIndex = 0;
        }

        private void UpdateDisplayStyle(TreeViewItem newSelection)
        {
            if (!MetadataHelpers.IsImage((string)newSelection.Tag)) SetupFolderDisplay(newSelection);
            else SetupImageDisplay(newSelection);
        }

        private void SetupImageDisplay(TreeViewItem newSelection)
        {
            UpdateImage(newSelection);

            UpdateProbability(newSelection);
            UniformProbability.IsEnabled = false;
        }

        private void SetupFolderDisplay(TreeViewItem newSelection)
        {
            SelectionName.Content = newSelection.Header.ToString();

            UpdateProbability(newSelection);
            UniformProbability.IsEnabled = true;

            MetadataDescriptor desc;
            MetadataGenerator.GetMetadataForName(newSelection.Header.ToString(), out desc);
            ExclusionButton.IsEnabled = desc.isCategory;
        }

        private void UpdateProbability(TreeViewItem newSelection)
        {
            MetadataDescriptor descriptor;
            MetadataGenerator.GetMetadataForGuid(newSelection.Uid, out descriptor);

            ProbabilityTextBox.Text = descriptor.probability.ToString();
        }

        private void UpdateImage(TreeViewItem newSelection)
        {
            string filePath = (string)newSelection.Tag;

            if (MetadataHelpers.IsImage(filePath))
            {
                PreviewImage.Source = new BitmapImage(new Uri(filePath));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                FolderView.Items.Clear();
                MetadataGenerator.m_MetadataDescriptors.Clear();

                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    TreeViewProgress.Visibility = Visibility.Visible;

                    ProbabilityTextBox.IsEnabled = true;
                    ExclusionDropDown.IsEnabled = true;
                    UniformProbability.IsEnabled = true;
                    ConfirmButton.IsEnabled = true;

                    SetupMetadata(fbd);
                }
            }
        }

        private void GenerateJSONFile(MetadataDescriptor desc)
        {
            string json = JsonConvert.SerializeObject(desc);

            string path = Path.GetDirectoryName(MetadataGenerator.m_MetadataDescriptors[0].path);
            string fullPath = Path.Combine(path, "Metadata.json");
            if (File.Exists(fullPath)) File.Delete(fullPath);

            File.WriteAllText(fullPath, json);
        }

        private void GrabUserProbabilityForTree(MetadataDescriptor element)
        {
            ShowMetadataCreator(element);

            foreach(MetadataDescriptor child in element.children)
            {
                ShowMetadataCreator(child);
                GrabUserProbabilityForTree(child);
            }
        }

        private void ShowMetadataCreator(MetadataDescriptor descriptor)
        {
            if(descriptor.isCategory)
            {
                List<string> contents = new List<string>();

                descriptor.children.ForEach((element) => contents.Add(element.name));

                CategoryMetadataCreator categoryCreator = new CategoryMetadataCreator(descriptor.name, descriptor.key, contents.ToArray());
                categoryCreator.ShowDialog();
            }
            else
            {
                List<string> contents = new List<string>();

                descriptor.children.ForEach((element) => contents.Add(element.name));

                ElementMetadataCreator categoryCreator = new ElementMetadataCreator(descriptor.name, descriptor.path, descriptor.key);
                categoryCreator.ShowDialog();
            }
        }

        private void SetupMetadata(FolderBrowserDialog fbd)
        {
            TreeViewProgress.Maximum = 1;
            TreeViewProgress.Value = 0;
            ((MainWindow)System.Windows.Application.Current.MainWindow).UpdateLayout();

            SelectedImagePath = fbd.SelectedPath;

            TreeViewItem item = new TreeViewItem();
            item.Header = fbd.SelectedPath.Substring(fbd.SelectedPath.LastIndexOf("\\") + 1);
            item.Tag = fbd.SelectedPath;
            item.FontWeight = FontWeights.Bold;
            item.Uid = Guid.NewGuid().ToString();
            item.Items.Add(dummyNode);
            item.Expanded += new RoutedEventHandler(folder_Expanded);
            FolderView.Items.Add(item);

            MetadataGenerator.m_MetadataDescriptors.Add(new MetadataDescriptor((string)item.Header, (string)item.Tag, 100, Guid.Parse(item.Uid)));

            item.ExpandSubtree(); //Initial expansion

            TreeViewProgress.Visibility = Visibility.Hidden;
        }

        void folder_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)sender;

            MetadataDescriptor desc;
            MetadataGenerator.GetMetadataForGuid(item.Uid, out desc);

            if (item.Items.Count == 1 && item.Items[0] == dummyNode)
            {
                item.Items.Clear();
                try
                {
                    string[] directories = Directory.GetDirectories(item.Tag.ToString());
                    foreach (string s in directories)
                    {
                        TreeViewProgress.Maximum++;
                        TreeViewProgress.Value++;
    

                        TreeViewItem subitem = new TreeViewItem();
                        subitem.Header = s.Substring(s.LastIndexOf("\\") + 1);
                        subitem.Tag = s;
                        subitem.FontWeight = FontWeights.DemiBold;
                        subitem.Uid = Guid.NewGuid().ToString();
                        subitem.Items.Add(dummyNode);
                        subitem.Expanded += new RoutedEventHandler(folder_Expanded);
                        item.Items.Add(subitem);

                        desc.AddMetadataChildItem(subitem, (int)Math.Round((double)(100 / directories.Length))); //Init probability to # of directories for even distribution
                    }

                    directories = Directory.GetFiles(item.Tag.ToString());
                    foreach (string s in Directory.GetFiles(item.Tag.ToString()))
                    {
                        TreeViewProgress.Maximum++;
                        TreeViewProgress.Value++;
    

                        TreeViewItem subitem = new TreeViewItem();
                        subitem.Header = s.Substring(s.LastIndexOf("\\") + 1);
                        subitem.Tag = s;
                        subitem.FontWeight = FontWeights.Normal;
                        subitem.Uid = Guid.NewGuid().ToString(); 
                        subitem.Items.Add(dummyNode);
                        subitem.Expanded += new RoutedEventHandler(folder_Expanded);
                        item.Items.Add(subitem);

                        desc.AddMetadataChildItem(subitem, (int)Math.Round((double)(100 / directories.Length))); //Init probability to # of directories for even distribution
                    }
                }
                catch (Exception) { } // clearly don't have time to care
            }
        }

        private void ExclusionButton_Click(object sender, RoutedEventArgs e)
        {
            MetadataDescriptor excludedDesc;

            MetadataGenerator.GetMetadataForName(ExclusionDropDown.SelectedItem.ToString(), out excludedDesc);

            if(!activeDescriptor.exclusions.Contains(excludedDesc.key))
                activeDescriptor.exclusions.Add(excludedDesc.key);
        }

        private void UniformProbability_Checked(object sender, RoutedEventArgs e)
        {
            if(activeDescriptor != null && activeDescriptor.isFolder && UniformProbability.IsChecked == true)
            {
                activeDescriptor.isUniformProbability = true;
                foreach(MetadataDescriptor desc in activeDescriptor.children)
                {
                    decimal probability = Math.Floor((decimal)(100 / activeDescriptor.children.Count));
                    desc.probability = decimal.ToInt32(probability);
                }
            }
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            GenerateJSONFile(MetadataGenerator.m_MetadataDescriptors[0]);

            Close();
        }
    }
}