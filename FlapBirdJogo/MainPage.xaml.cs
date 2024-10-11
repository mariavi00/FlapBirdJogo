namespace FlapBirdJogo;

public partial class MainPage : ContentPage
{
	const int gravidade=30;
	const int tempoEntreFrames=50;
	bool estaMorto=true;
	double larguraJanela=0;
	double alturaJanela=0;
	int velocidade=20;

	public MainPage()
	{
		InitializeComponent();
	}

	protected override void OnSizeAllocated(double w, double h)
	{
		base.OnSizeAllocated(w, h);
		larguraJanela=w;
		alturaJanela=h;
	}

	void GerenciaCanos()
	{
		ImagemCanoCima.TranslationX-=velocidade;
		ImagemCanoBaixo.TranslationX-=velocidade;

		if(ImagemCanoBaixo.TranslationX<-larguraJanela)
		{
			ImagemCanoBaixo.TranslationX=100;
			ImagemCanoCima.TranslationX=100;
		}
	}
	async Task Desenhar()
	{
		while(!estaMorto)
		{
			AplicaGravidade();
			await Task.Delay(tempoEntreFrames);

			GerenciaCanos();
		}
	}
	void AplicaGravidade()
	{
		ImagemFirebird.TranslationY+=gravidade;
	}
	void GameOverClicado(object s, TappedEventArgs a)
	{
		frameGameOver.IsVisible=false;
		Inicializar();
		Desenhar();
	}
	void Inicializar()
	{
		estaMorto=false;
		ImagemFirebird.TranslationY=0;
	}
}

