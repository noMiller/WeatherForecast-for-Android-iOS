
using System;
using System.Net.Http;
using Newtonsoft.Json.Linq;


namespace WeatherForecastAndroidiOS

{
    public partial class MainPage : ContentPage
    {
        private const string APIKey = "3a75bd2e9ee624791f19099a5687c9a3";
        private string city;
        private string Main;
        private string tempText;
        private int tempFull;
        private double RoundedTemp;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            city = CityTextBox.Text.Trim();
            HttpClient client = new HttpClient();
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={APIKey}&units=metric";
            string result = await client.GetStringAsync(url);

            var JsonResult = JObject.Parse(result);
            tempText = JsonResult["main"]["temp"].ToString();
            RoundedTemp = Double.Parse(tempText);
            int tempFull = (int)Math.Round(RoundedTemp);
            TempLabel.Text = "Температура: " + tempFull + "С";
            Main = JsonResult["weather"][0]["main"].ToString();

            switch (Main)
            {
                case "Clear":
                    MainImage.Source = "sun.png";
                    MainLabel.Text = "Погода: Солнечно";
                    break;

                case "Clouds":
                    MainImage.Source = "cloudy.png";
                    MainLabel.Text = "Погода: Облачно";
                    break;

                case "Rain":
                    MainImage.Source = "rain.png";
                    MainLabel.Text = "Погода: Дождь";
                    break;

                case "Snow":
                    MainImage.Source = "snow.png";
                    MainLabel.Text = "Погода: Снег";
                    break;
            }


        }
    }
}