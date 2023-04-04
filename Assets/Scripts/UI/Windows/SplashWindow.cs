using Cysharp.Threading.Tasks;

public class SplashWindow : Window
{
    protected override async void OnOpen(ViewParam viewParam)
    {
        await UniTask.Delay(2000);
        SoundManager.Play(Sound.MainMenu, true);
        WindowManager.Open<MainMenuWindow>();
    }

    protected override void OnClose()
    {
        
    }
}