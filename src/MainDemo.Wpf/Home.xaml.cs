using System.Configuration;
using MaterialDesignDemo.Domain;
using MaterialDesignThemes.Wpf;

namespace MaterialDesignDemo;

public partial class Home
{
    public Home() => InitializeComponent();

    private async void GitHubButton_OnClick(object sender, RoutedEventArgs e)
        => await DialogHost.Show(new Issue3428(), "RootDialog");

    private void TwitterButton_OnClick(object sender, RoutedEventArgs e)
        => Link.OpenInBrowser("https://twitter.com/James_Willock");

    private void ChatButton_OnClick(object sender, RoutedEventArgs e)
        => Link.OpenInBrowser("https://gitter.im/ButchersBoy/MaterialDesignInXamlToolkit");

    private void EmailButton_OnClick(object sender, RoutedEventArgs e)
        => Link.OpenInBrowser("mailto://james@dragablz.net");

    private void DonateButton_OnClick(object sender, RoutedEventArgs e)
        => Link.OpenInBrowser("https://opencollective.com/materialdesigninxaml");
}
