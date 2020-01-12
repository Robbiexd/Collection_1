using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.Win32;

namespace Collection_1
{
    using Model;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MyCollection collection;

        public MainWindow()
        {
            InitializeComponent();

            rdBtn_list.IsChecked = true;
            collection = new MyCollection();
            collection.ActiveCollection = 0;
        }

        //Radio-Buttons checked handling
        private void rdBtn_list_Checked(object sender, RoutedEventArgs e)
        {
            if (!(collection is null))
            {
                collection.ActiveCollection = 0;
                listBox_students.ItemsSource = collection.ToList();
            }
        }

        private void rdBtn_array_Checked(object sender, RoutedEventArgs e)
        {
            if (!(collection is null))
            {
                collection.ActiveCollection = 1;
                listBox_students.ItemsSource = collection.ToList();
            }
        }

        private void rdBtn_dictionary_Checked(object sender, RoutedEventArgs e)
        {
            if (!(collection is null))
            {
                collection.ActiveCollection = 2;
                listBox_students.ItemsSource = collection.ToList();
            }
        }

        private void rdBtn_queue_Checked(object sender, RoutedEventArgs e)
        {
            if (!(collection is null))
            {
                collection.ActiveCollection = 3;
                listBox_students.ItemsSource = collection.ToList();
            }
        }

        private void rdBtn_stack_Checked(object sender, RoutedEventArgs e)
        {
            if (!(collection is null))
            {
                collection.ActiveCollection = 4;
                listBox_students.ItemsSource = collection.ToList();
            }
        }


        //Buttons clicked handling
        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            if (prompt_name.Text != "" || prompt_schoolClass.Text != "" || prompt_day.Text != "" || prompt_month.Text != "" || prompt_year.Text != "" || comboBox_gender.SelectionBoxItem.ToString() != "")
            {
                int day, month, year;
                if (!(int.TryParse(prompt_day.Text, out day)))
                {
                    MessageBox.Show("Zadejte číslo!");
                    return;
                }
                if (!(int.TryParse(prompt_month.Text, out month)))
                {
                    MessageBox.Show("Zadejte číslo!");
                    return;
                }
                if (!(int.TryParse(prompt_year.Text, out year)))
                {
                    MessageBox.Show("Zadejte číslo!");
                    return;
                }
                collection.Add(prompt_name.Text, prompt_schoolClass.Text, day, month, year, comboBox_gender.SelectionBoxItem.ToString());
                listBox_students.ItemsSource = collection.ToList();
            }
        }

        private void btn_del_Click(object sender, RoutedEventArgs e)
        {
            if (prompt_name.Text != "" || prompt_schoolClass.Text != "" || prompt_day.Text != "" || prompt_month.Text != "" || prompt_year.Text != "" || comboBox_gender.SelectionBoxItem.ToString() != "")
            {
                int day, month, year;
                if (!(int.TryParse(prompt_day.Text, out day)))
                {
                    MessageBox.Show("Zadejte číslo!");
                    return;
                }
                if (!(int.TryParse(prompt_month.Text, out month)))
                {
                    MessageBox.Show("Zadejte číslo!");
                    return;
                }
                if (!(int.TryParse(prompt_year.Text, out year)))
                {
                    MessageBox.Show("Zadejte číslo!");
                    return;
                }
                /*prompt_name.Text = */
                collection.Remove(prompt_name.Text, prompt_schoolClass.Text, day, month, year, comboBox_gender.SelectionBoxItem.ToString());/*.ToString();*/
                listBox_students.ItemsSource = collection.ToList();
            }
        }

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            if (prompt_name.Text != "" || prompt_schoolClass.Text != "" || prompt_day.Text != "" || prompt_month.Text != "" || prompt_year.Text != "" || comboBox_gender.SelectionBoxItem.ToString() != "")
            {
                int day, month, year;
                if (!(int.TryParse(prompt_day.Text, out day)))
                {
                    MessageBox.Show("Zadejte číslo!");
                    return;
                }
                if (!(int.TryParse(prompt_month.Text, out month)))
                {
                    MessageBox.Show("Zadejte číslo!");
                    return;
                }
                if (!(int.TryParse(prompt_year.Text, out year)))
                {
                    MessageBox.Show("Zadejte číslo!");
                    return;
                }
                string student = collection.Search(prompt_name.Text, prompt_schoolClass.Text, day, month, year, comboBox_gender.SelectionBoxItem.ToString());
                if (!(student is null)) { listBox_students.ItemsSource = new List<string> { student }; }
                else { listBox_students.ItemsSource = null; }
            }
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == true) { collection.SaveToFile(saveFileDialog.FileName); }
        }

        private void btn_load_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == true) { collection.LoadFromFile(openFileDialog.FileName); }
            listBox_students.ItemsSource = collection.ToList();
        }
    }
}
