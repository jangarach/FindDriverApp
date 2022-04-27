using Android.App;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
[assembly: Application(UsesCleartextTraffic = true)] 

[assembly: ExportFont("FontAwesomeFreeSolid900.otf", Alias = "FASolid")]
[assembly: ExportFont("FontAwesomeFreeRegular400.otf", Alias = "FARegular")]
[assembly: ExportFont("FontAwesomeBrandsRegular400.otf", Alias = "FABrands")]