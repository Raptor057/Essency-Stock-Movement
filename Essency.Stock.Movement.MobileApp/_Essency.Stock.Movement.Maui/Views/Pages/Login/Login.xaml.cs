using Essency.Stock.Movement.Maui.Interfaces;
using Microsoft.Extensions.Logging;

namespace Essency.Stock.Movement.Maui.Views.Pages.Login;

public partial class Login : ContentPage
{
    private readonly IAppUsers _users;
    private readonly ILogger<Login> _logger;

    public Login(IAppUsers users, ILogger<Login> logger)
	{
		InitializeComponent();
        _users=users;
        _logger=logger;
	}

	

	public async void ButtonLogin(object sender, EventArgs e)
	{
        try
        {
            // Obt�n los valores de los campos Entry
            string username = UsernameEntry.Text?.Trim();
            string password = PasswordEntry.Text;

            // Validaci�n b�sica
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Error", "Please enter both username and password.","OK");
                //ErrorMessageLabel.IsVisible = true;
                return;
            }

            // Llama al m�todo Login pas�ndole los datos
            bool isAuthenticated = await _users.Login(username, password).ConfigureAwait(false);

            if (isAuthenticated)
            {
                // Redirige a la p�gina principal si la autenticaci�n es exitosa
                //await Shell.Current.GoToAsync("//MainPage");
                await DisplayAlert("Message", "Welcome.", "OK)");
                _logger.LogInformation($"Login Success from user {username}");
            }
            else
            {
                await DisplayAlert("Error", "Invalid username or password.","OK)");
                _logger.LogInformation($"Login failure from user {username}");
                //ErrorMessageLabel.IsVisible = true;
            }

        }
        catch (Exception ex) 
        {
            await DisplayAlert("Error", $"{ex.Message}","OK");
        }
    }
}