using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using clients.Models;
using System.Linq;
using System.Xml.Linq;
using System;
using System.IO;
using MsBox.Avalonia;
using Microsoft.VisualBasic.FileIO;

namespace clients;

public partial class AddClient : Window
{
    public Bitmap bitmapToBind;
    public string resultPhoto;
    public string filename;
    public string path;
    public string destPath;
    public bool ok;

    public int num;
    public AddClient()
    {
        InitializeComponent();
        maskTwo.IsVisible = true;
        num = 2;
        maskThree.IsVisible = false;
        maskFour.IsVisible = false;
    }


    public async void ClientImg_Click(object? sender, RoutedEventArgs args)
    {
        OpenFileDialog _openFileDialog = new OpenFileDialog();
        var result = await _openFileDialog.ShowAsync(this);
        if (result == null) return;
        path = string.Join("", result);
        resultPhoto = result.ToString();
        if (result != null)
        {
            bitmapToBind = new Bitmap(path);
        }
        imageClientAdd.Source = bitmapToBind;
        filename = Path.GetFileName(path);
        destPath = AppDomain.CurrentDomain.BaseDirectory.ToString() + "Assets";
        string destPathFile = Path.Combine(destPath, filename);


    }
    public void OkBtn_Click(object? sender, RoutedEventArgs args)
    {
        ok = true;
        Client client = new Client();
        switch (checkFIO(name.Text))
        {
            case true:
                client.NameClient = name.Text;
                break;
            case false:
                var box = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Поле имени содержит недопустимые символы");
                ok = false;
                box.ShowAsync();
                break;
        }
        switch (checkFIO(surname.Text))
        {
            case true:
                client.SurnameCl = surname.Text;
                break;
            case false:
                var box = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Поле фамилии содержит недопустимые символы");
                ok = false;
                box.ShowAsync();
                break;
        }
        switch (checkFIO(otchestvo.Text))
        {
            case true:
                client.OtchestvoCl = otchestvo.Text;
                break;
            case false:
                var box = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Поле отчества содержит недопустимые символы");
                ok = false;
                box.ShowAsync();
                break;
        }
        switch (checkMail(mail.Text))
        {
            case true:
                client.Mail = mail.Text;
                break;
            case false:
                var box = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Поле email не соответствует стандарту");
                ok = false;
                box.ShowAsync();
                break;
        }

        switch (num)
        {
            case 2:
                if (maskTwo.MaskFull == false)
                {
                    var box = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Введенный телефон не соответствует стандарту");
                    ok = false;
                    box.ShowAsync();
                }
                else
                {
                    client.Phone = maskTwo.Text;

                }
                break;
            case 3:
                if (maskThree.MaskFull == false)
                {
                    var box = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Введенный телефон не соответствует стандарту");
                    ok = false;
                    box.ShowAsync();
                }
                else
                {
                    client.Phone = maskThree.Text;

                }
                break;
            case 4:
                if (maskFour.MaskFull == false)
                {
                    var box = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Введенный телефон не соответствует стандарту");
                    ok = false;
                    box.ShowAsync();
                }
                else
                {
                    client.Phone = maskFour.Text;

                }
                break;

        }





        if (ok)
        {
            client.Birthday = DateOnly.FromDateTime(birthdateDP.SelectedDate.Value.Date);
            client.DateReg = DateOnly.FromDateTime(Convert.ToDateTime(DateTime.Now));
            client.Photo = filename;
            //   client.IdClient = Helper.User724Context.Clients.OrderBy(x => x.IdClient).Last().IdClient + 1;
            switch (male.IsChecked)
            {
                case true:
                    client.IdGender = 1;
                    break;
                case false:
                    switch (female.IsChecked)
                    {
                        case true:
                            client.IdGender = 2;
                            break;
                        case false:
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
            
            File.Move(path, destPath + "\\" + filename);
            Helper.User724Context.Clients.Add(client);
            Helper.User724Context.SaveChanges();
            client.IdTags.Add(Helper.User724Context.Tags.FirstOrDefault(x => x.IdTag == 1));
            Helper.User724Context.SaveChanges();

            switch (denied.IsChecked)
            {
                case false:
                    break;

                case true:
                    client.IdTags.Add(Helper.User724Context.Tags.FirstOrDefault(x => x.IdTag == 2));
                    Helper.User724Context.SaveChanges();
                    break;

            }

            switch (success.IsChecked)
            {
                case true:
                    client.IdTags.Add(Helper.User724Context.Tags.FirstOrDefault(x => x.IdTag == 1));
                    Helper.User724Context.SaveChanges();
                    break;
                case false:
                    break;
            }
            switch (success.IsChecked)
            {
                case true:
                    client.IdTags.Add(Helper.User724Context.Tags.FirstOrDefault(x => x.IdTag == 3));
                    Helper.User724Context.SaveChanges();
                    break;
                case false:
                    break;
            }

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();

        }



    }
    public void PhoneMask_SelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
    {
        switch (phoneMask.SelectedIndex.ToString())
        {
            case "0":
                maskTwo.IsVisible = true;
                maskThree.IsVisible = false;
                maskFour.IsVisible = false;
                break;
            case "1":
                maskTwo.IsVisible = false;
                maskThree.IsVisible = true;
                maskFour.IsVisible = false;
                break;
            case "2":
                maskTwo.IsVisible = false;
                maskThree.IsVisible = false;
                maskFour.IsVisible = true;
                break;
            case null:
            default:
                break;

        }

    }

    public bool checkFIO(string str)
    {
        bool res = true;

        foreach (char s in str)
        {
            if (!Char.IsLetter(s) && !Char.IsWhiteSpace(s) && s.ToString() != "-")
            {
                res = false;
                return res;
            }
        }

        if (str.Count() > 50)
        {
            res = false;
        }
        return res;
    }

    public bool checkMail(string str)
    {

        bool res = System.Text.RegularExpressions.Regex.IsMatch(str, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        return res;
    }




}