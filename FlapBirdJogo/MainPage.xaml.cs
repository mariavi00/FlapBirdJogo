namespace FlapBirdJogo;

public partial class MainPage : ContentPage
{
	const int gravidade=30;
	const int tempoEntreFrames=100;
	bool estaMorto=false;

	public MainPage()
	{
		InitializeComponent();
	}
	async Task Desenhar()
	{
		while(!estaMorto)
		{
			AplicaGravidade();
			await Task.Delay(tempoEntreFrames);
		}
	}
	void AplicaGravidade()
	{
		ImagemFirebird.TranslationY+=gravidade;
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
		Desenhar();
    }
}

