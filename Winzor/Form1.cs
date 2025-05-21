using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;

namespace Winzor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var services = new ServiceCollection();
            services.AddWindowsFormsBlazorWebView();

            services.AddSingleton<VolumeService>();
            blazorWebView1.Dock = DockStyle.Fill;
            blazorWebView1.HostPage = "wwwroot\\index.html";
            blazorWebView1.Services = services.BuildServiceProvider();
            blazorWebView1.RootComponents.Add<App>("#app");

            // Enable developer tools when the WebView2 is initialized
            blazorWebView1.BlazorWebViewInitialized += (sender, e) =>
            {
                e.WebView.CoreWebView2.Settings.AreDevToolsEnabled = true;
                // To open DevTools automatically during development
                e.WebView.CoreWebView2.OpenDevToolsWindow();
            };
        }
    }
}
