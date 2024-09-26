using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using System.IO;
using clients.Models;
using System.Collections.Generic;

namespace clients;

public partial class EditClient : Window
{
    public Bitmap bitmapToBind;
    public string resultPhoto;
    public string filename;
    public string path;
    public int index;
    public string destPath;
    public bool ok;
    public int maskSelect;
    public int num;
    public EditClient()
    {
        InitializeComponent();
        maskTwo.IsVisible = true;
        num = 2;
        maskThree.IsVisible = false;
        maskFour.IsVisible = false;
        index = Helper.editClInd;
        nameEdit.Text = Helper.User724Context.Clients.Where(x => x.IdClient == index).FirstOrDefault().NameClient;
        surnameEdit.Text = Helper.User724Context.Clients.Where(x => x.IdClient == index).FirstOrDefault().SurnameCl;
        lastnameEdit.Text = Helper.User724Context.Clients.Where(x => x.IdClient == index).FirstOrDefault().OtchestvoCl;
        mailEdit.Text = Helper.User724Context.Clients.Where(x => x.IdClient == index).FirstOrDefault().Mail;
        string phone = Helper.User724Context.Clients.Where(x => x.IdClient == index).FirstOrDefault().Phone;
        birthdateDP.SelectedDate = Helper.User724Context.Clients.Where(x => x.IdClient == index).FirstOrDefault().Birthday.Value.ToDateTime(new TimeOnly());
        switch (phone.Length)
        {
            case 14:
                maskSelect = 1;
                maskTwo.IsVisible = true;
                maskTwo.Text = phone;
                maskThree.IsVisible = false;
                maskFour.IsVisible = false;
                break;
            case 15:
                maskSelect = 2;
                maskTwo.IsVisible = false;
                maskThree.IsVisible = true;
                maskThree.Text = phone;
                maskFour.IsVisible = false;
                break;
            case 16:
                maskSelect = 3;
                maskTwo.IsVisible = false;
                maskThree.IsVisible = false;
                maskFour.IsVisible = true;
                maskFour.Text = phone;
                break;
        }

        switch (Helper.User724Context.Clients.Where(x => x.IdClient == index).FirstOrDefault().IdGender)
        {
            case 1:
                male.IsChecked = true;
                break;
            case 2:
                female.IsChecked = true;
                break;
        }
        List<Tag> tags = Helper.User724Context.Clients.Where(x => x.IdClient == index).FirstOrDefault().IdTags.ToList();
        foreach (Tag tag in tags)
        {
            foreach (Tag tagRef in Helper.User724Context.Tags)
            {
                if (tag == tagRef && tagRef.IdTag == 0)
                {
                    denied.IsChecked = true;
                }
                else if (tag == tagRef && tagRef.IdTag == 1)
                {
                    success.IsChecked = true;
                }
                else if (tag == tagRef && tagRef.IdTag == 2)
                {
                    inprocess.IsChecked = true;
                }
            }
        }

        imageClientEdit.Source = new Bitmap($"Assets/{Helper.User724Context.Clients.Where(x => x.IdClient == index).FirstOrDefault().Photo}");
       
    }

    public async void ClientImg_Click(object? sender, RoutedEventArgs args)
    {
        destPath = AppDomain.CurrentDomain.BaseDirectory.ToString() + "Assets";
        OpenFileDialog _openFileDialog = new OpenFileDialog();
        var result = await _openFileDialog.ShowAsync(this);
        if (result == null) return;
        path = string.Join("", result);
        resultPhoto = result.ToString();
        if (result != null)
        {
            bitmapToBind = new Bitmap(path);
        }
        imageClientEdit.Source = bitmapToBind;
        filename = Path.GetFileName(path);
        string destPathFile = Path.Combine(destPath, filename);

    }
    public void Nazad_Click(object? sender, RoutedEventArgs args)
    {
        MainWindow main = new MainWindow();
        main.Show();
        this.Close();
    }
        public void OkBtn_Click(object? sender, RoutedEventArgs args)
    {
        string prevfilename = Helper.User724Context.Clients.Where(x => x.IdClient == index).FirstOrDefault().Photo;
        destPath = AppDomain.CurrentDomain.BaseDirectory.ToString() + "Assets";
        File.Delete(Path.Combine(destPath, prevfilename));
        Client client = new Client();
        client.IdClient =Convert.ToInt32(idClient.Text);
        client.Photo = filename;
        client.NameClient = nameEdit.Text;
        client.SurnameCl = surnameEdit.Text;
        client.OtchestvoCl = lastnameEdit.Text;
        switch (maskSelect)
        {
            case 1:
                client.Phone = maskTwo.Text;
                break;
            case 2:
                client.Phone = maskThree.Text;
                break;
            case 3:
                client.Phone = maskFour.Text;
                break;
        }

        switch (male.IsChecked)
        {

            case true:
                client.IdGender = 1;
                break;
            case false:
            default:
                break;
        }
        switch (female.IsChecked)
        {
            case true:
                client.IdGender = 2;
                break;
            case false:
            default:
                break;
        }
        
        client.Birthday = DateOnly.FromDateTime(birthdateDP.SelectedDate.Value.DateTime);
        client.DateReg = Helper.User724Context.Clients.Where(x => x.IdClient == client.IdClient).FirstOrDefault().DateReg;
        client.ClientFiles = Helper.User724Context.Clients.Where(x => x.IdClient == client.IdClient).FirstOrDefault().ClientFiles;
        client.Visits = Helper.User724Context.Clients.Where(x => x.IdClient == client.IdClient).FirstOrDefault().Visits;

        MainWindow main = new MainWindow();
        main.Show();
        this.Close();

    }

}