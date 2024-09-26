using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;
using System.Collections.Generic;

namespace clients;

public partial class ClientVisit : Window
{
    List<DateTime> visits = new List<DateTime>();
    public ClientVisit()
    {
        InitializeComponent();
        visits = Helper.visits;
        visitsList.ItemsSource = visits;
    }

    public void nazad_Click(object? sender, RoutedEventArgs args)
    {
        MainWindow main = new MainWindow();
        main.Show();
        this.Close();


    }
        

}