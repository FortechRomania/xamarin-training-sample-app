using System;
using System.Threading.Tasks;

namespace TrainingPreparation.Services;

public interface ILoginService
{
    Task LoginAsync(string emailAddress, string password);
}

public class LoginService : ILoginService
{
    public async Task LoginAsync(string emailAddress, string password)
    {
        await Task.Delay(TimeSpan.FromSeconds(5));

        if (emailAddress != "test@test.ro" || password != "password")
        {
            throw new Exception("Invalid Credentials");
        }
    }
}
