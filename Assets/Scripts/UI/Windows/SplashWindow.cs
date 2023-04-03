using Cysharp.Threading.Tasks;

public class SplashWindow : Window
{
    public override async void OnOpen(ViewParam viewParam)
    {
        await UniTask.Delay(2000);
        WindowManager.Open<MainMenuWindow>();
    }

    public override void OnClose()
    {
        
    }
}