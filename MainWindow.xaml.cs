using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.InteropServices;
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

namespace Parser
{
    public partial class MainWindow : Window
    {
        List<string> Links;
        List<string> Words;

        List<string> UserAgents = new List<string>()
        {
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.0.0 Safari/537.36",
            "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.0.0 Safari/537.36",
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:123.0) Gecko/20100101 Firefox/123.0",           
            "Mozilla/5.0 (Macintosh; Intel Mac OS X 14_4) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/17.3.1 Safari/605.1.15",
            "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.0; Trident/4.0)",
            "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0)",
            "Mozilla/4.0 (compatible; MSIE 9.0; Windows NT 6.0; Trident/5.0)",
            "Mozilla/4.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)",
            "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; Trident/6.0)",
            "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; Trident/6.0)",
            "Mozilla/5.0 (Windows NT 6.1; Trident/7.0; rv:11.0) like Gecko",
            "Mozilla/5.0 (Windows NT 6.2; Trident/7.0; rv:11.0) like Gecko",
            "Mozilla/5.0 (Windows NT 6.3; Trident/7.0; rv:11.0) like Gecko",
            "Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko",
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.0.0 Safari/537.36 Edg/122.0.2365.80",
            "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.0.0 Safari/537.36 Edg/122.0.2365.80",
            "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.0.0 Safari/537.36 Vivaldi/6.6.3271.50",
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.0.0 Safari/537.36 Vivaldi/6.6.3271.50",
            "Mozilla/5.0 (Macintosh; Intel Mac OS X 14_4) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.0.0 Safari/537.36 Vivaldi/6.6.3271.50",
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.0.0 Safari/537.36 OPR/108.0.0.0",
            "Mozilla/5.0 (Windows NT 10.0; WOW64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.0.0 Safari/537.36 OPR/108.0.0.0",
            "Mozilla/5.0 (Macintosh; Intel Mac OS X 14_4) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.0.0 Safari/537.36 OPR/108.0.0.0",
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.0.0 YaBrowser/23.9.0.2325 Yowser/2.5 Safari/537.36",
            "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.0.0 YaBrowser/23.9.0.2325 Yowser/2.5 Safari/537.36",
        };

        int counter = 0;
        int error = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(sender is TextBox tb)
            {
                foreach(UIElement element in MainGrid.Children)
                {
                    if(element is TextBlock p)
                    {
                        if(p.Name == tb.Tag.ToString())
                        {
                            if (tb.Text == "")
                            {
                                p.Visibility = Visibility.Visible;
                            }
                            else
                            {
                                p.Visibility = Visibility.Hidden;
                            }
                            return;
                        }
                    }
                }
            }
        }

        private void ButtonDomain_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Текстовый файл | *.txt";

            if(openFileDialog.ShowDialog() == true)
            {
                tb1.Text = openFileDialog.FileName;
            }
        }

        private void ButtonWord_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Текстовый файл | *.txt";

            if (openFileDialog.ShowDialog() == true)
            {
                tb2.Text = openFileDialog.FileName;
            }
        }

        private void ButtonUserAgent_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Текстовый файл | *.txt";

            if (openFileDialog.ShowDialog() == true)
            {
                tb5.Text = openFileDialog.FileName;
            }
        }

        private void ButtonFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Текстовый файл | *.txt";

            if (saveFileDialog.ShowDialog() == true)
            {
                tb3.Text = saveFileDialog.FileName;
            }
        }

        private void ButtonErrorFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Текстовый файл | *.txt";

            if (saveFileDialog.ShowDialog() == true)
            {
                tb4.Text = saveFileDialog.FileName;
            }
        }

        private bool Init()
        {
            try
            {
                using (StreamReader sr = new StreamReader(tb1.Text))
                {
                    string str = sr.ReadToEnd();
                    Links = str.Split(',', '\n').Select(s => s.Trim(' ', ',', '\n', '\r')).Where(s => s != "").ToList();
                }

                using (StreamReader sr = new StreamReader(tb2.Text))
                {
                    string str = sr.ReadToEnd();
                    Words = str.Split(',', '\n').Select(s => s.Trim(' ', ',', '\n', '\r')).ToList();
                }

                if (tb5.Text.Contains(".txt"))
                    using (StreamReader sr = new StreamReader(tb5.Text))
                    {
                        string str = sr.ReadToEnd();
                        UserAgents = str.Split('\n').Select(s => s.Trim(' ', ',', '\n', '\r')).Where(s => s != "").ToList();
                    }
            }
            catch (Exception)
            {
                return false;
            }

            bar.Minimum = 0;
            bar.Maximum = Links.Count;
            bd.IsEnabled = false;
            bw.IsEnabled = false;
            bf.IsEnabled = false;
            bef.IsEnabled = false;
            bs.IsEnabled = false;
            bua.IsEnabled = false;

            return true;
        }
        private void Restart()
        {
            bar.Value = bar.Minimum;
            counter = 0;
            error = 0;
            bd.IsEnabled = true;
            bw.IsEnabled = true;
            bf.IsEnabled = true;
            bef.IsEnabled = true;
            bs.IsEnabled = true;
            bua.IsEnabled = true;
            end = 0;
        }

        private async void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            if (tb1.Text == "")
            {
                MessageBox.Show("Вы не указали путь к списку доменов", "Ошибка");
                return;
            }
            if (tb2.Text == "")
            {
                MessageBox.Show("Вы не указали слова", "Ошибка");
                return;
            }
            if (tb3.Text == "")
            {
                MessageBox.Show("Вы не указали путь к файлу", "Ошибка");
                return;
            }
            if (tb4.Text == "")
            {
                MessageBox.Show("Вы не указали путь к файлу", "Ошибка");
                return;
            }

            if (!Init()) return;

            if (Links.Count == 0)
            {
                MessageBox.Show($"В файле {tb1.Text}\nСсылки не найдены!");
                return;
            }

            for (int i = 0; i < Links.Count; i++)
            {
                Logic(Links[i]);
            }
        }

        int end = 0;
        private async void Logic(string Links)
        {
            bool? result = await FindAsync(Links);
            if (result == true)
            {
                counter++;
            }
            else if (result == null)
            {
                error++;
            }

            end++;
            if(end == this.Links.Count)
            {
                MessageBox.Show($"В файл были добавлены {counter} доменов\n" +
                                $"{this.Links.Count - counter - error} ссылки не прошли проверку\n" +
                                $"Оставшееся {error} ссылки получили ошибку 401/403/404", "Готово!");
                
                Restart();
            }
        }


        private async Task<bool?> FindAsync(string Links)
        {
            bool? isTrue = false;
            try
            {
                HttpClient client = new HttpClient();
                int rand = new Random().Next(UserAgents.Count);
                client.DefaultRequestHeaders.Add("User-Agent", UserAgents[rand]);

                string link = Links;
                if (!link.ToLower().Contains("http"))
                {
                    link = "https://" + link;
                }

                HttpResponseMessage message = await client.GetAsync(link);
                string content = await message.Content.ReadAsStringAsync();

                if (Words == null)
                    Words = tb2.Text.Split(',').Select(s => s.Trim(' ')).ToList();
                Words = Words.Where(s => s != "").ToList();

                string domain = Links;
                if (Links.ToLower().Contains("http"))
                    domain = Links.Split('/')[2];

                if (Words.Any(s => content.ToLower().Contains(s.ToLower())))
                {
                    using (StreamWriter sw = new StreamWriter(tb3.Text, true))
                    {
                        sw.WriteLine(domain);
                        isTrue = true;
                    }
                }
                else
                {
                    using (StreamWriter sw = new StreamWriter(tb4.Text, true))
                    {
                        sw.WriteLine(domain);
                        if (!(message.StatusCode == HttpStatusCode.OK))
                        {
                            isTrue = null;
                        }
                    }
                }
            }
            catch {   }

            bar.Value++;
            return isTrue;
        }
    }
}
