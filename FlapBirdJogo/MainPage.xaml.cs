namespace FlapBirdJogo;

public partial class MainPage : ContentPage
{
	const int gravidade = 10;
	const int tempoEntreFrames = 25;
	bool estaMorto = true;
	double larguraJanela = 0;
	double alturaJanela = 0;
	int velocidade = 10;
	const int maxTempoPulando = 3;
	int tempoPulando = 0;
	bool estaPulando = false;
	const int forcaPulo = 30;
	const int aberturaMinima =150;
	int pontuacao=0;

	public MainPage()
	{
		InitializeComponent();
	}

	protected override void OnSizeAllocated(double w, double h)
	{
		base.OnSizeAllocated(w, h);
		larguraJanela = w;
		alturaJanela = h;
	}

	void GerenciaCanos()
	{
		ImagemCanoCima.TranslationX -= velocidade;
		ImagemCanoBaixo.TranslationX -= velocidade;

		if (ImagemCanoBaixo.TranslationX <= -larguraJanela)
		{
			ImagemCanoBaixo.TranslationX = 100;
			ImagemCanoCima.TranslationX = 100;
			var alturaMax=-50;
			var alturaMin=-ImagemCanoBaixo.HeightRequest;
			ImagemCanoCima.TranslationY=Random.Shared.Next((int)alturaMin, (int)alturaMax);
			ImagemCanoBaixo.TranslationY=ImagemCanoCima.TranslationY+aberturaMinima+ImagemCanoBaixo.HeightRequest;
			pontuacao++;
			labelPontuacao.Text="Pontuação:"+pontuacao.ToString("D3");
		}
	}
	void AplicaPulo()
	{
		ImagemFirebird.TranslationY -= forcaPulo;
		tempoPulando++;
		if (tempoPulando >= maxTempoPulando)
		{
			estaPulando = false;
			tempoPulando = 0;
		}
	}
	async Task Desenhar()
	{
		while (!estaMorto)
		{
			if (estaPulando)
				AplicaPulo();
			else
				AplicaGravidade();

			GerenciaCanos();
			if (VerificaColisao())
			{
				estaMorto = true;
				frameGameOver.IsVisible = true;
				break;
			}
			await Task.Delay(tempoEntreFrames);
		}
	}
	void AplicaGravidade()
	{
		ImagemFirebird.TranslationY += gravidade;
	}
	bool VerificaColisaoTeto()
	{
		var minY = -alturaJanela / 2;
		if (ImagemFirebird.TranslationY <= minY)
			return true;
		else
			return false;
	}
	bool VerificaColisaoChao()
	{
		var maxY = alturaJanela / 2 - ImagemChao.HeightRequest;
		if (ImagemFirebird.TranslationY >= maxY)
			return true;
		else
			return false;
	}
	bool VerificaColisao()
	{
		if (!estaMorto)
		{
			if (VerificaColisaoTeto() || VerificaColisaoChao())
			{
				return true;
			}
		}
		return false;
	}
	void GameOverClicado(object s, TappedEventArgs a)
	{
		frameGameOver.IsVisible = false;
		Inicializar();
		Desenhar();
	}
	void GridClicado(object s, TappedEventArgs a)
	{
		estaPulando=true;
	}
	void Inicializar()
	{
		estaMorto = false;
		ImagemFirebird.TranslationY = 0;
	}
}

