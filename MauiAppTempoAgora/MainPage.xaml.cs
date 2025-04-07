using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;
using System.Threading.Tasks;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            VerificarConexao();
        }

        private async Task VerificarConexao()
        {
            var acesso = Connectivity.Current.NetworkAccess;

            if(acesso != NetworkAccess.Internet)
            {
                await DisplayAlert("Erro | Sem conexão com a internet", "Por favor Verifique sua conexão a Internet e tente novamente", "OK");
            }
        }

        private async void btnBuscar(object sender, EventArgs e)
        {
            try
            {
                if(!string.IsNullOrEmpty(txtCidade.Text))
                {
                    Tempo? t = await DataService.GetPrevisao(txtCidade.Text);

                    if (t != null)
                    {
                        string dados_previsao = "";

                        dados_previsao = $"Cidade: {txtCidade.Text}\n" +
                                        $"Temperatura: {t.temp_max}°C\n" +
                                        $"Temperatura Mínima: {t.temp_min}°C\n" +
                                        $"Descrição: {t.description}\n" +
                                        $"Velocidade do Vento: {t.speed} m/s\n" +
                                        $"Visibilidade: {t.visibility} m\n" +
                                        $"Latitude: {t.lat}\n" +
                                        $"Longitude: {t.lon}\n" +
                                        $"Nascer do Sol: {t.sunrise}\n" +
                                        $"Por do Sol: {t.sunset}";

                        txtlblResultado.Text = dados_previsao;
                    }
                    else 
                    {
                        
                    }

                }
                else
                {
                    txtlblResultado.Text = "Digite uma cidade";
                }
            }
            catch(Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }
    }

}
